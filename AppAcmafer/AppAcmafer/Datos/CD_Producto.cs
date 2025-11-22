using AppAcmafer.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AppAcmafer.Datos
{
    public class CD_Producto
    {
        // Método para LISTAR productos
        public List<Producto> Listar()
        {
            List<Producto> lista = new List<Producto>();

            using (SqlConnection conexion = ConexionBD.ObtenerConexion()) 
            {
                string query = @"SELECT p.idProducto, p.nombre, p.descripcion, 
                                p.codigo, p.stockActual, p.estado, p.fechaCreacion, 
                                p.precioUnitario, p.idCategoria, c.nombre AS categoria
                                FROM producto p
                                INNER JOIN categoria c ON p.idCategoria = c.idCategoria";

                SqlCommand cmd = new SqlCommand(query, conexion);
                conexion.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new Producto()
                        {
                            IdProducto = Convert.ToInt32(dr["idProducto"]),
                            Nombre = dr["nombre"].ToString(),
                            Descripcion = dr["descripcion"].ToString(),
                            Codigo = dr["codigo"].ToString(),
                            StockActual = dr["stockActual"].ToString(),
                            Estado = dr["estado"].ToString(),
                            FechaCreacion = Convert.ToDateTime(dr["fechaCreacion"]),
                            PrecioUnitario = Convert.ToDecimal(dr["precioUnitario"]),
                            IdCategoria = Convert.ToInt32(dr["idCategoria"]),
                            Categoria = new Categoria()
                            {
                                Nombre = dr["categoria"].ToString()
                            }
                        });
                    }
                }
            }

            return lista;
        }

        // Método para REGISTRAR producto
        public int Registrar(Producto obj, out string mensaje)
        {
            int idGenerado = 0;
            mensaje = string.Empty;

            using (SqlConnection conexion = ConexionBD.ObtenerConexion()) 
            {
                string query = @"INSERT INTO producto (nombre, descripcion, codigo, 
                                stockActual, estado, fechaCreacion, precioUnitario, idCategoria) 
                                VALUES (@nombre, @descripcion, @codigo, @stock, 
                                'Disponible', GETDATE(), @precio, @idCategoria);
                                SELECT SCOPE_IDENTITY();";

                SqlCommand cmd = new SqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@nombre", obj.Nombre);
                cmd.Parameters.AddWithValue("@descripcion", obj.Descripcion);
                cmd.Parameters.AddWithValue("@codigo", obj.Codigo);
                cmd.Parameters.AddWithValue("@stock", obj.StockActual);
                cmd.Parameters.AddWithValue("@precio", obj.PrecioUnitario);
                cmd.Parameters.AddWithValue("@idCategoria", obj.IdCategoria);

                conexion.Open();
                idGenerado = Convert.ToInt32(cmd.ExecuteScalar());
                mensaje = "Producto registrado correctamente";
            }

            return idGenerado;
        }

        // Método para EDITAR producto
        public bool Editar(Producto obj, out string mensaje)
        {
            bool respuesta = false;
            mensaje = string.Empty;

            using (SqlConnection conexion = ConexionBD.ObtenerConexion()) 
            {
                string query = @"UPDATE producto SET nombre = @nombre, 
                                descripcion = @descripcion, codigo = @codigo, 
                                stockActual = @stock, precioUnitario = @precio, 
                                idCategoria = @idCategoria 
                                WHERE idProducto = @id";

                SqlCommand cmd = new SqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@id", obj.IdProducto);
                cmd.Parameters.AddWithValue("@nombre", obj.Nombre);
                cmd.Parameters.AddWithValue("@descripcion", obj.Descripcion);
                cmd.Parameters.AddWithValue("@codigo", obj.Codigo);
                cmd.Parameters.AddWithValue("@stock", obj.StockActual);
                cmd.Parameters.AddWithValue("@precio", obj.PrecioUnitario);
                cmd.Parameters.AddWithValue("@idCategoria", obj.IdCategoria);

                conexion.Open();
                respuesta = cmd.ExecuteNonQuery() > 0;
                mensaje = "Producto editado correctamente";
            }

            return respuesta;
        }

        // Método para ELIMINAR producto
        public bool Eliminar(int id, out string mensaje)
        {
            bool respuesta = false;
            mensaje = string.Empty;

            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                string query = "UPDATE producto SET estado = 'Inactivo' WHERE idProducto = @id";

                SqlCommand cmd = new SqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@id", id);

                conexion.Open();
                respuesta = cmd.ExecuteNonQuery() > 0;
                mensaje = "Producto eliminado correctamente";
            }

            return respuesta;
        }

        // Método para BUSCAR producto por código
        public Producto BuscarPorCodigo(string codigo)
        {
            Producto producto = null;

            using (SqlConnection conexion = ConexionBD.ObtenerConexion()) 
            {
                string query = @"SELECT p.idProducto, p.nombre, p.descripcion, 
                                p.codigo, p.stockActual, p.precioUnitario, p.idCategoria
                                FROM producto p
                                WHERE p.codigo = @codigo AND p.estado = 'Disponible'";

                SqlCommand cmd = new SqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@codigo", codigo);

                conexion.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        producto = new Producto()
                        {
                            IdProducto = Convert.ToInt32(dr["idProducto"]),
                            Nombre = dr["nombre"].ToString(),
                            Descripcion = dr["descripcion"].ToString(),
                            Codigo = dr["codigo"].ToString(),
                            StockActual = dr["stockActual"].ToString(),
                            PrecioUnitario = Convert.ToDecimal(dr["precioUnitario"]),
                            IdCategoria = Convert.ToInt32(dr["idCategoria"])
                        };
                    }
                }
            }

            return producto;
        }
    }
}

