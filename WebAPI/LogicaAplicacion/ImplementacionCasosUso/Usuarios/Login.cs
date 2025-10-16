using Compartido.DTOs.Usuarios;
using Compartido.Mappers;
using LogicaAccesoDatos;
using LogicaAplicacion.InterfacesCasosUso.Usuarios;
using LogicaNegocio.Entidades;
using LogicaNegocio.ExcepcionesEntidades;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.ImplementacionCasosUso.Usuarios
{
    public class Login : ILogin
    {
        private IRepositorioUsuarios RepoUsuario { get; set; }

        public Login(IRepositorioUsuarios repoUsuarios)
        {
            RepoUsuario = repoUsuarios;
        }
        public UsuarioLogueadoDTO Ejecutar(UsuarioDTO usuarioDTO)
        {
            Usuario usuario = RepoUsuario.FindByEmailAndPassword(usuarioDTO.Email, usuarioDTO.Password);
            
            if (usuario == null)
            {
                throw new UsuarioException("Credenciales incorrectas");
            }
            return UsuarioMapper.UsuarioLogueadoDTOFromUsuario(usuario);
        }
    }
}
