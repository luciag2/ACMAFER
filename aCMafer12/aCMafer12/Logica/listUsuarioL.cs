using AppAcmafer.Datos;
using AppAcmafer.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppAcmafer.Logica
{
    public class listUsuarioL
    {
        private listUsuarioD usuarioData;

        public listUsuarioL()
        {
            usuarioData = new listUsuarioD();
        }

       
        private bool VerificarPermisoAdmin(Administrador admin)
        {
            if (admin == null)
            {
                return false;
            }

          
            if ((admin.Rol == "Administrador" || admin.Rol == "Supervisor") && admin.IdAdmin > 0)
            {
                return true;
            }

            return false;
        }

        public List<listUsuarioM> ListarUsuarios(Administrador admin)
        {
            if (!VerificarPermisoAdmin(admin))
            {
                return new List<listUsuarioM>(); 
            }

            try
            {
                return usuarioData.ObtenerTodosLosUsuarios();
            }
            catch (Exception ex)
            {
                
                Console.WriteLine("Error en ListarUsuarios (Lógica): " + ex.Message);
                throw; 
            }
        }

        
        public int ContarUsuarios(Administrador admin)
        {
            if (!VerificarPermisoAdmin(admin))
            {
                return 0;
            }

            try
            {
                return usuarioData.ObtenerTodosLosUsuarios().Count;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en ContarUsuarios: " + ex.Message);
                return 0;
            }
        }
    }
}