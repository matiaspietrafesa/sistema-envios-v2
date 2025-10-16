using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.ValueObjects
{
    [Owned]
    public class NroTracking
    {
        public int Valor {  get; set; }

        public NroTracking(int valor)
        {
            Valor = valor;
        }
    }
}
