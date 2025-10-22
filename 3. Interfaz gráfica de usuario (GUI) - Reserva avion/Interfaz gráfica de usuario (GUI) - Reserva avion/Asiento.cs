using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaz_gráfica_de_usuario__GUI____Reserva_avion
{
    public class Asiento
    {
        // Propiedades
        public string NumeroAsiento { get; set; }
        public bool EstaReservado { get; set; } // true si está ocupado, false si está libre
        public string NombrePasajero { get; set; }
        public string ApellidoPasajero { get; set; }

        // Constructor para inicializar el asiento
        public Asiento(string numero)
        {
            NumeroAsiento = numero;
            EstaReservado = false; // Por defecto, libre
            NombrePasajero = "";
            ApellidoPasajero = "";
        }
    }
}