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
        private ConexionBD conexionBD = new ConexionBD();

        public List<Producto> ObtenerTodosLosProductos()
        {
            List<Producto> productos = new List<Producto>();
            SqlConnection conexion = null;

            try
            {
                conexion = conexionBD.ObtenerConexion();
                string query = @"SELECT p.idProducto, p.nombre, p.descripcion, p.codigo, 
                                p.stockActual, p.estado, p.precioUnitario, 
                                p.idCategoria, c.nombre as nombreCategoria
                                FROM producto p
                                INNER JOIN categoria c ON p.idCategoria = c.idCategoria
                                WHERE p.estado = 'Disponible'";

                SqlCommand cmd = new SqlCommand(query, conexion);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    productos.Add(new Producto
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
                    });
                }
            }
            finally
            {
                conexionBD.CerrarConexion(conexion);
            }

            return productos;
        }


    }
}