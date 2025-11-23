<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ActualizarProducto.aspx.cs" Inherits="AppAcmafer.Vista.ActualizarProducto" %>

<!DOCTYPE html>

<html>
<head>
    <title>Actualizar Producto </title>
    <style>
        
        * {
            margin: 0;
            padding: 0;
            box-sizing: border-box;
        }
        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            
            background: #f4f6f9; 
            min-height: 100vh;
            padding: 20px;
        }
        .container {
            max-width: 900px;
            margin: 0 auto;
            background: white;
            padding: 30px;
            border-radius: 15px;
            box-shadow: 0 5px 20px rgba(0,0,0,0.1);
        }
        h2 {
            
            color: #2c3e50; 
            margin-bottom: 30px;
            text-align: center;
            font-size: 28px;
            
            padding-bottom: 10px;
            border-bottom: 3px solid #f39c12; 
        }
        .form-grid {
            display: grid;
            grid-template-columns: 1fr 1fr;
            gap: 20px;
            margin-bottom: 20px;
        }
        .form-group {
            margin-bottom: 20px;
        }
        .form-group.full-width {
            grid-column: 1 / -1;
        }
        label {
            display: block;
            margin-bottom: 8px;
            color: #555;
            font-weight: 600;
        }
        input[type="text"], input[type="number"], select, textarea {
            width: 100%;
            padding: 12px;
            border: 2px solid #ddd;
            border-radius: 8px;
            font-size: 14px;
            transition: all 0.3s;
        }
        input:focus, select:focus, textarea:focus {
            
            border-color: #f39c12; 
            outline: none;
            box-shadow: 0 0 0 3px rgba(243, 156, 18, 0.2);
        }
        textarea {
            resize: vertical;
            min-height: 80px;
        }
        .btn {
            padding: 14px 30px;
            border: none;
            border-radius: 8px;
            font-size: 16px;
            font-weight: bold;
            cursor: pointer;
            transition: all 0.3s;
            margin-right: 10px;
        }
        .btn-primary {
            
            background: #f39c12;
            color: white;
        }
        .btn-primary:hover {
            transform: translateY(-2px);
            background: #e67e22; 
            box-shadow: 0 5px 15px rgba(243, 156, 18, 0.4);
        }
        .btn-secondary {
            
            background: #7f8c8d;
            color: white;
        }
        .btn-secondary:hover {
            background: #6c7a89;
        }
        .mensaje {
            padding: 15px;
            margin-bottom: 20px;
            border-radius: 8px;
            text-align: center;
            font-weight: bold;
        }
        .exito {
            background-color: #d4edda;
            color: #155724;
            border: 2px solid #c3e6cb;
        }
        .error {
            background-color: #f8d7da;
            color: #721c24;
            border: 2px solid #f5c6cb;
        }
        .historial {
            margin-top: 40px;
            padding-top: 30px;
            
            border-top: 3px solid #f39c12;
        }
        .historial h3 {
            color: #333;
            margin-bottom: 20px;
        }
        table {
            width: 100%;
            border-collapse: collapse;
            margin-top: 15px;
        }
        table th, table td {
            padding: 12px;
            text-align: left;
            border-bottom: 1px solid #ddd;
        }
        table th {
            
            background: #34495e; 
            color: white;
            font-weight: bold;
        }
        table tr:hover {
            background-color: #f5f5f5;
        }
        .buttons-container {
            display: flex;
            gap: 10px;
            margin-top: 30px;
        }
        
        #ddlProductos {
            border-color: #ccc;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2>Acualizar Producto</h2>
            
            <asp:Panel ID="pnlMensaje" runat="server" Visible="false" CssClass="mensaje">
                <asp:Label ID="lblMensaje" runat="server"></asp:Label>
            </asp:Panel>
            
            <div class="form-group">
                <label>Seleccionar Producto:</label>
                <asp:DropDownList ID="ddlProductos" runat="server" AutoPostBack="true" 
                    OnSelectedIndexChanged="ddlProductos_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
            
            <asp:Panel ID="pnlFormulario" runat="server" Visible="false">
                <div class="form-grid">
                    <div class="form-group">
                        <label>Nombre:</label>
                        <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox>
                    </div>
                    
                    <div class="form-group">
                        <label>Código:</label>
                        <asp:TextBox ID="txtCodigo" runat="server"></asp:TextBox>
                    </div>
                    
                    <div class="form-group">
                        <label>Stock Actual:</label>
                        <asp:TextBox ID="txtStock" runat="server" TextMode="Number"></asp:TextBox>
                    </div>
                    
                    <div class="form-group">
                        <label>Precio Unitario:</label>
                        <asp:TextBox ID="txtPrecio" runat="server"></asp:TextBox>
                    </div>
                    
                    <div class="form-group">
                        <label>Categoría:</label>
                        <asp:DropDownList ID="ddlCategorias" runat="server"></asp:DropDownList>
                    </div>
                    
                    <div class="form-group full-width">
                        <label>Descripción:</label>
                        <asp:TextBox ID="txtDescripcion" runat="server" TextMode="MultiLine"></asp:TextBox>
                    </div>
                </div>
                
                <div class="buttons-container">
                    <asp:Button ID="btnActualizar" runat="server" Text="Actualizar Producto" 
                        CssClass="btn btn-primary" OnClick="btnActualizar_Click" />
                    <asp:Button ID="btnCancelar" runat="server" Text="❌ Cancelar" 
                        CssClass="btn btn-secondary" OnClick="btnCancelar_Click" />
                </div>
                
                <div class="historial">
                    <h3> Historial de Modificaciones</h3>
                    <asp:GridView ID="gvHistorial" runat="server" AutoGenerateColumns="true">
                    </asp:GridView>
                </div>
            </asp:Panel>
        </div>
    </form>
</body>
</html>