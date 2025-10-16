using LogicaNegocio.Entidades;
using LogicaNegocio.ExcepcionesEntidades;
using LogicaNegocio.InterfacesRepositorios;
using LogicaNegocio.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAccesoDatos.Repositorios
{
    public class RepositorioUsuarios : IRepositorioUsuarios
    {
        private Contexto Contexto { get; set; }

        public RepositorioUsuarios(Contexto contexto)
        {
            Contexto = contexto;
        }

        public void Add(Usuario item)
        {
            Usuario usuario = FindByEmail(item.Email.Valor);
            if (usuario == null)
            {
                Contexto.Usuarios.Add(item);
                Contexto.SaveChanges();
            } else
            {
                throw new UsuarioException("Ya hay un usuario con ese email en el sistema.");
            }
        }

        public void Delete(int id)
        {
            Usuario user = FindById(id);
            if (user != null)
            {
                Contexto.Usuarios.Remove(user);
                Contexto.SaveChanges();
            } else
            {
                throw new UsuarioException("No existe un usuario con ese id.");
            }
        }

        public IEnumerable<Usuario> FindAll()
        {
            return Contexto.Usuarios.Where(e => e.RolId == (int)Roles.Funcionario);
        }

        public Usuario FindById(int id)
        {
            return Contexto.Usuarios.Where(u => u.Id == id).SingleOrDefault();
        }

        public void Update(Usuario item)
        {
            Usuario user = FindById(item.Id);
            if (user != null)
            {
                Contexto.ChangeTracker.Clear();
                Contexto.Usuarios.Update(item);
                Contexto.SaveChanges();
            }
            else
            {
                throw new UsuarioException("No existe un usuario con ese id.");
            }
        }

        public Usuario FindByEmail(string email)
        {
            return Contexto.Usuarios.Include(u => u.Rol).Where(u => u.Email.Valor == email).SingleOrDefault();
        }

        public Usuario FindByEmailAndPassword(string email, string password)
        {
            return Contexto.Usuarios.Where(u => u.Email.Valor.Equals(email) && u.Password.Valor.Equals(password)).Include("Rol").SingleOrDefault();
        }

        public void ActualizarPassword(Usuario usuario, string NuevaPassword)
        {
            usuario.Password.Valor = NuevaPassword;
            Contexto.SaveChanges();
        }

    }
}
