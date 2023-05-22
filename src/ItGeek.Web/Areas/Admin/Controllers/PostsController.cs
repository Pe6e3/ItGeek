using ItGeek.BLL;
using ItGeek.BLL.Repositories;
using ItGeek.DAL.Entities;
using ItGeek.Web.Areas.Admin.Controllers;
using ItGeek.Web.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ItGeek.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PostsController : Controller
    {

        private readonly UnitOfWork _uow;
        private readonly IWebHostEnvironment _hostEnvironment;

        public PostsController(UnitOfWork uow, IWebHostEnvironment hostEnvironment)
        {
            _uow = uow;
            _hostEnvironment = hostEnvironment;

        }

        public async Task<IActionResult> Index()
        {
            List<Post> allPosts = (List<Post>)await _uow.PostRepository.ListAllAsync();
            List<PostContent> allPostsContent = (List<PostContent>)await _uow.PostContentRepository.ListAllAsync();


            List<PostViewModel> post = new List<PostViewModel>();
            foreach (var onePost in allPosts)
            {
                PostContent onePostsContent = allPostsContent.First(x => x.PostId == onePost.Id);
                post.Add(new PostViewModel()
                {
                    Slug = onePost.Slug,
                    IsDeleted = onePost.IsDeleted,
                    Title = onePostsContent.Title,
                    PostBody = onePostsContent.PostBody,
                    PostImage = onePostsContent.PostImage,
                    CommentsClosed = onePostsContent.CommentsClosed,
                }
                );
            }

            return View(post);
        }

        public async Task<IActionResult> Details(int id)
        {
            return View(await _uow.PostRepository.GetByIDAsync(id));
        }
        public async Task<IActionResult> Delete(int id)
        {
            Post posts = await _uow.PostRepository.GetByIDAsync(id);
            if (posts != null)
                await _uow.PostRepository.DeleteAsync(posts);

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PostViewModel postViewModel)
        {
            if (ModelState.IsValid)
            {
                postViewModel.PostImage = ProcessUploadFile(postViewModel).ToString();


                Post post = new Post()
                {
                    Id = postViewModel.Id,
                    Slug = postViewModel.Slug,
                    IsDeleted = postViewModel.IsDeleted,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                };
                PostContent postContent = new PostContent()
                {
                    PostId = postViewModel.Id,
                    Post = post,
                    Title = postViewModel.Title,
                    PostBody = postViewModel.PostBody,
                    PostImage = postViewModel.PostImage,
                    CommentsNum = 0,
                    CommentsClosed = postViewModel.CommentsClosed
                };
                await _uow.PostRepository.InsertAsync(post);
                await _uow.PostContentRepository.InsertAsync(postContent);
                return RedirectToAction(nameof(Index));
            }
            return View(postViewModel);

        }
        public async Task<IActionResult> Update(int id)
        {
            Post posts = await _uow.PostRepository.GetByIDAsync(id);
            if (posts == null)
            {
                return NotFound();
            }
            return View(posts);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Post posts)
        {
            if (ModelState.IsValid)
            {
                await _uow.PostRepository.UpdateAsync(posts);
                return RedirectToAction(nameof(Index));

            }
            return View(posts);
        }

        protected async Task<string> ProcessUploadFile(PostViewModel postViewModel)
        {
            string uniqueFileName = "";
            if(postViewModel.ImageFile != null)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(postViewModel.ImageFile.FileName); // Имя без расширения
                string fileExtension = Path.GetExtension(postViewModel.ImageFile.FileName); // Расширение (с точкой)
                uniqueFileName = fileName + DateTime.Now.ToString("yymmddssfff") + fileExtension;
                string path = Path.Combine(wwwRootPath + "/uploads/", uniqueFileName);
                using(var fileStream = new FileStream(path, FileMode.Create))
                {
                    await postViewModel.ImageFile.CopyToAsync(fileStream);
                }
            }
            return uniqueFileName;
        }
    }
}

