using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppAcmafer.Vista
{
    public partial class OpCliente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Verificar que el usuario esté logueado
            if (Session["emailUser"] == null)
            {
                Response.Redirect("~/Login.aspx");
                return;
            }

            // Verificar que sea Cliente (rol 3)
            if (Session["rol"] != null && Convert.ToInt32(Session["rol"]) != 3)
            {
                Response.Redirect("~/AccesoDenegado.aspx");
                return;
            }

            if (!IsPostBack)
            {
                lblBienvenida.Text = $"Bienvenido Cliente: {Session["nombreCompleto"]}";
                lblRol.Text = $"Rol: {Session["nombreRol"]}";
            }
        }

        // Botones de opciones del Cliente
        protected void btnVerCatalogo_Click(object sender, EventArgs e)
        {
            Response.Redirect("CatalogoProductos.aspx");
        }

        protected void btnMisPedidos_Click(object sender, EventArgs e)
        {
            Response.Redirect("MisPedidos.aspx");
        }

        protected void btnHacerPedido_Click(object sender, EventArgs e)
        {
            Response.Redirect("RealizarPedido.aspx");
        }

        protected void btnMiPerfil_Click(object sender, EventArgs e)
        {
            Response.Redirect("MiPerfilCliente.aspx");
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/Login.aspx");
        }
    }
}
    