using AutoMapper;
using CarLookUp.Core.Constants;
using CarLookUp.Core.Enum;
using CarLookUp.Core.Models;
using CarLookUp.Services.Interfaces;
using CarLookUp.Web.Filters;
using CarLookUp.Web.ViewModels;
using Postal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace CarLookUp.Controllers
{
    /// <summary>
    /// Controller for cars
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    [MvcAuthorization]
    public class CarsController : Controller
    {
        private ICarsService _carsService;
        private IEmailService _emailService;

        public CarsController(ICarsService carsService, IEmailService emailService)
        {
            _carsService = carsService;
            _emailService = emailService;
        }

        /// <summary>
        /// Creates new car view.
        /// </summary>
        /// <returns></returns>
        [MvcAuthorization(Roles = Roles.ADMIN)]
        public ActionResult Create()
        {
            GetBodyTypes();
            return View();
        }

        /// <summary>
        /// Creates the specified car and sends email about it.
        /// </summary>
        /// <param name="car">The car.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [MvcAuthorization(Roles = Roles.ADMIN)]
        public ActionResult Create([System.Web.Http.FromBody] CarVMWithBodyTypeName car)
        {
            if (ModelState.IsValid)
            {
                CarDTOWithBodyType newCar = Mapper.Map<CarDTOWithBodyType>(car);
                ValidationMessageList messages = new ValidationMessageList();

                _carsService.AddCar(newCar, messages);

                DetailsEmailVM email = new DetailsEmailVM(EmailSettings.DETAILS_EMAIL)
                {
                    Subject = "New car created.",
                    ToAddress = "test@gmail.com",
                    Maker = newCar.Maker,
                    Model = newCar.Model,
                    Year = newCar.Year
                };

                try
                {
                    _emailService.Send(email);
                }
                catch (Exception e)
                {
                }

                return RedirectToAction("Index");
            }
            GetBodyTypes();
            return View(car);
        }

        /// <summary>
        /// Delete view for car by specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [MvcAuthorization(Roles = Roles.ADMIN)]
        public ActionResult Delete(int id)
        {
            ValidationMessageList messages = new ValidationMessageList();
            var model = _carsService.GetCar(id, messages);

            if (messages.HasError)
            {
                var error = messages.GetFirstErrorMsg;

                ModelState.AddModelError(string.Empty, error);

                return View();
            }

            CarVM carVm = Mapper.Map<CarVM>(model);
            return View(carVm);
        }

        /// <summary>
        /// Deletes the car by specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="car">The car.</param>
        /// <returns></returns>
        [MvcAuthorization(Roles = Roles.ADMIN)]
        [HttpPost]
        public ActionResult Delete(int id, [System.Web.Http.FromBody] CarVM car)
        {
            ValidationMessageList messages = new ValidationMessageList();

            CarDTOWithBodyType carDto = _carsService.GetCar(id, messages);

            if (messages.HasError)
            {
                var error = messages.GetFirstErrorMsg;

                ModelState.AddModelError(string.Empty, error);

                return View(car);
            }

            _carsService.DeleteCar(id);

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Details for car by specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public ActionResult Details(int id)
        {
            ValidationMessageList messages = new ValidationMessageList();

            CarDTOWithBodyType carDto = _carsService.GetCar(id, messages);

            if (messages.HasError)
            {
                var error = messages.GetFirstErrorMsg;
                ViewBag.error = error;
                return HttpNotFound();
            }

            CarVMWithBodyTypeName carVm = Mapper.Map<CarVMWithBodyTypeName>(carDto);

            return View(carVm);
        }

        /// <summary>
        /// Edit view for car by specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [MvcAuthorization(Roles = Roles.ADMIN)]
        public ActionResult Edit(int id)
        {
            ICollection<SelectListItem> list = Mapper.Map<ICollection<SelectListItem>>(_carsService.GetAllBodyTypes());

            //adding to call error condition on non-exist bodytype.
            list.Add(new SelectListItem { Value = "999", Text = "Force Error" });

            ViewBag.BodyTypes = list;
            ValidationMessageList messages = new ValidationMessageList();
            CarDTOWithBodyType carDto = _carsService.GetCar(id, messages);

            if (messages.HasError)
            {
                var error = messages.GetFirstErrorMsg;
                ViewBag.error = error;
                return HttpNotFound();
            }

            CarVMWithBodyTypeName carVm = Mapper.Map<CarVMWithBodyTypeName>(carDto);
            return View(carVm);
        }

        /// <summary>
        /// Edits the car by specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="car">The car.</param>
        /// <returns></returns>
        [MvcAuthorization(Roles = Roles.ADMIN)]
        [HttpPost]
        public ActionResult Edit(int id, [System.Web.Http.FromBody]CarVMWithBodyTypeName car)
        {
            if (ModelState.IsValid)
            {
                ValidationMessageList messages = new ValidationMessageList();
                CarDTOWithBodyType carDto = Mapper.Map<CarDTOWithBodyType>(car);
                _carsService.Edit(id, carDto, messages);

                if (messages.HasError)
                {
                    var error = messages.GetFirstErrorMsg;
                    ModelState.AddModelError(string.Empty, error);
                    GetBodyTypes();
                    return View(car);
                }
                return RedirectToAction("Index");
            }
            GetBodyTypes();
            return View(car);
        }

        /// <summary>
        /// Index view for car collection.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ICollection<CarDTO> dtos = _carsService.GetAll<CarDTO>();

            ICollection<CarVM> vms = Mapper.Map<ICollection<CarVM>>(dtos);

            return View(vms);
        }

        private void GetBodyTypes()
        {
            ICollection<SelectListItem> list = Mapper.Map<ICollection<SelectListItem>>(_carsService.GetAllBodyTypes());
            //adding to call error condition on non-exist bodytype.
            list.Add(new SelectListItem { Value = "999", Text = "Force Error" });
            ViewBag.BodyTypes = list;
        }
    }
}
