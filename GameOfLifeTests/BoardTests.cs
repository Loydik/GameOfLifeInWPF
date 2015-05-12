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
    
    public class BoardTests
    {
    
        private static List<int> getTwoRandomNeighbours()
        {
            Random rand = new Random();

            List<int> offsets = new List<int>();

            while (true)
            {
                for (int i = 0; i < 4; i++)
                {
                    int random = rand.Next(-1, 2);

                    offsets.Add(random);
                }

                if ((offsets[0] == 0 && offsets[1] == 0) || (offsets[2] == 0 && offsets[3] == 0) || ((offsets[0] == offsets[2]) && (offsets[1] == offsets[3])))
                {
                    offsets.Clear();
                    continue;
                }
                else
                {
                    break;
                }
            }

            return offsets;
        }


        private Board _board = null;
        private int rows = 30;
        private int columns = 30;

        [SetUp]
        public void SetUp() 
        {
            _board = new Board(rows, columns);
        }

        [TearDown]
        public void TearDown() 
        {
            _board = null;
        }

        [Test]
        public void initializeBoard_ValidBoard_InitializesCorrectlyWithouNull() 
        {
            _board.initializeBoard();

            for (int i = 0; i < rows; i++) 
            {
                for (int j = 0; j < columns; j++)
                {
                    Assert.IsNotNull(_board.CellBoard[i, j]);
                }
            }
        }

        [Test]
        public void populateBoard_WrongRowsandColumns_throwsIndexOutOfRangeException() 
        {
            _board.initializeBoard();
            
            _board.Rows = 500;
            _board.Columns = 500;

            Assert.Throws(typeof(IndexOutOfRangeException), new TestDelegate(_board.populateBoard));
        }

        [Test]
        public void updateBoard_EmptyBoard_ReturnsEmptyBoard()
        {
            _board.initializeBoard();
            _board.updateBoard();

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Assert.IsFalse(_board.CellBoard[i, j].Alive);
                }
            }
        }

        [Test]
        public void updateBoard_SingleCell_ReturnsEmptyBoard()
        {
            _board.initializeBoard();
            Random rand = new Random();

            int x = (int)rand.Next(rows);
            int y = (int)rand.Next(columns);

            _board.CellBoard[x, y].setState(true);
            _board.updateBoard();

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Assert.IsFalse(_board.CellBoard[i, j].Alive);
                }
            }
        }

        [Test]
        [Category("Slow Tests")]
        public void updateBoard_TwoNeighbors_CellLives() 
        {
            //we check 50 random cells
            for (int i = 0; i < 50; i++)
            {

                _board.initializeBoard();

                Random rand = new Random();

                int x = 2 + (int)rand.Next(rows - 3);
                int y = 2 + (int)rand.Next(columns - 3);

                List<int> offsets = getTwoRandomNeighbours();

                _board.CellBoard[x, y].setState(true);

                _board.CellBoard[x + offsets[0], y + offsets[1]].setState(true);
                _board.CellBoard[x + offsets[2], y + offsets[3]].setState(true);


                _board.updateBoard();

                Assert.IsTrue(_board.CellBoard[x, y].Alive, "x: " + x + " y: " + y + " offsets: " + offsets[0] + ", " + offsets[1] + ", " + offsets[2] + ", " + offsets[3]);
            }
        }

        
        [Test]
        [Category("Slow Tests")]
        public void updateBoard_OneNeighbor_CellDies()
        {
            //we check 50 random cells
            for (int i = 0; i < 50; i++)
            {

                _board.initializeBoard();

                Random rand = new Random();

                int x = 2 + (int)rand.Next(rows - 3);
                int y = 2 + (int)rand.Next(columns - 3);

                List<int> offsets = getTwoRandomNeighbours();

                _board.CellBoard[x, y].setState(true);

                _board.CellBoard[x + offsets[0], y + offsets[1]].setState(true);

                _board.updateBoard();

                Assert.IsFalse(_board.CellBoard[x, y].Alive, "x: " + x + " y: " + y + " offsets: " + offsets[0] + ", " + offsets[1] + ", " + offsets[2] + ", " + offsets[3]);
            }
        }

        
        [Test]
        [Category("Slow Tests")]
        public void updateBoard_AliveCellThreeNeighbours_CellLives()
        {   
            //we check 50 random cells
            for (int i = 0; i < 50; i++)
            {
                _board.initializeBoard();

                Random rand = new Random();

                int x = 2 + (int)rand.Next(rows - 3);
                int y = 2 + (int)rand.Next(columns - 3);


                _board.CellBoard[x, y].setState(true);

                _board.CellBoard[x + 1, y].setState(true);
                _board.CellBoard[x, y + 1].setState(true);
                _board.CellBoard[x - 1, y].setState(true);


                _board.updateBoard();

                Assert.IsTrue(_board.CellBoard[x, y].Alive, "x: " + x + " y : ");
            }
        }


        [Test]
        [Category("Slow Tests")]
        public void updateBoard_AliveCellFourNeighbors_CellDies()
        {
            //we check 50 random cells
            for (int i = 0; i < 50; i++)
            {
                _board.initializeBoard();

                Random rand = new Random();

                int x = 2 + (int)rand.Next(rows - 3);
                int y = 2 + (int)rand.Next(columns - 3);


                _board.CellBoard[x, y].setState(true);

                _board.CellBoard[x + 1, y].setState(true);
                _board.CellBoard[x, y + 1].setState(true);
                _board.CellBoard[x - 1, y].setState(true);
                _board.CellBoard[x - 1, y - 1].setState(true);

                _board.updateBoard();

                Assert.IsFalse(_board.CellBoard[x, y].Alive, "x: " + x + " y : " + y);
            }
        }

        [Test]
        [Category("Slow Tests")]
        public void updateBoard_DeadCellFourNeighbors_CellRemainsDead()
        {
            //we check 50 random cells
            for (int i = 0; i < 50; i++)
            {
                _board.initializeBoard();

                Random rand = new Random();

                int x = 2 + (int)rand.Next(rows - 3);
                int y = 2 + (int)rand.Next(columns - 3);

                _board.CellBoard[x + 1, y].setState(true);
                _board.CellBoard[x, y + 1].setState(true);
                _board.CellBoard[x - 1, y].setState(true);
                _board.CellBoard[x - 1, y - 1].setState(true);

                _board.updateBoard();

                Assert.IsFalse(_board.CellBoard[x, y].Alive, "x: " + x + " y : " + y);
            }
        }


        [Test]
        [Category("Slow Tests")]
        public void updateBoard_AliveCellFiveNeighbors_CellDies()
        {
            //we check 50 random cells
            for (int i = 0; i < 50; i++)
            {
                _board.initializeBoard();

                Random rand = new Random();

                int x = 2 + (int)rand.Next(rows - 3);
                int y = 2 + (int)rand.Next(columns - 3);


                _board.CellBoard[x, y].setState(true);

                _board.CellBoard[x + 1, y].setState(true);
                _board.CellBoard[x, y + 1].setState(true);
                _board.CellBoard[x - 1, y].setState(true);
                _board.CellBoard[x - 1, y - 1].setState(true);
                _board.CellBoard[x - 1, y + 1].setState(true);

                _board.updateBoard();

                Assert.IsFalse(_board.CellBoard[x, y].Alive, "x: " + x + " y : " + y);
            }
        }

        [Test]
        [Category("Slow Tests")]
        public void updateBoard_AliveCellSixNeighbors_CellDies()
        {
            //we check 50 random cells
            for (int i = 0; i < 50; i++)
            {
                _board.initializeBoard();

                Random rand = new Random();

                int x = 2 + (int)rand.Next(rows - 3);
                int y = 2 + (int)rand.Next(columns - 3);


                _board.CellBoard[x, y].setState(true);

                _board.CellBoard[x + 1, y].setState(true);
                _board.CellBoard[x, y + 1].setState(true);
                _board.CellBoard[x - 1, y].setState(true);
                _board.CellBoard[x - 1, y - 1].setState(true);
                _board.CellBoard[x - 1, y + 1].setState(true);
                _board.CellBoard[x, y - 1].setState(true);

                _board.updateBoard();

                Assert.IsFalse(_board.CellBoard[x, y].Alive, "x: " + x + " y : " + y);
            }
        }

        [Test]
        [Category("Slow Tests")]
        public void updateBoard_AliveCellSevenNeighbors_CellDies()
        {
            //we check 50 random cells
            for (int i = 0; i < 50; i++)
            {
                _board.initializeBoard();

                Random rand = new Random();

                int x = 2 + (int)rand.Next(rows - 3);
                int y = 2 + (int)rand.Next(columns - 3);


                _board.CellBoard[x, y].setState(true);

                _board.CellBoard[x + 1, y].setState(true);
                _board.CellBoard[x, y + 1].setState(true);
                _board.CellBoard[x - 1, y].setState(true);
                _board.CellBoard[x - 1, y - 1].setState(true);
                _board.CellBoard[x - 1, y + 1].setState(true);
                _board.CellBoard[x, y - 1].setState(true);
                _board.CellBoard[x + 1, y + 1].setState(true);

                _board.updateBoard();

                Assert.IsFalse(_board.CellBoard[x, y].Alive, "x: " + x + " y : " + y);
            }
        }

        [Test]
        [Category("Slow Tests")]
        public void updateBoard_AliveCellEightNeighbors_CellDies()
        {
            //we check 50 random cells
            for (int i = 0; i < 50; i++)
            {

                _board.initializeBoard();
                Random rand = new Random();

                int x = 2 + (int)rand.Next(rows - 3);
                int y = 2 + (int)rand.Next(columns - 3);


                _board.CellBoard[x, y].setState(true);

                _board.CellBoard[x + 1, y].setState(true);
                _board.CellBoard[x, y + 1].setState(true);
                _board.CellBoard[x - 1, y].setState(true);
                _board.CellBoard[x - 1, y - 1].setState(true);
                _board.CellBoard[x - 1, y + 1].setState(true);
                _board.CellBoard[x, y - 1].setState(true);
                _board.CellBoard[x + 1, y + 1].setState(true);
                _board.CellBoard[x + 1, y - 1].setState(true);

                _board.updateBoard();

                Assert.IsFalse(_board.CellBoard[x, y].Alive, "x: " + x + " y : " + y);
            }
        }

        [Test]
        public void getLivingNeighbours_neighbours_worksCorrectly()
        {
            int expectedLivingNeighbours = 2;
            
            _board.initializeBoard();

            int x = 16;
            int y = 16;
            int[] offsets = { 1, 0, -1, 1 };
           
            _board.CellBoard[x, y].setState(true);

            _board.CellBoard[x + offsets[0], y + offsets[1]].setState(true);
            _board.CellBoard[x + offsets[2], y + offsets[3]].setState(true);

            Assert.AreEqual((_board.getLivingNeighbours(x, y)), expectedLivingNeighbours);
        }
     
    }

}


/*
       private static List<List<int>> getRandomNeighbours(int amount)
       {

           Random rand = new Random();

           List<List<int>> offsets = new List<List<int>>();

           while (true)
           {
               for (int i = 0; i < amount; i++)
               {
                   offsets.Add(new List<int>());

                   for (int j = 0; j < 2; j++)
                   {
                       offsets[i].Add(rand.Next(-1, 2));
                   }
               }

               if (checkNeighborsUniqueness(offsets))
               {
                   break;
               }
               else
               {
                   offsets.Clear();
                   continue;
               }
           }

           return offsets;
       }

       private static Boolean checkNeighborsUniqueness(List<List<int>> offsets) 
       {
           int amount = offsets.Count();
           List<int> zeroList = new List<int>();
           zeroList.Add(0);
           zeroList.Add(0);
           bool zeroElement = false;

            for (int i = 0; i < amount; i++)
               {
                   if(offsets[i].Equals(zeroList))
                   {
                       zeroElement = true;
                   }
               }


           if (amount != offsets.Distinct().Count()) {
               return false;
           }
           else if (zeroElement)
           {
               return false;
           }
           else
           {
               return true;
           }
            
       }
       */