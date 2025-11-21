using AppAcmafer.Datos;
using AppAcmafer.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppAcmafer.Vista
{
    public partial class Compras : System.Web.UI.Page
    {
        private CompraDAO compraDAO = new CompraDAO();
        private List<Compra> todasLasCompras;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCompras();
                CalcularResumen();
            }
        }

        private void CargarCompras()
        {
            try
            {
                todasLasCompras = compraDAO.ObtenerTodasLasCompras();
                gvCompras.DataSource = todasLasCompras;
                gvCompras.DataBind();

                // Guardar en ViewState para filtros
                ViewState["Compras"] = todasLasCompras;
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al cargar compras: " + ex.Message;
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                List<Compra> compras = (List<Compra>)ViewState["Compras"];
                string filtro = txtFiltro.Text.ToLower().Trim();

                if (!string.IsNullOrEmpty(filtro))
                {
                    var comprasFiltradas = compras.Where(c =>
                        c.NombreProducto.ToLower().Contains(filtro) ||
                        c.NumeroPedido.ToLower().Contains(filtro)
                    ).ToList();

                    gvCompras.DataSource = comprasFiltradas;
                    gvCompras.DataBind();
                    CalcularResumen(comprasFiltradas);
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al filtrar: " + ex.Message;
            }
        }

        protected void btnMostrarTodas_Click(object sender, EventArgs e)
        {
            txtFiltro.Text = "";
            CargarCompras();
            CalcularResumen();
        }

        protected void btnVerDetalle_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int idCompra = Convert.ToInt32(btn.CommandArgument);

            lblMensaje.ForeColor = System.Drawing.Color.Green;
            lblMensaje.Text = $"Mostrando detalle de la compra #{idCompra}";
        }

        private void CalcularResumen(List<Compra> compras = null)
        {
            try
            {
                if (compras == null)
                {
                    compras = (List<Compra>)ViewState["Compras"];
                }

                if (compras != null && compras.Count > 0)
                {
                    lblTotalCompras.Text = compras.Count.ToString();
                    lblValorTotal.Text = compras.Sum(c => c.ValorTotal - c.Descuento).ToString("N0");
                    lblTotalDescuentos.Text = compras.Sum(c => c.Descuento).ToString("N0");
                }
                else
                {
                    lblTotalCompras.Text = "0";
                    lblValorTotal.Text = "$0";
                    lblTotalDescuentos.Text = "$0";
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al calcular resumen: " + ex.Message;
            }
        }
    }
}