namespace CNRailway.MarshallingYard
{
    public interface IYardLocomotive
    {
        int MaximumCapacity { get; }
        void LoadCarsFromLine(ISortingLine sortingLine, int amount);
        void UnloadAllCarsIntoLine(ILine line);
    }
}
