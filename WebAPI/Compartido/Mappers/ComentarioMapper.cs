using Compartido.DTOs.Envios;
using LogicaNegocio.Entidades;
using LogicaNegocio.EntidadesNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compartido.Mappers
{
    public class ComentarioMapper
    {
        public static IEnumerable<ComentarioDTO> ListComentarioDTOFromListComentario(IEnumerable<Comentario> comentarios)
        {
            return comentarios.Select(c => new ComentarioDTO()
            {
                Id = c.Id,
                Texto = c.Texto,
                Fecha = c.Fecha
            });
        }
    }
}
