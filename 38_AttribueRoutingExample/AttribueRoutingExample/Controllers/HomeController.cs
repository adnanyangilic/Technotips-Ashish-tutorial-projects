using System.Web.Mvc;

namespace AttribueRoutingExample.Controllers
{
    /// [RouteArea("City")]         -> Definiáljuk, hogy melyik RouteArea-ba tartozzon a kontrollers
    /// [RoutePrefix("Home")]       -> Default Route definiálása (Controller Name)
    /// [Route("{action=Index}")]   -> Default action definiálása (Method Name) 
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
            else
            {
                return View();
            }
        }

        /// <summary>
        ///     Visszaadja az összes tanuló rekordját
        /// </summary>
        /// <returns>Karakterlánc, amely tartalmazza az összes tanuló rekordját</returns>
        [Route("Student")]
        public string GetStudent()
        {
            return "Az Összes Tanuló Rekordja";
        }

        /// <summary>
        ///     ../{ID:int:min(2)...}   -> Lehetőségünk van megadni paramétert. 
        ///     A példában ábrázolt formában, meghatározhatjuk, hogy milyen adattípusú
        ///     paramétert várunk, illeve azokra megszorításokat tehetünk, min, max, stb...
        ///     Ha nem felel meg a megszorításoknak, akkor a következő vele megegyező nevű
        ///     metódusra ugrik át, és próbálja lefuttatni a függvényben található sorokat
        /// </summary>
        /// <param name="ID">A keresett tanuló ID-ja</param>
        /// <returns>Karakterlánc, a keresett tanuló ID-jával</returns>
        [Route("Student/{ID:int:min(2):max(10)}")]
        public string GetStudent(int ID)
        {
            return "A(z) " + ID + ". ID-jú Tanuló Rekordja";
        }
        
        /// <summary>
        ///     ../{Name}          -> Szükséges átadni paramétert, különben Exception-t dob
        ///     ../{Name?}         -> Nem szükséges átadni paramétert, nem dob Exception-t
        ///     ../{Name=Valami}   -> Defaul-t paramétert adunk át abban az esetben ha nem adunk át semmit,
        ///                            Különben pedig az átadott értéket fogja átadni
        ///     Nem szükséges átadni paramétert
        /// </summary>
        /// <param name="Name">A keresett tanuló neve</param>
        /// <returns>Karakterlánc, a keresett tanuló nevével</returns>
        [Route("Student/{Name=Johny}")]
        public string GetStudent(string Name)
        {
            return "A keresett Tanuló neve: " + Name;
        }
    }
}