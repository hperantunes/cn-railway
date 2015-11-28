using CNRailway.Util;

namespace CNRailway.MarshallingYard
{
    public class TrainLine : Line
    {
        /// <summary>
        /// The name of the line
        /// </summary>
        public override string Name
        {
            get { return Constants.Labels.TrainLine; }
        }
    }
}
