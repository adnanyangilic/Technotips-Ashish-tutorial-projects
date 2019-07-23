using DeleteOperationWithPopupExample.Models;
using DeleteOperationWithPopupExample.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace DeleteOperationWithPopupExample.Controllers
{
    public class ExampleController : Controller
    {
        // GET: Example
        public ActionResult Index()
        {
            ViewBag.MessageAboutPartialView = "Partial View-ot akkor használunk amikor egy View tartalmát duplikálnunk " +
                "kell és minden elemet újra kell használnunk. Ennek a kiterjesztése szintén egy cshtml";

            EmployeesDBEntities db = new EmployeesDBEntities();

            ViewBag.Employees = GetEmployees(db);

            return View();
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

            if(deleteSiteRow != null && deleteEmployeeRow != null)
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