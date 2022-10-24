using App.DTO;
using App.Extensions;
using AutoMapper;
using Dominio.Core.Services.Notificador;
using Dominio.Core.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
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
        protected AspNetRoleManager<IdentityRole> _roleManager;


        public GestaoAcessosController
       (
            INotificador notificador, IUser usuario,
            IMapper mapper, ClaimsIdentity claimsIdentity,
            AspNetRoleManager<IdentityRole> roleManager,
            UserManager<IdentityUser> userManager) : base(notificador, usuario)
        {
            _claimIdentity = claimsIdentity;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
        }


      

        [HttpPost("RegistrarClaim", Name = "RegistrarClaim")]
        public async Task<IActionResult> RegistrarClaim(ParametrosAtribuirPerfil parametros)
        {
            if (!ObjetoValido(parametros))
                return CustomResponse();

            var usuarioregistrado = await ObterUsuarioPorId(parametros.UsuarioId);
            var claim = CadastrarClaim(parametros.type, parametros.value);

            if (OperacaoValida())
            {
                await _userManager.AddClaimAsync(usuarioregistrado, claim);
                return CustomResponse();
            }

            return CustomResponse();
        }

        [HttpPost("AtribuirRoleUsuario", Name = "AtribuirRoleUsuario")]
        [RoleAuthorize("Gerencia")]
        public async Task<IActionResult> AtribuirRoleUsuario(ParametrosAtribuirPerfil parametros)
        {
            if (!ObjetoValido(parametros))
                return CustomResponse();

            var roleRegistrada = await ObterRolePorId(parametros.roleName);
            await _userManager.AddToRoleAsync(await ObterUsuarioPorId(parametros.UsuarioId), roleRegistrada.ToString());

            return CustomResponse();
        }

        private async Task<IdentityUser> ObterUsuarioPorId(string userId)
        {
            if (String.IsNullOrEmpty(userId))
                NotificarErro("O Usuario não foi informado");

            return await _userManager.FindByIdAsync(userId);
        }

        private Claim CadastrarClaim(string tipo, string valor)
        {
            if (String.IsNullOrEmpty(tipo) || String.IsNullOrEmpty(valor))
                NotificarErro("O valores da Claim não foi informado");

            return new Claim(tipo, valor);
        }

        private async Task<IdentityRole> ObterRolePorId(string roleName)
        {
            return await _roleManager.FindByNameAsync(roleName);
        }
    }
}
