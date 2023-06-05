using ItGeek.DAL.Data;
using ItGeek.DAL.Data.Repositories;
using ItGeek.DAL.Entities;
using ItGeek.DAL.Interfaces;

namespace ItGeek.BLL.Repositories
{
    public class AuthorSocialRepository : GenericRepositoryAsync<AuthorsSocial>, IAuthorSocialRepository
    {
        public AuthorSocialRepository(AppDbContext db) : base(db)
        {
        }
    }
}
