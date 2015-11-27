using CNRailway.Util;
using System;
using System.Collections.Generic;
using CNRailway.MarshallingYard;

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

            var yard = new MarshallingYard.MarshallingYard(idGenerator, configuration);

            var lines = ui.GetSortingLines();
            var yardmaster = yard.Initialize(lines);

            var destination = ui.GetDestination();
            var linesMap = yard.GetLinesMap(destination);

            var steps = yardmaster.AssembleTrain(linesMap);

            foreach (var step in steps)
            {
                ui.ShowMessage(step.ToString());
            }
        }
    }
}
