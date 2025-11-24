using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppAcmafer.Logica
{
    public class MenuLogica
    {
        public class OpcionMenu
        {
            public int Id { get; set; }
            public string Texto { get; set; }
            public string Url { get; set; }
            public string Icono { get; set; }
        }

        public List<OpcionMenu> ObtenerOpcionesMenu(int idRol)
        {
            List<OpcionMenu> opciones = new List<OpcionMenu>();

            // Opciones para todos los usuarios autenticados
            opciones.Add(new OpcionMenu
            {
                Id = 1,
                Texto = "Productos",
                Url = "~/Vista/Productos.aspx",
                Icono = "📦"
            });

            opciones.Add(new OpcionMenu
            {
                Id = 2,
                Texto = "Compras",
                Url = "~/Vista/Compras.aspx",
                Icono = "🛒"
            });

            // Opción solo para Administradores (idRol = 1) y Supervisores (idRol = 4)
            if (idRol == 1 || idRol == 4)
            {
                opciones.Add(new OpcionMenu
                {
                    Id = 3,
                    Texto = "Asignación de Tareas",
                    Url = "~/Vista/AsignarTareas.aspx",
                    Icono = "✅"
                });
            }

            return opciones;
        }

        public bool ValidarAcceso(int idRol, string pagina)
        {
            // Páginas accesibles para todos
            List<string> paginasPublicas = new List<string>
            {
                "Productos.aspx",
                "Compras.aspx"
            };

            if (paginasPublicas.Contains(pagina))
            {
                return true;
            }

            // Página solo para Admin y Supervisor
            if (pagina == "AsignarTareas.aspx" && (idRol == 1 || idRol == 4))
            {
                return true;
            }

            return false;
        }
    }
}