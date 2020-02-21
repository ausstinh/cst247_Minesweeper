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
        public int amntBombs { get; set; }

        public BoardModel(int size)
        {
            if (size == 0)
            {
                Size = 7;
                DifficultyPercent = 15;
            }
            else if (size == 1)
            {
                Size = 9;
                DifficultyPercent = 18;
            }
            else
            {
                Size = 12;
                DifficultyPercent = 22;
            }

            RevealCounter = 0;

            TheGrid = new CellModel[Size, Size];

            // filling 2D array with cells
            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    TheGrid[y, x] = new CellModel(y, x);
                }
            }
        }

        public int getAmntBombs()
        {
            return (Size * Size * DifficultyPercent) / 100;
        }

    }
}
