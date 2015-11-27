using System.Collections.Generic;

namespace CNRailway.MarshallingYard
{
    public interface IMovement
    {
        INamed OriginLine { get; }
        INamed DestinationLine { get; }
        IEnumerable<INamed> Cars { get; }
    }
}
