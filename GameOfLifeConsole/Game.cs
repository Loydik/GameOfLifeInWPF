using System;
using System.Threading;
using GameOfLife;


class ConsoleBoard : Board
{
    public ConsoleBoard(int rows, int columns) : base (rows, columns)
    {
        this.Rows = rows;
        this.Columns = columns;
        this.CellBoard = new Cell[rows, columns];
    }

    public void drawConsoleBoard()
    {
        Console.Clear();
        
        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Columns; j++)
            {
                Console.Write((CellBoard[i, j].Alive == true) ? "██" : "||");
            }
            Console.WriteLine();
        }
    }
}

    

    class Game
    {
        static void Main(string[] args)
        {
            
            ConsoleBoard board = new ConsoleBoard(20, 20);

            board.initializeBoard();
            board.populateBoard();
            board.drawConsoleBoard();

            int currentGeneration = 0;
            int maxGenerations = 5000;

            
            while (currentGeneration<=maxGenerations)
            {
                board.updateBoard();
                board.drawConsoleBoard();
                currentGeneration++;

                Thread.Sleep(400);
            } 
        }
            

    }