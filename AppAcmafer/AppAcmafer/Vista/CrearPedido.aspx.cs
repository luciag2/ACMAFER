using AppAcmafer.Logica;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppAcmafer.Vista
{
    public partial class CrearPedido : System.Web.UI.Page
    {
        private Cl_Pedido pedidoLogica = new Cl_Pedido();
        private Cl_Usuario usuarioLogica = new Cl_Usuario();
        private CL_Producto productoLogica = new CL_Producto();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarClientes();
                CargarProductos();
                CargarPedidos();
            }
        }

        private void CargarClientes()
        {
            DataTable dt = usuarioLogica.ObtenerUsuarios();
            // Filtrar solo clientes (rol = 3 según tu BD)
            DataView dv = dt.DefaultView;
            dv.RowFilter = "rol = 'Cliente'";

            ddlClientes.DataSource = dv;
            ddlClientes.DataTextField = "nombre";
            ddlClientes.DataValueField = "idUsuario";
            ddlClientes.DataBind();
            ddlClientes.Items.Insert(0, new ListItem("-- Seleccione un cliente --", "0"));
        }

        private void CargarProductos()
        {
            DataTable dt = productoLogica.ObtenerProductos();
            ddlProductos.DataSource = dt;
            ddlProductos.DataTextField = "nombre";
            ddlProductos.DataValueField = "idProducto";
            ddlProductos.DataBind();
            ddlProductos.Items.Insert(0, new ListItem("-- Seleccione un producto --", "0"));
        }

        protected void ddlProductos_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idProducto = Convert.ToInt32(ddlProductos.SelectedValue);

            if (idProducto > 0)
            {
                DataRow producto = productoLogica.ObtenerProducto(idProducto);
                if (producto != null)
                {
                    lblStockDisponible.Text = producto["stockActual"].ToString() + " unidades";
                    pnlInfoStock.CssClass = "info-stock show";
                }
            }
            else
            {
                pnlInfoStock.CssClass = "info-stock";
            }
        }

        protected void btnCrearPedido_Click(object sender, EventArgs e)
        {
            string numeroPedido = txtNumeroPedido.Text;
            int idCliente = Convert.ToInt32(ddlClientes.SelectedValue);
            int idProducto = Convert.ToInt32(ddlProductos.SelectedValue);
            int cantidad = 0;

            if (!int.TryParse(txtCantidad.Text, out cantidad))
            {
                MostrarMensaje("⚠️ Debe ingresar una cantidad válida", false);
                return;
            }

            string observaciones = txtObservaciones.Text;

            if (idCliente == 0 || idProducto == 0)
            {
                MostrarMensaje("⚠️ Debe seleccionar un cliente y un producto", false);
                return;
            }

            string mensaje;
            bool resultado = pedidoLogica.CrearPedido(numeroPedido, idCliente, observaciones,
                                                     idProducto, cantidad, out mensaje);

            MostrarMensaje(mensaje, resultado);

            if (resultado)
            {
                LimpiarFormulario();
                CargarPedidos();
                CargarProductos(); // Recargar para actualizar stock en dropdown

                // Actualizar stock mostrado
                ddlProductos_SelectedIndexChanged(null, null);
            }
        }

        private void CargarPedidos()
        {
            DataTable dt = pedidoLogica.ObtenerPedidos();
            gvPedidos.DataSource = dt;
            gvPedidos.DataBind();
        }

        private void MostrarMensaje(string mensaje, bool esExito)
        {
            pnlMensaje.Visible = true;
            lblMensaje.Text = mensaje;
            pnlMensaje.CssClass = esExito ? "mensaje exito" : "mensaje error";
        }

        private void LimpiarFormulario()
        {
            txtNumeroPedido.Text = "";
            txtCantidad.Text = "";
            txtObservaciones.Text = "";
            ddlClientes.SelectedIndex = 0;
            ddlProductos.SelectedIndex = 0;
            pnlInfoStock.CssClass = "info-stock";
        }
    }
}