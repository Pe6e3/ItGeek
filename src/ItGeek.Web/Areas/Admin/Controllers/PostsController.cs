using ItGeek.BLL;
using ItGeek.DAL.Entities;
using ItGeek.Web.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ItGeek.Web.Areas.Admin.Controllers;

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
        List<Post> allPosts = (List<Post>)await _uow.PostRepository.ListPostsWithCatsAsync();
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
                Categories = onePost.Categories
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
        ViewBag.Authors = await _uow.AuthorRepository.ListAllAsync();
        ViewBag.Categories = await _uow.CategoryRepository.ListAllAsync();
        ViewBag.PostCategories = new int[0];
        ViewBag.PostAuthors = new int[0];
        ViewBag.CategoryCount = null;
        ViewBag.AuthorCount = null;
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
                EditedAt = DateTime.Now,
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


            string[] tagsNames = postViewModel.TagIds.Split(new char[] { ',' });

            foreach (string tagName in tagsNames)
            {
                int tagId = await _uow.PostTagRepository.GetTagIdByName(tagName.Trim());
                if (tagId != 0)
                {
                    PostTag postTag = new PostTag()
                    {
                        PostId = post.Id,
                        TagId = tagId
                    };
                    await _uow.PostTagRepository.InsertAsync(postTag);
                }
            }

            foreach (int catId in postViewModel.CategoryId)
            {

                PostCategory postCategory = new PostCategory()
                {
                    PostId = post.Id,
                    CategoryId = catId,
                };
                await _uow.PostCategoryRepository.InsertAsync(postCategory);
            }

            foreach (int authId in postViewModel.AuthorId)
            {

                PostAuthor postAuthor = new PostAuthor()
                {
                    PostId = post.Id,
                    AuthorId = authId,
                };
                await _uow.PostAuthorRepository.InsertAsync(postAuthor);
            }

            return RedirectToAction(nameof(Index));
        }
        ViewBag.Authors = await _uow.AuthorRepository.ListAllAsync();
        ViewBag.Categories = await _uow.CategoryRepository.ListAllAsync();
        ViewBag.PostCategories = await _uow.PostCategoryRepository.ListByPostIdAsync(postViewModel.Id);
        ViewBag.PostAuthors = await _uow.PostAuthorRepository.ListByPostIdAsync(postViewModel.Id);
        ViewBag.CategoryCount = ((IEnumerable<int>)ViewBag.PostCategories).ToList().Count;
        ViewBag.AuthorCount = ((IEnumerable<int>)ViewBag.PostAuthors).ToList().Count;
        return View(postViewModel);

    }
    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        Post post = await _uow.PostRepository.GetByIDAsync(id);
        if (post == null)
        {
            return NotFound();
        }
        PostContent postContent = await _uow.PostContentRepository.GetByPostIDAsync(id);

        string tagNames = await _uow.PostTagRepository.GetByPostIDAsync(id);

        PostViewModel postViewModel = new PostViewModel()
        {
            Id = id,
            Slug = post.Slug,
            IsDeleted = post.IsDeleted,
            Title = postContent.Title,
            PostBody = postContent.PostBody,
            PostImage = postContent.PostImage,
            CommentsClosed = postContent.CommentsClosed,
            TagIds = tagNames,
        };
        ViewBag.Authors = await _uow.AuthorRepository.ListAllAsync();
        ViewBag.Categories = await _uow.CategoryRepository.ListAllAsync();
        ViewBag.PostCategories = await _uow.PostCategoryRepository.ListByPostIdAsync(id);
        ViewBag.PostAuthors = await _uow.PostAuthorRepository.ListByPostIdAsync(id);
        ViewBag.CategoryCount = ((IEnumerable<int>)ViewBag.PostCategories).ToList().Count;
        ViewBag.AuthorCount = ((IEnumerable<int>)ViewBag.PostAuthors).ToList().Count;

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
            post.EditedAt = DateTime.Now;
            // TODO: post.EditedBy = User;

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

                // TODO: удалить старую картинку
            }

            await _uow.PostContentRepository.UpdateAsync(postContent);

            // Удалить все существующие теги для данного поста
            await _uow.PostTagRepository.DeleteTagsByPostIAsync(post.Id);

            string[] tagsNames = postViewModel.TagIds?.Split(new char[] { ',' }) ?? new string[0];

            foreach (string tagName in tagsNames)
            {
                int tagId = await _uow.PostTagRepository.GetTagIdByName(tagName.Trim());
                if (tagId != 0)
                {
                    bool havePostTag = await _uow.PostTagRepository.CheckTagInPost(post.Id, tagId);
                    if (!havePostTag)
                    {
                        PostTag postTag = new PostTag()
                        {
                            PostId = post.Id,
                            TagId = tagId
                        };
                        await _uow.PostTagRepository.InsertAsync(postTag);
                    }
                }
            }

            // Удалить всех существующих авторов для данного поста
            await _uow.PostAuthorRepository.DeleteAuthorsByPostIdAsync(post.Id);

            foreach (int authorId in postViewModel.AuthorId)
            {
                bool havePostAuthor = await _uow.PostAuthorRepository.CheckAuthorInPost(post.Id, authorId);
                if (!havePostAuthor)
                {
                    PostAuthor postAuthor = new PostAuthor()
                    {
                        PostId = post.Id,
                        AuthorId = authorId
                    };
                    await _uow.PostAuthorRepository.InsertAsync(postAuthor);
                }
            }

            // Удаляем все существующие категории
            await _uow.PostCategoryRepository.DeleteCategoriesByPostIdAsync(post.Id);

            foreach (int catId in postViewModel.CategoryId)
            {
                bool havePostCategory = await _uow.PostCategoryRepository.CheckCategoryPost(catId, post.Id);
                if (!havePostCategory)
                {

                    PostCategory postCategory = new PostCategory()
                    {
                        PostId = post.Id,
                        CategoryId = catId,
                    };
                    await _uow.PostCategoryRepository.InsertAsync(postCategory);
                }
            }

            return RedirectToAction(nameof(Index));
        }

        ViewBag.Authors = await _uow.AuthorRepository.ListAllAsync();
        ViewBag.Categories = await _uow.CategoryRepository.ListAllAsync();
        ViewBag.PostCategories = await _uow.PostCategoryRepository.ListByPostIdAsync(postViewModel.Id);
        ViewBag.PostAuthors = await _uow.PostAuthorRepository.ListByPostIdAsync(postViewModel.Id);
        ViewBag.CategoryCount = ((IEnumerable<int>)ViewBag.PostCategories).ToList().Count;
        ViewBag.AuthorCount = ((IEnumerable<int>)ViewBag.PostAuthors).ToList().Count;
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

    public async Task<JsonResult> GetTagByName(string tag)
    {

        List<Tag> result = await _uow.TagRepository.GetTagByNameAsync(tag);

        List<string> res = new List<string>();
        foreach (Tag item in result)
        {
            res.Add(item.Name);
        }

        return Json(res);
    }

    public async Task<IActionResult> GenerateRandomPosts(int count = 1)
    {
        for (int i = 0; i < count; i++)
        {

            Post post = new Post();
            PostContent content = new PostContent();
            PostCategory postCat = new PostCategory();
            PostAuthor postAuthor = new PostAuthor();
            PostTag postTag = new PostTag();
            Random random = new Random();

            post.Slug = await GetRandomWords(1);
            post.CreatedAt = DateTime.Now;
            //post.CreatedBy =
            await _uow.PostRepository.InsertAsync(post);

            content.Title = post.Id + ". " + await GetRandomWords(3);
            content.PostBody = await GetRandomWords(50);
            content.PostImage = random.Next(1, 32).ToString() + ".jpg"; // надо добавить в папку uploads изображения с названием 1.jpg, 2.jpg  и так далее попорядку. Количество поставить в random.Next (у меня 32)
            content.PostId = post.Id;

            postCat.PostId = post.Id;
            postCat.CategoryId = await _uow.CategoryRepository.RandomCatId();

            postAuthor.PostId = post.Id;
            postAuthor.AuthorId = await _uow.PostAuthorRepository.RandomAuthorId();

            postTag.PostId = post.Id;
            postTag.TagId = await _uow.PostTagRepository.RandomTagId();

            await _uow.PostAuthorRepository.InsertAsync(postAuthor);
            await _uow.PostCategoryRepository.InsertAsync(postCat);
            await _uow.PostContentRepository.InsertAsync(content);
            await _uow.PostTagRepository.InsertAsync(postTag);

        }
        return RedirectToAction("Index");
    }

    static async Task<string> GetRandomWords(int count)
    {
        string[] words = new string[count];
        string space = count > 1 ? " " : "";
        using (HttpClient client = new HttpClient())
        {
            string apiUrl = $"https://random-word-api.herokuapp.com/word?number={count}";
            HttpResponseMessage response = await client.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                words = JsonSerializer.Deserialize<string[]>(jsonResponse);
            }
            else
                throw new Exception("Failed to retrieve random words.");
        }
        return string.Join(space, words);
    }
}
