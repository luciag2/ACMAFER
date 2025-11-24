using AppAcmafer.Datos;
using AppAcmafer.Logica;
using AppAcmafer.Modelo;
using AppAcmafer.Utilidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppAcmafer.Vista
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Limpiar sesión al cargar la página
                Session.Clear();
            }
        }

        protected void BtnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                string usuario = txtUsuario.Text.Trim();
                string password = txtPassword.Text.Trim();

                if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(password))
                {
                    MostrarError("Por favor, ingresa usuario y contraseña.");
                    return;
                }

                listUsuarioD usuarioDAO = new listUsuarioD();
                var usuarioAutenticado = usuarioDAO.BuscarUsuarioPorEmailODocumento(usuario);

                if (usuarioAutenticado != null && usuarioAutenticado.Clave == password)
                {
                    // Verificar que el usuario esté activo
                    if (usuarioAutenticado.Estado != "Activo")
                    {
                        MostrarError("Tu cuenta está inactiva. Contacta al administrador.");
                        return;
                    }

                    // Guardar información del usuario en sesión
                    Session["UsuarioLogueado"] = usuarioAutenticado;
                    Session["IdUsuario"] = usuarioAutenticado.IdUsuario;
                    Session["nombreCompleto"] = usuarioAutenticado.NombreCompleto;
                    Session["emailUser"] = usuarioAutenticado.Email;
                    Session["rol"] = usuarioAutenticado.idRol;

                    // Crear objeto Administrador con el rol convertido a string
                    Administrador admin = new Administrador
                    {
                        IdAdmin = usuarioAutenticado.IdUsuario,
                        NombreAdmin = usuarioAutenticado.NombreCompleto,
                        Rol = ObtenerNombreRol(usuarioAutenticado.idRol)
                    };
                    Session["AdminActual"] = admin;

                    // Redireccionar según el rol del usuario
                    int idRol = usuarioAutenticado.idRol;

                    if (idRol == ClPermisosROL.ClPermisosHelper.ROL_ADMINISTRADOR ||
                        idRol == ClPermisosROL.ClPermisosHelper.ROL_SUPERVISOR)
                    {
                        Response.Redirect("~/Vista/ListarUsuarios.aspx");
                    }
                    else if (idRol == ClPermisosROL.ClPermisosHelper.ROL_EMPLEADO)
                    {
                        Response.Redirect("~/Vista/Productos.aspx");
                    }
                    else if (idRol == ClPermisosROL.ClPermisosHelper.ROL_CLIENTE)
                    {
                        Response.Redirect("~/Vista/Productos.aspx");
                    }
                    else
                    {
                        Response.Redirect("~/Vista/Productos.aspx");
                    }
                }
                else
                {
                    MostrarError("Usuario o contraseña incorrectos.");
                }
            }
            catch (Exception ex)
            {
                MostrarError("Error al iniciar sesión: " + ex.Message);
            }
        }

        private string ObtenerNombreRol(int idRol)
        {
            if (idRol == ClPermisosROL.ClPermisosHelper.ROL_ADMINISTRADOR)
                return "Administrador";
            else if (idRol == ClPermisosROL.ClPermisosHelper.ROL_SUPERVISOR)
                return "Supervisor";
            else if (idRol == ClPermisosROL.ClPermisosHelper.ROL_EMPLEADO)
                return "Empleado";
            else if (idRol == ClPermisosROL.ClPermisosHelper.ROL_CLIENTE)
                return "Cliente";
            else
                return "Desconocido";
        }

        private void MostrarError(string mensaje)
        {
            pnlError.Visible = true;
            lblError.Text = mensaje;
        }
    }
}