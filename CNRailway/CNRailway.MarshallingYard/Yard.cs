using CNRailway.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CNRailway.MarshallingYard
{
    /// <summary>
    /// The Yard represents the Marshalling Yard
    /// </summary>
    public class Yard : IYard
    {
        private IIdGenerator IdGenerator { get; set; }

        private IConfiguration Configuration { get; set; }

        private IList<ISortingLine> SortingLines { get; set; }

        private IYardLocomotive YardLocomotive { get; set; }

        /// <summary>
        /// An object responsible for assembling cars into the train line
        /// </summary>
        public IYardmaster Yardmaster { get; private set; }

        /// <summary>
        /// The train line
        /// </summary>
        public ILine TrainLine { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="idGenerator">A generator to create ids to sorting lines</param>
        /// <param name="configuration">A configuration object</param>
        /// <param name="lines">The character representation of all sorting lines</param>
        public Yard(IIdGenerator idGenerator, IConfiguration configuration, IEnumerable<IEnumerable<char>> lines)
        {
            IdGenerator = idGenerator;
            Configuration = configuration;
            SortingLines = CreateSortingLines(lines);
            YardLocomotive = new YardLocomotive(Configuration);
            Yardmaster = new Yardmaster(YardLocomotive);
            TrainLine = new TrainLine();
        }

        /// <summary>
        /// Gets a lines map
        /// </summary>
        /// <param name="destination">The given destination</param>
        /// <returns>
        /// An object that provides instructions on what moviments to make to 
        /// assemble cars bound to a given destination into the train line
        /// </returns>
        public ILinesMap GetLinesMap(char destination)
        {
            var linesMap = new LinesMap(TrainLine, SortingLines, destination);
            return linesMap;
        }

        /// <summary>
        /// Gets the current sorting lines
        /// </summary>
        /// <returns></returns>
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
