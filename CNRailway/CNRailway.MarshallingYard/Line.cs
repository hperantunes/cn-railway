using System;
using System.Collections.Generic;
using System.Linq;

namespace CNRailway.MarshallingYard
{
    public abstract class Line : ILine, IIncrementableLine
    {
        protected Stack<Car> Track { get; private set; }

        public abstract string Name { get; }

        public int Count
        {
            get { return Track.Count; }
        }

        public Line()
        {
            Track = new Stack<Car>();
        }

        public virtual void AddCar(Car car)
        {
            car.Position = Track.Count;
            Track.Push(car);
        }

        public override string ToString()
        {
            var destinations = Track.OrderBy(car => car.Position).Select(car => car.Destination);
            return string.Concat(destinations.Reverse());
        }
    }
}
