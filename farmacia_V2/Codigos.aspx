<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Codigos.aspx.vb" EnableSessionState="True" Inherits="Codigos" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div style="width: 700px; border: solid 1px #5d7b9d; text-align:left;">
        <table id="tblbasal" border="0" cellpadding="2" cellspacing="1" width="700">
            <tr>
                <td style="width:640px; text-align:center;">
                   <div style="width: 640px;" class="theader2"><asp:Label ID="lbl_titulo" runat="server"></asp:Label></div>
                </td>
                <td style="width:60px; text-align:left; padding-left:-8px;">
                    <asp:ImageButton ID="IB_exportar" runat="server" ImageUrl="~/images/excel.png" ToolTip="Exportar a Excel" Height="20px" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Panel ID="pnl_reporte" runat="server" BorderColor="#5d7b9d" BorderStyle="solid" BorderWidth="0px" ScrollBars="Auto" Width="696px">
                        <table id="tbl_reporte" runat="server" border="0" cellpadding="0" cellspacing="0" width="692px">
                            <tr>
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                
                                            <asp:GridView ID="GV_codigos" runat="server" 
                                                CellPadding="1" 
                                                ForeColor="#333333" 
                                                Width="100%" RowStyle-Wrap="false"  
                                                GridLines="both" BorderColor="#a0acc0" BorderStyle="solid" BorderWidth="1px"
                                                ShowFooter="False" AutoGenerateColumns="True" >
                                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" CssClass="GV_rowpadREP" />
                                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" CssClass="GV_headerREP" HorizontalAlign="Right" />
                                                <PagerStyle BackColor="#aba392" ForeColor="#333333" HorizontalAlign="Right" />
                                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" CssClass="GV_headerREP" />
                                                <EditRowStyle BackColor="#999999" />
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" CssClass="GV_rowpadREP" />
                                            </asp:GridView>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;<asp:Label ID="lbl_error" runat="server" CssClass="error"></asp:Label>
                </td>
            </tr>
        </table>
        
    </div>
</asp:Content>
