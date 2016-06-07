using CarLookUp.Web.Filters;
using System.Web;
using System.Web.Mvc;

namespace CarLookUp
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new ErrorHandler());
        }
    }
}
