using HowToDisplayMultipleCheckBox.Models.CheckBoxModels;
using System.Collections.Generic;
using System.Web.Mvc;

namespace HowToDisplayMultipleCheckBox.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            /// Eltároljuk ViewBag-ben az előre definiált ShopItem listát
            ViewBag.ShopItemList = GetShopItemList();
   
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
                new ShopItems { ID = 5, Name = "Könyv", IsAvailable = true }
            };

            return shopItemList;
        }
    }
}