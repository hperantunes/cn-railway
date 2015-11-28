using CNRailway.Util;
using System.Collections.Generic;
using System.Linq;

namespace CNRailway.MarshallingYard
{
    /// <summary>
    /// The Yard Locomotive is responsible for moving cars from and to 
    /// different lines
    /// </summary>
    public class YardLocomotive : IYardLocomotive, ICapped
    {
        private IConfiguration Configuration { get; set; }

        private List<Car> Slots { get; set; }

        /// <summary>
        /// Maximum number of cars this locomotive supports
        /// </summary>
        public int MaximumCapacity { get; private set; }

        /// <summary>
        /// Number of remaining free slots in this locomotive
        /// </summary>
        public int OpenSlots
        {
            get { return MaximumCapacity - Slots.Count; }
        }

        /// <summary>
        /// Whether the locomotive has reached its maximum capacity
        /// </summary>
        public bool IsFull
        {
            get { return MaximumCapacity.Equals(Slots.Count); }
        }

        /// <summary>
        /// The current cars attached to the locomotive
        /// </summary>
        public IEnumerable<Car> Cars
        {
            get { return Slots.AsEnumerable(); }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration">A configuration object</param>
        public YardLocomotive(IConfiguration configuration)
        {
            Configuration = configuration;
            MaximumCapacity = configuration.YardLocomotiveMaximumCapacity;
            Slots = new List<Car>();
        }

        /// <summary>
        /// Removes cars from a given line and attaches them to the locomotive
        /// </summary>
        /// <param name="line">The line that cars will be loaded from</param>
        /// <param name="amount">The amount of cars to load</param>
        public void LoadCarsFromLine(IDecrementableLine line, int amount)
        {
            // Do not load more cars than the locomotive's maximum capacity
            amount = amount > MaximumCapacity ? MaximumCapacity : amount;

            while (amount-- > 0)
            {
                var car = line.RemoveCar();
                Slots.Add(car);
            }
        }

        /// <summary>
        /// Removes cars attached to the locomotive and adds them to the line 
        /// </summary>
        /// <param name="line">The line that cars will be unloaded into</param>
        public void UnloadAllCarsIntoLine(IIncrementableLine line)
        {
            Slots.ForEach(car => line.AddCar(car));
            Slots.Clear();
        }
    }
}
