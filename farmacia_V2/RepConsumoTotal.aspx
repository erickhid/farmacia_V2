<%@ Page  Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="RepConsumoTotal.aspx.vb" Inherits="RConsumoArvOIngresos" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div style="width: 700px; border: solid 1px #5d7b9d; text-align:left;">
        <table id="tblbasal" border="0" cellpadding="2" cellspacing="1" width="700">
            <tr>
                <th colspan="9" class="theader">
                    REPORTE DE CONSUMOS + OTROS EGRESOS</th>
            </tr>
            <tr>
                <td style="width: 50px; background-color: #5d7b9d; color: #ffffff;">
                    TIPO:
                </td>
                <td style="width: 100px; background-color: #e9ecf1;">
                    <asp:DropDownList ID="DDL_tipoR" runat="server" CssClass="datos">
                        <asp:ListItem Value="1">ARV</asp:ListItem>                        
                        <asp:ListItem Value="2">PROFILAXIS</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="background-color: #5d7b9d; color: #ffffff;">Fecha Inicio:
                </td>
                <td style="background-color: #e9ecf1; text-align: center;">
                    <asp:TextBox ID="txt_fechai" runat="server" Style="width: 75px; text-align:center;" AutoPostBack="False" MaxLength="10" TabIndex="1"></asp:TextBox>
                    <cc1:TextBoxWatermarkExtender ID="txt_fechaI_TBWE" runat="server" TargetControlID="txt_fechaI" WatermarkCssClass="wm" WatermarkText="dd/mm/aaaa">
                    </cc1:TextBoxWatermarkExtender>
                    <cc1:FilteredTextBoxExtender ID="txt_fechaI_filterd" runat="server" TargetControlID="txt_fechaI" ValidChars="0123456789/">
                    </cc1:FilteredTextBoxExtender>
                    <cc1:CalendarExtender ID="txt_fechaI_CE" runat="server" Format="dd/MM/yyyy" TargetControlID="txt_fechaI">
                    </cc1:CalendarExtender>
                </td>
                <td style="background-color: #5d7b9d; color: #ffffff;">Fecha Fin:
                </td>
                <td style="background-color: #e9ecf1; text-align: center;">
                    <asp:TextBox ID="txt_fechaf" runat="server" Style="width: 75px; text-align:center;" AutoPostBack="False" MaxLength="10" TabIndex="1"></asp:TextBox>
                    <cc1:TextBoxWatermarkExtender ID="txt_fechaF_TBWE" runat="server" TargetControlID="txt_fechaF" WatermarkCssClass="wm" WatermarkText="dd/mm/aaaa">
                    </cc1:TextBoxWatermarkExtender>
                    <cc1:FilteredTextBoxExtender ID="txt_fechaF_filterd" runat="server" TargetControlID="txt_fechaF" ValidChars="0123456789/">
                    </cc1:FilteredTextBoxExtender>
                    <cc1:CalendarExtender ID="txt_fechaF_CE" runat="server" Format="dd/MM/yyyy" TargetControlID="txt_fechaF">
                    </cc1:CalendarExtender>
                </td>               
                <td style="text-align:center" >
                    <asp:Button ID="btn_grabar" runat="server" Text="Generar" TabIndex="60" Height="25" Width="85" CssClass="button01" />
                </td>
                <td style="width:18px; text-align:center;">
                    <asp:UpdateProgress ID="UP_procesar" runat="server" DisplayAfter="100">
                        <ProgressTemplate>
                            <div>
                                <asp:Image ID="iloader1" runat="server" ImageUrl="~/images/ajax-loader1.gif" />
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                 </td>
                 <td style="width:22px; text-align:center;">
                    <asp:ImageButton ID="IB_exportar" runat="server" ImageUrl="~/images/excel.png" ToolTip="Exportar a Excel" Height="20px" />
                </td>
            </tr>
            <tr>
                <td colspan="9">
                    &nbsp;<asp:Label ID="lbl_error" runat="server" CssClass="error"></asp:Label>
                </td>
            </tr>
        </table>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div style="width: 694px;" class="theader2"><asp:Label ID="lbl_titulo" runat="server"></asp:Label></div>
                <asp:Panel ID="pnl_reporte" runat="server" BorderColor="#5d7b9d" BorderStyle="solid" BorderWidth="0px" ScrollBars="Auto" Width="698px">
                    <table id="tbl_reporte" runat="server" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <asp:GridView ID="GV_rconsumo" runat="server" 
                                    CellPadding="1" 
                                    ForeColor="#333333" 
                                    Width="100%" RowStyle-Wrap="false"  
                                    GridLines="both" BorderColor="#a0acc0" BorderStyle="solid" BorderWidth="1px"
                                    ShowFooter="true" AutoGenerateColumns="True" >
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" CssClass="GV_rowpadREP" />
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" CssClass="GV_headerREP" HorizontalAlign="Right" />
                                    <PagerStyle BackColor="#aba392" ForeColor="#333333" HorizontalAlign="Right" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" CssClass="GV_headerREP" />
                                    <EditRowStyle BackColor="#999999" />
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" CssClass="GV_rowpadREP" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
        </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btn_grabar" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>