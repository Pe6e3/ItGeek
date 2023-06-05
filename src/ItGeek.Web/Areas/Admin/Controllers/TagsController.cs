using ItGeek.BLL;
using ItGeek.BLL.Repositories;
using ItGeek.DAL.Entities;
using ItGeek.Web.Areas.Admin.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace ItGeek.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TagsController : Controller
    {
        private readonly UnitOfWork _uow;
        public TagsController(UnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _uow.TagRepository.ListAllAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            return View(await _uow.TagRepository.GetByIDAsync(id));
        }
        public async Task<IActionResult> Delete(int id)
        {
            Tag tags = await _uow.TagRepository.GetByIDAsync(id);
            if (tags != null)
                await _uow.TagRepository.DeleteAsync(tags);

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Create()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Tag tags)
        {
            if (ModelState.IsValid)
            {
                await _uow.TagRepository.InsertAsync(tags);
                return RedirectToAction(nameof(Index));

            }
            return View(tags);
        }

        public async Task<IActionResult> Update(int id)
        {
            Tag tags = await _uow.TagRepository.GetByIDAsync(id);
            if (tags == null)
            {
                return NotFound();
            }
            return View(tags);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Tag tags)
        {
            if (ModelState.IsValid)
            {
                await _uow.TagRepository.UpdateAsync(tags);
                return RedirectToAction(nameof(Index));

            }
            return View(tags);
        }
    }
}

