using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppAcmafer.Logica
{
    public class Cl_Rol
    {
        // Método para VALIDAR acceso según rol
        public bool ValidarAcceso(int idRol, string nombreMenu)
        {
            Dictionary<int, List<string>> permisos = ObtenerTodosLosPermisos();

            if (permisos.ContainsKey(idRol))
            {
                return permisos[idRol].Contains(nombreMenu);
            }

            return false;
        }

        // Método para OBTENER menú por rol
        public List<string> ObtenerMenuPorRol(int idRol)
        {
            Dictionary<int, List<string>> permisos = ObtenerTodosLosPermisos();

            if (permisos.ContainsKey(idRol))
            {
                return permisos[idRol];
            }

            return new List<string>();
        }

        // Método privado para definir permisos
        private Dictionary<int, List<string>> ObtenerTodosLosPermisos()
        {
            Dictionary<int, List<string>> permisos = new Dictionary<int, List<string>>();

            // Permisos para Administrador (idRol = 1)
            permisos.Add(1, new List<string>
            {
                "menuUsuarios",
                "menuProductos",
                "menuCategorias",
                "menuPedidos",
                "menuTareas",
                "menuReportes",
                "menuRoles"
            });

            // Permisos para Empleado (idRol = 2)
            permisos.Add(2, new List<string>
            {
                "menuTareas",
                "menuProductos",
                "menuPedidos"
            });

            // Permisos para Cliente (idRol = 3)
            permisos.Add(3, new List<string>
            {
                "menuPedidos",
                "menuProductos"
            });

            // Permisos para Supervisor (idRol = 4)
            permisos.Add(4, new List<string>
            {
                "menuTareas",
                "menuProductos",
                "menuPedidos",
                "menuReportes"
            });

            return permisos;
        }
    }
}

