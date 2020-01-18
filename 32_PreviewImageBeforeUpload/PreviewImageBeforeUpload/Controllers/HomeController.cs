using System.Web.Mvc;

namespace PreviewImageBeforeUpload.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["UserID"] != null)
            {
                return RedirectToAction("../Example/Index");
            }

            return View();
        }
    }
}