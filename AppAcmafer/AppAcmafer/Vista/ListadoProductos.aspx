<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListadoProductos.aspx.cs" Inherits="AppAcmafer.Vista.ListadoProductos" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
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

            <asp:Repeater ID="rptProductos" runat="server" OnItemCommand="RptProductos_ItemCommand">
                <HeaderTemplate>
                    <table class="table table-striped table-hover table-bordered">
                        <thead class="thead-dark">
                            <tr>
                                <th>ID Producto</th>
                                <th>Nombre</th>
                                <th>Categoría</th>
                                <th>Precio Unitario</th>
                                <th>Stock Actual</th>
                                <td>
                                    <asp:LinkButton
                                        ID="btnComprar"
                                        runat="server"
                                        CssClass="btn btn-sm btn-primary"
                                        Text="Comprar"
                                        CommandName="ComprarProducto"
                                        CommandArgument='<%# Eval("IdProducto") %>' />
                                </td>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><%# Eval("IdProducto") %></td>
                        <td><%# Eval("nombre") %></td>
                        <td><%# Eval("CategoriaNombre") %></td>
                        <td><%# Eval("precioUnitario", "{0:N2}") %></td>
                        <td><%# Eval("stockActual") %></td>

                        <td>
                            <asp:LinkButton
                                ID="btnComprar"
                                runat="server"
                                CssClass="btn btn-sm btn-primary"
                                Text="Comprar"
                                CommandName="Comprar"
                                CommandArgument='<%# Eval("IdProducto") %>' />
                        </td>
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

        <asp:UpdatePanel ID="upModal" runat="server">
            <ContentTemplate>
                <div class="modal fade" id="ModalCompra" tabindex="-1" role="dialog" aria-hidden="true">
                    <div class="modal-dialog" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title">🛒 Agregar al Carrito:
                                    <asp:Label ID="lblModalProductoNombre" runat="server" /></h5>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            </div>
                            <div class="modal-body">
                                <div class="form-group">
                                    <label>Precio Unitario:</label>
                                    <asp:Label ID="lblModalPrecio" runat="server" CssClass="form-control-plaintext" />
                                </div>
                                <div class="form-group">
                                    <label>Cantidad Requerida:</label>
                                    <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control" TextMode="Number" Text="1" min="1" />
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtCantidad" ErrorMessage="Cantidad obligatoria." ForeColor="Red" />
                                </div>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                                <asp:Button ID="btnAgregarItem" runat="server" Text="Añadir al Carrito"
                                    CssClass="btn btn-success" OnClick="btnAgregarItem_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        º
    </form>
</body>
</html>
