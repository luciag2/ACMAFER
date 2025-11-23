using AppAcmafer.Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace AppAcmafer.Datos
{
    public class CD_Usuario
    {
        // ============ LISTAR USUARIOS ============
        public DataTable ListarUsuarios()
        {
            DataTable dt = new DataTable();
            SqlConnection conexion = null;

            try
            {
                conexion = ConexionBD.ObtenerConexion();
                if (conexion != null)
                {
                    conexion.Open();

                    string query = @"
                        SELECT 
                            u.idUsuario,
                            u.documento,
                            u.nombre,
                            u.apellido,
                            u.email,
                            u.celular,
                            u.estado,
                            r.rol as nombreRol,
                            u.idRol
                        FROM usuario u
                        LEFT JOIN rol r ON u.idRol = r.idRol
                        ORDER BY u.idUsuario";

                    SqlCommand cmd = new SqlCommand(query, conexion);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar usuarios: " + ex.Message);
            }
            finally
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return dt;
        }

        // ============ CAMBIAR CONTRASEÑA ============
        public bool CambiarContrasena(int idUsuario, string claveActual, string claveNueva)
        {
            SqlConnection conexion = null;

            try
            {
                conexion = ConexionBD.ObtenerConexion();
                if (conexion != null)
                {
                    conexion.Open();

                    // Primero verificar que la contraseña actual sea correcta
                    string queryVerificar = "SELECT clave FROM usuario WHERE idUsuario = @IdUsuario";
                    SqlCommand cmdVerificar = new SqlCommand(queryVerificar, conexion);
                    cmdVerificar.Parameters.AddWithValue("@IdUsuario", idUsuario);

                    string claveGuardada = cmdVerificar.ExecuteScalar()?.ToString();

                    if (string.IsNullOrEmpty(claveGuardada))
                    {
                        return false; // Usuario no encontrado
                    }

                    // Verificar si la clave actual coincide
                    if (claveGuardada != claveActual)
                    {
                        return false; // Contraseña actual incorrecta
                    }

                    // Actualizar a la nueva contraseña
                    string queryActualizar = @"
                        UPDATE usuario 
                        SET clave = @ClaveNueva 
                        WHERE idUsuario = @IdUsuario";

                    SqlCommand cmdActualizar = new SqlCommand(queryActualizar, conexion);
                    cmdActualizar.Parameters.AddWithValue("@IdUsuario", idUsuario);
                    cmdActualizar.Parameters.AddWithValue("@ClaveNueva", claveNueva);

                    int filasAfectadas = cmdActualizar.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cambiar contraseña: " + ex.Message);
            }
            finally
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }
        }

        // ============ ASIGNAR ROL A USUARIO ============
        public bool AsignarRol(int idUsuario, int idRol)
        {
            SqlConnection conexion = null;

            try
            {
                conexion = ConexionBD.ObtenerConexion();
                if (conexion != null)
                {
                    conexion.Open();

                    // Actualizar el rol directamente en la tabla usuario
                    string queryActualizar = @"
                        UPDATE usuario 
                        SET idRol = @IdRol 
                        WHERE idUsuario = @IdUsuario";

                    SqlCommand cmd = new SqlCommand(queryActualizar, conexion);
                    cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);
                    cmd.Parameters.AddWithValue("@IdRol", idRol);

                    int filasAfectadas = cmd.ExecuteNonQuery();

                    // También insertar en la tabla usuarioRol si existe
                    if (filasAfectadas > 0)
                    {
                        string queryUsuarioRol = @"
                            IF NOT EXISTS (SELECT 1 FROM usuarioRol WHERE idUsuario = @IdUsuario AND idRol = @IdRol)
                            BEGIN
                                INSERT INTO usuarioRol (idUsuario, idRol) 
                                VALUES (@IdUsuario, @IdRol)
                            END";

                        SqlCommand cmdUsuarioRol = new SqlCommand(queryUsuarioRol, conexion);
                        cmdUsuarioRol.Parameters.AddWithValue("@IdUsuario", idUsuario);
                        cmdUsuarioRol.Parameters.AddWithValue("@IdRol", idRol);
                        cmdUsuarioRol.ExecuteNonQuery();
                    }

                    return filasAfectadas > 0;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al asignar rol: " + ex.Message);
            }
            finally
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }
        }

        // ============ OBTENER USUARIO POR ID ============
        public Usuario ObtenerUsuarioPorId(int idUsuario)
        {
            Usuario usuario = null;
            SqlConnection conexion = null;
            SqlDataReader reader = null;

            try
            {
                conexion = ConexionBD.ObtenerConexion();
                if (conexion != null)
                {
                    conexion.Open();

                    string query = @"
                        SELECT 
                            u.idUsuario,
                            u.documento,
                            u.nombre,
                            u.apellido,
                            u.email,
                            u.celular,
                            u.clave,
                            u.estado,
                            u.idRol
                        FROM usuario u
                        WHERE u.idUsuario = @IdUsuario";

                    SqlCommand cmd = new SqlCommand(query, conexion);
                    cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);
                    reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        usuario = new Usuario
                        {
                            IdUsuario = Convert.ToInt32(reader["idUsuario"]),
                            Documento = reader["documento"].ToString(),
                            Nombre = reader["nombre"].ToString(),
                            Apellido = reader["apellido"].ToString(),
                            Email = reader["email"].ToString(),
                            Celular = reader["celular"].ToString(),
                            Clave = reader["clave"].ToString(),
                            Estado = reader["estado"].ToString(),
                            IdRol = Convert.ToInt32(reader["idRol"])
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener usuario: " + ex.Message);
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                }
                if (conexion != null && conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return usuario;
        }

        // ============ INSERTAR USUARIO ============
        public bool InsertarUsuario(Usuario usuario)
        {
            SqlConnection conexion = null;

            try
            {
                conexion = ConexionBD.ObtenerConexion();
                if (conexion != null)
                {
                    conexion.Open();

                    string query = @"
                        INSERT INTO usuario 
                        (documento, nombre, apellido, email, celular, clave, estado, idRol)
                        VALUES 
                        (@Documento, @Nombre, @Apellido, @Email, @Celular, @Clave, @Estado, @IdRol)";

                    SqlCommand cmd = new SqlCommand(query, conexion);
                    cmd.Parameters.AddWithValue("@Documento", usuario.Documento);
                    cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("@Apellido", usuario.Apellido);
                    cmd.Parameters.AddWithValue("@Email", usuario.Email);
                    cmd.Parameters.AddWithValue("@Celular", usuario.Celular);
                    cmd.Parameters.AddWithValue("@Clave", usuario.Clave);
                    cmd.Parameters.AddWithValue("@Estado", usuario.Estado);
                    cmd.Parameters.AddWithValue("@IdRol", usuario.IdRol);

                    int filasAfectadas = cmd.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar usuario: " + ex.Message);
            }
            finally
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }
        }

        // ============ ACTUALIZAR USUARIO ============
        public bool ActualizarUsuario(Usuario usuario)
        {
            SqlConnection conexion = null;

            try
            {
                conexion = ConexionBD.ObtenerConexion();
                if (conexion != null)
                {
                    conexion.Open();

                    string query = @"
                        UPDATE usuario 
                        SET 
                            documento = @Documento,
                            nombre = @Nombre,
                            apellido = @Apellido,
                            email = @Email,
                            celular = @Celular,
                            estado = @Estado,
                            idRol = @IdRol
                        WHERE idUsuario = @IdUsuario";

                    SqlCommand cmd = new SqlCommand(query, conexion);
                    cmd.Parameters.AddWithValue("@IdUsuario", usuario.IdUsuario);
                    cmd.Parameters.AddWithValue("@Documento", usuario.Documento);
                    cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("@Apellido", usuario.Apellido);
                    cmd.Parameters.AddWithValue("@Email", usuario.Email);
                    cmd.Parameters.AddWithValue("@Celular", usuario.Celular);
                    cmd.Parameters.AddWithValue("@Estado", usuario.Estado);
                    cmd.Parameters.AddWithValue("@IdRol", usuario.IdRol);

                    int filasAfectadas = cmd.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar usuario: " + ex.Message);
            }
            finally
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }
        }
    }
}