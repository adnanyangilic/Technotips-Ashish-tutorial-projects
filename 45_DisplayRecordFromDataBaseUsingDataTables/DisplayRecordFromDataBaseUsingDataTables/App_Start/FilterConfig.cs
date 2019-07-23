using System.Web;
using System.Web.Mvc;

namespace DisplayRecordFromDataBaseUsingDataTables
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
