using ItGeek.BLL;
using ItGeek.BLL.Repositories;
using ItGeek.DAL.Entities;
using ItGeek.Web.Areas.Admin.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace ItGeek.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthorsController : Controller
    {
        private readonly UnitOfWork _uow;
        public AuthorsController(UnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _uow.AuthorRepository.ListAllAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            return View(await _uow.AuthorRepository.GetByIDAsync(id));
        }
        public async Task<IActionResult> Delete(int id)
        {
            Author authors = await _uow.AuthorRepository.GetByIDAsync(id);
            if (authors != null)
                await _uow.AuthorRepository.DeleteAsync(authors);

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Create()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Author authors)
        {
            if (ModelState.IsValid)
            {
                await _uow.AuthorRepository.InsertAsync(authors);
                return RedirectToAction(nameof(Index));

            }
            return View(authors);
        }

        public async Task<IActionResult> Update(int id)
        {
            Author authors = await _uow.AuthorRepository.GetByIDAsync(id);
            if (authors == null)
            {
                return NotFound();
            }
            return View(authors);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Author authors)
        {
            if (ModelState.IsValid)
            {
                await _uow.AuthorRepository.UpdateAsync(authors);
                return RedirectToAction(nameof(Index));

            }
            return View(authors);
        }
    }
}

 