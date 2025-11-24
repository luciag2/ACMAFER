using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppAcmafer.Modelo
{
    [Serializable]
    public class Categoria
    {
        public int IdCategoria { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        // Constructor
        public Categoria()
        {
            IdCategoria = 0;
            Nombre = string.Empty;
            Descripcion = string.Empty;
        }
    }
}