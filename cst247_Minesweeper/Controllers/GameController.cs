using cst247_Minesweeper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace cst247_Minesweeper.Controllers
{
    public class GameController : Controller
    {
        // GET: Game
        public ActionResult Index()
        {
            return View("Index");
        }

        [HttpPost]
        public ActionResult SetDifficulty(DifficultyModel difficulty)
        {
            Session["difficulty"] = difficulty;

            // create board
            // pass board

            return View("~/Views/Game/Board.cshtml");

        }
    }
}