using AppAcmafer.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AppAcmafer.Datos
{
    public class CD_Usuario
    {
        public List<Usuario> Listar()
        {
            List<Usuario> lista = new List<Usuario>();

            // CORRECCIÓN CS0103
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                // Trae usuarios que estén activos Y que tengan el Rol de Empleado (ID 2 es un ejemplo común)
                string query = @"SELECT u.idUsuario, u.nombre, u.apellido, u.correo, u.telefono, u.idRol, r.nombre AS rolNombre
                         FROM usuario u
                         INNER JOIN Rol r ON u.idRol = r.idRol
                         WHERE u.estado = 'Activo' AND r.nombre = 'Empleado' 
                         -- o WHERE u.idRol = 2 -- Si prefieres usar el ID directamente
                        ";

                SqlCommand cmd = new SqlCommand(query, conexion);

                try
                {
                    conexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Usuario()
                            {
                                IdUsuario = Convert.ToInt32(dr["idUsuario"]),
                                Nombre = dr["nombre"].ToString(),
                                Apellido = dr["apellido"].ToString(),
                                // ... mapeo de otras propiedades

                                Rol = new Rol() // Asumo que tienes una clase Rol
                                {
                                    IdRol = Convert.ToInt32(dr["idRol"]),
                                    NombreRol = dr["rolNombre"].ToString()
                                }
                            });
                        }
                    }
                }
                catch (Exception)
                {
                    // Manejo de errores
                }
            }
            return lista;
        }

        // ... (Debes aplicar la misma corrección de conexión en Registrar, Editar, Eliminar, etc.)
    

        // Método para REGISTRAR un usuario
        public int Registrar(Usuario obj, out string mensaje)
        {
            int idGenerado = 0;
            mensaje = string.Empty;

            using (SqlConnection conexion = ConexionBD.ObtenerConexion()) 
            {
                string query = @"INSERT INTO usuario (documento, nombre, apellido, 
                                email, celular, clave, estado, idRol) 
                                VALUES (@documento, @nombre, @apellido, @email, 
                                @celular, @clave, 'Activo', @idRol);
                                SELECT SCOPE_IDENTITY();";

                SqlCommand cmd = new SqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@documento", obj.Documento);
                cmd.Parameters.AddWithValue("@nombre", obj.Nombre);
                cmd.Parameters.AddWithValue("@apellido", obj.Apellido);
                cmd.Parameters.AddWithValue("@email", obj.Email);
                cmd.Parameters.AddWithValue("@celular", obj.Celular);
                cmd.Parameters.AddWithValue("@clave", obj.Clave);
                cmd.Parameters.AddWithValue("@idRol", obj.IdRol);

                conexion.Open();
                idGenerado = Convert.ToInt32(cmd.ExecuteScalar());
                mensaje = "Usuario registrado correctamente";
            }

            return idGenerado;
        }

        // Método para VALIDAR LOGIN
        public Usuario ValidarLogin(string email, string clave)
        {
            Usuario usuario = null;

            using (SqlConnection conexion = ConexionBD.ObtenerConexion()) 
            {
                string query = @"SELECT u.idUsuario, u.nombre, u.apellido, 
                                u.email, u.idRol, r.rol 
                                FROM usuario u
                                INNER JOIN rol r ON u.idRol = r.idRol
                                WHERE u.email = @email AND u.clave = @clave 
                                AND u.estado = 'Activo'";

                SqlCommand cmd = new SqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@clave", clave);

                conexion.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        usuario = new Usuario()
                        {
                            IdUsuario = Convert.ToInt32(dr["idUsuario"]),
                            Nombre = dr["nombre"].ToString(),
                            Apellido = dr["apellido"].ToString(),
                            Email = dr["email"].ToString(),
                            IdRol = Convert.ToInt32(dr["idRol"]),
                            Rol = new Rol() { NombreRol = dr["rol"].ToString() }
                        };
                    }
                }
            }

            return usuario;
        }

        // Método para EDITAR usuario
        public bool Editar(Usuario obj, out string mensaje)
        {
            bool respuesta = false;
            mensaje = string.Empty;

            using (SqlConnection conexion = ConexionBD.ObtenerConexion()) 
            {
                string query = @"UPDATE usuario SET documento = @documento, 
                                nombre = @nombre, apellido = @apellido, 
                                email = @email, celular = @celular, 
                                idRol = @idRol 
                                WHERE idUsuario = @idUsuario";

                SqlCommand cmd = new SqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@idUsuario", obj.IdUsuario);
                cmd.Parameters.AddWithValue("@documento", obj.Documento);
                cmd.Parameters.AddWithValue("@nombre", obj.Nombre);
                cmd.Parameters.AddWithValue("@apellido", obj.Apellido);
                cmd.Parameters.AddWithValue("@email", obj.Email);
                cmd.Parameters.AddWithValue("@celular", obj.Celular);
                cmd.Parameters.AddWithValue("@idRol", obj.IdRol);

                conexion.Open();
                respuesta = cmd.ExecuteNonQuery() > 0;
                mensaje = "Usuario editado correctamente";
            }

            return respuesta;
        }

        // Método para ELIMINAR usuario
        public bool Eliminar(int id, out string mensaje)
        {
            bool respuesta = false;
            mensaje = string.Empty;

            using (SqlConnection conexion = ConexionBD.ObtenerConexion()) 
            {
                string query = "UPDATE usuario SET estado = 'Inactivo' WHERE idUsuario = @id";

                SqlCommand cmd = new SqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@id", id);

                conexion.Open();
                respuesta = cmd.ExecuteNonQuery() > 0;
                mensaje = "Usuario eliminado correctamente";
            }

            return respuesta;
        }
    }
}

