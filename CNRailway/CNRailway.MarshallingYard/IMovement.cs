using System.Collections.Generic;

namespace CNRailway.MarshallingYard
{
    public interface IMovement
    {
        INamed OriginLine { get; }
        INamed DestinationLine { get; }
        IEnumerable<ICar> Cars { get; }
    }
}
