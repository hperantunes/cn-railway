using System.Collections.Generic;
using System.Linq;

namespace CNRailway.MarshallingYard
{
    public interface IMarshallingYard
    {
        IYardmaster Initialize(IEnumerable<IEnumerable<char>> lines);
    }
}
