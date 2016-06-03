using AutoMapper;
using CarLookUp.Core.Constants;
using CarLookUp.Core.Models;
using CarLookUp.Services.Interfaces;
using CarLookUp.Web.ViewModels;
using Postal;
using System;
using System.Collections.Generic;
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
            var model = _carsService.GetAllBodyTypes<BodyTypeDTO>();
            ViewBag.BodyTypes = model;
            return View();
        }

        // POST: CarsView/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([System.Web.Http.FromBody] CarVM car)
        {
            if (ModelState.IsValid)
            {
                var newCar = new CarDTO();
                newCar.Maker = car.Maker;
                newCar.Model = car.Model;
                newCar.Year = car.Year;
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
            var model = _carsService.GetCar(id);
            return View(model);
        }

        // POST: CarsView/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, [System.Web.Http.FromBody] CarVM car)
        {
            CarDTOWithBodyTypeName carDto = _carsService.GetCar(id);
            if (carDto != null)
            {
                _carsService.DeleteCar(id);

                return RedirectToAction("Index");
            }

            return View(car);
        }

        // GET: CarsView/Details/5
        public ActionResult Details(int id)
        {
            CarDTOWithBodyTypeName carDto = _carsService.GetCar(id);

            CarVMWithBodyTypeName carVm = Mapper.Map<CarVMWithBodyTypeName>(carDto);

            return View(carVm);
        }

        // GET: CarsView/Edit/5
        public ActionResult Edit(int id)
        {
            CarDTOWithBodyTypeName carDto = _carsService.GetCar(id);

            CarVM carVm = Mapper.Map<CarVM>(carDto);
            return View(carVm);
        }

        // POST: CarsView/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, [System.Web.Http.FromBody]CarVM car)
        {
            CarDTOWithBodyTypeName carDto = _carsService.GetCar(id);

            if (ModelState.IsValid && carDto != null)
            {
                CarDTO dto = Mapper.Map<CarDTO>(car);
                //carDto.Maker = car.Maker;
                //carDto.Model = car.Model;
                //carDto.Year = car.Year;
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
