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


        [HttpPost("RegistrarRole", Name = "RegistrarRole")]
        public async Task<IActionResult> RegistrarRole(ParametrosAtribuirPerfil parametros)
        {
            if (!ObjetoValido(parametros))
                return CustomResponse();

            var role = new IdentityRole
            {
                Name = parametros.roleName
            };

            await _roleManager.CreateAsync(role);
            return CustomResponse(role);
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
        public async Task<IActionResult> AtribuirRoleUsuario(ParametrosAtribuirPerfil parametros)
        {
            if (!ObjetoValido(parametros))
                return CustomResponse();

            var usuarioRegistrado = await ObterUsuarioPorId(parametros.UsuarioId);
            var roleRegistrada = await ObterRolePorId(parametros.roleName);

            await _userManager.AddToRoleAsync(usuarioRegistrado, roleRegistrada.ToString());

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

        private bool ObjetoValido(object objeto)
        {
            if (objeto != null)
                return true;

            NotificarErro("O objeto não está preenchido favor verificar a operação");
            return false;
        }

        private async Task<IdentityRole> ObterRolePorId(string roleName)
        {
            return await _roleManager.FindByNameAsync(roleName);
        }
    }
}
