using Compartido.DTOs.Usuarios;
using LogicaAplicacion.InterfacesCasosUso.Usuarios;
using LogicaNegocio.ExcepcionesEntidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.JWT;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private ICambioContrasenia CUCambioContrasenia { get; set; }

        public ILogin CULogin { get; set; }

        public UsuarioController(ILogin cuLogin, ICambioContrasenia cambioContrasenia)
        {
            CULogin = cuLogin;
            CUCambioContrasenia = cambioContrasenia;
        }


        /// <summary>
        /// Permite loguearse en el sistema y obtiene un token JWT válido.
        /// </summary>
        /// <param name="usuarioDTO">Datos de acceso: email y contraseña.</param>
        /// <returns>Objeto <see cref="UsuarioLogueadoDTO"/> que incluye información del usuario y su token.</returns>
        /// <response code="200">Login exitoso. Devuelve datos del usuario y token.</response>
        /// <response code="400">Datos de entrada inválidos o credenciales incorrectas.</response>
        /// <response code="404">Usuario no encontrado.</response>
        /// <response code="500">Error interno del servidor.</response>
        [ProducesResponseType(typeof(UsuarioLogueadoDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("/api/Usuario/Login")]
        public IActionResult Login(UsuarioDTO usuarioDTO)
        {
            try
            {
                if (usuarioDTO == null)
                {
                    return BadRequest("Datos incorrectos");
                }
                UsuarioLogueadoDTO usuarioLogueado = CULogin.Ejecutar(usuarioDTO);
                if (usuarioLogueado != null)
                {
                    usuarioLogueado.Token = ManejadorToken.GenerarToken(usuarioLogueado);
                    return Ok(usuarioLogueado);
                }
                else
                {
                    return BadRequest("Datos incorrectos");
                }
            }
            catch (UsuarioException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error");
            }
        }

        /// <summary>
        /// Permite al cliente autenticado actualizar su contraseña.
        /// </summary>
        /// <param name="cambioDTO">DTO que contiene la contraseña actual y la nueva contraseña.</param>
        /// <returns>Confirma que el cambio fue realizado con exito.</returns>
        /// <response code="200">Contraseña cambiada correctamente.</response>
        /// <response code="400">Datos de entrada inválidos.</response>
        /// <response code="404">Usuario no encontrado.</response>
        /// <response code="500">Error interno del servidor.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("/api/Usuario/CambiarPassword")]
        [Authorize(Roles = "Cliente")]
        public IActionResult CambiarContrasenia(CambioContraseniaDTO cambioDTO)
        {
            try
            {
                if (cambioDTO == null)
                {
                    return BadRequest("Datos incorrectos");
                }
                CUCambioContrasenia.Ejecutar(cambioDTO);
                return Ok();
            }
            catch (UsuarioException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error");
            }
        }

    }

}
