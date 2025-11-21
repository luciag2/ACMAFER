using AppAcmafer.Datos;
using AppAcmafer.Logica;
using AppAcmafer.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace AppAcmafer.Vista
{
    public class ClUsuarioV
    {
        private ClUsuarioD oUsuarioD = new ClUsuarioD();

        
        public List<ClUsuarioM> ListarUsuarios()
        {
            List<ClUsuarioM> usuarios = oUsuarioD.ListarUsuarios();

            return usuarios ?? new List<ClUsuarioM>(); 
        }
    }
}