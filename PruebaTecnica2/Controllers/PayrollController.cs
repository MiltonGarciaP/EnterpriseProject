using Application.Services;
using Application.ViewModels.Payroll;
using Database;
using Microsoft.AspNetCore.Mvc;

namespace PruebaTecnica2.Controllers
{
    public class PayrollController : Controller
    {
        private readonly PayrollServices _payrollServices;

        public PayrollController(ApplicationContext dbContext)
        {
            _payrollServices = new(dbContext);
        }

        public async Task<IActionResult> Index()
        {
            return View(await _payrollServices.GetAllViewModel());
        }

        public IActionResult Create()
        {
            PayrollViewModel vm = new();
            return View("SavePayroll", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PayrollViewModel vm)
        {
            await _payrollServices.Add(vm);
            return RedirectToRoute(new { controller = "Payroll", action = "Index" });
        }

        public async Task<IActionResult> Update(int id)
        {
            return View("SavePayroll", await _payrollServices.GetByIdViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> Update(PayrollViewModel vm)
        {
            await _payrollServices.Update(vm);
            return RedirectToRoute(new { controller = "Payroll", action = "Index" });
        }

        public async Task<IActionResult> Remove(int id)
        {
            await _payrollServices.GetByIdViewModel(id);
            await _payrollServices.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
