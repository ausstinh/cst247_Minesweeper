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
            int difficultyInt = (int)difficulty.Difficulty;
            Session["difficulty"] = difficultyInt;

            // create board
            // pass board
            BoardModel board;
            if (difficultyInt == 0)
            {
                board = new BoardModel(5);
            }
            else if (difficultyInt == 1)
            {
                board = new BoardModel(8);
            }
            else
            {
                board = new BoardModel(10);
            }
            GameModel game = new GameModel();
            game.Board = board;

            return View("~/Views/Game/Board.cshtml", game);

        }
    }
}