using System.Collections.Generic;

namespace CNRailway.MarshallingYard
{
    public interface ILine
    {
        int MaximumCapacity { get; }
        IEnumerable<int> GetCarsPositions(char destination);
    }
}
