using System.Collections.Generic;

namespace CNRailway.MarshallingYard
{
    public interface IYard
    {
        ILine TrainLine { get; }
        IYardmaster Initialize();
        ILinesMap GetLinesMap(char destination);
        IEnumerable<ISortingLine> GetSortingLines();
    }
}
