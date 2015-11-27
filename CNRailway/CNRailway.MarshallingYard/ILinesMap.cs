using System;

namespace CNRailway.MarshallingYard
{
    public interface ILinesMap
    {
        void IncreaseDepth(ISortingLine line, int amount);
        void DecreaseDepth(ISortingLine line, int amount);
        Tuple<IDecrementableLine, IIncrementableLine, int> GetDirections();
    }
}
