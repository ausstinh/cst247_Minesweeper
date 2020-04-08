using cst247_Minesweeper.Models;
using cst247_Minesweeper.Models.business;
using GameService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Script.Serialization;

namespace HighScoreService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class HighScoreService : IHighScoreService
    {
        public DTO GetScore(string id)
        {
            GameBusinessService bs = new GameBusinessService();
            List<ScoreModel> score = new List<ScoreModel>();
            score.Add(bs.getScore(Int32.Parse(id)));

            if(score[0] == null)
            {
                DTO dto = new DTO(-1, "Score does not exist", null);
                return dto;
            }
            else
            {
                DTO dto = new DTO(0, "OK", score);
                return dto;
            }
        }

        public DTO GetAllScores()
        {
            GameBusinessService bs = new GameBusinessService();
            List<ScoreModel> scores = new List<ScoreModel>();
            scores = bs.getAllScores();

            if (scores.Count == 0)
            {
                DTO dto = new DTO(-1, "No scores found", scores);
                return dto;
            }
            else
            {
                DTO dto = new DTO(0, "OK", scores);
                return dto;
            }
        }
    }
}
