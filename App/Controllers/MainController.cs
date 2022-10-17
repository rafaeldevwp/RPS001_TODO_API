using Dominio.Core.Services.Notificador;
using Dominio.Core.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace App.Controllers
{
    [ApiController]
    public class MainController : ControllerBase
    {
        protected readonly INotificador _notificador;
        protected readonly IUser _usuario;

        public Guid UsuarioId;
        public bool UsuarioAutenticado;

        public MainController(INotificador notificador,
                              IUser usuario)
        {
            _notificador = notificador;
            _usuario = usuario;

            if (usuario.IsAuthenticated())
            {
                UsuarioId = usuario.GetUserId();
                UsuarioAutenticado = true;
            }

        }

        protected bool OperacaoValida()
        {
            return !_notificador.TemNotificacao();
        }

        protected ActionResult CustomResponse(object result = null)
        {
            if (OperacaoValida())
            {
                return Ok(new
                {
                    success = true,
                    data = result
                });

            }


            return BadRequest(new
            {
                success = false,
                errors = _notificador.ObterNotificacoes().Select(n => n.Mensagem)
            }); 
        }

        protected bool UsuarioLogado()
        {
            if (!UsuarioAutenticado) NotificarErro("Usuário deve estar logado para executar a operação");
            return false;

        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid) NotificarErroModelInvalida(modelState);
            return CustomResponse();
        }

        protected void NotificarErroModelInvalida(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(e => e.Errors);
            foreach (var erro in erros)
            {
                var errorMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                NotificarErro(errorMsg );
            }

        }

        protected void NotificarErro(string mensagem)
        {
            _notificador.Handle(new Notificacao(mensagem));
        }


    }


}

