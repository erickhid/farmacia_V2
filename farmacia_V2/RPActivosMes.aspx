<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="RPActivosMes.aspx.vb" EnableSessionState="True" Inherits="RPActivosMes" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div style="width: 700px; border: solid 1px #5d7b9d; text-align:left;">
        <table id="tblbasal" border="0" cellpadding="2" cellspacing="1" width="700">
            <tr>
                <th colspan="7" class="theader">
                    LISTADO PACIENTES ACTIVOS</th>
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
                    <asp:DropDownList ID="DDL_ano" runat="server" CssClass="datos">
                    </asp:DropDownList>
                </td>
                <td style="text-align:center;">
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
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div style="width:694px;" class="theader2"><asp:Label ID="lbl_titulo" runat="server"></asp:Label></div>
                <asp:Panel ID="pnl_reporte" runat="server" BorderColor="#5d7b9d" BorderStyle="solid" BorderWidth="0px" ScrollBars="Auto" Width="698px" Height="600px">
                    <table id="tbl_reporte" runat="server" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <asp:GridView ID="GV_rsigpro" runat="server" 
                                    CellPadding="1" 
                                    ForeColor="#333333" 
                                    GridLines="both" BorderColor="#a0acc0" BorderStyle="solid" BorderWidth="1px"
                                    Width="100%" ShowFooter="False" AutoGenerateColumns="False" RowStyle-Wrap="false" HeaderStyle-Wrap="false" >
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" CssClass="GV_rowpadREP" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="NHC">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_NHC" runat="server" Text='<%# Bind("NHC") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="50px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="50px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Paciente">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_PACIENTE" runat="server" Text='<%# Bind("Paciente") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="250px" HorizontalAlign="Left" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="250px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Género">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_IdGenero" runat="server" Text='<%# Bind("IdGenero") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="45px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="45px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
										<asp:TemplateField HeaderText="GrupoVulnerabilidad">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGV" runat="server" Text='<%# Bind("GrupoVulnerabilidad") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="45px" HorizontalAlign="Left" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="45px" HorizontalAlign="Left" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Edad">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Edad" runat="server" Text='<%# Bind("Edad") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="45px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="45px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Embarazo">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Embarazo" runat="server" Text='<%# Bind("Embarazo") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="45px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="45px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Fecha Entrega">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_FechaEntrega" runat="server" Text='<%# Bind("FechaEntrega") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="160px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="160px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Fecha Retorno">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_FechaRetorno" runat="server" Text='<%# Bind("FechaRetorno") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="160px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="160px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Esquema">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_IdEsquema" runat="server" Text='<%# Bind("IdEsquema") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="45px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sub-Esquema">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_IdSEsquema" runat="server" Text='<%# Bind("IdSEsquema") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="45px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Estatus">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_EsquemaEstatus" runat="server" Text='<%# Bind("EsquemaEstatus") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="45px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="M1">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_M1" runat="server" Text='<%# Bind("M1") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="45px" HorizontalAlign="Right" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="45px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ME1">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_ME1" runat="server" Text='<%# Bind("ME1") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="45px" HorizontalAlign="Right" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="45px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="M2">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_M2" runat="server" Text='<%# Bind("M2") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="45px" HorizontalAlign="Right" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="45px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ME2">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_ME2" runat="server" Text='<%# Bind("ME2") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="45px" HorizontalAlign="Right" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="45px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="M3">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_M3" runat="server" Text='<%# Bind("M3") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="45px" HorizontalAlign="Right" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="45px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ME3">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_ME3" runat="server" Text='<%# Bind("ME3") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="45px" HorizontalAlign="Right" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="45px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="M4">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_M4" runat="server" Text='<%# Bind("M4") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="45px" HorizontalAlign="Right" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="45px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ME4">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_ME4" runat="server" Text='<%# Bind("ME4") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="45px" HorizontalAlign="Right" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="45px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="M5">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_M5" runat="server" Text='<%# Bind("M5") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="45px" HorizontalAlign="Right" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="45px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ME5">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_ME5" runat="server" Text='<%# Bind("ME5") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="45px" HorizontalAlign="Right" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="45px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="M6">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_M6" runat="server" Text='<%# Bind("M6") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="45px" HorizontalAlign="Right" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="45px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ME6">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_ME6" runat="server" Text='<%# Bind("ME6") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="45px" HorizontalAlign="Right" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="45px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="M7">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_M7" runat="server" Text='<%# Bind("M7") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="45px" HorizontalAlign="Right" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="45px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ME7">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_ME7" runat="server" Text='<%# Bind("ME7") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="45px" HorizontalAlign="Right" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="45px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="M8">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_M8" runat="server" Text='<%# Bind("M8") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="45px" HorizontalAlign="Right" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="45px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ME8">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_ME8" runat="server" Text='<%# Bind("ME8") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="45px" HorizontalAlign="Right" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="45px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" CssClass="GV_headerREP" HorizontalAlign="Right" />
                                    <PagerStyle BackColor="#aba392" ForeColor="#333333" HorizontalAlign="Right" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" CssClass="GV_headerREP" />
                                    <EditRowStyle BackColor="#999999" />
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" CssClass="GV_rowpadREP" />
                                </asp:GridView>
                                <asp:GridView ID="GV_exportar" runat="server"
                                    AutoGenerateColumns="true" Visible="false" >
                                    <RowStyle />
                                    <HeaderStyle />
                                    <AlternatingRowStyle/>
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
