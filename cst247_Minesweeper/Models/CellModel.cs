using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cst247_Minesweeper.Models
{
    public class CellModel
    {

        public int x { get; set; }
        public int y { get; set; }
        public int neighbors { get; set; }
        public bool bomb { get; set; }
        public bool revealed { get; set; }
        public bool flagged { get; set; }

        // default constructor
        public CellModel()
        {
            x = -1;
            y = -1;
            neighbors = 0;
            bomb = false;
            revealed = false;
            flagged = false;
        }

        public CellModel(int x, int y)
        {
            this.x = x;
            this.y = y;
            neighbors = 0;
            bomb = false;
            revealed = false;
            flagged = false;
        }

    }
}

