using System;
using System.Data;
using System.Data.SqlClient;

namespace AppAcmafer.Datos
{
    public class CD_Producto
    {
        // ============ LISTAR TODOS LOS PRODUCTOS ============
        public DataTable ListarProductos()
        {
            DataTable dt = new DataTable();
            SqlConnection conexion = null;

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
                            p.fechaCreacion,
                            p.idCategoria,
                            c.nombre as nombreCategoria
                        FROM producto p
                        INNER JOIN categoria c ON p.idCategoria = c.idCategoria
                        ORDER BY p.nombre";

                    SqlCommand cmd = new SqlCommand(query, conexion);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar productos: " + ex.Message);
            }
            finally
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return dt;
        }

        // ============ OBTENER PRODUCTO POR ID ============
        public DataRow ObtenerProductoPorId(int idProducto)
        {
            DataTable dt = new DataTable();
            SqlConnection conexion = null;

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
                            p.fechaCreacion,
                            p.idCategoria,
                            c.nombre as nombreCategoria
                        FROM producto p
                        INNER JOIN categoria c ON p.idCategoria = c.idCategoria
                        WHERE p.idProducto = @IdProducto";

                    SqlCommand cmd = new SqlCommand(query, conexion);
                    cmd.Parameters.AddWithValue("@IdProducto", idProducto);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener producto: " + ex.Message);
            }
            finally
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return dt.Rows.Count > 0 ? dt.Rows[0] : null;
        }

        // ============ ACTUALIZAR PRODUCTO ============
        public bool ActualizarProducto(int idProducto, string nombre, string descripcion,
                                      string codigo, int stock, decimal precio, int idCategoria)
        {
            SqlConnection conexion = null;

            try
            {
                conexion = ConexionBD.ObtenerConexion();
                if (conexion != null)
                {
                    conexion.Open();

                    string query = @"
                        UPDATE producto 
                        SET 
                            nombre = @Nombre,
                            descripcion = @Descripcion,
                            codigo = @Codigo,
                            stockActual = @Stock,
                            precioUnitario = @Precio,
                            idCategoria = @IdCategoria
                        WHERE idProducto = @IdProducto";

                    SqlCommand cmd = new SqlCommand(query, conexion);
                    cmd.Parameters.AddWithValue("@IdProducto", idProducto);
                    cmd.Parameters.AddWithValue("@Nombre", nombre);
                    cmd.Parameters.AddWithValue("@Descripcion", descripcion);
                    cmd.Parameters.AddWithValue("@Codigo", codigo);
                    cmd.Parameters.AddWithValue("@Stock", stock);
                    cmd.Parameters.AddWithValue("@Precio", precio);
                    cmd.Parameters.AddWithValue("@IdCategoria", idCategoria);

                    int filasAfectadas = cmd.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar producto: " + ex.Message);
            }
            finally
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }
        }

        // ============ OBTENER HISTORIAL DE PRODUCTO ============
        public DataTable ObtenerHistorialProducto(int idProducto)
        {
            DataTable dt = new DataTable();
            SqlConnection conexion = null;

            try
            {
                conexion = ConexionBD.ObtenerConexion();
                if (conexion != null)
                {
                    conexion.Open();

                    // Simulamos un historial con la fecha de creación del producto
                    // Si tienes una tabla de historial, cambia este query
                    string query = @"
                        SELECT 
                            p.idProducto,
                            p.fechaCreacion as Fecha,
                            'Creación del producto' as Accion,
                            p.nombre as NombreProducto,
                            p.stockActual as Stock,
                            p.precioUnitario as Precio
                        FROM producto p
                        WHERE p.idProducto = @IdProducto
                        ORDER BY p.fechaCreacion DESC";

                    SqlCommand cmd = new SqlCommand(query, conexion);
                    cmd.Parameters.AddWithValue("@IdProducto", idProducto);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener historial: " + ex.Message);
            }
            finally
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return dt;
        }

        // ============ VALIDAR CÓDIGO ÚNICO ============
        public bool ValidarCodigoUnico(string codigo, int idProducto = 0)
        {
            SqlConnection conexion = null;
            SqlDataReader reader = null;

            try
            {
                conexion = ConexionBD.ObtenerConexion();
                if (conexion != null)
                {
                    conexion.Open();

                    string query = @"
                        SELECT COUNT(*) 
                        FROM producto 
                        WHERE codigo = @Codigo 
                        AND idProducto != @IdProducto";

                    SqlCommand cmd = new SqlCommand(query, conexion);
                    cmd.Parameters.AddWithValue("@Codigo", codigo);
                    cmd.Parameters.AddWithValue("@IdProducto", idProducto);

                    int count = (int)cmd.ExecuteScalar();
                    return count == 0; // true si es único
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al validar código: " + ex.Message);
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
        }

        // ============ INSERTAR PRODUCTO ============
        public bool InsertarProducto(string nombre, string descripcion, string codigo,
                                    int stock, decimal precio, int idCategoria)
        {
            SqlConnection conexion = null;

            try
            {
                conexion = ConexionBD.ObtenerConexion();
                if (conexion != null)
                {
                    conexion.Open();

                    string query = @"
                        INSERT INTO producto 
                        (nombre, descripcion, codigo, stockActual, estado, fechaCreacion, precioUnitario, idCategoria)
                        VALUES 
                        (@Nombre, @Descripcion, @Codigo, @Stock, 'Disponible', GETDATE(), @Precio, @IdCategoria)";

                    SqlCommand cmd = new SqlCommand(query, conexion);
                    cmd.Parameters.AddWithValue("@Nombre", nombre);
                    cmd.Parameters.AddWithValue("@Descripcion", descripcion);
                    cmd.Parameters.AddWithValue("@Codigo", codigo);
                    cmd.Parameters.AddWithValue("@Stock", stock);
                    cmd.Parameters.AddWithValue("@Precio", precio);
                    cmd.Parameters.AddWithValue("@IdCategoria", idCategoria);

                    int filasAfectadas = cmd.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar producto: " + ex.Message);
            }
            finally
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }
        }

        // ============ ELIMINAR/DESACTIVAR PRODUCTO ============
        public bool EliminarProducto(int idProducto)
        {
            SqlConnection conexion = null;

            try
            {
                conexion = ConexionBD.ObtenerConexion();
                if (conexion != null)
                {
                    conexion.Open();

                    string query = @"
                        UPDATE producto 
                        SET estado = 'No Disponible'
                        WHERE idProducto = @IdProducto";

                    SqlCommand cmd = new SqlCommand(query, conexion);
                    cmd.Parameters.AddWithValue("@IdProducto", idProducto);

                    int filasAfectadas = cmd.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar producto: " + ex.Message);
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