using System.Collections.Generic;

namespace CNRailway.MarshallingYard
{
    public interface ILine
    {
        IEnumerable<int> GetCarsPositions(char destination);
    }
}
