using AutoMapper;
using CarLookUp.Core.ApplicationSettings;
using CarLookUp.Core.Utilities;
using CarLookUp.Web.ViewModels;
using System.Web.Mvc;

namespace CarLookUp.Web.Controllers
{
    /// <summary>
    /// Just home controller
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class HomeController : Controller
    {
        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.Title = TestApplicationSettings.Test;

            ViewBag.User = Mapper.Map<UserVM>(SessionManager.User);

            return View();
        }
    }
}
