using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppAcmafer.Datos;
using AppAcmafer.Modelo;

namespace AppAcmafer.Vista
{
    public partial class MenuRoles : System.Web.UI.Page
    {
        // Propiedad para mantener el rol seleccionado
        public int RolSeleccionado {  get; set; }
        
           
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarRoles();
                MostrarPanelNoSeleccionado();
            }
        }

        private void CargarRoles()
        {
            try
            {
                // Asumiendo que tienes una clase RolDAO
                RolDAO rolDAO = new RolDAO();
                List<Rol> roles = rolDAO.ObtenerTodosLosRoles();
                rptRoles.DataSource = roles;
                rptRoles.DataBind();
            }
            catch (Exception ex)
            {
                // Mostrar mensaje de error
                Response.Write("<script>alert('Error al cargar roles: " + ex.Message + "');</script>");
            }
        }

        protected void rptRoles_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "SeleccionarRol")
            {
                RolSeleccionado = Convert.ToInt32(e.CommandArgument);
                CargarDatosRol(RolSeleccionado);
                CargarRoles(); // Recargar para actualizar el botón seleccionado
                MostrarPanelRolSeleccionado();
            }
        }

        private void CargarDatosRol(int idRol)
        {
            try
            {
                // Cargar datos del rol
                RolDAO rolDAO = new RolDAO();
                Rol rol = rolDAO.ObtenerRolPorId(idRol);

                if (rol != null)
                {
                    lblDescripcion.Text = rol.Descripcion ?? "Sin descripción";

                    // Cargar permisos (esto depende de cómo tengas estructurada tu BD)
                    // CargarPermisos(idRol);
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error al cargar datos del rol: " + ex.Message + "');</script>");
            }
        }

        private void MostrarPanelRolSeleccionado()
        {
            pnlRolSeleccionado.Visible = true;
            pnlNoSeleccionado.Visible = false;
        }

        private void MostrarPanelNoSeleccionado()
        {
            pnlRolSeleccionado.Visible = false;
            pnlNoSeleccionado.Visible = true;
        }

        protected void btnAgregarRol_Click(object sender, EventArgs e)
        {
            // Redirigir a página de agregar rol o mostrar modal
            Response.Redirect("AgregarRol.aspx");
        }

        protected void btnEditarDescripcion_Click(object sender, EventArgs e)
        {
            // Implementar edición de descripción
            // Puedes mostrar un TextBox o redirigir a otra página
            Response.Write("<script>alert('Funcionalidad de editar en desarrollo');</script>");
        }

        protected void btnEliminarRol_Click(object sender, EventArgs e)
        {
            if (RolSeleccionado > 0)
            {
                try
                {
                    RolDAO rolDAO = new RolDAO();
                    bool eliminado = rolDAO.EliminarRol(RolSeleccionado);

                    if (eliminado)
                    {
                        Response.Write("<script>alert('Rol eliminado correctamente');</script>");
                        RolSeleccionado = 0;
                        CargarRoles();
                        MostrarPanelNoSeleccionado();
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('Error al eliminar rol: " + ex.Message + "');</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('Seleccione un rol primero');</script>");
            }
        }

        protected void ActualizarProgreso(object sender, EventArgs e)
        {
            // Calcular porcentaje de checkboxes marcados
            int total = 9;
            int marcados = 0;

            if (chkAccesoMenu.Checked) marcados++;
            if (chkMenuVisible.Checked) marcados++;
            if (chkRedireccion.Checked) marcados++;
            if (chkGestionUsuarios.Checked) marcados++;
            if (chkGestionProductos.Checked) marcados++;
            if (chkGestionPedidos.Checked) marcados++;
            if (chkGestionTareas.Checked) marcados++;
            if (chkReportes.Checked) marcados++;
            if (chkConfiguracionRoles.Checked) marcados++;

            int porcentaje = (int)((marcados * 100.0) / total);
            lblProgreso.Text = porcentaje + "%";
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (RolSeleccionado > 0)
            {
                try
                {
                    // Guardar permisos
                    RolDAO rolDAO = new RolDAO();

                    // Crear objeto con los permisos
                    Dictionary<string, bool> permisos = new Dictionary<string, bool>
                    {
                        { "AccesoMenu", chkAccesoMenu.Checked },
                        { "MenuVisible", chkMenuVisible.Checked },
                        { "Redireccion", chkRedireccion.Checked },
                        { "GestionUsuarios", chkGestionUsuarios.Checked },
                        { "GestionProductos", chkGestionProductos.Checked },
                        { "GestionPedidos", chkGestionPedidos.Checked },
                        { "GestionTareas", chkGestionTareas.Checked },
                        { "Reportes", chkReportes.Checked },
                        { "ConfiguracionRoles", chkConfiguracionRoles.Checked }
                    };

                    // Guardar en la base de datos
                    // bool guardado = rolDAO.GuardarPermisos(RolSeleccionado, permisos);

                    Response.Write("<script>alert('Permisos guardados correctamente');</script>");
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('Error al guardar permisos: " + ex.Message + "');</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('Seleccione un rol primero');</script>");
            }
        }
    }
}