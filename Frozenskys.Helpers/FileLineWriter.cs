namespace Frozenskys.Helpers
{
    using System.Collections.Generic;
    using System.IO.Abstractions;

    public class FileLineWriter
    {
        private readonly string _filename = "";
        private readonly IFileSystem _fileSystem;

        public FileLineWriter(string filename)
        {
            _filename = filename;
            _fileSystem = new FileSystem();
        }

        public FileLineWriter(IFileSystem fileSystem, string filename)
        {
            _filename = filename;
            _fileSystem = fileSystem;
        }

        public void WriteLine(string line)
        {
            var lines = new List<string> { line };
            _fileSystem.File.AppendAllLines(_filename, lines);
        }
    }
}
