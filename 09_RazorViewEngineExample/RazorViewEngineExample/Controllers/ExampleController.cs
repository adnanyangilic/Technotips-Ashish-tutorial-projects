using System.Web.Mvc;

namespace RazorViewEngineExample.Controllers
{
    public class ExampleController : Controller
    {
        // GET: Example
        public ActionResult Index()
        {
            ViewBag.MessageRVE = "Razor View Engine példa bemutatása...";

            return View();
        }
    }
}