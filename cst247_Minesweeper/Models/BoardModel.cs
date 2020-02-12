using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cst247_Minesweeper.Models
{
    public class BoardModel
    {

        public int Size { get; set; }
        public int DifficultyPercent { get; set; }
        public CellModel[,] TheGrid { get; set; }
        public int RevealCounter { get; set; }
        public int Flags { get; set; }

        public BoardModel(int size)
        {
            if (size == 0)
            {
                size = 7;
                this.DifficultyPercent = 15;
            }
            else if (size == 1)
            {
                size = 9;
                this.DifficultyPercent = 18;
            }
            else
            {
                size = 12;
                this.DifficultyPercent = 22;
            }
            this.Size = size;

            //difficultyPercent = Global.difficulty;
            RevealCounter = 0;

            TheGrid = new CellModel[size, size];

            // filling 2D array with cells
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    TheGrid[y, x] = new CellModel(y, x);

                }
            }

            setupBombsAndNeighbors();
        }

        public void setupBombsAndNeighbors()
        {
            Random rnd = new Random();
            int amntBombs = (Size * Size * DifficultyPercent) / 100;
            int count = 0;
            int row = -1;
            int col = -1;
            while (amntBombs > count)
            {
                row = rnd.Next(0, Size);
                col = rnd.Next(0, Size);
                if (TheGrid[row, col].Bomb == false)
                {
                    TheGrid[row, col].Bomb = true;
                    count++;
                }
            }

            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    CellModel c = TheGrid[y, x];
                    c.Neighbors = 0;
                    // N
                    if (inRange(c.Y + 1, c.X) && TheGrid[c.Y + 1, c.X].Bomb == true)
                        c.Neighbors++;
                    // NE
                    if (inRange(c.Y + 1, c.X + 1) && TheGrid[c.Y + 1, c.X + 1].Bomb == true)
                        c.Neighbors++;
                    // E
                    if (inRange(c.Y, c.X + 1) && TheGrid[c.Y, c.X + 1].Bomb == true)
                        c.Neighbors++;
                    // SE
                    if (inRange(c.Y - 1, c.X + 1) && TheGrid[c.Y - 1, c.X + 1].Bomb == true)
                        c.Neighbors++;
                    // S
                    if (inRange(c.Y - 1, c.X) && TheGrid[c.Y - 1, c.X].Bomb == true)
                        c.Neighbors++;
                    // SW
                    if (inRange(c.Y - 1, c.X - 1) && TheGrid[c.Y - 1, c.X - 1].Bomb == true)
                        c.Neighbors++;
                    // W
                    if (inRange(c.Y, c.X - 1) && TheGrid[c.Y, c.X - 1].Bomb == true)
                        c.Neighbors++;
                    // NW
                    if (inRange(c.Y + 1, c.X - 1) && TheGrid[c.Y + 1, c.X - 1].Bomb == true)
                        c.Neighbors++;
                }
            }
        }

        private bool inRange(int y, int x)
        {
            if (x >= 0 && x < Size && y >= 0 && y < Size)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool revealOneCell(CellModel mine)
        {
            if (!mine.Flagged)
            {
                if (!mine.Revealed)
                {
                    // game over if bomb
                    if (mine.Bomb)
                    {
                        // true = game over
                        return true;
                    }
                    // flood fill
                    floodFillCellAndAround(mine.Y, mine.X);
                }
            }
            // false = not game over
            return false;
        }

        public void floodFillCellAndAround(int y, int x)
        {
            if (inRange(y, x) && !TheGrid[y, x].Revealed)
            {
                // reveal cell and add to counter
                TheGrid[y, x].Revealed = true;
                RevealCounter++;

                if (TheGrid[y, x].Neighbors == 0)
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
            return (Size * Size * DifficultyPercent) / 100;
        }

    }
}
