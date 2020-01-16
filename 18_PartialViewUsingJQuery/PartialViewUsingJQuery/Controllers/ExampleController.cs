using PartialViewUsingJQuery.Models;
using PartialViewUsingJQuery.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace PartialViewUsingJQuery.Controllers
{
    public class ExampleController : Controller
    {
        public ActionResult Index()
        {
            EmployeesDBEntities db = new EmployeesDBEntities();

            ViewBag.Employees = GetEmployees(db);

            return View();
        }

        /// <summary>
        ///     A PartialView bemutatásához elkészített metódus
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowEmployeeDetails(int EmployeeID)
        {
            EmployeesDBEntities db = new EmployeesDBEntities();

            ViewBag.EmployeeDetails = GetSelectedEmployee(db, EmployeeID);

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

        /// <summary>
        ///     A paraméterben megkapott ID-hoz tartozó összes adatöt kitörli mind az
        ///     Employee táblából mint pedig a Site táblából
        /// </summary>
        /// <param name="db">Adatbázis kapcsolat</param>
        /// <param name="EmployeeID">A törlendő ID-jú objektumok</param>
        /// <returns>True - Ha sikeres a törlés; False - Ha sikertelen</returns>
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
            List<Employee> employees = db.Employees.ToList();

            List<EmployeeViewModel> employeesInViewableFormat = employees.Select(x => new EmployeeViewModel
            {
                EmployeeID = x.EmployeeID,
                Name = x.Name,
                DepartmentName = x.Department.Name,
                Adress = x.Adress
            }).ToList();

            return employeesInViewableFormat;
        }

        /// <summary>
        ///     Visszatéríti azt az Employee adatait, amelynek ID-ja megegyezik a paraméterben kapott ID-val
        /// </summary>
        /// <param name="db">Adatbázis kapcsolat</param>
        /// <param name="employeeID">Keresendő Employee ID-ja</param>
        /// <returns>Olyan EmployeeViewModel, amelynek attribútumainak értékeivel fel tudjunk tölteni egy View-ot</returns>
        private EmployeeViewModel GetSelectedEmployee(EmployeesDBEntities db, int employeeID)
        {
            return ConvertSelectedEmployeeInViewableFormat(db.Employees.SingleOrDefault(x => x.EmployeeID == employeeID));
        }

        /// <summary>
        ///     A paraméterben megkapott Employee objektumból előállít, a View-on megjeleníthető EmployeeViewModel-t
        /// </summary>
        /// <param name="selectedEmployee">Az átalakítandó Employee objektum</param>
        /// <returns>Olyan EmployeeViewModel, amelynek attribútumainak értékeivel fel tudjunk tölteni egy View-ot</returns>
        private EmployeeViewModel ConvertSelectedEmployeeInViewableFormat(Employee selectedEmployee)
        {
            return (new EmployeeViewModel
            {
                Name = selectedEmployee.Name,
                DepartmentName = selectedEmployee.Department.Name,
                Adress = selectedEmployee.Adress,
                SiteName = selectedEmployee.Sites.SingleOrDefault(x => x.EmployeeID == selectedEmployee.EmployeeID).SiteName
            });
        }
    }
}