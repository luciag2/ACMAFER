using AppAcmafer.Datos;
using AppAcmafer.Modelo;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppAcmafer.Vista
{
    public partial class ListadoProductos : System.Web.UI.Page
    {
        CompraDAO dao = new CompraDAO();
        ClConexion Conexion;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Creamos la conexión localmente, como buena práctica
            // (La clase ClConexion ya fue corregida para manejar esto)
            // Conexion = new ClConexion(); // <--- Ya no es necesario

            if (!IsPostBack)
            {
                CargarProductosEnVenta();
                CargarCategorias();
                CargarProductos(0); // 0 = Mostrar Todos
            }
        }

        public void CargarCategorias()
        {
            ClConexion miConexion = new ClConexion(); // Instancia local
            string consulta = "SELECT idCategoria, Nombre FROM dbo.categoria ORDER BY Nombre";

            try
            {
                DataTable tablaCategorias = miConexion.ObtenerTabla(consulta);

                ddlCategoria.DataSource = tablaCategorias;
                ddlCategoria.DataValueField = "idCategoria";
                ddlCategoria.DataTextField = "Nombre"; // Asegúrate que el campo se llama 'Nombre'
                ddlCategoria.DataBind();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al cargar las categorías: " + ex.Message;
                return;
            }

            // Insertar la opción "Mostrar Todos"
            ddlCategoria.Items.Insert(0, new ListItem("--- Mostrar Todos ---", "0"));
        }

        public void CargarProductos(int idCategoriaSeleccionada)
        {
            ClConexion miConexion = new ClConexion(); // Instancia local

            // --- SENTENCIA SQL CORREGIDA ---
            // 1. Usamos alias 'p' para producto y 'c' para categoría.
            // 2. Usamos c.Nombre para obtener el nombre de la categoría (asumiendo que es 'Nombre').
            // 3. Quitamos las referencias a T1 que no existen.

            string consultaSQL = $@"
            SELECT
                    p.idProducto, p.nombre, c.nombre AS CategoriaNombre, 
                    p.precioUnitario, p.stockActual
                FROM 
                    dbo.producto p
                INNER JOIN 
                    dbo.categoria c ON p.idCategoria = c.idCategoria"";
            "; // No se usa WHERE ni ORDER BY aún.

            if (idCategoriaSeleccionada > 0)
            {
                // Si hay filtro, se añade el WHERE con el alias 'p' (producto)
                consultaSQL += $" WHERE p.idCategoria = {idCategoriaSeleccionada}";
            }

            // Se añade el ORDER BY al final, usando el alias 'p' (producto)
            consultaSQL += " ORDER BY p.Nombre";

            try
            {
                DataTable tablaProductos = miConexion.ObtenerTabla(consultaSQL);

                rptProductos.DataSource = tablaProductos;
                rptProductos.DataBind();

                if (tablaProductos.Rows.Count == 0 && idCategoriaSeleccionada > 0)
                {
                    lblMensaje.Text = "No se encontraron productos en la categoría seleccionada.";
                }
                else if (tablaProductos.Rows.Count == 0 && idCategoriaSeleccionada == 0)
                {
                    lblMensaje.Text = "No se encontraron productos en la base de datos.";
                }
                else
                {
                    lblMensaje.Text = "";
                }
            }
            catch (Exception ex)
            {
                // Esto te ayudará a ver errores de conexión o nombres de columna si aún existen
                lblMensaje.Text = "Error al cargar productos: " + ex.Message;
            }
        }

        protected void ddlCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idSeleccionado = Convert.ToInt32(ddlCategoria.SelectedValue);
            CargarProductos(idSeleccionado);
        }

        // --- NUEVA FUNCIÓN PARA OBTENER SOLO PRODUCTOS EN VENTA ---
        public void CargarProductosEnVenta()
        {
            ClConexion miConexion = new ClConexion();

            string consultaSQL = @"
        SELECT
            p.idProducto, p.nombre, c.nombre AS Categoria, p.precioUnitario, p.stockActual
        FROM 
            dbo.producto p
        INNER JOIN 
            dbo.categoria c ON p.idCategoria = c.idCategoria
        WHERE 
            p.estado = 'Disponible'
            AND CAST(p.stockActual AS INT) > 0
        ORDER BY 
            p.nombre;";

            try
            {
                System.Data.DataTable tablaProductosEnVenta = miConexion.ObtenerTabla(consultaSQL);

                rptProductos.DataSource = tablaProductosEnVenta;
                rptProductos.DataBind();

                if (tablaProductosEnVenta.Rows.Count == 0)
                {
                    lblMensaje.Text = "No se encontraron productos disponibles para la venta.";
                }
                else
                {
                    lblMensaje.Text = "";
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al cargar productos en venta: " + ex.Message;
            }
        }

        protected void RptProductos_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "ComprarProducto")
            {
                int idProducto = Convert.ToInt32(e.CommandArgument);

                Producto productoSeleccionado = new Producto();
                productoSeleccionado.Nombre = "Producto de Prueba " + idProducto.ToString();
                productoSeleccionado.PrecioUnitario = 100.50m;

                {
                    lblModalProductoNombre.Text = productoSeleccionado.Nombre;
                    lblModalPrecio.Text = productoSeleccionado.PrecioUnitario.ToString("C");
                    ViewState["CurrentProductID"] = idProducto;

                    string script = "$('#ModalCompra').modal('show');";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ShowModal", script, true);
                }
            }
        }

        protected void btnAgregarItem_Click(object sender, EventArgs e)
        {
            if (ViewState["CurrentProductID"] != null && int.TryParse(txtCantidad.Text, out int cantidad) && cantidad > 0)
            {
                int idProducto = Convert.ToInt32(ViewState["CurrentProductID"]);

                Producto productoInfo = dao.ObtenerProductoPorId(idProducto);

                if (productoInfo != null)
                {
                    List<CarritoItem> carrito = (Session["Carrito"] as List<CarritoItem>) ?? new List<CarritoItem>();

                    carrito.Add(new CarritoItem
                    {
                        IdProducto = idProducto,
                        NombreProducto = productoInfo.Nombre,
                        Cantidad = cantidad,
                        PrecioUnitario = productoInfo.PrecioUnitario,
                        Subtotal = cantidad * productoInfo.PrecioUnitario
                    });

                    Session["Carrito"] = carrito;

                    string script = "$('#ModalCompra').modal('hide'); alert('Producto agregado: " + productoInfo.Nombre + " x" + cantidad + "');";
                    ScriptManager.RegisterStartupScript(this, GetType(), "HideModal", script, true);

                    txtCantidad.Text = "1";
                    ViewState["CurrentProductID"] = null;
                }
            }
        }
    }
}