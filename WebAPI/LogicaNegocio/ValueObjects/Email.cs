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
    public class Email
    {
        public string Valor { get; set; }

        public Email(string valor)
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
            if (Valor.IndexOf(" ") != -1) 
            {
                throw new UsuarioException("El mail no puede tener espacios.");
            }
            if (Valor.IndexOf("@") == -1 || Valor.IndexOf("@") != Valor.LastIndexOf("@"))
            {
                throw new UsuarioException("El mail debe tener un arroba.");
            }
            if (Valor.IndexOf(".") == -1)
            {
                throw new UsuarioException("El mail debe tener un punto.");
            }
        }
    }
}
