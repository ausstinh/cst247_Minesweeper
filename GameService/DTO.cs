using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using cst247_Minesweeper.Models;

namespace GameService
{
    [DataContract]
    public class DTO
    {
        [DataMember]
        public int ErrorCode { get; set; }
        [DataMember]
        public string ErrorMsg { get; set; }
        [DataMember]
        public List<ScoreModel> Data { get; set; }

        public DTO(int ErrorCode, string ErrorMsg, List<ScoreModel> Data)
        {
            this.ErrorCode = ErrorCode;
            this.ErrorMsg = ErrorMsg;
            this.Data = Data;
        }
    }
}