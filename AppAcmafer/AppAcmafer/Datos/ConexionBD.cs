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
       
       
            public static SqlConnection ObtenerConexion()
            {
                try
                {
                    
                    string connectionString = ConfigurationManager.ConnectionStrings["CadenaConexion"].ConnectionString;

                    // 2. Crear y retornar el objeto SqlConnection.
                    SqlConnection conexion = new SqlConnection(connectionString);

                    

                    return conexion;
                }
                catch (Exception ex)
                {
                   
                    Console.WriteLine("Error al obtener la cadena de conexión: " + ex.Message);
                    return null;
                }
            }

            // El método CerrarConexion ya no es necesario si usas el patrón 'using'
            // en tus clases DAO (como lo hicimos en CD_Compra y como lo hace CD_AsignacionTarea).
            /*
            public void CerrarConexion(SqlConnection conexion)
            {
                if (conexion != null && conexion.State == System.Data.ConnectionState.Open)
                {
                    conexion.Close();
                }
            }
            */
    }
    
}