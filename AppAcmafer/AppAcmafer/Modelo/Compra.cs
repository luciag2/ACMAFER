using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppAcmafer.Modelo
{
    [Serializable]
    public class Compra
    {
        public int IdCompra { get; set; }
        public int Cantidad { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal Descuento { get; set; }
        public int IdProducto { get; set; }
        public int IdPedido { get; set; }

        // Propiedades adicionales para mostrar
        public string NombreProducto { get; set; }
        public string NumeroPedido { get; set; }
    }
}