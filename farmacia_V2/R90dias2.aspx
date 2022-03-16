<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="R90dias2.aspx.vb" EnableSessionState="True" Inherits="R90dias2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div style="width: 700px; border: solid 1px #5d7b9d; text-align:left;">
        <table id="tblbasal" border="0" cellpadding="2" cellspacing="1">
            <tr>
                <th colspan="7" class="theader">
                    REPORTE DIARIO +90 DIAS</th>
            </tr>
            <tr>
                <td style="width: 660px; text-align:right;">
                    <asp:Button ID="btn_grabar" runat="server" Text="GENERAR" TabIndex="45" CssClass="button" />
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
                <td colspan="7">
                    &nbsp;<asp:Label ID="lbl_error" runat="server" CssClass="error"></asp:Label>
                </td>
            </tr>
        </table>
        <asp:Panel ID="pnl_reporte" runat="server" BorderColor="#5d7b9d" BorderStyle="solid" BorderWidth="0px" ScrollBars="Auto" Width="698px">
            <table id="tbl_reporte" runat="server" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div style="width: 692px;" class="theader2"><asp:Label ID="lbl_titulo" runat="server"></asp:Label></div>
                                <asp:GridView ID="GV_reportes" runat="server" 
                                    CellPadding="1" 
                                    ForeColor="#333333" 
                                    GridLines="both" BorderColor="#a0acc0" BorderStyle="solid" BorderWidth="1px"
                                    Width="698px" ShowFooter="False" AutoGenerateColumns="False" >
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" CssClass="GV_rowpadREP1" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Cohorte">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Cohorte" runat="server" Text='<%# Bind("Cohorte") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="60px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="60px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="NHC">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_NHC" runat="server" Text='<%# Bind("NHC") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="60px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="60px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Paciente">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Paciente" runat="server" Text='<%# Bind("Paciente") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="338px" HorizontalAlign="Left" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="338px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Ultima Entrega">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_UltimaFechaEntrega" runat="server" Text='<%# Bind("UltimaFechaEntrega") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="100px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="100px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Fecha Retorno">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_FechaRetorno" runat="server" Text='<%# Bind("FechaRetorno") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="100px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="100px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Días">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Dias" runat="server" Text='<%# Bind("Dias") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="40px" HorizontalAlign="Right" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" CssClass="GV_headerREP1" HorizontalAlign="Center" />
                                    <PagerStyle BackColor="#aba392" ForeColor="#333333" HorizontalAlign="Right" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" CssClass="GV_headerREP1" />
                                    <EditRowStyle BackColor="#999999" />
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" CssClass="GV_rowpadREP1" />
                                </asp:GridView>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btn_grabar" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
</asp:Content>
