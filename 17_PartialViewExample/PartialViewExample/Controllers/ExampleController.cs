using PartialViewExample.Models;
using PartialViewExample.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace PartialViewExample.Controllers
{
    public class ExampleController : Controller
    {
        // GET: Example
        public ActionResult Index()
        {
            ViewBag.MessageAboutPartialView = "A Partial View egy visszaállítható/újrahasználható view, azaz képesek vagyunk " +
                "a view tartalmát duplikálni és mint egy View elemet újrahasználni, megjeleníteni más különböző View-okban. " +
                "Kiterjesztése szintén cshtml.";

            ViewBag.MessageHTMLPartial = "A @Html.Partial()-al csak átadjuk neki a PartialView-nevét és egy objektumot, vagy " +
                "objektumokat. Tehát lehetőségünk van egy vagy több PartialView-ot vagy egy vagy több objektumot átadni neki.";

            ViewBag.MessageHTMLRenderPartial = "Akkor használunk RenderPartial-t amikor be akarjuk tölteni egy PartialView-ot a Parent " +
                "Page-be és szintén át kell adni neki egy modelt, azaz ha adatot akarok átadni a PartialView-nak akkor így lehetőségünk " +
                "van a Model-t átadni";

            ViewBag.MessageHTMLRenderAction = "RederAction Szintaktika = @Html.RenderAction(Metódus név, Controller név, Model név) " +
                "Akkor használjuk ezt a változatot, amikor ActionResult-okat szeretnénk meghívni a Controllerekből, amely azt fogja tartalmazni " +
                "hogy melyik elkészített PartialView.cshtml fájlt töltsük be";

            EmployeesDBEntities db = new EmployeesDBEntities();

            ViewBag.Employees = GetEmployees(db);

            return View();
        }

        /// <summary>
        ///     A PartialView bemutatásához elkészített metódus
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowPartial()
        {
            return PartialView("PartialViewExample");
        }

        /// <summary>
        ///     A paraméterben megkapott indexű Employee-t kitörli az adatbázis-ból
        ///     és a hozzá kapcsolódó adatokat a többi táblából
        /// </summary>
        /// <param name="EmployeeID">A törlendő Employee ID-ja</param>
        /// <returns></returns>
        public JsonResult DeleteEmployee(int EmployeeID)
        {
            EmployeesDBEntities db = new EmployeesDBEntities();

            return Json(DeleteEmployeeFromDB(db, EmployeeID), JsonRequestBehavior.AllowGet);
        }

        private bool DeleteEmployeeFromDB(EmployeesDBEntities db, int EmployeeID)
        {
            Site deleteSiteRow = GetDeleteSiteRow(db, EmployeeID);
            Employee deleteEmployeeRow = GetDeleteEmployeeRow(db, EmployeeID);

            if (deleteSiteRow != null && deleteEmployeeRow != null)
            {
                DeleteSiteRowInSiteTable(db, deleteSiteRow);
                DeleteEmployeeRowInEmployeeTable(db, deleteEmployeeRow);

                return true;
            }

            return false;
        }

        /// <summary>
        ///     Visszatéríti azt a Site objektumot amelyet a Site táblából törölni kell
        /// </summary>
        /// <param name="db">Adatbázis kapcsolat</param>
        /// <param name="EmployeeID">A törlendő objektum EmployeeID-ja</param>
        /// <returns>Ha benne van a táblában az objektum, akkor az.</returns>
        private Site GetDeleteSiteRow(EmployeesDBEntities db, int EmployeeID)
        {
            return db.Sites.SingleOrDefault(x => x.EmployeeID == EmployeeID);
        }

        /// <summary>
        ///     Visszatéríti azt az Employee objektumot amelyet az Employee táblából törölni kell
        /// </summary>
        /// <param name="db">Adatbázis kapcsolat</param>
        /// <param name="EmloyeeID"></param>
        /// <returns></returns>
        private Employee GetDeleteEmployeeRow(EmployeesDBEntities db, int EmloyeeID)
        {
            return db.Employees.SingleOrDefault(x => x.EmployeeID == EmloyeeID);
        }

        private void DeleteSiteRowInSiteTable(EmployeesDBEntities db, Site deleteSiteRow)
        {
            db.Sites.Remove(deleteSiteRow);
            db.SaveChanges();
        }

        private void DeleteEmployeeRowInEmployeeTable(EmployeesDBEntities db, Employee deleteEmployeeRow)
        {
            db.Employees.Remove(deleteEmployeeRow);
            db.SaveChanges();
        }

        /// <summary>
        ///     Lekérdezi a Dolgozó Adatait az Employee és Department táblából
        /// </summary>
        /// <param name="db">Adatbázis kapcsolat</param>
        /// <returns>Dolgozói adatok a View-on megjeleníthető formátumban</returns>
        private List<EmployeeViewModel> GetEmployees(EmployeesDBEntities db)
        {
            /// Adatok lekérdezése az Employee táblából
            List<Employee> employees = db.Employees.ToList();

            /// A lekérdezett adatokat átalakítjuk a View-on megjeleníthető formátumú objektummá
            /// amely a Dolgozó Nevét és ID-ját fogja tartalmazni
            List<EmployeeViewModel> employeesInViewableFormat = employees.Select(x => new EmployeeViewModel
            {
                EmployeeID = x.EmployeeID,
                Name = x.Name,
                DepartmentName = x.Department.Name,
                Adress = x.Adress
            }).ToList();

            return employeesInViewableFormat;
        }
    }
}