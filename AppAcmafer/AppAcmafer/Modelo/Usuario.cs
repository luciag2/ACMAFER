using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppAcmafer.Modelo
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string Documento { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Celular { get; set; }
        public string Clave { get; set; }
        public string Estado { get; set; }
        public int IdRol { get; set; }

        // Propiedad adicional para mostrar nombre completo
        public string NombreCompleto => $"{Nombre} {Apellido}";

        public Rol Rol { get; set; }

        // Constructor
        public Usuario()
        {
            IdUsuario = 0;
            Documento = string.Empty;
            Nombre = string.Empty;
            Apellido = string.Empty;
            Email = string.Empty;
            Celular = string.Empty;
            Clave = string.Empty;
            Estado = "Activo";
            IdRol = 0;
            Rol = null;
        }
    }
}
