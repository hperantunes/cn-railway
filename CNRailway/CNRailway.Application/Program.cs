using System;
using CNRailway.Util;
using System.Linq;

namespace CNRailway.Application
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IUserInterface ui = new ConsoleUtil();
            var file = ui.GetFullFilePath();

            IFileReader reader = new FileReader();
            var lines = reader.ReadFrom(file);

            lines.ToList().ForEach(line => Console.WriteLine(line));
        }
    }
}
