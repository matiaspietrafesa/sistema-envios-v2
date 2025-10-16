using Compartido.DTOs.Envios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.InterfacesCasosUso.Envios
{
    public interface IObtenerPorNroTracking
    {
        DetalleEnvioDTO Ejecutar(int nro);
    }
}
