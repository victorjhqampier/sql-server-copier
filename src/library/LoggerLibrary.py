import logging


class LoggerLibrary:
    @staticmethod
    def set_logger() -> logging.Logger:
        migrator_logger = logging.getLogger("dp-migrate-sqlserver")
        migrator_logger.setLevel(logging.INFO)

        if not migrator_logger.handlers:
            console_handler = logging.StreamHandler()
            console_handler.setLevel(logging.INFO)
            formatter = logging.Formatter("%(asctime)s - %(name)s - %(levelname)s - %(message)s")
            console_handler.setFormatter(formatter)
            migrator_logger.addHandler(console_handler)

        migrator_logger.propagate = False
        return migrator_logger
