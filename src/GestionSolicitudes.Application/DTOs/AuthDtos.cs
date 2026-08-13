using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionSolicitudes.Application.DTOs
{
    public class LoginDto
    {
       public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class RegistroDto 
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Rol { get; set; } = "Usuario";

    }

    public class RespuestaAuthDto
    {
        public bool Exito { get; set; } 
        public string Token { get; set; } = string.Empty;
        public string Mensaje { get; set; } = string.Empty;
    }
}
