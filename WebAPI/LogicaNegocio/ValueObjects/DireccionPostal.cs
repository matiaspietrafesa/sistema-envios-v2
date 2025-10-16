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
    public class DireccionPostal
    {
        public string Valor {  get; set; }

        public DireccionPostal(string valor)
        {
            Valor = valor;
        }
    }
}
