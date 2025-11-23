<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AsignarRol.aspx.cs" Inherits="AppAcmafer.Vista.AsignarRol" %>

<!DOCTYPE html>

<html>
<head>
    <title>Asignar Rol a Usuario</title>
    <style>
        /* CSS Modificado para usar colores similares a la imagen de referencia (Naranja/Gris Oscuro) */
        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            margin: 0;
            padding: 20px;
            /* Fondo muy claro, parecido al de la aplicación */
            background-color: #f4f6f9;
        }
        .container {
            max-width: 800px;
            margin: 40px auto;
            background: white;
            padding: 30px;
            border-radius: 10px;
            box-shadow: 0 4px 20px rgba(0,0,0,0.1);
        }
        h2 {
            /* Título en color azul oscuro/gris */
            color: #2c3e50;
            text-align: center;
            margin-bottom: 25px;
            padding-bottom: 10px;
            /* Línea divisoria en color naranja/ámbar */
            border-bottom: 3px solid #f39c12;
        }
        h3 {
            color: #343a40;
            margin-top: 25px;
            margin-bottom: 15px;
        }
        .form-group {
            margin-bottom: 20px;
        }
        label {
            display: block;
            font-weight: 600;
            color: #495057;
            margin-bottom: 8px;
        }
        select {
            padding: 12px;
            width: 100%;
            border: 1px solid #ced4da;
            border-radius: 6px;
            font-size: 16px;
            transition: border-color 0.3s;
        }
        select:focus {
            /* Foco en color naranja/ámbar */
            border-color: #f39c12; 
            outline: none;
            box-shadow: 0 0 0 0.2rem rgba(243, 156, 18, 0.25);
        }
        .btn {
            padding: 14px 20px; /* Un poco más de padding para que se vea como botón principal */
            width: 100%;
            /* Botón principal en color naranja/ámbar */
            background-color: #f39c12;
            color: white;
            cursor: pointer;
            border: none;
            border-radius: 6px;
            font-size: 17px;
            font-weight: bold;
            transition: background-color 0.3s, transform 0.3s;
        }
        .btn:hover {
            /* Tono más oscuro al pasar el ratón */
            background-color: #e67e22;
            transform: translateY(-1px);
        }
        .mensaje {
            padding: 15px;
            margin: 20px 0;
            border-radius: 6px;
            font-weight: 600;
        }
        .exito {
            background-color: #d4edda;
            color: #155724;
            border: 1px solid #c3e6cb;
        }
        .error {
            background-color: #f8d7da;
            color: #721c24;
            border: 1px solid #f5c6cb;
        }
        hr {
            border: 0;
            /* Separador en color azul oscuro */
            border-top: 2px solid #34495e;
        }
        table {
            width: 100%;
            border-collapse: collapse;
            margin-top: 15px;
            border: 1px solid #dee2e6;
        }
        table th, table td {
            padding: 12px;
            text-align: left;
            border-bottom: 1px solid #dee2e6;
        }
        table th {
            /* Cabecera de tabla en color azul oscuro/gris */
            background-color: #34495e;
            color: white;
            font-weight: 600;
            border-bottom: 2px solid #2c3e50;
        }
        table tr:nth-child(even) {
            background-color: #f9f9f9;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2> Asignar Rol a Usuario</h2>
            
            <div class="form-group">
                <label>Seleccionar Usuario:</label>
                <asp:DropDownList ID="ddlUsuarios" runat="server" AutoPostBack="true" 
                    OnSelectedIndexChanged="ddlUsuarios_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
            
            <div class="form-group">
                <label>Seleccionar Rol:</label>
                <asp:DropDownList ID="ddlRoles" runat="server">
                </asp:DropDownList>
            </div>
            
            <asp:Button ID="btnAsignar" runat="server" Text="Asignar Rol" 
                CssClass="btn" OnClick="btnAsignar_Click" />
            
            <asp:Panel ID="pnlMensaje" runat="server" Visible="false" CssClass="mensaje">
                <asp:Label ID="lblMensaje" runat="server"></asp:Label>
            </asp:Panel>
            
            <hr style="margin: 30px 0;" />
            
            <h3>Roles actuales del usuario seleccionado</h3>
            <asp:GridView ID="gvRolesUsuario" runat="server" AutoGenerateColumns="false" 
                CssClass="table">
                <Columns>
                    <asp:BoundField DataField="rol" HeaderText="Rol" />
                    <asp:BoundField DataField="estado" HeaderText="Estado" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>