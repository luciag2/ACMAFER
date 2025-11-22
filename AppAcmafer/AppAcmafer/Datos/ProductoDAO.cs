using AppAcmafer.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AppAcmafer.Datos
{
    public class ProductoDAO
    {
        // NO debe haber NADA aquí - elimina cualquier instancia de ConexionBD

        public List<Producto> ObtenerTodosLosProductos()
        {
            List<Producto> productos = new List<Producto>();
            SqlConnection conexion = null;
            SqlDataReader reader = null;

            try
            {
                // Línea 18 - debe ser EXACTAMENTE así:
                conexion = ConexionBD.ObtenerConexion();

                if (conexion != null)
                {
                    conexion.Open();
                }

                string query = @"SELECT p.idProducto, p.nombre, p.descripcion, p.codigo,
                                p.stockActual, p.estado, p.precioUnitario,
                                p.idCategoria, c.nombre as nombreCategoria
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
                        Estado = reader["estado"].ToString(),
                        PrecioUnitario = Convert.ToDecimal(reader["precioUnitario"]),
                        IdCategoria = Convert.ToInt32(reader["idCategoria"]),
                        NombreCategoria = reader["nombreCategoria"].ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener productos: " + ex.Message);
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

            return productos;
        }
    }
}