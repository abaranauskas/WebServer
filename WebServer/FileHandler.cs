using System;
using System.IO;
using System.Text.RegularExpressions;

namespace WebServer
{
    public class FileHandler
    {
        private string _fileName;
        private string _dirrectoryPath;

        public FileHandler(string rawUrl)
        {
            _fileName = rawUrl.Remove(0, 1);
            _dirrectoryPath = Path.GetFullPath(Path.Combine(System.IO.Directory.GetCurrentDirectory(), @"..\..\..\TestFiles\"));
            IsFileNameValid = ValidateFileName();          
        }

        public bool IsFileNameValid { get; private set; }
        public bool FileExist { get; private set; }

        public FileReadResults GetFileReadResults()
        {
            string directoryPath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\TestFiles\"));
            string filePath = String.Concat(directoryPath, _fileName);
            byte[] bytes = File.ReadAllBytes(filePath);
            string fileExtension = Path.GetExtension(filePath);

            return new FileReadResults() { FileBytes = bytes, FileExtension = fileExtension, FileName= _fileName };
        }

        private bool ValidateFileName()
        {
            Regex rx = new Regex(@"([a-zA-Z0-9\s_\\.\-\(\):])+(.txt|.png)$", RegexOptions.IgnoreCase);
            MatchCollection matches = rx.Matches(_fileName);
            return matches.Count != 0;
        }       
    }
}