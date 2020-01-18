using HowToReturnMultipleModels.Models.ABCModels;
using System.Collections.Generic;
using System.Web.Mvc;

namespace HowToReturnMultipleModels.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["UserID"] != null)
            {
                return RedirectToAction("../Example/Index");
            }

            return View(GetAFinalModel());
        }

        /// <summary>
        ///     Előállít egy AModel típusú listát, amelyet feltölt AModel
        ///     típusú objektumokkal
        /// </summary>
        /// <returns>AModel típusú objektumokat tartalmazó lista</returns>
        private List<AModel> GetAModelsList()
        {
            List<AModel> list_A = new List<AModel>();

            list_A.Add(new AModel { Name = "Test_Name_1" });
            list_A.Add(new AModel { Name = "Test_Name_2" });
            list_A.Add(new AModel { Name = "Test_Name_3" });

            return list_A;
        }

        /// <summary>
        ///     Előállít egy BModel típusú listát, amelyet feltölt BModel
        ///     típusú objektumokkal
        /// </summary>
        /// <returns>BModel típusú objektumokat tartalmazó lista</returns>
        private List<BModel> GetBModelsList()
        {
            List<BModel> list_B = new List<BModel>();

            list_B.Add(new BModel { Country = "Hungary" });
            list_B.Add(new BModel { Country = "USA" });
            list_B.Add(new BModel { Country = "Canada" });

            return list_B;
        }

        /// <summary>
        ///     Előállít egy olyan végelges model-t, amely modelleket tartalmaz
        ///     de ezzel az előállított véglelges model-el fogunk dolgozni a 
        ///     View oldalon
        /// </summary>
        /// <returns>
        ///     Olyan előállított végleges model, amely tartalmazza az:
        ///         AModel-eket tartalmazó listát
        ///         BModel-eket tartalmazó listát
        ///         és egy Életkor típusú attribútumot
        /// </returns>
        private CModel GetAFinalModel()
        {
            CModel finalModel = new CModel();

            finalModel.AModelsList = GetAModelsList();
            finalModel.BModelsList = GetBModelsList();
            finalModel.Age = 12;

            return finalModel;
        }
    }
}