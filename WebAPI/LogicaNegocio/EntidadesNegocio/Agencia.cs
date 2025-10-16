using LogicaNegocio.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Entidades
{
    public class Agencia
    {
        public int Id { get; set; }
        public Nombre Nombre { get; set; }
        public DireccionPostal Direccion { get; set; }
        public Coordenada Ubicacion { get; set; }

        public Agencia() { }

        public Agencia(string nombre, string direccion, int lat, int lon)
        {
            Nombre = new Nombre(nombre);
            Direccion = new DireccionPostal(direccion);
            Ubicacion = new Coordenada(lat, lon);
        }
    }
}
