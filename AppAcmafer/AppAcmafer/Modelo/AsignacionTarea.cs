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
        public string TituloTarea { get; set; }
        public int IdEmpleado { get; set; }
        public string NombreEmpleado { get; set; }
        public int IdAdmin { get; set; }
        public string NombreAdmin { get; set; }
        public DateTime FechaInicio { get; set; }
        public string HoraInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string HoraFin { get; set; }
        public string ComentarioAdmin { get; set; }

        // PROPIEDADES NUEVAS QUE FALTABAN
        public string DescripcionTarea { get; set; }
        public string Prioridad { get; set; }
        public string EstadoTarea { get; set; }

        // Navegación
        public Tarea Tarea { get; set; }
        public Usuario Empleado { get; set; }
        public Usuario Admin { get; set; }

        // Constructor
        public AsignacionTarea()
        {
            IdAsignacionTarea = 0;
            IdTarea = 0;
            IdEmpleado = 0;
            IdAdmin = 0;
            FechaInicio = DateTime.Now;
            HoraInicio = string.Empty;
            FechaFin = DateTime.Now;
            HoraFin = string.Empty;
            ComentarioAdmin = string.Empty;
            DescripcionTarea = string.Empty;
            Prioridad = string.Empty;
            EstadoTarea = string.Empty;
            Tarea = null;
            Empleado = null;
            Admin = null;
        }
    }
}