using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compartido.DTOs.Usuarios
{
    public class UsuarioLogueadoDTO
    {
        public string Token { get; set; }
        public int RolId { get; set; }

        public string Rol { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get;set; }
    }
}
