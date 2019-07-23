using System.Web.Mvc;

namespace CreateLoginExample.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            /// Vizsgálat, hogy volt-e már bejelentkezett felhasználó,
            /// mert akkor a bejelentkeztetett Index-et jelenítjük meg
            /// a User-nak
            if(Session["UserID"] != null)
            {
                return RedirectToAction("../Example/Index");
            }

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}