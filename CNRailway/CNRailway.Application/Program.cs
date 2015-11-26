using System;
using System.IO;
using System.Configuration;

namespace CNRailway.Application
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // directory path
            var directoryPath = ConfigurationManager.AppSettings["DefaultDirectoryPath"];
            Console.WriteLine($"Directory is currently set as {directoryPath}.");
            Console.WriteLine("Set a new directory or press ENTER to keep:");
            var inputDirectoryPath = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(inputDirectoryPath)) {
                directoryPath = inputDirectoryPath;
            }

            // file name
            var fileName = ConfigurationManager.AppSettings["DefaultFileName"];
            Console.WriteLine($"File name is currently set as {fileName}.");
            Console.WriteLine("Set a new file name or press ENTER to keep:");
            var inputFileName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(inputFileName))
            {
                fileName = inputFileName;
            }

            // read file
            var path = Path.Combine(directoryPath, fileName);
            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    Console.WriteLine(reader.ReadToEnd());
                }
            }
            catch (FileNotFoundException)
            {
                Console.Error.WriteLine($"Can not find file {fileName}.");
            }
            catch (DirectoryNotFoundException)
            {
                Console.Error.WriteLine("Invalid directory in the file path.");
            }
            catch (IOException)
            {
                Console.Error.WriteLine($"Can not open the file {fileName}");
            }
            finally
            {
                Console.ReadKey();
            }
        }
    }
}
