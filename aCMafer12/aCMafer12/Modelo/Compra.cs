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

        // Navegación
        public Producto Producto { get; set; }
        public Pedido Pedido { get; set; }

        // Constructor
        public Compra()
        {
            IdCompra = 0;
            Cantidad = 0;
            ValorTotal = 0;
            Descuento = 0;
            IdProducto = 0;
            IdPedido = 0;
            Producto = null;
            Pedido = null;
        }
    }
}
