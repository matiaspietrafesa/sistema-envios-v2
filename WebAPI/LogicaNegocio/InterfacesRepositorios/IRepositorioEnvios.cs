using LogicaNegocio.Entidades;
using LogicaNegocio.EntidadesNegocio;
using LogicaNegocio.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.InterfacesRepositorios
{
    public interface IRepositorioEnvios : IRepositorio<Envio>
    {
        Envio FindByNroTracking(int nroTracking);
        IEnumerable<Envio> EnviosPorEmail(string email);
        IEnumerable<Envio> EnviosEntreFechas(string email, DateTime desde, DateTime hasta, Estado? estado);
        IEnumerable<Envio> EnviosConPalabra(string email, string palabra);
        IEnumerable<Comentario> ObtenerComentarios(string email, int idEnvio);
    }
}
