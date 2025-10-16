using Compartido.DTOs.Envios;
using Compartido.Mappers;
using LogicaAplicacion.InterfacesCasosUso.Envios;
using LogicaNegocio.Entidades;
using LogicaNegocio.ExcepcionesEntidades;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.ImplementacionCasosUso.Envios
{
    public class ObtenerPorNroTracking : IObtenerPorNroTracking
    {
        private IRepositorioEnvios RepoEnvios { get; set; }

        public ObtenerPorNroTracking(IRepositorioEnvios repoEnvios)
        {
            RepoEnvios = repoEnvios;
        }

        public DetalleEnvioDTO Ejecutar(int nro)
        {
            Envio e = RepoEnvios.FindByNroTracking(nro);
            if (e == null)
            {
                throw new EnvioException("No se encontro un envio con ese numero de tracking");
            }
            return EnvioMapper.DetalleEnvioDTOFromEnvio(e);
        }
    }
}
