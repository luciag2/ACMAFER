using AppAcmafer.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Web;

namespace AppAcmafer.Datos
{
    public class CompraDAO
    {
        private ConexionBD conexionBD = new ConexionBD();

        public List<Compra> ObtenerTodasLasCompras()
        {
            List<Compra> compras = new List<Compra>();
            SqlConnection conexion = null;

            try
            {
                conexion = conexionBD.ObtenerConexion();
                string query = @"SELECT c.idCompra, c.cantidad, c.valorTotal, c.descuento,
                                c.idProducto, c.idPedido, 
                                p.nombre as nombreProducto, 
                                pe.numeroPedido
                                FROM compra c
                                INNER JOIN producto p ON c.idProducto = p.idProducto
                                INNER JOIN pedido pe ON c.idPedido = pe.idPedido";

                SqlCommand cmd = new SqlCommand(query, conexion);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    compras.Add(new Compra
                    {
                        IdCompra = Convert.ToInt32(reader["idCompra"]),
                        Cantidad = Convert.ToInt32(reader["cantidad"]),
                        ValorTotal = Convert.ToDecimal(reader["valorTotal"]),
                        Descuento = Convert.ToDecimal(reader["descuento"]),
                        IdProducto = Convert.ToInt32(reader["idProducto"]),
                        IdPedido = Convert.ToInt32(reader["idPedido"]),
                        NombreProducto = reader["nombreProducto"].ToString(),
                        NumeroPedido = reader["numeroPedido"].ToString()
                    });
                }
            }
            finally
            {
                conexionBD.CerrarConexion(conexion);
            }

            return compras;
        }

        public Producto ObtenerProductoPorId (int id)
        {
            Producto producto = null;
            string connectionString = ConfigurationManager.ConnectionStrings["ClConexion"].ConnectionString;

            string query = "select idProducto, nombre, precioUnitario from producto where idProducto = @id";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            producto = new Producto
                            {
                                IdProducto = (int)reader["idProducto"],
                                Nombre = reader["nombre"].ToString(),
                                PrecioUnitario = Convert.ToDecimal(reader["precioUnitario"])
                            };
                        }
                    }
                }
            }
            return producto;
        }
    }
}