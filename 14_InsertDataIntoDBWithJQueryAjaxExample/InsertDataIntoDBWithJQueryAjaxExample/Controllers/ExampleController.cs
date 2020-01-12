using InsertDataIntoDBWithJQueryAjaxExample.Models;
using InsertDataIntoDBWithJQueryAjaxExample.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace InsertDataIntoDBWithJQueryAjaxExample.Controllers
{
    public class ExampleController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.MessageAboutHTMLHelper = "A HTML Helper a modell segítségével html elemeket generál";

            EmployeesDBEntities db = new EmployeesDBEntities();

            TempData["DepartmentsDicitionaryTableElements"] =
                new SelectList(GetDepartmentsDictionaryTableElements(db), "DepartmentID", "Name");
            TempData.Keep();

            return View();
        }

        /// <summary>
        ///     A Submit gomb megnyomására hívódik meg a metódus, eltárolja az új Employee-t a
        ///     DB-be
        /// </summary>
        /// <param name="employeeInViewableFormat">Az eltárolandó Employee a View-tól</param>
        [HttpPost]
        public ActionResult SaveEmployee(EmployeeViewModel employeeInViewableFormat)
        {
            TempData.Keep();

            if (ModelState.IsValid)
            {
                try
                {
                    EmployeesDBEntities db = new EmployeesDBEntities();

                    Employee employee = CreateEmployeeInDBFormat(employeeInViewableFormat);

                    db.Employees.Add(employee);
                    db.SaveChanges();

                    db.Sites.Add(CreateSiteInDBFormat(employeeInViewableFormat, employee.EmployeeID));
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("> ERROR - " + ex);
                }

                return RedirectToAction("Index");
            }
            else
            {
                return View("Index", employeeInViewableFormat);
            }

        }

        /// <summary>
        ///     Átalakítja a View-tól kapott formátumú objektumból, egy, a DB-nek megfelelő
        ///     Employee objektummá.
        /// </summary>
        /// <param name="employeeInViewableFormat">A View-tól kapott EmployeeViewModel objektum</param>
        /// <returns>A DB-nek megfelelő Employee modell</returns>
        private Employee CreateEmployeeInDBFormat(EmployeeViewModel employeeInViewableFormat)
        {
            Employee employee = new Employee();

            employee.DepartmentID = employeeInViewableFormat.DepartmentID;
            employee.Name = employeeInViewableFormat.Name;
            employee.Adress = employeeInViewableFormat.Adress;

            return employee;
        }

        /// <summary>
        ///     Átalakítja a View-tól kapott formátumú objektumból, egy a DB-nek megfelelő Site
        ///     objektummá. Szükséges a rögzítéshez a legutóbb rögzített Employee objektum ID-ja,
        ///     ugyanis, hozzá fogjuk rendelni a Site-ot.
        /// </summary>
        /// <param name="employeeInViewableFormat">A View-tól kapott EmployeeViewModel objektum</param>
        /// <param name="lastestEmployeeID">A legutóbb rögzített Employee objektum ID-ja</param>
        /// <returns></returns>
        private Site CreateSiteInDBFormat(EmployeeViewModel employeeInViewableFormat, int lastestEmployeeID)
        {
            Site site = new Site();

            site.EmployeeID = lastestEmployeeID;
            site.SiteName = employeeInViewableFormat.SiteName;

            return site;
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

            foreach (Department item in departments)
            {
                System.Diagnostics.Debug.WriteLine(item.DepartmentID + " " + item.Name);
            }

            return departments;
        }
    }
}