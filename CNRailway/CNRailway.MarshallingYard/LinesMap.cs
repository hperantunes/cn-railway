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

            // Creates a dictionary where the key is a sorting line and the value is a list of depths
            // of cars that match the desired destination.
            SortingLinesMap = sortingLines.ToDictionary(key => key, value => value.GetPositions(destination).Select(position => value.Count - 1 - position).ToList());
        }

        public void UpdateDepths(ISortingLine line, int amount)
        {
            var depths = SortingLinesMap[line].Select(depth => depth += amount).ToList();
            SortingLinesMap[line].Clear();
            SortingLinesMap[line].AddRange(depths.Where(depth => depth >= 0));
        }

        public Tuple<IDecrementableLine, IIncrementableLine, int> GetInstructions()
        {
            var readyCars = GetReadyCars();
            if (readyCars != null)
            {
                return new Tuple<IDecrementableLine, IIncrementableLine, int>(readyCars.Item1, TrainLine, readyCars.Item2);
            }
            return null;
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
            int amount = 1;
            for (var i = 0; i < depths.Count - 1; i++)
            {
                if (depths[i + 1] - depths[i] > 1)
                {
                    break;
                }
                amount++;
            }

            // Return origin line and amount of cars ready to remove
            return new Tuple<IDecrementableLine, int>(line.Key, amount);
        }
    }
}
