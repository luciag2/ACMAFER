using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppAcmafer.Modelo
{
    public class Pedido
    {
        public int IdPedido { get; set; }
        public string NumeroPedido { get; set; }
        public DateTime FechaPedido { get; set; }
        public string Estado { get; set; }
        public string Observaciones { get; set; }
        public int IdCliente { get; set; }

        // Propiedad adicional
        public string NombreCliente { get; set; }

        // Navegación
        public Usuario Cliente { get; set; }

        // Constructor
        public Pedido()
        {
            IdPedido = 0;
            NumeroPedido = string.Empty;
            FechaPedido = DateTime.Now;
            Estado = "Pendiente";
            Observaciones = string.Empty;
            IdCliente = 0;
            Cliente = null;
        }
    }
}
