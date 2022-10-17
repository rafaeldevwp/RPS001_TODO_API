using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Core.Models.Tarefas.TarefaValidator
{
    public class TarefaValidator : AbstractValidator<Tarefa>
    {
        public TarefaValidator()
        {
            RuleFor(t => t.Nome).NotEmpty().NotNull().WithMessage("O Campo titulo da tarefa deve ser informado");
            RuleFor(t => t.Descricao).NotEmpty().WithMessage("O Campo descrição deve ser preenchido");
        }
    }
}
