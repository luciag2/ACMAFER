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
        

        public List<Tarea> ObtenerTodasLasTareas() // O el método que sea
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

                string query = @"TU QUERY AQUÍ";

                SqlCommand cmd = new SqlCommand(query, conexion);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    // Tu lógica de lectura
                }
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }

                if (conexion != null && conexion.State == System.Data.ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return tareas;
        }
    }
}