<%@ Page Title="Asignación de Tareas" Language="C#" MasterPageFile="~/Vista/MasterPage.Master" AutoEventWireup="true" CodeBehind="AsignarTareas.aspx.cs" Inherits="AppAcmafer.Vista.AsignarTareas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="background: white; padding: 30px; border-radius: 10px; box-shadow: 0 2px 10px rgba(0,0,0,0.1);">
        <h2 style="color: #2c3e50; margin-bottom: 20px; border-bottom: 3px solid #e74c3c; padding-bottom: 10px;">
            ✅ Asignación de Tareas a Empleados
        </h2>
        
        
        <div style="background: #fff3cd; border-left: 4px solid #ffc107; padding: 15px; margin-bottom: 20px; border-radius: 5px;">
            <strong>ℹ️ Información:</strong> Esta sección solo está disponible para Administradores y Supervisores.
        </div>
        
        
        <div style="margin-bottom: 20px; padding: 15px; background: #e8f5e9; border-radius: 5px;">
            <div style="display: flex; gap: 15px; align-items: center; flex-wrap: wrap;">
                <div>
                    <asp:Label ID="lblFiltroEmpleado" runat="server" Text="Empleado:" Font-Bold="true"></asp:Label>
                    <asp:TextBox ID="txtFiltroEmpleado" runat="server" placeholder="Nombre empleado..." 
                        style="padding: 8px; width: 200px; border: 1px solid #bdc3c7; border-radius: 5px; margin-left: 5px;"></asp:TextBox>

                
                <div>
                    <asp:Label ID="lblFiltroFecha" runat="server" Text="Fecha:" Font-Bold="true"></asp:Label>
                    <asp:TextBox ID="txtFiltroFecha" runat="server" TextMode="Date" 
                        style="padding: 8px; border: 1px solid #bdc3c7; border-radius: 5px; margin-left: 5px;"></asp:TextBox>
                </div>
                
                <asp:Button ID="btnFiltrar" runat="server" Text="🔍 Filtrar" OnClick="btnFiltrar_Click" 
                    style="padding: 8px 20px; background: linear-gradient(135deg, #b8860b 0%, #d4af37 50%, #ffd700 100%); color: #1f1f1f; border: none; border-radius: 5px; cursor: pointer;" />
                <asp:Button ID="btnLimpiar" runat="server" Text="🔄 Limpiar" OnClick="btnLimpiar_Click" 
                    style="padding: 8px 20px; background: linear-gradient(135deg, #8b0000 0%, #ff0000 25%, #b30000 50%, #ff4d4d 75%, #660000 100%); color: #1f1f1f; border: none; border-radius: 5px; cursor: pointer;" />
            </div>
        </div>
        
        <!-- GridView de Asignaciones -->
        <asp:GridView ID="gvAsignaciones" runat="server" 
                      AutoGenerateColumns="False" 
                      CellPadding="12"
                      GridLines="None"
                      Width="100%"
                      BackColor="White"
                      EmptyDataText="No hay tareas asignadas">
            
            <HeaderStyle BackColor="#2c3e50" ForeColor="White" Font-Bold="true" Height="45px" />
            <RowStyle BackColor="#d1d5db" Height="40px" />
            <AlternatingRowStyle BackColor="White" />
            
            <Columns>
                <asp:BoundField DataField="IdAsignacionTarea" HeaderText="ID" />
                <asp:BoundField DataField="TituloTarea" HeaderText="Tarea" />
                <asp:BoundField DataField="NombreEmpleado" HeaderText="Empleado" />
                <asp:BoundField DataField="NombreAdmin" HeaderText="Asignado por" />
                
                <asp:TemplateField HeaderText="Fecha Inicio">
                    <ItemTemplate>
                        <div>
                            📅 <%# ((DateTime)Eval("FechaInicio")).ToString("dd/MM/yyyy") %><br />
                            🕒 <%# Eval("HoraInicio") %>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Fecha Fin">
                    <ItemTemplate>
                        <div>
                            📅 <%# ((DateTime)Eval("FechaFin")).ToString("dd/MM/yyyy") %><br />
                            🕒 <%# Eval("HoraFin") %>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Estado">
                    <ItemTemplate>
                        <span style="padding: 5px 10px; border-radius: 15px; background: linear-gradient(90deg, #2c2c2c, #555555, #b5b5b5, #7a7a7a, #d6d6d6, #5f5f5f, #2c2c2c); color: #000000; font-size: 12px;">
                            <%# EvaluarEstado((DateTime)Eval("FechaFin")) %>
                        </span>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Acciones">
                    <ItemTemplate>
                        <asp:Button ID="btnVerComentario" runat="server" 
                                   Text="💬 Ver" 
                                   CommandArgument='<%# Eval("IdAsignacionTarea") %>' 
                                   OnClick="btnVerComentario_Click"
                                   ToolTip='<%# Eval("ComentarioAdmin") %>'
                                   style="padding: 5px 15px; background: linear-gradient(90deg, #0a1a2f 0%, #0f355c 20%, #1a4d85 35%, #68a4ff 50%, #1a4d85 65%, #0f355c 80%, #0a1a2f 100%); color: white; border: none; border-radius: 5px; cursor: pointer; margin-right: 5px;" />
                        <asp:Button ID="btnEditar" runat="server" 
                                   Text="✏️ Editar" 
                                   CommandArgument='<%# Eval("IdAsignacionTarea") %>' 
                                   OnClick="btnEditar_Click"
                                   style="padding: 5px 15px; background: linear-gradient(135deg, #b8860b 0%, #d4af37 50%, #ffd700 100%); color: #1f1f1f; border: none; border-radius: 5px; cursor: pointer;" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        
        <!-- Panel de Comentario -->
        <asp:Panel ID="pnlComentario" runat="server" Visible="false" 
            style="margin-top: 20px; padding: 20px; background: #fff8e1; border-left: 4px solid #ffc107; border-radius: 5px;">
            <h4 style="color: #f57c00; margin-bottom: 10px;">💬 Comentario del Administrador</h4>
            <asp:Label ID="lblComentario" runat="server" style="display: block; color: #555; line-height: 1.6;"></asp:Label>
            <asp:Button ID="btnCerrarComentario" runat="server" Text="✖️ Cerrar" OnClick="btnCerrarComentario_Click"
                style="margin-top: 15px; padding: 8px 20px; background: #e74c3c; color: white; border: none; border-radius: 5px; cursor: pointer;" />
        </asp:Panel>
        
        <!-- Estadísticas -->
        <div style="margin-top: 30px; display: grid; grid-template-columns: repeat(auto-fit, minmax(200px, 1fr)); gap: 20px;">
            <div style="background: linear-gradient(90deg, #2c2c2c, #555555, #b5b5b5, #7a7a7a, #d6d6d6, #5f5f5f, #2c2c2c); padding: 20px; border-radius: 10px; color: #1f1f1f; text-align: center;">
                <div style="font-size: 14px; opacity: 0.9;">Total Asignaciones</div>
                <div style="font-size: 32px; font-weight: bold; margin-top: 10px;">
                    <asp:Label ID="lblTotalAsignaciones" runat="server" Text="0"></asp:Label>
                </div>
            </div>
            
            <div style="background: linear-gradient(90deg, #0a1a2f 0%, #0f355c 20%, #1a4d85 35%, #68a4ff 50%, #1a4d85 65%, #0f355c 80%, #0a1a2f 100%); padding: 20px; border-radius: 10px; color: #1f1f1f; text-align: center;">
                <div style="font-size: 14px; opacity: 0.9;">En Progreso</div>
                <div style="font-size: 32px; font-weight: bold; margin-top: 10px;">
                    <asp:Label ID="lblEnProgreso" runat="server" Text="0"></asp:Label>
                </div>
            </div>
            
            <div style="background: linear-gradient(135deg, #0a1f0a 0%, #1f7a1f 25%, #b6ffb6 50%, #1f7a1f 75%, #0a1f0a 100%); padding: 20px; border-radius: 10px; color: #1f1f1f; text-align: center;">
                <div style="font-size: 14px; opacity: 0.9;">Completadas</div>
                <div style="font-size: 32px; font-weight: bold; margin-top: 10px;">
                    <asp:Label ID="lblCompletadas" runat="server" Text="0"></asp:Label>
                </div>
            </div>
            
            <div style="background: linear-gradient(135deg, #8b0000 0%, #ff0000 25%, #b30000 50%, #ff4d4d 75%, #660000 100%); padding: 20px; border-radius: 10px; color: #1f1f1f; text-align: center;">
                <div style="font-size: 14px; opacity: 0.9;">Vencidas</div>
                <div style="font-size: 32px; font-weight: bold; margin-top: 10px;">
                    <asp:Label ID="lblVencidas" runat="server" Text="0"></asp:Label>
                </div>
            </div>
        </div>
        
        <!-- Mensaje -->
        <asp:Label ID="lblMensaje" runat="server" Font-Bold="true" style="display: block; margin-top: 20px;"></asp:Label>
    </div>
</asp:Content>
