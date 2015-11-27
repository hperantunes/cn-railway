namespace CNRailway.MarshallingYard
{
    public class Car : ICar
    {
        public char Destination { get; private set; }

        public int Position { get; set; }

        public Car(char destination)
        {
            Destination = destination;
        }
    }
}
