using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UploadImageByCopyingImageLink.Models;
using UploadImageByCopyingImageLink.Models.UploadModels;

namespace UploadImageByCopyingImageLink.Controllers
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
        ///     JSON objektum, amely tartalmazza az adatbázis táblájában
        ///     hanyas ID-t képviseli az újonnan beszúrt fájl
        /// </returns>
        public JsonResult ImageUpload(ProductViewModel productViewModel)
        {
            EmployeesDBEntities db = new EmployeesDBEntities();
            HttpPostedFileWrapper file = null;

            int lastUploadedImageID = 0;

            /// Modellből lekérdezzük a feltöltendő fájl adatait
            file = productViewModel.ImageFile;

            /// Vizsgálat, hogy a fájl-t sikeresen megkaptuk-e
            if (file != null)
            {
                /// Kép elmentése az adatbázisba
                lastUploadedImageID = SaveFileOnSQLDB(db, file);

                /// JSON Objektum, amely tartalmazza a feltöltött fájl nevét a webszerveren
                return Json(lastUploadedImageID, JsonRequestBehavior.AllowGet);
            }

            /// Vizsgálat, hogy az URL-t sikeresen megkaptuk-e
            if(productViewModel.ImageURL != null)
            {
                /// Kép elmentése az adatbázisba
                lastUploadedImageID = SaveFileByURLOnSQLDB(db, productViewModel.ImageURL);

                /// JSON Objektum, amely tartalmazza a feltöltött fájl nevét a webszerveren
                return Json(lastUploadedImageID, JsonRequestBehavior.AllowGet);
            }

            /// JSON Objektum, amely tartalmazza a feltöltött fájl nevét a webszerveren
            return Json(lastUploadedImageID, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///     Az paraméterben megadott ID-jú képet kikeresi az adatbázisből, majd
        ///     azt a fájl-t visszatéríti a View-nak megjelenítési célból
        /// </summary>
        /// <param name="lastUploadedImageID">A keresendő kép ID-ja</param>
        /// <returns>Olyan fájl, amely tartalmazza a megjelenítendő kép adattartalmát</returns>
        public ActionResult ShowLastUploadedImage(int lastUploadedImageID)
        {
            EmployeesDBEntities db = new EmployeesDBEntities();

            return File(db.ImageStore.SingleOrDefault(x => x.ImageID == lastUploadedImageID).ImageByte, "image/jpg");
        }

        /// <summary>
        ///     Elment egy képet az adatbázisba, majd a legutóbb feltöltött kép ID-ját
        ///     visszatéríti
        /// </summary>
        /// <param name="db">Adatbázis kapcsolat</param>
        /// <param name="URL">A mentendő fájl elérési útvonala</param>
        /// <returns>A legutóbb feltöltött fájl ID-ja</returns>
        private int SaveFileByURLOnSQLDB(EmployeesDBEntities db, string URL)
        {
            try
            {
                string fileName = GetFileName(URL);
                string directoryName = "/UploadedImage/";

                /// Objektum, amely az adatbázisnak megfelelő objektumként áll elő
                ImageStore image = CreateANewImageForDB(fileName, new WebClient().DownloadData(URL), directoryName + fileName);

                /// A korábban elkészített objektum hozzáadása a megfelelő táblához
                /// majd adatbázis mentés
                db.ImageStore.Add(image);
                db.SaveChanges();

                return image.ImageID;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        ///     Elment egy képet az adatbázisba, majd a legutóbb feltöltött kép ID-ját
        ///     visszatéríti
        /// </summary>
        /// <param name="db">Adatbázis kapcsolat</param>
        /// <param name="file">A mentendő fájl adatai</param>
        /// <returns>A legutóbb feltöltött fájl ID-ja</returns>
        private int SaveFileOnSQLDB(EmployeesDBEntities db, HttpPostedFileWrapper file)
        {
            /// Fájl kiolvasása az InputStream-ből
            BinaryReader binaryReader = new BinaryReader(file.InputStream);
            string directoryName = "/UploadedImage/";
            string fileName = Path.GetFileName(file.FileName);

            /// Objektum, amely az adatbázisnak megfelelő objektumként áll elő
            ImageStore image = CreateANewImageForDB(fileName, binaryReader.ReadBytes(file.ContentLength), directoryName + fileName);

            /// A korábban elkészített objektum hozzáadása a megfelelő táblához
            /// majd adatbázis mentés
            db.ImageStore.Add(image);
            db.SaveChanges();

            return image.ImageID;
        }

        /// <summary>
        ///     Elkészíti az adatbázis megfelelő táblájának megfelelő objektumát
        ///     amivel el lehet menteni a feltöltendő képet
        /// </summary>
        /// <param name="fileName">Fájl neve</param>
        /// <param name="imageByte">Fájl tartalma bináris adatként</param>
        /// <param name="imagePath">Kép elérési útvonala (Ha webszerveren lenne)</param>
        /// <returns>
        ///     Egy olyan objektum amely megfelel annak az adatbázis táblának,
        ///     amelybe menteni szeretnénk az aktuális feltöltendő képet
        /// </returns>
        private ImageStore CreateANewImageForDB(string fileName, byte[] imageByte, string imagePath)
        {
            return (new ImageStore
            {
                ImageName = fileName,
                ImageByte = imageByte,
                ImagePath = imagePath
            });
        }

        /// <summary>
        ///     A paraméterben kapott URL-ben levágja a fájl nevét
        /// </summary>
        /// <param name="URL">A vizsgálandó URL</param>
        /// <returns>Az URL-ben található fájlnév</returns>
        private string GetFileName(string URL)
        {
            int i = URL.LastIndexOf("/") + 1;
            int length = URL.Length - i;

            return URL.Substring(i, length);
        }
    }
}