﻿using System.Collections.Generic;

namespace ItGeek.DAL.Interfaces;

public interface IGenericRepositoryAsync<T> where T : class
{
    Task<IReadOnlyList<T>> ListAllAsync();
    Task<IReadOnlyList<T>> GetPagedAsync(int page,int size);

    Task<T> GetByIDAsync(int id);
    Task<T> InsertAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);

}
