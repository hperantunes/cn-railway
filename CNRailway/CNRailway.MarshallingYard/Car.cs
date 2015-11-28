namespace CNRailway.MarshallingYard
{
    public class Car : ICar, INamed
    {
        /// <summary>
        /// The destination to where the car is bound
        /// </summary>
        public char Destination { get; private set; }

        /// <summary>
        /// The position of the car in its current line
        /// </summary>
        public int Position { get; set; }

        /// <summary>
        /// The name of the car
        /// </summary>
        public string Name
        {
            get { return Destination.ToString(); }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="destination">The destination where the car is bound</param>
        public Car(char destination)
        {
            Destination = destination;
        }

        /// <summary>
        /// Specific string representation of the car
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Name;
        }
    }
}
