namespace CNRailway.MarshallingYard
{
    public interface IYardLocomotive
    {
        void LoadCarsFromLine(IDecrementableLine line, int amount);
        void UnloadAllCarsIntoLine(IIncrementableLine line);
    }
}
