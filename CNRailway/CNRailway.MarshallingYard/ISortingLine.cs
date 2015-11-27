namespace CNRailway.MarshallingYard
{
    public interface ISortingLine : ILine, IIncrementableLine
    {
        int Id { get; }
    }
}
