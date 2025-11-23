using AppAcmafer.Logica;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppAcmafer.Vista
{
    public partial class AsignarRol : System.Web.UI.Page
    {
        private Cl_Usuario usuarioLogica = new Cl_Usuario();
        private Cl_Rol rolLogica = new Cl_Rol();
        private Cl_UsuarioRol usuarioRolLogica = new Cl_UsuarioRol();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarUsuarios();
                CargarRoles();
            }
        }

        private void CargarUsuarios()
        {
            try
            {
                DataTable dt = usuarioLogica.ObtenerUsuarios();
                ddlUsuarios.DataSource = dt;
                ddlUsuarios.DataValueField = "idUsuario";
                ddlUsuarios.DataTextField = "nombre";
                ddlUsuarios.DataBind();
                ddlUsuarios.Items.Insert(0, new ListItem("-- Seleccione un usuario --", "0"));
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al cargar usuarios: " + ex.Message, false);
            }
        }

        private void CargarRoles()
        {
            try
            {
                DataTable dt = rolLogica.ObtenerRoles();
                ddlRoles.DataSource = dt;
                ddlRoles.DataValueField = "idRol";
                ddlRoles.DataTextField = "rol";
                ddlRoles.DataBind();
                ddlRoles.Items.Insert(0, new ListItem("-- Seleccione un rol --", "0"));
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al cargar roles: " + ex.Message, false);
            }
        }

        protected void ddlUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idUsuario = Convert.ToInt32(ddlUsuarios.SelectedValue);
            if (idUsuario > 0)
            {
                CargarRolesUsuario(idUsuario);
            }
            else
            {
                gvRolesUsuario.DataSource = null;
                gvRolesUsuario.DataBind();
            }
        }

        private void CargarRolesUsuario(int idUsuario)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
                string query = @"SELECT r.rol AS nombreRol, r.idRol 
                         FROM dbo.usuarioRol ur
                         INNER JOIN dbo.rol r ON ur.idRol = r.idRol
                         WHERE ur.idUsuario = @idUsuario";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                        conn.Open();

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        gvRolesUsuario.DataSource = dt;
                        gvRolesUsuario.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al cargar roles del usuario: " + ex.Message, false);
            }
        }

        protected void btnAsignar_Click(object sender, EventArgs e)
        {
            try
            {
                int idUsuario = Convert.ToInt32(ddlUsuarios.SelectedValue);
                int idRol = Convert.ToInt32(ddlRoles.SelectedValue);

                if (idUsuario > 0 && idRol > 0)
                {
                    bool resultado = usuarioRolLogica.AsignarRolAUsuario(idUsuario, idRol);

                    if (resultado)
                    {
                        MostrarMensaje("Rol asignado correctamente", true);
                        CargarRolesUsuario(idUsuario);
                    }
                    else
                    {
                        MostrarMensaje("Error: El rol ya está asignado o hubo un problema", false);
                    }
                }
                else
                {
                    MostrarMensaje("Debe seleccionar un usuario y un rol", false);
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al asignar rol: " + ex.Message, false);
            }
        }

        private void MostrarMensaje(string mensaje, bool esExito)
        {
            pnlMensaje.Visible = true;
            lblMensaje.Text = mensaje;
            pnlMensaje.CssClass = esExito ? "mensaje exito" : "mensaje error";
        }
    }
}