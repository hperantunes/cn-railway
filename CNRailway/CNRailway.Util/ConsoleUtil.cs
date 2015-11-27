using System;
using System.IO;
using System.Linq;

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

        public char GetDestination()
        {
            var input = PromptDestination();
            var destination = input.ToCharArray().First();
            return destination;
        }

        public void ShowErrorMessage(string message)
        {
            WriteError(message);
            Wait();
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
