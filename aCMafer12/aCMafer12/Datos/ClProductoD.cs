using AppAcmafer.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AppAcmafer.Datos
{
    public class ClProductoD
    {
         ClConexion oConexion = new ClConexion();

       
        public List<ClCategoriaM> ListarCategoriasDB()
        {
            List<ClCategoriaM> lista = new List<ClCategoriaM>();
            string query = "SELECT idCategoria, nombre FROM categoria WHERE estado = 'Activo'";

            try
            {
                using (SqlConnection oConex = oConexion.MtAbrirConexion())
                {
                    SqlCommand command = new SqlCommand(query, oConex);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new ClCategoriaM()
                            {
                                IdCategoria = Convert.ToInt32(reader["idCategoria"]),
                                Nombre = reader["nombre"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                
                Console.WriteLine("Error al listar categorías: " + ex.Message);
            }
            finally
            {
                oConexion.MtCerrarConexion();
            }
            return lista;
        }

        
        public List<ClProductoM> ListarProductosPorCategoriaDB(int idCategoria)
        {
            List<ClProductoM> lista = new List<ClProductoM>();
            string query = "SELECT p.idProducto, p.nombre, p.descripcion, p.precio, p.stock, c.nombre AS Categoria " +
                           "FROM producto p INNER JOIN categoria c ON p.idCategoria = c.idCategoria " +
                           "WHERE (@IdCategoria = 0 OR p.idCategoria = @IdCategoria) AND p.estado = 'Activo'";

            try
            {
                using (SqlConnection oConex = oConexion.MtAbrirConexion())
                {
                    SqlCommand command = new SqlCommand(query, oConex);
                    command.Parameters.AddWithValue("@IdCategoria", idCategoria); 

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new ClProductoM()
                            {
                                IdProducto = Convert.ToInt32(reader["idProducto"]),
                                Nombre = reader["nombre"].ToString(),
                                Precio = Convert.ToDecimal(reader["precio"]),
                                Stock = Convert.ToInt32(reader["stock"]),
                                Categoria = reader["Categoria"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al listar productos: " + ex.Message);
            }
            finally
            {
                oConexion.MtCerrarConexion();
            }
            return lista;
        }
    }
}