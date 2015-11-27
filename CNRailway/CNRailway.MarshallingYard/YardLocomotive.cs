using System;
using System.Collections.Generic;

namespace CNRailway.MarshallingYard
{
    public class YardLocomotive : IYardLocomotive
    {
        private List<Car> Slots { get; set; }

        public int MaximumCapacity { get; private set; }

        public YardLocomotive(int maximumCapacity)
        {
            MaximumCapacity = maximumCapacity;
            Slots = new List<Car>();
        }

        public void LoadCarsFromLine(ISortingLine sortingLine, int amount)
        {
            if (amount + Slots.Count > MaximumCapacity)
            {
                throw new InvalidOperationException();
            }

            while (amount-- > 0)
            {
                var car = sortingLine.RemoveCar();
                Slots.Add(car);
            }
        }

        public void UnloadAllCarsIntoLine(ILine line)
        {
            Slots.ForEach(car => line.AddCar(car));
            Slots.Clear();
        }
    }
}
