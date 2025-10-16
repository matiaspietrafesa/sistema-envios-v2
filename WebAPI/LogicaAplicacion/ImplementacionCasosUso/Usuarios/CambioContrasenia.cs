using Compartido.DTOs.Usuarios;
using Compartido.Mappers;
using LogicaAccesoDatos;
using LogicaAplicacion.InterfacesCasosUso.Usuarios;
using LogicaNegocio.Entidades;
using LogicaNegocio.ExcepcionesEntidades;
using LogicaNegocio.InterfacesRepositorios;
using LogicaNegocio.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.ImplementacionCasosUso.Usuarios
{
    public class CambioContrasenia : ICambioContrasenia
    {
        private IRepositorioUsuarios RepoUsuario { get; set; }

        public CambioContrasenia(IRepositorioUsuarios repoUsuarios)
        {
            RepoUsuario = repoUsuarios;
        }
        public void Ejecutar(CambioContraseniaDTO cambioDTO)
        {
            Usuario usuario = RepoUsuario.FindByEmailAndPassword(cambioDTO.Email, cambioDTO.Password);
            
            if (usuario == null)
            {
                throw new UsuarioException("Credenciales incorrectas");
            }

            if (string.IsNullOrEmpty(cambioDTO.NuevaPassword)) {
                throw new UsuarioException("La nueva contraseña es requerida");
            }

            Password newPass = new Password(cambioDTO.NuevaPassword);

            RepoUsuario.ActualizarPassword(usuario, newPass.Valor);

        }
    }
}
