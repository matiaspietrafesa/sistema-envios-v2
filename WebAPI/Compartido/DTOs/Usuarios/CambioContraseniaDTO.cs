using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compartido.DTOs.Usuarios
{
    public class CambioContraseniaDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public string NuevaPassword { get; set; }
    }
}
