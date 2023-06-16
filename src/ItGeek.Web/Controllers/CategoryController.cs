using ItGeek.BLL;
using ItGeek.DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ItGeek.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly UnitOfWork uow;

        public CategoryController(UnitOfWork uow)
        {
            this.uow = uow;
        }

        [HttpGet("[Controller]/{categorySlug}")]
        public async Task<IActionResult> Index(string categorySlug) =>
            View(await uow.CategoryRepository.GetBySlugAsync(categorySlug));

        [HttpGet("[Controller]/{categorySlug}/{postSlug}")]
        public async Task<IActionResult> Post(string categorySlug, string postSlug)
        {
            Post postOne = await uow.PostRepository.GetBySlugAsync(postSlug);
            ViewBag.Category = await uow.CategoryRepository.GetBySlugAsync(categorySlug);
            return View(postOne);
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(Comment comment, string categorySlugOld, string postSlugOld)
        {
            comment.CreatedAt = DateTime.Now;
            if (ModelState.IsValid)
            {
                await uow.CommentRepository.InsertAsync(comment);

                Post postOne = await uow.PostRepository.GetBySlugAsync(postSlugOld);
                PostComment postComment = new PostComment()
                {
                    PostId = postOne.Id,
                    CommentId = comment.Id,
                };
                await uow.PostCommentRepository.InsertAsync(postComment);

            }
            return RedirectToAction("Post", new { categorySlug = categorySlugOld, postSlug = postSlugOld });
        }
    }
}
