using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using cst247_Minesweeper.Models.data;

namespace cst247_Minesweeper.Models.business
{
    public class GameBusinessService
    {
        private BoardModel Board;

        public GameBusinessService()
        {
        }

        public GameBusinessService(GameModel game)
        {
            this.Board = game.Board;
        }
        public BoardModel setupBoard(BoardModel board)
        {
            Random rnd = new Random();
            int amntBombs = (board.Size * board.Size * board.DifficultyPercent) / 100;
            board.amntBombs = amntBombs;
            board.Flags = amntBombs;

            int count = 0;
            int row = -1;
            int col = -1;
            while (amntBombs > count)
            {
                row = rnd.Next(0, board.Size);
                col = rnd.Next(0, board.Size);
                if (board.TheGrid[row, col].Bomb == false)
                {
                    board.TheGrid[row, col].Bomb = true;
                    count++;
                }
            }

            for (int y = 0; y < board.Size; y++)
            {
                for (int x = 0; x < board.Size; x++)
                {
                    CellModel c = board.TheGrid[y, x];
                    c.Neighbors = 0;
                    // N
                    if (inRange(c.Y + 1, c.X) && board.TheGrid[c.Y + 1, c.X].Bomb == true)
                        c.Neighbors++;
                    // NE
                    if (inRange(c.Y + 1, c.X + 1) && board.TheGrid[c.Y + 1, c.X + 1].Bomb == true)
                        c.Neighbors++;
                    // E
                    if (inRange(c.Y, c.X + 1) && board.TheGrid[c.Y, c.X + 1].Bomb == true)
                        c.Neighbors++;
                    // SE
                    if (inRange(c.Y - 1, c.X + 1) && board.TheGrid[c.Y - 1, c.X + 1].Bomb == true)
                        c.Neighbors++;
                    // S
                    if (inRange(c.Y - 1, c.X) && board.TheGrid[c.Y - 1, c.X].Bomb == true)
                        c.Neighbors++;
                    // SW
                    if (inRange(c.Y - 1, c.X - 1) && board.TheGrid[c.Y - 1, c.X - 1].Bomb == true)
                        c.Neighbors++;
                    // W
                    if (inRange(c.Y, c.X - 1) && board.TheGrid[c.Y, c.X - 1].Bomb == true)
                        c.Neighbors++;
                    // NW
                    if (inRange(c.Y + 1, c.X - 1) && board.TheGrid[c.Y + 1, c.X - 1].Bomb == true)
                        c.Neighbors++;
                }
            }
            Board = board;
            return board;
        }

        public BoardModel revealOneCell(CellModel mine)
        {
            if (!mine.Flagged)
            {
                if (!mine.Revealed)
                {
                    // game over if bomb
                    if (mine.Bomb)
                    {
                        // true = game over
                        return Board;
                    }
                    // flood fill
                    floodFillCellAndAround(mine.Y, mine.X);
                }
            }
            // false = not game over
            return Board;
        }

        public void floodFillCellAndAround(int y, int x)
        {
            if (inRange(y, x) && !Board.TheGrid[y, x].Revealed)
            {
                // reveal cell and add to counter
                Board.TheGrid[y, x].Revealed = true;
                Board.RevealCounter++;

                if (Board.TheGrid[y, x].Neighbors == 0)
                {
                    floodFillCellAndAround(y + 1, x - 1);
                    floodFillCellAndAround(y + 1, x);
                    floodFillCellAndAround(y + 1, x + 1);
                    floodFillCellAndAround(y, x + 1);
                    floodFillCellAndAround(y - 1, x + 1);
                    floodFillCellAndAround(y - 1, x);
                    floodFillCellAndAround(y - 1, x - 1);
                    floodFillCellAndAround(y, x - 1);
                }
            }

        }

        private bool inRange(int y, int x)
        {
            if (x >= 0 && x < Board.Size && y >= 0 && y < Board.Size)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool gameWin()
        {
            int nonBombs = (Board.Size * Board.Size) - Board.amntBombs;

            if (Board.RevealCounter == nonBombs)
            {
                return true;
            }

            return false;
        }

        public GameModel getGame(GameModel partialGame)
        {
            ActiveGameDataService GameDS = new ActiveGameDataService();
            GameModel game = new JavaScriptSerializer().Deserialize<GameModel>(GameDS.Read(partialGame.Id));
            game.Id = partialGame.Id;
            return game;
        }

        public int saveGame(GameModel game)
        {
            ActiveGameDataService GameDS = new ActiveGameDataService();
            if (GameDS.Read(game.Id) != null)
            {
                GameDS.Delete(game.Id);
            }

            return GameDS.Create(new JavaScriptSerializer().Serialize(game));
        }

        public int calculateScore(GameModel game)
        {
            int score = 0;
            int seconds = (int)game.Stopwatch.ElapsedMilliseconds / 1000;
            if (game.Board.Difficulty == 0)
            {
                score = (180 - seconds) * 10;
            }
            else if (game.Board.Difficulty == 1)
            {
                score = (240 - seconds) * 20;
            }
            else if (game.Board.Difficulty == 2)
            {
                score = (300 - seconds) * 30;
            }

            if (score < 0)
                score = 0;

            return score;
        }

        public bool saveScore(ScoreModel score)
        {
            ScoreDataService ds = new ScoreDataService();
            if (ds.Create(score))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public IEnumerable<ScoreModel> getHighScores()
        {
            ScoreDataService scoreDS = new ScoreDataService();
            List<ScoreModel> scores = scoreDS.ReadAll();

            AccountDataService accountDS = new AccountDataService();
            foreach(ScoreModel score in scores)
            {
                score.UserName = accountDS.Read(score.UserId).UserName;
            }

            var highScores =
                (from score in scores
                 orderby score
                 select score);//.Take(10);

            return highScores;
        }
    }
}