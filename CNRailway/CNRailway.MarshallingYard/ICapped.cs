using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNRailway.MarshallingYard
{
    public interface ICapped
    {
        int MaximumCapacity { get; }
        bool IsFull { get; }
    }
}
