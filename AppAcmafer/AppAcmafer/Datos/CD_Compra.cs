using AppAcmafer.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AppAcmafer.Datos
{
    // Cambiamos el nombre a CD_Compra para seguir la convención de tu proyecto
    public class CD_Compra
    {
        // Solución al error CS0176 y CS0106:
        // El error CS0176 ocurre porque estás declarando 'conexionBD' como miembro de instancia (no estático)
        // en la línea 12, pero tu método Listar() y ObtenerConexion() son accedidos de forma estática o por un
        // helper sin instancia en otras clases. Vamos a eliminar la instancia de 'ConexionBD'
        // y asumir que 'ConexionBD.ObtenerConexion()' es un método estático o accesible globalmente, 
        // tal como se ve en CD_AsignacionTarea.cs.

        // private ConexionBD conexionBD = new ConexionBD(); // ELIMINAR ESTA LÍNEA

        public List<Compra> ObtenerTodasLasCompras()
        {
            List<Compra> compras = new List<Compra>();

            // Eliminamos la declaración SqlConnection conexion = null;
            // Solución al error CS0106 y CS0176: Usamos el patrón using seguro y consistente.
            // Esto también soluciona el error CS0106 ("ConexionBD no contiene una definición para 'CerrarConexion'")
            // al eliminar la necesidad de llamar a ese método.
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                try
                {
                    conexion.Open();

                    string query = @"SELECT c.idCompra, c.cantidad, c.valorTotal, c.descuento,
                                             c.idProducto, c.idPedido, 
                                             p.nombre as nombreProducto, 
                                             pe.numeroPedido
                                     FROM compra c
                                     INNER JOIN producto p ON c.idProducto = p.idProducto
                                     INNER JOIN pedido pe ON c.idPedido = pe.idPedido";

                    SqlCommand cmd = new SqlCommand(query, conexion);

                    // Usamos 'using' también para el DataReader para asegurar su liberación.
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
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
                }
                catch (Exception ex)
                {
                    // Manejo de errores (por ejemplo, registrar en consola)
                    Console.WriteLine("Error DB al obtener compras: " + ex.Message);
                    // Opcional: devolver lista vacía o manejar el error según la política de la aplicación.
                }
            } // El 'using' cierra la conexión aquí, eliminando el bloque finally.

            // Solución al error CS0161: No todas las rutas de acceso de código devuelven un valor
            return compras;
        }
    }
}