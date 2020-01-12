using System.Web.Mvc;

namespace RazorViewEngineExample.Controllers
{
    public class ExampleController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.MessageRVE = "Razor View Engine példa bemutatása...";

            return View();
        }
    }
}