using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.ExcepcionesEntidades
{
    public class EnvioException : Exception
    {
        public EnvioException() { }

        public EnvioException(string message) : base(message) { }

        public EnvioException(string message, Exception innerException) : base(message, innerException) { }
    }
}
