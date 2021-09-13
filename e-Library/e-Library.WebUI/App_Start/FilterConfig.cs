using System.Web;
using System.Web.Mvc;

namespace e_Library.WebUI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new RequireHttpsAttribute()); //Added for Social Login
        }
    }
}
