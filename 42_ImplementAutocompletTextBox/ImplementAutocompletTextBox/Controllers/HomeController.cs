using ImplementAutocompletTextBox.Models.ShopModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ImplementAutocompletTextBox.Controllers
{
    public class HomeController : Controller
    {
        private List<ShopItems> shopItemsList;

        public HomeController()
        {
            shopItemsList = GetShopItemList();
        }

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
        ///     Visszaad egy olyan találati listát, amelyben bármelyik elemben
        ///     megtalálható a keresett karakterlánc
        /// </summary>
        /// <param name="SeaarchText">A keresendő karakterlánc</param>
        /// <returns>Egy olyan JSON objektum, amely tartalmazza a találati listát</returns>
        [HttpGet]
        public JsonResult GetSuggestionListFromShopItemList(string SeaarchText)
        {
            return Json(CreateSuggestionList(SeaarchText), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///     Elkészít egy olyan listát, amely Shop objektumokat tartalmaz,
        ///     azaz a Shop-ban található ShopItem objektumok
        /// </summary>
        /// <returns>ShopItem objektumokat tartalmazó lista</returns>
        private List<ShopItems> GetShopItemList()
        {
            List<ShopItems> shopItemList = new List<ShopItems>
            {
                new ShopItems { ID = 1, Name = "Rízs", IsAvailable = true },
                new ShopItems { ID = 2, Name = "Só", IsAvailable = false },
                new ShopItems { ID = 3, Name = "Cukor", IsAvailable = true },
                new ShopItems { ID = 4, Name = "Szappan", IsAvailable = false },
                new ShopItems { ID = 5, Name = "Könyv", IsAvailable = true },
                new ShopItems { ID = 6, Name = "Alma", IsAvailable = true },
                new ShopItems { ID = 7, Name = "Körte", IsAvailable = false },
                new ShopItems { ID = 8, Name = "Cseresznye", IsAvailable = true },
                new ShopItems { ID = 9, Name = "Szilva", IsAvailable = false },
                new ShopItems { ID = 10, Name = "Barack", IsAvailable = true }
            };

            return shopItemList;
        }

        /// <summary>
        ///     Megkeresi a paraméterben kapott karakterláncot, az előre
        ///     elkészített Objektum listában. LINQ-val visszaad minden olyan
        ///     ShopItem.Name karakterláncot, amelyben megtalálhatóak a paraméterben
        ///     kapott karakterlánc
        /// </summary>
        /// <param name="SearchText">A keresendő karakterlánc</param>
        /// <returns>   
        ///     Egy olyan Lista, amely tartalmazza azokat a karakterláncokat, amelyben
        ///     megtalálható a paraméterben kapott karakterlánc
        /// </returns>
        private List<string> CreateSuggestionList(string SearchText)
        {
            return shopItemsList.Where(x => x.Name.ToLower().Contains(SearchText.ToLower())).Select(x => x.Name).ToList();
        }
    }
}