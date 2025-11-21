using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppAcmafer.Vista
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ConfigurarMenu();
            }
        }

        private void ConfigurarMenu()
        {
            // Simular rol de usuario (en producción usar Session)
            // Cambiar entre "Administrador", "Empleado", "Cliente", "Supervisor"
            int idRol = 1; // 1=Admin, 2=Empleado, 3=Cliente, 4=Supervisor

            // Mostrar todas las opciones por defecto
            // En producción, aquí validarías permisos según el rol
        }
    }
}