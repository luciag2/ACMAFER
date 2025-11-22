<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="MenuRoles.aspx.cs" Inherits="AppAcmafer.Vista.MenuRoles" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Gestión de Roles</title>
    <style>
        * {
            margin: 0;
            padding: 0;
            box-sizing: border-box;
        }

        body {
            font-family: Arial, sans-serif;
            background-color: #1e1e1e;
        }

        .container-roles {
            display: flex;
            gap: 20px;
            padding: 20px;
            background-color: #1e1e1e;
            color: #fff;
            font-family: Arial, sans-serif;
            min-height: 100vh;
        }

        .panel-roles {
            flex: 0 0 220px;
            background-color: #252526;
            border-radius: 8px;
            padding: 15px;
            height: fit-content;
        }

        .panel-descripcion {
            flex: 1;
            background-color: #252526;
            border-radius: 8px;
            padding: 20px;
        }

        .titulo-seccion {
            font-size: 18px;
            font-weight: bold;
            margin-bottom: 15px;
            display: flex;
            justify-content: space-between;
            align-items: center;
        }

        .btn-rol {
            width: 100%;
            padding: 12px;
            margin-bottom: 10px;
            background-color: #7c3aed;
            color: white;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            font-size: 14px;
            transition: all 0.3s ease;
            text-align: left;
        }

        .btn-rol:hover {
            background-color: #6d28d9;
            transform: translateX(5px);
        }

        .btn-rol.selected {
            background-color: #a855f7;
            border: 2px solid #c084fc;
            box-shadow: 0 0 10px rgba(168, 85, 247, 0.5);
        }

        .btn-agregar {
            background-color: transparent;
            color: #fff;
            border: 1px solid #fff;
            padding: 8px 15px;
            border-radius: 4px;
            cursor: pointer;
            font-size: 14px;
            transition: all 0.3s ease;
        }

        .btn-agregar:hover {
            background-color: #374151;
            border-color: #7c3aed;
            color: #7c3aed;
        }

        .descripcion-box {
            background-color: #1e1e1e;
            border: 1px solid #3f3f46;
            border-radius: 5px;
            padding: 15px;
            margin-bottom: 20px;
            min-height: 100px;
            line-height: 1.6;
        }

        .btn-editar {
            background-color: #374151;
            color: white;
            border: none;
            padding: 8px 15px;
            border-radius: 4px;
            cursor: pointer;
            transition: all 0.3s ease;
        }

        .btn-editar:hover {
            background-color: #4b5563;
        }

        .seccion-tarea {
            background-color: #1e1e1e;
            border: 1px solid #3f3f46;
            border-radius: 5px;
            padding: 20px;
        }

        .tarea-header {
            display: flex;
            justify-content: space-between;
            align-items: center;
            margin-bottom: 20px;
            padding-bottom: 15px;
            border-bottom: 1px solid #3f3f46;
        }

        .progreso {
            font-size: 14px;
            color: #9ca3af;
            margin-left: 10px;
            padding: 4px 12px;
            background-color: #374151;
            border-radius: 12px;
        }

        .btn-eliminar {
            background-color: transparent;
            color: #ef4444;
            border: 1px solid #ef4444;
            padding: 6px 12px;
            border-radius: 4px;
            cursor: pointer;
            transition: all 0.3s ease;
        }

        .btn-eliminar:hover {
            background-color: #ef4444;
            color: white;
        }

        .checkbox-container {
            margin: 15px 0;
        }

        .checkbox-item {
            display: flex;
            align-items: center;
            margin-bottom: 12px;
            padding: 10px;
            border-radius: 5px;
            transition: background-color 0.2s ease;
        }

        .checkbox-item:hover {
            background-color: #2d2d30;
        }

        .checkbox-item input[type="checkbox"] {
            width: 18px;
            height: 18px;
            margin-right: 10px;
            cursor: pointer;
            accent-color: #7c3aed;
        }

        .checkbox-item label {
            cursor: pointer;
            font-size: 14px;
            user-select: none;
        }

        .btn-guardar {
            background-color: #10b981;
            color: white;
            border: none;
            padding: 12px 40px;
            border-radius: 5px;
            cursor: pointer;
            font-size: 14px;
            margin-top: 20px;
            transition: all 0.3s ease;
            font-weight: bold;
        }

        .btn-guardar:hover {
            background-color: #059669;
            transform: translateY(-2px);
            box-shadow: 0 4px 12px rgba(16, 185, 129, 0.3);
        }

        .no-rol-seleccionado {
            text-align: center;
            padding: 40px;
            color: #9ca3af;
            font-size: 16px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-roles">
            <!-- Panel de Roles -->
            <div class="panel-roles">
                <div class="titulo-seccion">
                    <span>📋 Menú de roles</span>
                    <asp:Button ID="btnAgregarRol" runat="server" Text="+ Añadir" CssClass="btn-agregar" OnClick="btnAgregarRol_Click" />
                </div>
                
                <asp:Repeater ID="rptRoles" runat="server" OnItemCommand="rptRoles_ItemCommand">
                    <ItemTemplate>
                        <asp:Button 
                            ID="btnRol" 
                            runat="server"
                            Text='<%# Eval("NombreRol") %>'
                            CommandName="SeleccionarRol"
                            CommandArgument='<%# Eval("IdRol") %>'
                            
                        />
                    </ItemTemplate>
                </asp:Repeater>
            </div>

            <!-- Panel de Descripción y Permisos -->
            <div class="panel-descripcion">
                <asp:Panel ID="pnlRolSeleccionado" runat="server" Visible="true">
                    <!-- Sección Descripción -->
                    <div class="titulo-seccion" style="align-items: flex-start;">
                        <span>📝 Descripción</span>
                        <asp:Button ID="btnEditarDescripcion" runat="server" Text="Editar" CssClass="btn-editar" OnClick="btnEditarDescripcion_Click" />
                    </div>
                    
                    <div class="descripcion-box">
                        <asp:Label ID="lblDescripcion" runat="server" Text="Seleccione un rol para ver su descripción"></asp:Label>
                    </div>

                    <!-- Sección Permisos -->
                    <div class="seccion-tarea">
                        <div class="tarea-header">
                            <div style="display: flex; align-items: center;">
                                <span style="font-size: 16px; font-weight: bold;">🔐 Permisos</span>
                                <span class="progreso">
                                    <asp:Label ID="lblProgreso" runat="server" Text="0%"></asp:Label>
                                </span>
                            </div>
                            <asp:Button ID="btnEliminarRol" runat="server" Text="Eliminar Rol" CssClass="btn-eliminar" OnClick="btnEliminarRol_Click" />
                        </div>

                        <div class="checkbox-container">
                            <div class="checkbox-item">
                                <asp:CheckBox ID="chkAccesoMenu" runat="server" AutoPostBack="true" OnCheckedChanged="ActualizarProgreso" />
                                <label for="<%= chkAccesoMenu.ClientID %>">Permitir acceso al menú de funciones</label>
                            </div>
                            
                            <div class="checkbox-item">
                                <asp:CheckBox ID="chkMenuVisible" runat="server" AutoPostBack="true" OnCheckedChanged="ActualizarProgreso" />
                                <label for="<%= chkMenuVisible.ClientID %>">Menú visible en la interfaz</label>
                            </div>
                            
                            <div class="checkbox-item">
                                <asp:CheckBox ID="chkRedireccion" runat="server" AutoPostBack="true" OnCheckedChanged="ActualizarProgreso" />
                                <label for="<%= chkRedireccion.ClientID %>">Redirección automática a secciones</label>
                            </div>

                            <div class="checkbox-item">
                                <asp:CheckBox ID="chkGestionUsuarios" runat="server" AutoPostBack="true" OnCheckedChanged="ActualizarProgreso" />
                                <label for="<%= chkGestionUsuarios.ClientID %>">Gestión de Usuarios</label>
                            </div>

                            <div class="checkbox-item">
                                <asp:CheckBox ID="chkGestionProductos" runat="server" AutoPostBack="true" OnCheckedChanged="ActualizarProgreso" />
                                <label for="<%= chkGestionProductos.ClientID %>">Gestión de Productos</label>
                            </div>

                            <div class="checkbox-item">
                                <asp:CheckBox ID="chkGestionPedidos" runat="server" AutoPostBack="true" OnCheckedChanged="ActualizarProgreso" />
                                <label for="<%= chkGestionPedidos.ClientID %>">Gestión de Pedidos</label>
                            </div>

                            <div class="checkbox-item">
                                <asp:CheckBox ID="chkGestionTareas" runat="server" AutoPostBack="true" OnCheckedChanged="ActualizarProgreso" />
                                <label for="<%= chkGestionTareas.ClientID %>">Gestión de Tareas</label>
                            </div>

                            <div class="checkbox-item">
                                <asp:CheckBox ID="chkReportes" runat="server" AutoPostBack="true" OnCheckedChanged="ActualizarProgreso" />
                                <label for="<%= chkReportes.ClientID %>">Acceso a Reportes</label>
                            </div>

                            <div class="checkbox-item">
                                <asp:CheckBox ID="chkConfiguracionRoles" runat="server" AutoPostBack="true" OnCheckedChanged="ActualizarProgreso" />
                                <label for="<%= chkConfiguracionRoles.ClientID %>">Configuración de Roles y Permisos</label>
                            </div>
                        </div>

                        <asp:Button ID="btnGuardar" runat="server" Text="💾 Guardar Permisos" CssClass="btn-guardar" OnClick="btnGuardar_Click" />
                    </div>
                </asp:Panel>

                <asp:Panel ID="pnlNoSeleccionado" runat="server" Visible="false">
                    <div class="no-rol-seleccionado">
                        <p>👈 Seleccione un rol del panel izquierdo para ver sus detalles y permisos</p>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </form>
</body>
</html>