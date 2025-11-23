using AppAcmafer.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AppAcmafer.Datos
{
    public class RolDAO
    {
        public List<Rol> ObtenerTodosLosRoles()
        {
            List<Rol> roles = new List<Rol>();
            SqlConnection conexion = null;
            SqlDataReader reader = null;

            try
            {
                conexion = ConexionBD.ObtenerConexion();

                if (conexion != null)
                {
                    conexion.Open();
                }

                string query = @"SELECT IdRol, NombreRol, Descripcion, FechaCreacion, Estado
                                FROM Rol
                                WHERE Estado = 'Activo'
                                ORDER BY NombreRol";

                SqlCommand cmd = new SqlCommand(query, conexion);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    roles.Add(new Rol
                    {
                        IdRol = Convert.ToInt32(reader["IdRol"]),
                        NombreRol = reader["NombreRol"].ToString(),
                        Descripcion = reader["Descripcion"]?.ToString(),
                        FechaCreacion = reader["FechaCreacion"] != DBNull.Value
                            ? Convert.ToDateTime(reader["FechaCreacion"])
                            : DateTime.Now,
                        Estado = reader["Estado"].ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener roles: " + ex.Message);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }

                if (conexion != null && conexion.State == System.Data.ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return roles;
        }

        public Rol ObtenerRolPorId(int idRol)
        {
            Rol rol = null;
            SqlConnection conexion = null;
            SqlDataReader reader = null;

            try
            {
                conexion = ConexionBD.ObtenerConexion();

                if (conexion != null)
                {
                    conexion.Open();
                }

                string query = @"SELECT IdRol, NombreRol, Descripcion, FechaCreacion, Estado
                                FROM Rol
                                WHERE IdRol = @IdRol";

                SqlCommand cmd = new SqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@IdRol", idRol);
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    rol = new Rol
                    {
                        IdRol = Convert.ToInt32(reader["IdRol"]),
                        NombreRol = reader["NombreRol"].ToString(),
                        Descripcion = reader["Descripcion"]?.ToString(),
                        FechaCreacion = reader["FechaCreacion"] != DBNull.Value
                            ? Convert.ToDateTime(reader["FechaCreacion"])
                            : DateTime.Now,
                        Estado = reader["Estado"].ToString()
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener rol: " + ex.Message);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }

                if (conexion != null && conexion.State == System.Data.ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return rol;
        }

        public bool EliminarRol(int idRol)
        {
            SqlConnection conexion = null;
            bool eliminado = false;

            try
            {
                conexion = ConexionBD.ObtenerConexion();

                if (conexion != null)
                {
                    conexion.Open();
                }

                // Eliminación lógica (cambiar estado a Inactivo)
                string query = @"UPDATE Rol 
                                SET Estado = 'Inactivo'
                                WHERE IdRol = @IdRol";

                SqlCommand cmd = new SqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@IdRol", idRol);

                int filasAfectadas = cmd.ExecuteNonQuery();
                eliminado = filasAfectadas > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar rol: " + ex.Message);
            }
            finally
            {
                if (conexion != null && conexion.State == System.Data.ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return eliminado;
        }

        public bool GuardarPermisos(int idRol, Dictionary<string, bool> permisos)
        {
            SqlConnection conexion = null;
            bool guardado = false;

            try
            {
                conexion = ConexionBD.ObtenerConexion();

                if (conexion != null)
                {
                    conexion.Open();
                }

                // Aquí implementarías la lógica para guardar permisos
                // Depende de cómo tengas estructurada tu tabla de permisos

                string query = @"UPDATE RolPermisos 
                                SET AccesoMenu = @AccesoMenu,
                                    MenuVisible = @MenuVisible,
                                    Redireccion = @Redireccion,
                                    GestionUsuarios = @GestionUsuarios,
                                    GestionProductos = @GestionProductos,
                                    GestionPedidos = @GestionPedidos,
                                    GestionTareas = @GestionTareas,
                                    Reportes = @Reportes,
                                    ConfiguracionRoles = @ConfiguracionRoles
                                WHERE IdRol = @IdRol";

                SqlCommand cmd = new SqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@IdRol", idRol);
                cmd.Parameters.AddWithValue("@AccesoMenu", permisos["AccesoMenu"]);
                cmd.Parameters.AddWithValue("@MenuVisible", permisos["MenuVisible"]);
                cmd.Parameters.AddWithValue("@Redireccion", permisos["Redireccion"]);
                cmd.Parameters.AddWithValue("@GestionUsuarios", permisos["GestionUsuarios"]);
                cmd.Parameters.AddWithValue("@GestionProductos", permisos["GestionProductos"]);
                cmd.Parameters.AddWithValue("@GestionPedidos", permisos["GestionPedidos"]);
                cmd.Parameters.AddWithValue("@GestionTareas", permisos["GestionTareas"]);
                cmd.Parameters.AddWithValue("@Reportes", permisos["Reportes"]);
                cmd.Parameters.AddWithValue("@ConfiguracionRoles", permisos["ConfiguracionRoles"]);

                int filasAfectadas = cmd.ExecuteNonQuery();
                guardado = filasAfectadas > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar permisos: " + ex.Message);
            }
            finally
            {
                if (conexion != null && conexion.State == System.Data.ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return guardado;
        }
    }
}