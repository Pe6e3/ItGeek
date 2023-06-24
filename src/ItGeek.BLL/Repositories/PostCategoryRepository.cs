using ItGeek.DAL.Data;
using ItGeek.DAL.Entities;
using ItGeek.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ItGeek.BLL.Repositories;

public class PostCategoryRepository : GenericRepositoryAsync<PostCategory>, IPostCategoryRepository
{
	private readonly AppDbContext _db;

	public PostCategoryRepository(AppDbContext db) : base(db)
	{
		_db = db;
	}

    public async Task<bool> CheckCategoryPost(int catId, int postId)
    {
        return await _db.PostCategories
            .AnyAsync(pc => pc.PostId == postId && pc.CategoryId == catId);
    }

    public async Task DeleteCategoriesByPostIdAsync(int id)
    {
        List<PostCategory> pc = await _db.PostCategories.Where(pc => pc.PostId == id).ToListAsync();
        _db.PostCategories.RemoveRange(pc);
        await _db.SaveChangesAsync();
    }

    public async Task<int[]> ListByPostIdAsync(int postId)
	{
		List<PostCategory> postCategories = await _db.PostCategories.Where(x=>x.PostId==postId).ToListAsync();

		int[] result = new int[postCategories.Count];

		for (int i = 0; i < postCategories.Count; i++)
		{
			result[i] = postCategories[i].CategoryId;
		}

		return result;
	}
}
