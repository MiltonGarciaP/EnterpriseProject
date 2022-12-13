using Application.Services;
using Application.ViewModels.Employee;
using Database;
using Microsoft.AspNetCore.Mvc;

namespace PruebaTecnica2.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeServices _employeeServices;
        private readonly PayrollServices _payrollServices;
        private readonly VacantionServices _vacantionServices;
        public EmployeeController(ApplicationContext dbContext)
        {
            _employeeServices = new(dbContext);
            _payrollServices = new(dbContext);
            _vacantionServices = new(dbContext);
        }

        public async Task<IActionResult> Index() 
        {
            return View(await _employeeServices.GetAllViewModel());
        }

        public async Task<IActionResult> Create()
        {
            EmployeeViewModel vm = new();
            vm.Payrolls = await _payrollServices.GetAllViewModel();
            return View("SaveEmployee", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeViewModel vm)
        {
            await _employeeServices.Add(vm);
            return RedirectToRoute(new { controller = "Employee", action = "Index" });
        }


        public async Task<IActionResult> Update(int id)
        {
            EmployeeViewModel vm = await _employeeServices.GetByIdViewModel(id);
            vm.Payrolls = await _payrollServices.GetAllViewModel();
            return View("SaveEmployee", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(EmployeeViewModel vm)
        {
            await _employeeServices.Update(vm);
            return RedirectToRoute(new { controller = "Employee", action = "Index" });
        }

        public async Task<IActionResult> Remove(int id)
        {
            await _employeeServices.GetByIdViewModel(id);
            await _employeeServices.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
