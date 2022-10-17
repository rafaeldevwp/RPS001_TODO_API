using Dominio.Core.Model;
using Dominio.Core.Models.Usuarios;
using Dominio.Core.User;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Core.Models.Tarefas
{
    public class Tarefa : Entity
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int Status { get; set; }
        [Required]
        public string UsuarioId { get; set; }

    }
}
