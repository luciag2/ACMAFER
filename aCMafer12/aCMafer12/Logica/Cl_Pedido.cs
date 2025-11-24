using AppAcmafer.Datos;
using AppAcmafer.Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace AppAcmafer.Logica
{
    public class Cl_Pedido
    {
        // Método para VALIDAR estado del pedido
        public bool ValidarEstadoPedido(string estadoActual, string nuevoEstado)
        {
            // Definir transiciones válidas
            Dictionary<string, List<string>> transicionesValidas = new Dictionary<string, List<string>>
            {
                { "Pendiente", new List<string> { "En Proceso", "Cancelado" } },
                { "En Proceso", new List<string> { "Entregado", "Cancelado" } },
                { "Entregado", new List<string>{ } },
                { "Cancelado", new List<string>{ } }
            };

            if (transicionesValidas.ContainsKey(estadoActual))
            {
                return transicionesValidas[estadoActual].Contains(nuevoEstado);
            }

            return false;
        }

        public decimal CalcularTotalPedido(List<Compra> compras)
        {
            decimal total = 0;
            foreach (var compra in compras)
            {
                decimal valorTotal = compra.ValorTotal;
                decimal descuento = compra.Descuento;
                total += (valorTotal - descuento);
            }
            return total;
        }

        // Método para VALIDAR datos del pedido
        public bool ValidarDatosPedido(Pedido pedido, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrEmpty(pedido.NumeroPedido))
            {
                mensaje = "El número de pedido es obligatorio";
                return false;
            }

            if (pedido.IdCliente == 0)
            {
                mensaje = "Debe seleccionar un cliente";
                return false;
            }

            return true;
        }

        // Método para GENERAR número de pedido automático
        public string GenerarNumeroPedido()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmss");
        }
    
      private CD_Pedido pedidoDatos = new CD_Pedido();

        public bool CrearPedido(string numeroPedido, int idCliente, string observaciones,
                               int idProducto, int cantidad, out string mensaje)
        {
            // Validaciones de negocio
            if (cantidad <= 0)
            {
                mensaje = "La cantidad debe ser mayor a 0";
                return false;
            }

            if (string.IsNullOrWhiteSpace(numeroPedido))
            {
                mensaje = "El número de pedido es obligatorio";
                return false;
            }

            return pedidoDatos.CrearPedido(numeroPedido, idCliente, observaciones,
                                          idProducto, cantidad, out mensaje);
        }

        public DataTable ObtenerPedidos()
        {
            return pedidoDatos.ListarPedidos();
        }
    }
}
