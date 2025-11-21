using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppAcmafer.Vista
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            var email = txtEmail.Text.Trim();
            var password = txtPassword.Text;

            bool isValid = (email.Equals("admin@acmafer.com", StringComparison.OrdinalIgnoreCase) && password == "Admin123*")
                        || (email.Equals("user@acmafer.com", StringComparison.OrdinalIgnoreCase) && password == "User123*");

            if (!isValid)
            {
                vs.HeaderText = "Credenciales inválidas";
                return;
            }

            FormsAuthentication.SetAuthCookie(email, false);

            if (email.StartsWith("admin", StringComparison.OrdinalIgnoreCase))
                Response.Redirect("~/Admin/Dashboard.aspx");
            else
                Response.Redirect("~/Worker/Dashboard.aspx");
        }
    }
}
