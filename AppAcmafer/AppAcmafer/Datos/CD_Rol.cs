using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace AppAcmafer.Datos
{
    public class CD_Rol
    {
        public DataTable ObtenerRoles()
        {
            DataTable dt = new DataTable();

            try
            {
                string conexion = ConfigurationManager.ConnectionStrings["CadenaConexion"]?.ConnectionString;
                if (string.IsNullOrEmpty(conexion))
                {
                    throw new Exception("No se encontró la cadena de conexión");
                }

                using (SqlConnection conn = new SqlConnection(conexion))
                {
                    string query = "SELECT * FROM dbo.rol WHERE estado = 'Activo'";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dt;
        }
    }
}