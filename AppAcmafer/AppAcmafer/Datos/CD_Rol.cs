using AppAcmafer.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AppAcmafer.Datos
{
    public class CD_Rol
    {
        // Método para LISTAR roles
        public List<Rol> ListarRoles()
        {
            List<Rol> lista = new List<Rol>();

            using (SqlConnection conexion = ConexionBD.ObtenerConexion()) 
            {
                string query = "SELECT idRol, rol, estado FROM rol WHERE estado = 'Activo'";

                SqlCommand cmd = new SqlCommand(query, conexion);
                conexion.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new Rol()
                        {
                            IdRol = Convert.ToInt32(dr["idRol"]),
                            NombreRol = dr["rol"].ToString(),
                            Estado = dr["estado"].ToString()
                        });
                    }
                }
            }

            return lista;
        }

        // Método para obtener permisos por rol (para el menú dinámico)
        public List<string> ObtenerPermisosPorRol(int idRol)
        {
            List<string> permisos = new List<string>();

            // Aquí defines qué permisos tiene cada rol
            if (idRol == 1) // Administrador
            {
                permisos.Add("menuUsuarios");
                permisos.Add("menuProductos");
                permisos.Add("menuPedidos");
                permisos.Add("menuTareas");
                permisos.Add("menuReportes");
            }
            else if (idRol == 2) // Empleado
            {
                permisos.Add("menuTareas");
                permisos.Add("menuProductos");
            }
            else if (idRol == 3) // Cliente
            {
                permisos.Add("menuPedidos");
            }

            return permisos;
        }
    }
}
