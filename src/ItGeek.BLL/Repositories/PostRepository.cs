using ItGeek.DAL.Data;
using ItGeek.DAL.Data.Repositories;
using ItGeek.DAL.Entities;
using ItGeek.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ItGeek.BLL.Repositories;


public class PostRepository : GenericRepositoryAsync<Post>, IPostRepository
{

	private readonly AppDbContext _db;

	public PostRepository(AppDbContext db) : base(db)
	{
		_db = db;
	}

	public async Task<Post> GetBySlugAsync(string slug) => await _db.Posts.Where(x => x.Slug == slug).FirstAsync(); // Находим все посты, у которых Слаг такой же, который передали в метод


	public async Task<List<PostCategory>> ListByCategoryIdAsync(int categoryId)
	{
		var postContentCategory = await _db.PostCategories
			.Where(cat => cat.CategoryId == categoryId)
			.Include(table => table.Post)
			.Include(table => table.Post.PostContent)
			.ToListAsync();

		return postContentCategory;
	}





	public async Task<Post> GetPostWithContentAsync(int postId)
	{
		return await _db.Posts
			.Include(p => p.PostContent)
			.FirstOrDefaultAsync(p => p.Id == postId);
	}

}