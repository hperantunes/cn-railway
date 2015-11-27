using System.Collections.Generic;

namespace CNRailway.MarshallingYard
{
    public interface IYardmaster
    {
        IEnumerable<IMovement> AssembleTrainToDestination(ILinesMap map);
    }
}
