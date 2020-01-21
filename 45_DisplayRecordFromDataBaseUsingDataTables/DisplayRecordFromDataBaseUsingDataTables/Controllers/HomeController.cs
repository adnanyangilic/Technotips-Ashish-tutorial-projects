using DisplayRecordFromDataBaseUsingDataTables.Models;
using DisplayRecordFromDataBaseUsingDataTables.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace DisplayRecordFromDataBaseUsingDataTables.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        ///     Visszatéríti a Dolgozói adatokat a View részére JSON objektumon
        ///     keresztül
        /// </summary>
        /// <returns>Dolgozói adatok JSON formában</returns>
        public JsonResult GetEmployeeRecords()
        {
            EmployeesDBEntities db = new EmployeesDBEntities();

            return Json(GetEmployees(db), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///     Lekérdezi a Dolgozó Adatait az Employee és Department táblából
        /// </summary>
        /// <param name="db">Adatbázis kapcsolat</param>
        /// <returns>Dolgozói adatok a View-on megjeleníthető formátumban</returns>
        private List<EmployeeViewModel> GetEmployees(EmployeesDBEntities db)
        {
            return db.Employees.Select(x => new EmployeeViewModel
            {
                EmployeeID = x.EmployeeID,
                Name = x.Name,
                DepartmentName = x.Department.Name,
                Adress = x.Adress
            }).ToList();
        }
    }
}