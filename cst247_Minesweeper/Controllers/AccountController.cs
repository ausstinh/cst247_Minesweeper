using cst247_Minesweeper.Models;
using System.Web.Mvc;
using cst247_Minesweeper.Models.business;

namespace cst247_Minesweeper.Controllers
{
    public class AccountController : Controller
    {
       
        public ActionResult Index()
        {
            return View("Login");
        }
      
        [HttpGet]
        public ActionResult Register()
        {
            return View("Register");
        }

        [HttpPost]
        public ActionResult Login(UserModel user)
        {
            AccountBusinessService bs = new AccountBusinessService();

            // -1 means login failed
            int loggedInId = bs.Authenticate(user);

            if (loggedInId != -1)
            {
                Session["user_id"] = loggedInId;
                return View("~/Views/Home/Index.cshtml",  user);
            }
            else
            {
                return View("LoginFail");
            }
        }

        [HttpPost]
        public ActionResult Register(UserModel user)
        {
            AccountBusinessService bs = new AccountBusinessService();

            if (bs.Register(user))
            {
                return View("Login", user);
            }
            else
            {
                return View("RegisterFail");
            }
        }

        [HttpGet]
        public ActionResult Logout()
        {
            Session["user_id"] = null;
            return View("Login");
        }
    }
}