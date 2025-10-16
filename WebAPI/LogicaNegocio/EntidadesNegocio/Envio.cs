using LogicaNegocio.EntidadesNegocio;
using LogicaNegocio.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Entidades
{
    public class Envio
    {
        public int Id { get; set; }
        public NroTracking NroTracking { get; set; }
        public Usuario Empleado { get; set; }
        [ForeignKey("Empleado")]
        public int EmpleadoId { get; set; }
        public Usuario Cliente { get; set; }
        [ForeignKey("Cliente")]
        public int ClienteId { get; set; }
        public Peso Peso { get; set; }
        public Estado Estado { get; set; }
        public List<Comentario> Comentarios { get; set; } = new List<Comentario>();

        public DateTime Fecha {  get; set; }

        public Envio() { }

        public Envio(int clienteId, int peso, int empleadoId)
        {
            ClienteId = clienteId;
            Peso = new Peso(peso);
            EmpleadoId = empleadoId;
            Estado = Estado.EN_PROCESO;
        }

        public virtual void Finalizar()
        {
            Estado = Estado.FINALIZADO;
        }
    }
}
