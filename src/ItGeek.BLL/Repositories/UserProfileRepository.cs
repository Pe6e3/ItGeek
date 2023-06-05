using ItGeek.DAL.Data;
using ItGeek.DAL.Data.Repositories;
using ItGeek.DAL.Entities;
using ItGeek.DAL.Interfaces;

namespace ItGeek.BLL.Repositories
{
    public class UserProfileRepository : GenericRepositoryAsync<UserProfile>, IUserProfilesRepository
    {
        public UserProfileRepository(AppDbContext db) : base(db)
        {
        }
    }
}
