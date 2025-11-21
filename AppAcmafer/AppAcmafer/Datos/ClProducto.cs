using AppAcmafer.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AppAcmafer.Datos
{
    public class ClProducto
    {
        public List<ClProductoM> ListarProductosPorCategoria(int idCategoria)
        {
            List<ClProductoM> lista = new List<ClProductoM>();
            using (SqlConnection oConex = new ClConexion().MtAbrirConexion())
            {


                string query = "SELECT p.idProducto, p.nombre, p.descripcion, p.precio, p.stock, c.nombre AS Categoria " +
                           "FROM producto p INNER JOIN categoria c ON p.idCategoria = c.idCategoria " +
                           "WHERE @IdCategoria = 0 OR p.idCategoria = @IdCategoria AND p.estado = 'Activo'";

          
            SqlCommand command = new SqlCommand(query, oConex);
            command.Parameters.AddWithValue("@IdCategoria", idCategoria); 


            return lista;
                
            }
        }
    }
}