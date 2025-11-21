using AppAcmafer.Datos;
using AppAcmafer.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppAcmafer.Logica
{
    public class ClUsuarioL
    {
        ClUsuarioD oUsuarioD = new ClUsuarioD();

        public List<ClUsuarioM> ListarUsuarios()
        {
            List<ClUsuarioM> usuarios = oUsuarioD.ListarUsuarios();

            return usuarios ?? new List<ClUsuarioM>();
        }
    }
}
        
    