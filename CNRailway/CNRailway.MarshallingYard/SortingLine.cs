using CNRailway.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CNRailway.MarshallingYard
{
    public class SortingLine : Line, ISortingLine, ICapped
    {
        private IConfiguration Configuration { get; set; }

        /// <summary>
        /// Identification of the line
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Maximum number of cars this line supports
        /// </summary>
        public int MaximumCapacity { get; private set; }

        /// <summary>
        /// Number of remaining free slots in this line
        /// </summary>
        public int OpenSlots
        {
            get { return MaximumCapacity - Track.Count; }
        }

        /// <summary>
        /// Name of the line
        /// </summary>
        public override string Name
        {
            get { return $"{Constants.Labels.Line} {Id}"; }
        }

        /// <summary>
        /// Whether the line has reached its maximum capacity
        /// </summary>
        public bool IsFull
        {
            get { return MaximumCapacity.Equals(Track.Count); }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">An identification for the line</param>
        /// <param name="configuration">A configuration object</param>
        public SortingLine(int id, IConfiguration configuration)
        {
            Id = id;
            Configuration = configuration;
            MaximumCapacity = configuration.SortingLineMaximumCapacity;
        }

        /// <summary>
        /// Adds a car to the line
        /// </summary>
        /// <param name="car">The given car</param>
        public override void AddCar(Car car)
        {
            if (IsFull)
            {
                throw new InvalidOperationException();
            }
            base.AddCar(car);
        }

        /// <summary>
        /// Removes a car from the line
        /// </summary>
        /// <returns></returns>
        public Car RemoveCar()
        {
            return Track.Pop();
        }

        /// <summary>
        /// Gets a collection of positions of cars in the line, bound to a given destination
        /// </summary>
        /// <param name="destination">The giben destination</param>
        /// <returns></returns>
        public IEnumerable<int> GetPositions(char destination)
        {
            var positions = Track.Where(car => destination.Equals(car.Destination)).Select(car => car.Position);
            return positions;
        }

        /// <summary>
        /// Specific string representation of the line
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString().PadLeft(MaximumCapacity, Configuration.EmptySlotCharacter);
        }
    }
}
