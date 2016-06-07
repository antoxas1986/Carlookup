using AutoMapper;
using CarLookUp.Core.Constants;
using CarLookUp.Core.Enum;
using CarLookUp.Core.Models;
using CarLookUp.Services.Interfaces;
using CarLookUp.Web.ViewModels;
using Postal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace CarLookUp.Controllers
{
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
        public ActionResult Create()
        {
            ViewBag.BodyTypes = new SelectList(_carsService.GetAllBodyTypes<BodyTypeDTO>(), "Id", "TypeOfBody");
            return View();
        }

        // POST: CarsView/Create
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
        public ActionResult Details(int id)
        {
            ValidationMassageList messages = new ValidationMassageList();

            CarDTOWithBodyType carDto = _carsService.GetCar(id, messages);

            if (messages.HasError)
            {
                var error = messages.Where(m => m.Type == MessageTypes.Error).Select(m => m.Text).FirstOrDefault();
                ModelState.AddModelError(string.Empty, error);
                return View("Index");
            }

            CarVMWithBodyTypeName carVm = Mapper.Map<CarVMWithBodyTypeName>(carDto);

            return View(carVm);
        }

        // GET: CarsView/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.BodyTypes = new SelectList(_carsService.GetAllBodyTypes<BodyTypeDTO>(), "Id", "TypeOfBody");

            ValidationMassageList messages = new ValidationMassageList();
            CarDTOWithBodyType carDto = _carsService.GetCar(id, messages);

            if (messages.HasError)
            {
                var error = messages.Where(m => m.Type == MessageTypes.Error).Select(m => m.Text).FirstOrDefault();
                ModelState.AddModelError(string.Empty, error);
                return View("Index");
            }

            CarVMWithBodyTypeName carVm = Mapper.Map<CarVMWithBodyTypeName>(carDto);
            return View(carVm);
        }

        // POST: CarsView/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, [System.Web.Http.FromBody]CarVMWithBodyTypeName car)
        {
            ValidationMassageList messages = new ValidationMassageList();

            CarDTOWithBodyType carDto = _carsService.GetCar(id, messages);

            if (messages.HasError)
            {
                var error = messages.Where(m => m.Type == MessageTypes.Error).Select(m => m.Text).FirstOrDefault();
                ModelState.AddModelError(string.Empty, error);
                return View(car);
            }

            if (ModelState.IsValid)
            {
                carDto = Mapper.Map<CarDTOWithBodyType>(car);
                _carsService.Edit(carDto);

                return RedirectToAction("Index");
            }
            return View(car);
        }

        // GET: CarsView
        public ActionResult Index()
        {
            ICollection<CarDTO> dtos = _carsService.GetAll<CarDTO>();

            ICollection<CarVM> vms = Mapper.Map<ICollection<CarVM>>(dtos);

            return View(vms);
        }
    }
}
