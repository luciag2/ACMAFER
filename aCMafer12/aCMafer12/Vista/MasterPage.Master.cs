using AppAcmafer.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace AppAcmafer.Vista
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                VerificarSesion();
            }
        }

        private void VerificarSesion()
        {
            // Verificar que el usuario esté logueado usando ClPermisosHelper
            if (!ClPermisosROL.ClPermisosHelper.EstaLogueado())
            {
                Response.Redirect("~/Vista/Login.aspx");
                return;
            }

            // El menú ya se controla automáticamente con los bloques <% if %> 
            // en el archivo .Master, así que no necesitamos hacer nada más aquí
        }

        protected void CerrarSesion_Click(object sender, EventArgs e)
        {
            // Usar el método de ClPermisosHelper para cerrar sesión
            ClPermisosROL.ClPermisosHelper.CerrarSesion();
        }
    }
}

    