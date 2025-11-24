using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppAcmafer.Vista
{
    public partial class OpSupervisor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["emailUser"] == null)
            {
                Response.Redirect("~/Login.aspx");
                return;
            }

            if (Session["rol"] != null && Convert.ToInt32(Session["rol"]) != 4)
            {
                Response.Redirect("~/AccesoDenegado.aspx");
                return;
            }

            if (!IsPostBack)
            {
                lblBienvenida.Text = $"Bienvenido Supervisor: {Session["nombreCompleto"]}";
                lblRol.Text = $"Rol: {Session["nombreRol"]}";
            }
        }

        protected void btnSupervisionVentas_Click(object sender, EventArgs e)
        {
            Response.Redirect("SupervisionVentas.aspx");
        }

        protected void btnReportesGenerales_Click(object sender, EventArgs e)
        {
            Response.Redirect("ReportesGenerales.aspx");
        }

        protected void btnGestionInventario_Click(object sender, EventArgs e)
        {
            Response.Redirect("GestionInventario.aspx");
        }

        protected void btnSupervisionEmpleados_Click(object sender, EventArgs e)
        {
            Response.Redirect("SupervisionEmpleados.aspx");
        }

        protected void btnEstadisticas_Click(object sender, EventArgs e)
        {
            Response.Redirect("EstadisticasGenerales.aspx");
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
    