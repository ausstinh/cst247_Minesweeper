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
        public static GameModel game;

        // GET: Game
        public ActionResult Index()
        {
            return View("Index");
        }

        [HttpPost]
        public ActionResult SetDifficulty(int size)
        {
            game = new GameModel();
            game.Size = size;

            // create board
            // pass board
            BoardModel board;

            board = new BoardModel(size);

            game.Board = board;

            return View("~/Views/Game/Board.cshtml", game);

        }

        [HttpPost]
        public ActionResult onReveal(string coords)
        {
            string[] array = coords.Split('|');
            int y = int.Parse(array[0]);
            int x = int.Parse(array[1]);

            game.Board.revealOneCell(game.Board.TheGrid[y, x]);

            return View("~/Views/Game/Board.cshtml", game);

        }
    }
}