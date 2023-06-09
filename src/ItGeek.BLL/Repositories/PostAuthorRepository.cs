﻿using ItGeek.DAL.Data;
using ItGeek.DAL.Entities;
using ItGeek.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ItGeek.BLL.Repositories;

public class PostAuthorRepository : GenericRepositoryAsync<PostAuthor>, IPostAuthorRepository
{
    private readonly AppDbContext _db;

    public PostAuthorRepository(AppDbContext db) : base(db)
    {
        _db = db;
    }

    public async Task<int[]> ListByPostIdAsync(int postId)
    {
        List<PostAuthor> postAuthors = await _db.PostAuthors.Where(x => x.PostId == postId).ToListAsync();

        int[] result = new int[postAuthors.Count];

        for (int i = 0; i < postAuthors.Count; i++)
        {
            result[i] = postAuthors[i].AuthorId;
        }

        return result;
    }
    public async Task DeleteAuthorsByPostIdAsync(int postId)
    {
        List<PostAuthor> postAuthors = await _db.PostAuthors.Where(x => x.PostId == postId).ToListAsync();

        foreach (PostAuthor item in postAuthors)
        {
            _db.PostAuthors.Remove(item);
            await _db.SaveChangesAsync();
        }
    }


    public async Task<bool> CheckAuthorInPost(int postId, int authorId)
    {
        return await _db.PostAuthors
            .AnyAsync(pa => pa.PostId == postId && pa.AuthorId == authorId);
    }

    public async Task<int> RandomAuthorId()
    {
        Author? author = await _db.Authors.OrderBy(x => Guid.NewGuid()).FirstOrDefaultAsync();
        return author != null ? author.Id : 1;
    }
}
