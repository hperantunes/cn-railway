using CNRailway.Util;

namespace CNRailway.MarshallingYard
{
    public class TrainLine : Line
    {
        public override string Name
        {
            get { return Constants.Labels.TrainLine; }
        }
    }
}
