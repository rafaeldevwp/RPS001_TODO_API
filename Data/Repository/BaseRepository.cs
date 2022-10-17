using Data.Contexto;
using Dominio.Core.Model;
using Dominio.Core.Models.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class BaseRepository<T> : IRepository<T> where T : Entity, new()
    {
        private readonly TarefaContexto _context;
        private readonly DbSet<T> _dbset;

        public BaseRepository(TarefaContexto contexto)
        {
            _context = contexto;
            _dbset = _context.Set<T>();
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            
            _context.Entry(new { Id = id }).State = EntityState.Deleted;
            await SaveChanges();
            Dispose();
        }


        public async Task<T> FindByIdAsync(Guid id)
        {
            return await _dbset.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbset.AsNoTracking().ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbset.FirstOrDefaultAsync(x => x.Id == id);

        }

        public async Task InsertAsync(T entity)
        {
            _context.Add(entity);
            await _context.SaveChangesAsync();
            Dispose();

        }

        public async Task UpdateByIdAsync(T entity)
        {
            _dbset.Update(entity);
            await SaveChanges();
 
        }

        public async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
