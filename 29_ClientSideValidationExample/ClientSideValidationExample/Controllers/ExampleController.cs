using ClientSideValidationExample.Models;
using ClientSideValidationExample.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ClientSideValidationExample.Controllers
{
    public class ExampleController : Controller
    {
        private const int USERID = 3;

        public ActionResult Index()
        {
            EmployeesDBEntities db = new EmployeesDBEntities();

            ViewBag.Employees = GetEmployees(db);

            return View();
        }

        public ActionResult SecondPage()
        {
            ViewBag.Message = "Hey! Üdvözöllek a második oldalon!";

            return View();
        }

        public ActionResult Registration()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();

            return RedirectToAction("Login");
        }

        /// <summary>
        ///     Az Employee adatainak megmutatásához szükséges metódus
        /// </summary>
        /// <returns>
        ///     Egy PartialView amely tartalmazza az adatok megjelenítéséhez szükséges
        ///     oldalt
        /// </returns>
        public ActionResult ShowEmployeeDetails(int EmployeeID)
        {
            EmployeesDBEntities db = new EmployeesDBEntities();

            ViewBag.EmployeeDetails = GetSelectedEmployee(db, EmployeeID);

            return PartialView("ShowEmployeeDetails");
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

            TempData["DepartmentsDicitionaryTableElements"] = new SelectList(GetDepartmentsDictionaryTableElements(db), "DepartmentID", "Name");
            TempData.Keep();

            if (EmployeeID != 0)
            {
                return PartialView("EditOrNewEmployee", GetSelectedEmployee(db, EmployeeID));
            }

            return PartialView("EditOrNewEmployee", new EmployeeViewModel());
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
        ///     A paraméterben kapott RegistrationViewModel ben található adatokat elmenti az adatbázisba,
        ///     majd visszanavigál az INDEX oldalra
        /// </summary>
        /// <param name="registrationViewModel">A modell, amely a mentendő adatokat tartalmazza</param>
        /// <returns>URL cím, amely az Index oldalra navigál vissza (JSON)</returns>
        [HttpPost]
        public ActionResult RegisterUser(RegistrationViewModel registrationViewModel)
        {
            EmployeesDBEntities db = new EmployeesDBEntities();

            RegisterNewUserInDB(db, registrationViewModel);

            var createRedirectToViewURL = new UrlHelper(Request.RequestContext).Action("Index", "Example");

            return Json(new { Url = createRedirectToViewURL });
        }

        /// <summary>
        ///     A paraméterben kapott LoginViewModel-ben található adatok alapján keres
        ///     egyezőséget az adatbázisban, és annak eredményeképp megfelelően bejelentkeztet
        /// </summary>
        /// <param name="loginViewModel">A modell, amely a keresendő adatokat tartalmazza</param>
        /// <returns>JSON objektum, amelyben található a megfelelő navigációs irány</returns>
        [HttpPost]
        public JsonResult LoginUser(Models.ViewModels.LoginViewModel loginViewModel)
        {
            string result = "";

            EmployeesDBEntities db = new EmployeesDBEntities();

            SiteUser user = GetSearchLoginUser(db, loginViewModel);

            if (user != null)
            {
                Session["UserID"] = user.UserID;
                Session["UserName"] = user.UserName;

                if (user.RoleID == 3)
                {
                    result = "User";
                }
                else if (user.RoleID == 1)
                {
                    result = "Admin";
                }
            }
            else
            {
                result = "NotFound";
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///     A felhasználó által megadott adatok alapján, megvizsgálja, hogy van-e egyezőség
        ///     az adatbázisban, azaz érvényes LOGIN kérést hajtottak-e végre
        /// </summary>
        /// <param name="db">Adatbázis kapcsolat</param>
        /// <param name="loginViewModel">A modell, amely a keresendő adatokat tartalmazza</param>
        /// <returns>
        ///     SiteUser objektum, amely sikeres keresés alapján az adott User-ről tartalmazza az
        ///     összes információt
        /// </returns>
        private SiteUser GetSearchLoginUser(EmployeesDBEntities db, Models.ViewModels.LoginViewModel loginViewModel)
        {
            return db.SiteUsers.SingleOrDefault(x => x.EmailID == loginViewModel.EmailID && x.Password == loginViewModel.Password);
        }

        /// <summary>
        ///     A Viewtól kapott paraméterben kapott RegistrationViewModel-t elmenti az adatbázisba
        /// </summary>
        /// <param name="db">Adatbázis kapcsolat</param>
        /// <param name="registrationViewModel">A mentendő SiteUser objektum</param>
        private void RegisterNewUserInDB(EmployeesDBEntities db, RegistrationViewModel registrationViewModel)
        {
            try
            {
                db.SiteUsers.Add(CreateNewSiteUser(db, registrationViewModel));
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("> ERROR - " + ex);
            }
        }

        /// <summary>
        ///     Átalakítja a View-tól kapott formátumú objektumból, egy, a DB-nek megfelelő
        ///     SiteUser objektummá.
        /// </summary>
        /// <param name="registrationViewModel">A View-tól kapott RegistrationViewModel objektum</param>
        /// <returns>A DB-nek megfelelő SiteUser modell</returns>
        private SiteUser CreateNewSiteUser(EmployeesDBEntities db, RegistrationViewModel registrationViewModel)
        {
            return (new SiteUser
            {
                UserName = registrationViewModel.UserName,
                EmailID = registrationViewModel.EmailID,
                Password = registrationViewModel.Password,
                Address = registrationViewModel.Adress,
                RoleID = USERID
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

            db.Employees.Add(employee);
            db.SaveChanges();

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
                EmployeeID = selectedEmployee.EmployeeID,
                Name = selectedEmployee.Name,
                DepartmentName = selectedEmployee.Department.Name,
                Adress = selectedEmployee.Adress,
                DepartmentID = selectedEmployee.DepartmentID,
                SiteName = selectedEmployee.Sites.SingleOrDefault(x => x.EmployeeID == selectedEmployee.EmployeeID).SiteName
            });
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