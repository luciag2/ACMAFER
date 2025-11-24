<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CrearPedido.aspx.cs" Inherits="AppAcmafer.Vista.CrearPedido" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
     <title>Crear Pedido</title>
    <style>
        body {
            font-family: 'Arial', sans-serif;
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            min-height: 100vh;
            padding: 20px;
        }
        .container {
            max-width: 800px;
            margin: 0 auto;
            background: white;
            padding: 30px;
            border-radius: 15px;
            box-shadow: 0 10px 30px rgba(0,0,0,0.3);
        }
        h2 {
            color: #333;
            text-align: center;
            margin-bottom: 30px;
        }
        .form-group {
            margin-bottom: 20px;
        }
        label {
            display: block;
            margin-bottom: 8px;
            color: #555;
            font-weight: bold;
        }
        input, select, textarea {
            width: 100%;
            padding: 12px;
            border: 2px solid #ddd;
            border-radius: 8px;
            font-size: 14px;
            box-sizing: border-box;
        }
        input:focus, select:focus, textarea:focus {
            border-color: #667eea;
            outline: none;
        }
        .btn {
            width: 100%;
            padding: 15px;
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            color: white;
            border: none;
            border-radius: 8px;
            font-size: 16px;
            font-weight: bold;
            cursor: pointer;
            transition: all 0.3s;
        }
        .btn:hover {
            transform: translateY(-2px);
            box-shadow: 0 5px 15px rgba(102, 126, 234, 0.4);
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
        .info-stock {
            background: #e7f3ff;
            padding: 15px;
            border-radius: 8px;
            margin-top: 10px;
            display: none;
        }
        .info-stock.show {
            display: block;
        }
        table {
            width: 100%;
            border-collapse: collapse;
            margin-top: 30px;
        }
        table th, table td {
            padding: 12px;
            text-align: left;
            border-bottom: 1px solid #ddd;
        }
        table th {
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            color: white;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2>🛒 Crear Nuevo Pedido</h2>
            
            <asp:Panel ID="pnlMensaje" runat="server" Visible="false" CssClass="mensaje">
                <asp:Label ID="lblMensaje" runat="server"></asp:Label>
            </asp:Panel>
            
            <div class="form-group">
                <label>Número de Pedido:</label>
                <asp:TextBox ID="txtNumeroPedido" runat="server" placeholder="Ej: PED-001"></asp:TextBox>
            </div>
            
            <div class="form-group">
                <label>Cliente:</label>
                <asp:DropDownList ID="ddlClientes" runat="server"></asp:DropDownList>
            </div>
            
            <div class="form-group">
                <label>Producto:</label>
                <asp:DropDownList ID="ddlProductos" runat="server" AutoPostBack="true"
                    OnSelectedIndexChanged="ddlProductos_SelectedIndexChanged"></asp:DropDownList>
            </div>
            
            <asp:Panel ID="pnlInfoStock" runat="server" CssClass="info-stock">
                <strong>📦 Stock Disponible: </strong>
                <asp:Label ID="lblStockDisponible" runat="server" ForeColor="Green"></asp:Label>
            </asp:Panel>
            
            <div class="form-group">
                <label>Cantidad:</label>
                <asp:TextBox ID="txtCantidad" runat="server" TextMode="Number" placeholder="0"></asp:TextBox>
            </div>
            
            <div class="form-group">
                <label>Observaciones:</label>
                <asp:TextBox ID="txtObservaciones" runat="server" TextMode="MultiLine" 
                    Rows="3" placeholder="Observaciones del pedido..."></asp:TextBox>
            </div>
            
            <asp:Button ID="btnCrearPedido" runat="server" Text="Crear Pedido" 
                CssClass="btn" OnClick="btnCrearPedido_Click" />
            
            <h3 style="margin-top: 40px; color: #333;">📋 Pedidos Recientes</h3>
            <asp:GridView ID="gvPedidos" runat="server" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField DataField="numeroPedido" HeaderText="N° Pedido" />
                    <asp:BoundField DataField="fechaPedido" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField DataField="Cliente" HeaderText="Cliente" />
                    <asp:BoundField DataField="estado" HeaderText="Estado" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>