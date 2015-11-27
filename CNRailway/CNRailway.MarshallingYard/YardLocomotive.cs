using CNRailway.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CNRailway.MarshallingYard
{
    public class YardLocomotive : IYardLocomotive, ICapped
    {
        private IConfiguration Configuration { get; set; }

        private List<Car> Slots { get; set; }

        public int MaximumCapacity { get; private set; }

        public bool IsFull
        {
            get { return MaximumCapacity.Equals(Slots.Count); }
        }

        public IEnumerable<Car> Cars
        {
            get { return Slots.AsEnumerable(); }
        }

        public YardLocomotive(IConfiguration configuration)
        {
            Configuration = configuration;
            MaximumCapacity = configuration.YardLocomotiveMaximumCapacity;
            Slots = new List<Car>();
        }

        public void LoadCarsFromLine(IDecrementableLine line, int amount)
        {
            if (amount + Slots.Count > MaximumCapacity)
            {
                throw new InvalidOperationException();
            }

            while (amount-- > 0)
            {
                var car = line.RemoveCar();
                Slots.Add(car);
            }
        }

        public void UnloadAllCarsIntoLine(IIncrementableLine line)
        {
            Slots.ForEach(car => line.AddCar(car));
            Slots.Clear();
        }
    }
}
