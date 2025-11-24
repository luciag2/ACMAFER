using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppAcmafer.Logica
{
    public class NavegacionLogica
    {
        public void RedirigirASeccion(string seccion)
        {
            string url = "";

            switch (seccion.ToLower())
            {
                case "productos":
                    url = "~/Vista/Productos.aspx";
                    break;
                case "compras":
                    url = "~/Vista/Compras.aspx";
                    break;
                case "tareas":
                case "asignartareas":
                    url = "~/Vista/AsignarTareas.aspx";
                    break;
                default:
                    url = "~/Vista/Productos.aspx";
                    break;
            }

            HttpContext.Current.Response.Redirect(url);
        }

        public bool ValidarYRedirigir(string seccion, int idRol)
        {
            MenuLogica menuLogica = new MenuLogica();
            string pagina = seccion + ".aspx";

            if (menuLogica.ValidarAcceso(idRol, pagina))
            {
                RedirigirASeccion(seccion);
                return true;
            }

            return false;
        }
    }
}