using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AppAcmafer.Datos
{
    public class CD_UsuarioRol
    {
        public DataTable ObtenerRolesUsuario(int idUsuario)
        {
            DataTable dt = new DataTable();

            try
            {
                string conexion = ConfigurationManager.ConnectionStrings["CadenaConexion"]?.ConnectionString;
                if (string.IsNullOrEmpty(conexion))
                {
                    throw new Exception("No se encontró la cadena de conexión");
                }

                using (SqlConnection conn = new SqlConnection(conexion))
                {
                    string query = @"SELECT ur.idUsuarioRol, r.idRol, r.nombre as nombreRol
                                   FROM dbo.usuarioRol ur
                                   INNER JOIN dbo.rol r ON ur.idRol = r.idRol
                                   WHERE ur.idUsuario = @idUsuario";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@idUsuario", idUsuario);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener roles del usuario: " + ex.Message);
            }

            return dt;
        }

        public bool AsignarRol(int idUsuario, int idRol)
        {
            try
            {
                string conexion = ConfigurationManager.ConnectionStrings["CadenaConexion"]?.ConnectionString;
                if (string.IsNullOrEmpty(conexion))
                {
                    throw new Exception("No se encontró la cadena de conexión");
                }

                using (SqlConnection conn = new SqlConnection(conexion))
                {
                    conn.Open();

                    // Verificar si ya existe la asignación
                    string queryVerificar = "SELECT COUNT(*) FROM dbo.usuarioRol WHERE idUsuario = @idUsuario AND idRol = @idRol";
                    SqlCommand cmdVerificar = new SqlCommand(queryVerificar, conn);
                    cmdVerificar.Parameters.AddWithValue("@idUsuario", idUsuario);
                    cmdVerificar.Parameters.AddWithValue("@idRol", idRol);

                    int existe = (int)cmdVerificar.ExecuteScalar();

                    if (existe > 0)
                    {
                        return false; // Ya existe
                    }

                    // Insertar la nueva asignación
                    string query = "INSERT INTO dbo.usuarioRol (idUsuario, idRol) VALUES (@idUsuario, @idRol)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                    cmd.Parameters.AddWithValue("@idRol", idRol);

                    int filasAfectadas = cmd.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al asignar rol: " + ex.Message);
            }
        }
    }
}