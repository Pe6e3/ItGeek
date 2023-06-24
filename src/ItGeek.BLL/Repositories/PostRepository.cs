using ItGeek.DAL.Data;
using ItGeek.DAL.Entities;
using ItGeek.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ItGeek.BLL.Repositories;

public class PostRepository : GenericRepositoryAsync<Post>, IPostRepository
{

    private readonly AppDbContext db;

    public PostRepository(AppDbContext db) : base(db)
    {
        this.db = db;
    }

    public async Task<Post> GetBySlugAsync(string slug) => await
    db.Posts
        .Include(x => x.Comments)
        .Where(x => x.Slug == slug)
        .Include(x => x.PostContents)
        .FirstAsync();


    public async Task<List<Post>> GetLastAsync(int numberPosts) => await
    db.Posts
        .Include(x => x.PostContents)
        .Include(q => q.Categories)
        .OrderByDescending(x => x.Id)
        .Take(numberPosts)
        .ToListAsync();
    public async Task<List<Post>> ListByCategoryIdAsync(int categoryId)
    {
        List<PostCategory> postCategory = await
        db.PostCategories
            .Where(x => x.CategoryId == categoryId)
            .ToListAsync();

        List<Post> post = new List<Post>();

        foreach (var pc in postCategory)
            post.Add(pc.Post);

        return post;
    }


    public async Task<int> RandomPostId()
    {
        Post? lastPost = await db.Posts.LastOrDefaultAsync();
        int id = lastPost?.Id + 1 ?? 1; // Получаем Id последнего поста и прибавляем к нему 1
        return id;
    }

    public async Task<List<Post>> ListWithIncludeAllAsync() => await
    db.Posts
        .Include(q => q.Categories)
        .ToListAsync();

    public async Task<List<Post>> ListPostsWithCatsAsync() => await
    db.Posts
        .Include(x => x.Categories)
        .ToListAsync();
}
