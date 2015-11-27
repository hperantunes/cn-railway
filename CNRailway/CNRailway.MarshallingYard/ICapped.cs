namespace CNRailway.MarshallingYard
{
    public interface ICapped
    {
        int MaximumCapacity { get; }
        int OpenSlots { get; }
        bool IsFull { get; }
    }
}
