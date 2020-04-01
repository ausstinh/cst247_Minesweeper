using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace cst247_Minesweeper.Models
{
    [DataContract]
    public class BoardModel
    {
        [DataMember]
        public int Size { get; set; }

        [DataMember]
        public int Difficulty { get; set; }

        [DataMember]
        public int DifficultyPercent { get; set; }

        [DataMember]
        public CellModel[,] TheGrid { get; set; }

        [DataMember]
        public int RevealCounter { get; set; }

        [DataMember]
        public int Flags { get; set; }

        [DataMember]
        public int amntBombs { get; set; }

        public BoardModel(int difficulty)
        {
            Difficulty = difficulty;
            if (difficulty == 0)
            {
                Size = 7;
                DifficultyPercent = 15;
            }
            else if (difficulty == 1)
            {
                Size = 9;
                DifficultyPercent = 17;
            }
            else if (difficulty == 2)
            {
                Size = 12;
                DifficultyPercent = 19;
            }
            else
            {
                Size = 0;
                DifficultyPercent = 0;
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
