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
            try
            {
                return GetSortingLines(false);
            }
            catch (ArgumentException e)
            {
                ShowErrorMessage(e.Message);
                return GetSortingLines(true);
            }
        }

        private IEnumerable<IEnumerable<char>> GetSortingLines(bool forcePromptFileLocation)
        {
            string path;
            if (Configuration.UseDefaultFileLocation && !forcePromptFileLocation)
            {
                path = Path.Combine(Configuration.DefaultDirectory, Configuration.DefaultFileName);
            }
            else
            {
                path = GetFullFilePath();
            }
            var fileContents = FileReader.ReadFrom(path);

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

        public void ShowList(IEnumerable<string> list)
        {
            foreach (var item in list)
            {
                Write(string.Concat(Constants.UI.ItemListBullet, item));
            }
        }

        public void BeginSection()
        {
            Write(string.Concat(new string(Constants.UI.SectionSeparator, Constants.UI.SectionSeparatorSize), Environment.NewLine));
        }

        public string GetFullFilePath()
        {
            var directory = GetValue(Constants.Labels.Directory);
            var file = GetValue(Constants.Labels.FileName);

            return Path.Combine(directory, file);
        }

        public void Wait()
        {
            ReadKey();
        }

        public bool HasUserChosenToExit()
        {
            BeginSection();
            Write("Press the ESCAPE to exit or any other key to restart the process...");
            return Console.ReadKey(true).Key == ConsoleKey.Escape;
        }

        private string GetValue(string label)
        {
            var fileName = PromptNewValue(label);
            if (string.IsNullOrWhiteSpace(fileName))
            {
                WriteError($"Invalid {label}.");
                return GetValue(label);
            }

            return fileName;
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

        private void ReadKey()
        {
            Console.ReadKey();
        }

        private string PromptNewValue(string label)
        {
            Write($"Set a new {label}:");
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

            return input.ToUpper();
        }

    }
}
