using AppAcmafer.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AppAcmafer.Datos
{
    public class CD_AsignacionTarea
    {
        // Método para ASIGNAR tarea a empleado
        public int AsignarTarea(AsignacionTarea obj, out string mensaje)
        {
            int idGenerado = 0;
            mensaje = string.Empty;

            using (SqlConnection conexion = ConexionBD.ObtenerConexion()) 
            {
                string query = @"INSERT INTO asignacionTarea (idTarea, idEmpleado, 
                                idAdmin, fechaInicio, horaInicio, fechaFin, horaFin, 
                                comentarioAdmin) 
                                VALUES (@idTarea, @idEmpleado, @idAdmin, @fechaInicio, 
                                @horaInicio, @fechaFin, @horaFin, @comentario);
                                SELECT SCOPE_IDENTITY();";

                SqlCommand cmd = new SqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@idTarea", obj.IdTarea);
                cmd.Parameters.AddWithValue("@idEmpleado", obj.IdEmpleado);
                cmd.Parameters.AddWithValue("@idAdmin", obj.IdAdmin);
                cmd.Parameters.AddWithValue("@fechaInicio", obj.FechaInicio);
                cmd.Parameters.AddWithValue("@horaInicio", obj.HoraInicio);
                cmd.Parameters.AddWithValue("@fechaFin", obj.FechaFin);
                cmd.Parameters.AddWithValue("@horaFin", obj.HoraFin);
                cmd.Parameters.AddWithValue("@comentario", obj.ComentarioAdmin);

                conexion.Open();
                idGenerado = Convert.ToInt32(cmd.ExecuteScalar());
                mensaje = "Tarea asignada correctamente";
            }

            return idGenerado;
        }


        // Método para LISTAR asignaciones
        public List<AsignacionTarea> ListarAsignaciones()
        {
            List<AsignacionTarea> lista = new List<AsignacionTarea>();

            using (SqlConnection conexion = ConexionBD.ObtenerConexion()) 
            {
                string query = @"SELECT at.idAsignacionTarea, at.fechaInicio, 
                                at.fechaFin, at.comentarioAdmin,
                                t.titulo AS tarea,
                                e.nombre + ' ' + e.apellido AS empleado,
                                a.nombre + ' ' + a.apellido AS admin
                                FROM asignacionTarea at
                                INNER JOIN tarea t ON at.idTarea = t.idTarea
                                INNER JOIN usuario e ON at.idEmpleado = e.idUsuario
                                INNER JOIN usuario a ON at.idAdmin = a.idUsuario";

                SqlCommand cmd = new SqlCommand(query, conexion);
                conexion.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new AsignacionTarea()
                        {
                            IdAsignacionTarea = Convert.ToInt32(dr["idAsignacionTarea"]),
                            FechaInicio = Convert.ToDateTime(dr["fechaInicio"]),
                            FechaFin = Convert.ToDateTime(dr["fechaFin"]),
                            ComentarioAdmin = dr["comentarioAdmin"].ToString()
                        });
                    }
                }
            }

            return lista;
        }

        // Método para AGREGAR comentario a una tarea
        public int AgregarComentario(ComentarioTarea obj, out string mensaje)
        {
            int idGenerado = 0;
            mensaje = string.Empty;

            using (SqlConnection conexion = ConexionBD.ObtenerConexion()) 
            {
                try
                {
                    string query = @"INSERT INTO comentarioTarea (fechaComentario, 
                                comentario, idAsignacionTarea) 
                                VALUES (GETDATE(), @comentario, @idAsignacion);
                                SELECT SCOPE_IDENTITY();";
                    SqlCommand cmd = new SqlCommand(query, conexion);
                    cmd.Parameters.AddWithValue("@comentario", obj.Comentario);
                    cmd.Parameters.AddWithValue("@idAsignacion", obj.IdAsignacionTarea);

                    object resultado = cmd.ExecuteScalar();

                    if (resultado != null && int.TryParse(resultado.ToString(), out idGenerado))
                    {
                        mensaje = "Comentario agregado correctamente.";
                    }
                    else
                    {
                        mensaje = "Error al agregar el comentario.";
                    }
                }
                catch (Exception ex)
                {
                    mensaje = "Error en la base de datos: " + ex.Message;
                }
            }
            // 3. CORREGIDO: Debe retornar un int
            return idGenerado;
        }



            
                
        
    }
}