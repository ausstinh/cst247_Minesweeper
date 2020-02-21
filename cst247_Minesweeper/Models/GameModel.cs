using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace cst247_Minesweeper.Models
{
    public class GameModel
    {
        public BoardModel Board { get; set; }
        public ScoreModel Score { get; set; }
        public Stopwatch Stopwatch { get; set; }
    }
}