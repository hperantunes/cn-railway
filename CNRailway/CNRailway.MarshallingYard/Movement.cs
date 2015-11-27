using System.Collections.Generic;
using System.Linq;

namespace CNRailway.MarshallingYard
{
    public class Movement : IMovement
    {
        public INamed OriginLine { get; private set; }

        public INamed DestinationLine { get; private set; }

        public IEnumerable<INamed> Cars { get; private set; }

        public Movement(INamed originLine, INamed destinationLine, IEnumerable<INamed> cars)
        {
            OriginLine = originLine;
            DestinationLine = destinationLine;
            Cars = cars;
        }

        // TODO: This may require some refactoring.
        public override string ToString()
        {
            var count = Cars.Count();
            var carsAmount = count > 1 ? $"{count} cars" : $"{count} car";
            var carNames = string.Join(", ", Cars.Select(car => car.Name));

            var text = $"Move {carsAmount} ({carNames}) from {OriginLine.Name} to {DestinationLine.Name}.";
            return text;
        }
    }
}
