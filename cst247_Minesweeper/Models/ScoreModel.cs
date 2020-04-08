using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace cst247_Minesweeper.Models
{
    [DataContract]
    public class ScoreModel : IComparable<ScoreModel>
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int Score { get; set; }
        [DataMember]
        public int Difficulty { get; set; }
        [DataMember]
        public int UserId { get; set; }

        // only used for high score
        [DataMember]
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