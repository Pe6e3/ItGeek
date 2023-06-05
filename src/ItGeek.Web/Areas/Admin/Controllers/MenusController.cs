using ItGeek.BLL;
using ItGeek.BLL.Repositories;
using ItGeek.DAL.Entities;
using ItGeek.Web.Areas.Admin.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace ItGeek.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MenusController : Controller
    {
        private readonly UnitOfWork _uow;
        public MenusController(UnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _uow.MenuRepository.ListAllAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            return View(await _uow.MenuRepository.GetByIDAsync(id));
        }
        public async Task<IActionResult> Delete(int id)
        {
            Menu menus = await _uow.MenuRepository.GetByIDAsync(id);
            if (menus != null)
                await _uow.MenuRepository.DeleteAsync(menus);

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Create()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Menu menus)
        {
            if (ModelState.IsValid)
            {
                await _uow.MenuRepository.InsertAsync(menus);
                return RedirectToAction(nameof(Index));

            }
            return View(menus);
        }

        public async Task<IActionResult> Update(int id)
        {
            Menu menus = await _uow.MenuRepository.GetByIDAsync(id);
            if (menus == null)
            {
                return NotFound();
            }
            return View(menus);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Menu menus)
        {
            if (ModelState.IsValid)
            {
                await _uow.MenuRepository.UpdateAsync(menus);
                return RedirectToAction(nameof(Index));

            }
            return View(menus);
        }
    }
}

 