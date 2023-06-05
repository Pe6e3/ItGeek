using ItGeek.BLL;
using ItGeek.BLL.Repositories;
using ItGeek.DAL.Entities;
using ItGeek.Web.Areas.Admin.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace ItGeek.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RolesController : Controller
    {

        private readonly UnitOfWork _uow;
        public RolesController(UnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _uow.RoleRepository.ListAllAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            return View(await _uow.RoleRepository.GetByIDAsync(id));
        }
        public async Task<IActionResult> Delete(int id)
        {
            Role roles = await _uow.RoleRepository.GetByIDAsync(id);
            if (roles != null)
                await _uow.RoleRepository.DeleteAsync(roles);

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Create()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Role roles)
        {
            if (ModelState.IsValid)
            {
                await _uow.RoleRepository.InsertAsync(roles);
                return RedirectToAction(nameof(Index));

            }
            return View(roles);
        }

        public async Task<IActionResult> Update(int id)
        {
            Role roles = await _uow.RoleRepository.GetByIDAsync(id);
            if (roles == null)
            {
                return NotFound();
            }
            return View(roles);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Role roles)
        {
            if (ModelState.IsValid)
            {
                await _uow.RoleRepository.UpdateAsync(roles);
                return RedirectToAction(nameof(Index));

            }
            return View(roles);
        }
    }
}

 