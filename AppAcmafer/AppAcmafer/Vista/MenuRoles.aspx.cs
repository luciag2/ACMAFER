using System;
using System.Collections.Generic;
using System.Data;  // ← AGREGAR ESTA LÍNEA
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



namespace AppAcmafer.Vista
{
    // ** ESTA CLASE DEBE COINCIDIR CON LA PROPIEDAD INHERITS EN EL ASPX **
    public partial class MenuRoles : System.Web.UI.Page
    {
        // Variable de estado para el ID del rol seleccionado
        private int IdRolSeleccionado
        {
            get { return (int)(ViewState["IdRolSeleccionado"] ?? 0); }
            set { ViewState["IdRolSeleccionado"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Llama al método para cargar los roles la primera vez
                CargarRoles();
            }
        }

        // --- MÉTODOS REQUERIDOS POR LOS EVENTOS EN MenuRoles.aspx ---

        // 1. Maneja el clic en el botón "+ Añadir"
        protected void btnAgregarRol_Click(object sender, EventArgs e)
        {
            // Simulación de acción
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Funcionalidad de agregar rol (IdRolSeleccionado: " + IdRolSeleccionado + ")');", true);
        }

        // 2. Maneja los comandos del Repeater (Selección de Rol)
        protected void rptRoles_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "SeleccionarRol")
            {
                // Guarda el ID del rol seleccionado
                IdRolSeleccionado = Convert.ToInt32(e.CommandArgument);

                // Llama a la función para mostrar los detalles y permisos de ese rol
                MostrarDetallesRol(IdRolSeleccionado);

                // Oculta el panel "No seleccionado" y muestra el de detalles
                pnlNoSeleccionado.Visible = false;
                pnlRolSeleccionado.Visible = true;

                // Añade la clase 'selected' al botón presionado (requiere Javascript)
                ScriptManager.RegisterStartupScript(this, GetType(), "selectRol", "document.querySelectorAll('.btn-rol').forEach(b => b.classList.remove('selected')); document.querySelector('input[value=\"" + e.Item.FindControl("btnRol").ClientID + "\"]').classList.add('selected');", true);

            }
        }

        // 3. Maneja el clic en el botón "Editar Descripción"
        protected void btnEditarDescripcion_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", $"alert('Editando descripción para Rol ID: {IdRolSeleccionado}');", true);
        }

        // 4. Maneja el clic en el botón "Eliminar Rol"
        protected void btnEliminarRol_Click(object sender, EventArgs e)
        {
            // NOTA: Implementar confirmación en el lado del cliente o aquí
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", $"alert('Eliminando Rol ID: {IdRolSeleccionado}');", true);
            // Si el rol fue eliminado:
            IdRolSeleccionado = 0;
            pnlNoSeleccionado.Visible = true;
            pnlRolSeleccionado.Visible = false;
            CargarRoles();
        }

        // 5. Maneja el clic en el botón "Guardar Permisos"
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", $"alert('Permisos guardados para Rol ID: {IdRolSeleccionado}');", true);
        }

        // 6. Maneja el cambio de estado en cualquier CheckBox
        protected void ActualizarProgreso(object sender, EventArgs e)
        {
            int totalCheckboxes = 9;
            List<CheckBox> checkboxes = new List<CheckBox> {
                chkAccesoMenu, chkMenuVisible, chkRedireccion, chkGestionUsuarios, chkGestionProductos,
                chkGestionPedidos, chkGestionTareas, chkReportes, chkConfiguracionRoles
            };

            int checkedCount = checkboxes.Count(chk => chk.Checked);
            int porcentaje = (totalCheckboxes > 0) ? (checkedCount * 100) / totalCheckboxes : 0;
            lblProgreso.Text = $"{porcentaje}%";
        }

        // --- MÉTODOS DE LÓGICA DE DATOS (Simulación) ---

        private void CargarRoles()
        {
            // SIMULACIÓN DE DATOS
            DataTable dt = new DataTable();
            dt.Columns.Add("IdRol", typeof(int));
            dt.Columns.Add("NombreRol", typeof(string));

            dt.Rows.Add(1, "Administrador Supremo");
            dt.Rows.Add(2, "Jefe de Almacén");
            dt.Rows.Add(3, "Vendedor Junior");
            dt.Rows.Add(4, "Visitante Invitado");

            rptRoles.DataSource = dt;
            rptRoles.DataBind();
        }

        private void MostrarDetallesRol(int idRol)
        {
            // SIMULACIÓN DE CARGA DE DETALLES
            string nombre = "Rol No Encontrado";
            string descripcion = "Seleccione un rol para ver su descripción";

            // Simular permisos
            bool esAdmin = idRol == 1;
            bool esJefe = idRol == 2;
            bool esVendedor = idRol == 3;
            bool esVisitante = idRol == 4;

            if (esAdmin)
            {
                nombre = "Administrador Supremo";
                descripcion = "Control total sobre todos los módulos y la gestión de permisos.";
            }
            else if (esJefe)
            {
                nombre = "Jefe de Almacén";
                descripcion = "Gestión de inventario, pedidos y reportes intermedios.";
            }
            else if (esVendedor)
            {
                nombre = "Vendedor Junior";
                descripcion = "Solo acceso a la gestión de pedidos y reportes básicos.";
            }
            else if (esVisitante)
            {
                nombre = "Visitante Invitado";
                descripcion = "Rol limitado a la visualización de reportes básicos.";
            }


            lblNombreRol.Text = nombre;
            lblDescripcion.Text = descripcion;

            // Carga de permisos simulada
            chkAccesoMenu.Checked = !esVisitante;
            chkMenuVisible.Checked = !esVisitante;
            chkRedireccion.Checked = esAdmin || esJefe;
            chkGestionUsuarios.Checked = esAdmin;
            chkGestionProductos.Checked = esAdmin || esJefe;
            chkGestionPedidos.Checked = !esVisitante;
            chkGestionTareas.Checked = esAdmin || esJefe;
            chkReportes.Checked = true;
            chkConfiguracionRoles.Checked = esAdmin;

            ActualizarProgreso(null, null);
        }
    }
}