using Compartido.DTOs.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.InterfacesCasosUso.Usuarios
{
    public interface ILogin
    {
        public UsuarioLogueadoDTO Ejecutar(UsuarioDTO usuarioDTO);
    }
}
