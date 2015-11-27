namespace CNRailway.MarshallingYard
{
    public interface ISortingLine : ILine
    {
        int Id { get; }
        Car RemoveCar();
    }
}
