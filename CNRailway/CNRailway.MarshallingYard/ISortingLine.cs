using System.Collections.Generic;

namespace CNRailway.MarshallingYard
{
    public interface ISortingLine : ILine, IIncrementableLine, IDecrementableLine, ICapped
    {
        int Id { get; }
        IEnumerable<int> GetPositions(char destination);
    }
}
