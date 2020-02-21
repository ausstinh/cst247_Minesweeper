using cst247_Minesweeper.Models;
using cst247_Minesweeper.Models.business;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

            BoardModel board = new BoardModel(size);
            game.Board = board;

            GameBusinessService bs = new GameBusinessService(game);
            game.Board = bs.setupBombsAndNeighbors();

            game.Stopwatch = new Stopwatch();
            game.Stopwatch.Start();

            return View("~/Views/Game/Board.cshtml", game);

        }

        [HttpPost]
        public ActionResult onReveal(string coords)
        {
            string[] array = coords.Split('|');
            int y = int.Parse(array[0]);
            int x = int.Parse(array[1]);

            GameBusinessService bs = new GameBusinessService(game);

            game.Board = bs.revealOneCell(game.Board.TheGrid[y, x]);

            if (game.Board.TheGrid[y, x].Bomb)
            {
                game.Stopwatch.Stop();
                return View("~/Views/Game/Loss.cshtml", game);
            }

            if (bs.gameWin())
            {
                game.Stopwatch.Stop();
                return View("~/Views/Game/Scores.cshtml", game);
            }
          
            return View("~/Views/Game/Board.cshtml", game);

        }
    }
}