using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarLookUp.Controllers
{
    /// <summary>
    /// Custom view error controller
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class ErrorController : Controller
    {
        /// <summary>
        /// Forbiddens this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Forbidden()
        {
            return View("Forbidden");
        }

        /// <summary>
        /// Send view with not found error.
        /// </summary>
        /// <returns></returns>
        public ActionResult NotFound()
        {
            return View("NotFound");
        }

        /// <summary>
        /// Send custom view with server error.
        /// </summary>
        /// <returns></returns>
        public ActionResult ServerError()
        {
            return View("ServerError");
        }
    }
}
