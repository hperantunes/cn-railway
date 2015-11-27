using CNRailway.Util;
using System;

namespace CNRailway.MarshallingYard
{
    public class SortingLine : Line, ISortingLine, IDecrementableLine, ICapped
    {
        private IConfiguration Configuration { get; set; }

        public int Id { get; private set; }

        public int MaximumCapacity { get; private set; }

        public bool IsFull
        {
            get { return MaximumCapacity.Equals(Track.Count); }
        }

        public SortingLine(int id, IConfiguration configuration)
        {
            Id = id;
            Configuration = configuration;
            MaximumCapacity = configuration.SortingLineMaximumCapacity;
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

        public override string ToString()
        {
            return base.ToString().PadLeft(MaximumCapacity, Configuration.EmptySlotCharacter);
        }
    }
}
