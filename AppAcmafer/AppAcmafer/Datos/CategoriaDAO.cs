using AppAcmafer.Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace AppAcmafer.Datos
{
    public class CategoriaDAO
    {
        // Obtener todas las categorías
        public List<Categoria> ObtenerTodasLasCategorias()
        {
            List<Categoria> categorias = new List<Categoria>();
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
                            idCategoria, 
                            nombre, 
                            descripcion
                        FROM categoria
                        ORDER BY nombre";

                    SqlCommand cmd = new SqlCommand(query, conexion);
                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        categorias.Add(new Categoria
                        {
                            IdCategoria = Convert.ToInt32(reader["idCategoria"]),
                            Nombre = reader["nombre"].ToString(),
                            Descripcion = reader["descripcion"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener categorías: " + ex.Message);
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

            return categorias;
        }

        // Obtener categoría por ID
        public Categoria ObtenerCategoriaPorId(int idCategoria)
        {
            Categoria categoria = null;
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
                            idCategoria, 
                            nombre, 
                            descripcion
                        FROM categoria
                        WHERE idCategoria = @IdCategoria";

                    SqlCommand cmd = new SqlCommand(query, conexion);
                    cmd.Parameters.AddWithValue("@IdCategoria", idCategoria);
                    reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        categoria = new Categoria
                        {
                            IdCategoria = Convert.ToInt32(reader["idCategoria"]),
                            Nombre = reader["nombre"].ToString(),
                            Descripcion = reader["descripcion"].ToString()
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener categoría: " + ex.Message);
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

            return categoria;
        }

        // Insertar categoría
        public bool InsertarCategoria(Categoria categoria)
        {
            SqlConnection conexion = null;

            try
            {
                conexion = ConexionBD.ObtenerConexion();
                if (conexion != null)
                {
                    conexion.Open();

                    string query = @"
                        INSERT INTO categoria (nombre, descripcion)
                        VALUES (@Nombre, @Descripcion)";

                    SqlCommand cmd = new SqlCommand(query, conexion);
                    cmd.Parameters.AddWithValue("@Nombre", categoria.Nombre);
                    cmd.Parameters.AddWithValue("@Descripcion", categoria.Descripcion);

                    int filasAfectadas = cmd.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar categoría: " + ex.Message);
            }
            finally
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }
        }

        // Actualizar categoría
        public bool ActualizarCategoria(Categoria categoria)
        {
            SqlConnection conexion = null;

            try
            {
                conexion = ConexionBD.ObtenerConexion();
                if (conexion != null)
                {
                    conexion.Open();

                    string query = @"
                        UPDATE categoria 
                        SET 
                            nombre = @Nombre,
                            descripcion = @Descripcion
                        WHERE idCategoria = @IdCategoria";

                    SqlCommand cmd = new SqlCommand(query, conexion);
                    cmd.Parameters.AddWithValue("@IdCategoria", categoria.IdCategoria);
                    cmd.Parameters.AddWithValue("@Nombre", categoria.Nombre);
                    cmd.Parameters.AddWithValue("@Descripcion", categoria.Descripcion);

                    int filasAfectadas = cmd.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar categoría: " + ex.Message);
            }
            finally
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }
        }

        // Eliminar categoría
        public bool EliminarCategoria(int idCategoria)
        {
            SqlConnection conexion = null;

            try
            {
                conexion = ConexionBD.ObtenerConexion();
                if (conexion != null)
                {
                    conexion.Open();

                    string query = "DELETE FROM categoria WHERE idCategoria = @IdCategoria";

                    SqlCommand cmd = new SqlCommand(query, conexion);
                    cmd.Parameters.AddWithValue("@IdCategoria", idCategoria);

                    int filasAfectadas = cmd.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar categoría: " + ex.Message);
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