using System.Collections.Generic;

namespace CNRailway.MarshallingYard
{
    public interface IYardLocomotive
    {
        IEnumerable<Car> Cars { get; }
        void LoadCarsFromLine(IDecrementableLine line, int amount);
        void UnloadAllCarsIntoLine(IIncrementableLine line);
    }
}
