using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CNRailway.Util;

namespace CNRailway.MarshallingYard
{
    public class Movement
    {
        public INamed OriginLine { get; private set; }

        public INamed DestinationLine { get; private set; }

        public IEnumerable<ICar> Cars { get; private set; }

        public Movement(INamed originLine, INamed destinationLine, IEnumerable<ICar> cars)
        {
            OriginLine = originLine;
            DestinationLine = destinationLine;
            Cars = cars;
        }
    }
}
