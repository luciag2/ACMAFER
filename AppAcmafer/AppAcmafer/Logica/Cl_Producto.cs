using AppAcmafer.Datos;
using System;
using System.Data;

namespace AppAcmafer.Logica
{
    public class CL_Producto
    {
        private CD_Producto productoDatos = new CD_Producto();

        // MÉTODO CORREGIDO - Ahora recibe int y decimal
        public bool ActualizarProducto(int idProducto, string nombre, string descripcion,
                                       string codigo, int stock, decimal precio, int idCategoria)
        {
            // Validaciones
            if (string.IsNullOrWhiteSpace(nombre))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(codigo))
            {
                return false;
            }

            if (stock < 0)
            {
                return false;
            }

            if (precio <= 0)
            {
                return false;
            }

            if (idCategoria == 0)
            {
                return false;
            }

            try
            {
                // Llama al método de la capa de datos con los parámetros correctos
                return productoDatos.ActualizarProducto(idProducto, nombre, descripcion,
                                                        codigo, stock, precio, idCategoria);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la capa lógica al actualizar producto: " + ex.Message);
            }
        }

        public DataTable ObtenerProductos()
        {
            try
            {
                return productoDatos.ListarProductos();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener productos: " + ex.Message);
            }
        }

        public DataRow ObtenerProducto(int idProducto)
        {
            try
            {
                return productoDatos.ObtenerProductoPorId(idProducto);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener producto: " + ex.Message);
            }
        }

        public DataTable ObtenerHistorial(int idProducto)
        {
            try
            {
                return productoDatos.ObtenerHistorialProducto(idProducto);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener historial: " + ex.Message);
            }
        }

        public bool ValidarCodigoUnico(string codigo, int idProducto = 0)
        {
            try
            {
                return productoDatos.ValidarCodigoUnico(codigo, idProducto);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al validar código: " + ex.Message);
            }
        }
    }
}