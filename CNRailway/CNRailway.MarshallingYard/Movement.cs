using System.Collections.Generic;
using System.Linq;

namespace CNRailway.MarshallingYard
{
    public class Movement : IMovement
    {
        /// <summary>
        /// The line that cars were removed from
        /// </summary>
        public INamed OriginLine { get; private set; }

        /// <summary>
        /// The line that cars were added to
        /// </summary>
        public INamed DestinationLine { get; private set; }

        /// <summary>
        /// The moved cars
        /// </summary>
        public IEnumerable<INamed> Cars { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="originLine">The line where cars are being removed</param>
        /// <param name="destinationLine">The line where cars are being added</param>
        /// <param name="cars">The given cars</param>
        public Movement(INamed originLine, INamed destinationLine, IEnumerable<INamed> cars)
        {
            OriginLine = originLine;
            DestinationLine = destinationLine;
            Cars = cars;
        }

        /// <summary>
        /// Specific string representation of the movement
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            // TODO: This requires some refactoring.
            var count = Cars.Count();
            var carsAmount = count > 1 ? $"{count} cars" : $"{count} car";
            var carNames = string.Join(", ", Cars.Select(car => car.Name));

            var text = $"Move {carsAmount} ({carNames}) from {OriginLine.Name} to {DestinationLine.Name}.";
            return text;
        }
    }
}
