using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Data.Contexto.ContextoFactory
{
    internal class AppDbContextFactory : IDesignTimeDbContextFactory<TarefaContexto>
    {
        public TarefaContexto CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TarefaContexto>();
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Tarefas;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            return new TarefaContexto(optionsBuilder.Options);
        }
    }
}
