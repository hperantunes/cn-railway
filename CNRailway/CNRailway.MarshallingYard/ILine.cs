using System.Collections.Generic;

namespace CNRailway.MarshallingYard
{
    public interface ILine : IIncrementableLine
    {
        int Count { get; }
        bool ContainsCarToDestination(char destination);
    }
}
