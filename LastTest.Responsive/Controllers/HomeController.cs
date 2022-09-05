using LastTest.Application.Contracts.Data;
using LastTest.Application.Handlers;
using LastTest.DataAccess.Repository;
using LastTest.DataAccess.Repository.UnitOfWork;
using LastTest.Domain.Models.DataModels;
using LastTest.Domain.Models.ViewModels;
using LastTest.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Threading.Tasks;

namespace LastTest.Responsive.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        public HomeController(IUnitOfWork<ApplicationDbContext> unitOfWork, IApplicationDbContext context)
        {
            _unitOfWork = unitOfWork;
            _carRepository = _unitOfWork.Repository<Car>();
            _statusRepository = _unitOfWork.Repository<ReservationStatus>();
            _context = context;
        }

        readonly IApplicationDbContext _context;
        readonly IRepository<Car> _carRepository;
        readonly IUnitOfWork<ApplicationDbContext> _unitOfWork;
        readonly IRepository<ReservationStatus> _statusRepository;

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var cars = await _context.Cars.Include(s => s.Status).ToListAsync();

            return View(cars);
        }

        [HttpPost]
        public IActionResult Index(int placa)
        {
            var carSelected = _carRepository.GetFirstOrDefault(x => x.Placa == placa);

            //change status
            carSelected.Status =  _statusRepository.GetFirstOrDefault(x => x.Id == 2);
            _unitOfWork.Save();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Accepts(int placa)
        {
            var carSelected = _carRepository.GetFirstOrDefault(x => x.Placa == placa);

            //change status
            carSelected.Status = _statusRepository.GetFirstOrDefault(x => x.Id == 3);
            _unitOfWork.Save();

            return RedirectToAction("Index");

        }

        [HttpPost]
        public IActionResult Rejects(int placa)
        {
            var carSelected = _carRepository.GetFirstOrDefault(x => x.Placa == placa);

            //change status
            carSelected.Status = _statusRepository.GetFirstOrDefault(x => x.Id == 1);
            _unitOfWork.Save();

            return RedirectToAction("Index");

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
