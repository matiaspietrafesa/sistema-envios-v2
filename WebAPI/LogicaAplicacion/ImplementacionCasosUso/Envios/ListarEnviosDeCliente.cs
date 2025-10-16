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
    public class ListarEnviosDeCliente : IListarEnviosDeCliente
    {
        private IRepositorioEnvios RepoEnvios { get; set; }

        public ListarEnviosDeCliente(IRepositorioEnvios repoEnvios)
        {
            RepoEnvios = repoEnvios;
        }

        public IEnumerable<EnvioClienteDTO> Ejecutar(string email)
        {
            ArgumentNullException.ThrowIfNull(email);
            IEnumerable<Envio> envios = RepoEnvios.EnviosPorEmail(email);
            if (envios == null)
            {
                throw new EnvioException("No se encontraron envios de ese cliente.");
            }
            return EnvioMapper.ListEnvioClienteDTOFromListEnvio(envios);
        }
    }
}
