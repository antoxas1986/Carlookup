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

        // GET: CarsView/Create
        /// <summary>
        /// Creates new car view.
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            ViewBag.BodyTypes = new SelectList(_carsService.GetAllBodyTypes<BodyTypeDTO>(), "Id", "TypeOfBody");
            return View();
        }

        // POST: CarsView/Create
        /// <summary>
        /// Creates the specified car and sends email about it.
        /// </summary>
        /// <param name="car">The car.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([System.Web.Http.FromBody] CarVMWithBodyTypeName car)
        {
            if (ModelState.IsValid)
            {
                CarDTOWithBodyType newCar = Mapper.Map<CarDTOWithBodyType>(car);

                _carsService.AddCar(newCar);

                DetailsEmailVM email = new DetailsEmailVM(EmailSettings.DETAILS_EMAIL)
                {
                    Subject = "subject",
                    ToAddress = "test@c.c",
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
            return View();
        }

        // GET: CarsView/Delete/5
        /// <summary>
        /// Delete view for car by specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [MvcAuthorization(Roles = Roles.ADMIN)]
        public ActionResult Delete(int id)
        {
            ValidationMassageList messages = new ValidationMassageList();
            var model = _carsService.GetCar(id, messages);

            if (messages.HasError)
            {
                var error = messages.Where(m => m.Type == MessageTypes.Error).Select(m => m.Text).FirstOrDefault();

                ModelState.AddModelError(string.Empty, error);

                return View();
            }

            CarVM carVm = Mapper.Map<CarVM>(model);
            return View(carVm);
        }

        // POST: CarsView/Delete/5
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
            ValidationMassageList messages = new ValidationMassageList();

            CarDTOWithBodyType carDto = _carsService.GetCar(id, messages);

            if (messages.HasError)
            {
                var error = messages.Where(m => m.Type == MessageTypes.Error).Select(m => m.Text).FirstOrDefault();

                ModelState.AddModelError(string.Empty, error);

                return View(car);
            }

            _carsService.DeleteCar(id);

            return RedirectToAction("Index");
        }

        // GET: CarsView/Details/5
        /// <summary>
        /// Details for car by specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public ActionResult Details(int id)
        {
            ValidationMassageList messages = new ValidationMassageList();

            CarDTOWithBodyType carDto = _carsService.GetCar(id, messages);

            if (messages.HasError)
            {
                var error = messages.Where(m => m.Type == MessageTypes.Error).Select(m => m.Text).FirstOrDefault();
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
            ICollection<SelectListItem> list = Mapper.Map<ICollection<SelectListItem>>(_carsService.GetAllBodyTypes<BodyTypeDTO>());

            //adding to call error condition on non-exist bodytype.
            list.Add(new SelectListItem { Value = "999", Text = "Force Error" });

            ViewBag.BodyTypes = list;
            ValidationMassageList messages = new ValidationMassageList();
            CarDTOWithBodyType carDto = _carsService.GetCar(id, messages);

            if (messages.HasError)
            {
                var error = messages.Where(m => m.Type == MessageTypes.Error).Select(m => m.Text).FirstOrDefault();
                ViewBag.error = error;
                return HttpNotFound();
            }

            CarVMWithBodyTypeName carVm = Mapper.Map<CarVMWithBodyTypeName>(carDto);
            return View(carVm);
        }

        // POST: CarsView/Edit/5
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
                ValidationMassageList messages = new ValidationMassageList();
                CarDTOWithBodyType carDto = _carsService.GetCar(id, messages);

                if (messages.HasError)
                {
                    var error = messages.Where(m => m.Type == MessageTypes.Error).Select(m => m.Text).FirstOrDefault();
                    ModelState.AddModelError(string.Empty, error);
                    return View(car);
                }
                carDto = Mapper.Map<CarDTOWithBodyType>(car);
                messages.Clear();
                _carsService.Edit(carDto, messages);
                if (messages.HasError)
                {
                    var error = messages.Where(m => m.Type == MessageTypes.Error).Select(m => m.Text).FirstOrDefault();
                    ViewBag.error = error;
                    return RedirectToAction("ServerError", "Error");
                }
                return RedirectToAction("Index");
            }
            return View(car);
        }

        // GET: CarsView
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
    }
}
