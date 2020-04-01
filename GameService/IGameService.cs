using cst247_Minesweeper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace GameService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IGameService
    {
        [OperationContract]
        GameModel GetGame(int owner_id);

        [OperationContract]
        int SaveGame(GameModel game);
    }

}
