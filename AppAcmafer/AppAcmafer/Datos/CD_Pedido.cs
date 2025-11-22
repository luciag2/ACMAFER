using AppAcmafer.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AppAcmafer.Datos
{
    public class CD_Pedido
    {
        // Método para LISTAR pedidos
        public List<Pedido> Listar()
        {
            List<Pedido> lista = new List<Pedido>();

            using (SqlConnection conexion = ConexionBD.ObtenerConexion()) 
            {
                string query = @"SELECT p.idPedido, p.numeroPedido, p.fechaPedido, 
                                p.estado, p.observaciones, p.idCliente, 
                                u.nombre + ' ' + u.apellido AS cliente
                                FROM pedido p
                                INNER JOIN usuario u ON p.idCliente = u.idUsuario";

                SqlCommand cmd = new SqlCommand(query, conexion);
                conexion.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new Pedido()
                        {
                            IdPedido = Convert.ToInt32(dr["idPedido"]),
                            NumeroPedido = dr["numeroPedido"].ToString(),
                            FechaPedido = Convert.ToDateTime(dr["fechaPedido"]),
                            Estado = dr["estado"].ToString(),
                            Observaciones = dr["observaciones"].ToString(),
                            IdCliente = Convert.ToInt32(dr["idCliente"]),
                            Cliente = new Usuario()
                            {
                                Nombre = dr["cliente"].ToString()
                            }
                        });
                    }
                }
            }

            return lista;
        }

        // Método para REGISTRAR pedido
        public int Registrar(Pedido obj, out string mensaje)
        {
            int idGenerado = 0;
            mensaje = string.Empty;

            using (SqlConnection conexion = ConexionBD.ObtenerConexion()) 
            {
                string query = @"INSERT INTO pedido (numeroPedido, fechaPedido, 
                                estado, observaciones, idCliente) 
                                VALUES (@numero, GETDATE(), 'Pendiente', 
                                @observaciones, @idCliente);
                                SELECT SCOPE_IDENTITY();";

                SqlCommand cmd = new SqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@numero", obj.NumeroPedido);
                cmd.Parameters.AddWithValue("@observaciones", obj.Observaciones);
                cmd.Parameters.AddWithValue("@idCliente", obj.IdCliente);

                conexion.Open();
                idGenerado = Convert.ToInt32(cmd.ExecuteScalar());
                mensaje = "Pedido registrado correctamente";
            }

            return idGenerado;
        }

        // Método para ACTUALIZAR estado del pedido
        public bool ActualizarEstado(int idPedido, string estado, out string mensaje)
        {
            bool respuesta = false;
            mensaje = string.Empty;

            using (SqlConnection conexion = ConexionBD.ObtenerConexion()) 
            {
                string query = "UPDATE pedido SET estado = @estado WHERE idPedido = @id";

                SqlCommand cmd = new SqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@id", idPedido);
                cmd.Parameters.AddWithValue("@estado", estado);

                conexion.Open();
                respuesta = cmd.ExecuteNonQuery() > 0;
                mensaje = "Estado actualizado correctamente";
            }

            return respuesta;
        }

        // Método para OBTENER pedidos por cliente
        public List<Pedido> ObtenerPorCliente(int idCliente)
        {
            List<Pedido> lista = new List<Pedido>();

            using (SqlConnection conexion = ConexionBD.ObtenerConexion()) 
            {
                string query = @"SELECT idPedido, numeroPedido, fechaPedido, 
                                estado, observaciones 
                                FROM pedido 
                                WHERE idCliente = @idCliente";

                SqlCommand cmd = new SqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@idCliente", idCliente);

                conexion.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new Pedido()
                        {
                            IdPedido = Convert.ToInt32(dr["idPedido"]),
                            NumeroPedido = dr["numeroPedido"].ToString(),
                            FechaPedido = Convert.ToDateTime(dr["fechaPedido"]),
                            Estado = dr["estado"].ToString(),
                            Observaciones = dr["observaciones"].ToString()
                        });
                    }
                }
            }

            return lista;
        }
    }
}