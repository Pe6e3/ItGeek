﻿using ItGeek.DAL.Data;
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

	public async Task<List<Post>> ListByCategoryIdAsync(int categoryId)
	{
        Category cat = await _db.Categories.FindAsync(categoryId);  // нашли одну категорию, посты которой будем искать
        List<PostCategory> postCategory = await _db.PostCategories.Where(x=>x.CategoryId == categoryId).ToListAsync(); // Нашли все объекты промежуточной таблицы, которые относятся к этой категории
        List<Post> post = new List<Post>(); // создали пустой список постов
        foreach (var pc in postCategory)    // добавили в этот список все посты, ссылки на которые были в промежуточной таблице
            post.Add(pc.Post);
        return post;                        // вернули список полученных постов
	}
}