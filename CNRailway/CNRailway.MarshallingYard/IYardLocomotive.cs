namespace CNRailway.MarshallingYard
{
    public interface IYardLocomotive
    {
        int MaximumCapacity { get; }
        void LoadCarsFromLine(IDecrementableLine line, int amount);
        void UnloadAllCarsIntoLine(IIncrementableLine line);
    }
}
