using System;

namespace CNRailway.MarshallingYard
{
    public interface ILinesMap
    {
        void UpdateDepths(ISortingLine line, int amount);
        Tuple<IDecrementableLine, IIncrementableLine, int> GetInstructions();
    }
}
