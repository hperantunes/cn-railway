using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace CNRailway.Util
{
    public class ConsoleUtil : IUserInterface
    {
        private IConfiguration Configuration { get; set; }

        private IFileReader FileReader { get; set; }

        public ConsoleUtil(IConfiguration configuration, IFileReader fileReader)
        {
            Configuration = configuration;
            FileReader = fileReader;
        }

        public char GetDestination()
        {
            var input = PromptDestination();
            var destination = input.ToCharArray().First();
            return destination;
        }

        public IEnumerable<IEnumerable<char>> GetSortingLines()
        {
            var path = GetFullFilePath();

            IEnumerable<IEnumerable<char>> fileContents;
            try
            {
                fileContents = FileReader.ReadFrom(path);
            }
            catch (ArgumentException e)
            {
                ShowErrorMessage(e.Message);
                return Enumerable.Empty<IEnumerable<char>>();
            }

            return fileContents;
        }

        public void ShowErrorMessage(string message)
        {
            WriteError(message);
            Wait();
        }

        public void ShowMessage(string message)
        {
            Write(message);
        }

        private string GetDirectoryPath()
        {
            ShowCurrentValue(Constants.Directory, Configuration.DefaultDirectory);
            var path = PromptNewValue(Constants.Directory);

            return string.IsNullOrWhiteSpace(path) ? Configuration.DefaultDirectory : path;
        }

        private string GetFileName()
        {
            ShowCurrentValue(Constants.FileName, Configuration.DefaultFileName);
            var fileName = PromptNewValue(Constants.FileName);

            return string.IsNullOrWhiteSpace(fileName) ? Configuration.DefaultFileName : fileName;
        }

        public string GetFullFilePath()
        {
            var directory = GetDirectoryPath();
            var file = GetFileName();

            return Path.Combine(directory, file);
        }

        private void Write(string message)
        {
            Console.WriteLine(message);
        }

        private void WriteError(string message)
        {
            Console.Error.WriteLine(message);
        }

        private string Read()
        {
            return Console.ReadLine();
        }

        private void Wait()
        {
            Console.ReadKey();
        }

        private void ShowCurrentValue(string label, string value)
        {
            Write($"{label} is currently set as {value}.");
        }

        private string PromptNewValue(string label)
        {
            Write($"Set a new {label} or press ENTER to keep:");
            return Read();
        }

        private string PromptDestination()
        {
            Write("Type one character that represents the desired destination and press ENTER:");

            var input = Read();
            if (string.IsNullOrWhiteSpace(input))
            {
                WriteError("Invalid input.");
                return PromptDestination();
            }

            return input;
        }

    }
}
