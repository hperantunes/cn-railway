using System.Collections.Generic;

namespace CNRailway.MarshallingYard
{
    public interface IYard
    {
        IYardmaster Initialize();
        ILinesMap GetLinesMap(char destination);
    }
}
