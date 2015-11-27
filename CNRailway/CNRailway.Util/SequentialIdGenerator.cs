namespace CNRailway.Util
{
    public class SequentialIdGenerator : IIdGenerator
    {
        private int InitialId { get; set; }

        public int NewId
        {
            get { return InitialId++; }
        }

        public SequentialIdGenerator(int initialId = 1)
        {
            InitialId = initialId;
        }
    }
}
