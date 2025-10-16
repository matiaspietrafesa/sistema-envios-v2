using Compartido.DTOs.Envios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.InterfacesCasosUso.Envios
{
    public interface IListarEnviosPorFecha
    {
        IEnumerable<EnvioClienteDTO> Ejecutar(string email, DateTime desde, DateTime hasta, string? estado);
    }
}
