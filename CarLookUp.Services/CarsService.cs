using CarLookUp.Core.Constants;
using CarLookUp.Core.Enum;
using CarLookUp.Core.Models;
using CarLookUp.Core.Utilities;
using CarLookUp.Data.Repository.Interfaces;
using CarLookUp.Services.Interfaces;
using System.Collections.Generic;

namespace CarLookUp.Services
{
    /// <summary>
    /// Service to interact with cars
    /// </summary>
    /// <seealso cref="CarLookUp.Services.CarServices.ICarsService" />
    public class CarsService : ICarsService
    {
        private IBodyTypeRepository _bodyTypeRepo;
        private ICarRepository _carsRepo;
        private IUnitOfWork _unit;

        public CarsService(ICarRepository carsRepo, IBodyTypeRepository bodyTypeRepo, IUnitOfWork unit)
        {
            _carsRepo = carsRepo;
            _bodyTypeRepo = bodyTypeRepo;
            _unit = unit;
        }

        /// <summary>
        /// Adds the car.
        /// </summary>
        /// <param name="car">The car.</param>
        public void AddCar(CarDTOWithBodyType car, ValidationMessageList messages)
        {
            if (IsBodyTypeValid(car, messages))
            {
                _carsRepo.AddCar(car);
                _unit.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes the car by id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void DeleteCar(int id)
        {
            CarDTO car = _carsRepo.GetCar<CarDTO>(id);
            if (car != null)
            {
                _carsRepo.DeleteCar(id);
                _unit.SaveChanges();
            }
        }

        /// <summary>
        /// Edits the specified car dto.
        /// </summary>
        /// <param name="carDto">The car dto.</param>
        public void Edit(int id, CarDTOWithBodyType carDto, ValidationMessageList messages)
        {
            if (id != carDto.Id)
            {
                messages.Add(new ValidationMessage(MessageTypes.Error, ErrorMessages.ID_NOT_MATCH));
                return;
            }

            if (IsBodyTypeValid(carDto, messages))
            {
                _carsRepo.Edit(carDto, messages);
                if (!messages.HasError)
                {
                    _unit.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Gets all cars.
        /// </summary>
        /// <returns></returns>
        public ICollection<T> GetAll<T>()
        {
            return _carsRepo.GetAll<T>();
        }

        /// <summary>
        /// Gets all body types.
        /// </summary>
        /// <typeparam name="BodyTypeDTO">The type of the body type dto.</typeparam>
        /// <returns></returns>
        public ICollection<BodyTypeDTO> GetAllBodyTypes()
        {
            ICollection<BodyTypeDTO> list = (ICollection<BodyTypeDTO>)GlobalCachingProvider.Instance.GetItem(CacheKeys.BODYTYPES);
            if (list == null)
            {
                list = _bodyTypeRepo.GetAll();
                GlobalCachingProvider.Instance.AddItem(CacheKeys.BODYTYPES, list);
            }
            return list;
        }

        /// <summary>
        /// Gets the body type by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public BodyTypeDTO GetBodyTypeById(int id)
        {
            BodyTypeDTO bodyType = (BodyTypeDTO)GlobalCachingProvider.Instance.GetItem(CacheKeys.BODYTYPES + id);
            if (bodyType == null)
            {
                bodyType = _bodyTypeRepo.GetById(id);
                if (bodyType != null)
                {
                    GlobalCachingProvider.Instance.AddItem(CacheKeys.BODYTYPES + id, bodyType);
                }
            }
            return bodyType;
        }

        /// <summary>
        /// Gets the car by id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>CarDTOWithBodyType</returns>
        public CarDTOWithBodyType GetCar(int id, ValidationMessageList messages)
        {
            var car = _carsRepo.GetCar<CarDTOWithBodyType>(id);

            if (car == null)
            {
                messages.Add(new ValidationMessage(MessageTypes.Error, ErrorMessages.NO_CAR));
            }

            return car;
        }

        private bool IsBodyTypeValid(CarDTOWithBodyType car, ValidationMessageList messages)
        {
            BodyTypeDTO bodyTypeDto = _bodyTypeRepo.GetById(car.BodyTypeId);
            if (bodyTypeDto == null)
            {
                messages.Add(new ValidationMessage(MessageTypes.Error, ErrorMessages.NO_BODYTYPE));
                return false;
            }
            return true;
        }
    }
}
