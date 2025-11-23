using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppAcmafer.Modelo
{
    
    public class Rol
    {
        public int IdRol { get; set; }
        public string NombreRol { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Estado { get; set; }
        

        // Constructor vacío
        public Rol()
        {
            FechaCreacion = DateTime.Now;
            Estado = "Activo";
        }

        // Constructor con parámetros
        public Rol(int idRol, string nombreRol, string descripcion)
        {
            IdRol = idRol;
            NombreRol = nombreRol;
            Descripcion = descripcion;
            FechaCreacion = DateTime.Now;
            Estado = "Activo";
        }
    }
}
