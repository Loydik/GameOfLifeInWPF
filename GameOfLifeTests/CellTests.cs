using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using GameOfLife;

namespace GameOfLifeTests
{
    [TestFixture]

    class CellTests
    {
        private Cell _cell = null;

        [SetUp]
        public void SetUp() 
        {
            _cell = new Cell(false);
        }

        [TearDown]
        public void TearDown()
        {
            _cell = null;
        }

        [Test]
        //Any live cell with two or three live neighbours lives on to the next generation.
        public void calculateNewState_twoNeighboursCellAlive_returnsTrue()
        {
            int neighbors = 2;
            _cell.setState(true);

            Assert.IsTrue(Cell.calculateNewState(neighbors, _cell));
        }

        [Test]
        public void calculateNewState_twoNeighboursCellDead_returnsFalse()
        {
            int neighbors = 2;
            Assert.IsFalse(Cell.calculateNewState(neighbors, _cell));
        }

        [Test]
        //Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction
        public void calculateNewState_CellDeadThreeNeighbors_returnsTrue() 
        {
            int neighbors = 3;
            Assert.IsTrue(Cell.calculateNewState(neighbors, _cell));
        }

        [Test]
        //Any live cell with two or three live neighbours lives on to the next generation.
        public void calculateNewState_CellAliveThreeNeighbors_returnsTrue()
        {
            int neighbors = 3;
            _cell.setState(true);

            Assert.IsTrue(Cell.calculateNewState(neighbors, _cell));
        }

        [Test]
        public void calculateNewState_CellDeadMoreThanThreeNeighbors_returnsFalse()
        {
            int neighbors = 4;
            Assert.IsFalse(Cell.calculateNewState(neighbors, _cell));
        }

        [Test]
        //Any live cell with more than three live neighbours dies, as if by overcrowding.
        public void calculateNewState_CellAliveMoreThanThreeNeighbors_returnsFalse()
        {
            int neighbors = 4;
            _cell.setState(true);
            Assert.IsFalse(Cell.calculateNewState(neighbors, _cell));
        }

        [Test]
        public void calculateNewState_CellDeadLessThanTwoNeighbors_returnsFalse()
        {
            int neighbors = 1;
            Assert.IsFalse(Cell.calculateNewState(neighbors, _cell));
        }

        [Test]
        public void calculateNewState_CellAliveLessThanTwoNeighbors_returnsFalse()
        {
            int neighbors = 1;
            _cell.setState(true);
            Assert.IsFalse(Cell.calculateNewState(neighbors, _cell));
        }
    }
}
