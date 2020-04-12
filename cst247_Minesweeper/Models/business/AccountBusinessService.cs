using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using cst247_Minesweeper.Models.data;

namespace cst247_Minesweeper.Models.business
{
    public class AccountBusinessService
    {
        private AccountDataService ds = new AccountDataService();

        public int Authenticate(UserModel user)
        {
            return ds.Authenticate(user);
        }

        public bool Register(UserModel user)
        {
            return ds.Create(user);
        }

        public UserModel getUser(int id)
        {
            return ds.Read(id);
        }

        public bool UpdateUser(UserModel user)
        {
            return ds.Update(user);
        }

        public bool ResetActiveGame(int id)
        {
            ActiveGameDataService gameDS = new ActiveGameDataService();
            
            UserModel user = getUser(id);

            gameDS.Delete(user.ActiveGameId);
            user.ActiveGameId = -1;
            return UpdateUser(user);
        }
    }
}