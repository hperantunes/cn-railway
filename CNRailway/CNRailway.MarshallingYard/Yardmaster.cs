using System;
using System.Collections.Generic;
using System.Linq;

namespace CNRailway.MarshallingYard
{
    public class Yardmaster : IYardmaster
    {
        private IYardLocomotive YardLocomotive { get; set; }

        private ILine TrainLine { get; set; }

        private List<ISortingLine> SortingLines { get; set; }

        public Yardmaster(IYardLocomotive yardLocomotive, ILine trainLine, IEnumerable<ISortingLine> sortingLines)
        {
            YardLocomotive = yardLocomotive;
            TrainLine = trainLine;
            SortingLines = sortingLines.ToList();
        }

        public IEnumerable<string> AssembleTrainToDestination(char destination)
        {
            throw new NotImplementedException();
        }
    }
}
