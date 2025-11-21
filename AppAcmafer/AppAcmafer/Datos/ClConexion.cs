using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;

namespace AppAcmafer.Datos
{
    public class ClConexion
    {

        public SqlConnection oConex;

        public ClConexion()
        {
            oConex = new SqlConnection("Data Source=.;Initial Catalog=ProyectoACMAFER;Integrated Security=True;");
        }

        public SqlConnection MtAbrirConexion()
        {
            oConex.Open();
            return oConex;
        }

        public DataTable ObtenerTabla(string consultaSQL)
        {
            DataTable dtResultados = new DataTable();

            try
            {
                MtAbrirConexion();
                SqlCommand cmd = new SqlCommand(consultaSQL, oConex);
                SqlDataAdapter adaptador = new SqlDataAdapter(cmd);
                adaptador.Fill(dtResultados);
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR DE CONEXIÓN O CONSULTA: " + ex.Message, ex);
            }
            finally
            {
                MtCerrarConexion();
            }
            using (SqlConnection oConex = MtAbrirConexion()) // Usamos tu método existente MtAbrirConexion()
            {
                using (SqlCommand oComando = new SqlCommand(consultaSQL, oConex))
                {
                    using (SqlDataAdapter oAdaptador = new SqlDataAdapter(oComando))
                    {
                        DataTable dt = new DataTable();
                        oAdaptador.Fill(dt);
                        // MtCerrarConexion(oConex); // Si MtAbrirConexion devuelve el objeto, lo cerramos aquí.
                        return dt;
                    }
                }
            }
        }
        public int EjecutarComando(string consultaSQL)
        {
            int filasAfectadas = 0;

            // Usamos 'using' para asegurar que la conexión se cierre
            using (SqlConnection oConex = MtAbrirConexion())
            {
                using (SqlCommand oComando = new SqlCommand(consultaSQL, oConex))
                {
                    try
                    {
                        filasAfectadas = oComando.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        // Aquí podrías loguear el error de SQL
                        throw new Exception("Error al ejecutar el comando SQL: " + ex.Message, ex);
                    }
                    finally
                    {
                       
                    }
                }
            }
            return filasAfectadas;
        }
        public void MtCerrarConexion()
        {
            if (oConex != null && oConex.State == ConnectionState.Open)
            {
                oConex.Close();
            }
        }

        public void MtCerrarConexion(SqlConnection conexionACerrar)
        {
            if (conexionACerrar != null && conexionACerrar.State == ConnectionState.Open)
            {
                conexionACerrar.Close();
            }
        }
    }
}
    


    

