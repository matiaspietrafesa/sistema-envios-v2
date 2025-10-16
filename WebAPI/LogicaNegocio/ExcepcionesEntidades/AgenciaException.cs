using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.ExcepcionesEntidades
{
    public class AgenciaException : Exception
    {
        public AgenciaException() { }
        public AgenciaException(string message) : base(message) { }

        public AgenciaException(string message, Exception innerException) : base(message, innerException) { }
    }
}
