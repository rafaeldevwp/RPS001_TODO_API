using App.DTO;
using Dominio.Core.Services.Notificador;
using Dominio.Core.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GestaoRolesController : MainController
    {
        protected readonly AspNetRoleManager<IdentityRole> _roleManager;

        public GestaoRolesController(INotificador notificador, IUser usuario, AspNetRoleManager<IdentityRole> roleManager) : base(notificador, usuario)
        {
            _roleManager = roleManager;
        }

        // GET: api/<GestaoRolesController>
        [HttpGet]
        public IActionResult listarRoles()
        {
            var listaRoles = _roleManager.Roles.Where(a => a.Name != null).ToList();
            if (!ObjetoValido(listaRoles))
                return CustomResponse();

            return CustomResponse(listaRoles);

        }

        // GET api/<GestaoRolesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> ObterRolePorId(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (!ObjetoValido(role))
                return CustomResponse();

            return CustomResponse(role);
        }

        // POST api/<GestaoRolesController>
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

        // PUT api/<GestaoRolesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarRole(string id, string value)
        {
            if (!ObjetoValido(id))
                CustomResponse();

            var role = await _roleManager.FindByIdAsync(id);

            if (!ObjetoValido(role))
                CustomResponse();

            await _roleManager.UpdateAsync(role);
            return CustomResponse(role);
        }

        // DELETE api/<GestaoRolesController>/5
        [HttpDelete("{id}")]
        public async void Delete(string id)
        {
            if (!ObjetoValido(id))
                CustomResponse();

            var role = await _roleManager.FindByIdAsync(id);
            await _roleManager.DeleteAsync(role);
        }
    }
}
