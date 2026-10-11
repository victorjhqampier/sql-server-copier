class CreateCqlFileHanlder:
    def __init__(self, file_path):
        self.file_path = file_path

    def create_file(self, content):
        with open(self.file_path, 'w') as f:
            f.write(content)