using AutoMapper;
using CarLookUp.Core.Enum;
using CarLookUp.Core.Models;
using CarLookUp.Services.Interfaces;
using CarLookUp.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarLookUp.Web.Controllers
{
    /// <summary>
    /// Login controller
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class LoginController : Controller
    {
        private ILoginService _loginService;
        private IRoleService _roleService;

        public LoginController(IRoleService roleService, ILoginService loginService)
        {
            _roleService = roleService;
            _loginService = loginService;
        }

        // GET: Login
        public ActionResult Index()
        {
            GetRoles();
            return View();
        }

        /// <summary>
        /// Logins the user into session.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([System.Web.Http.FromBody]UserVM model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Login");
            }
            UserDTO dto = Mapper.Map<UserDTO>(model);

            ValidationMessageList messages = new ValidationMessageList();

            _loginService.LoginUser(dto, messages);

            if (messages.HasError)
            {
                string error = messages.GetFirstErrorMsg;
                ModelState.AddModelError(string.Empty, error);
                GetRoles();
                return View("Index", model);
            }

            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Logoffs user from session.
        /// </summary>
        /// <returns></returns>
        public ActionResult Logoff()
        {
            _loginService.Logoff();
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Gets the roles. Test method
        /// </summary>
        private void GetRoles()
        {
            ICollection<RoleDTO> dtos = _roleService.GetAll();
            ICollection<SelectListItem> list = Mapper.Map<ICollection<SelectListItem>>(dtos);
            list.Add(new SelectListItem { Value = "-1", Text = "Force Error" });
            ViewBag.Roles = list;
        }
    }
}
