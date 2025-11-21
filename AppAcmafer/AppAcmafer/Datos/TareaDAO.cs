using AppAcmafer.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AppAcmafer.Datos
{
    public class TareaDAO
    {
        private ConexionBD conexionBD = new ConexionBD();

        public List<AsignacionTarea> ObtenerAsignacionesTareas()
        {
            List<AsignacionTarea> asignaciones = new List<AsignacionTarea>();
            SqlConnection conexion = null;

            try
            {
                conexion = conexionBD.ObtenerConexion();
                string query = @"SELECT at.idAsignacionTarea, at.idTarea, at.idEmpleado, at.idAdmin,
                                at.fechaInicio, at.horaInicio, at.fechaFin, at.horaFin, at.comentarioAdmin,
                                t.titulo as tituloTarea,
                                CONCAT(ue.nombre, ' ', ue.apellido) as nombreEmpleado,
                                CONCAT(ua.nombre, ' ', ua.apellido) as nombreAdmin
                                FROM asignacionTarea at
                                INNER JOIN tarea t ON at.idTarea = t.idTarea
                                INNER JOIN usuario ue ON at.idEmpleado = ue.idUsuario
                                INNER JOIN usuario ua ON at.idAdmin = ua.idUsuario";

                SqlCommand cmd = new SqlCommand(query, conexion);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    asignaciones.Add(new AsignacionTarea
                    {
                        IdAsignacionTarea = Convert.ToInt32(reader["idAsignacionTarea"]),
                        IdTarea = Convert.ToInt32(reader["idTarea"]),
                        IdEmpleado = Convert.ToInt32(reader["idEmpleado"]),
                        IdAdmin = Convert.ToInt32(reader["idAdmin"]),
                        FechaInicio = Convert.ToDateTime(reader["fechaInicio"]),
                        HoraInicio = reader["horaInicio"].ToString(),
                        FechaFin = Convert.ToDateTime(reader["fechaFin"]),
                        HoraFin = reader["horaFin"].ToString(),
                        ComentarioAdmin = reader["comentarioAdmin"].ToString(),
                        TituloTarea = reader["tituloTarea"].ToString(),
                        NombreEmpleado = reader["nombreEmpleado"].ToString(),
                        NombreAdmin = reader["nombreAdmin"].ToString()
                    });
                }
            }
            finally
            {
                conexionBD.CerrarConexion(conexion);
            }

            return asignaciones;
        }
    }
}