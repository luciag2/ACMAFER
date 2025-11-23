using AppAcmafer.Modelo;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AppAcmafer.Datos
{
    public class CD_Categoria
    {
        // Método para LISTAR categorías
        public List<Categoria> ListarCategoria()
        {
            List<Categoria> lista = new List<Categoria>();

            using (SqlConnection conexion = ConexionBD.ObtenerConexion()) 
            {
                string query = "SELECT idCategoria, nombre, descripcion FROM categoria";

                SqlCommand cmd = new SqlCommand(query, conexion);
                conexion.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new Categoria()
                        {
                            IdCategoria = Convert.ToInt32(dr["idCategoria"]),
                            Nombre = dr["nombre"].ToString(),
                            Descripcion = dr["descripcion"].ToString()
                        });
                    }
                }
            }

            return lista;
        }
    
    public DataTable ListarCategorias()
        {
            DataTable dt = new DataTable();
            string conexion = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(conexion))
            {
                string query = "SELECT idCategoria, nombre FROM categoria";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }
    }
}
