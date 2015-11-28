using System.Collections.Generic;
using System.Linq;

namespace CNRailway.MarshallingYard
{
    public abstract class Line : ILine, IIncrementableLine
    {
        protected Stack<Car> Track { get; private set; }

        /// <summary>
        /// The name of the line
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// The amount of cars currently in the line
        /// </summary>
        public int Count
        {
            get { return Track.Count; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public Line()
        {
            Track = new Stack<Car>();
        }

        /// <summary>
        /// Checks whether a line contains at least one car bound to a given destination
        /// </summary>
        /// <param name="destination">Character that represents a car's destination</param>
        /// <returns></returns>
        public bool ContainsCarToDestination(char destination)
        {
            return Track.Any(car => destination.Equals(car.Destination));
        }

        /// <summary>
        /// Adds a car to the line
        /// </summary>
        /// <param name="car">The given car</param>
        public virtual void AddCar(Car car)
        {
            car.Position = Track.Count;
            Track.Push(car);
        }

        /// <summary>
        /// Specific string representation of the line
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var destinations = Track.OrderBy(car => car.Position).Select(car => car.Destination);
            return string.Concat(destinations.Reverse());
        }
    }
}
