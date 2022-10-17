using Dominio.Core.Data;
using Dominio.Core.Models.Tarefas;
using Dominio.Core.Models.Tarefas.TarefaValidator;
using Dominio.Core.Services.Notificador;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dominio.Core.Services.TarefasService
{
    public class TarefaService : BaseServices, ITarefaService
    {

        private readonly ITarefaRepository _tarefaRepository;
      
        public TarefaService(ITarefaRepository tarefa,
                             INotificador notificador) : base(notificador)
        {
            _tarefaRepository = tarefa;
         
        }
        public async Task DeleteAsync(Tarefa t)
        {
            await _tarefaRepository.DeleteByIdAsync(t.Id);
        }

        public async Task<IEnumerable<Tarefa>> GetAllAsync()
        {
            return await _tarefaRepository.GetAllAsync();
        }

        public async Task<Tarefa> GetById(Guid id)
        {
            return await _tarefaRepository.GetByIdAsync(id);
        }

        public async Task InsertAsync(Tarefa t)
        {
            if (!ExecutarValidacao(new TarefaValidator(), t)) return;
             await _tarefaRepository.InsertAsync(t);
        }

        public async Task UpdateAsync(Tarefa t)
        {
            if (!ExecutarValidacao(new TarefaValidator(), t)) return;
            await _tarefaRepository.UpdateByIdAsync(t);
        }

        public void Dispose() {
            _tarefaRepository?.Dispose();
        }
    }
}
