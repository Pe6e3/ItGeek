﻿using ItGeek.DAL.Data;
using ItGeek.DAL.Data.Repositories;
using ItGeek.DAL.Entities;
using ItGeek.DAL.Interfaces;

namespace ItGeek.BLL.Repositories
{
    public class CommentRepository : GenericRepositoryAsync<Comment>, ICommentsRepository
    {
        public CommentRepository(AppDbContext db) : base(db)
        {
        }
    }
}
