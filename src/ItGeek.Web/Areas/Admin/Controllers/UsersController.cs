using ItGeek.BLL;
using ItGeek.BLL.Repositories;
using ItGeek.DAL.Entities;
using ItGeek.Web.Areas.Admin.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace ItGeek.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {

        private readonly UnitOfWork _uowUser;
        public UsersController(UnitOfWork uow)
        {
            _uowUser = uow;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _uowUser.UserRepository.ListAllAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            return View(await _uowUser.UserRepository.GetByIDAsync(id));
        }
        public async Task<IActionResult> Delete(int id)
        {
            User users = await _uowUser.UserRepository.GetByIDAsync(id);
            if (users != null)
                await _uowUser.UserRepository.DeleteAsync(users);

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Create()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(User users)
        {
            if (ModelState.IsValid)
            {
                await _uowUser.UserRepository.InsertAsync(users);
                return RedirectToAction(nameof(Index));

            }
            return View(users);
        }

        public async Task<IActionResult> Update(int id)
        {
            User users = await _uowUser.UserRepository.GetByIDAsync(id);
            if (users == null)
            {
                return NotFound();
            }
            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> Update(User users)
        {
            if (ModelState.IsValid)
            {
                await _uowUser.UserRepository.UpdateAsync(users);
                return RedirectToAction(nameof(Index));

            }
            return View(users);
        }
    }
}

 