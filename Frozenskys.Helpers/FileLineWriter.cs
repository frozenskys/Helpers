namespace Frozenskys.Helpers
{
    using System.IO;

    public class FileLineWriter
    {
        private string _filename = "";
        public FileLineWriter(string filename)
        {
            _filename = filename;
        }
        public void WriteLine(string line)
        {
            using (StreamWriter file = new StreamWriter(_filename, true))
            {
                file.WriteLine(line);
            }
        }
    }
}
