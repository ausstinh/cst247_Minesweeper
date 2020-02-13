using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using cst247_Minesweeper.Models.data;

namespace cst247_Minesweeper.Models.business
{
    public class GameBusinessService
    {
        private BoardModel Board;

        public GameBusinessService(GameModel game)
        {
            this.Board = game.Board;
        }

        public BoardModel setupBombsAndNeighbors()
        {
            Random rnd = new Random();
            int amntBombs = (Board.Size * Board.Size * Board.DifficultyPercent) / 100;
            int count = 0;
            int row = -1;
            int col = -1;
            while (amntBombs > count)
            {
                row = rnd.Next(0, Board.Size);
                col = rnd.Next(0, Board.Size);
                if (Board.TheGrid[row, col].Bomb == false)
                {
                    Board.TheGrid[row, col].Bomb = true;
                    count++;
                }
            }

            for (int y = 0; y < Board.Size; y++)
            {
                for (int x = 0; x < Board.Size; x++)
                {
                    CellModel c = Board.TheGrid[y, x];
                    c.Neighbors = 0;
                    // N
                    if (inRange(c.Y + 1, c.X) && Board.TheGrid[c.Y + 1, c.X].Bomb == true)
                        c.Neighbors++;
                    // NE
                    if (inRange(c.Y + 1, c.X + 1) && Board.TheGrid[c.Y + 1, c.X + 1].Bomb == true)
                        c.Neighbors++;
                    // E
                    if (inRange(c.Y, c.X + 1) && Board.TheGrid[c.Y, c.X + 1].Bomb == true)
                        c.Neighbors++;
                    // SE
                    if (inRange(c.Y - 1, c.X + 1) && Board.TheGrid[c.Y - 1, c.X + 1].Bomb == true)
                        c.Neighbors++;
                    // S
                    if (inRange(c.Y - 1, c.X) && Board.TheGrid[c.Y - 1, c.X].Bomb == true)
                        c.Neighbors++;
                    // SW
                    if (inRange(c.Y - 1, c.X - 1) && Board.TheGrid[c.Y - 1, c.X - 1].Bomb == true)
                        c.Neighbors++;
                    // W
                    if (inRange(c.Y, c.X - 1) && Board.TheGrid[c.Y, c.X - 1].Bomb == true)
                        c.Neighbors++;
                    // NW
                    if (inRange(c.Y + 1, c.X - 1) && Board.TheGrid[c.Y + 1, c.X - 1].Bomb == true)
                        c.Neighbors++;
                }
            }
            return Board;
        }

        public BoardModel revealOneCell(CellModel mine)
        {
            if (!mine.Flagged)
            {
                if (!mine.Revealed)
                {
                    // game over if bomb
                    if (mine.Bomb)
                    {
                        // true = game over
                        return Board;
                    }
                    // flood fill
                    floodFillCellAndAround(mine.Y, mine.X);
                }
            }
            // false = not game over
            return Board;
        }

        public void floodFillCellAndAround(int y, int x)
        {
            if (inRange(y, x) && !Board.TheGrid[y, x].Revealed)
            {
                // reveal cell and add to counter
                Board.TheGrid[y, x].Revealed = true;
                //RevealCounter++;

                if (Board.TheGrid[y, x].Neighbors == 0)
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

        private bool inRange(int y, int x)
        {
            if (x >= 0 && x < Board.Size && y >= 0 && y < Board.Size)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}