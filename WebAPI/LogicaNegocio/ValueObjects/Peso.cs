using LogicaNegocio.ExcepcionesEntidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.ValueObjects
{
    [Owned]
    public class Peso
    {
        public int Valor {  get; set; }

        public Peso(int valor)
        {
            Valor = valor;
            Validar();
        }

        private void Validar()
        {
            if (Valor <= 0)
            {
                throw new EnvioException("El peso debe ser mayor que cero.");
            }
        }
    }
}
