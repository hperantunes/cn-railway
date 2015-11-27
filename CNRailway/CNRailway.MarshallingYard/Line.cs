using System;
using System.Collections.Generic;
using System.Linq;

namespace CNRailway.MarshallingYard
{
    public class Line : ILine, IIncrementableLine
    {
        protected Stack<Car> Track { get; private set; }

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
