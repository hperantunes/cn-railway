using CNRailway.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CNRailway.MarshallingYard
{
    public class Yard : IYard
    {
        private IIdGenerator IdGenerator { get; set; }

        private IConfiguration Configuration { get; set; }

        private IList<ISortingLine> SortingLines { get; set; }

        private IYardLocomotive YardLocomotive { get; set; }

        private bool Initialized { get; set; }

        public ILine TrainLine { get; private set; }

        public Yard(IIdGenerator idGenerator, IConfiguration configuration, IEnumerable<IEnumerable<char>> lines)
        {
            IdGenerator = idGenerator;
            Configuration = configuration;
            SortingLines = CreateSortingLines(lines);
        }

        public IYardmaster Initialize()
        {
            TrainLine = new TrainLine();
            YardLocomotive = new YardLocomotive(Configuration);

            var yardmaster = new Yardmaster(YardLocomotive);
            Initialized = true;

            return yardmaster;
        }

        public ILinesMap GetLinesMap(char destination)
        {
            if (!Initialized)
            {
                throw new InvalidOperationException();
            }

            var linesMap = new LinesMap(TrainLine, SortingLines, destination);
            return linesMap;
        }

        public IEnumerable<ISortingLine> GetSortingLines()
        {
            return SortingLines;
        }

        private Car CreateCar(char destination)
        {
            if (Configuration.EmptySlotCharacter.Equals(destination))
            {
                return null;
            }

            var car = new Car(destination);
            return car;
        }

        private ISortingLine CreateSortingLine(int id, IEnumerable<char> destinations)
        {
            var sortingLine = new SortingLine(id, Configuration);
            foreach (var destination in destinations.Reverse())
            {
                var car = CreateCar(destination);
                if (car != null)
                {
                    sortingLine.AddCar(car);
                }
            }

            return sortingLine;
        }

        private IList<ISortingLine> CreateSortingLines(IEnumerable<IEnumerable<char>> lines)
        {
            var sortingLines = new List<ISortingLine>();
            foreach (var destinations in lines)
            {
                var sortingLine = CreateSortingLine(IdGenerator.NewId, destinations);
                sortingLines.Add(sortingLine);
            }
            return sortingLines;
        }
    }
}
