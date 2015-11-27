using System.Collections.Generic;

namespace CNRailway.MarshallingYard
{
    public interface ILine
    {
        int MaximumCapacity { get; }
        void AddCar(Car car);
        IEnumerable<int> GetCarsPositions(char destination);
    }
}
