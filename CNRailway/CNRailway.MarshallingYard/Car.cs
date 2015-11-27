using System;

namespace CNRailway.MarshallingYard
{
    public class Car : ICar, INamed
    {
        public char Destination { get; private set; }

        public int Position { get; set; }

        public string Name
        {
            get { return Destination.ToString(); }
        }

        public Car(char destination)
        {
            Destination = destination;
        }
    }
}
