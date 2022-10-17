using Data.Contexto;
using Dominio.Core.Data;
using Dominio.Core.Models.Tarefas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.TarefaRepository
{
    public class TarefaRepository : BaseRepository<Tarefa>, ITarefaRepository
    {
        public TarefaRepository(TarefaContexto contexto) : base(contexto)
        {
        }
    }
}
