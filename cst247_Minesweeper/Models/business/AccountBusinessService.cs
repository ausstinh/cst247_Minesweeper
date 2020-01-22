using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cst247_Minesweeper.Models.business
{
    public class AccountBusinessService
    {
        private AccountDataService ds = new AccountDataService();

        public bool Authenticate(UserModel user)
        {
            if (ds.authenticate(user))
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
            if (ds.create(user))
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