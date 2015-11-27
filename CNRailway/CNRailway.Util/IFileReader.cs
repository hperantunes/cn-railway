using System.Collections.Generic;

namespace CNRailway.Util
{
    public interface IFileReader
    {
        IList<char[]> ReadFrom(string file);
    }
}
