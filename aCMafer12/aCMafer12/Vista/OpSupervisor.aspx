<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OpSupervisor.aspx.cs" Inherits="AppAcmafer.Vista.OpSupervisor" %>


<!DOCTYPE html>

<html>
<head runat="server">
    <title>Panel de Supervisor</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet">
</head>
<body>
    <form id="form1" runat="server">
        <div class="container mt-5">
            <div class="card">
                <div class="card-header bg-warning text-dark">
                    <h3>Panel de Supervisor</h3>
                </div>
                <div class="card-body">
                    <asp:Label ID="lblBienvenida" runat="server" CssClass="h4"></asp:Label>
                    <br />
                    <asp:Label ID="lblRol" runat="server" CssClass="text-muted"></asp:Label>
                    
                    <hr />
                    
                    <div class="row mt-4">
                        <div class="col-md-6 mb-3">
                            <asp:Button ID="btnSupervisionVentas" runat="server" Text="Supervisión de Ventas" 
                                CssClass="btn btn-warning btn-lg w-100" OnClick="btnSupervisionVentas_Click" />
                        </div>
                        <div class="col-md-6 mb-3">
                            <asp:Button ID="btnReportesGenerales" runat="server" Text="Reportes Generales" 
                                CssClass="btn btn-info btn-lg w-100" OnClick="btnReportesGenerales_Click" />
                        </div>
                        <div class="col-md-6 mb-3">
                            <asp:Button ID="btnGestionInventario" runat="server" Text="Gestión de Inventario" 
                                CssClass="btn btn-primary btn-lg w-100" OnClick="btnGestionInventario_Click" />
                        </div>
                        <div class="col-md-6 mb-3">
                            <asp:Button ID="btnSupervisionEmpleados" runat="server" Text="Supervisión de Empleados" 
                                CssClass="btn btn-success btn-lg w-100" OnClick="btnSupervisionEmpleados_Click" />
                        </div>
                        <div class="col-md-6 mb-3">
                            <asp:Button ID="btnEstadisticas" runat="server" Text="Estadísticas" 
                                CssClass="btn btn-dark btn-lg w-100" OnClick="btnEstadisticas_Click" />
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
