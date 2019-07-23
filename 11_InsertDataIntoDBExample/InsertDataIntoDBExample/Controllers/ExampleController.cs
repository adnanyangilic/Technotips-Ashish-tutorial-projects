using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using InsertDataIntoDBExample.Models;
using InsertDataIntoDBExample.Models.ViewModels;
using System;

namespace InsertDataIntoDBExample.Controllers
{
    public class ExampleController : Controller
    {
        // GET: Example
        public ActionResult Index()
        {
            ViewBag.MessageAboutHTMLHelper = "A HTML Helper a modell segítségével html elemeket generál";

            EmployeesDBEntities db = new EmployeesDBEntities();

            /// Eltároljuk a szótár elemeket, hogy egy SelectListBox-ot fel tudjunk tölteni
            /// Szintakszis(Átadandó lista, Melyik attribútumot szeretnénk szállítani, melyik attribútumot jelenítsük meg a View-on
            /// {Kulcs érték párok})
            ViewBag.DepartmentsDicitionaryTableElements = new SelectList(GetDepartmentsDictionaryTableElements(db), "DepartmentID", "Name");

            return View();
        }

        /// <summary>
        ///     A Submit gomb megnyomására hívódik meg a metódus, eltárolja az új Employee-t a
        ///     DB-be
        /// </summary>
        /// <param name="employeeInViewableFormat">Az eltárolandó Employee a View-tól</param>
        /// <returns></returns>
        public ActionResult SaveEmployee(EmployeeViewModel employeeInViewableFormat)
        {
            try
            {
                EmployeesDBEntities db = new EmployeesDBEntities();

                db.Employees.Add(CreateEmployeeInDBFormat(employeeInViewableFormat));

                db.SaveChanges();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("> ERROR - " + ex);
            }

            return RedirectToAction("Index");
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