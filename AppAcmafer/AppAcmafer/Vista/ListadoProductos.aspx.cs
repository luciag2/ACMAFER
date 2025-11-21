using AppAcmafer.Datos;
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
         ClConexion Conexion;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Creamos la conexión localmente, como buena práctica
            // (La clase ClConexion ya fue corregida para manejar esto)
            // Conexion = new ClConexion(); // <--- Ya no es necesario

            if (!IsPostBack)
            {
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
                p.idProducto, 
                p.Nombre AS nombre, 
                c.Nombre AS CategoriaNombre, 
                p.PrecioUnitario AS precioUnitario, 
                p.StockActual AS stockActual 
            FROM dbo.producto p
            INNER JOIN dbo.categoria c ON p.idCategoria = c.idCategoria
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
    }
}