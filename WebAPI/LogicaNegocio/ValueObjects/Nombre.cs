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
    public class Nombre
    {
        public string Valor { get; set; }

        public Nombre(string valor)
        {
            Valor = valor;
            Validar();
        }

        private void Validar()
        {
            if (string.IsNullOrEmpty(Valor))
            {
                throw new UsuarioException("El nombre no puede estar vacio.");
            }
            if (Valor.Length < 2)
            {
                throw new UsuarioException("El nombre debe tener mas de 2 caracteres.");
            }
            if (Valor.Length > 30)
            {
                throw new UsuarioException("El nombre no puede tener mas de 30 caracteres.");
            }
        }
    }
}
