using LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.EntidadesNegocio
{
    public class Comentario
    {
        public int Id { get; set; }
        public string Texto {  get; set; }
        public DateTime Fecha { get; set; }
        public Usuario Empleado { get; set; }
        [ForeignKey("Empleado")]
        public int EmpleadoId { get; set; }

        public Comentario() { }

        public Comentario(string texto, int empleadoId)
        {
            Texto = texto;
            EmpleadoId = empleadoId;
            Fecha = DateTime.Now;
        }
    }
}
