using ItGeek.BLL;
using ItGeek.DAL.Entities;
using ItGeek.Web.Areas.Admin.ViewModels;
using ItGeek.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ItGeek.Web.Controllers
{
	public class CategoryController : Controller
	{
		private readonly UnitOfWork _uow;

		public CategoryController(UnitOfWork uow)
		{
			_uow = uow;
		}
		[HttpGet("[Controller]/{categorySlug}")]
		public async Task<IActionResult> Index(string categorySlug)
		{
			Category cat = await _uow.CategoryRepository.GetBySlugAsync(categorySlug);
			ViewBag.Category = cat;
			List<PostCategory> catPosts = await _uow.PostRepository.ListByCategoryIdAsync(cat.Id);
			List<PostViewModel> catPostContents = new List<PostViewModel>();

			foreach (PostCategory postCategory in catPosts)
			{
				PostViewModel postCategoryViewModel = new PostViewModel()
				{
					Id = postCategory.PostId,
					Slug = postCategory.Post?.Slug,
					Title = postCategory.Post?.PostContent?.Title,
					PostBody = postCategory.Post?.PostContent?.PostBody,
					PostImage = postCategory.Post.PostContent?.PostImage,
					IsDeleted = postCategory.Post.IsDeleted,
					CommentsClosed = postCategory.Post.PostContent.CommentsClosed,
				};

				catPostContents.Add(postCategoryViewModel);
			}

			return View(catPostContents);
		}



		//// по этому адресу будет переходить на этот экшн. Например https://localhost:7067/itnews/123
		[HttpGet("[Controller]/{categorySlug}/{postSlug}")]
		public async Task<IActionResult> Post(string categorySlug, string postSlug)
		{
			Post postOne = await _uow.PostRepository.GetBySlugAsync(postSlug);
			Category category = await _uow.CategoryRepository.GetBySlugAsync(categorySlug);
			List<PostCategory> allPosts = await _uow.PostRepository.ListByCategoryIdAsync(category.Id);


			//PostContentViewModel postContent = new PostContentViewModel()
			//{
			//	category = category,
			//	post = postOne,
			//	postContent = await _uow.PostContentRepository.GetByPostIDAsync(postOne.Id),
			//	posts = allPosts,
			//	postContents = allPostContents
			//};

			return View();
		}



	}
}
