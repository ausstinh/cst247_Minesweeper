using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cst247_Minesweeper.Models
{
    public class CellModel
    {

        public int Y { get; set; }
        public int X { get; set; }
        public int Neighbors { get; set; }
        public bool Bomb { get; set; }
        public bool Revealed { get; set; }
        public bool Flagged { get; set; }

        // default constructor
        public CellModel()
        {
            X = -1;
            Y = -1;
            Neighbors = 0;
            Bomb = false;
            Revealed = false;
            Flagged = false;
        }

        public CellModel(int y, int x)
        {
            this.Y = y;
            this.X = x;
            Neighbors = 0;
            Bomb = false;
            Revealed = false;
            Flagged = false;
        }

    }
}

