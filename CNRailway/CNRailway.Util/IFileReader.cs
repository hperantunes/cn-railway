using System.Collections.Generic;

namespace CNRailway.Util
{
    public interface IFileReader
    {
        IEnumerable<string> ReadFrom(string file);
    }
}
