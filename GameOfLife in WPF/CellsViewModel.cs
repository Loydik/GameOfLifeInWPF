using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Threading;
using GameOfLife;

namespace GameOfLife_in_WPF
{
    public class CellsViewModel : ObservableCollection<CellViewModel>
    {
        private CellViewModel[,] _cellBoard;

        public int Rows { get; set; }
        public int Columns { get; set; }
        
        public CellViewModel[,] CellBoard { get; set; }


        public CellsViewModel(int rows, int columns) {
            this.Rows = rows;
            this.Columns = columns;
            this.CellBoard = new CellViewModel[rows, columns];
        }

        public void initializeBoard(CellViewModel[,] board)
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    board[i, j] = new CellViewModel(i,j);
                }
            }
        }

        
        public void populateBoard(CellViewModel[,] board)
        {
            Random rand = new Random();

            for (int i = 0; i <= (int)(Rows * Columns) / 6; i++)
            {
                int x, y;
                x = rand.Next(Rows);
                y = rand.Next(Columns);

                board[x, y].Cell.Alive = true;
            }
        }


        public CellViewModel[,] updateBoard(CellViewModel[,] board) // update board
        {
            CellViewModel[,] tempboard = board;


            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    if (i == board[i, j].X && j == board[i, j].Y) //For testing
                    {

                        int neighbors = 0;
                        try { neighbors += checkNeighbor(board[i, j + 1].Cell.Alive); }
                        catch { }
                        try { neighbors += checkNeighbor(board[i, j - 1].Cell.Alive); }
                        catch { }
                        try { neighbors += checkNeighbor(board[i + 1, j].Cell.Alive); }
                        catch { }
                        try { neighbors += checkNeighbor(board[i - 1, j].Cell.Alive); } // we try and catch because some indexes are off
                        catch { }                                                // the board (like if it was on a border) and are outside
                        try { neighbors += checkNeighbor(board[i + 1, j + 1].Cell.Alive); }                // the bounds of the array
                        catch { }
                        try { neighbors += checkNeighbor(board[i - 1, j - 1].Cell.Alive); }
                        catch { }
                        try { neighbors += checkNeighbor(board[i + 1, j - 1].Cell.Alive); }
                        catch { }
                        try { neighbors += checkNeighbor(board[i - 1, j + 1].Cell.Alive); }
                        catch { }

                        //RULES OF SURVIVAL

                        try
                        {
                            if (neighbors < 2)
                            {
                                tempboard[i, j].Cell.Alive = false; // underpopulation, dies
                            }

                            if (neighbors > 3)
                            {
                                tempboard[i, j].Cell.Alive = false; // overpopulation, dies
                            }

                            if (board[i, j].Cell.Alive == false && neighbors == 3)
                            {
                                tempboard[i, j].Cell.Alive = true; // a cell is born!
                            }

                            if (neighbors >= 2 && neighbors <= 3 && board[i, j].Cell.Alive == true)
                            {
                                tempboard[i, j].Cell.Alive = true; // else same as given
                            }
                        }

                        catch { continue; }
                    }
                }

                
            }
            
            board = tempboard;
            
            return board;
        }


        static int checkNeighbor(Boolean alive)
        {
            if (alive == true)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        
        
    }
}
