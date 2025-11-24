using System;

namespace AppAcmafer.Modelo
{
    [Serializable]
    public class CarritoItem
    {
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; }

        public CarritoItem() { }

        public CarritoItem(int id, string nombre, int cantidad, decimal precio)
        {
            this.IdProducto = id;
            this.NombreProducto = nombre;
            this.Cantidad = cantidad;
            this.PrecioUnitario = precio;
            this.Subtotal = cantidad * precio;
        }
    }
}
