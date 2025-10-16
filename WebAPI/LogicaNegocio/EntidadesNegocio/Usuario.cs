using LogicaNegocio.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Entidades
{
    public class Usuario
    {
        public int Id { get; set; }
        public Nombre Nombre { get; set; }
        public Apellido Apellido { get; set; }
        public Email Email { get; set; }
        public Password Password { get; set; }
        public Rol Rol { get; set; }
        [ForeignKey("Rol")]
        public int RolId { get; set; }

        public Usuario() { }

        public Usuario(string nombre, string apellido, string email, string passowrd, int rolId)
        {
            Nombre = new Nombre(nombre);
            Apellido = new Apellido(apellido);
            Email = new Email(email);
            Password = new Password(passowrd);
            RolId = rolId;
        }

        public bool VerificarPassword(string password)
        {
            return Password.Valor.Equals(password);
        }

    }
}
