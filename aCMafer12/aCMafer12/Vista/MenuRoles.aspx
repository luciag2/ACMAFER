<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MenuRoles.aspx.cs" Inherits="AppAcmafer.Vista.MenuRoles" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Gestión de Roles</title>
    <style>
        /* CSS Modificado: Estilo Claro y Limpio con Acentos Naranja/Ámbar */
        * {
            margin: 0;
            padding: 0;
            box-sizing: border-box;
        }

        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            /* Fondo muy claro */
            background-color: #f8f9fa; 
            color: #343a40; /* Texto oscuro para buen contraste */
            min-height: 100vh;
            display: flex;
            justify-content: center;
            align-items: flex-start; /* Alineación superior en el inicio */
            padding-top: 40px;
        }

        .container-roles {
            display: flex;
            gap: 25px; 
            padding: 30px;
            background-color: transparent; /* El contenedor es transparente para usar el fondo del body */
            border-radius: 12px;
            max-width: 1200px;
            width: 100%;
        }

        .panel-roles {
            flex: 0 0 260px;
            /* Panel en blanco */
            background-color: white; 
            border-radius: 10px;
            padding: 20px;
            height: fit-content;
            /* Sombra sutil */
            box-shadow: 0 4px 15px rgba(0, 0, 0, 0.1); 
            border: 1px solid #e9ecef;
        }

        .panel-descripcion {
            flex: 1;
            /* Panel en blanco */
            background-color: white; 
            border-radius: 10px;
            padding: 30px;
            /* Sombra sutil */
            box-shadow: 0 4px 15px rgba(0, 0, 0, 0.1);
            border: 1px solid #e9ecef;
        }

        .titulo-seccion {
            font-size: 22px; 
            font-weight: bold;
            margin-bottom: 25px;
            display: flex;
            justify-content: space-between;
            align-items: center;
            /* Título en color azul oscuro (gris) */
            color: #2c3e50; 
            /* Borde más claro */
            border-bottom: 2px solid #ced4da; 
            padding-bottom: 12px;
        }

        .btn-rol {
            width: 100%;
            padding: 14px;
            margin-bottom: 10px;
            /* Botón de rol inactivo en gris claro */
            background-color: #f1f3f5; 
            color: #495057; /* Texto oscuro */
            border: none;
            border-radius: 6px;
            cursor: pointer;
            font-size: 16px;
            transition: all 0.3s ease;
            text-align: left;
            position: relative;
        }
        
        .btn-rol::after {
            content: '›'; 
            position: absolute;
            right: 15px;
            top: 50%;
            transform: translateY(-50%);
            font-size: 1.2em;
            color: #adb5bd;
            transition: color 0.3s ease, transform 0.3s ease;
        }

        .btn-rol:hover {
            /* Naranja/Ámbar al pasar el ratón */
            background-color: #f39c12; 
            transform: translateX(5px);
            color: white; /* Texto blanco en hover */
        }

        .btn-rol:hover::after {
            color: white;
            transform: translateY(-50%) translateX(3px);
        }

        .btn-rol.selected {
            /* Rol seleccionado en naranja más oscuro */
            background-color: #e67e22; 
            border: 2px solid #f39c12;
            box-shadow: 0 0 15px rgba(243, 156, 18, 0.3);
            color: white;
            font-weight: bold;
        }
        .btn-rol.selected::after {
            color: white;
            transform: translateY(-50%) translateX(3px);
        }

        .btn-agregar {
            background-color: transparent;
            color: #f39c12;
            border: 1px solid #f39c12;
            padding: 10px 18px;
            border-radius: 6px;
            cursor: pointer;
            font-size: 15px;
            font-weight: 600;
            transition: all 0.3s ease;
        }

        .btn-agregar:hover {
            background-color: #f39c12;
            color: white;
            box-shadow: 0 2px 8px rgba(243, 156, 18, 0.4);
        }

        .descripcion-box {
            /* Fondo muy claro para la descripción */
            background-color: #f8f9fa; 
            border: 1px solid #ced4da;
            border-radius: 8px;
            padding: 20px;
            margin-bottom: 30px;
            min-height: 120px;
            line-height: 1.8;
            color: #495057;
            font-size: 15px;
            /* Sombra interna sutil */
            box-shadow: inset 0 1px 3px rgba(0, 0, 0, 0.05); 
        }

        .btn-editar {
            /* Botón secundario en gris azulado */
            background-color: #6c757d; 
            color: white;
            border: none;
            padding: 10px 18px;
            border-radius: 6px;
            cursor: pointer;
            font-size: 15px;
            font-weight: 600;
            transition: all 0.3s ease;
        }

        .btn-editar:hover {
            background-color: #5a6268;
            transform: translateY(-1px);
        }

        .seccion-tarea {
            /* Fondo muy claro para la sección de permisos */
            background-color: #f8f9fa; 
            border: 1px solid #ced4da;
            border-radius: 8px;
            padding: 25px;
        }

        .tarea-header {
            display: flex;
            justify-content: space-between;
            align-items: center;
            margin-bottom: 25px;
            padding-bottom: 15px;
            border-bottom: 1px solid #dee2e6;
        }

        .progreso {
            font-size: 15px;
            color: white;
            margin-left: 15px;
            padding: 6px 15px;
            /* Fondo de progreso en azul oscuro */
            background-color: #2c3e50; 
            border-radius: 15px;
            font-weight: bold;
            box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
        }

        .btn-eliminar {
            background-color: transparent;
            color: #dc3545; /* Rojo de Bootstrap */
            border: 1px solid #dc3545;
            padding: 8px 15px;
            border-radius: 6px;
            cursor: pointer;
            font-weight: 600;
            transition: all 0.3s ease;
        }

        .btn-eliminar:hover {
            background-color: #dc3545;
            color: white;
            box-shadow: 0 2px 8px rgba(220, 53, 69, 0.4);
        }

        .checkbox-container {
            margin: 20px 0;
        }

        .checkbox-item {
            display: flex;
            align-items: center;
            margin-bottom: 15px;
            padding: 12px;
            border-radius: 6px;
            transition: background-color 0.2s ease;
        }

        .checkbox-item:hover {
            background-color: #e9ecef; /* Gris claro al pasar el ratón */
        }

        .checkbox-item input[type="checkbox"] {
            width: 20px;
            height: 20px;
            margin-right: 12px;
            cursor: pointer;
            /* Acento del checkbox en naranja/ámbar */
            accent-color: #f39c12; 
            border: 1px solid #ced4da;
        }

        .checkbox-item label {
            cursor: pointer;
            font-size: 15px;
            user-select: none;
            color: #495057;
        }

        .btn-guardar {
            /* Naranja/Ámbar para el botón principal */
            background-color: #f39c12; 
            color: white;
            border: none;
            padding: 16px 50px;
            border-radius: 8px;
            cursor: pointer;
            font-size: 17px;
            margin-top: 35px;
            transition: all 0.3s ease;
            font-weight: bold;
            display: block; 
            margin-left: auto;
            margin-right: auto;
            box-shadow: 0 4px 15px rgba(243, 156, 18, 0.4);
        }

        .btn-guardar:hover {
            background-color: #e67e22;
            transform: translateY(-2px);
            box-shadow: 0 8px 20px rgba(243, 156, 18, 0.5);
        }

        .no-rol-seleccionado {
            text-align: center;
            padding: 50px;
            color: #6c757d;
            font-size: 18px;
            line-height: 1.5;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-roles">
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
                            CssClass="btn-rol"
                        />
                    </ItemTemplate>
                </asp:Repeater>
            </div>

            <div class="panel-descripcion">
                <asp:Panel ID="pnlRolSeleccionado" runat="server" Visible="false">
                    <div class="titulo-seccion" style="align-items: flex-start;">
                        <span>📝 Rol Seleccionado: <asp:Label ID="lblNombreRol" runat="server" Text=""></asp:Label></span>
                        <asp:Button ID="btnEditarDescripcion" runat="server" Text="✏️ Editar" CssClass="btn-editar" OnClick="btnEditarDescripcion_Click" />
                    </div>
                    
                    <div class="descripcion-box">
                        <asp:Label ID="lblDescripcion" runat="server" Text="Descripción del rol..."></asp:Label>
                    </div>

                    <div class="seccion-tarea">
                        <div class="tarea-header">
                            <div style="display: flex; align-items: center;">
                                <span style="font-size: 18px; font-weight: bold; color: #2c3e50;"> Permisos</span>
                                <span class="progreso">
                                    <asp:Label ID="lblProgreso" runat="server" Text="0%"></asp:Label>
                                </span>
                            </div>
                            <asp:Button ID="btnEliminarRol" runat="server" Text="🗑️ Eliminar Rol" CssClass="btn-eliminar" OnClick="btnEliminarRol_Click" />
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

                <asp:Panel ID="pnlNoSeleccionado" runat="server" Visible="true"> 
                    <div class="no-rol-seleccionado">
                        <p>👈 Seleccione un rol del panel izquierdo para ver sus detalles y permisos</p>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </form>
</body>
</html>