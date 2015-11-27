using System;
using System.Collections.Generic;
using System.Linq;

namespace CNRailway.MarshallingYard
{
    public class Yardmaster : IYardmaster
    {
        private IYardLocomotive YardLocomotive { get; set; }

        private ILine TrainLine { get; set; }

        public Yardmaster(IYardLocomotive yardLocomotive)
        {
            YardLocomotive = yardLocomotive;
        }

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
            map.UpdateDepths((ISortingLine)origin, -movement.Cars.Count());

            locomotive.UnloadAllCarsIntoLine(destination);
            if (destination is SortingLine)
            {
                map.UpdateDepths((ISortingLine)destination, -movement.Cars.Count());
            }

            return movement;
        }
    }
}
