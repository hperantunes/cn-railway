﻿using CNRailway.MarshallingYard;
using CNRailway.Util;

namespace CNRailway.Application
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configuration = new Configuration();
            var idGenerator = new SequentialIdGenerator();

            IFileReader fileReader = new FileReader();
            IUserInterface ui = new ConsoleUtil(configuration, fileReader);

            var yard = new Yard(idGenerator, configuration);

            var lines = ui.GetSortingLines();
            var yardmaster = yard.Initialize(lines);

            var destination = ui.GetDestination();

            var linesMap = yard.GetLinesMap(destination);

            var steps = yardmaster.AssembleTrain(linesMap);

            foreach (var step in steps)
            {
                ui.ShowMessage(step.ToString());
            }

            ui.Wait();
        }
    }
}
