using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WorkingWithMultipleTablesExample.Models;
using WorkingWithMultipleTablesExample.Models.ViewModels;

namespace WorkingWithMultipleTablesExample.Controllers
{
    public class ExampleController : Controller
    {
        // GET: Example
        public ActionResult Index()
        {
            EmployeesDBEntities db = new EmployeesDBEntities();

            return View(GetEmployeesData(db));
        }

        /// <summary>
        ///     Visszatérití az összes dolgozói adatot (ID, Név, Osztálynév, Cím Formátumban)
        /// </summary>
        /// <param name="db">Adatbázis kapcsolat</param>
        /// <returns>Az összes Employees adat</returns>
        private List<EmployeeViewModel> GetEmployeesData(EmployeesDBEntities db)
        {
            List<Employee> employees = db.Employees.ToList();

            return CreateEmployeeInViewableFormat(employees);
        }

        /// <summary>
        ///     A paraméterben kapott Dolgozói adatokból előállítja a View oldalon megjeleníthatő
        ///     formátumban. (ID, Név, Osztálynév, Cím Formátum)
        /// </summary>
        /// <param name="employees">Az adathalmaz amelyet át kell alakítani</param>
        /// <returns>Az összes Dolgozói/ Employees adat a megfelelő formátumba</returns>
        private List<EmployeeViewModel> CreateEmployeeInViewableFormat(List<Employee> employees)
        {
            return employees.Select(x => new EmployeeViewModel
            {
                EmployeeID = x.EmployeeID,
                Name = x.Name,
                DepartmentName = x.Department.Name,
                Adress = x.Adress
            }).ToList();
        }
    }
}