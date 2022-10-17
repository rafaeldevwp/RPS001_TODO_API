using Dominio.Core.Model;
using FluentValidation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Core.Services.Notificador
{
    public interface INotificador 
    {
        bool TemNotificacao();
        List<Notificacao> ObterNotificacoes();
        void Handle(Notificacao notificacao);
      
    }
}
