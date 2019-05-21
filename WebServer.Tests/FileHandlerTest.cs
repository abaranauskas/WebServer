using System;
using System.IO;
using Xunit;

namespace WebServer.Tests
{
    public class FileHandlerTest
    {      
        [Fact]
        public void ReturnsFileNotFoundException()
        {
            var expected = new FileReadResults()
            {
                FileBytes = File.ReadAllBytes(@"C:\Users\aidas\source\repos\WebServer\WebServer\TestFiles\Cat.png"),
                FileExtension = Path.GetExtension(@"C:\Users\aidas\source\repos\WebServer\WebServer\TestFiles\cat.png")
            };

            var rawUrl = "/cats.png";
            FileHandler fileHandler = new FileHandler(rawUrl);

            var actual = Record.Exception(() => fileHandler.GetFileReadResults());

            Assert.IsType<FileNotFoundException>(actual);
        }

        [Fact]
        public void ReturnsDictionaryNotFoundException()
        {
            var expected = new FileReadResults()
            {
                FileBytes = File.ReadAllBytes(@"C:\Users\aidas\source\repos\WebServer\WebServer\TestFiles\Cat.png"),
                FileExtension = Path.GetExtension(@"C:\Users\aidas\source\repos\WebServer\WebServer\TestFiles\cat.png")
            };

            var rawUrl = "/cats/cat.png";
            FileHandler fileHandler = new FileHandler(rawUrl);

            var actual = Record.Exception(() => fileHandler.GetFileReadResults());

            Assert.IsType<DirectoryNotFoundException>(actual);
        }

        [Fact]
        public void ReturnsNotNullWithExpectedValuesPngFile()
        {
            var expected = new FileReadResults()
            {
                FileBytes = File.ReadAllBytes(@"C:\Users\aidas\source\repos\WebServer\WebServer\TestFiles\Cat.png"),
                FileExtension = Path.GetExtension(@"C:\Users\aidas\source\repos\WebServer\WebServer\TestFiles\cat.png")
            };

            var rawUrl = "/cat.png";
            FileHandler fileHandler = new FileHandler(rawUrl);

            FileReadResults actual = fileHandler.GetFileReadResults();

            Assert.NotNull(actual);
            Assert.Equal(actual.FileBytes, expected.FileBytes);
            Assert.Equal(actual.FileExtension, expected.FileExtension);
        }

        [Fact]
        public void ReturnsNotNullWithExpectedValuesTextFile()
        {
            var expected = new FileReadResults()
            {
                FileBytes = File.ReadAllBytes(@"C:\Users\aidas\source\repos\WebServer\WebServer\TestFiles\myfile.txt"),
                FileExtension = Path.GetExtension(@"C:\Users\aidas\source\repos\WebServer\WebServer\TestFiles\myfile.txt")
            };

            var rawUrl = "/myfile.txt";
            FileHandler fileHandler = new FileHandler(rawUrl);

            FileReadResults actual = fileHandler.GetFileReadResults();

            Assert.NotNull(actual);
            Assert.Equal(actual.FileBytes, expected.FileBytes);
            Assert.Equal(actual.FileExtension, expected.FileExtension);
        }

        [Fact]
        public void ReturnsNotNullWithUnexpectedValues()
        {
            var expected = new FileReadResults()
            {
                FileBytes = File.ReadAllBytes(@"C:\Users\aidas\source\repos\WebServer\WebServer\TestFiles\Cat.png"),
                FileExtension = Path.GetExtension(@"C:\Users\aidas\source\repos\WebServer\WebServer\TestFiles\cat.png")
            };

            var rawUrl = "/myfile.txt";
            FileHandler fileHandler = new FileHandler(rawUrl);

            FileReadResults actual = fileHandler.GetFileReadResults();

            Assert.NotNull(actual);
            Assert.NotEqual(actual.FileBytes, expected.FileBytes);
            Assert.NotEqual(actual.FileExtension, expected.FileExtension);
        }

        [Fact]
        public void ValidateValidTextFileName()
        {
            var rawUrl = "/fileName.txt";
            FileHandler fileHandler = new FileHandler(rawUrl);

            bool isFileNameValid = fileHandler.IsFileNameValid;

            Assert.True(isFileNameValid);
        }

        [Fact]
        public void ValidateValidPngFileName()
        {
            var rawUrl = "/anotherDileName.Png";
            FileHandler fileHandler = new FileHandler(rawUrl);

            bool isFileNameValid = fileHandler.IsFileNameValid;

            Assert.True(isFileNameValid);
        }

        [Fact]
        public void ValidateInvalidDocFileName()
        {
            var rawUrl = "/wordDocument.doc";
            FileHandler fileHandler = new FileHandler(rawUrl);

            bool isFileNameValid = fileHandler.IsFileNameValid;

            Assert.False(isFileNameValid);
        }

        [Fact]
        public void ValidateInvalidAnyText()
        {
            var rawUrl = "/JustAnyTest";
            FileHandler fileHandler = new FileHandler(rawUrl);

            bool isFileNameValid = fileHandler.IsFileNameValid;

            Assert.False(isFileNameValid);
        }
    }
}
