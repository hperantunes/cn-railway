using System.Collections.Generic;

namespace CNRailway.MarshallingYard
{
    public interface ISortingLine
    {
        int Id { get; }
        IEnumerable<int> GetCarsPositions(char destination);
    }
}
