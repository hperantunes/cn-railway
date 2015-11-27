namespace CNRailway.MarshallingYard
{
    public interface IDecrementableLine : INamed
    {
        Car RemoveCar();
    }
}
