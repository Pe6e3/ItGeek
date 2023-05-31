using ItGeek.DAL.Data;
using ItGeek.DAL.Data.Repositories;
using ItGeek.DAL.Entities;
using ItGeek.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ItGeek.BLL.Repositories
{
    public class UserRepository : GenericRepositoryAsync<User>, IUserRepository
    {
        private readonly AppDbContext _db;

        public UserRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<User?> ValidateLoginPasswordAsync(string email, string password)
        {
            User user = await _db.Users.Where(x => x.Email == email).FirstAsync();
            if (user == null)
                return null;
            bool newPassword = BCrypt.Net.BCrypt.Verify(password, user.Password);
            if (newPassword)
                return user;
            return null;
        }
        public string PasswordHash(string password) => BCrypt.Net.BCrypt.HashPassword(password);

        private string PasswordDeHash(string password) => BCrypt.Net.BCrypt.HashPassword(password);

        public async Task<User> GetByEmailAsync(string email)
        {
            User? user = await _db.Users.Where(x => x.Email == email).FirstOrDefaultAsync();
            if (user != null)
                return user;
            return null;
        }
    }
}
