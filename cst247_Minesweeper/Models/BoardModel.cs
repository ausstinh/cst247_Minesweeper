using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cst247_Minesweeper.Models
{
    public class BoardModel
    {

        public int size { get; set; }
        public int difficultyPercent { get; set; }
        public CellModel[,] TheGrid { get; set; }
        public int revealCounter { get; set; }
        public int flags { get; set; }

        public BoardModel(int size)
        {
            //difficultyPercent = Global.difficulty;
            revealCounter = 0;
            this.size = size;

            TheGrid = new CellModel[size, size];

            // filling 2D array with cells
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    TheGrid[i, j] = new CellModel(i, j);
                }
            }
        }

        public void setupLiveNeighbors()
        {
            Random rnd = new Random();
            int amntBombs = (size * size * difficultyPercent) / 100;
            int count = 0;
            int row = -1;
            int col = -1;

            while (amntBombs > count)
            {
                row = rnd.Next(0, size);
                col = rnd.Next(0, size);
                if (TheGrid[row, col].bomb == false)
                {
                    TheGrid[row, col].bomb = true;
                    count++;
                }
            }

            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    CellModel c = TheGrid[y, x];
                    c.neighbors = 0;
                    if (inRange(c.x, c.y + 1) && TheGrid[c.x, c.y + 1].bomb == true)
                        c.neighbors++;
                    if (inRange(c.x + 1, c.y + 1) && TheGrid[c.x + 1, c.y + 1].bomb == true)
                        c.neighbors++;
                    if (inRange(c.x + 1, c.y) && TheGrid[c.x + 1, c.y].bomb == true)
                        c.neighbors++;
                    if (inRange(c.x + 1, c.y - 1) && TheGrid[c.x + 1, c.y - 1].bomb == true)
                        c.neighbors++;
                    if (inRange(c.x, c.y - 1) && TheGrid[c.x, c.y - 1].bomb == true)
                        c.neighbors++;
                    if (inRange(c.x - 1, c.y - 1) && TheGrid[c.x - 1, c.y - 1].bomb == true)
                        c.neighbors++;
                    if (inRange(c.x - 1, c.y) && TheGrid[c.x - 1, c.y].bomb == true)
                        c.neighbors++;
                    if (inRange(c.x - 1, c.y + 1) && TheGrid[c.x - 1, c.y + 1].bomb == true)
                        c.neighbors++;
                }
            }
        }

        private bool inRange(int y, int x)
        {
            if (x >= 0 && x < size && y >= 0 && y < size)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool revealOneCell(int y, int x)
        {
            if (TheGrid[y, x].flagged == false)
            {
                if (TheGrid[y, x].revealed == false)
                {
                    // game over if bomb
                    if (TheGrid[y, x].bomb == true)
                    {
                        // true = game over
                        return true;
                    }
                    // flood fill
                    floodFillCellAndAround(y, x);
                }
            }
            // false = not game over
            return false;
        }

        public void floodFillCellAndAround(int y, int x)
        {
            if (inRange(y, x) && TheGrid[y, x].revealed == false)
            {
                // reveal cell and add to counter
                TheGrid[y, x].revealed = true;
                revealCounter++;

                if (TheGrid[y, x].neighbors == 0)
                {
                    floodFillCellAndAround(y + 1, x - 1);
                    floodFillCellAndAround(y + 1, x);
                    floodFillCellAndAround(y + 1, x + 1);
                    floodFillCellAndAround(y, x + 1);
                    floodFillCellAndAround(y - 1, x + 1);
                    floodFillCellAndAround(y - 1, x);
                    floodFillCellAndAround(y - 1, x - 1);
                    floodFillCellAndAround(y, x - 1);
                }
            }

        }

        public int getAmntBombs()
        {
            return (size * size * difficultyPercent) / 100;
        }

    }
}
