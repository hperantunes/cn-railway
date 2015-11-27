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
            var filePath = ui.GetFullFilePath();

            IFileReader fileReader = new FileReader();
            IEnumerable<IEnumerable<char>> fileContents;
            try
            {
                fileContents = fileReader.ReadFrom(filePath);
            }
            catch (ArgumentException e)
            {
                ui.ShowErrorMessage(e.Message);
                return;
            }

            var idGenerator = new SequentialIdGenerator();
            var yard = new MarshallingYard.MarshallingYard(idGenerator, configuration);
            var yardmaster = yard.Initialize(fileContents);

            var destination = ui.GetDestination();

            var steps = yardmaster.AssembleTrainToDestination(destination);
        }
    }
}
