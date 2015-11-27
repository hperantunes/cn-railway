namespace CNRailway.MarshallingYard
{
    public class SortingLine : Line, IDecrementableLine
    {
        public int Id { get; private set; }

        public SortingLine(int id, int maximumCapacity) 
            : base(maximumCapacity)
        {
            Id = id;
        }

        public Car RemoveCar()
        {
            return Track.Pop();
        }
    }
}
