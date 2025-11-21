<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" 
    Inherits="AppAcmafer.Vista.Login" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>ACMAFER - Login</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" 
          rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container" style="max-width:420px; margin-top:60px;">
            <div class="card shadow-sm">
                <div class="card-body">
                    <h3 class="mb-3 text-center">ACMAFER | Ingreso</h3>
                    <asp:ValidationSummary ID="vs" runat="server" CssClass="text-danger" />
                    <div class="mb-3">
                        <label for="txtEmail" class="form-label">Correo</label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" />
                        <asp:RequiredFieldValidator ID="rfvEmail" runat="server" 
                            ControlToValidate="txtEmail" 
                            ErrorMessage="Correo es obligatorio" 
                            CssClass="text-danger" 
                            Display="Dynamic" />
                    </div>
                    <div class="mb-3">
                        <label for="txtPassword" class="form-label">Contraseña</label>
                        <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" />
                        <asp:RequiredFieldValidator ID="rfvPass" runat="server" 
                            ControlToValidate="txtPassword" 
                            ErrorMessage="Contraseña es obligatoria" 
                            CssClass="text-danger" 
                            Display="Dynamic" />
                    </div>
                    <div class="d-grid gap-2">
                        <asp:Button ID="btnLogin" runat="server" CssClass="btn btn-primary" 
                            Text="Ingresar" OnClick="btnLogin_Click" />
                    </div>
                    <div class="mt-3 text-center">
                        <a href="Recover.aspx">¿Olvidaste tu contraseña?</a>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>