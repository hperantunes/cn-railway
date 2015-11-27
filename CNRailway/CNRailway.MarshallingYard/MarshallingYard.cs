using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using CNRailway.Util;

namespace CNRailway.MarshallingYard
{
    public class MarshallingYard : IMarshallingYard
    {
        private IIdGenerator IdGenerator { get; set; }

        private IConfiguration Configuration { get; set; }

        private IFileReader FileReader { get; set; }

        private IUserInterface UserInterface { get; set; }

        public MarshallingYard(IIdGenerator idGenerator, IConfiguration configuration)
        {
            IdGenerator = idGenerator;
            Configuration = configuration;
        }

        public IYardmaster Initialize(IEnumerable<IEnumerable<char>> lines)
        {
            var yardLocomotive = new YardLocomotive(Configuration);
            var trainLine = new Line();
            var sortingLines = CreateSortingLines(lines);
            
            var yardmaster = new Yardmaster(yardLocomotive, trainLine, sortingLines);
            return yardmaster;
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
            foreach (var destinations in lines) {
                var sortingLine = CreateSortingLine(IdGenerator.NewId, destinations);
                yield return sortingLine;
            }
        }
    }
}
