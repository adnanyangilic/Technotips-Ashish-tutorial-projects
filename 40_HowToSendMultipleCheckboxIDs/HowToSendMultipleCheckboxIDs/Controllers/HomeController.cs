using HowToSendMultipleCheckboxIDs.Models.CheckBoxModels;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace HowToSendMultipleCheckboxIDs.Controllers
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
            /// Eltároljuk ViewBag-ben az előre definiált ShopItem listát
            ViewBag.ShopItemList = shopItemsList;

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
        ///     A listában található elemek frissítéséhez szükséges metódus
        /// </summary>
        /// <param name="CheckedItemList">Azokat az ID-k amelyeknek az értékük TRUE lesz</param>
        /// <returns>
        ///     Egy olyan PartalView amely tartalmazza azt a kész CheckBoxItem Listát amely,
        ///     a frissített lista alapján tevődik össze
        /// </returns>
        public ActionResult RefreshItemsList(string CheckedItemList)
        {
            /// Feldaraboljuk a karekterláncot tömb formátummá
            string[] checkedItemIDList = CheckedItemList.Split(',');

            /// Lita inicializálása/ beállítása
            SetAllItemFalseStatus(shopItemsList);
            SetAllItemStatus(shopItemsList, checkedItemIDList);

            return PartialView("RefreshItemsList", shopItemsList);
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

        /// <summary>
        ///     Beállítja az összes olyan objektum státuszát TRUE-ra amelyek a paraméterben
        ///     kapott tömb ID-jával megfelel
        /// </summary>
        /// <param name="shopItemsList">Lista, amely a ShopItem objektumokat tartalmazza</param>
        /// <param name="checkedItemIDList">Azokat az ID-kat tartalmazza amelyeknek a Státuszát át kell állítani</param>
        private void SetAllItemStatus(List<ShopItems> shopItemsList, string[] checkedItemIDList)
        {
            foreach(var item in checkedItemIDList)
            {
                shopItemsList.Find(x => x.ID == Int32.Parse(item)).IsAvailable = true;
            }
        }

        /// <summary>
        ///     Beállítja az összes a listában található objektum státuszát False-ra
        /// </summary>
        /// <param name="shopItemList">Lista, amely a ShopItem objektumokat tartalmazza</param>
        private void SetAllItemFalseStatus(List<ShopItems> shopItemList)
        {
            foreach(ShopItems item in shopItemList)
            {
                item.IsAvailable = false;
            }
        }
    }
}