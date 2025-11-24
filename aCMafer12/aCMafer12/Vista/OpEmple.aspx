<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OpEmple.aspx.cs" Inherits="AppAcmafer.Vista.OpEmple" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Panel de Empleado</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet">
</head>
<body>
    <form id="form1" runat="server">
        <div class="container mt-5">
            <div class="card">
                <div class="card-header bg-info text-white">
                    <h3>Panel de Empleado</h3>
                </div>
                <div class="card-body">
                    <asp:Label ID="lblBienvenida" runat="server" CssClass="h4"></asp:Label>
                    <br />
                    <asp:Label ID="lblRol" runat="server" CssClass="text-muted"></asp:Label>
                    
                    <hr />
                    
                    <div class="row mt-4">
                        <div class="col-md-6 mb-3">
                            <asp:Button ID="btnRegistrarVenta" runat="server" Text="Registrar Venta" 
                                CssClass="btn btn-success btn-lg w-100" OnClick="btnRegistrarVenta_Click" />
                        </div>
                        <div class="col-md-6 mb-3">
                            <asp:Button ID="btnConsultarInventario" runat="server" Text="Consultar Inventario" 
                                CssClass="btn btn-primary btn-lg w-100" OnClick="btnConsultarInventario_Click" />
                        </div>
                        <div class="col-md-6 mb-3">
                            <asp:Button ID="btnVerClientes" runat="server" Text="Ver Clientes" 
                                CssClass="btn btn-info btn-lg w-100" OnClick="btnVerClientes_Click" />
                        </div>
                        <div class="col-md-6 mb-3">
                            <asp:Button ID="btnMiPerfil" runat="server" Text="Mi Perfil" 
                                CssClass="btn btn-secondary btn-lg w-100" OnClick="btnMiPerfil_Click" />
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