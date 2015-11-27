namespace CNRailway.MarshallingYard
{
    public interface ICapped
    {
        int MaximumCapacity { get; }
        bool IsFull { get; }
    }
}
