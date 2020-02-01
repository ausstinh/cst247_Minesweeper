using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace cst247_Minesweeper.Models
{
    public class DifficultyModel
    {
        public DifficultyTypes Difficulty { get; set; }
        public enum DifficultyTypes : int
    {
        [Display(Name = "Easy")]
        Easy = 0,
        [Display(Name = "Medium")]
        Medium = 1,
        [Display(Name = "Hard")]
        Hard = 2
    }
    }
}