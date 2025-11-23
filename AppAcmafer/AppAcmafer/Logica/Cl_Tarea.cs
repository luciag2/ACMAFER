using AppAcmafer.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppAcmafer.Logica
{
    public class Cl_Tarea
    
    {
        // Método para VALIDAR fechas
        public bool ValidarFechas(DateTime fechaInicio, DateTime fechaFin, out string mensaje)
        {
            mensaje = string.Empty;

            if (fechaInicio > fechaFin)
            {
                mensaje = "La fecha de inicio no puede ser mayor a la fecha fin";
                return false;
            }

            if (fechaInicio < DateTime.Now.Date)
            {
                mensaje = "La fecha de inicio no puede ser anterior a hoy";
                return false;
            }

            return true;
        }

        // Método para VALIDAR prioridad
        public bool ValidarPrioridad(string prioridad)
        {
            string[] prioridadesValidas = { "Alta", "Media", "Baja" };
            return Array.Exists(prioridadesValidas, p => p == prioridad);
        }

        // Método para VALIDAR datos de tarea
        public bool ValidarDatosTarea(Tarea tarea, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrEmpty(tarea.Titulo))
            {
                mensaje = "El título es obligatorio";
                return false;
            }

            if (string.IsNullOrEmpty(tarea.Descripcion))
            {
                mensaje = "La descripción es obligatoria";
                return false;
            }

            if (string.IsNullOrEmpty(tarea.Prioridad))
            {
                mensaje = "La prioridad es obligatoria";
                return false;
            }

            if (!ValidarPrioridad(tarea.Prioridad))
            {
                mensaje = "La prioridad debe ser: Alta, Media o Baja";
                return false;
            }

            return true;
        }

        // Método para VALIDAR asignación de tarea
        public bool ValidarAsignacionTarea(AsignacionTarea asignacion, out string mensaje)
        {
            mensaje = string.Empty;

            if (asignacion.IdTarea == 0)
            {
                mensaje = "Debe seleccionar una tarea";
                return false;
            }

            if (asignacion.IdEmpleado == 0)
            {
                mensaje = "Debe seleccionar un empleado";
                return false;
            }

            if (!ValidarFechas(asignacion.FechaInicio, asignacion.FechaFin, out mensaje))
            {
                return false;
            }

            return true;
        }
    }
}