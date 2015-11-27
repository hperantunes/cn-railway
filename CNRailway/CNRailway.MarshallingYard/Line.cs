using System;
using System.Collections.Generic;
using System.Linq;

namespace CNRailway.MarshallingYard
{
    public abstract class Line : ILine
    {
        protected Stack<Car> Track { get; private set; }

        public int MaximumCapacity { get; private set; }

        public bool IsFull
        {
            get { return MaximumCapacity.Equals(Track.Count); }
        }

        public Line(int maximumCapacity)
        {
            Track = new Stack<Car>();
            MaximumCapacity = maximumCapacity;
        }

        public void AddCar(Car car)
        {
            if (IsFull)
            {
                throw new InvalidOperationException();
            }

            car.Position = MaximumCapacity - Track.Count;
            Track.Push(car);
        }

        public IEnumerable<int> GetCarsPositions(char destination)
        {
            var positions = Track.Where(car => destination.Equals(car.Destination)).Select(car => car.Position);
            return positions;
        }

        public override string ToString()
        {
            var destinations = Track.Select(car => car.Destination);
            var representation = string.Concat(destinations.Reverse());
            return representation.PadLeft(MaximumCapacity, '0');
        }
    }
}
