using System.Collections.Generic;
using System.Linq;
using CNRailway.Util;

namespace CNRailway.MarshallingYard
{
    public class MarshallingYard : IMarshallingYard
    {
        private IIdGenerator IdGenerator { get; set; }

        private IConfiguration Configuration { get; set; }

        private IEnumerable<ISortingLine> SortingLines { get; set; }

        private ILine TrainLine { get; set; }

        private IYardLocomotive YardLocomotive { get; set; }

        public MarshallingYard(IIdGenerator idGenerator, IConfiguration configuration)
        {
            IdGenerator = idGenerator;
            Configuration = configuration;
        }

        public IYardmaster Initialize(IEnumerable<IEnumerable<char>> lines)
        {
            SortingLines = CreateSortingLines(lines);
            TrainLine = new TrainLine();
            YardLocomotive = new YardLocomotive(Configuration);

            var yardmaster = new Yardmaster(YardLocomotive);
            return yardmaster;
        }

        public ILinesMap GetLinesMap(char destination)
        {
            var linesMap = new LinesMap(TrainLine, SortingLines, destination);
            return linesMap;
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

        private IEnumerable<ISortingLine> CreateSortingLines(IEnumerable<IEnumerable<char>> lines)
        {
            foreach (var destinations in lines)
            {
                var sortingLine = CreateSortingLine(IdGenerator.NewId, destinations);
                yield return sortingLine;
            }
        }
    }
}
