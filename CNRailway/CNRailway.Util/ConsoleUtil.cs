using System;
using System.Configuration;
using System.IO;

namespace CNRailway.Util
{
    public class ConsoleUtil : IUserInterface
    {
        private const string DIRECTORY_LABEL = "Directory";
        private const string FILENAME_LABEL = "File Name";

        private readonly string DefaultDirectory = ConfigurationManager.AppSettings["DefaultDirectoryPath"];
        private readonly string DefaultFileName = ConfigurationManager.AppSettings["DefaultFileName"];

        public string GetDirectoryPath()
        {
            ShowCurrentValue(DIRECTORY_LABEL, DefaultDirectory);
            var path = PromptNewValue(DIRECTORY_LABEL);

            return string.IsNullOrWhiteSpace(path) ? DefaultDirectory : path;
        }

        public string GetFileName()
        {
            ShowCurrentValue(FILENAME_LABEL, DefaultFileName);
            var fileName = PromptNewValue(FILENAME_LABEL);

            return string.IsNullOrWhiteSpace(fileName) ? DefaultFileName : fileName;
        }

        public string GetFullFilePath()
        {
            var directory = GetDirectoryPath();
            var file = GetFileName();

            return Path.Combine(directory, file);
        }

        public void ShowErrorMessage(string message)
        {
            Console.Error.WriteLine(message);
            Console.ReadKey();
        }

        private void ShowCurrentValue(string label, string value)
        {
            Console.WriteLine($"{label} is currently set as {value}.");
        }

        private string PromptNewValue(string label)
        {
            Console.WriteLine($"Set a new {label} or press ENTER to keep:");
            return Console.ReadLine();
        }

    }
}
