from __future__ import annotations

import argparse

from src.config.tables import tables
from src.Executors.SQLMigrateExecutor import SQLMigrateExecutor
from src.Executors.SQLTableExecutor import SQLTableExecutor
from src.library.EnvironmentLibrary import EnvironmentLibrary, EnvironmentVariables
from src.library.SqlServerConnector import ConnectionString

_CONNECTION_KEYS = (
    "SOURCE_SQLSERVER_HOST",
    "SOURCE_SQLSERVER_USER",
    "SOURCE_SQLSERVER_PASSWORD",
    "SOURCE_SQLSERVER_DATABASE",
    "TARGET_SQLSERVER_HOST",
    "TARGET_SQLSERVER_USER",
    "TARGET_SQLSERVER_PASSWORD",
    "TARGET_SQLSERVER_DATABASE",
)


def _connection_string(prefix: str, env: EnvironmentVariables) -> ConnectionString:
    return ConnectionString(
        host=env.get(f"{prefix}_SQLSERVER_HOST"),
        user=env.get(f"{prefix}_SQLSERVER_USER"),
        password=env.get(f"{prefix}_SQLSERVER_PASSWORD"),
        database=env.get(f"{prefix}_SQLSERVER_DATABASE"),
    )


def main() -> None:
    parser = argparse.ArgumentParser(description="Migrador SQL Server -> SQL Server")
    parser.add_argument("action", choices=["table", "migrate"], help="table: copia estructura, migrate: copia datos")
    parser.add_argument(
        "--batch-size", type=int, default=1000, dest="batch_size", help="Registros por lote (default 1000)"
    )
    parser.add_argument(
        "--drop-if-exists", action="store_true", dest="drop_if_exists", help="Recrea la tabla destino si existe"
    )
    args, _ = parser.parse_known_args()

    env = EnvironmentLibrary().get(*_CONNECTION_KEYS)
    source = _connection_string("SOURCE", env)
    target = _connection_string("TARGET", env)

    if args.action == "table":
        SQLTableExecutor(source, target, tables, drop_if_exists=args.drop_if_exists).run()
    else:
        SQLMigrateExecutor(source, target, tables, batch_size=args.batch_size).run()


if __name__ == "__main__":
    main()
