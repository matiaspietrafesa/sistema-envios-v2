using Compartido.DTOs.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.InterfacesCasosUso.Usuarios
{
    public interface ICambioContrasenia
    {
        public void Ejecutar(CambioContraseniaDTO cambioDTO);
    }
}
