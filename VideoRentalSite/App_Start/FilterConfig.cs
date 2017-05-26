using System.Web;
using System.Web.Mvc;
using VideoRentalSite.Filters;

namespace VideoRentalSite
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
