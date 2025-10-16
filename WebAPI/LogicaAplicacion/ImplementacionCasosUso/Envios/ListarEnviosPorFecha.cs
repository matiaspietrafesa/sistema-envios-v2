using Compartido.DTOs.Envios;
using Compartido.Mappers;
using LogicaAplicacion.InterfacesCasosUso.Envios;
using LogicaNegocio.Entidades;
using LogicaNegocio.ExcepcionesEntidades;
using LogicaNegocio.InterfacesRepositorios;
using LogicaNegocio.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.ImplementacionCasosUso.Envios
{
    public class ListarEnviosPorFecha : IListarEnviosPorFecha
    {
        private IRepositorioEnvios RepoEnvios {  get; set; }

        public ListarEnviosPorFecha(IRepositorioEnvios repoEnvios)
        {
            RepoEnvios = repoEnvios;
        }

        public IEnumerable<EnvioClienteDTO> Ejecutar(string email, DateTime desde, DateTime hasta, string? estado)
        {
            if (string.IsNullOrEmpty(email) || desde == null || hasta == null)
            {
                throw new ArgumentNullException("Los datos son incorrectos.");
            }
            IEnumerable<Envio> envios;
            if (estado == "FINALIZADO" || estado == "EN_PROCESO")
            {
                Estado state = estado == "FINALIZADO" ? Estado.FINALIZADO : Estado.EN_PROCESO;
                envios = RepoEnvios.EnviosEntreFechas(email, desde, hasta, state);
            } else
            {
                envios = RepoEnvios.EnviosEntreFechas(email, desde, hasta, null);
            }

            if (envios.Count() == 0)
            {
                throw new EnvioException("No se encontraron envios entre esas fechas.");
            }
            IEnumerable<EnvioClienteDTO> dtos = EnvioMapper.ListEnvioClienteDTOFromListEnvio(envios);
            return dtos;
        }
    }
}
