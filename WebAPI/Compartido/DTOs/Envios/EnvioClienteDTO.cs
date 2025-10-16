using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compartido.DTOs.Envios
{
    public class EnvioClienteDTO
    {
        public int Id { get; set; }
        public int NroTracking { get; set; }
        public int Peso { get; set; }
        public string Fecha { get; set; }
        public string Estado { get; set; }
    }
}
