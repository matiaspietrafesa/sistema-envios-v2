using Compartido.DTOs.Usuarios;
using LogicaNegocio.Entidades;
using LogicaNegocio.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compartido.Mappers
{
    public class UsuarioMapper
    {

        public static UsuarioLogueadoDTO UsuarioLogueadoDTOFromUsuario(Usuario usuario)
        {
            if (usuario == null)
            {
                throw new ArgumentException("Datos incorrectos");
            }
            return new UsuarioLogueadoDTO()
            {
                Email = usuario.Email.Valor,
                RolId = usuario.Rol.Id,
                Nombre = usuario.Nombre.Valor,
                Apellido = usuario.Apellido.Valor,
                Rol = usuario.Rol.Nombre
            };
        }


    }
}
