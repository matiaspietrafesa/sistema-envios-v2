using LogicaNegocio.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compartido.DTOs.Envios
{
    public class DetalleEnvioDTO
    {
        public int Id { get; set; }
        public int NroTracking { get; set; }
        public int EmpleadoId { get; set; }
        public int ClienteId { get; set; }
        public int Peso { get; set; }
        public string Estado { get; set; }
        public List<DetalleComentarioDTO> Comentarios { get; set; } = new List<DetalleComentarioDTO>();
    }
}
