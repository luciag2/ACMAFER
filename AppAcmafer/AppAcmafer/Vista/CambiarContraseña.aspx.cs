using AppAcmafer.Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppAcmafer.Vista
{
    public partial class CambiarContrasena : System.Web.UI.Page
    {
        private Cl_Usuario usuarioLogica = new Cl_Usuario();

        protected void Page_Load(object sender, EventArgs e)
        {
            // Si tienes sesión, puedes obtener el ID del usuario automáticamente
            // txtIdUsuario.Text = Session["idUsuario"].ToString();
        }

        protected void btnCambiar_Click(object sender, EventArgs e)
        {
            // Validar que todos los campos estén llenos
            if (string.IsNullOrWhiteSpace(txtIdUsuario.Text) ||
                string.IsNullOrWhiteSpace(txtClaveActual.Text) ||
                string.IsNullOrWhiteSpace(txtClaveNueva.Text) ||
                string.IsNullOrWhiteSpace(txtClaveConfirmar.Text))
            {
                MostrarMensaje("⚠️ Todos los campos son obligatorios", false);
                return;
            }

            // Validar que las contraseñas nuevas coincidan
            if (txtClaveNueva.Text != txtClaveConfirmar.Text)
            {
                MostrarMensaje("❌ Las contraseñas nuevas no coinciden", false);
                return;
            }

            int idUsuario = Convert.ToInt32(txtIdUsuario.Text);
            string claveActual = txtClaveActual.Text;
            string claveNueva = txtClaveNueva.Text;

            bool resultado = usuarioLogica.CambiarContrasena(idUsuario, claveActual, claveNueva);

            if (resultado)
            {
                MostrarMensaje("✅ Contraseña cambiada exitosamente", true);
                LimpiarCampos();
            }
            else
            {
                MostrarMensaje("❌ Error: Verifique su contraseña actual o que cumpla con las reglas de seguridad", false);
            }
        }

        private void MostrarMensaje(string mensaje, bool esExito)
        {
            pnlMensaje.Visible = true;
            lblMensaje.Text = mensaje;
            pnlMensaje.CssClass = esExito ? "mensaje exito" : "mensaje error";
        }

        private void LimpiarCampos()
        {
            txtClaveActual.Text = "";
            txtClaveNueva.Text = "";
            txtClaveConfirmar.Text = "";
        }
    }
}
