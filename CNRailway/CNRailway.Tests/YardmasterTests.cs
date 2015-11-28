using CNRailway.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace CNRailway.MarshallingYard
{
    [TestClass]
    public class YardmasterTests
    {
        private IYardmaster Yardmaster { get; set; }
        private IYard Yard { get; set; }
        private IIdGenerator IdGenerator { get; set; }
        private char Destination { get; set; }

        private Mock<IConfiguration> Configuration { get; set; }

        [TestInitialize]
        public void Init()
        {
            var lines = new List<char[]>
            {
                "00ABC".ToCharArray(),
                "0BCAD".ToCharArray()
            };

            Configuration = new Mock<IConfiguration>();
            Configuration.SetupGet(conf => conf.EmptySlotCharacter).Returns('0');
            Configuration.SetupGet(conf => conf.SortingLineMaximumCapacity).Returns(5);
            Configuration.SetupGet(conf => conf.YardLocomotiveMaximumCapacity).Returns(1);

            Destination = 'A';
            IdGenerator = new SequentialIdGenerator();

            Yard = new Yard(IdGenerator, Configuration.Object, lines);
            Yardmaster = Yard.Initialize();
        }

        [TestMethod]
        public void AssembleTrain_SetDestination_HasExpectedCarsInTrainLine()
        {
            // arrange
            var map = Yard.GetLinesMap(Destination);

            // act
            Yardmaster.AssembleTrain(map);

            // assert
            var trainLine = Yard.TrainLine;
            Assert.IsNotNull(trainLine);

            var cars = trainLine.ToString().ToCharArray();
            Assert.AreEqual(2, cars.Count());
            Assert.IsTrue(cars.All(car => Destination.Equals(car)));
        }

        [TestMethod]
        public void AssembleTrain_SetDestination_RemovesAllCarsFromSortingLines()
        {
            // arrange
            var map = Yard.GetLinesMap(Destination);

            // act
            var steps = Yardmaster.AssembleTrain(map);

            // assert
            var sortingLines = Yard.GetSortingLines();
            Assert.IsNotNull(sortingLines);
            Assert.IsFalse(sortingLines.Any(line => line.ContainsCarToDestination(Destination)));
        }

        [TestMethod]
        public void AssembleTrain_NotEnoughRoomInSortingLines_DoesNotMakeAnyMovement()
        {
            // arrange
            var lines = new List<char[]>
            {
                "0BDCA".ToCharArray(),
                "0CBDA".ToCharArray(),
                "0DCBA".ToCharArray()
            };
            var yard = new Yard(IdGenerator, Configuration.Object, lines);
            var yardmaster = yard.Initialize();
            var map = yard.GetLinesMap(Destination);

            // act
            var steps = yardmaster.AssembleTrain(map);

            // assert
            Assert.IsFalse(steps.Any());
        }
    }
}