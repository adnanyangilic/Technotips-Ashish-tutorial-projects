using System.IO;
using System.Web.Mvc;
using UploadAndDisplayImageExample.Models.UploadsModels;

namespace UploadAndDisplayImageExample.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        ///     A kép feltöltéséért felelős metódus
        /// </summary>
        /// <param name="productViewModel">
        ///     Modell, amely tartalmazza a feltöltendő fájlhoz szükséges
        ///     adatokat
        /// </param>
        /// <returns>
        ///     JSON objektum, amely tartalmazza a webszerveren található
        ///     feltöltött fájl új nevét + kiterjesztésést
        /// </returns>
        public JsonResult ImageUpload(ProductViewModel productViewModel)
        {
            var file = productViewModel.ImageFile;
            string fileName = "";

            if (file != null)
            {
                fileName = Path.GetFileName(file.FileName);
                string directoryName = "/UploadedImage/";
                string directoryPath = Server.MapPath(directoryName);

                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                file.SaveAs(Server.MapPath(directoryName + fileName));
            }

            return Json(fileName, JsonRequestBehavior.AllowGet);
        }
    }
}