using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AppAcmafer.Datos
{
    public class ConexionBD
    {
        // Método para obtener la conexión
        public static SqlConnection ObtenerConexion()
        {
            try
            {
                // Verificar que existe la cadena de conexión
                if (ConfigurationManager.ConnectionStrings["CadenaConexion"] == null)
                {
                    throw new Exception("No se encontró la cadena de conexión 'CadenaConexion' en Web.config");
                }

                string connectionString = ConfigurationManager.ConnectionStrings["CadenaConexion"].ConnectionString;

                // Verificar que no esté vacía
                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new Exception("La cadena de conexión 'CadenaConexion' está vacía");
                }

                // Crear y retornar el objeto SqlConnection
                SqlConnection conexion = new SqlConnection(connectionString);
                return conexion;
            }
            catch (Exception ex)
            {
                // Log del error para debugging
                Console.WriteLine("Error al obtener la cadena de conexión: " + ex.Message);
                throw new Exception("Error al obtener la cadena de conexión: " + ex.Message);
            }
        }

        // Método alternativo para cerrar conexión (opcional, pero útil)
        public static void CerrarConexion(SqlConnection conexion)
        {
            try
            {
                if (conexion != null && conexion.State == System.Data.ConnectionState.Open)
                {
                    conexion.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cerrar conexión: " + ex.Message);
            }
        }
    }
}
