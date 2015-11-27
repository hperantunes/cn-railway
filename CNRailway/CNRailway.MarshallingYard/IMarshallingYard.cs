using System.Collections.Generic;

namespace CNRailway.MarshallingYard
{
    public interface IMarshallingYard
    {
        IYardmaster Initialize(IEnumerable<IEnumerable<char>> lines);
        ILinesMap GetLinesMap(char destination);
    }
}
