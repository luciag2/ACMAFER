using AppAcmafer.Datos;
using AppAcmafer.Logica;
using AppAcmafer.Modelo;
using AppAcmafer.Utilidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static AppAcmafer.Modelo.listUsuarioM;

namespace AppAcmafer.Vista
{
    public partial class ListarUsuarios : System.Web.UI.Page
    {

        listUsuarioL usuarioLogica;
        Administrador adminActual;

        protected void Page_Load(object sender, EventArgs e)
        {
            // ✅ CORREGIDO: Permitir acceso a Administrador Y Supervisor
            ClPermisosROL.ClPermisosHelper.VerificarAccesoMultiple(
                ClPermisosROL.ClPermisosHelper.ROL_ADMINISTRADOR,
                ClPermisosROL.ClPermisosHelper.ROL_SUPERVISOR);

            if (!IsPostBack)
            {
                adminActual = ObtenerAdministradorActual();
                CargarListadoUsuarios();
            }
        }

        private void CargarListadoUsuarios()
        {
            adminActual = ObtenerAdministradorActual();

            if (adminActual == null || (adminActual.Rol != "Administrador" && adminActual.Rol != "Supervisor"))
            {
                MostrarErrorPermiso();
                return;
            }

            try
            {
                usuarioLogica = new listUsuarioL();
                List<listUsuarioM> usuarios = usuarioLogica.ListarUsuarios(adminActual);

                if (usuarios == null || usuarios.Count == 0)
                {
                    lblSinDatos.Visible = true;
                    rptUsuarios.DataSource = new List<listUsuarioM>();
                    rptUsuarios.DataBind();
                    lblMensajeError.Visible = false;
                    ActualizarEstadisticas(new List<listUsuarioM>());
                    return;
                }

                lblSinDatos.Visible = false;
                lblMensajeError.Visible = false;
                rptUsuarios.DataSource = usuarios;
                rptUsuarios.DataBind();
                ActualizarEstadisticas(usuarios);
            }
            catch (Exception ex)
            {
                lblMensajeError.Visible = true;
                spanError.InnerText = "❌ Error al cargar usuarios: " + ex.Message;
                rptUsuarios.DataSource = new List<listUsuarioM>();
                rptUsuarios.DataBind();
            }
        }

        private void MostrarErrorPermiso()
        {
            lblMensajeError.Visible = true;
            spanError.InnerText = "❌ No tienes permisos para acceder a este listado. Solo administradores y supervisores pueden ver esta información.";
            rptUsuarios.DataSource = new List<listUsuarioM>();
            rptUsuarios.DataBind();
            lblSinDatos.Visible = false;
        }

        private void ActualizarEstadisticas(List<listUsuarioM> usuarios)
        {
            lblTotalUsuarios.Text = usuarios.Count.ToString();
            lblUsuariosActivos.Text = usuarios.Count(u => u.Estado == "Activo").ToString();
            lblAdministradores.Text = usuarios.Count(u => u.Rol == "Administrador").ToString();
        }

        protected void BtnFiltrar_Click(object sender, EventArgs e)
        {
            adminActual = ObtenerAdministradorActual();

            if (adminActual == null || (adminActual.Rol != "Administrador" && adminActual.Rol != "Supervisor"))
            {
                MostrarErrorPermiso();
                return;
            }

            try
            {
                usuarioLogica = new listUsuarioL();
                List<listUsuarioM> usuarios = usuarioLogica.ListarUsuarios(adminActual);

                if (!string.IsNullOrEmpty(txtBuscar.Text))
                {
                    usuarios = usuarios.Where(u => u.NombreCompleto.ToLower().Contains(txtBuscar.Text.ToLower())).ToList();
                }

                if (!string.IsNullOrEmpty(ddlRol.SelectedValue))
                {
                    usuarios = usuarios.Where(u => u.Rol == ddlRol.SelectedValue).ToList();
                }

                if (usuarios.Count == 0)
                {
                    lblSinDatos.Visible = true;
                    lblMensajeError.Visible = false;
                    rptUsuarios.DataSource = new List<listUsuarioM>();
                    rptUsuarios.DataBind();
                    ActualizarEstadisticas(new List<listUsuarioM>());
                    return;
                }

                lblSinDatos.Visible = false;
                lblMensajeError.Visible = false;
                rptUsuarios.DataSource = usuarios;
                rptUsuarios.DataBind();
                ActualizarEstadisticas(usuarios);
            }
            catch (Exception ex)
            {
                lblMensajeError.Visible = true;
                spanError.InnerText = "❌ Error en el filtrado: " + ex.Message;
            }
        }

        private Administrador ObtenerAdministradorActual()
        {
            if (Session["AdminActual"] != null)
            {
                return (Administrador)Session["AdminActual"];
            }

            // Si no hay AdminActual en sesión, redirigir al login
            Response.Redirect("~/Vista/Login.aspx");
            return null;
        }
    }
}