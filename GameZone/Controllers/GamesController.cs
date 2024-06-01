using GameZone.Models;
using GameZone.Services;
using GameZone.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GameZone.Controllers
{
    public class GamesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICategoryService _categoryService;
        private readonly IDevicesServices _devicesServices;
        private readonly IGamesServices _gamesServices;


        //******************************************************************
        public GamesController(
            ApplicationDbContext context,
            ICategoryService categoryService,
            IDevicesServices devicesServices,
            IGamesServices gamesServices

            )
        {
            _context = context;
            _categoryService = categoryService;
            _devicesServices = devicesServices;
            _gamesServices = gamesServices;
        }
        //******************************************************************

        public IActionResult Index()
        {
            var game = _gamesServices.GetAll();
            return View(game);
        }
        //******************************************************************

        public IActionResult Details(int Id)
        {
            var game = _gamesServices.GetById(Id);
            if (game is null)
                return NotFound();
            return View(game);
        }

        //******************************************************************
        [HttpGet]
        public IActionResult Create()
        {
            GameCreateViewModel viewModel = new()
            {
                Categories = _categoryService.GetSelectedCategories(),
                Devices = _devicesServices.GetSelectedDevices()
            };

            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GameCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = _categoryService.GetSelectedCategories();
                model.Devices = _devicesServices.GetSelectedDevices();
                return View(model);
            }
            await _gamesServices.Create(model);
            return RedirectToAction(nameof(Index));
        }
        //******************************************************************

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var game = _gamesServices.GetById(Id);
            if (game is null)
                return NotFound();


            GameEditViewModel EditModel = new()
            {
                Id = Id,
                Name = game.Name!,
                Description = game.Description!,
                CategoryID = game.CategoryId,
                SelectedDevices = game.Devices.Select(d => d.DeviceId).ToList(),
                Categories = _categoryService.GetSelectedCategories(),
                Devices = _devicesServices.GetSelectedDevices(),
                DisplayImgString = game.Cover,

            };
            return View(EditModel);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(GameEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = _categoryService.GetSelectedCategories();
                model.Devices = _devicesServices.GetSelectedDevices();
                return View(model);
            }
            var game = await _gamesServices.Edit(model);

            if (game is null)
                return BadRequest();

            return RedirectToAction(nameof(Index));

        }
        //******************************************************************
        [HttpDelete]
        public IActionResult Delete(int Id)
        {
            bool isDeleted = _gamesServices.Delete(Id);

            return isDeleted ? Ok() : BadRequest();
        }
    }
}
