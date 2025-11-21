<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListarUsuarios.aspx.cs" Inherits="AppAcmafer.Vista.ListarUsuarios" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title>Listado de Usuarios</title>
    <link href="../bootstrap/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container mt-5">
            <h2 class="mb-4 border-bottom pb-2">Listado de Usuarios Registrados</h2>
            <hr />

            <asp:Label ID="lblMensaje" runat="server" ForeColor="Red"></asp:Label>

            <asp:Repeater ID="rptUsuarios" runat="server">

                <HeaderTemplate>
                    <table class="table table-striped table-hover table-bordered shadow-sm">
                        <thead>
                            <tr>
                                <th>Documento</th>
                                <th>Nombre Completo</th>
                                <th>Correo</th>
                                <th>Rol</th>
                                <th>Estado</th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>

                <ItemTemplate>
                    <tr>
                        <td><strong><%# Eval("Documento") %></strong></td>
                        <td><%# Eval("NombreCompleto") %></td>
                        <td><%# Eval("Correo") %></td>
                        <td><%# Eval("Rol") %></td>

                        <td>
                            <asp:Label
                                runat="server"
                                Text='<%# Eval("Estado") %>'
                                CssClass='<%# Eval("Estado").ToString() == "Activo" ? "badge bg-success" : "badge bg-danger" %>' />
                        </td>



                    </tr>
                </ItemTemplate>

                <FooterTemplate>
                    </tbody>
                    </table>
               
                </FooterTemplate>

            </asp:Repeater>

        </div>
    </form>
</body>
</html>
