using ItGeek.DAL.Data;
using ItGeek.DAL.Data.Repositories;
using ItGeek.DAL.Enitities;
using ItGeek.DAL.Interfaces;

namespace ItGeek.BLL.Repositories
{
    public class RoleRepository : GenericRepositoryAsync<Role>, IRoleRepository
    {
        public RoleRepository(AppDbContext db) : base(db)
        {
        }
    }
}
