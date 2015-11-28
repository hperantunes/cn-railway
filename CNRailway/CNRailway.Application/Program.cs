using CNRailway.MarshallingYard;
using CNRailway.Util;
using System.Linq;
using System;

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
                ui.ShowMessage("Welcome to the Marshalling Yard!");

                // Load sorting lines from file or prompt the user for file location
                ui.BeginSection();
                var lines = ui.GetSortingLines();
                IYard yard = null;
                try
                {
                    yard = new Yard(idGenerator, configuration, lines);
                }
                catch (InvalidOperationException)
                {
                    var message = "Cannot create marshalling yard! Check the application's parameters.";
                    ui.ShowErrorMessage(message);
                    break;
                }

                var yardmaster = yard.Initialize();

                // Show initial sorting lines
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
