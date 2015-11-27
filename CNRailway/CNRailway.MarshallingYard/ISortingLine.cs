using System.Collections.Generic;

namespace CNRailway.MarshallingYard
{
    public interface ISortingLine
    {
        int Id { get; }
        int Count { get; }
        IEnumerable<int> GetPositions(char destination);
        bool ContainsCar(char destination);
    }
}
