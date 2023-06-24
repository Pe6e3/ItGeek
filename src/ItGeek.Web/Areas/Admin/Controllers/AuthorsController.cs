using ItGeek.BLL;
using ItGeek.BLL.Repositories;
using ItGeek.DAL.Entities;
using ItGeek.Web.Areas.Admin.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace ItGeek.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthorsController : Controller
    {
        private readonly UnitOfWork _uow;
        private readonly IWebHostEnvironment _hostEnvironment;
        public AuthorsController(UnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _uow = unitOfWork;
            _hostEnvironment = hostEnvironment;
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
            ViewBag.isCreate = true;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Author author)
        {
            author.AuthorImage = "";
            if (ModelState.IsValid)
            {
                author.AuthorImage = await ProcessUploadFile(author);
                await _uow.AuthorRepository.InsertAsync(author);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.isCreate = true;
            return View(author);
        }

        public async Task<IActionResult> Update(int id)
        {
            Author authors = await _uow.AuthorRepository.GetByIDAsync(id);
            if (authors == null)
            {
                return NotFound();
            }
            ViewBag.isCreate = false;
            return View(authors);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Author author)
        {
            if (ModelState.IsValid)
            {
                // Получили новую картинку 
                if (author.ImageFile != null)
                {
                    string newImage = await ProcessUploadFile(author);
                    author.AuthorImage = newImage;
                }

                await _uow.AuthorRepository.UpdateAsync(author);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.isCreate = false;
            return View(author);
        }

        protected async Task<string> ProcessUploadFile(Author author)
        {
            string uniqueFileName = "";
            if (author.ImageFile != null)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(author.ImageFile.FileName);
                string fileExtension = Path.GetExtension(author.ImageFile.FileName);
                uniqueFileName = fileName + DateTime.Now.ToString("yymmddssfff") + fileExtension;
                string path = Path.Combine(wwwRootPath + "/uploads/", uniqueFileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await author.ImageFile.CopyToAsync(fileStream);
                }
            }
            return uniqueFileName;
        }
    }
}

