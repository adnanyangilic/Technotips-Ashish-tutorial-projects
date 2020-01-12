using HyperlinksAndButtonsExample.Models;
using HyperlinksAndButtonsExample.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace HyperlinksAndButtonsExample.Controllers
{
    public class ExampleController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.MessageHL = "Egyszerű Hyperlink example bemutatása";
            ViewBag.MessageHLB = "Hyperlink example bemutatása Button objektumként";

            EmployeesDBEntities db = new EmployeesDBEntities();

            return View(GetEmployees(db));
        }

        public ActionResult EmployeeDetail(int EmployeeID)
        {
            EmployeesDBEntities db = new EmployeesDBEntities();

            return View(GetSelectedEmployeeDetail(db, EmployeeID));
        }

        /// <summary>
        ///     Lekérdezi és visszatéríti a kiválasztott Employee adatait
        /// </summary>
        /// <param name="db">Adatbázis kapcsolat</param>
        /// <param name="EmployeeID">A kiválasztott Employee ID-ja</param>
        /// <returns>A kiválasztott Employee adatai (Név, cím, osztálynév)</returns>
        private EmployeeViewModel GetSelectedEmployeeDetail(EmployeesDBEntities db, int EmployeeID)
        {
            Employee selectedEmployee = db.Employees.SingleOrDefault(x => x.EmployeeID == EmployeeID);

            return GetSelectedEmployeeDetailInViewableFormat(selectedEmployee);
        }

        /// <summary>
        ///     A kiválasztott Employee adatainak átalakítása a View-on megjeleníthető formában.
        /// </summary>
        /// <param name="selectedEmployee">A lekérdezett és visszakapott Employee összes adata</param>
        /// <returns>
        ///     A View-on megjeleníthető Employee objektum
        /// </returns>
        private EmployeeViewModel GetSelectedEmployeeDetailInViewableFormat(Employee selectedEmployee)
        {
            EmployeeViewModel employeeInViewableFormat = new EmployeeViewModel();

            employeeInViewableFormat.Name = selectedEmployee.Name;
            employeeInViewableFormat.Adress = selectedEmployee.Adress;
            employeeInViewableFormat.DepartmentName = selectedEmployee.Department.Name;

            return employeeInViewableFormat;
        }

        /// <summary>
        ///     Lekérdezi a Dolgozó Nevét és ID-ját az Employee Táblából
        /// </summary>
        /// <param name="db">Adatbázis kapcsolat</param>
        /// <returns>Dolgozói adatok a View-on megjeleníthető formátumban</returns>
        private List<EmployeeViewModel> GetEmployees(EmployeesDBEntities db)
        {
            List<Employee> employees = db.Employees.ToList();

            List<EmployeeViewModel> employeesInViewableFormat = employees.Select(x => new EmployeeViewModel
            {
                EmployeeID = x.EmployeeID,
                Name = x.Name
            }).ToList();

            return employeesInViewableFormat;
        }
    }
}
