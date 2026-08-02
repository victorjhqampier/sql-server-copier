from __future__ import annotations

from dataclasses import dataclass
from typing import Any

import pymssql

from src.library.LoggerLibrary import LoggerLibrary


@dataclass(frozen=True)
class ConnectionString:
    """Parametros de conexion a SQL Server. Es la entrada del SqlServerConnector."""

    host: str
    user: str
    password: str
    database: str
    port: int = 1433
    charset: str = "utf8"
    login_timeout: int = 15

    def __post_init__(self) -> None:
        if not self.host or not str(self.host).strip():
            raise ValueError("host no puede estar vacio")
        if not self.user or not str(self.user).strip():
            raise ValueError("user no puede estar vacio")
        if not self.database or not str(self.database).strip():
            raise ValueError("database no puede estar vacia")
        if not (0 < self.port <= 65535):
            raise ValueError(f"port invalido: {self.port}")

    @property
    def connection_key(self) -> str:
        return f"{self.host}:{self.port}/{self.user}/{self.database}"

    def as_pymssql_kwargs(self) -> dict[str, Any]:
        return {
            "server": self.host,
            "port": self.port,
            "user": self.user,
            "password": self.password,
            "database": self.database,
            "charset": self.charset,
            "login_timeout": self.login_timeout,
            "autocommit": False,
        }


class SqlServerConnector:
    """Unico punto que abre y mantiene conexiones con SQL Server.

    Administra una conexion por ConnectionString, permitiendo mantener
    varias bases de datos abiertas a la vez (origen, destino, etc.).
    Reutiliza la conexion existente y la reconecta si se perdio.
    """

    def __init__(self, logger: Any = None) -> None:
        self._logger = logger or LoggerLibrary.set_logger().getChild(self.__class__.__name__)
        self._connections: dict[str, pymssql.Connection] = {}

    def _is_connection_alive(self, connection: pymssql.Connection) -> bool:
        """Verifica si la conexion sigue activa usando SELECT 1 (mas confiable que ping)."""
        try:
            cursor = connection.cursor()
            cursor.execute("SELECT 1")
            cursor.fetchone()
            cursor.close()
            return True
        except Exception:
            return False

    def get_connection(self, connection_string: ConnectionString) -> pymssql.Connection:
        key = connection_string.connection_key
        connection = self._connections.get(key)

        if connection is not None:
            if self._is_connection_alive(connection):
                return connection
            self._logger.warning(f"Conexion caida, reconectando: {key}")
            self._disconnect(key)

        connection = pymssql.connect(**connection_string.as_pymssql_kwargs())
        self._connections[key] = connection
        self._logger.info(f"Conexion abierta: {key}")
        return connection

    def connect(self, connection_string: ConnectionString) -> pymssql.Connection:
        return self.get_connection(connection_string)

    def is_connected(self, connection_string: ConnectionString) -> bool:
        connection = self._connections.get(connection_string.connection_key)
        if connection is None:
            return False
        return self._is_connection_alive(connection)

    def close(self, connection_string: ConnectionString) -> None:
        self._disconnect(connection_string.connection_key)

    def close_all(self) -> None:
        had_connections = bool(self._connections)
        for key in list(self._connections):
            self._disconnect(key)
        if had_connections:
            self._logger.info("Todas las conexiones cerradas")

    def _disconnect(self, key: str) -> None:
        connection = self._connections.pop(key, None)
        if connection is None:
            return
        try:
            connection.close()
            self._logger.info(f"Conexion cerrada: {key}")
        except Exception:
            self._logger.warning(f"Error al cerrar conexion {key}", exc_info=True)

    def __enter__(self) -> "SqlServerConnector":
        return self

    def __exit__(self, *exc: Any) -> None:
        self.close_all()
