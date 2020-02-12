using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cst247_Minesweeper.Models
{
    public class GameModel
    {
        public BoardModel Board { get; set; }
        public ScoreModel Score { get; set; }
        public int time { get; set; }
        public int Size { get; set; }
    }
}