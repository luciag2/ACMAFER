using AppAcmafer.Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppAcmafer.Vista
{
    public partial class ListarUsuarios : System.Web.UI.Page
    {
        ClConexion Conexion;

        protected void Page_Load(object sender, EventArgs e)
        {
            Conexion = new ClConexion();


            if (!IsPostBack)
            {
                CargarUsuarios();

            }
        }
        public void CargarUsuarios()
        {
            string consultaSQL = @"
        SELECT 
            u.documento AS Documento, 
            u.nombre + ' ' + u.apellido AS NombreCompleto, 
            u.email AS Correo, 
            r.rol AS Rol,
            CASE 
                WHEN u.estado = 'Activo' THEN 1 
                ELSE 0 
            END AS Estado  -- << ESTA COLUMNA ES UN INT
        FROM [dbo].[usuario] u 
        INNER JOIN [dbo].[rol] r ON u.idRol = r.idRol
        ORDER BY u.nombre ASC";
            try
            {
                DataTable tablaUsuarios = Conexion.ObtenerTabla(consultaSQL);

                rptUsuarios.DataSource = tablaUsuarios; 
                rptUsuarios.DataBind();

                if (tablaUsuarios.Rows.Count == 0)
                {
                    lblMensaje.Text = "No se encontraron usuarios registrados en esta lista.";
                    rptUsuarios.Visible = false;
                }
                else
                {
                    lblMensaje.Text = "";
                    rptUsuarios.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al cargar usuarios: " + ex.Message;
            }
        }
    }
}
       
