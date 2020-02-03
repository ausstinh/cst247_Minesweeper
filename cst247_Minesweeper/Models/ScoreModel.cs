using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cst247_Minesweeper.Models
{
    public class ScoreModel
    {
        public int time { get; set; }
        public int difficulty { get; set; }
        public int name { get; set; }
        public int score { get; set; }

        public void calculateScore()
        {
            if (difficulty == 0)
            {
                score = (200 - time) * 10;
            }
            else if (difficulty == 1)
            {
                score = (200 - time) * 20;
            }
            else if (difficulty == 2)
            {
                score =(200 - time) * 50;
            }
            else
            {
                score = 0;
            }
        }
    }
}