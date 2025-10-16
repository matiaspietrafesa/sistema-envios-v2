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
    public class ListarEnviosPorComentario : IListarEnviosPorComentario
    {
        private IRepositorioEnvios RepoEnvios { get; set; }

        public ListarEnviosPorComentario(IRepositorioEnvios repoEnvios)
        {
            RepoEnvios = repoEnvios;
        }
        public IEnumerable<EnvioClienteDTO> Ejecutar(string email, string palabra)
        {
            ArgumentNullException.ThrowIfNull(email);
            ArgumentNullException.ThrowIfNull(palabra);
            IEnumerable<Envio> envios = RepoEnvios.EnviosConPalabra(email, palabra);
            if (envios.Count() == 0)
            {
                throw new EnvioException("No se encontraron envios de ese cliente.");
            }
            return EnvioMapper.ListEnvioClienteDTOFromListEnvio(envios);
        }
    }
}
