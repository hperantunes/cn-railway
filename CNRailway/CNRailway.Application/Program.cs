using CNRailway.MarshallingYard;
using CNRailway.Util;
using System.Linq;

namespace CNRailway.Application
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configuration = new Configuration();
            var idGenerator = new SequentialIdGenerator();

            var fileReader = new FileReader();
            var ui = new ConsoleUtil(configuration, fileReader);

            do
            {
                // Load sorting lines from file or prompt the user for file location
                var lines = ui.GetSortingLines();
                var yard = new Yard(idGenerator, configuration, lines);
                var yardmaster = yard.Initialize();

                // Show initial sorting lines
                ui.BeginSection();
                ui.ShowMessage("Initial sorting lines:");
                var sortingLines = yard.GetSortingLines().Select(line => line.ToString());
                ui.ShowList(sortingLines);

                // Prompt user for destination
                ui.BeginSection();
                var destination = ui.GetDestination();

                // Create navigational map and move cars
                var linesMap = yard.GetLinesMap(destination);
                var steps = yardmaster.AssembleTrain(linesMap);

                // Show list of steps
                ui.BeginSection();
                ui.ShowMessage("Movements:");
                ui.ShowList(steps.Select(step => step.ToString()));

                // Show total of movements
                steps.Select(step => step.ToString());
                ui.ShowMessage($"Total of movements: {steps.Count()}.");

                // Show final sorting lines
                ui.BeginSection();
                ui.ShowMessage("Final sorting lines:");
                sortingLines = yard.GetSortingLines().Select(line => line.ToString());
                ui.ShowList(sortingLines);

                // Show train line
                ui.BeginSection();
                ui.ShowMessage("Train line:");
                var trainLine = yard.TrainLine.ToString();
                ui.ShowList(new[] { trainLine });
                ui.ShowMessage($"Total of cars in train line: {trainLine.Count()}.");

            } while (!ui.HasUserChosenToExit());
        }
    }
}
