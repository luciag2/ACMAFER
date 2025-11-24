using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppAcmafer.Modelo
{
    public class ComentarioTarea
    {
        public int IdComentarioTarea { get; set; }
        public DateTime FechaComentario { get; set; }
        public string Comentario { get; set; }
        public int IdAsignacionTarea { get; set; }

        // Navegación
        public AsignacionTarea AsignacionTarea { get; set; }

        // Constructor
        public ComentarioTarea()
        {
            IdComentarioTarea = 0;
            FechaComentario = DateTime.Now;
            Comentario = string.Empty;
            IdAsignacionTarea = 0;
            AsignacionTarea = null;
        }
    }
}