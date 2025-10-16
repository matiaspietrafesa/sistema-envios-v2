using LogicaNegocio.ExcepcionesEntidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.ValueObjects
{
    [Owned]
    public class Coordenada
    {
        public float Latitud {  get; set; }
        public float Longitud { get; set; }

        public Coordenada(float latitud, float longitud)
        {
            Latitud = latitud;
            Longitud = longitud;
            Validar();
        }

        public void Validar()
        {
            if (Latitud < -90 || Latitud > 90)
            {
                throw new AgenciaException("La latitud debe estar entre -90 y 90.");
            }
            if (Longitud < -180 || Longitud > 180)
            {
                throw new AgenciaException("La longitud debe estar entre -180 y 180.");
            }
        }
    }
}
