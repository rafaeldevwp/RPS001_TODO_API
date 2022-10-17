using App.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace App.Controllers
{
    public interface IAuthController
    {
         Task<ActionResult> Registrar(UsuarioRegisterDto userDto);
        Task<string> GerarJwt(string email);
    }
}
