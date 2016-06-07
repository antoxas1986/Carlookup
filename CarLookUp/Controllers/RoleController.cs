using AutoMapper;
using CarLookUp.Core.Constants;
using CarLookUp.Core.Models;
using CarLookUp.Services.Interfaces;
using CarLookUp.Web.Filters;
using CarLookUp.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace CarLookUp.Controllers
{
    [MvcAuthorization]
    public class RoleController : Controller
    {
        private IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [MvcAuthorization(Roles = Roles.ADMIN)]
        public ActionResult Details(int id)
        {
            RoleDTO dto = _roleService.GetById(id);
            RoleVM vm = Mapper.Map<RoleVM>(dto);

            if (vm == null)
            {
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        // GET: Role
        public ActionResult Index()
        {
            ICollection<RoleDTO> dtos = _roleService.GetAll();
            ICollection<RoleVM> vms = Mapper.Map<ICollection<RoleVM>>(dtos);
            //throw new Exception("Test");
            return View(vms);
        }
    }
}
