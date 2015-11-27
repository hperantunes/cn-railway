using CNRailway.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CNRailway.MarshallingYard
{
    public class SortingLine : Line, ISortingLine, ICapped
    {
        private IConfiguration Configuration { get; set; }

        public int Id { get; private set; }

        public int MaximumCapacity { get; private set; }

        public int OpenSlots
        {
            get { return MaximumCapacity - Track.Count; }
        }

        public override string Name
        {
            get { return $"{Constants.Labels.Line} {Id}"; }
        }

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

        public IEnumerable<int> GetPositions(char destination)
        {
            var positions = Track.Where(car => destination.Equals(car.Destination)).Select(car => car.Position);
            return positions;
        }

        public override string ToString()
        {
            return base.ToString().PadLeft(MaximumCapacity, Configuration.EmptySlotCharacter);
        }
    }
}
