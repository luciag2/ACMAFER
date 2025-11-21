using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppAcmafer.Modelo
{
    [Serializable]
    public class Producto
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Codigo { get; set; }
        public int StockActual { get; set; }
        public string Estado { get; set; }
        public decimal PrecioUnitario { get; set; }
        public int IdCategoria { get; set; }

        // Propiedad adicional
        public string NombreCategoria { get; set; }
    }
}