using Dominio.Core.Models.Tarefas;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Dominio.Core.Services.TarefasService
{
    public interface ITarefaService
    {
        Task InsertAsync(Tarefa t);
        Task DeleteAsync(Tarefa t);
        Task UpdateAsync(Tarefa t);
        Task<Tarefa> GetById(Guid id);
        Task<IEnumerable<Tarefa>> GetAllAsync();
        void Dispose();
    }
}
