using System;
using System.Configuration;
using System.IO;

namespace CNRailway.Util
{
    public class ConsoleUtil : IUserInterface
    {
        private IConfiguration Configuration { get; set; }

        public ConsoleUtil(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public string GetDirectoryPath()
        {
            ShowCurrentValue(Constants.Directory, Configuration.DefaultDirectory);
            var path = PromptNewValue(Constants.Directory);

            return string.IsNullOrWhiteSpace(path) ? Configuration.DefaultDirectory : path;
        }

        public string GetFileName()
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
