using AppAcmafer.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    }
}