using ItGeek.DAL.Data;
using ItGeek.DAL.Data.Repositories;
using ItGeek.DAL.Entities;
using ItGeek.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ItGeek.BLL.Repositories;

public class PostTagRepository : GenericRepositoryAsync<PostTag>, IPostTagRepository
{
    private readonly AppDbContext _db;

    public PostTagRepository(AppDbContext db) : base(db)
    {
        _db = db;
    }

    public async Task<string> GetByPostIdAsync(int postId)
    {
        string tags = "";
        List<PostTag> postTags = await _db.PostTags.Include(x => x.Tag).Where(x => x.PostId == postId).ToListAsync();
        if (postTags != null)
            foreach (PostTag postTag in postTags)
                tags += postTag.Tag.Name + ", ";
        return tags;
    }


    public Task<bool> GetByTagIdAsync(int postId, int tagId) => _db.PostTags.AnyAsync(x => x.PostId == postId && x.TagId == tagId);


    public async Task<int> GetTagIdByName(string tagName)
    {
        Tag tag = await _db.Tags.Where(x => x.Name == tagName).FirstOrDefaultAsync();
        if (tag != null)
            return tag.Id;
        return 0;
    }
}