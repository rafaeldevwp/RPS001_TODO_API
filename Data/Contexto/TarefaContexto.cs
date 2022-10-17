using Data.Mapping;
using Dominio.Core.Models.Tarefas;
using Microsoft.EntityFrameworkCore;

namespace Data.Contexto
{
    public class TarefaContexto : DbContext
    {
        public TarefaContexto(DbContextOptions<TarefaContexto> options) : base(options) {   }

        public virtual DbSet<Tarefa> Tarefa { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new TarefaConfig());

        }

    }
}
