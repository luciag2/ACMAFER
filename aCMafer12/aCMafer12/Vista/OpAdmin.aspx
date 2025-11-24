<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OpAdmin.aspx.cs" Inherits="AppAcmafer.Vista.OpAdmin" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Panel de Administrador</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet">
</head>
<body>
    <form id="form1" runat="server">
        <div class="container mt-5">
            <div class="card">
                <div class="card-header bg-primary text-white">
                    <h3>Panel de Administrador</h3>
                </div>
                <div class="card-body">
                    <asp:Label ID="lblBienvenida" runat="server" CssClass="h4"></asp:Label>
                    <br />
                    <asp:Label ID="lblRol" runat="server" CssClass="text-muted"></asp:Label>
                    
                    <hr />
                    
                    <div class="row mt-4">
                        <div class="col-md-6 mb-3">
                            <asp:Button ID="btnGestionarUsuarios" runat="server" Text="Gestionar Usuarios" 
                                CssClass="btn btn-primary btn-lg w-100" OnClick="btnGestionarUsuarios_Click" />
                        </div>
                        <div class="col-md-6 mb-3">
                            <asp:Button ID="btnGestionarRoles" runat="server" Text="Gestionar Roles" 
                                CssClass="btn btn-info btn-lg w-100" OnClick="btnGestionarRoles_Click" />
                        </div>
                        <div class="col-md-6 mb-3">
                            <asp:Button ID="btnReportes" runat="server" Text="Ver Reportes" 
                                CssClass="btn btn-success btn-lg w-100" OnClick="btnReportes_Click" />
                        </div>
                        <div class="col-md-6 mb-3">
                            <asp:Button ID="btnConfiguracion" runat="server" Text="Configuración del Sistema" 
                                CssClass="btn btn-warning btn-lg w-100" OnClick="btnConfiguracion_Click" />
                        </div>
                    </div>
                    
                    <hr />
                    
                    <asp:Button ID="btnCerrarSesion" runat="server" Text="Cerrar Sesión" 
                        CssClass="btn btn-danger" OnClick="btnCerrarSesion_Click" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>