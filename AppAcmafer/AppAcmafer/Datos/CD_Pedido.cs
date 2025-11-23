using AppAcmafer.Modelo;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
    
     // Crear pedido con validación de stock
        public bool CrearPedido(string numeroPedido, int idCliente, string observaciones,
                               int idProducto, int cantidad, out string mensaje)
        {
            mensaje = "";
            string conexion = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(conexion))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    // 1. Verificar stock disponible
                    string queryStock = "SELECT stockActual FROM producto WHERE idProducto = @idProducto";
                    SqlCommand cmdStock = new SqlCommand(queryStock, conn, transaction);
                    cmdStock.Parameters.AddWithValue("@idProducto", idProducto);

                    object stockObj = cmdStock.ExecuteScalar();
                    int stockActual = stockObj != null ? Convert.ToInt32(stockObj) : 0;

                    // 2. Validar que hay suficiente stock
                    if (cantidad > stockActual)
                    {
                        mensaje = $"❌ Stock insuficiente. Disponible: {stockActual}, Solicitado: {cantidad}";
                        transaction.Rollback();
                        return false;
                    }

                    // 3. Crear el pedido
                    string queryPedido = @"INSERT INTO pedido (numeroPedido, fechaPedido, estado, observaciones, idCliente) 
                                          VALUES (@numeroPedido, GETDATE(), 'Pendiente', @observaciones, @idCliente);
                                          SELECT SCOPE_IDENTITY();";
                    SqlCommand cmdPedido = new SqlCommand(queryPedido, conn, transaction);
                    cmdPedido.Parameters.AddWithValue("@numeroPedido", numeroPedido);
                    cmdPedido.Parameters.AddWithValue("@observaciones", observaciones);
                    cmdPedido.Parameters.AddWithValue("@idCliente", idCliente);

                    int idPedido = Convert.ToInt32(cmdPedido.ExecuteScalar());

                    // 4. Obtener precio del producto
                    string queryPrecio = "SELECT precioUnitario FROM producto WHERE idProducto = @idProducto";
                    SqlCommand cmdPrecio = new SqlCommand(queryPrecio, conn, transaction);
                    cmdPrecio.Parameters.AddWithValue("@idProducto", idProducto);
                    decimal precioUnitario = Convert.ToDecimal(cmdPrecio.ExecuteScalar());
                    decimal valorTotal = precioUnitario * cantidad;

                    // 5. Crear la compra
                    string queryCompra = @"INSERT INTO compra (cantidad, valorTotal, descuento, idProducto, idPedido)
                                          VALUES (@cantidad, @valorTotal, '0', @idProducto, @idPedido)";
                    SqlCommand cmdCompra = new SqlCommand(queryCompra, conn, transaction);
                    cmdCompra.Parameters.AddWithValue("@cantidad", cantidad.ToString());
                    cmdCompra.Parameters.AddWithValue("@valorTotal", valorTotal.ToString());
                    cmdCompra.Parameters.AddWithValue("@idProducto", idProducto);
                    cmdCompra.Parameters.AddWithValue("@idPedido", idPedido);
                    cmdCompra.ExecuteNonQuery();

                    // 6. Actualizar el stock
                    string queryActualizarStock = @"UPDATE producto 
                                                   SET stockActual = stockActual - @cantidad 
                                                   WHERE idProducto = @idProducto";
                    SqlCommand cmdActualizarStock = new SqlCommand(queryActualizarStock, conn, transaction);
                    cmdActualizarStock.Parameters.AddWithValue("@cantidad", cantidad);
                    cmdActualizarStock.Parameters.AddWithValue("@idProducto", idProducto);
                    cmdActualizarStock.ExecuteNonQuery();

                    transaction.Commit();
                    mensaje = $"✅ Pedido creado exitosamente. Nuevo stock: {stockActual - cantidad}";
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    mensaje = "❌ Error al crear el pedido: " + ex.Message;
                    return false;
                }
            }
        }

        // Listar pedidos
        public DataTable ListarPedidos()
        {
            DataTable dt = new DataTable();
            string conexion = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(conexion))
            {
                string query = @"SELECT p.idPedido, p.numeroPedido, p.fechaPedido, p.estado,
                                u.nombre + ' ' + u.apellido as Cliente, p.observaciones
                                FROM pedido p
                                INNER JOIN usuario u ON p.idCliente = u.idUsuario
                                ORDER BY p.fechaPedido DESC";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }
    }
}
