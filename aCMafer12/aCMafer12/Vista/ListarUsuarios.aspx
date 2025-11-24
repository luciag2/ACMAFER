

<%@ Page Language="C#" MasterPageFile="~/Vista/MasterPage.Master" AutoEventWireup="true" CodeBehind="ListarUsuarios.aspx.cs" Inherits="AppAcmafer.Vista.ListarUsuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <!-- ELIMINA: la etiqueta <form> porque ya está en el MasterPage -->
    <!-- ELIMINA: todo el header con el menú porque ahora viene del MasterPage -->
    
    <!-- Solo deja el contenido de la tarjeta -->
    <div class="card-section" style="background: white; border-radius: 8px; box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1); padding: 30px;">
        
        <div class="section-title" style="font-size: 22px; font-weight: 600; color: #333; margin-bottom: 10px; display: flex; align-items: center; gap: 10px;">
            <i class="fas fa-list-check" style="color: #d97736;"></i>
            Listado de Usuarios Registrados
        </div>
        <div style="border-bottom: 3px solid #d97736; margin-bottom: 25px;"></div>

        <!-- Alerta informativa -->
        <div style="background-color: #fef3cd; border-left: 5px solid #ffc107; padding: 15px; border-radius: 4px; margin-bottom: 25px; display: flex; gap: 15px; align-items: flex-start;">
            <i class="fas fa-info-circle" style="color: #0c5460; font-size: 18px; margin-top: 3px;"></i>
            <p style="margin: 0; color: #856404; font-weight: 500;"><strong>Información:</strong> Esta sección solo está disponible para Administradores y Supervisores.</p>
        </div>

        <!-- Mensaje de error -->
        <asp:Label ID="lblMensajeError" runat="server" Visible="false" style="background-color: #f8d7da; border: 1px solid #f5c6cb; color: #721c24; padding: 15px; border-radius: 4px; margin-bottom: 20px; display: flex; align-items: center; gap: 10px;">
            <i class="fas fa-exclamation-circle" style="font-size: 18px;"></i>
            <span runat="server" id="spanError"></span>
        </asp:Label>

        <!-- Sección de filtros -->
        <div style="background-color: #f9f9f9; padding: 20px; border-radius: 6px; margin-bottom: 25px; display: flex; gap: 15px; align-items: flex-end; flex-wrap: wrap;">
            <div style="display: flex; flex-direction: column; gap: 5px;">
                <label style="font-weight: 600; color: #555; font-size: 14px;">Buscar por Nombre:</label>
                <asp:TextBox ID="txtBuscar" runat="server" placeholder="Nombre del usuario..." style="padding: 10px 12px; border: 1px solid #ddd; border-radius: 4px; font-size: 14px; min-width: 200px;"></asp:TextBox>
            </div>
            <div style="display: flex; flex-direction: column; gap: 5px;">
                <label style="font-weight: 600; color: #555; font-size: 14px;">Filtrar por Rol:</label>
                <asp:DropDownList ID="ddlRol" runat="server" style="padding: 10px 12px; border: 1px solid #ddd; border-radius: 4px; font-size: 14px; min-width: 200px;">
                    <asp:ListItem Value="">-- Todos --</asp:ListItem>
                    <asp:ListItem Value="Administrador">Administrador</asp:ListItem>
                    <asp:ListItem Value="Empleado">Empleado</asp:ListItem>
                    <asp:ListItem Value="Cliente">Cliente</asp:ListItem>
                    <asp:ListItem Value="Supervisor">Supervisor</asp:ListItem>
                </asp:DropDownList>
            </div>
            <asp:Button ID="btnFiltrar" runat="server" Text="🔍 Filtrar" OnClick="BtnFiltrar_Click" 
                style="background-color: #ffc107; color: #333; border: none; padding: 10px 30px; border-radius: 4px; font-weight: 600; cursor: pointer;" />
        </div>

        <!-- Tabla de usuarios -->
        <div style="overflow-x: auto;">
            <asp:Repeater ID="rptUsuarios" runat="server">
                <HeaderTemplate>
                    <table style="width: 100%; border-collapse: collapse; font-size: 14px;">
                        <thead style="background-color: #2c3e50; color: white;">
                            <tr>
                                <th style="padding: 15px; text-align: left; font-weight: 600; border-bottom: 2px solid #1a252f; width: 50px;">ID</th>
                                <th style="padding: 15px; text-align: left; font-weight: 600; border-bottom: 2px solid #1a252f; width: 120px;">Documento</th>
                                <th style="padding: 15px; text-align: left; font-weight: 600; border-bottom: 2px solid #1a252f;">Nombre Completo</th>
                                <th style="padding: 15px; text-align: left; font-weight: 600; border-bottom: 2px solid #1a252f;">Correo</th>
                                <th style="padding: 15px; text-align: left; font-weight: 600; border-bottom: 2px solid #1a252f; width: 120px;">Rol</th>
                                <th style="padding: 15px; text-align: left; font-weight: 600; border-bottom: 2px solid #1a252f; width: 100px;">Estado</th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr style="border-bottom: 1px solid #ecf0f1;">
                        <td style="padding: 15px; font-weight: 600; color: #d97736;"><%# Eval("IdUsuario") %></td>
                        <td style="padding: 15px;"><strong><%# Eval("Documento") %></strong></td>
                        <td style="padding: 15px;"><%# Eval("NombreCompleto") %></td>
                        <td style="padding: 15px;"><%# Eval("Email") %></td>
                        <td style="padding: 15px;"><%# Eval("Rol") %></td>
                        <td style="padding: 15px;">
                            <span style="display: inline-block; padding: 6px 12px; border-radius: 20px; font-size: 12px; font-weight: 600; text-align: center; min-width: 80px; 
                                background-color: <%# Eval("Estado").ToString() == "Activo" ? "#d4edda" : "#f8d7da" %>; 
                                color: <%# Eval("Estado").ToString() == "Activo" ? "#155724" : "#721c24" %>;">
                                <%# Eval("Estado") %>
                            </span>
                        </td>
                        
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                        </tbody>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>

        <!-- Mensaje sin datos -->
        <asp:Label ID="lblSinDatos" runat="server" Visible="false" style="text-align: center; padding: 40px; color: #999; font-size: 16px;">
            <i class="fas fa-inbox" style="font-size: 40px; margin-bottom: 10px;"></i><br />
            No hay usuarios para mostrar.
        </asp:Label>

        <!-- Estadísticas -->
        <div style="display: flex; gap: 20px; margin-top: 25px; padding-top: 25px; border-top: 1px solid #ecf0f1;">
            <div style="display: flex; align-items: center; gap: 15px;">
                <div style="font-size: 24px; color: #d97736;"><i class="fas fa-users"></i></div>
                <div>
                    <h4 style="margin: 0; font-size: 14px; color: #999;">Total de Usuarios</h4>
                    <p style="margin: 0; font-size: 24px; font-weight: 700; color: #333;"><asp:Label ID="lblTotalUsuarios" runat="server">0</asp:Label></p>
                </div>
            </div>
            <div style="display: flex; align-items: center; gap: 15px;">
                <div style="font-size: 24px; color: #d97736;"><i class="fas fa-check-circle"></i></div>
                <div>
                    <h4 style="margin: 0; font-size: 14px; color: #999;">Usuarios Activos</h4>
                    <p style="margin: 0; font-size: 24px; font-weight: 700; color: #333;"><asp:Label ID="lblUsuariosActivos" runat="server">0</asp:Label></p>
                </div>
            </div>
            <div style="display: flex; align-items: center; gap: 15px;">
                <div style="font-size: 24px; color: #d97736;"><i class="fas fa-user-shield"></i></div>
                <div>
                    <h4 style="margin: 0; font-size: 14px; color: #999;">Administradores</h4>
                    <p style="margin: 0; font-size: 24px; font-weight: 700; color: #333;"><asp:Label ID="lblAdministradores" runat="server">0</asp:Label></p>
                </div>
            </div>
        </div>
    </div>

</asp:Content>