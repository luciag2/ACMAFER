using AppAcmafer.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AppAcmafer.Datos
{
    public class CD_Tarea
    {
        // Método para LISTAR tareas
        public List<Tarea> ObtenerTareasDisponiblles()
        {
            List<Tarea> lista = new List<Tarea>();

            using (SqlConnection conexion = ConexionBD.ObtenerConexion()) 
            {
                string query = "SELECT idTarea, titulo, descripcion FROM tarea WHERE estado = 'Activa'";
                SqlCommand cmd = new SqlCommand(query, conexion);


                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new Tarea()
                        {
                            IdTarea = Convert.ToInt32(dr["idTarea"]),
                            Titulo = dr["titulo"].ToString(),
                            Descripcion = dr["descripcion"].ToString(),
                            Prioridad = dr["prioridad"].ToString(),
                            Estado = dr["estado"].ToString()
                        });
                    }
                }
            }

            return lista;
        }

        // Método para REGISTRAR tarea
        public int Registrar(Tarea obj, out string mensaje)
        {
            int idGenerado = 0;
            mensaje = string.Empty;

            using (SqlConnection conexion = ConexionBD.ObtenerConexion()) 
            {
                string query = @"INSERT INTO tarea (titulo, descripcion, 
                                prioridad, estado) 
                                VALUES (@titulo, @descripcion, @prioridad, 'Pendiente');
                                SELECT SCOPE_IDENTITY();";

                SqlCommand cmd = new SqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@titulo", obj.Titulo);
                cmd.Parameters.AddWithValue("@descripcion", obj.Descripcion);
                cmd.Parameters.AddWithValue("@prioridad", obj.Prioridad);

                conexion.Open();
                idGenerado = Convert.ToInt32(cmd.ExecuteScalar());
                mensaje = "Tarea registrada correctamente";
            }

            return idGenerado;
        }

        // Método para ACTUALIZAR estado de tarea
        public bool ActualizarEstado(int idTarea, string estado, out string mensaje)
        {
            bool respuesta = false;
            mensaje = string.Empty;

            using (SqlConnection conexion = ConexionBD.ObtenerConexion()) 
            {
                string query = "UPDATE tarea SET estado = @estado WHERE idTarea = @id";

                SqlCommand cmd = new SqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@id", idTarea);
                cmd.Parameters.AddWithValue("@estado", estado);

                conexion.Open();
                respuesta = cmd.ExecuteNonQuery() > 0;
                mensaje = "Estado actualizado correctamente";
            }

            return respuesta;
        }
    }
}
