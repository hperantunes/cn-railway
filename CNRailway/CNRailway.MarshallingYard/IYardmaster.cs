using System.Collections.Generic;

namespace CNRailway.MarshallingYard
{
    public interface IYardmaster
    {
        IEnumerable<string> AssembleTrainToDestination(char destination);
    }
}
