using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cst247_Minesweeper.Models
{
    public class ScoreModel : IComparable<ScoreModel>
    {
        public int Id { get; set; }
        public int Score { get; set; }
        public int Difficulty { get; set; }
        public int UserId { get; set; }

        // only used for high score
        public string UserName { get; set; }

        public ScoreModel(int score, int difficulty, int userId)
        {
            Score = score;
            Difficulty = difficulty;
            UserId = userId;
        }

        public int CompareTo(ScoreModel otherScore)
        {
            if(this.Score == otherScore.Score)
            {
                return this.Score.CompareTo(otherScore.Score);
            }
            return otherScore.Score.CompareTo(this.Score);
        }
    }
}