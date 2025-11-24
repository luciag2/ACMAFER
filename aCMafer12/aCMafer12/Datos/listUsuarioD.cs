using AppAcmafer.Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AppAcmafer.Datos
{
    public class listUsuarioD
    {
        private ClConexion conexion;

        public listUsuarioD()
        {
            conexion = new ClConexion();
        }

        public List<listUsuarioM> ObtenerTodosLosUsuarios()
        {
            List<listUsuarioM> usuarios = new List<listUsuarioM>();
            SqlConnection cn = null;
            SqlDataReader reader = null;

            try
            {
                cn = conexion.MtAbrirConexion();
                string consulta = @"SELECT 
                    U.idUsuario, 
                    U.documento, 
                    U.nombre, 
                    U.apellido, 
                    U.email, 
                    U.estado, 
                    R.rol AS nombreRol
                FROM usuario U 
                LEFT JOIN rol R ON U.idRol = R.idRol";

                SqlCommand cmd = new SqlCommand(consulta, cn);
                cmd.CommandType = CommandType.Text;

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    listUsuarioM usuario = new listUsuarioM
                    {
                        IdUsuario = (int)reader["idUsuario"],
                        Documento = reader["documento"].ToString(),
                        NombreCompleto = reader["nombre"].ToString() + " " + reader["apellido"].ToString(),
                        Email = reader["email"].ToString(),
                        Rol = reader["nombreRol"] != DBNull.Value ? reader["nombreRol"].ToString() : "Sin Rol",
                        Estado = reader["estado"].ToString()
                    };

                    usuarios.Add(usuario);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener usuarios: " + ex.Message);
                throw;
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                    reader.Close();

                if (cn != null)
                    conexion.MtCerrarConexion();
            }

            return usuarios;
        }

        public listUsuarioM ObtenerUsuarioPorId(int id)
        {
            listUsuarioM usuario = null;
            SqlConnection cn = null;
            SqlDataReader reader = null;

            try
            {
                cn = conexion.MtAbrirConexion();
                string consulta = @"SELECT 
                    U.idUsuario, 
                    U.documento, 
                    U.nombre, 
                    U.apellido, 
                    U.email, 
                    U.estado, 
                    R.rol AS nombreRol
                FROM usuario U 
                LEFT JOIN rol R ON U.idRol = R.idRol
                WHERE U.idUsuario = @id";

                SqlCommand cmd = new SqlCommand(consulta, cn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.CommandType = CommandType.Text;

                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    usuario = new listUsuarioM
                    {
                        IdUsuario = (int)reader["idUsuario"],
                        Documento = reader["documento"].ToString(),
                        NombreCompleto = reader["nombre"].ToString() + " " + reader["apellido"].ToString(),
                        Email = reader["email"].ToString(),
                        Rol = reader["nombreRol"] != DBNull.Value ? reader["nombreRol"].ToString() : "Sin Rol",
                        Estado = reader["estado"].ToString()
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener usuario: " + ex.Message);
                throw;
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                    reader.Close();

                if (cn != null)
                    conexion.MtCerrarConexion();
            }

            return usuario;
        }

        public bool AgregarUsuario(listUsuarioM usuario)
        {
            SqlConnection cn = null;

            try
            {
                cn = conexion.MtAbrirConexion();

                string consulta = @"INSERT INTO usuario (documento, nombre, apellido, email, idRol, estado) 
                                   VALUES (@documento, @nombre, @apellido, @correo, @idRol, @estado)";

                SqlCommand cmd = new SqlCommand(consulta, cn);

                string[] nombreCompleto = usuario.NombreCompleto.Split(' ');
                string nombre = nombreCompleto.Length > 0 ? nombreCompleto[0] : "";
                string apellido = nombreCompleto.Length > 1 ? string.Join(" ", nombreCompleto.Skip(1)) : "";

                cmd.Parameters.AddWithValue("@documento", usuario.Documento);
                cmd.Parameters.AddWithValue("@nombre", nombre);
                cmd.Parameters.AddWithValue("@apellido", apellido);
                cmd.Parameters.AddWithValue("@email", usuario.Email);
                cmd.Parameters.AddWithValue("@idRol", ObtenerIdRolPorNombre(usuario.Rol));
                cmd.Parameters.AddWithValue("@estado", usuario.Estado);
                cmd.CommandType = CommandType.Text;

                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al agregar usuario: " + ex.Message);
                return false;
            }
            finally
            {
                if (cn != null)
                    conexion.MtCerrarConexion();
            }
        }

        public bool EditarUsuario(listUsuarioM usuario)
        {
            SqlConnection cn = null;

            try
            {
                cn = conexion.MtAbrirConexion();

                string consulta = @"UPDATE usuario 
                                   SET documento = @documento, 
                                       nombre = @nombre, 
                                       apellido = @apellido,
                                       email = @correo, 
                                       idRol = @idRol, 
                                       estado = @estado 
                                   WHERE idUsuario = @id";

                SqlCommand cmd = new SqlCommand(consulta, cn);

                string[] nombreCompleto = usuario.NombreCompleto.Split(' ');
                string nombre = nombreCompleto.Length > 0 ? nombreCompleto[0] : "";
                string apellido = nombreCompleto.Length > 1 ? string.Join(" ", nombreCompleto.Skip(1)) : "";

                cmd.Parameters.AddWithValue("@id", usuario.IdUsuario);
                cmd.Parameters.AddWithValue("@documento", usuario.Documento);
                cmd.Parameters.AddWithValue("@nombre", nombre);
                cmd.Parameters.AddWithValue("@apellido", apellido);
                cmd.Parameters.AddWithValue("@email", usuario.Email);
                cmd.Parameters.AddWithValue("@idRol", ObtenerIdRolPorNombre(usuario.Rol));
                cmd.Parameters.AddWithValue("@estado", usuario.Estado);
                cmd.CommandType = CommandType.Text;

                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al editar usuario: " + ex.Message);
                return false;
            }
            finally
            {
                if (cn != null)
                    conexion.MtCerrarConexion();
            }
        }

        public bool EliminarUsuario(int id)
        {
            SqlConnection cn = null;

            try
            {
                cn = conexion.MtAbrirConexion();
                SqlCommand cmd = new SqlCommand("DELETE FROM usuario WHERE idUsuario=@id", cn);

                cmd.Parameters.AddWithValue("@id", id);
                cmd.CommandType = CommandType.Text;

                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar usuario: " + ex.Message);
                return false;
            }
            finally
            {
                if (cn != null)
                    conexion.MtCerrarConexion();
            }
        }

        private int ObtenerIdRolPorNombre(string nombreRol)
        {
            switch (nombreRol.ToLower())
            {
                case "administrador":
                    return 1;
                case "empleado":
                    return 2;
                case "cliente":
                    return 3;
                case "supervisor":
                    return 4;
                default:
                    return 2;
            }
        }

        public listUsuarioM BuscarUsuarioPorEmailODocumento(string usuario)
        {
            listUsuarioM usuarioEncontrado = null;
            SqlConnection cn = null;
            SqlDataReader reader = null;

            try
            {
                cn = conexion.MtAbrirConexion();

                string consulta = @"SELECT 
                    U.idUsuario, 
                    U.documento, 
                    U.nombre, 
                    U.apellido, 
                    U.email, 
                    U.clave,
                    U.estado,
                    U.idRol,
                    R.rol AS nombreRol
                FROM usuario U 
                LEFT JOIN rol R ON U.idRol = R.idRol
                WHERE U.email = @usuario OR U.documento = @usuario";

                SqlCommand cmd = new SqlCommand(consulta, cn);
                cmd.Parameters.AddWithValue("@usuario", usuario);

                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    usuarioEncontrado = new listUsuarioM
                    {
                        IdUsuario = (int)reader["idUsuario"],
                        Documento = reader["documento"].ToString(),
                        NombreCompleto = reader["nombre"].ToString() + " " + reader["apellido"].ToString(),
                        Email = reader["email"].ToString(),
                        Clave = reader["clave"].ToString(),
                        idRol = (int)reader["idRol"], // ✅ CORREGIDO
                        Rol = reader["nombreRol"] != DBNull.Value ? reader["nombreRol"].ToString() : "Sin Rol",
                        Estado = reader["estado"].ToString()
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar usuario: " + ex.Message);
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                    reader.Close();

                if (cn != null)
                    conexion.MtCerrarConexion();
            }

            return usuarioEncontrado;
        }
    }
}