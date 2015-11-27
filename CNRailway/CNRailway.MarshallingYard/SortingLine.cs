using System;

namespace CNRailway.MarshallingYard
{
    public class SortingLine : Line, IDecrementableLine, ICapped
    {
        public int Id { get; private set; }

        public int MaximumCapacity { get; private set; }

        public bool IsFull
        {
            get { return MaximumCapacity.Equals(Track.Count); }
        }

        public SortingLine(int id, int maximumCapacity)
        {
            Id = id;
            MaximumCapacity = maximumCapacity;
        }

        public override void AddCar(Car car)
        {
            if (IsFull)
            {
                throw new InvalidOperationException();
            }
            base.AddCar(car);
        }

        public Car RemoveCar()
        {
            return Track.Pop();
        }
    }
}
