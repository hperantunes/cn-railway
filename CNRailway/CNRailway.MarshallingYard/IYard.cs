using System.Collections.Generic;
using System.Linq;

namespace CNRailway.MarshallingYard
{
    public interface IYard
    {
        IYardmaster InitializeYard(IList<IOrderedEnumerable<char>> lines);
    }
}
