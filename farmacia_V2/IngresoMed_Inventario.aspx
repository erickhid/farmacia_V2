<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="IngresoMed_Inventario.aspx.vb" Inherits="IngresoMed_Inventario" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
        <div style="width: 770px; border: solid 1px #5d7b9d; text-align:center;">
            <table id="tbl_ingresomed_inventario" border="0" cellpadding="2" cellspacing="1" style="width: 770px">
                <tr style="width:770px;">
                    <th colspan="16" class="theader">INGRESO INVENTARIO ARV'S</th>
                </tr>
                <tr>
                    
                         <td style="width: 50px; background-color: #5d7b9d; color: #ffffff;">
                    MES:
                </td>

                <td style="width: 100px; background-color: #e9ecf1;">
                    <asp:DropDownList ID="DDL_mes" runat="server" CssClass="datos">
                        <asp:ListItem Value="1">Enero</asp:ListItem>
                        <asp:ListItem Value="2">Febrero</asp:ListItem>
                        <asp:ListItem Value="3">Marzo</asp:ListItem>
                        <asp:ListItem Value="4">Abril</asp:ListItem>
                        <asp:ListItem Value="5">Mayo</asp:ListItem>
                        <asp:ListItem Value="6">Junio</asp:ListItem>
                        <asp:ListItem Value="7">Julio</asp:ListItem>
                        <asp:ListItem Value="8">Agosto</asp:ListItem>
                        <asp:ListItem Value="9">Septiembre</asp:ListItem>
                        <asp:ListItem Value="10">Octubre</asp:ListItem>
                        <asp:ListItem Value="11">Noviembre</asp:ListItem>
                        <asp:ListItem Value="12">Diciembre</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="width: 50px; background-color: #5d7b9d; color: #ffffff;">
                    AÑO:
                </td>
                <td style="width: 100px; background-color: #e9ecf1;">
                    <asp:DropDownList ID="DDL_anio" runat="server" CssClass="datos">
                    </asp:DropDownList>
                </td>
                    <td style="text-align:center;">
                    <asp:Button ID="btn_generar" runat="server" Text="GENERAR" TabIndex="45" CssClass="button" />
                </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;<asp:Label ID="lbl_error" runat="server" CssClass="error"></asp:Label>
                    </td>
                </tr>
<%--<tr>
                        <td style="background-color: #e9ecf1; text-align:center; width:82px;" colspan="2">
                            Código: 
                        </td>
                        <td style="background-color: #e9ecf1; text-align:center; width:125px;" colspan="2">
                            Descripción: 
                        </td>
                        <td  style="background-color: #e9ecf1; text-align:center; width:75px;" colspan="2">
                            Cantidad: 
                        </td>
                      
                        <td style="background-color: #e9ecf1; text-align:center; width:82px;" colspan="2">
                            Código: 
                        </td>
                        <td style="background-color: #e9ecf1; text-align:center; width:125px;" colspan="2">
                            Descripción: 
                        </td>
                        <td  style="background-color: #e9ecf1; text-align:center; width:75px;" colspan="2">
                            Cantidad: 
                        </td>
                    </tr>
                    <tr>
                            <td colspan="1" style="text-align:start; width: 25px;">
                                <span class="numeracion">#1</span>
                            </td>
                        <td colspan="1" style="text-align:start;">
                                <asp:DropDownList ID="DDL_cod1" runat="server" CssClass="datos" Width="60" AutoPostBack="True">
                                </asp:DropDownList>
                        </td>
                        <td style="text-align:center; width:125px;" colspan="2">
                            <asp:Label ID="lbl_descripcion1" runat="server" CssClass="datos2"></asp:Label>
                        </td>
                        <td style="text-align:center;" colspan="2">
                             <asp:TextBox ID="txt_cant1" runat="server" CssClass="datosN" MaxLength="3" Width="25px"></asp:TextBox>
                        </td>

                            <td colspan="1" style="text-align:start; width: 25px;">
                                <span class="numeracion">#2</span>
                            </td>
                        <td colspan="1" style="text-align:start;">
                                <asp:DropDownList ID="DDL_cod2" runat="server" CssClass="datos" Width="60" AutoPostBack="True">
                                </asp:DropDownList>
                        </td>
                        <td style="text-align:center; width:125px;" colspan="2">
                            <asp:Label ID="lbl_descripcion2" runat="server" CssClass="datos2"></asp:Label>
                        </td>
                        <td style="text-align:center;">
                             <asp:TextBox ID="txt_cant2" runat="server" CssClass="datosN" MaxLength="3" Width="25px"></asp:TextBox>
                        </td>
                    </tr>


                    </table>
                    </div>--%>
                </table>
            </div>
        <div>
            <asp:Panel ID="pnl_IngresoARV1" runat="server">
                <table id="tbl_IARV" border="0" cellpadding="0" cellspacing="1" style="width:770px;">
                    <tr>
                        <td>
                            <table id="Table8" border="0" cellpadding="0" cellspacing="0" style="width:768px;">
                                <tr>
                                    <th class="theader" style="width:20px;"><asp:ImageButton ID="ib_IngresoARV" ImageUrl="~/images/plus2.png" runat="server" BorderWidth="0"  Height="10px" Width="10px"/></th>
                                    <th class="theader4">INGRESO ARV</th>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
            <div>
            <asp:Panel ID="pnl_IngresoARV" runat="server">
                <asp:UpdatePanel ID="up_pnl_IngresoARV" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table id="tblpnl_IngresoARV" border="0" cellpadding="0" cellspacing="0" style="width:770px;">
                            <tr>
                                <td>
                                    <asp:GridView ID="GV_pnl_IngresoARV" runat="server" ForeColor="#333333"
                                        EmptyDataText="No se existe ningun ingreso del día de hoy." 
                                        Font-Names="Trebuchet MS" Font-Size="8pt" GridLines="None"
                                        CellPadding="0" CellSpacing="1" Width="770px" AutoGenerateColumns="False" 
                                        ShowFooter="False" DataKeyNames="IdIngresoMed" >
                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" Wrap="False" />
                                        <Columns>
                                            <asp:TemplateField ShowHeader="False" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_IdIngresoMed" runat="server" Text='<%# Bind("IdIngresoMed")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Fecha Ingreso">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_fechaingreso" runat="server" Text='<%# Bind("FechaIngreso")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="130px" CssClass="GV_rowpad" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Código">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Nombre" runat="server" Text='<%# Bind("Codigo")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="90px" CssClass="GV_rowpad" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Medicamento">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_NomTipoRelacion" runat="server" Text='<%# Bind("Medicamento")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="120px" CssClass="GV_rowpad" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cantidad">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_cantidad" runat="server" Text='<%# Bind("Cantidad_Ingreso")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="35px" CssClass="GV_rowpad" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="No. Requisición">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_no_requisicion" runat="server" Text='<%# Bind("No_Requisicion")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="35px" CssClass="GV_rowpad" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="No. Lote">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_no_lote" runat="server" Text='<%# Bind("No_Lote")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="35px" CssClass="GV_rowpad" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Fecha Vencimiento">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_fecha_vencionmiento" runat="server" Text='<%# Bind("Fecha_Vencimiento")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="35px" CssClass="GV_rowpad" />
                                            </asp:TemplateField>
                                            <%--<asp:TemplateField HeaderText="Nivel Educativo">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_NivelEducativo" runat="server" Text='<%# Bind("NivelEducativo")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="158px" CssClass="GV_rowpad" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Situación Laboral">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_SituacionLaboral" runat="server" Text='<%# Bind("SituacionLaboral")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="150px" CssClass="GV_rowpad" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Ingreso">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Ingreso" runat="server" Text='<%# Bind("Ingreso")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="55px" CssClass="GV_rowpad" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Conoce Dx">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_ConoceDx" runat="server" Text='<%# Bind("NomConoceDx")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="75px" CssClass="GV_rowpad" />
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField ShowHeader="False">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ibtEditar" runat="server" ImageUrl="~/images/file_edit.png" CommandName="Editar" ToolTip="Editar" />
                                                    <asp:ImageButton ID="ibtBorrar" runat="server" ImageUrl="~/images/delete.png" CommandName="Borrar" ToolTip="Borrar" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="right" Width="41px" CssClass="GV_rowpad" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataRowStyle Font-Bold="True" />
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                        <EditRowStyle BackColor="#999999" />
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                        <EmptyDataTemplate>
                                            <table border="0" cellspacing="1" cellpadding="0" width="770px">
                                                <tr>
                                                    <td colspan="8" style="font-weight:bold; color:#333333; text-align:Left;">No existen ingresos de hoy.</td>
                                                </tr>
                                                <tr>
                                                    <td style="background-color:#5D7B9D; font-weight:bold; color:#ffffff; text-align:center; width:100px;">Fecha Ingreso</td>
                                                    <td style="background-color:#5D7B9D; font-weight:bold; color:#ffffff; text-align:center; width:80px;">Código</td>
                                                    <td style="background-color:#5D7B9D; font-weight:bold; color:#ffffff; text-align:center; width:90px;">Nombre</td>
                                                    <td style="background-color:#5D7B9D; font-weight:bold; color:#ffffff; text-align:center; width:35px;">Cantidad</td>
                                                    <td style="background-color:#5D7B9D; font-weight:bold; color:#ffffff; text-align:center; width:35px;">No. Requisición</td>
                                                    <td style="background-color:#5D7B9D; font-weight:bold; color:#ffffff; text-align:center; width:35px;">No. Lote</td>
                                                    <td style="background-color:#5D7B9D; font-weight:bold; color:#ffffff; text-align:center; width:35px;">Fecha Vencimiento</td>
<%--                                                    <td style="background-color:#5D7B9D; font-weight:bold; color:#ffffff; text-align:center; width:162px;">Nivel Educativo</td>
                                                    <td style="background-color:#5D7B9D; font-weight:bold; color:#ffffff; text-align:center; width:150px;">Situación Laboral</td>
                                                    <td style="background-color:#5D7B9D; font-weight:bold; color:#ffffff; text-align:center; width:55px;">Ingreso</td>
                                                    <td style="background-color:#5D7B9D; font-weight:bold; color:#ffffff; text-align:center; width:75px;">Conoce Dx</td>--%>
                                                    <td style="background-color:#5D7B9D; font-weight:bold; color:#ffffff; text-align:center; width:41px;"></td>
                                                </tr>
                                            </table> 
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td style="border-top:2px solid #5d7b9d; border-bottom:2px solid #5d7b9d; background-color:#e9ecf1;">
                                    <div id="divingresoGF" runat="server" visible="true">
                                        <table border="0" cellspacing="1" cellpadding="0" width="770px">
                                            <tr>
                                                <td style="text-align:center; width:216px;" class="GV_rowpad">
<%--                                                    <asp:TextBox ID="txt_fechaingreso" runat="server" Width="60px" CssClass="texto2" TabIndex="10"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txt_fechaingreso" ValidChars="0123456789/">
                                                    </cc1:FilteredTextBoxExtender>--%>
                                     <asp:TextBox ID="txt_fe_dd" runat="server" Width="20px" CssClass="datos" MaxLength="2"
                                        TabIndex="2"></asp:TextBox>/
                                    <cc1:TextBoxWatermarkExtender ID="txt_fe_dd_TextBoxWatermarkExtender" runat="server"
                                        TargetControlID="txt_fe_dd" WatermarkText="dd">
                                    </cc1:TextBoxWatermarkExtender>
                                    <cc1:FilteredTextBoxExtender ID="txt_fe_dd_FilteredTextBoxExtender" runat="server"
                                        TargetControlID="txt_fe_dd" ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                    <asp:TextBox ID="txt_fe_mm" runat="server" Width="20px" CssClass="datos" MaxLength="2"
                                        TabIndex="3"></asp:TextBox>/
                                    <cc1:TextBoxWatermarkExtender ID="txt_fe_mm_TextBoxWatermarkExtender" runat="server"
                                        TargetControlID="txt_fe_mm" WatermarkText="mm">
                                    </cc1:TextBoxWatermarkExtender>
                                    <cc1:FilteredTextBoxExtender ID="txt_fe_mm_FilteredTextBoxExtender" runat="server"
                                        TargetControlID="txt_fe_mm" ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                    <asp:TextBox ID="txt_fe_yy" runat="server" Width="20px" CssClass="datos" MaxLength="2"
                                        TabIndex="4"></asp:TextBox>
                                    <cc1:TextBoxWatermarkExtender ID="txt_fe_yy_TextBoxWatermarkExtender" runat="server"
                                        TargetControlID="txt_fe_yy" WatermarkText="aa">
                                    </cc1:TextBoxWatermarkExtender>
                                    <cc1:FilteredTextBoxExtender ID="txt_fe_yy_FilteredTextBoxExtender" runat="server"
                                        TargetControlID="txt_fe_yy" ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                                </td>                                              
                                            
                                            
                                                <td style="text-align:center; width:92px;" class="GV_rowpad">
                                                    <asp:DropDownList ID="DDL_ARV" runat="server" CssClass="datos" 
                                                        AutoPostBack="true" TabIndex="5">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="text-align:center; width:237px;" class="GV_rowpad">
                                                    <asp:Label ID="NomARV" runat="server" Text=""></asp:Label>
                                                </td>
                                              
                                                
                                                <td style="text-align:center; width:57px;" class="GV_rowpad">
                                                    <asp:TextBox ID="txt_cantidadARV" runat="server" Width="40px" CssClass="texto2" TabIndex="6">0</asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="ftbe_ingresosARV" runat="server" TargetControlID="txt_cantidadARV" ValidChars="0123456789">
                                                    </cc1:FilteredTextBoxExtender>
                                                </td>     
                                                <td style="text-align:center; width:69px;" class="GV_rowpad">
                                                    <asp:TextBox ID="txt_no_req" runat="server" Width="40px" CssClass="texto2" TabIndex="7">0</asp:TextBox>
<%--                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txt_no_req" ValidChars="0123456789">
                                                    </cc1:FilteredTextBoxExtender>--%>
                                                </td>
                                                <td style="text-align:center; width:64px;" class="GV_rowpad">
                                                    <asp:TextBox ID="txt_lote" runat="server" Width="40px" CssClass="texto2" TabIndex="8">0</asp:TextBox>
<%--                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txt_lote" ValidChars="0123456789">
                                                    </cc1:FilteredTextBoxExtender>--%>
                                                </td>
                                        <td style="text-align:center; width:141px;" class="GV_rowpad">
<%--                                                    <asp:TextBox ID="txt_fechaingreso" runat="server" Width="60px" CssClass="texto2" TabIndex="10"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txt_fechaingreso" ValidChars="0123456789/">
                                                    </cc1:FilteredTextBoxExtender>--%>
                                     <asp:TextBox ID="txt_fe_dd_ven" runat="server" Width="20px" CssClass="datos" MaxLength="2"
                                        TabIndex="9"></asp:TextBox>/
                                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server"
                                        TargetControlID="txt_fe_dd_ven" WatermarkText="dd">
                                    </cc1:TextBoxWatermarkExtender>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                        TargetControlID="txt_fe_dd_ven" ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                    <asp:TextBox ID="txt_fe_mm_ven" runat="server" Width="20px" CssClass="datos" MaxLength="2"
                                        TabIndex="10"></asp:TextBox>/
                                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server"
                                        TargetControlID="txt_fe_mm_ven" WatermarkText="mm">
                                    </cc1:TextBoxWatermarkExtender>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server"
                                        TargetControlID="txt_fe_mm_ven" ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                    <asp:TextBox ID="txt_fe_yy_ven" runat="server" Width="20px" CssClass="datos" MaxLength="2"
                                        TabIndex="11"></asp:TextBox>
                                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server"
                                        TargetControlID="txt_fe_yy_ven" WatermarkText="aa">
                                    </cc1:TextBoxWatermarkExtender>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server"
                                        TargetControlID="txt_fe_yy_ven" ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                                </td>                  
                                                                                                                         
                                                <td style="text-align:right; width:50px;" class="GV_rowpad">
                                                    <asp:ImageButton ID="ibt_Agregar" runat="server" ImageUrl="~/images/add.png" OnClick="ibt_Agregar_Click" ToolTip="Agregar" />
                                                    <asp:ImageButton ID="ibt_Modificar" runat="server" ImageUrl="~/images/edit.png" OnClick="ibt_Modificar_Click" ToolTip="Modificar" Visible="false"  />
                                                    <asp:ImageButton ID="ibt_Cancelar" runat="server" ImageUrl="~/images/no.png" OnClick="ibt_Cancelar_Click" ToolTip="Cancelar"  />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                       <%-- <asp:AsyncPostBackTrigger ControlID="btn_buscar" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="txt_asi" EventName="TextChanged" />--%>
                    </Triggers>
                </asp:UpdatePanel>
            </asp:Panel>
        </div>
            <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtender3" runat="server" 
            SuppressPostBack="true" ExpandedImage="~/images/minus2.png" TargetControlID="pnl_IngresoARV"
            CollapseControlID="pnl_IngresoARV1" ExpandControlID="pnl_IngresoARV1" CollapsedImage="~/images/plus2.png"
            Collapsed="true" ImageControlID="ib_IngresoARV">
        </cc1:CollapsiblePanelExtender>


</asp:Content>
