using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppAcmafer.Modelo
{
    public class ClUsuarioM
    {
            public int IdUsuario { get; set; }
            public string Documento { get; set; }
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public string Email { get; set; }
            public string Estado { get; set; }
            public int IdRol { get; set; }
        }
    }