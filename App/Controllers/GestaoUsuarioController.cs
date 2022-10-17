using App.DTO;
using AutoMapper;
using Dominio.Core.Models.Usuarios;
using Dominio.Core.Services.Notificador;
using Dominio.Core.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace App.Controllers
{
    [Route("api/[controller]")]
    public class GestaoAcessosController : MainController
    {
        protected ClaimsIdentity _claimIdentity;
        protected IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;



        public GestaoAcessosController
       (
            INotificador notificador, IUser usuario,
            IMapper mapper, ClaimsIdentity claimsIdentity,
            UserManager<IdentityUser> userManager) : base(notificador, usuario)
        {
            _claimIdentity = claimsIdentity;
            _mapper = mapper;
            _userManager = userManager;
        }



        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }



        [HttpPost]
        public async Task<IActionResult> Registrar(ParametrosAtribuirPerfil parametros)
        {
            if (parametros is null)
            { NotificarErro("O objeto não está preenchido favor verificar a operação");
                return CustomResponse();
            }

            var usuarioregistrado = await ObterUsuarioPorId(parametros.UsuarioId);
            var claim = CadastrarClaim(parametros.type, parametros.value);

            if (OperacaoValida()) {
                await _userManager.AddClaimAsync(usuarioregistrado, claim);
                return CustomResponse();
            }

            return CustomResponse();
        }



        private async Task<IdentityUser> ObterUsuarioPorId(string userId)
        {
            if (String.IsNullOrEmpty(userId)) NotificarErro("O Usuario não foi informado");

            return await _userManager.FindByIdAsync(userId);
        }

        private Claim CadastrarClaim(string tipo, string valor)
        {
            if (String.IsNullOrEmpty(tipo) || String.IsNullOrEmpty(valor))
                NotificarErro("O valores da Claim não foi informado");

            return new Claim(tipo,valor);
        }

    }
}
