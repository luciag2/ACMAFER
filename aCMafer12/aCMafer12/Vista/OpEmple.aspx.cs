using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppAcmafer.Vista
{
    public partial class OpEmple : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["emailUser"] == null)
            {
                Response.Redirect("~/Login.aspx");
                return;
            }

            if (Session["rol"] != null && Convert.ToInt32(Session["rol"]) != 2)
            {
                Response.Redirect("~/AccesoDenegado.aspx");
                return;
            }

            if (!IsPostBack)
            {
                lblBienvenida.Text = $"Bienvenido Empleado: {Session["nombreCompleto"]}";
                lblRol.Text = $"Rol: {Session["nombreRol"]}";
            }
        }

        protected void btnRegistrarVenta_Click(object sender, EventArgs e)
        {
            Response.Redirect("RegistrarVenta.aspx");
        }

        protected void btnConsultarInventario_Click(object sender, EventArgs e)
        {
            Response.Redirect("ConsultarInventario.aspx");
        }

        protected void btnVerClientes_Click(object sender, EventArgs e)
        {
            Response.Redirect("ConsultarClientes.aspx");
        }

        protected void btnMiPerfil_Click(object sender, EventArgs e)
        {
            Response.Redirect("MiPerfil.aspx");
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/Login.aspx");
        }
    }
}
