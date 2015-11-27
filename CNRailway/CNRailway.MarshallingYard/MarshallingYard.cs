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
        private int SortingLineMaximumCapacity { get; set; }

        private int YardLocomotiveMaximumCapacity { get; set; }

        private char EmptySlotCharacter { get; set; }

        private IIdGenerator IdGenerator { get; set; }

        private IConfiguration Configuration { get; set; }

        public MarshallingYard(IIdGenerator idGenerator, IConfiguration configuration)
        {
            SortingLineMaximumCapacity = Convert.ToInt32(ConfigurationManager.AppSettings["SortingLineMaximumCapacity"]);
            YardLocomotiveMaximumCapacity = Convert.ToInt32(ConfigurationManager.AppSettings["YardLocomotiveMaximumCapacity"]);
            EmptySlotCharacter = Convert.ToChar(ConfigurationManager.AppSettings["EmptySlotCharacter"]);
            IdGenerator = idGenerator;
            Configuration = configuration;
        }

        public IYardmaster InitializeYard(IEnumerable<IEnumerable<char>> lines)
        {
            var yardLocomotive = new YardLocomotive(Configuration);
            var trainLine = new Line();
            var sortingLines = CreateSortingLines(lines);
            
            var yardmaster = new Yardmaster(yardLocomotive, trainLine, sortingLines);
            return yardmaster;
        }

        private Car CreateCar(char destination)
        {
            if (EmptySlotCharacter.Equals(destination))
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
