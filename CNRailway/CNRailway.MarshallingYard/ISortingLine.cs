using System.Collections.Generic;

namespace CNRailway.MarshallingYard
{
    public interface ISortingLine : IIncrementableLine, IDecrementableLine
    {
        int Id { get; }
        int Count { get; }
        IEnumerable<int> GetPositions(char destination);
        bool ContainsCar(char destination);
    }
}
