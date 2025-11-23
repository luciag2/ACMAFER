using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace AppAcmafer.Modelo
{
    [Serializable]
    public class Tarea
    {
        public int IdTarea { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string Prioridad { get; set; }
        public string Estado { get; set; }

        // Constructor
        public Tarea()
        {
            IdTarea = 0;
            Titulo = string.Empty;
            Descripcion = string.Empty;
            Prioridad = "Media";
            Estado = "Pendiente";
        }
    }
}
