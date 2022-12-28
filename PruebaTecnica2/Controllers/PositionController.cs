using Application.Services;
using Application.ViewModels.Position;
using Database;
using Microsoft.AspNetCore.Mvc;

namespace PruebaTecnica2.Controllers
{
    public class PositionController : Controller
    {
        private readonly PositionServices _positionServices;
        public PositionController(ApplicationContext dbContext)
        {
            _positionServices = new(dbContext);
        }

        public async Task<IActionResult> Index()
        {
            return View(await _positionServices.GetAllViewModel());
        }

        public async Task<IActionResult> Create()
        {
            PositionViewModel pvm = new();
            return View("SavePosition", pvm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PositionViewModel pvm)
        {
            await _positionServices.Add(pvm);
            return RedirectToRoute(new { controller = "Position", action = "Index" });
        }
    }
}
