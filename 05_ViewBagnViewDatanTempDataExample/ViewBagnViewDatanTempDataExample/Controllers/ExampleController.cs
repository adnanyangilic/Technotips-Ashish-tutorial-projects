using System.Collections.Generic;
using System.Web.Mvc;
using ViewBagnViewDatanTempDataExample.Models;

namespace ViewBagnViewDatanTempDataExample.Controllers
{
    public class ExampleController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.MessageAboutVB = "A ViewBag egy dinamikus konténer amely képes tárolni egy listát " +
                "vagy bármilyen más típusú adathalmazt, azaz nem figyeli, hogy milyen típusú " +
                "az adat!";

            ViewBag.MessageAboutVD = "A ViewData egy objektum szótár azaz amely a ViewDataDictionary osztályból " +
                "származik és kulcsszavakkal elérhetőek. Elengedhetetlen a typecasting a kmplesz adattípusok számára és " +
                "szükséges figyelni, hogy nem-e null, a hibák elkerülése érdekében!";

            ViewBag.MessageAboutTD = "A TempData szintén egy szótár bojektum amely a TempDataDictionary-ból származik és " +
                "amelyeket egy rövíd életű sessionban tárolja. Ez a ViewBag és ViewData-val ellentétben segíti az adatok mozgatását " +
                "egy vagy több kontrolleren, illetve egy vagy több műveleten keresztül. Tehát ha egy VB-t vagy egy VD-t felhasználtunk " +
                "akkor már nincs lehetőségünk elérni ismételten, a TD-vel ellentétben";

            ViewBag.Employees = GetEmployees();

            ViewData["Employees"] = GetEmployees();

            ViewBag.EmployeeNameVB = "Teszt_Elek_With_VB";

            ViewData["EmployeeNameVD"] = "Teszt_Elek_With_VD";

            TempData["EmployeeName"] = "Teszt_Elek_Width_TD";
            TempData.Keep();

            return View();
        }

        public ActionResult SecondPage()
        {
            TempData.Keep();

            return View();   
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