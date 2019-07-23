using SearchingDataTableServerSide.Models;
using SearchingDataTableServerSide.Models.DataTableModels;
using SearchingDataTableServerSide.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SearchingDataTableServerSide.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            /// Vizsgálat, hogy volt-e már bejelentkezett felhasználó,
            /// mert akkor a bejelentkeztetett Index-et jelenítjük meg
            /// a User-nak
            if (Session["UserID"] != null)
            {
                return RedirectToAction("../Example/Index");
            }
            else
            {
                return View();
            }
        }

        /// <summary>
        ///     Visszatéríti a Dolgozói adatokat a View részére JSON objektumon
        ///     keresztül
        /// </summary>
        /// <returns>Dolgozói adatok JSON formában</returns>
        public JsonResult GetEmployeeRecords(DataTablesParam dataTablesParam)
        {
            EmployeesDBEntities db = new EmployeesDBEntities();

            List<EmployeeViewModel> employees = new List<EmployeeViewModel>();
        
            /// Vizsgálat, hogy a search mező üres volt-e, ha nem, akkor a szűrési feltételnek megfelelő
            /// adathalmazt jelenítjük meg, ha igen, akkor pedig az összes adatot megjelenítjük
            if(dataTablesParam.sSearch != null)
            {
                employees = GetSearchedEmployees(db, dataTablesParam.sSearch);
            }
            else
            {
                employees = GetEmployees(db);
            }

            return Json(new
            {
                aaData = employees,                             /// Adathalmaz inicializálása (A táblázatban található adatok)
                sEcho = dataTablesParam.sEcho,                  /// Szekvenciális növekedési információ. Ha valamilyen műveletet végzünk a DT-n akkor növekszik 1-el
                iTotalDisplayRecords = employees.Count(),       /// Bal sarokban megjelenő érték inicializálása
                iTotalRecords = employees.Count()               /// Összes sorra vonatkozó adatok megjelenítése (Bal alsó sarok vége)
            }
            , JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///     Az Employee adatainak szerkesztéséhez szükséges metódus
        /// </summary>
        /// <returns>
        ///     Egy PartialView amely tartalmazza az adatok szerkesztéséhez szükséges
        ///     oldalt
        /// </returns>
        public ActionResult EditOrNewEmployee(int EmployeeID)
        {
            EmployeesDBEntities db = new EmployeesDBEntities();

            /// Eltároljuk a szótár elemeket, hogy egy SelectListBox-ot fel tudjunk tölteni
            /// Szintakszis(Átadandó lista, Melyik attribútumot szeretnénk szállítani, melyik attribútumot jelenítsük meg a View-on
            /// {Kulcs érték párok})
            TempData["DepartmentsDicitionaryTableElements"] = new SelectList(GetDepartmentsDictionaryTableElements(db), "DepartmentID", "Name");
            TempData.Keep();

            /// Vizsgálat, hogy új Employee-t szeretnénk létrehozni, vagy pedig meglévőt szeretnénk
            /// szerkeszteni
            if (EmployeeID != 0)
            {
                return PartialView("EditOrNewEmployee", GetSelectedEmployee(db, EmployeeID));
            }
            else
            {
                return PartialView("EditOrNewEmployee", new EmployeeViewModel());
            }
        }

        /// <summary>
        ///     A menés gomb megnyomására hívódik meg a metódus. Ha az EmployeeID-ja 0 akkor
        ///     új adatot rögzítünk, ha pedig ID > 0 akkor frissítjük az adatokat
        /// </summary>
        /// <param name="employeeInViewableFormat">Az eltárolandó Employee a View-tól</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveEmployee(EmployeeViewModel employeeInViewableFormat)
        {
            try
            {
                EmployeesDBEntities db = new EmployeesDBEntities();

                if (employeeInViewableFormat.EmployeeID == 0)
                {
                    SaveNewEmployee(db, employeeInViewableFormat);
                }
                else
                {
                    UpdateEmployee(db, employeeInViewableFormat);
                    UpdateSite(db, employeeInViewableFormat);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("> ERROR - " + ex);
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        ///     Lekérdezi a Dolgozó Adatait az Employee és Department táblából
        /// </summary>
        /// <param name="db">Adatbázis kapcsolat</param>
        /// <returns>Dolgozói adatok a View-on megjeleníthető formátumban</returns>
        private List<EmployeeViewModel> GetEmployees(EmployeesDBEntities db)
        {
            /// Adatok lekérdezése az Employee táblából. A lekérdezett adatokat átalakítjuk
            /// a View-on megjeleníthető formátumú objektummá amely a Dolgozó Nevét és ID-ját
            /// fogja tartalmazni
            return db.Employees.Select(x => new EmployeeViewModel
            {
                EmployeeID = x.EmployeeID,
                Name = x.Name,
                DepartmentName = x.Department.Name,
                Adress = x.Adress
            }).ToList();
        }

        /// <summary>
        ///     Lekérdezi a Dolgozó Adatait az Employee és Department táblából a keresési
        ///     feltételnek megfelelően
        /// </summary>
        /// <param name="db">Adatbázis kapcsolat</param>
        /// <param name="SearchText">Szűrési feltétel</param>
        /// <returns>Dolgozói adatok a View-on megjeleníthető formátumban</returns>
        private List<EmployeeViewModel> GetSearchedEmployees(EmployeesDBEntities db, string SearchText)
        {
            /// Adatok lekérdezése az Employee táblából
            List<Employee> employees = db.Employees.ToList();

            /// A szűrési feltételnek megfelelően lekérdezett adatokat átalakítjuk
            /// a View-on megjeleníthető formátumú objektummá amely a Dolgozó Nevét
            /// és ID-ját fogja tartalmazni. 
            List<EmployeeViewModel> employeesInViewableFormat
                = employees.Where(x => x.Name.ToUpper().Contains(SearchText.ToUpper()) ||
                x.Department.Name.ToUpper().Contains(SearchText.ToUpper()) ||
                x.Adress.ToUpper().Contains(SearchText.ToUpper())).Select(x => new EmployeeViewModel
            {
                EmployeeID = x.EmployeeID,
                Name = x.Name,
                DepartmentName = x.Department.Name,
                Adress = x.Adress
            }).ToList();

            return employeesInViewableFormat;
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
                EmployeeID = selectedEmployee.EmployeeID,
                Name = selectedEmployee.Name,
                DepartmentName = selectedEmployee.Department.Name,
                Adress = selectedEmployee.Adress,
                DepartmentID = selectedEmployee.DepartmentID,
                SiteName = selectedEmployee.Sites.SingleOrDefault(x => x.EmployeeID == selectedEmployee.EmployeeID).SiteName
            });
        }

        /// <summary>
        ///     A Viewtól kapott paraméterben kapott EmployeeViewModel-t elmenti az adatbázisba
        /// </summary>
        /// <param name="db">Adatbázis kapcsolat</param>
        /// <param name="employeeInViewableFormat">A mentendő Employee objektum</param>
        private void SaveNewEmployee(EmployeesDBEntities db, EmployeeViewModel employeeInViewableFormat)
        {
            Employee employee = CreateEmployeeInDBFormat(employeeInViewableFormat);

            /// Employee mentése
            db.Employees.Add(employee);
            db.SaveChanges();

            /// Employee-hez tartozó Site mentése
            db.Sites.Add(CreateSiteInDBFormat(employeeInViewableFormat, employee.EmployeeID));
            db.SaveChanges();
        }

        /// <summary>
        ///     A Viewtól kapott paraméterben kapott EmployeeViewModel-t frissíti az adatbázis Employee táblájában
        /// </summary>
        /// <param name="db">Adatbázis kapcsolat</param>
        /// <param name="employeeInViewableFormat">A frissítendő Employee objektum</param>
        private void UpdateEmployee(EmployeesDBEntities db, EmployeeViewModel employeeInViewableFormat)
        {
            Employee employee = db.Employees.SingleOrDefault(x => x.EmployeeID == employeeInViewableFormat.EmployeeID);

            employee.EmployeeID = employeeInViewableFormat.EmployeeID;
            employee.Adress = employeeInViewableFormat.Adress;
            employee.DepartmentID = employeeInViewableFormat.DepartmentID;
            employee.Name = employeeInViewableFormat.Name;

            db.SaveChanges();
        }

        /// <summary>
        ///     A Viewtól kapott paraméterben kapott EmployeeViewModel-t frissíti az adatbázis Site táblájában
        /// </summary>
        /// <param name="db">Adatbázis kapcsolat</param>
        /// <param name="employeeInViewableFormat">A frissítendő Employee objektum</param>
        private void UpdateSite(EmployeesDBEntities db, EmployeeViewModel employeeInViewableFormat)
        {
            Site site = db.Sites.SingleOrDefault(x => x.EmployeeID == employeeInViewableFormat.EmployeeID);

            site.EmployeeID = employeeInViewableFormat.EmployeeID;
            site.SiteName = employeeInViewableFormat.SiteName;

            db.SaveChanges();
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
    }
}