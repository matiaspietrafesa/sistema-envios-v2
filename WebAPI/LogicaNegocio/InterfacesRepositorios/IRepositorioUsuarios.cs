using LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.InterfacesRepositorios
{
    public interface IRepositorioUsuarios : IRepositorio<Usuario>
    {
        Usuario FindByEmail(string email);

        Usuario FindByEmailAndPassword(string email, string password);
        public void ActualizarPassword(Usuario usuario, string NuevaPassword);
    }
}
