using CNRailway.Util;

namespace CNRailway.MarshallingYard
{
    public class TrainLine : Line, INamed
    {
        public string Name
        {
            get { return Constants.TrainLine; }
        }
    }
}
