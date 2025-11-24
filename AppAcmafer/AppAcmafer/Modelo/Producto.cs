using AppAcmafer.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace AppAcmafer.Modelo
{
    [Serializable]
    public class Producto
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Codigo { get; set; }
        public int StockActual { get; set; }  // ✅ CAMBIADO de string a int
        public string Estado { get; set; }
        public byte[] Imagen { get; set; }
        public DateTime FechaCreacion { get; set; }
        public decimal PrecioUnitario { get; set; }
        public int IdCategoria { get; set; }
        // Propiedad adicional
        public string NombreCategoria { get; set; }

        // Navegación
        public Categoria Categoria { get; set; }

        // Constructor
        public Producto()
        {
            IdProducto = 0;
            Nombre = string.Empty;
            Descripcion = string.Empty;
            Codigo = string.Empty;
            StockActual = 0;  // ✅ CAMBIADO de "0" a 0
            Estado = "Disponible";
            Imagen = null;
            FechaCreacion = DateTime.Now;
            PrecioUnitario = 0;
            IdCategoria = 0;
            Categoria = null;
        }
    }
}