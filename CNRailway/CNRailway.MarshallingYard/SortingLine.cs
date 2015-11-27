namespace CNRailway.MarshallingYard
{
    public class SortingLine : Line, ISortingLine
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
