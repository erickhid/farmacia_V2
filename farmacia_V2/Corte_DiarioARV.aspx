<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Corte_DiarioARV.aspx.vb" Inherits="Corte_DiarioARV" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
        <div style="width: 770px; border: solid 1px #5d7b9d; text-align:center;">
            <table id="tbl_ingresomed_inventario" border="0" cellpadding="2" cellspacing="1" style="width: 770px">
                <tr style="width:770px;">
                    <th colspan="16" class="theader">CORTE DIARIO EXISTENCIAS ARV</th>
                </tr>
                <tr>
                    <td>
                        &nbsp;<asp:Label ID="lbl_error" runat="server" CssClass="error"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="text-align:left">
                        <asp:ImageButton ID="ibt_generar" runat="server" ImageUrl="~/images/Corte_med.png" ToolTip="Generar" />
                    </td>
                <td style="width:60px; text-align:right; padding-left:-8px;">
                    <asp:ImageButton ID="IB_exportar" runat="server" ImageUrl="~/images/excel.png" ToolTip="Exportar a Excel" Height="20px" />
                </td>
                </tr>


                </table>
            </div>
<%--        <div>
            <asp:Panel ID="pnl_CorteARV" runat="server">
                <table id="tbl_CARV" border="0" cellpadding="0" cellspacing="1" style="width:770px;">
                    <tr>
                        <td>
                            <table id="Table8" border="0" cellpadding="0" cellspacing="0" style="width:768px;">
                                <tr>
                                    <th class="theader" style="width:20px;"></th>
                                    <th class="theader4">CORTE ARV</th>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>--%>
            <div>
            <asp:Panel ID="pnl_CorteARV" runat="server" Visible="false">
                <asp:UpdatePanel ID="up_pnl_CorteARV" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table id="tblpnl_CorteARV" border="0" cellpadding="0" cellspacing="0" style="width:770px;">
                            
                            <tr>
                                <td>
                                    <asp:GridView ID="GV_pnl_CorteARV" runat="server" ForeColor="#333333"
                                        EmptyDataText="No se existe ningun ingreso del día de hoy." 
                                        Font-Names="Trebuchet MS" Font-Size="8pt" GridLines="None"
                                        CellPadding="0" CellSpacing="1" Width="770px" AutoGenerateColumns="False" 
                                        ShowFooter="False" DataKeyNames="IdFFARV" TabIndex="3">
                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" Wrap="False" />
                                        <Columns>
                                            <asp:TemplateField ShowHeader="False" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_IdIngresoMed" runat="server" Text='<%# Bind("IdFFARV")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Fecha Corte">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_fechacorte" runat="server" Text='<%# Bind("Fecha_Corte")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="75px" CssClass="GV_rowpad" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Código">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Nombre" runat="server" Text='<%# Bind("Codigo")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="75px" CssClass="GV_rowpad" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Medicamento">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Medicamento" runat="server" Text='<%# Bind("Medicamento")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="150px" CssClass="GV_rowpad" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Existencia">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Existencia" runat="server" Text='<%# Bind("Existencia")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="35px" CssClass="GV_rowpad" />
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
<%--                                                <tr>
                                                    <td colspan="8" style="font-weight:bold; color:#333333; text-align:Left;">No existen ingresos de hoy.</td>
                                                </tr>--%>
                                                <tr>
                                                    <td style="background-color:#5D7B9D; font-weight:bold; color:#ffffff; text-align:center; width:95px;">Fecha Corte</td>
                                                    <td style="background-color:#5D7B9D; font-weight:bold; color:#ffffff; text-align:center; width:157px;">Código Medicamento</td>
                                                    <td style="background-color:#5D7B9D; font-weight:bold; color:#ffffff; text-align:center; width:95px;">Nombre</td>
                                                    <td style="background-color:#5D7B9D; font-weight:bold; color:#ffffff; text-align:center; width:35px;">Existencia</td>
                                                </tr>
                                            </table> 
                                        </EmptyDataTemplate>
                                    </asp:GridView>                                    
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
</asp:Content>
