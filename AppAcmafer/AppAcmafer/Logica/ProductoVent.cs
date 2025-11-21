using AppAcmafer.Datos;
using AppAcmafer.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppAcmafer.Logica
{
    public class ProductoVent
    {

        public class ProductoBLL
        {
            private readonly ProductoDAL _productoDAL = new ProductoDAL();
            public List<Producto> ObtenerCatalogoDeVenta()
            {
                return _productoDAL.ObtenerProductosEnVenta();
            }
        }
    }
}