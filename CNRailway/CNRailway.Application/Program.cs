using CNRailway.MarshallingYard;
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

            var lines = ui.GetSortingLines();

            var yard = new Yard(idGenerator, configuration, lines);

            var yardmaster = yard.Initialize();

            var destination = ui.GetDestination();

            var linesMap = yard.GetLinesMap(destination);

            var steps = yardmaster.AssembleTrain(linesMap);

            foreach (var step in steps)
            {
                ui.ShowMessage(step.ToString());
            }

            ui.ShowMessage("No movements left. Press any key to exit...");
            ui.Wait();
        }
    }
}
