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
    public partial class AsignarTareas : System.Web.UI.Page
    {
        private TareaDAO tareaDAO = new TareaDAO();
        private List<AsignacionTarea> todasLasAsignaciones;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Verificar permisos (simulado - en producción usar Session)
                int idRol = 1; // Administrador

                if (idRol != 1 && idRol != 4) // No es Admin ni Supervisor
                {
                    Response.Redirect("~/Vista/Productos.aspx");
                    return;
                }

                CargarAsignaciones();
                CalcularEstadisticas();
            }
        }

        private void CargarAsignaciones()
        {
            try
            {
                todasLasAsignaciones = tareaDAO.ObtenerAsignacionesTareas();
                gvAsignaciones.DataSource = todasLasAsignaciones;
                gvAsignaciones.DataBind();

                ViewState["Asignaciones"] = todasLasAsignaciones;
            }
            catch (Exception ex)
            {
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                lblMensaje.Text = "Error al cargar asignaciones: " + ex.Message;
            }
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            try
            {
                List<AsignacionTarea> asignaciones = (List<AsignacionTarea>)ViewState["Asignaciones"];
                string filtroEmpleado = txtFiltroEmpleado.Text.ToLower().Trim();

                var asignacionesFiltradas = asignaciones;

                if (!string.IsNullOrEmpty(filtroEmpleado))
                {
                    asignacionesFiltradas = asignacionesFiltradas.Where(a =>
                        a.NombreEmpleado.ToLower().Contains(filtroEmpleado)
                    ).ToList();
                }

                if (!string.IsNullOrEmpty(txtFiltroFecha.Text))
                {
                    DateTime fecha = Convert.ToDateTime(txtFiltroFecha.Text);
                    asignacionesFiltradas = asignacionesFiltradas.Where(a =>
                        a.FechaInicio.Date == fecha.Date || a.FechaFin.Date == fecha.Date
                    ).ToList();
                }

                gvAsignaciones.DataSource = asignacionesFiltradas;
                gvAsignaciones.DataBind();
                CalcularEstadisticas(asignacionesFiltradas);
            }
            catch (Exception ex)
            {
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                lblMensaje.Text = "Error al filtrar: " + ex.Message;
            }
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtFiltroEmpleado.Text = "";
            txtFiltroFecha.Text = "";
            CargarAsignaciones();
            CalcularEstadisticas();
        }

        protected void btnVerComentario_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int idAsignacion = Convert.ToInt32(btn.CommandArgument);

            List<AsignacionTarea> asignaciones = (List<AsignacionTarea>)ViewState["Asignaciones"];
            AsignacionTarea asignacion = asignaciones.FirstOrDefault(a => a.IdAsignacionTarea == idAsignacion);

            if (asignacion != null)
            {
                lblComentario.Text = asignacion.ComentarioAdmin;
                pnlComentario.Visible = true;
            }
        }

        protected void btnCerrarComentario_Click(object sender, EventArgs e)
        {
            pnlComentario.Visible = false;
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int idAsignacion = Convert.ToInt32(btn.CommandArgument);

            lblMensaje.ForeColor = System.Drawing.Color.Green;
            lblMensaje.Text = $"Editando asignación #{idAsignacion}";
        }

        // Método auxiliar para evaluar estado
        protected string EvaluarEstado(DateTime fechaFin)
        {
            if (fechaFin < DateTime.Now)
                return "⏰ Vencida";
            else if (fechaFin.Date == DateTime.Now.Date)
                return "🔥 Urgente";
            else
                return "✅ En Progreso";
        }

        private void CalcularEstadisticas(List<AsignacionTarea> asignaciones = null)
        {
            try
            {
                if (asignaciones == null)
                {
                    asignaciones = (List<AsignacionTarea>)ViewState["Asignaciones"];
                }

                if (asignaciones != null && asignaciones.Count > 0)
                {
                    lblTotalAsignaciones.Text = asignaciones.Count.ToString();

                    int enProgreso = asignaciones.Count(a => a.FechaFin >= DateTime.Now);
                    lblEnProgreso.Text = enProgreso.ToString();

                    int completadas = asignaciones.Count(a => a.FechaFin < DateTime.Now.AddDays(-7));
                    lblCompletadas.Text = completadas.ToString();

                    int vencidas = asignaciones.Count(a => a.FechaFin < DateTime.Now && a.FechaFin >= DateTime.Now.AddDays(-7));
                    lblVencidas.Text = vencidas.ToString();
                }
                else
                {
                    lblTotalAsignaciones.Text = "0";
                    lblEnProgreso.Text = "0";
                    lblCompletadas.Text = "0";
                    lblVencidas.Text = "0";
                }
            }
            catch (Exception ex)
            {
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                lblMensaje.Text = "Error al calcular estadísticas: " + ex.Message;
            }
        }
    }
}