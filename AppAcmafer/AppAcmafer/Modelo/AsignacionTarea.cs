using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppAcmafer.Modelo
{
    [Serializable]
    public class AsignacionTarea
    {
        public int IdAsignacionTarea { get; set; }
        public int IdTarea { get; set; }
        public int IdEmpleado { get; set; }
        public int IdAdmin { get; set; }
        public DateTime FechaInicio { get; set; }
        public string HoraInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string HoraFin { get; set; }
        public string ComentarioAdmin { get; set; }

        // Propiedades adicionales para mostrar
        public string TituloTarea { get; set; }
        public string NombreEmpleado { get; set; }
        public string NombreAdmin { get; set; }
    }
}