using Compartido.DTOs.Envios;
using LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.InterfacesCasosUso.Envios
{
    public interface IListarEnviosDeCliente
    {
        IEnumerable<EnvioClienteDTO> Ejecutar(string email);
    }
}
