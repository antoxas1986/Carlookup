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
    /// <summary>
    /// Controller to work with roles
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    [MvcAuthorization]
    public class RoleController : Controller
    {
        private IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        /// <summary>
        /// Details for role by specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
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
        /// <summary>
        /// Index
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ICollection<RoleDTO> dtos = _roleService.GetAll();
            ICollection<RoleVM> vms = Mapper.Map<ICollection<RoleVM>>(dtos);
            //throw new Exception("Test");
            return View(vms);
        }
    }
}
