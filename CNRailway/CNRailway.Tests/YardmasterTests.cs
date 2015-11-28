using Microsoft.VisualStudio.TestTools.UnitTesting;
using CNRailway.MarshallingYard;
using CNRailway.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;

namespace CNRailway.MarshallingYard
{
    [TestClass]
    public class YardmasterTests
    {
        private IYardmaster Yardmaster { get; set; }
        private IYard Yard { get; set; }
        private char Destination { get; set; }

        [TestInitialize]
        public void Init()
        {
            var lines = new List<char[]>
            {
                "00ABC".ToCharArray(),
                "0BCAD".ToCharArray(),
            };

            var configuration = new Mock<IConfiguration>();
            configuration.SetupGet(conf => conf.EmptySlotCharacter).Returns('0');
            configuration.SetupGet(conf => conf.SortingLineMaximumCapacity).Returns(5);
            configuration.SetupGet(conf => conf.YardLocomotiveMaximumCapacity).Returns(1);

            Destination = 'A';

            Yard = new Yard(new SequentialIdGenerator(), configuration.Object, lines);
            Yardmaster = Yard.Initialize();
        }

        [TestMethod]
        public void AssembleTrain_SetDestination_HasExpectedCarsInTrainLine()
        {
            // arrange
            var map = Yard.GetLinesMap(Destination);

            // act
            var steps = Yardmaster.AssembleTrain(map);

            // assert
            var trainLine = Yard.TrainLine;
            Assert.IsNotNull(trainLine);

            var destinations = trainLine.ToString().ToCharArray();
            Assert.AreEqual(2, destinations.Count());
            Assert.IsTrue(destinations.All(destination => Destination.Equals(destination)));
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
    }
}