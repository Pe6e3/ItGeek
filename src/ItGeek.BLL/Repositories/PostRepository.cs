using ItGeek.DAL.Data;
using ItGeek.DAL.Data.Repositories;
using ItGeek.DAL.Enitities;
using ItGeek.DAL.Interfaces;

namespace ItGeek.BLL.Repositories
{
    public class PostRepository : GenericRepositoryAsync<Post>, IPostsRepository
    {
        public PostRepository(AppDbContext db) : base(db)
        {
        }
    }
}
