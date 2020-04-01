using cst247_Minesweeper.Models;
using cst247_Minesweeper.Models.business;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace cst247_Minesweeper.Controllers
{
    public class GameController : Controller
    {
        public static GameModel game;

        // GET: Game
        public ActionResult Index()
        {
            AccountBusinessService accountBS = new AccountBusinessService();
            GameBusinessService gameBS = new GameBusinessService();

            int userActiveGameId = accountBS.getUser(Int32.Parse(Session["user_id"].ToString())).ActiveGameId;
            if (userActiveGameId != -1)
            {
                GameModel activeGame = gameBS.getGame(new GameModel());
                return View("~/Views/Game/Board.cshtml", activeGame);
            }

            return View("Index");

        }

        [HttpPost]
        public ActionResult OnSetDifficulty(int difficulty)
        {
            game = new GameModel();

            BoardModel board = new BoardModel(difficulty);
            game.Board = board;

            GameBusinessService bs = new GameBusinessService(game);
            game.Board = bs.setupBoard(game.Board);

            game.Stopwatch = new Stopwatch();
            game.Stopwatch.Start();

            return View("~/Views/Game/Board.cshtml", game);

        }

        [HttpPost]
        public ActionResult OnReveal(string coords)
        {
            string[] array = coords.Split('|');
            int y = int.Parse(array[0]);
            int x = int.Parse(array[1]);

            GameBusinessService bs = new GameBusinessService(game);

            game.Board = bs.revealOneCell(game.Board.TheGrid[y, x]);

            if (game.Board.TheGrid[y, x].Bomb)
            {
                game.Stopwatch.Stop();
                return View("~/Views/Game/Loss.cshtml");
            }

            if (bs.gameWin())
            {
                game.Stopwatch.Stop();

                ScoreModel score = new ScoreModel(bs.calculateScore(game), game.Board.Difficulty, Int32.Parse(Session["user_id"].ToString()));
                bs.saveScore(score);

                return View("~/Views/Game/Win.cshtml", score);
            }

            AjaxOptions ajaxOptions = new AjaxOptions
            {
                HttpMethod = "POST",
                InsertionMode = InsertionMode.Replace,
                UpdateTargetId = "game"
            };

            Tuple<GameModel, AjaxOptions> tuple = new Tuple<GameModel, AjaxOptions>(game, ajaxOptions);

            return PartialView("~/Views/Game/_boardInfo.cshtml", tuple);

        }

        [HttpPost]
        public ActionResult OnGetGame(int game_id)
        {
            GameModel partialGame = new GameModel();
            partialGame.Id = game_id;
            GameBusinessService bs = new GameBusinessService();
            game = bs.getGame(partialGame);

            return View("~/Views/Game/Board.cshtml", game);
        }

        [HttpPost]
        public void OnSaveGame()
        {
            GameBusinessService gameBS = new GameBusinessService();
            game.Id = gameBS.saveGame(game);

            AccountBusinessService accountBS = new AccountBusinessService();
            UserModel user = accountBS.getUser(Int32.Parse(Session["user_id"].ToString()));
            user.ActiveGameId = game.Id;
            accountBS.UpdateUser(user);
        }

        [HttpGet]
        public ActionResult OnGetHighScores()
        {
            GameBusinessService bs = new GameBusinessService();

            IEnumerable<ScoreModel> highScores = bs.getHighScores();

            return View("~/Views/Game/HighScores.cshtml", highScores);
        }
    }
}