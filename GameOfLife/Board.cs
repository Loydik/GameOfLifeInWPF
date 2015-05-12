using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    public class Board
    {
        #region Properties

        public int Rows { get; set; }
        public int Columns { get; set; }
        public Cell[,] CellBoard { get; set; }

        #endregion Properties

        #region Construction

        public Board(int rows, int columns) {
            this.Rows = rows;
            this.Columns = columns;
            this.CellBoard = new Cell[rows, columns];
        }
        
        #endregion Construction

        #region Methods

        //Tested
        public void initializeBoard()
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    this.CellBoard[i, j] = new Cell(false);
                }
            }
        }
   

        public void populateBoard()
        {
            Random rand = new Random();

            for (int i = 0; i <= (int)(Rows * Columns) / 6; i++)
            {
                int x, y;
                x = (int) rand.Next(Rows);
                y = (int) rand.Next(Columns);

                try
                {
                    this.CellBoard[x, y].setState(true);
                }
                catch (IndexOutOfRangeException ex)
                {
                    throw;
                }
                
            }
        }

        public virtual void updateBoard() // update board
        {
            Cell[,] tempboard = DeepObjectCopy.DeepCopy(this.CellBoard);
            
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                        int neighbours = getLivingNeighbours(i,j);

                        try
                        {
                            tempboard[i, j].setState(Cell.calculateNewState(neighbours, tempboard[i, j]));
                        }
                        catch(NullReferenceException ex) 
                        { 
                            continue; 
                        }       
                }

                
            }
            
            this.CellBoard = tempboard;
         }

        static int checkNeighbor(Boolean alive)
        {   
            return (alive) ? 1 : 0;

        }

        public int getLivingNeighbours(int i, int j) 
        {
            int neighbors = 0;

                    if ((i < Rows - 1 && j < Columns - 1) && (i > 1 && j > 1))
                    {
                        neighbors += checkNeighbor(CellBoard[i, j + 1].Alive);
                        neighbors += checkNeighbor(CellBoard[i, j - 1].Alive);
                        neighbors += checkNeighbor(CellBoard[i + 1, j].Alive);
                        neighbors += checkNeighbor(CellBoard[i - 1, j].Alive);
                        neighbors += checkNeighbor(CellBoard[i + 1, j + 1].Alive);
                        neighbors += checkNeighbor(CellBoard[i - 1, j - 1].Alive);
                        neighbors += checkNeighbor(CellBoard[i + 1, j - 1].Alive);
                        neighbors += checkNeighbor(CellBoard[i - 1, j + 1].Alive);
                    }
                
            return neighbors;
        }

   }

        #endregion Methods
}

/*
                            try { neighbors += checkNeighbor(CellBoard[i, j + 1].Alive); }
                            catch(IndexOutOfRangeException ex) { }
                            try { neighbors += checkNeighbor(CellBoard[i, j - 1].Alive); }
                            catch (IndexOutOfRangeException ex) { }
                            try { neighbors += checkNeighbor(CellBoard[i + 1, j].Alive); }
                            catch (IndexOutOfRangeException ex) { }
                            try { neighbors += checkNeighbor(CellBoard[i - 1, j].Alive); }
                            catch (IndexOutOfRangeException ex) { }
                            try { neighbors += checkNeighbor(CellBoard[i + 1, j + 1].Alive); }
                            catch (IndexOutOfRangeException ex) { }
                            try { neighbors += checkNeighbor(CellBoard[i - 1, j - 1].Alive); }
                            catch (IndexOutOfRangeException ex) { }
                            try { neighbors += checkNeighbor(CellBoard[i + 1, j - 1].Alive); }
                            catch (IndexOutOfRangeException ex) { }
                            try { neighbors += checkNeighbor(CellBoard[i - 1, j + 1].Alive); }
                            catch (IndexOutOfRangeException ex) { }
                            */