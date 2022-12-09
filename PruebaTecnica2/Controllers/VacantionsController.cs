using Application.Services;
using Application.ViewModels.Vacations;
using Database;
using Microsoft.AspNetCore.Mvc;

namespace PruebaTecnica2.Controllers
{
    public class VacantionsController : Controller
    {
        private readonly VacantionServices _vacantionServices;
        private readonly EmployeeServices _employeeServices;
        public VacantionsController(ApplicationContext dbContext)
        {
            _vacantionServices = new(dbContext);
            _employeeServices = new(dbContext);
        }

        public async Task<IActionResult> Index()
        {
            return View(await _vacantionServices.GetAllViewModel());
        }

        public async Task<IActionResult> Create()
        {
            VacantionViewModel vm = new();
            vm.EmployeeViewModels = await _employeeServices.GetAllViewModel();
            return View("SaveVacation", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(VacantionViewModel vm)
        {
            await _vacantionServices.Add(vm);
            return RedirectToRoute(new { controller = "Vacantions", action = "Index" });
        }

        public async Task<IActionResult> Update(int id)
        {
            VacantionViewModel vm = await _vacantionServices.GetByIdViewModel(id);
            vm.EmployeeViewModels = await _employeeServices.GetAllViewModel();
            return View("SaveVacation", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(VacantionViewModel vm)
        {
            await _vacantionServices.Update(vm);
            return RedirectToRoute(new { controller = "Vacantions", action = "Index" });
        }

        public async Task<IActionResult> Remove(int id)
        {
            await _vacantionServices.GetByIdViewModel(id);
            await _vacantionServices.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
