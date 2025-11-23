using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppAcmafer.Modelo
{
    public class EvidenciaTarea
    {
        public int IdEvidenciaTarea { get; set; }
        public DateTime FechaSubida { get; set; }
        public int IdAsignacionTarea { get; set; }
        public string InformeTrabajo { get; set; }

        // Navegación
        public AsignacionTarea AsignacionTarea { get; set; }

        // Constructor
        public EvidenciaTarea()
        {
            IdEvidenciaTarea = 0;
            FechaSubida = DateTime.Now;
            IdAsignacionTarea = 0;
            InformeTrabajo = string.Empty;
            AsignacionTarea = null;
        }
    }
}