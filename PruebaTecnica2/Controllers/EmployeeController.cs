using Application.Services;
using Application.ViewModels.Employee;
using Database;
using Microsoft.AspNetCore.Mvc;

namespace PruebaTecnica2.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeServices _employeeServices;
        public EmployeeController(ApplicationContext dbContext)
        {
            _employeeServices = new(dbContext);
        }

        public async Task<IActionResult> Index() 
        {
            return View(await _employeeServices.GetAllViewModel());
        }

        public IActionResult Create()
        {
            EmployeeViewModel vm = new();
            return View("SaveClient", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeViewModel vm)
        {
            await _employeeServices.Add(vm);
            return RedirectToRoute(new { controller = "Client", action = "Index" });
        }


        public async Task<IActionResult> Update(int id)
        {
            return View("SaveClient", await _employeeServices.GetByIdViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> Update(EmployeeViewModel vm)
        {
            await _employeeServices.Update(vm);
            return RedirectToRoute(new { controller = "Client", action = "Index" });
        }

        public async Task<IActionResult> Remove(int id)
        {
            await _employeeServices.GetByIdViewModel(id);
            await _employeeServices.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
