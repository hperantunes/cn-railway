using System;
using System.Collections.Generic;
using System.Linq;

namespace CNRailway.MarshallingYard
{
    /// <summary>
    /// The Yardmaster is responsible for assembling trains bound to a given 
    /// destination into the train line
    /// </summary>
    public class Yardmaster : IYardmaster
    {
        private IYardLocomotive YardLocomotive { get; set; }

        private ILine TrainLine { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="yardLocomotive">A locomotive that will move cars</param>
        public Yardmaster(IYardLocomotive yardLocomotive)
        {
            YardLocomotive = yardLocomotive;
        }

        /// <summary>
        /// Assemble trains bound to a given destination into the train line
        /// </summary>
        /// <param name="map">A map to provide assemblage instructions</param>
        /// <returns>
        /// A collection of all movements that were executed when
        /// assembling the train
        /// </returns>
        public IEnumerable<IMovement> AssembleTrain(ILinesMap map)
        {
            var movements = new List<IMovement>();
            Tuple<IDecrementableLine, IIncrementableLine, int> directions;
            while ((directions = map.GetInstruction()) != null)
            {
                var movement = MoveConvoy(YardLocomotive, map, directions.Item1, directions.Item2, directions.Item3);
                movements.Add(movement);
            }

            return movements;
        }

        private IMovement MoveConvoy(IYardLocomotive locomotive, ILinesMap map, IDecrementableLine origin, IIncrementableLine destination, int amount)
        {
            locomotive.LoadCarsFromLine(origin, amount);
            var movement = new Movement(origin, destination, locomotive.Cars.AsEnumerable<INamed>().ToList());
            map.UpdateDepths(origin as ISortingLine, -movement.Cars.Count());

            locomotive.UnloadAllCarsIntoLine(destination);
            map.UpdateDepths(destination as ISortingLine, movement.Cars.Count());

            return movement;
        }
    }
}
