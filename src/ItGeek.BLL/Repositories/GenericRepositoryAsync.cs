using ItGeek.DAL.Interfaces;
using ItGeek.DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace ItGeek.DAL.Data.Repositories
{
    public class GenericRepositoryAsync<T> : IGenericRepositoryAsync<T> where T : class
    {
        private readonly AppDbContext _db;

        public GenericRepositoryAsync(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IReadOnlyList<T>> GetPagedAsync(int page, int size)
        {
            return await _db.Set<T>().ToListAsync();
        }

        public Task<T> GetByIDAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(T entity)
        {
            throw new NotImplementedException();
        }
  

        public Task<T> InsertAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<T>> ListAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
