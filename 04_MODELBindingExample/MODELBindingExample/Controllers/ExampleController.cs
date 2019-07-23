using MODELBindingExample.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ViewInMVCExample.Controllers
{
    public class ExampleController : Controller
    {
        // GET: Example
        public ActionResult Index()
        {
            /// Modell átadása a View-nak
            return View(@GetEmployees());
        }

        /// <summary>
        ///     Employee-s típusú objektumokat tartalmazó lista feltöltése adatokkal, amely visszatéríthető
        /// </summary>
        /// <returns>Employee-s típusú objektumokat tartalmazó lista</returns>
        private List<Employee> GetEmployees()
        {
            List<Employee> employees = new List<Employee>();

            employees.Add(new Employee { EmployeeID = 1, Name = "Test_1", Department = "IT" });
            employees.Add(new Employee { EmployeeID = 2, Name = "Test_2", Department = "NOT_IT" });
            employees.Add(new Employee { EmployeeID = 3, Name = "Test_3", Department = "IT_2" });
            employees.Add(new Employee { EmployeeID = 4, Name = "Test_4", Department = "NOT_IT_2" });

            return employees;
        }
    }
}