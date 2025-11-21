<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListadoProductos.aspx.cs" Inherits="AppAcmafer.Vista.ListadoProductos" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Productos por Categoría</title>
    
    <link rel="stylesheet" href="botstrap/css/bootstrap.min.css" />
    
</head>
<body>
    <form id="form1" runat="server">
        <div class="container mt-5"> 
            
            <h2 class="mb-4">Listado de Productos por Categoría</h2>
            <hr />

            <div class="form-group row align-items-center">
                <label class="col-sm-3 col-form-label">Filtrar por Categoría:</label>
                <div class="col-sm-4">
                    <asp:DropDownList ID="ddlCategoria" runat="server" 
                        AutoPostBack="True" 
                        OnSelectedIndexChanged="ddlCategoria_SelectedIndexChanged"
                        CssClass="form-control">
                    </asp:DropDownList>
                </div>
            </div>
            
            <asp:Repeater ID="rptProductos" runat="server">
                <HeaderTemplate>
                    <table class="table table-striped table-hover table-bordered">
                        <thead class="thead-dark">
                            <tr>
                                <th>ID Producto</th>
                                <th>Nombre</th>
                                <th>Categoría</th>
                                <th>Precio Unitario</th>
                                <th>Stock Actual</th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><%# Eval("idProducto") %></td>
                        <td><%# Eval("nombre") %></td>
                        <td><%# Eval("CategoriaNombre") %></td>
                        <td>$ <%# Eval("precioUnitario", "{0:N2}") %></td> <td><%# Eval("stockActual") %></td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                        </tbody>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
            
            <asp:Label ID="lblMensaje" runat="server" ForeColor="Red" CssClass="mt-3"></asp:Label>
            
        </div>
        
        <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
        <script src="https://cdn.jsdelivr.net/npm/popper.js@1.16.1/dist/umd/popper.min.js"></script>
        <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
    </form>
</body>
</html>