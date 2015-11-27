namespace CNRailway.Util
{
    public interface IConfiguration
    {
        string DefaultDirectory { get; }
        string DefaultFileName { get; }
        int YardLocomotiveMaximumCapacity { get; }
        int SortingLineMaximumCapacity { get; }
        char EmptySlotCharacter { get; }
        bool UseDefaultFileLocation { get; }
    }
}
