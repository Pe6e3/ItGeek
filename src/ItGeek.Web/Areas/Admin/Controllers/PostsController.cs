﻿using ItGeek.BLL;
using ItGeek.BLL.Repositories;
using ItGeek.DAL.Entities;
using ItGeek.Web.Areas.Admin.Controllers;
using ItGeek.Web.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
                    Id = onePost.Id,
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
            Post post = await _uow.PostRepository.GetByIDAsync(id);
            PostContent postContent = await _uow.PostContentRepository.GetByPostIDAsync(id);

            PostViewModel postViewModel = new PostViewModel()
            {
                Id = id,
                Slug = post.Slug,
                IsDeleted = post.IsDeleted,
                Title = postContent.Title,
                PostBody = postContent.PostBody,
                PostImage = postContent.PostImage,
                CommentsClosed = postContent.CommentsClosed
            };

            return View(postViewModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            PostContent postContent = await _uow.PostContentRepository.GetByPostIDAsync(id);
            if (postContent != null)
                await _uow.PostContentRepository.DeleteAsync(postContent);
            Post post = await _uow.PostRepository.GetByIDAsync(id);
            if (post != null)
                await _uow.PostRepository.DeleteAsync(post);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = new SelectList(await _uow.CategoryRepository.ListAllAsync(),"Id", "Name" );
            ViewBag.Authors = new SelectList(await _uow.AuthorRepository.ListAllAsync(),"Id", "Name" );
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PostViewModel postViewModel)
        {
            if (ModelState.IsValid)
            {
                postViewModel.PostImage = await ProcessUploadFile(postViewModel);


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
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            Post post = await _uow.PostRepository.GetByIDAsync(id);
            if (post == null)
                return NotFound();
            PostContent postContent = await _uow.PostContentRepository.GetByPostIDAsync(id);

            PostViewModel postViewModel = new PostViewModel()
            {
                Id = id,
                Slug = post.Slug,
                IsDeleted = post.IsDeleted,
                Title = postContent.Title,
                PostBody = postContent.PostBody,
                PostImage = postContent.PostImage,
                CommentsClosed = postContent.CommentsClosed,
            };
            return View(postViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(PostViewModel postViewModel)
        {
            if (ModelState.IsValid)
            {
                Post post = await _uow.PostRepository.GetByIDAsync(postViewModel.Id);

                post.Slug = postViewModel.Slug;
                post.IsDeleted = postViewModel.IsDeleted;
                post.UpdatedAt = DateTime.Now;
                //TODO: post.EditedBy = User;

                await _uow.PostRepository.UpdateAsync(post);

                // Получим Пост контент
                PostContent postContent = await _uow.PostContentRepository.GetByPostIDAsync(postViewModel.Id);

                // Заполняем Пост контент из формы
                postContent.Title = postViewModel.Title;
                postContent.PostBody = postViewModel.PostBody;
                postContent.CommentsClosed = postViewModel.CommentsClosed;

                // Получили новую картинку 
                if (postViewModel.ImageFile != null)
                {
                    string newImage = await ProcessUploadFile(postViewModel);
                    postContent.PostImage = newImage;

                    //TODO удалить старую картинку
                }
                await _uow.PostContentRepository.UpdateAsync(postContent);

                return RedirectToAction(nameof(Index));
            }
            return View(postViewModel);
        }



        protected async Task<string> ProcessUploadFile(PostViewModel postViewModel)
        {
            string uniqueFileName = "";
            if (postViewModel.ImageFile != null)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath; // путь к корневой папке wwwroot
                string fileName = Path.GetFileNameWithoutExtension(postViewModel.ImageFile.FileName); // Имя без расширения
                string fileExtension = Path.GetExtension(postViewModel.ImageFile.FileName); // Расширение (с точкой)
                uniqueFileName = fileName + DateTime.Now.ToString("yymmddssfff") + fileExtension; // задаем уникальное имя чтобы случайно не совпало с чьим-то другим
                string path = Path.Combine(wwwRootPath + "/uploads/", uniqueFileName); // задаем полный путь к файлу
                using (var fileStream = new FileStream(path, FileMode.Create))  // создаем новый файл по указанному пути
                {
                    await postViewModel.ImageFile.CopyToAsync(fileStream); // сохраняем в него загруженный из формы файл
                }
            }
            return uniqueFileName;
        }
    }
}

