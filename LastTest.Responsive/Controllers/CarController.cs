using LastTest.Application.Handlers;
using LastTest.DataAccess.Repository;
using LastTest.DataAccess.Repository.UnitOfWork;
using LastTest.Domain.Models.DataModels;
using LastTest.Domain.Models.InputModels;
using LastTest.Domain.Models.ViewModels;
using LastTest.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace LastTest.Responsive.Controllers
{
    public class CarController : Controller
    {
        
        public CarController(IUnitOfWork<ApplicationDbContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _carRepository = _unitOfWork.Repository<Car>();
            _statusRepository = _unitOfWork.Repository<ReservationStatus>();

        }

        readonly IUnitOfWork<ApplicationDbContext> _unitOfWork;
        readonly IRepository<Car> _carRepository;
        readonly IRepository<ReservationStatus> _statusRepository;

       // [Authorize(Policy = PermissionTypeNames.ADMINISTRADOR)]
        public IActionResult Index()
        {
            List<Car> cars = (List<Car>) _carRepository.GetAll();
            return View(cars);
        }
       // [Authorize(Policy = PermissionTypeNames.ADMINISTRADOR)]
        public IActionResult Upsert(int? id)
        {
            CarViewModel model =
                new CarViewModel
                {
                    Car = new()
                };

            if (id == null || id == 0)
            {
                //insert car
                return View(model);
            }
            else
            {
                //update car
                model.Car = _carRepository.GetFirstOrDefault(x => x.Placa == id);
                return View(model);
            }
        }

        [HttpPost]
       // [Authorize(Policy = PermissionTypeNames.ADMINISTRADOR)]
        public IActionResult Upsert([FromForm]CarViewModel model)
        {
            if (ModelState.IsValid)
            {
                var noReserved = _statusRepository.GetFirstOrDefault(x => x.Id == 1);


                if (model.Car.Placa == 0)
                {
                    model.Car.Status = noReserved;
                    _carRepository.Add(model.Car);
                }
                else
                {
                    _carRepository.Update(model.Car);
                }
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(model);
        }

    }
}
