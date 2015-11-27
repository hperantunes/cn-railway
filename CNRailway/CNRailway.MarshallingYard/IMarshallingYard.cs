using System.Collections.Generic;
using System.Linq;

namespace CNRailway.MarshallingYard
{
    public interface IMarshallingYard
    {
        IYardmaster InitializeYard(IEnumerable<IEnumerable<char>> lines);
    }
}
