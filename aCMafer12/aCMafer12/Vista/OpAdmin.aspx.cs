using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppAcmafer.Vista
{
    public partial class OpAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["emailUser"] == null)
            {
                Response.Redirect("~/Login.aspx");
                return;
            }

            if (Session["rol"] != null && Convert.ToInt32(Session["rol"]) != 1)
            {
                Response.Redirect("~/AccesoDenegado.aspx");
                return;
            }

            if (!IsPostBack)
            {
                lblBienvenida.Text = $"Bienvenido Administrador: {Session["nombreCompleto"]}";
                lblRol.Text = $"Rol: {Session["nombreRol"]}";
            }
        }

        protected void btnGestionarUsuarios_Click(object sender, EventArgs e)
        {
            Response.Redirect("GestionUsuarios.aspx");
        }

        protected void btnGestionarRoles_Click(object sender, EventArgs e)
        {
            Response.Redirect("GestionRoles.aspx");
        }

        protected void btnReportes_Click(object sender, EventArgs e)
        {
            Response.Redirect("Reportes.aspx");
        }

        protected void btnConfiguracion_Click(object sender, EventArgs e)
        {
            Response.Redirect("Configuracion.aspx");
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/Login.aspx");
        }
    }
}
