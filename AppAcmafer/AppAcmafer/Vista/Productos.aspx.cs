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
    public partial class Productos : System.Web.UI.Page
    {
        private ProductoDAO productoDAO = new ProductoDAO();
        private List<Producto> todosLosProductos;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCategorias();
                CargarProductos();
                CalcularEstadisticas();
            }
        }

        private void CargarCategorias()
        {
            try
            {
                // Cargar categorías en el DropDownList
                ddlCategoria.Items.Clear();
                ddlCategoria.Items.Add(new ListItem("-- Todas las Categorías --", "0"));
                ddlCategoria.Items.Add(new ListItem("Materia Prima", "1"));
                ddlCategoria.Items.Add(new ListItem("Insumos de Moldeo", "2"));
                ddlCategoria.Items.Add(new ListItem("Equipos de Protección", "3"));
                ddlCategoria.Items.Add(new ListItem("Herramientas de Acabado", "4"));
                ddlCategoria.Items.Add(new ListItem("Maquinaria y Horno", "5"));
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al cargar categorías: " + ex.Message, false);
            }
        }

        private void CargarProductos()
        {
            try
            {
                todosLosProductos = productoDAO.ObtenerTodosLosProductos();
                gvProductos.DataSource = todosLosProductos;
                gvProductos.DataBind();

                // Guardar en ViewState para filtros
                ViewState["Productos"] = todosLosProductos;
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al cargar productos: " + ex.Message, false);
            }
        }

        protected void ddlCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                List<Producto> productos = (List<Producto>)ViewState["Productos"];
                int idCategoria = Convert.ToInt32(ddlCategoria.SelectedValue);

                if (idCategoria == 0)
                {
                    gvProductos.DataSource = productos;
                }
                else
                {
                    var productosFiltrados = productos.Where(p => p.IdCategoria == idCategoria).ToList();
                    gvProductos.DataSource = productosFiltrados;
                }

                gvProductos.DataBind();
                CalcularEstadisticas();
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al filtrar: " + ex.Message, false);
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                List<Producto> productos = (List<Producto>)ViewState["Productos"];
                string busqueda = txtBuscar.Text.ToLower().Trim();

                if (!string.IsNullOrEmpty(busqueda))
                {
                    var productosFiltrados = productos.Where(p =>
                        p.Nombre.ToLower().Contains(busqueda) ||
                        p.Descripcion.ToLower().Contains(busqueda) ||
                        p.Codigo.ToLower().Contains(busqueda)
                    ).ToList();

                    gvProductos.DataSource = productosFiltrados;
                    gvProductos.DataBind();

                    if (productosFiltrados.Count == 0)
                    {
                        MostrarMensaje($"No se encontraron productos con el término '{txtBuscar.Text}'", false);
                    }
                    else
                    {
                        MostrarMensaje($"Se encontraron {productosFiltrados.Count} producto(s)", true);
                    }

                    CalcularEstadisticas(productosFiltrados);
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al buscar: " + ex.Message, false);
            }
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = "";
            ddlCategoria.SelectedIndex = 0;
            CargarProductos();
            CalcularEstadisticas();
            pnlDetalle.Visible = false;
            lblMensaje.Text = "";
        }

        protected void gvProductos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int idProducto = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "VerDetalle")
            {
                MostrarDetalle(idProducto);
            }
            else if (e.CommandName == "Comprar")
            {
                MostrarMensaje($"Producto #{idProducto} agregado al carrito. Redirigiendo a Compras...", true);
                // Aquí podrías redirigir a la página de compras
                // Response.Redirect("~/Vista/Compras.aspx?idProducto=" + idProducto);
            }
        }

        private void MostrarDetalle(int idProducto)
        {
            try
            {
                List<Producto> productos = (List<Producto>)ViewState["Productos"];
                Producto producto = productos.FirstOrDefault(p => p.IdProducto == idProducto);

                if (producto != null)
                {
                    lblDetalleCodigo.Text = producto.Codigo;
                    lblDetalleNombre.Text = producto.Nombre;
                    lblDetalleDescripcion.Text = producto.Descripcion;
                    lblDetallePrecio.Text = producto.PrecioUnitario.ToString("N0");
                    lblDetalleStock.Text = producto.StockActual + " unidades";
                    lblDetalleCategoria.Text = producto.NombreCategoria;

                    pnlDetalle.Visible = true;

                    // Scroll al detalle
                    ScriptManager.RegisterStartupScript(this, GetType(), "scrollToDetail",
                        "window.scrollTo(0, document.body.scrollHeight);", true);
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al mostrar detalle: " + ex.Message, false);
            }
        }

        protected void btnCerrarDetalle_Click(object sender, EventArgs e)
        {
            pnlDetalle.Visible = false;
        }

        private void CalcularEstadisticas(List<Producto> productos = null)
        {
            try
            {
                if (productos == null)
                {
                    productos = (List<Producto>)ViewState["Productos"];
                }

                if (productos != null && productos.Count > 0)
                {
                    lblTotalProductos.Text = productos.Count.ToString();
                    lblTotalCategorias.Text = productos.Select(p => p.IdCategoria).Distinct().Count().ToString();
                    lblStockTotal.Text = productos.Sum(p => p.StockActual).ToString("N0");
                    lblValorTotal.Text = productos.Sum(p => p.PrecioUnitario * p.StockActual).ToString("N0");
                }
                else
                {
                    lblTotalProductos.Text = "0";
                    lblTotalCategorias.Text = "0";
                    lblStockTotal.Text = "0";
                    lblValorTotal.Text = "$0";
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al calcular estadísticas: " + ex.Message, false);
            }
        }

        private void MostrarMensaje(string mensaje, bool esExito)
        {
            lblMensaje.Text = mensaje;
            lblMensaje.ForeColor = esExito ? System.Drawing.Color.Green : System.Drawing.Color.Red;
            lblMensaje.Style["background-color"] = esExito ? "#d4edda" : "#f8d7da";
            lblMensaje.Style["border"] = esExito ? "1px solid #c3e6cb" : "1px solid #f5c6cb";
        }
    }
}