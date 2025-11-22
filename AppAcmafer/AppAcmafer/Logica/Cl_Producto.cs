using AppAcmafer.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppAcmafer.Logica
{
    public class CL_Producto
    {
        public bool ValidarStock(int stockActual, int cantidad)
        {
            return stockActual >= cantidad;
        }

        public decimal CalcularPrecioConDescuento(decimal precioUnitario, decimal descuento)
        {
            return precioUnitario - (precioUnitario * (descuento / 100));
        }

        public bool ValidarDatosProducto(Producto producto, out string mensaje)
        {
            mensaje = string.Empty;

            // Validar nombre
            if (string.IsNullOrEmpty(producto.Nombre))
            {
                mensaje = "El nombre es obligatorio";
                return false;
            }

            // Validar código
            if (string.IsNullOrEmpty(producto.Codigo))
            {
                mensaje = "El código es obligatorio";
                return false;
            }


            // Validar precio (es decimal, no string)
            if (producto.PrecioUnitario <= 0)
            {
                mensaje = "El precio es obligatorio y debe ser mayor a cero";
                return false;
            }

            // Validar categoría
            if (producto.IdCategoria == 0)
            {
                mensaje = "Debe seleccionar una categoría";
                return false;
            }

            return true;
        }

        public bool ValidarCodigoUnico(string codigo, int idProducto = 0)
        {
           
            return true;
        }
    }
}