using CascadingDropdownList.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace CascadingDropdownList.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            EmployeesDBEntities db = new EmployeesDBEntities();

            TempData["CountryDicitionaryTableElements"] = new SelectList(GetCountryDictionaryTableElements(db), "CountryID", "CountryName");
            TempData.Keep();

            return View();
        }

        /// <summary>
        ///     Visszaad egy olyan PartialView-ot, amely tartalmaz egy olyan HTML
        ///     kódot, ami inicializál egy DropDownList-et
        /// </summary>
        /// <param name="CountryID">Az ID-nak megfelelő State-ket kérdezzük le a DB-ből</param>
        /// <returns>Olyan PartialView amely inicializál egy </returns>
        public ActionResult GetStates(int CountryID)
        {
            EmployeesDBEntities db = new EmployeesDBEntities();

            TempData["StateDicitionaryTableElements"] = new SelectList(GetStateDictionaryTableElements(db, CountryID), "StateID", "StateName");
            TempData.Keep();

            return PartialView("StateOptionPartial");
        }

        /// <summary>
        ///     Visszatéríti a State Szótár Táblából az összes State elemet
        /// </summary>
        /// <param name="db">Adatbáziskapcsolat</param>
        /// <returns>State szótár tábla összes eleme</returns>
        private List<State> GetStateDictionaryTableElements(EmployeesDBEntities db, int CountryID)
        {
            return db.States.Where(x => x.CountryID == CountryID).ToList();
        }

        /// <summary>
        ///     Visszatéríti a Country Szótár Táblából az összes Country elemet
        /// </summary>
        /// <param name="db">Adatbáziskapcsolat</param>
        /// <returns>Country szótár tábla összes eleme</returns>
        private List<Country> GetCountryDictionaryTableElements(EmployeesDBEntities db)
        {
            return db.Countries.ToList();
        }
    }
}