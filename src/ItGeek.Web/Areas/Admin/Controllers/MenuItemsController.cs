using ItGeek.BLL;
using ItGeek.BLL.Repositories;
using ItGeek.DAL.Entities;
using ItGeek.Web.Areas.Admin.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace ItGeek.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MenuItemsController : Controller
    {

        private readonly UnitOfWork _uow;
        public MenuItemsController(UnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _uow.MenuItemRepository.ListAllAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            return View(await _uow.MenuItemRepository.GetByIDAsync(id));
        }
        public async Task<IActionResult> Delete(int id)
        {
            MenuItem menuItems = await _uow.MenuItemRepository.GetByIDAsync(id);
            if (menuItems != null)
                await _uow.MenuItemRepository.DeleteAsync(menuItems);

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Create()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(MenuItem menuItems)
        {
            if (ModelState.IsValid)
            {
                await _uow.MenuItemRepository.InsertAsync(menuItems);
                return RedirectToAction(nameof(Index));

            }
            return View(menuItems);
        }

        public async Task<IActionResult> Update(int id)
        {
            MenuItem menuItems = await _uow.MenuItemRepository.GetByIDAsync(id);
            if (menuItems == null)
            {
                return NotFound();
            }
            return View(menuItems);
        }

        [HttpPost]
        public async Task<IActionResult> Update(MenuItem menuItems)
        {
            if (ModelState.IsValid)
            {
                await _uow.MenuItemRepository.UpdateAsync(menuItems);
                return RedirectToAction(nameof(Index));

            }
            return View(menuItems);
        }
    }
}

 