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

namespace CarLookUp.Controllers
{
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
            ICollection<RoleDTO> dtos = _roleService.GetAll();
            ICollection<SelectListItem> vms = Mapper.Map<ICollection<SelectListItem>>(dtos);
            ViewBag.Roles = vms;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([System.Web.Http.FromBody]UserVM model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Login");
            }
            UserDTO dto = Mapper.Map<UserDTO>(model);

            ValidationMassageList messages = new ValidationMassageList();

            _loginService.LoginUser(dto, messages);

            if (messages.HasError)
            {
                string error = messages.Where(m => m.Type == MessageTypes.Error).Select(m => m.Text).FirstOrDefault();
                ModelState.AddModelError(string.Empty, error);
                GetRoles();
                return View("Index", model);
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logoff()
        {
            _loginService.Logoff();
            return RedirectToAction("Index", "Login");
        }

        private void GetRoles()
        {
            ICollection<RoleDTO> dtos = _roleService.GetAll();
            ICollection<SelectListItem> list = Mapper.Map<ICollection<SelectListItem>>(dtos);
            list.Add(new SelectListItem { Value = "-1", Text = "Force Error" });
            ViewBag.Roles = list;
        }
    }
}
