from __future__ import annotations

from dataclasses import dataclass
from typing import Any

from cassandra.auth import PlainTextAuthProvider
from cassandra.cluster import Cluster, Session

from src.library.LoggerLibrary import LoggerLibrary


@dataclass(frozen=True)
class ScylladbConnectionString:
    """Parametros necesarios para abrir una sesion contra ScyllaDB."""

    contact_points: tuple[str, ...]
    port: int = 9042
    username: str | None = None
    password: str | None = None
    keyspace: str | None = None

    def __post_init__(self) -> None:
        contact_points = tuple(str(point).strip() for point in self.contact_points if str(point).strip())
        if not contact_points:
            raise ValueError("contact_points no puede estar vacio")
        if not (0 < self.port <= 65535):
            raise ValueError(f"port invalido: {self.port}")
        if bool(self.username) != bool(self.password):
            raise ValueError("username y password deben configurarse juntos")
        object.__setattr__(self, "contact_points", contact_points)

    @property
    def connection_key(self) -> str:
        keyspace = self.keyspace or "sin-keyspace"
        return f"{','.join(self.contact_points)}:{self.port}/{keyspace}"


class ScylladbConnector:
    """Administra exclusivamente el ciclo de vida de sesiones ScyllaDB."""

    def __init__(self, logger: Any = None) -> None:
        self._logger = logger or LoggerLibrary.set_logger().getChild(self.__class__.__name__)
        self._connections: dict[ScylladbConnectionString, tuple[Cluster, Session]] = {}

    def get_session(self, connection_string: ScylladbConnectionString) -> Session:
        connection = self._connections.get(connection_string)
        if connection is not None and self.is_connected(connection_string):
            return connection[1]

        if connection is not None:
            self.close(connection_string)

        cluster_kwargs: dict[str, Any] = {
            "contact_points": connection_string.contact_points,
            "port": connection_string.port,
        }
        if connection_string.username:
            cluster_kwargs["auth_provider"] = PlainTextAuthProvider(
                username=connection_string.username,
                password=connection_string.password or "",
            )

        cluster = Cluster(**cluster_kwargs)
        try:
            session = cluster.connect(connection_string.keyspace)
        except Exception:
            cluster.shutdown()
            raise

        self._connections[connection_string] = (cluster, session)
        self._logger.info(f"Sesion ScyllaDB abierta: {connection_string.connection_key}")
        return session

    def connect(self, connection_string: ScylladbConnectionString) -> Session:
        return self.get_session(connection_string)

    def is_connected(self, connection_string: ScylladbConnectionString) -> bool:
        connection = self._connections.get(connection_string)
        if connection is None:
            return False

        cluster, session = connection
        return not self._is_shutdown(cluster) and not self._is_shutdown(session)

    def close(self, connection_string: ScylladbConnectionString) -> None:
        connection = self._connections.pop(connection_string, None)
        if connection is None:
            return

        cluster, session = connection
        try:
            session.shutdown()
        except Exception:
            self._logger.warning(
                f"Error al cerrar sesion ScyllaDB: {connection_string.connection_key}",
                exc_info=True,
            )
        finally:
            try:
                cluster.shutdown()
                self._logger.info(f"Sesion ScyllaDB cerrada: {connection_string.connection_key}")
            except Exception:
                self._logger.warning(
                    f"Error al cerrar cluster ScyllaDB: {connection_string.connection_key}",
                    exc_info=True,
                )

    def close_all(self) -> None:
        for connection_string in list(self._connections):
            self.close(connection_string)

    @staticmethod
    def _is_shutdown(resource: Any) -> bool:
        state = getattr(resource, "is_shutdown", False)
        return bool(state() if callable(state) else state)

    def __enter__(self) -> "ScylladbConnector":
        return self

    def __exit__(self, *exc: Any) -> None:
        self.close_all()
