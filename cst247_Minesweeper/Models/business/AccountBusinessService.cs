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

        public bool Authenticate(UserModel user)
        {
            if (ds.Authenticate(user))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool register(UserModel user)
        {
            if (ds.Create(user))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}