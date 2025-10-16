using Compartido.DTOs.Envios;
using Compartido.Mappers;
using LogicaAplicacion.InterfacesCasosUso.Envios;
using LogicaNegocio.EntidadesNegocio;
using LogicaNegocio.ExcepcionesEntidades;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.ImplementacionCasosUso.Envios
{
    public class ObtenerComentarios : IObtenerComentarios
    {
        private IRepositorioEnvios RepoEnvios { get; set; }

        public ObtenerComentarios(IRepositorioEnvios repoEnvios)
        {
            RepoEnvios = repoEnvios;
        }

        public IEnumerable<ComentarioDTO> Ejecutar(string email, int idEnvio)
        {
            ArgumentNullException.ThrowIfNull(email);
            ArgumentNullException.ThrowIfNull(idEnvio);
            IEnumerable<Comentario> comentarios = RepoEnvios.ObtenerComentarios(email, idEnvio);
            if (comentarios == null)
            {
                throw new EnvioException("No se encontraron los comentarios del envio.");
            }
            return ComentarioMapper.ListComentarioDTOFromListComentario(comentarios);
        }
    }
}
