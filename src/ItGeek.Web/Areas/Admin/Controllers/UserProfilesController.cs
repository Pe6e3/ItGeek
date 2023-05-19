using ItGeek.BLL;
using ItGeek.BLL.Repositories;
using ItGeek.DAL.Entities;
using ItGeek.Web.Areas.Admin.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace ItGeek.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserProfilesController : Controller
    {

        private readonly UnitOfWork _uow;
        public UserProfilesController(UnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _uow.UserProfileRepository.ListAllAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            return View(await _uow.UserProfileRepository.GetByIDAsync(id));
        }
        public async Task<IActionResult> Delete(int id)
        {
            UserProfile userProfiles = await _uow.UserProfileRepository.GetByIDAsync(id);
            if (userProfiles != null)
                await _uow.UserProfileRepository.DeleteAsync(userProfiles);

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Create()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserProfile userProfiles)
        {
            if (ModelState.IsValid)
            {
                await _uow.UserProfileRepository.InsertAsync(userProfiles);
                return RedirectToAction(nameof(Index));

            }
            return View(userProfiles);
        }

        public async Task<IActionResult> Update(int id)
        {
            UserProfile userProfiles = await _uow.UserProfileRepository.GetByIDAsync(id);
            if (userProfiles == null)
            {
                return NotFound();
            }
            return View(userProfiles);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UserProfile userProfiles)
        {
            if (ModelState.IsValid)
            {
                await _uow.UserProfileRepository.UpdateAsync(userProfiles);
                return RedirectToAction(nameof(Index));

            }
            return View(userProfiles);
        }
    }
}

 