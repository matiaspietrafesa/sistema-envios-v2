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
    public class Password
    {
        public string Valor { get; set; }

        public Password(string valor)
        {
            Valor = valor;
            Validar();
        }

        private void Validar()
        {
            char[] digitos = { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
            if (string.IsNullOrEmpty(Valor))
            {
                throw new UsuarioException("La contraseña no puede estar vacio.");
            }
            if (Valor.Length < 8)
            {
                throw new UsuarioException("La contraseña debe tener mas de 8 caracteres.");
            }
            if (Valor.Length > 30)
            {
                throw new UsuarioException("La contraseña no puede tener mas de 30 caracteres.");
            }
            if (Valor.IndexOfAny(digitos) == -1)
            {
                throw new UsuarioException("La contraseña debe contener al menos un digito.");
            }
        }
    }
}
