using DBConnectionWithEntityFramework.Models;
using DBConnectionWithEntityFramework.Models.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace DBConnectionWithEntityFramework.Controllers
{
    public class ExampleController : Controller
    {
        // GET: Example
        public ActionResult Index()
        {
            /// Kapcsolódás az adatbázishoz
            EmployeesDBEntities db = new EmployeesDBEntities();

            return View(GetSelectedEmployee(db, 1));
        }

        /// <summary>
        ///     A paraméterben megadott ID-jú Employee-t megkeresi az Employee táblában
        /// </summary>
        /// <param name="db">Adatbázis kacsolat</param>
        /// <param name="id">A keresendő Employee ID-ja</param>
        /// <returns>Az x. ID-jú Employee objektum</returns>
        private EmployeeViewModel GetSelectedEmployee(EmployeesDBEntities db, int id)
        {
            /// Az 1-es sorszámú Employee típusú objektum lekérdezése az Employee táblából
            /// Lambda kifejezéssel
            Employee selectedEmployee = db.Employee.SingleOrDefault(x => x.EmployeeID == 1);

            return ConvertEmployeeToEmployeeViewModel(selectedEmployee);
        }
        
        /// <summary>
        ///     A paraméterben megkapott Employee típusú objektumot átalakítja
        ///     EmployeeViewModel típusú objektummá, azaz magát csak az Employee-ra vonatkozó adatokat
        ///     tárolja, az Employee táblából.
        /// </summary>
        /// <param name="selectedEmployee"></param>
        /// <returns>Csak az Employee táblában található adatokat tartalmazó objektum</returns>
        private EmployeeViewModel ConvertEmployeeToEmployeeViewModel(Employee selectedEmployee)
        {
            EmployeeViewModel viewEmployee = new EmployeeViewModel();

            viewEmployee.EmployeeID = selectedEmployee.EmployeeID;
            viewEmployee.DepartmentID = selectedEmployee.DepartmentID;
            viewEmployee.Adress = selectedEmployee.Adress;
            viewEmployee.Name = selectedEmployee.Name;

            return viewEmployee;
        }
    }
}