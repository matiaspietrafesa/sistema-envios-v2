using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Entidades
{
    public class Auditoria
    {
        public int Id { get; set; }
        public string Accion {  get; set; }
        public DateTime Fecha { get; set; }
        public Usuario Usuario { get; set; }
        [ForeignKey("Usuario")]
        public int UsuarioId { get; set; }

        public Auditoria() { }

        public Auditoria(string accion, int usuarioId)
        {
            Accion = accion;
            Fecha = DateTime.Now;
            UsuarioId = usuarioId;
        }
    }
}
