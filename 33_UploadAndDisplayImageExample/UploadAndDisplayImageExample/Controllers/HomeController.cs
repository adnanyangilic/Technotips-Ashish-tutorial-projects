using System.IO;
using System.Web.Mvc;
using UploadAndDisplayImageExample.Models.UploadsModels;

namespace UploadAndDisplayImageExample.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            /// Vizsgálat, hogy volt-e már bejelentkezett felhasználó,
            /// mert akkor a bejelentkeztetett Index-et jelenítjük meg
            /// a User-nak
            if (Session["UserID"] != null)
            {
                return RedirectToAction("../Example/Index");
            }

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
            /// Modellből lekérdezzük a feltöltendő fájl adatait
            var file = productViewModel.ImageFile;
            string fileName = "";

            /// Vizsgálat, hogy a fájl-t sikeresen megkaptuk-e
            if (file != null)
            {
                fileName = Path.GetFileName(file.FileName);
                string directoryName = "/UploadedImage/";
                string directoryPath = Server.MapPath(directoryName);

                /// Vizsgálat, hogy a könyvtár, ahová menteni szeretnénk, létezik-e...
                if (!Directory.Exists(directoryPath))
                {
                    /// Ha nem, létrehozzuk! 
                    Directory.CreateDirectory(directoryPath);
                }

                /// Fájl mentése a létrehozott könyvtáron belül a megadott fájl-néven
                file.SaveAs(Server.MapPath(directoryName + fileName));
            }

            /// JSON Objektum, amely tartalmazza a feltöltött fájl nevét a webszerveren
            return Json(fileName, JsonRequestBehavior.AllowGet);
        }
    }
}