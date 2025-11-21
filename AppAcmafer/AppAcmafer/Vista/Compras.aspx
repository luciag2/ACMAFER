<%@ Page Title="Compras" Language="C#" MasterPageFile="~/Vista/MasterPage.Master" AutoEventWireup="true" CodeBehind="Compras.aspx.cs" Inherits="AppAcmafer.Vista.Compras" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="background: white; padding: 30px; border-radius: 10px; box-shadow: 0 2px 10px rgba(0,0,0,0.1);">
        <h2 style="color: #2c3e50; margin-bottom: 20px; border-bottom: 3px solid #3498db; padding-bottom: 10px;">
            🛒 Historial de Compras
        </h2>

        <div id="PanelFormularioCompra" runat="server" visible="false" class="card" style="margin-bottom: 20px; padding: 20px; background-color: #f8f9fa; border: 1px solid #ddd;">
    <h3>🛒 Detalle de Producto para Compra</h3>
    <hr />
    <div class="row">
        <div class="col-md-6">
            <p><strong>Producto:</strong> <asp:Label ID="lblNombreProducto" runat="server"></asp:Label></p>
            <p><strong>Precio Unitario:</strong> <asp:Label ID="lblPrecioUnitario" runat="server"></asp:Label></p>
        </div>
        <div class="col-md-6">
            <div class="form-group">
                <label for="txtCantidad">Cantidad a Comprar:</label>
                <asp:TextBox ID="txtCantidad" runat="server" Text="1" CssClass="form-control" TextMode="Number"></asp:TextBox>
            </div>
            
            <div style="margin-top: 15px;">
                <asp:Button ID="btnConfirmarCompra" runat="server" Text="Confirmar Compra" OnClick="btnConfirmarCompra_Click" CssClass="btn btn-success" />
                
                <asp:Label ID="lblMensajeCompra" runat="server" ForeColor="Red" style="display: block; margin-top: 10px;"></asp:Label>
            </div>
        </div>
    </div>
    
    <asp:HiddenField ID="hfIdProducto" runat="server" />
    <asp:HiddenField ID="hfPrecioOriginal" runat="server" />
</div>
        
        <!-- Filtros -->
        <div style="margin-bottom: 20px; padding: 15px; background: #ecf0f1; border-radius: 5px;">
            <asp:Label ID="lblFiltro" runat="server" Text="Filtrar por Producto:" Font-Bold="true" style="margin-right: 10px;"></asp:Label>
            <asp:TextBox ID="txtFiltro" runat="server" placeholder="Buscar producto..." style="padding: 8px; width: 250px; border: 1px solid #bdc3c7; border-radius: 5px;"></asp:TextBox>
            <asp:Button ID="btnBuscar" runat="server" Text="🔍 Buscar" OnClick="btnBuscar_Click" 
                style="padding: 8px 20px; background: #3498db; color: white; border: none; border-radius: 5px; cursor: pointer; margin-left: 10px;" />
            <asp:Button ID="btnMostrarTodas" runat="server" Text="📋 Mostrar Todas" OnClick="btnMostrarTodas_Click" 
                style="padding: 8px 20px; background: #95a5a6; color: white; border: none; border-radius: 5px; cursor: pointer; margin-left: 10px;" />
        </div>
        
        <!-- GridView de Compras -->
        <asp:GridView ID="gvCompras" runat="server" 
                      AutoGenerateColumns="False" 
                      CellPadding="12"
                      GridLines="None"
                      Width="100%"
                      BackColor="White"
                      EmptyDataText="No hay compras registradas">
            
            <HeaderStyle BackColor="#2c3e50" ForeColor="White" Font-Bold="true" Height="45px" />
            <RowStyle BackColor="#ecf0f1" Height="40px" />
            <AlternatingRowStyle BackColor="White" />
            
            <Columns>
                <asp:BoundField DataField="IdCompra" HeaderText="ID" />
                <asp:BoundField DataField="NumeroPedido" HeaderText="# Pedido" />
                <asp:BoundField DataField="NombreProducto" HeaderText="Producto" />
                <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                <asp:BoundField DataField="ValorTotal" HeaderText="Valor Total" DataFormatString="{0:N0}" />
                <asp:BoundField DataField="Descuento" HeaderText="Descuento" DataFormatString="{0:N0}" />
                
                <asp:TemplateField HeaderText="Total Final">
                    <ItemTemplate>
                        <strong style="color: #27ae60;">
                            <%# string.Format("{0:N0}", Convert.ToDecimal(Eval("ValorTotal")) - Convert.ToDecimal(Eval("Descuento"))) %>
                        </strong>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Acciones">
                    <ItemTemplate>
                        <asp:Button ID="btnVerDetalle" runat="server" 
                                   Text="👁️ Ver" 
                                   CommandArgument='<%# Eval("IdCompra") %>' 
                                   OnClick="btnVerDetalle_Click"
                                   style="padding: 5px 15px; background: #3498db; color: white; border: none; border-radius: 5px; cursor: pointer;" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        
        <!-- Resumen estadístico -->
        <div style="margin-top: 30px; padding: 20px; background: linear-gradient(135deg, #667eea 0%, #764ba2 100%); border-radius: 10px; color: white;">
            <h3 style="margin-bottom: 15px;">📊 Resumen</h3>
            <div style="display: flex; justify-content: space-around; text-align: center;">
                <div>
                    <div style="font-size: 14px; opacity: 0.9;">Total Compras</div>
                    <div style="font-size: 28px; font-weight: bold;">
                        <asp:Label ID="lblTotalCompras" runat="server" Text="0"></asp:Label>
                    </div>
                </div>
                <div>
                    <div style="font-size: 14px; opacity: 0.9;">Valor Total</div>
                    <div style="font-size: 28px; font-weight: bold;">
                        <asp:Label ID="lblValorTotal" runat="server" Text="$0"></asp:Label>
                    </div>
                </div>
                <div>
                    <div style="font-size: 14px; opacity: 0.9;">Descuentos</div>
                    <div style="font-size: 28px; font-weight: bold;">
                        <asp:Label ID="lblTotalDescuentos" runat="server" Text="$0"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
        
        <!-- Mensaje -->
        <asp:Label ID="lblMensaje" runat="server" ForeColor="Red" Font-Bold="true" style="display: block; margin-top: 15px;"></asp:Label>
    </div>
</asp:Content>