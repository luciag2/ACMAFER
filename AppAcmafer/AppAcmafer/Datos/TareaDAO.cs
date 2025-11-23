using AppAcmafer.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace AppAcmafer.Datos
{
    public class TareaDAO
    {
        // MÉTODO QUE FALTABA - Este es el que necesitas
        public List<AsignacionTarea> ObtenerAsignacionesTareas()
        {
            List<AsignacionTarea> asignaciones = new List<AsignacionTarea>();
            SqlConnection conexion = null;
            SqlDataReader reader = null;

            try
            {
                conexion = ConexionBD.ObtenerConexion();
                if (conexion != null)
                {
                    conexion.Open();
                }

                string query = @"
                    SELECT 
                        at.idAsignacionTarea,
                        at.fechaInicio,
                        at.horaInicio,
                        at.fechaFin,
                        at.horaFin,
                        at.comentarioAdmin,
                        t.titulo AS TituloTarea,
                        t.descripcion AS DescripcionTarea,
                        t.prioridad,
                        t.estado AS EstadoTarea,
                        emp.nombre + ' ' + emp.apellido AS NombreEmpleado,
                        adm.nombre + ' ' + adm.apellido AS NombreAdmin
                    FROM asignacionTarea at
                    INNER JOIN tarea t ON at.idTarea = t.idTarea
                    INNER JOIN usuario emp ON at.idEmpleado = emp.idUsuario
                    INNER JOIN usuario adm ON at.idAdmin = adm.idUsuario
                    ORDER BY at.fechaInicio DESC";

                SqlCommand cmd = new SqlCommand(query, conexion);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    AsignacionTarea asignacion = new AsignacionTarea
                    {
                        IdAsignacionTarea = Convert.ToInt32(reader["idAsignacionTarea"]),
                        FechaInicio = Convert.ToDateTime(reader["fechaInicio"]),
                        HoraInicio = reader["horaInicio"].ToString(),
                        FechaFin = Convert.ToDateTime(reader["fechaFin"]),
                        HoraFin = reader["horaFin"].ToString(),
                        ComentarioAdmin = reader["comentarioAdmin"].ToString(),
                        TituloTarea = reader["TituloTarea"].ToString(),
                        DescripcionTarea = reader["DescripcionTarea"].ToString(),
                        Prioridad = reader["prioridad"].ToString(),
                        EstadoTarea = reader["EstadoTarea"].ToString(),
                        NombreEmpleado = reader["NombreEmpleado"].ToString(),
                        NombreAdmin = reader["NombreAdmin"].ToString()
                    };

                    asignaciones.Add(asignacion);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener asignaciones de tareas: " + ex.Message);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (conexion != null && conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return asignaciones;
        }

        // Tu método original
        public List<Tarea> ObtenerTodasLasTareas()
        {
            List<Tarea> tareas = new List<Tarea>();
            SqlConnection conexion = null;
            SqlDataReader reader = null;

            try
            {
                conexion = ConexionBD.ObtenerConexion();
                if (conexion != null)
                {
                    conexion.Open();
                }

                string query = @"SELECT idTarea, titulo, descripcion, prioridad, estado 
                                FROM tarea 
                                ORDER BY idTarea";

                SqlCommand cmd = new SqlCommand(query, conexion);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Tarea tarea = new Tarea
                    {
                        IdTarea = Convert.ToInt32(reader["idTarea"]),
                        Titulo = reader["titulo"].ToString(),
                        Descripcion = reader["descripcion"].ToString(),
                        Prioridad = reader["prioridad"].ToString(),
                        Estado = reader["estado"].ToString()
                    };

                    tareas.Add(tarea);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener tareas: " + ex.Message);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (conexion != null && conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return tareas;
        }
    }
}

namespace AppAcmafer.Datos
{
    public class ClProductoDAO
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
                            StockActual = Convert.ToInt32(reader["stockActual"]),
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

        // ============ MÉTODO PARA ACTUALIZAR PRODUCTO ============
        public bool ActualizarProducto(Producto producto)
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
                            stockActual = @StockActual,
                            estado = @Estado,
                            precioUnitario = @PrecioUnitario,
                            idCategoria = @IdCategoria
                        WHERE idProducto = @IdProducto";

                    SqlCommand cmd = new SqlCommand(query, conexion);
                    cmd.Parameters.AddWithValue("@IdProducto", producto.IdProducto);
                    cmd.Parameters.AddWithValue("@Nombre", producto.Nombre);
                    cmd.Parameters.AddWithValue("@Descripcion", producto.Descripcion);
                    cmd.Parameters.AddWithValue("@Codigo", producto.Codigo);
                    cmd.Parameters.AddWithValue("@StockActual", producto.StockActual);
                    cmd.Parameters.AddWithValue("@Estado", producto.Estado);
                    cmd.Parameters.AddWithValue("@PrecioUnitario", producto.PrecioUnitario);
                    cmd.Parameters.AddWithValue("@IdCategoria", producto.IdCategoria);

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

        // ============ MÉTODO PARA INSERTAR PRODUCTO ============
        public bool InsertarProducto(Producto producto)
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
                        (@Nombre, @Descripcion, @Codigo, @StockActual, @Estado, @FechaCreacion, @PrecioUnitario, @IdCategoria)";

                    SqlCommand cmd = new SqlCommand(query, conexion);
                    cmd.Parameters.AddWithValue("@Nombre", producto.Nombre);
                    cmd.Parameters.AddWithValue("@Descripcion", producto.Descripcion);
                    cmd.Parameters.AddWithValue("@Codigo", producto.Codigo);
                    cmd.Parameters.AddWithValue("@StockActual", producto.StockActual);
                    cmd.Parameters.AddWithValue("@Estado", producto.Estado);
                    cmd.Parameters.AddWithValue("@FechaCreacion", DateTime.Now);
                    cmd.Parameters.AddWithValue("@PrecioUnitario", producto.PrecioUnitario);
                    cmd.Parameters.AddWithValue("@IdCategoria", producto.IdCategoria);

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

        // ============ MÉTODO PARA ELIMINAR/DESACTIVAR PRODUCTO ============
        public bool EliminarProducto(int idProducto)
        {
            SqlConnection conexion = null;

            try
            {
                conexion = ConexionBD.ObtenerConexion();
                if (conexion != null)
                {
                    conexion.Open();

                    // Cambiar estado en lugar de eliminar físicamente
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