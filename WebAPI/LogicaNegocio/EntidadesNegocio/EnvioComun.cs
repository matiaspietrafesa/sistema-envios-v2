using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Entidades
{
    public class EnvioComun : Envio
    {
        public Agencia Agencia { get; set; }
        [ForeignKey("Agencia")]
        public int AgenciaId { get; set; }

        public EnvioComun() { }

        public EnvioComun(int clienteId, int peso, int empleadoId, int agenciaId) : base(clienteId, peso, empleadoId)
        {
            AgenciaId = agenciaId;
        }
    }
}
