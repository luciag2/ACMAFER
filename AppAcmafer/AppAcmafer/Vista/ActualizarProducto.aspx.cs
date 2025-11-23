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
    public partial class ActualizarProducto : System.Web.UI.Page
    {
        private CL_Producto productoLogica = new CL_Producto();
        private Cl_Categoria categoriaLogica = new Cl_Categoria();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarProductos();
                CargarCategorias();
            }
        }

        private void CargarProductos()
        {
            try
            {
                DataTable dt = productoLogica.ObtenerProductos();
                ddlProductos.DataSource = dt;
                ddlProductos.DataTextField = "nombre";
                ddlProductos.DataValueField = "idProducto";
                ddlProductos.DataBind();
                ddlProductos.Items.Insert(0, new ListItem("-- Seleccione un producto --", "0"));
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al cargar productos: " + ex.Message, false);
            }
        }

        private void CargarCategorias()
        {
            try
            {
                DataTable dt = categoriaLogica.ObtenerCategorias();
                ddlCategorias.DataSource = dt;
                ddlCategorias.DataTextField = "nombre";
                ddlCategorias.DataValueField = "idCategoria";
                ddlCategorias.DataBind();
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al cargar categorías: " + ex.Message, false);
            }
        }

        protected void ddlProductos_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idProducto = Convert.ToInt32(ddlProductos.SelectedValue);

            if (idProducto > 0)
            {
                pnlFormulario.Visible = true;
                CargarDatosProducto(idProducto);
                CargarHistorial(idProducto);
            }
            else
            {
                pnlFormulario.Visible = false;
            }
        }

        private void CargarDatosProducto(int idProducto)
        {
            try
            {
                DataRow producto = productoLogica.ObtenerProducto(idProducto);

                if (producto != null)
                {
                    txtNombre.Text = producto["nombre"].ToString();
                    txtDescripcion.Text = producto["descripcion"].ToString();
                    txtCodigo.Text = producto["codigo"].ToString();
                    txtStock.Text = producto["stockActual"].ToString();
                    txtPrecio.Text = producto["precioUnitario"].ToString();
                    ddlCategorias.SelectedValue = producto["idCategoria"].ToString();
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al cargar producto: " + ex.Message, false);
            }
        }

        private void CargarHistorial(int idProducto)
        {
            try
            {
                DataTable dt = productoLogica.ObtenerHistorial(idProducto);
                gvHistorial.DataSource = dt;
                gvHistorial.DataBind();
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al cargar historial: " + ex.Message, false);
            }
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                int idProducto = Convert.ToInt32(ddlProductos.SelectedValue);
                string nombre = txtNombre.Text.Trim();
                string descripcion = txtDescripcion.Text.Trim();
                string codigo = txtCodigo.Text.Trim();

                // ✅ CORRECCIÓN: Convertir a int y decimal, NO dejar como string
                int stock = Convert.ToInt32(txtStock.Text);
                decimal precio = Convert.ToDecimal(txtPrecio.Text);
                int idCategoria = Convert.ToInt32(ddlCategorias.SelectedValue);

                // Ahora pasa los tipos correctos
                bool resultado = productoLogica.ActualizarProducto(
                    idProducto,
                    nombre,
                    descripcion,
                    codigo,
                    stock,      // int, no string
                    precio,     // decimal, no string
                    idCategoria
                );

                MostrarMensaje(
                    resultado
                        ? "✅ Producto actualizado correctamente. Fecha de modificación registrada."
                        : "❌ Error al actualizar el producto",
                    resultado
                );

                if (resultado)
                {
                    CargarHistorial(idProducto);
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error: " + ex.Message, false);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            pnlFormulario.Visible = false;
            ddlProductos.SelectedIndex = 0;
        }

        private void LimpiarFormulario()
        {
            txtNombre.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            txtCodigo.Text = string.Empty;
            txtStock.Text = string.Empty;
            txtPrecio.Text = string.Empty;
            pnlMensaje.Visible = false;
        }

        private void MostrarMensaje(string mensaje, bool esExito)
        {
            pnlMensaje.Visible = true;
            lblMensaje.Text = mensaje;
            pnlMensaje.CssClass = esExito ? "mensaje exito" : "mensaje error";
        }
    }
}