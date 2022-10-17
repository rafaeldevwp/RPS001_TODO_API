using Dominio.Core.Models.Tarefas;
using Microsoft.AspNetCore.Identity;
using System.Collections;
using System.Collections.ObjectModel;

namespace Dominio.Core.Models.Usuarios
{
    public class Usuario : IdentityUser
    {
        public virtual Collection<Tarefa> Tarefas { get; set; }
    }
}
