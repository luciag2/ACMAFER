using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppAcmafer.Utilidades
{
    public class ClPermisosROL
    {
        public static class ClPermisosHelper
        {

            public const int ROL_ADMINISTRADOR = 1;
            public const int ROL_EMPLEADO = 2;
            public const int ROL_CLIENTE = 3;
            public const int ROL_SUPERVISOR = 4;

            public static bool EstaLogueado()
            {
                return HttpContext.Current.Session["emailUser"] != null;
            }

            public static int ObtenerRolActual()
            {
                if (HttpContext.Current.Session["rol"] != null)
                {
                    return Convert.ToInt32(HttpContext.Current.Session["rol"]);
                }
                return 0;
            }

            public static bool TieneRol(int idRol)
            {
                return ObtenerRolActual() == idRol;
            }

            public static bool EsAdministrador()
            {
                return TieneRol(ROL_ADMINISTRADOR);
            }

            public static bool EsEmpleado()
            {
                return TieneRol(ROL_EMPLEADO);
            }

            public static bool EsCliente()
            {
                return TieneRol(ROL_CLIENTE);
            }

            public static bool EsSupervisor()
            {
                return TieneRol(ROL_SUPERVISOR);
            }

            public static bool TienePermisoAdminOEmpleado()
            {
                int rol = ObtenerRolActual();
                return rol == ROL_ADMINISTRADOR || rol == ROL_EMPLEADO;
            }

            /// <summary>
            /// Verifica si el usuario tiene acceso a una página según el rol requerido
            /// </summary>
            public static void VerificarAcceso(int rolRequerido)
            {
                if (!EstaLogueado())
                {
                    HttpContext.Current.Response.Redirect("~/Vista/Login.aspx"); // ✅ CORREGIDO
                    return;
                }

                if (!TieneRol(rolRequerido))
                {
                    HttpContext.Current.Response.Redirect("~/Vista/AccesoDenegado.aspx"); // ✅ CORREGIDO
                }
            }

            /// <summary>
            /// Verifica si el usuario tiene acceso permitiendo múltiples roles
            /// </summary>
            public static void VerificarAccesoMultiple(params int[] rolesPermitidos)
            {
                if (!EstaLogueado())
                {
                    HttpContext.Current.Response.Redirect("~/Vista/Login.aspx"); // ✅ CORREGIDO
                    return;
                }

                int rolActual = ObtenerRolActual();
                bool tienePermiso = false;

                foreach (int rol in rolesPermitidos)
                {
                    if (rolActual == rol)
                    {
                        tienePermiso = true;
                        break;
                    }
                }

                if (!tienePermiso)
                {
                    HttpContext.Current.Response.Redirect("~/Vista/AccesoDenegado.aspx"); // ✅ CORREGIDO
                }
            }

            public static string ObtenerNombreUsuario()
            {
                if (HttpContext.Current.Session["nombreCompleto"] != null)
                {
                    return HttpContext.Current.Session["nombreCompleto"].ToString();
                }
                return "Usuario";
            }

            public static void CerrarSesion()
            {
                HttpContext.Current.Session.Clear();
                HttpContext.Current.Session.Abandon();
                HttpContext.Current.Response.Redirect("~/Vista/Login.aspx"); // ✅ CORREGIDO
            }
        }
    }
}