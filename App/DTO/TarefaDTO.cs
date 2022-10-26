using System;
using System.ComponentModel.DataAnnotations;

namespace App.DTO
{
    public class TarefaDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage =" O campo {0} é obigatório")]
        [Display (Name = "Titulo da Tarefa")]
        public string Nome { get; set; }

        [Display(Name = "Descrição da tarefa")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Descricao { get; set; }

        [Display(Name = "Status da tarefa")]
        public int Status { get; set; }

        public string UsuarioId { get; set; }
    }
}
