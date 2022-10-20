using Dominio.Core.User;
using System.Security.Claims;

namespace App.DTO
{
    public class ParametrosAtribuirPerfil 
    {
        public string UsuarioId { get; set; }
        public string type { get; set; }
        public string value { get; set; }
        public string roleName { get; set; }
    }
}
