using Dominio.Core.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dominio.Core.Models.Data
{
    public interface IRepository<T> : IDisposable where T : Entity
    {
        Task<T> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> FindByIdAsync(Guid id);
        Task DeleteByIdAsync(Guid id);
        Task UpdateByIdAsync(T entity);
        Task<int> SaveChanges();
        Task InsertAsync(T entity);

    }

}
