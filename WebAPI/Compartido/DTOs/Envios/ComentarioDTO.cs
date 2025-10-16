using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compartido.DTOs.Envios
{
    public class ComentarioDTO
    {
        public int Id { get; set; }
        public string Texto { get; set; }
        public int CreadorId { get; set; }
        public DateTime ? Fecha { get; set; }
    }
}
