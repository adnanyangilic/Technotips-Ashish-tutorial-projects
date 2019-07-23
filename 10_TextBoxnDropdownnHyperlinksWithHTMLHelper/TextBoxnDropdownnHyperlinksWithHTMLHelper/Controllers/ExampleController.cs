using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TextBoxnDropdownnHyperlinksWithHTMLHelper.Models;

namespace TextBoxnDropdownnHyperlinksWithHTMLHelper.Controllers
{
    public class ExampleController : Controller
    {
        // GET: Example
        public ActionResult Index()
        {
            ViewBag.MessageAboutHTMLHelper = "A HTML Helper a modell segítségével html elemeket generál";

            EmployeesDBEntities db = new EmployeesDBEntities();

            /// Eltároljuk a szótár elemeket, hogy egy SelectListBox-ot fel tudjunk tölteni
            /// Szintakszis(Átadandó lista, Melyik attribútumot szeretnénk szállítani, melyik attribútumot jelenítsük meg a View-on
            /// {Kulcs érték párok})
            ViewBag.DepartmentsDicitionaryTableElements = new SelectList(GetDepartmentsDictionaryTableElements(db), "DepartmentID", "Name");

            return View();
        }

        /// <summary>
        ///     Visszatéríti a Departmnets Szótár Táblából az összes Departments elemet
        /// </summary>
        /// <param name="db">Adatbáziskapcsolat</param>
        /// <returns>Departments szótár tábla összes eleme</returns>
        private List<Department> GetDepartmentsDictionaryTableElements(EmployeesDBEntities db)
        {
            List<Department> departments = new List<Department>();

            departments = db.Departments.ToList();
            
            foreach(Department item in departments)
            {
                System.Diagnostics.Debug.WriteLine(item.DepartmentID + " " + item.Name);
            }

            return departments;
        }
    }
}