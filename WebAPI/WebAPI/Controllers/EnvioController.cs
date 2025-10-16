using LogicaAplicacion.ImplementacionCasosUso.Usuarios;
using LogicaAplicacion.InterfacesCasosUso.Envios;
using LogicaNegocio.Entidades;
using LogicaNegocio.EntidadesNegocio;
using LogicaNegocio.ExcepcionesEntidades;
using LogicaNegocio.ValueObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860


namespace WebAPI.Controllers
{
    /// <summary>
    /// Maneja las consultas sobre envios.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class EnvioController : ControllerBase
    {
        private IObtenerPorNroTracking ObtenerPorNroTrackingCU {  get; set; }

        private IListarEnviosDeCliente EnviosDeCliente { get; set; }
        private IObtenerComentarios ObtenerComentariosCU { get; set; }
        private IListarEnviosPorFecha ListarEnviosPorFechaCU {  get; set; }
        private IListarEnviosPorComentario ListarEnviosPorComentarioCU {  get; set; }

        public EnvioController(IObtenerPorNroTracking obtenerPorNroTrackingCU, IListarEnviosDeCliente enviosDeCliente, IObtenerComentarios obtenerComentariosCU, IListarEnviosPorFecha listarEnviosPorFechaCU, IListarEnviosPorComentario listarEnviosPorComentarioCU)
        {
            ObtenerPorNroTrackingCU = obtenerPorNroTrackingCU;

            EnviosDeCliente = enviosDeCliente;
            ObtenerComentariosCU = obtenerComentariosCU;
            ListarEnviosPorFechaCU = listarEnviosPorFechaCU;
            ListarEnviosPorComentarioCU = listarEnviosPorComentarioCU;
        }


        /// <summary>
        /// Obtiene un envío por su número de tracking.
        /// </summary>
        /// <param name="codTracking">Número de tracking del envío (debe ser mayor que 0).</param>
        /// <returns>Objeto correspondiente al número de tracking especificado.</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{codTracking:int}")]
        public IActionResult Get(int codTracking)
        {
            try
            {
                if (codTracking <= 0)
                {
                    return BadRequest("El codigo recibido no es correcto");
                }
                return Ok(ObtenerPorNroTrackingCU.Ejecutar(codTracking));

            }
            catch (EnvioException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno" + ex.Message);
            }
        }

        /// <summary>
        /// Lista todos los envíos asociados a un cliente.
        /// </summary>
        /// <param name="email">Email del cliente (no puede ser nulo ni vacío).</param>
        /// <returns>Lista de los envios del cliente indicado.</returns>
        [ProducesResponseType(typeof(IEnumerable<Envio>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("email{email}", Name = "EnviosDeCliente")]
        [Authorize(Roles = "Cliente")]
        public IActionResult Get(string email)
        {
            try
            {
                if (string.IsNullOrEmpty(email))
                {
                    return BadRequest("El codigo recibido no es correcto");
                }
                return Ok(EnviosDeCliente.Ejecutar(email));

            }
            catch (EnvioException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno" + ex.Message);
            }
        }

        /// <summary>
        /// Obtiene los comentarios de un envío específico de un cliente.
        /// </summary>
        /// <param name="email">Email del cliente (no puede ser nulo ni vacío).</param>
        /// <param name="idEnvio">Identificador del envío (debe ser mayor que 0).</param>
        /// <returns>Lista de los comentarios asociados al envío.</returns>
        [ProducesResponseType(typeof(IEnumerable<Comentario>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("email/{email}envio/{idEnvio}")]
        [Authorize(Roles = "Cliente")]
        public IActionResult Get(string email, int idEnvio)
        {
            try
            {
                if (string.IsNullOrEmpty(email) || idEnvio <= 0)
                {
                    return BadRequest("Los datos pasados no son correctos.");
                }
                return Ok(ObtenerComentariosCU.Ejecutar(email,idEnvio));

            }
            catch (EnvioException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno" + ex.Message);
            }
        }

        /// <summary>
        /// Lista los envíos de un cliente dentro de un rango de fechas y opcionalmente por estado.
        /// </summary>
        /// <param name="email">Email del cliente (no puede ser nulo ni vacío).</param>
        /// <param name="desde">Fecha de inicio del rango.</param>
        /// <param name="hasta">Fecha de fin del rango.</param>
        /// <param name="estado">Estado del envío (opcional).</param>
        /// <returns>Lista de los envios que cumplen los criterios.</returns>
        [ProducesResponseType(typeof(IEnumerable<Envio>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("email/{email}desde/{desde}hasta/{hasta}estado/{estado}")]
        [Authorize(Roles = "Cliente")]
        public IActionResult Get(string email, DateTime desde, DateTime hasta, string? estado)
        {
            try
            {
                if (string.IsNullOrEmpty(email) || desde == null || hasta == null)
                {
                    return BadRequest("Los datos pasados no son correctos.");
                }
                return Ok(ListarEnviosPorFechaCU.Ejecutar(email, desde, hasta, estado));

            }
            catch (EnvioException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno" + ex.Message);
            }
        }


        /// <summary>
        /// Lista los envíos de un cliente cuyos comentarios contienen una palabra clave.
        /// </summary>
        /// <param name="email">Email del cliente (no puede ser nulo ni vacío).</param>
        /// <param name="palabra">Palabra clave para filtrar comentarios (no puede ser nula ni vacía).</param>
        /// <returns>Lista de envios cuyos comentarios incluyen la palabra clave.</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        [HttpGet("email/{email}palabra/{palabra}")]
        [Authorize(Roles = "Cliente")]
        public IActionResult Get(string email, string palabra)
        {
            try
            {
                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(palabra))
                {
                    return BadRequest("Los datos pasados no son correctos.");
                }
                return Ok(ListarEnviosPorComentarioCU.Ejecutar(email, palabra));

            }
            catch (EnvioException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno" + ex.Message);
            }
        }
    }
}
