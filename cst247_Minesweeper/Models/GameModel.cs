using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace cst247_Minesweeper.Models
{
    [DataContract]
    public class GameModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public BoardModel Board { get; set; }

        [DataMember]
        public Stopwatch Stopwatch { get; set; }

        public GameModel()
        {

        }

        public GameModel(int id)
        {
            this.Id = id;
        }
    }
}