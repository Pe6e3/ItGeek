using ItGeek.BLL;
using ItGeek.BLL.Repositories;
using ItGeek.DAL.Entities;
using ItGeek.Web.Areas.Admin.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace ItGeek.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CommentsController : Controller
    {

        private readonly UnitOfWork _uow;
        public CommentsController(UnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _uow.CommentRepository.ListAllAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            return View(await _uow.CommentRepository.GetByIDAsync(id));
        }
        public async Task<IActionResult> Delete(int id)
        {
            Comment commments = await _uow.CommentRepository.GetByIDAsync(id);
            if (commments != null)
                await _uow.CommentRepository.DeleteAsync(commments);

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Create()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Comment commments)
        {
            if (ModelState.IsValid)
            {
                await _uow.CommentRepository.InsertAsync(commments);
                return RedirectToAction(nameof(Index));

            }
            return View(commments);
        }

        public async Task<IActionResult> Update(int id)
        {
            Comment commments = await _uow.CommentRepository.GetByIDAsync(id);
            if (commments == null)
            {
                return NotFound();
            }
            return View(commments);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Comment commments)
        {
            if (ModelState.IsValid)
            {
                await _uow.CommentRepository.UpdateAsync(commments);
                return RedirectToAction(nameof(Index));

            }
            return View(commments);
        }
    }
}

 