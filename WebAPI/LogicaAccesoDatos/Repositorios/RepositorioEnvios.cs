using LogicaNegocio.Entidades;
using LogicaNegocio.EntidadesNegocio;
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
    public class RepositorioEnvios : IRepositorioEnvios
    {
        private Contexto Contexto { get; set; }

        public RepositorioEnvios(Contexto contexto)
        {
            Contexto = contexto;
        }

        public void Add(Envio item)
        {
            Envio e = FindById(item.Id);
            if (e != null)
            {
                throw new EnvioException("Ya existe un envio con ese numero de tracking.");
            }
            Contexto.Envios.Add(item);
            Contexto.SaveChanges();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Envio> FindAll()
        {
            return Contexto.Envios.Include(e => e.Cliente).Include(e => e.Empleado).Where(e => e.Estado == LogicaNegocio.ValueObjects.Estado.EN_PROCESO);
        }

        public Envio FindById(int id)
        {
            return Contexto.Envios.Include(e => e.Cliente).Include(e => e.Empleado).Where(e => e.Id == id).SingleOrDefault();
        }

        public void Update(Envio item)
        {
            Envio e = FindById(item.Id);
            if (e != null)
            {
                Contexto.Envios.Update(item);
                Contexto.SaveChanges();
            }
            else
            {
                throw new EnvioException("No se encontro un envio con ese id");
            }
        }

        public Envio FindByNroTracking(int nroTracking)
        {
            return Contexto.Envios.Include(e => e.Cliente).Include(e => e.Empleado).Include(e => e.Comentarios).ThenInclude(c => c.Empleado).Where(e => e.NroTracking.Valor == nroTracking).SingleOrDefault();
        }


        public IEnumerable<Envio> EnviosPorEmail(string email)
        {
            // agregar ordenado por fecha
            return Contexto.Envios.Where(e => e.Cliente.Email.Valor.Equals(email)).OrderBy(e => e.Fecha);
        }

        public IEnumerable<Comentario> ObtenerComentarios(string email, int idEnvio)
        {
            return Contexto.Envios.Where(e => e.Cliente.Email.Valor.Equals(email) && e.Id == idEnvio).Include(e => e.Comentarios).SelectMany(e => e.Comentarios);
        }

        public IEnumerable<Envio> EnviosEntreFechas(string email, DateTime desde, DateTime hasta, Estado? estado)
        {
            IEnumerable<Envio> envios;
            if (estado != null)
            {
                envios = Contexto.Envios.Where(e => e.Cliente.Email.Valor == (email)
                        && e.Fecha >= desde && e.Fecha <= hasta && e.Estado == estado)
                    .Include(e => e.Cliente).Include(e => e.Empleado).OrderBy(e => e.NroTracking.Valor);
            }
            else
            {
                envios = Contexto.Envios.Where(e => e.Cliente.Email.Valor == (email)
                        && e.Fecha >= desde && e.Fecha <= hasta).Include(e => e.Cliente)
                        .Include(e => e.Empleado).OrderBy(e => e.NroTracking.Valor);
            }
            return envios;
        }

        public IEnumerable<Envio> EnviosConPalabra(string email, string palabra)
        {
            return Contexto.Envios.Where(e => e.Cliente.Email.Valor.Equals(email))
                .Include(e => e.Comentarios).Where(e => e.Comentarios.Any(c => c.Texto.Contains(palabra)))
                .OrderBy(e => e.Fecha);
        }
    }
}
