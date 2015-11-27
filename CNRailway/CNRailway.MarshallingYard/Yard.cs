using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace CNRailway.MarshallingYard
{
    public class Yard : IYard
    {
        private int SortingLineMaximumCapacity { get; set; }

        private int YardLocomotiveMaximumCapacity { get; set; }

        private char EmptySlotCharacter { get; set; }

        public Yard()
        {
            SortingLineMaximumCapacity = Convert.ToInt32(ConfigurationManager.AppSettings["SortingLineMaximumCapacity"]);
            YardLocomotiveMaximumCapacity = Convert.ToInt32(ConfigurationManager.AppSettings["YardLocomotiveMaximumCapacity"]);
            EmptySlotCharacter = Convert.ToChar(ConfigurationManager.AppSettings["EmptySlotCharacter"]);
        }

        public IYardmaster InitializeYard(IList<IOrderedEnumerable<char>> lines)
        {
            var yardLocomotive = new YardLocomotive(YardLocomotiveMaximumCapacity);
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

        private ISortingLine CreateSortingLine(int id, IOrderedEnumerable<char> destinations)
        {
            var sortingLine = (ISortingLine) new SortingLine(id, SortingLineMaximumCapacity);
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

        private IEnumerable<ISortingLine> CreateSortingLines(IList<IOrderedEnumerable<char>> lines)
        {
            for (var i = 1; i < lines.Count; i++)
            {
                var destinations = lines[i];
                var sortingLine = CreateSortingLine(i, lines[i]);
                yield return sortingLine;
            }
        }
    }
}
