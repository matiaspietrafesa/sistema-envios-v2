using Compartido.DTOs.Envios;
using LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compartido.Mappers
{
    public class EnvioMapper
    {
        
        public static IEnumerable<EnvioClienteDTO> ListEnvioClienteDTOFromListEnvio(IEnumerable<Envio> envios)
        {
            return envios.Select(e => new EnvioClienteDTO()
            {
                Id = e.Id,
                NroTracking = e.NroTracking.Valor,
                Peso = e.Peso.Valor,
                Estado = e.Estado.ToString(),
                Fecha = e.Fecha.ToShortDateString()
            });
        }

        public static DetalleEnvioDTO DetalleEnvioDTOFromEnvio(Envio e)
        {
            return new DetalleEnvioDTO()
            {
                Id = e.Id,
                ClienteId = e.Cliente.Id,
                EmpleadoId = e.Empleado.Id,
                Peso = e.Peso.Valor,
                NroTracking = e.NroTracking.Valor,
                Estado = e.Estado.ToString(),
                Comentarios = e.Comentarios?.Select(c => new DetalleComentarioDTO
                {
                    Id = c.Id,
                    Texto = c.Texto,
                    Email = c.Empleado.Email.Valor,
                    Fecha = c.Fecha
                }).ToList() ?? new List<DetalleComentarioDTO>()
            };
        }
    }
}
