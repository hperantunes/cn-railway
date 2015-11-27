using CNRailway.Util;
using System;
using System.Collections.Generic;

namespace CNRailway.Application
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configuration = new Configuration();

            IUserInterface ui = new ConsoleUtil(configuration);
            var file = ui.GetFullFilePath();

            IFileReader reader = new FileReader();

            IEnumerable<IEnumerable<char>> lines;
            try
            {
                lines = reader.ReadFrom(file);
            }
            catch (ArgumentException e)
            {
                ui.ShowErrorMessage(e.Message);
                return;
            }

            var idGenerator = new SequentialIdGenerator();
            var yard = new MarshallingYard.MarshallingYard(idGenerator, configuration);
            var yardmaster = yard.InitializeYard(lines);
        }
    }
}
