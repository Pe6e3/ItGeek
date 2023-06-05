using ItGeek.DAL.Data;
using ItGeek.DAL.Data.Repositories;
using ItGeek.DAL.Entities;
using ItGeek.DAL.Enum;
using ItGeek.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ItGeek.BLL.Repositories
{
    public class RoleRepository : GenericRepositoryAsync<Role>, IRoleRepository
    {
        private readonly AppDbContext _db;

        public RoleRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<int> GetBasicAsync() => await _db.Roles.Where(x => x.RoleName == RoleName.Basic).Select(x => x.Id).FirstAsync();
    }
}
