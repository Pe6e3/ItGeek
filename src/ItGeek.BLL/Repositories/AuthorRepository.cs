using ItGeek.DAL.Data;
using ItGeek.DAL.Data.Repositories;
using ItGeek.DAL.Entities;
using ItGeek.DAL.Interfaces;

namespace ItGeek.BLL.Repositories
{
    public class AuthorRepository : GenericRepositoryAsync<Author>, IAuthorRepository
    {
        public AuthorRepository(AppDbContext db) : base(db)
        {
        }
    }
}
