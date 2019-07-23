using HowToSendMultipleCheckboxIDs.Models;
using HowToSendMultipleCheckboxIDs.Models.MenuModels;
using HowToSendMultipleCheckboxIDs.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace HowToSendMultipleCheckboxIDs.Controllers
{
    public class ExampleController : Controller
    {
        private const int USERID = 3;

        #region Ajax hívás által használt metódusok

        // GET: Example
        public ActionResult Index()
        {
            EmployeesDBEntities db = new EmployeesDBEntities();

            ViewBag.Employees = GetEmployees(db);

            return View();
        }

        // GET: Második oldal Page
        public ActionResult SecondPage()
        {
            ViewBag.Message = "Hey! Üdvözöllek a második oldalon!";

            return View();
        }

        // GET Regisztráció Page
        public ActionResult Registration()
        {
            return View();
        }

        // GET Bejelentkezés Page
        public ActionResult Login()
        {
            return View();
        }

        // GET Kijelentkezés
        public ActionResult Logout()
        {
            /// Sessionból töröljük az adatokat
            Session.Clear();
            Session.Abandon();

            /// Navigálás a Login oldalra
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
        ///     Az Employee-k kereséséhez szükséges metódus
        /// </summary>
        /// <param name="SearchText"></param>
        /// <returns>
        ///     Egy olyan PartalView amely tartalmazza azt a kész táblázat törzset, amely
        ///     a keresési eredménynek megfelelően leszűkíti az eredmény halmazt
        /// </returns>
        public ActionResult GetSearchRecords(string SearchText)
        {
            EmployeesDBEntities db = new EmployeesDBEntities();

            return PartialView("GetSearchRecords", GetSearchedEmployees(db, SearchText));
        }

        /// <summary>
        ///     A jobb oldalsó menüsáv elkészítéséhez szükséges metódus
        /// </summary>
        /// <returns>
        ///     Egy PartialView amely tartalmazza a jobb oldalsó menüsávot
        /// </returns>
        public ActionResult SideMenu()
        {
            if (Session["UserID"] != null)
            {
                return PartialView("SideMenu", GetMenuItemsWhenUserLogIn());
            }
            else
            {
                return PartialView("SideMenu", GetMenuItemsWhenLogOut());
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
        ///     A paraméterben kapott RegistrationViewModel ben található adatokat elmenti az adatbázisba,
        ///     majd visszanavigál az INDEX oldalra
        /// </summary>
        /// <param name="registrationViewModel">A modell, amely a mentendő adatokat tartalmazza</param>
        /// <returns>URL cím, amely az Index oldalra navigál vissza (JSON)</returns>
        [HttpPost]
        public ActionResult RegisterUser(RegistrationViewModel registrationViewModel)
        {
            EmployeesDBEntities db = new EmployeesDBEntities();

            /// Új User regisztrálása
            RegisterNewUserInDB(db, registrationViewModel);

            /// Navigációs URL elkészítése az AJAX számára
            var createRedirectToViewURL = new UrlHelper(Request.RequestContext).Action("Index", "Example");

            /// Navigációs URL visszatérítése a VIEW oldalnak (JSON Objektumként)
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

            /// Ellenőrzés, hogy volt-e ilyen felhasználó - jelszó páros az adatbázisban
            if (user != null)
            {
                /// Session-ben eltároljuk a felhasználói adatokat
                Session["UserID"] = user.UserID;
                Session["UserName"] = user.UserName;

                /// Beállítjuk, hogy milyen felhasználó jelentkezett be, hogy a továbbnavigálás
                /// során meg tudjuk határozni a jogokat
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

        #endregion

        #region Ajax hívás által használt metódusokhoz tartozó függvények

        /// <summary>
        ///     Előállít egy olyan listát, amely tartalmazza azokat a gombokat a menüsávon
        ///     amelyeket akkor kell látni, amikor bármilyen jogosultságú User ki van
        ///     jelentkezve
        /// </summary>
        /// <returns>
        ///     Lista amely tartalmazza, hogy milyen gombok jelenjenek meg kijelentkezett
        ///     állapotban
        /// </returns>
        private List<MenuItem> GetMenuItemsWhenLogOut()
        {
            List<MenuItem> menuItemsList = new List<MenuItem>
            {
                new MenuItem { Link = "/Home/Index", LinkName = "Kezdőlap" },
                new MenuItem { Link = "/Example/Login", LinkName = "Bejelentkezés" },
                new MenuItem { Link = "/Example/Registration", LinkName = "Regisztráció" }
            };

            return menuItemsList;
        }

        /// <summary>
        ///     Előállít egy olyan listát, amely tartalmazza azokat a gombokat a menüsávon
        ///     amelyeket akkor kell látni, amikor User jogosultságú User be van
        ///     jelentkezve
        /// </summary>
        /// <returns>
        ///     Lista amely tartalmazza, hogy milyen gombok jelenjenek meg egy User bejelentkezett
        ///     állapotban
        /// </returns>
        private List<MenuItem> GetMenuItemsWhenUserLogIn()
        {
            List<MenuItem> menuItemsList = new List<MenuItem>
            {
                new MenuItem { Link = "/Example/Index", LinkName = "Kezdőlap" },
                new MenuItem { Link = "/Example/Logout", LinkName = "Kijelentkezés" },
            };

            return menuItemsList;
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
                /// SiteUser mentése
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
            /// Objektumok lekérdezése a táblákból
            Site deleteSiteRow = GetDeleteSiteRow(db, EmployeeID);
            Employee deleteEmployeeRow = GetDeleteEmployeeRow(db, EmployeeID);

            /// Ha minden objektum megtalálható az adott, hozzá tartozó táblákban, akkor...
            if (deleteSiteRow != null && deleteEmployeeRow != null)
            {
                /// Site táblából töröljük, az adott EmployeeID-jú sort
                DeleteSiteRowInSiteTable(db, deleteSiteRow);

                /// Employee táblából töröljük az adott EmployeeID-jú sort
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
            /// Adatok lekérdezése az Employee táblából
            List<Employee> employees = db.Employees.ToList();

            /// A lekérdezett adatokat átalakítjuk a View-on megjeleníthető formátumú objektummá
            /// amely a Dolgozó Nevét és ID-ját fogja tartalmazni
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
            List<EmployeeViewModel> employeesInViewableFormat = employees.Where(x => x.Name.ToUpper().Contains(SearchText.ToUpper())).Select(x => new EmployeeViewModel
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

        #endregion
    }
}