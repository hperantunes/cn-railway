using System;
using System.Collections.Generic;
using System.Linq;

namespace CNRailway.MarshallingYard
{
    public class LinesMap : ILinesMap
    {
        private IDictionary<ISortingLine, List<int>> SortingLinesMap { get; set; }

        private ILine TrainLine { get; set; }

        public LinesMap(ILine trainLine, IEnumerable<ISortingLine> sortingLines, char destination)
        {
            TrainLine = trainLine;

            // Creates a dictionary where the key is a sorting line and the value is 
            // a list of depths of cars that match the desired destination.
            SortingLinesMap = sortingLines
                                .ToDictionary(line => line, line => line.GetPositions(destination)
                                .Select(position => line.Count - 1 - position)
                                .ToList());
        }

        public void UpdateDepths(ISortingLine line, int amount)
        {
            if (line == null)
            {
                return;
            }

            var depths = SortingLinesMap[line].Select(depth => depth += amount).ToList();
            SortingLinesMap[line].Clear();
            SortingLinesMap[line].AddRange(depths.Where(depth => depth >= 0));
        }

        public Tuple<IDecrementableLine, IIncrementableLine, int> GetInstruction()
        {
            // There are no cars to the correct destination in any sorting
            // lines, return no instruction
            if (!SortingLinesMap.Any(kvp => kvp.Value.Any()))
            {
                return null;
            }

            // If there are cars on the top of a sorting line, move them to 
            // the train line
            var readyCars = GetReadyCars();
            if (readyCars != null)
            {
                return new Tuple<IDecrementableLine, IIncrementableLine, int>(readyCars.Item1, TrainLine, readyCars.Item2);
            }

            // Order sorting lines by having a car to the correct destination 
            // closest to the top
            var lines = SortingLinesMap.Where(kvp => kvp.Value.Any()).OrderBy(kvp => kvp.Value.FirstOrDefault());

            // Work with the first sorting line
            var line = lines.First();

            // Depth is also the amount of cars to move out of the way 
            var amount = line.Value.First();

            // Find closest sorting line with enough room
            var destination = FindSuitableDestinationLine(line.Key.Id, amount);

            // There is no suitable destination for any cars, return no instruction
            if (destination == null)
            {
                return null;
            }

            // Return instruction to move cars to a new sorting line
            return new Tuple<IDecrementableLine, IIncrementableLine, int>(line.Key, destination.Item1, destination.Item2);
        }

        private Tuple<IDecrementableLine, int> GetReadyCars()
        {
            // Lines with a car to the correct destination on top (depth of a car is zero)
            var lines = SortingLinesMap.Where(kvp => kvp.Value.Contains(0));

            // No lines are ready
            if (!lines.Any())
            {
                return null;
            }

            // If multiple lines have cars with the correct destination on top, it 
            // does not matter which line will be selected
            var line = lines.First();

            // Count how many cars with the correct destination are in sequence on top
            var depths = line.Value;
            var amount = 1;
            for (var i = 0; i < depths.Count - 1; i++)
            {
                // If the next car with the correct destination is not in sequence, 
                // stop counting
                if (depths[i + 1] - depths[i] > 1)
                {
                    break;
                }
                amount++;
            }

            // Return origin line and amount of cars ready to remove
            return new Tuple<IDecrementableLine, int>(line.Key, amount);
        }

        private Tuple<IIncrementableLine, int> FindSuitableDestinationLine(int sortingLineId, int slots)
        {
            // Get all other sorting lines that are not full, ordered
            // by the Id closest to the id of the original line
            var lines = SortingLinesMap
                .Where(kvp => !sortingLineId.Equals(kvp.Key.Id))
                .Where(kvp => !kvp.Key.IsFull)
                .OrderBy(kvp => Math.Abs(sortingLineId - kvp.Key.Id));

            // No suitable lines
            if (!lines.Any()) {
                return null;
            }

            // Return sorting line and how many cars should be moved to it
            var line = lines.First().Key;
            var amount = line.OpenSlots < slots ? line.OpenSlots : slots;

            return new Tuple<IIncrementableLine, int>(line, amount);
        }
    }
}
