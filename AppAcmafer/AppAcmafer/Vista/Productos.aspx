<%@ Page Title="Productos" Language="C#" MasterPageFile="~/Vista/MasterPage.Master" AutoEventWireup="true" CodeBehind="Productos.aspx.cs" Inherits="AppAcmafer.Vista.Productos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="background: white; padding: 30px; border-radius: 10px; box-shadow: 0 2px 10px rgba(0,0,0,0.1);">
        
        
        <h2 style="color: #2c3e50; margin-bottom: 20px; border-bottom: 3px solid #3498db; padding-bottom: 10px;">
            📦 Catálogo de Productos - Fundición ACMAFER
        </h2>
        
        
        <p style="color: #7f8c8d; margin-bottom: 20px; line-height: 1.6;">
            Aquí puedes ver todos los productos disponibles en el inventario de la empresa de fundición.
        </p>
        
        
        <div style="margin-bottom: 25px; padding: 20px; background: linear-gradient(135deg, #000000 0%, #bfbfbf 100%); border-radius: 8px;">
            <div style="display: flex; gap: 15px; align-items: center; flex-wrap: wrap;">
                <div>
                    <asp:Label ID="lblFiltroCategoria" runat="server" Text="Categoría:" Font-Bold="true" ForeColor="White"></asp:Label>
                    <asp:DropDownList ID="ddlCategoria" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCategoria_SelectedIndexChanged"
                        style="margin-left: 10px; padding: 8px; border-radius: 5px; border: none; min-width: 200px;">
                    </asp:DropDownList>
                </div>
                
                <div>
                    <asp:Label ID="lblBuscar" runat="server" Text="Buscar:" Font-Bold="true" ForeColor="White"></asp:Label>
                    <asp:TextBox ID="txtBuscar" runat="server" placeholder="Nombre del producto..." 
                        style="margin-left: 10px; padding: 8px; border-radius: 5px; border: none; width: 250px;"></asp:TextBox>
                </div>
                
                <asp:Button ID="btnBuscar" runat="server" Text="🔍 Buscar" OnClick="btnBuscar_Click" 
                    style="padding: 8px 25px; background:  linear-gradient(135deg, #b8860b 0%, #d4af37 50%, #ffd700 100%); color: #1f1f1f; border: none; border-radius: 5px; cursor: pointer; font-weight: bold;" />
                
                <asp:Button ID="btnLimpiar" runat="server" Text="🔄 Limpiar" OnClick="btnLimpiar_Click" 
                    style="padding: 8px 25px; background: linear-gradient(135deg, #8b0000 0%, #ff0000 25%, #b30000 50%, #ff4d4d 75%, #660000 100%); color: #1f1f1f; border: none; border-radius: 5px; cursor: pointer; font-weight: bold;" />
            </div>
        </div>
        
        
        <asp:GridView ID="gvProductos" runat="server" 
                      AutoGenerateColumns="False" 
                      CellPadding="15"
                      GridLines="None"
                      Width="100%"
                      BackColor="White"
                      EmptyDataText="No hay productos disponibles en este momento"
                      OnRowCommand="gvProductos_RowCommand">
            
            <HeaderStyle BackColor="#4b4d4e" ForeColor="White" Font-Bold="true" Height="50px" Font-Size="14px" />
            <RowStyle BackColor="#ecf0f1" Height="45px" />
            <AlternatingRowStyle BackColor="White" />
            <EmptyDataRowStyle BackColor="#ffe6e6" ForeColor="#c0392b" Font-Bold="true" HorizontalAlign="Center" />
            
            <Columns>
                
                <asp:BoundField DataField="Codigo" HeaderText="Código" ItemStyle-Font-Bold="true" ItemStyle-ForeColor="#2c3e50" />
                
                
                <asp:BoundField DataField="Nombre" HeaderText="Producto" ItemStyle-Font-Size="14px" ItemStyle-Font-Bold="true" />
                
                
                <asp:TemplateField HeaderText="Descripción">
                    <ItemTemplate>
                        <div style="max-width: 300px; color: #555;">
                            <%# Eval("Descripcion") %>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                
                
                <asp:TemplateField HeaderText="Categoria" HeaderStyle-Width="180px" ItemStyle-Width="180px">
                    <ItemTemplate>
                        <span style="background: linear-gradient(135deg, #b8860b 0%, #d4af37 50%, #ffd700 100%); color: #1f1f1f; padding: 5px 12px; border-radius: 15px; font-size: 12px; font-weight: bold;">
                            <%# Eval("NombreCategoria") %>
                        </span>
                    </ItemTemplate>
                </asp:TemplateField>
                
                
                <asp:TemplateField HeaderText="Precio Unitario">
                    <ItemTemplate>
                        <div style="font-size: 16px; font-weight: bold; color: #000;">
                            <%# string.Format("{0:N0}", Eval("PrecioUnitario")) %>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                
                
                <asp:TemplateField HeaderText="Stock" HeaderStyle-Width="180px" ItemStyle-Width="180px">
                    <ItemTemplate>
                        <div style="text-align: center;">
                            <span style="background: <%# Convert.ToInt32(Eval("StockActual")) > 50 ? "#1f2937" : (Convert.ToInt32(Eval("StockActual")) > 20 ? "#f39c12" : "#e74c3c") %>; 
                                         color: white; padding: 4px 10px; border-radius: 10px; font-weight: bold; font-size: 14px;">
                                <%# Eval("StockActual") %> unidades
                            </span>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                
                
                <asp:TemplateField HeaderText="Estado" HeaderStyle-Width="150px" ItemStyle-Width="150px">
                    <ItemTemplate>
                        <div style="text-align: center;">
                           <span style="background: #1f2937; color: white; padding: 5px 12px; border-radius: 15px; font-size: 12px; font-weight: bold;">
                            ✓ <%# Eval("Estado") %>
                           </span>
                        </div>

                    </ItemTemplate>
                </asp:TemplateField>
                
                
                
                <asp:TemplateField HeaderText="Acciones">
                    <ItemTemplate>
                        <div style="text-align: center;">
                            <asp:Button ID="btnVerDetalle" runat="server" 
                                   Text="👁️ Ver Detalle" 
                                   CommandName="VerDetalle"
                                   CommandArgument='<%# Eval("IdProducto") %>' 
                                   style="padding: 4px 10px; background: #1f2937; color: white; border: none; border-radius: 5px; cursor: pointer; margin-right: 5px; font-weight: bold;" />
                        
                           <asp:Button ID="btnComprar" runat="server" 
                                   Text="🛒 Comprar" 
                                   CommandName="Comprar"
                                   CommandArgument='<%# Eval("IdProducto") %>' 
                                   style="padding: 4px 10px; background: linear-gradient(135deg, #b8860b 0%, #d4af37 50%, #ffd700 100%); color: #1f1f1f; border: none; border-radius: 5px; cursor: pointer; font-weight: bold;" />
                         </div>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        
        
        <asp:Panel ID="pnlDetalle" runat="server" Visible="false" 
            style="margin-top: 30px; padding: 25px; background: #fff3cd; border-left: 5px solid #ffc107; border-radius: 8px; box-shadow: 0 2px 8px rgba(0,0,0,0.1);">
            
            <h3 style="color: #f57c00; margin-bottom: 15px; display: flex; align-items: center; gap: 10px;">
                <span style="font-size: 24px;">ℹ️</span> Detalle del Producto
            </h3>
            
            <div style="display: grid; grid-template-columns: repeat(2, 1fr); gap: 15px; margin-bottom: 20px;">
                <div>
                    <strong style="color: #555;">Código:</strong>
                    <asp:Label ID="lblDetalleCodigo" runat="server" style="margin-left: 10px; color: #333;"></asp:Label>
                </div>
                <div>
                    <strong style="color: #555;">Categoría:</strong>
                    <asp:Label ID="lblDetalleCategoria" runat="server" style="margin-left: 10px; color: #333;"></asp:Label>
                </div>
                <div>
                    <strong style="color: #555;">Nombre:</strong>
                    <asp:Label ID="lblDetalleNombre" runat="server" style="margin-left: 10px; color: #333; font-weight: bold;"></asp:Label>
                </div>
                <div>
                    <strong style="color: #555;">Precio:</strong>
                    <asp:Label ID="lblDetallePrecio" runat="server" style="margin-left: 10px; color: #27ae60; font-weight: bold; font-size: 16px;"></asp:Label>
                </div>
            </div>
            
            <div style="margin-bottom: 15px;">
                <strong style="color: #555;">Descripción:</strong>
                <p style="margin-top: 8px; color: #555; line-height: 1.6;">
                    <asp:Label ID="lblDetalleDescripcion" runat="server"></asp:Label>
                </p>
            </div>
            
            <div style="margin-bottom: 15px;">
                <strong style="color: #555;">Stock Disponible:</strong>
                <asp:Label ID="lblDetalleStock" runat="server" style="margin-left: 10px; color: #333; font-weight: bold;"></asp:Label>
            </div>
            
            <asp:Button ID="btnCerrarDetalle" runat="server" Text="✖️ Cerrar" OnClick="btnCerrarDetalle_Click"
                style="padding: 10px 25px; background: #e74c3c; color: white; border: none; border-radius: 5px; cursor: pointer; font-weight: bold;" />
        </asp:Panel>
        
        
        <div style="margin-top: 40px; display: grid; grid-template-columns: repeat(auto-fit, minmax(200px, 1fr)); gap: 20px;">
            
            <div style="background: linear-gradient(135deg, #000000 0%, #777777 50%, #d4af37 100%); padding: 25px; border-radius: 10px; color: white; text-align: center; box-shadow: 0 4px 15px rgba(0,0,0,0.1);">
                <div style="font-size: 14px; opacity: 0.9; margin-bottom: 10px;">Total Productos</div>
                <div style="font-size: 36px; font-weight: bold;">
                    <asp:Label ID="lblTotalProductos" runat="server" Text="0"></asp:Label>
                </div>
            </div>
            
            <div style="background: linear-gradient(135deg, #000000 0%, #777777 50%, #d4af37 100%); padding: 25px; border-radius: 10px; color: white; text-align: center; box-shadow: 0 4px 15px rgba(0,0,0,0.1);">
                <div style="font-size: 14px; opacity: 0.9; margin-bottom: 10px;">Categorías</div>
                <div style="font-size: 36px; font-weight: bold;">
                    <asp:Label ID="lblTotalCategorias" runat="server" Text="0"></asp:Label>
                </div>
            </div>
            
            <div style="background: linear-gradient(135deg, #000000 0%, #777777 50%, #d4af37 100%); padding: 25px; border-radius: 10px; color: white; text-align: center; box-shadow: 0 4px 15px rgba(0,0,0,0.1);">
                <div style="font-size: 14px; opacity: 0.9; margin-bottom: 10px;">Stock Total</div>
                <div style="font-size: 36px; font-weight: bold;">
                    <asp:Label ID="lblStockTotal" runat="server" Text="0"></asp:Label>
                </div>
            </div>
            
            <div style="background: linear-gradient(135deg, #000000 0%, #777777 50%, #d4af37 100%); padding: 25px; border-radius: 10px; color: white; text-align: center; box-shadow: 0 4px 15px rgba(0,0,0,0.1);">
                <div style="font-size: 14px; opacity: 0.9; margin-bottom: 10px;">Valor Inventario</div>
                <div style="font-size: 36px; font-weight: bold;">
                    <asp:Label ID="lblValorTotal" runat="server" Text="$0"></asp:Label>
                </div>
            </div>
            
        </div>
        
        
        <asp:Label ID="lblMensaje" runat="server" Font-Bold="true" style="display: block; margin-top: 20px; padding: 15px; border-radius: 5px;"></asp:Label>
    </div>
</asp:Content>