<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="RVisitas.aspx.vb" EnableSessionState="True" Inherits="RVisitas" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true" EnableScriptLocalization="true"></asp:ScriptManager>
    <div style="width: 700px; border: solid 1px #5d7b9d; text-align:left;">
        <table id="tblbasal" border="0" cellpadding="2" cellspacing="1">
            <tr>
                <th colspan="6" class="theader">
                    REPORTE PROXIMAS VISITAS FARMACIA</th>
            </tr>
            <tr>
                <td style="width: 50px; background-color: #5d7b9d; color: #ffffff;">
                    FECHA:
                </td>
                <td style="width: 80px; background-color: #e9ecf1;">
                    <asp:TextBox ID="txt_fecha" runat="server" CssClass="datos" Width="80px"></asp:TextBox>
                </td>
                <td style="width: 20px; background-color: #e9ecf1;">
                    <asp:ImageButton ID="IB_calendario" runat="server" 
                        ImageUrl="~/images/datePickerPopupHover.gif" BorderWidth="0" />
                    <cc1:CalendarExtender ID="txt_fecha_CalendarExtender" runat="server" 
                        TargetControlID="txt_fecha" Format="dd/MM/yyyy" 
                        PopupButtonID="IB_calendario" CssClass="ajax__calendar" >
                    </cc1:CalendarExtender>
                </td>
                <td style="width: 510px; text-align:center;">
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
                <td colspan="6">
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
                                            <ItemStyle Width="50px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="50px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Paciente">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Paciente" runat="server" Text='<%# Bind("Paciente") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="318px" HorizontalAlign="Left" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="318px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Género">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_NomGenero" runat="server" Text='<%# Bind("NomGenero") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="100px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="100px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
										<asp:TemplateField HeaderText="GrupoVulnerabilidad">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGV" runat="server" Text='<%# Bind("GrupoVulnerabilidad") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="45px" HorizontalAlign="Left" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="45px" HorizontalAlign="Left" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Fecha Entrega">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_FechaEntrega" runat="server" Text='<%# Bind("FechaEntrega") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="80px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="80px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Fecha Retorno">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_FechaRetorno" runat="server" Text='<%# Bind("FechaRetorno") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="90px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="90px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cita Médica">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_cita_med" runat="server" Text='<%# Bind("Cita_Medica")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="90px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="90px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cita Farmacia">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_cita_med" runat="server" Text='<%# Bind("Cita_Farmacia")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="90px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="90px" HorizontalAlign="Center" CssClass="GV_rowpad" />
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
