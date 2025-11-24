using AppAcmafer.Datos;
using AppAcmafer.Modelo;
using AppAcmafer.Vista;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppAcmafer.Logica
{
    public class ClUsuarioL
    {
        public listUsuarioM MtLogin(string usu, string clav)
        {
            ClUsuarioD ousuarioD = new ClUsuarioD();

            listUsuarioM oDatos = ousuarioD.MtLogin(usu, clav);

            return oDatos;
        }
    }
}



