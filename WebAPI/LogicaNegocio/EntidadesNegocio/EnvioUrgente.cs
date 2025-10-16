using LogicaNegocio.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Entidades
{
    public class EnvioUrgente : Envio
    {
        public DireccionPostal Destino { get; set; }
        public int Eficiencia { get; set; }
        public DateTime Salida { get; set; }
        public DateTime Entrega { get; set; }

        public EnvioUrgente() { }

        public EnvioUrgente(int clienteId, int peso, int empleadoId, string destino) : base(clienteId, peso, empleadoId)
        {
            Destino = new DireccionPostal(destino);
            Salida = DateTime.Now;
        }

        public void Finalizar()
        {
            Entrega = DateTime.Now;
            CalcularEficiencia();
            base.Finalizar();
        }
        
        private void CalcularEficiencia()
        {
            TimeSpan diferencia = Entrega - Salida;
            int horas = diferencia.Hours;
            if (horas <= 24)
            {
                Eficiencia = 5;
            }
            else if (horas <= 28)
            {
                Eficiencia = 4;
            }
            else if (horas <= 32)
            {
                Eficiencia = 3;
            }
            else if (horas <= 36)
            {
                Eficiencia = 2;
            } else
            {
                Eficiencia = 1;
            }
        }
    }
}
