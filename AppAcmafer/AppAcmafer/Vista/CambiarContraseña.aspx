<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CambiarContrasena.aspx.cs" Inherits="AppAcmafer.Vista.CambiarContrasena" %>

<!DOCTYPE html>
<html>
<head>
    <title>Cambiar Contraseña</title>
    <style>
       
        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            
            background-color: #f4f6f9; 
            height: 100vh;
            display: flex;
            justify-content: center;
            align-items: center;
            margin: 0;
        }
        .container {
            background: white;
            padding: 40px;
            border-radius: 10px;
            
            box-shadow: 0 8px 20px rgba(0,0,0,0.1);
            width: 400px;
            border-top: 5px solid #f39c12; 
        }
        h2 {
            text-align: center;
            /* Color de texto principal (gris oscuro) */
            color: #2c3e50; 
            margin-bottom: 30px;
            font-size: 26px;
        }
        .form-group {
            margin-bottom: 20px;
        }
        label {
            display: block;
            margin-bottom: 5px;
            color: #555;
            font-weight: 600;
        }
        input[type="text"], input[type="password"], input[type="number"] {
            width: 100%;
            padding: 12px;
            border: 1px solid #ced4da; /* Borde más sutil */
            border-radius: 5px;
            font-size: 14px;
            box-sizing: border-box;
            transition: all 0.3s;
        }
        input:focus {
            /* Foco en color naranja/ámbar */
            border-color: #f39c12; 
            outline: none;
            box-shadow: 0 0 0 3px rgba(243, 156, 18, 0.2);
        }
        .btn {
            width: 100%;
            padding: 14px;
            /* Color de fondo naranja/ámbar (color de acento principal) */
            background-color: #f39c12; 
            color: white;
            border: none;
            border-radius: 5px;
            font-size: 16px;
            cursor: pointer;
            font-weight: bold;
            transition: background-color 0.3s, transform 0.2s;
        }
        .btn:hover {
            /* Tono más oscuro al pasar el ratón */
            background-color: #e67e22;
            transform: translateY(-1px);
        }
        .mensaje {
            padding: 12px;
            margin-bottom: 20px;
            border-radius: 5px;
            text-align: center;
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
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2> 🔒 Cambiar Contraseña</h2>
            
            <asp:Panel ID="pnlMensaje" runat="server" Visible="false" CssClass="mensaje">
                <asp:Label ID="lblMensaje" runat="server"></asp:Label>
            </asp:Panel>
            
            <div class="form-group">
                <label>ID de Usuario:</label>
                <asp:TextBox ID="txtIdUsuario" runat="server" TextMode="Number"></asp:TextBox>
            </div>
            
            <div class="form-group">
                <label>Contraseña Actual:</label>
                <asp:TextBox ID="txtClaveActual" runat="server" TextMode="Password"></asp:TextBox>
            </div>
            
            <div class="form-group">
                <label>Nueva Contraseña:</label>
                <asp:TextBox ID="txtClaveNueva" runat="server" TextMode="Password"></asp:TextBox>
            </div>
            
            <div class="form-group">
                <label>Confirmar Nueva Contraseña:</label>
                <asp:TextBox ID="txtClaveConfirmar" runat="server" TextMode="Password"></asp:TextBox>
            </div>
            
            <asp:Button ID="btnCambiar" runat="server" Text="Actualizar Contraseña" 
                CssClass="btn" OnClick="btnCambiar_Click" />
        </div>
    </form>
</body>
</html>