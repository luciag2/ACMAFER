using AppAcmafer.Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace AppAcmafer.Datos
{
    public class ProductoDAO
    {
        public List<Producto> ObtenerTodosLosProductos()
        {
            List<Producto> productos = new List<Producto>();
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
                            p.idProducto, 
                            p.nombre, 
                            p.descripcion, 
                            p.codigo,
                            p.stockActual, 
                            p.estado, 
                            p.precioUnitario,
                            p.idCategoria, 
                            c.nombre as nombreCategoria
                        FROM producto p
                        INNER JOIN categoria c ON p.idCategoria = c.idCategoria
                        WHERE p.estado = 'Disponible'";

                    SqlCommand cmd = new SqlCommand(query, conexion);
                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        productos.Add(new Producto
                        {
                            IdProducto = Convert.ToInt32(reader["idProducto"]),
                            Nombre = reader["nombre"].ToString(),
                            Descripcion = reader["descripcion"].ToString(),
                            Codigo = reader["codigo"].ToString(),
                            StockActual = Convert.ToInt32(reader["stockActual"]),  // ✅ Ahora es int
                            Estado = reader["estado"].ToString(),
                            PrecioUnitario = Convert.ToDecimal(reader["precioUnitario"]),
                            IdCategoria = Convert.ToInt32(reader["idCategoria"]),
                            NombreCategoria = reader["nombreCategoria"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener productos: " + ex.Message);
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

            return productos;
        }


        public Producto ObtenerProductoPorId(int idProducto)
        {
            Producto producto = null;
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
                            p.idProducto, 
                            p.nombre, 
                            p.descripcion, 
                            p.codigo,
                            p.stockActual, 
                            p.estado, 
                            p.precioUnitario,
                            p.idCategoria, 
                            c.nombre as nombreCategoria
                        FROM producto p
                        INNER JOIN categoria c ON p.idCategoria = c.idCategoria
                        WHERE p.idProducto = @IdProducto";

                    SqlCommand cmd = new SqlCommand(query, conexion);
                    cmd.Parameters.AddWithValue("@IdProducto", idProducto);
                    reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        producto = new Producto
                        {
                            IdProducto = Convert.ToInt32(reader["idProducto"]),
                            Nombre = reader["nombre"].ToString(),
                            Descripcion = reader["descripcion"].ToString(),
                            Codigo = reader["codigo"].ToString(),
                            StockActual = Convert.ToInt32(reader["stockActual"]),
                            Estado = reader["estado"].ToString(),
                            PrecioUnitario = Convert.ToDecimal(reader["precioUnitario"]),
                            IdCategoria = Convert.ToInt32(reader["idCategoria"]),
                            NombreCategoria = reader["nombreCategoria"].ToString()
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener producto: " + ex.Message);
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

            return producto;
    }

    public class ProductoDAL
    {
        private readonly string connectionString = "Data Source=.;Initial Catalog=ProyectoACMAFER;Integrated Security=True;";

        public List<Producto> ObtenerProductosEnVenta()
        {
            var listaProductos = new List<Producto>();
            string query = @"
            SELECT
                p.idProducto, p.nombre, p.descripcion, 
                p.codigo, p.stockActual, p.precioUnitario, 
                p.estado, c.nombre
            FROM dbo.producto p
            INNER JOIN dbo.categoria c ON p.idCategoria = c.idCategoria
            WHERE p.estado = 'Disponible' AND CAST(p.stockActual AS INT) > 0;
        ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        var producto = new Producto
                        {
                            IdProducto = reader.GetInt32(reader.GetOrdinal("idProducto")),
                            Nombre = reader["nombre"].ToString(),
                            Descripcion = reader["descripcion"].ToString(),
                            Codigo = reader["codigo"].ToString(),
                            Estado = reader["estado"].ToString(),
                            IdCategoria = reader.GetInt32(reader.GetOrdinal("idCategoria")),
                            StockActual = Convert.ToInt32(reader["stockActual"]),
                            PrecioUnitario = Convert.ToDecimal(reader["precioUnitario"])
                        };
                        listaProductos.Add(producto);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al obtener productos: {ex.Message}");
                    throw new Exception("Error al consultar productos disponibles.", ex);
                }
            } 

            return listaProductos;
        }
    }
}
}