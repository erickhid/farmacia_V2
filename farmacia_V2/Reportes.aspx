<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Reportes.aspx.vb" EnableSessionState="True" Inherits="Reportes" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div style="width: 700px; border: solid 1px #5d7b9d; text-align:left;">
        <table id="tblbasal" border="0" cellpadding="2" cellspacing="1" width="700">
            <tr>
                <th colspan="9" class="theader">
                    REPORTES</th>
            </tr>
            <tr>
                <td style="width: 50px; background-color: #5d7b9d; color: #ffffff;">
                    TIPO:
                </td>
                <td style="width: 100px; background-color: #e9ecf1;">
                    <asp:DropDownList ID="DDL_tipoR" runat="server" CssClass="datos">
                        <asp:ListItem Value="1">EMBARAZADAS</asp:ListItem>
                        <asp:ListItem Value="2">POSTPARTO</asp:ListItem>
                        <asp:ListItem Value="3">FALLECIDOS</asp:ListItem>
                        <asp:ListItem Value="4">ABANDONOS</asp:ListItem>
                        <asp:ListItem Value="5">TRASLADOS</asp:ListItem>
                        <asp:ListItem Value="6">INICIOS</asp:ListItem>
                        <asp:ListItem Value="7">REINICIOS</asp:ListItem>
                        <asp:ListItem Value="8">CAMBIOS</asp:ListItem>
                        <asp:ListItem Value="9">REFERIDOS</asp:ListItem>
                        <asp:ListItem Value="10">REINGRESOS</asp:ListItem>
                        <asp:ListItem Value="11">CAMBIOS FF</asp:ListItem>
                    </asp:DropDownList>
                </td>
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
                <td colspan="9">
                    &nbsp;<asp:Label ID="lbl_error" runat="server" CssClass="error"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="resultado" runat="server" style="width: 698px; border: solid 1px #5d7b9d; text-align:left;">
                <asp:Panel ID="pnl_reporte" runat="server" BorderColor="#5d7b9d" BorderStyle="solid" BorderWidth="0px" ScrollBars="Auto" Width="698px">
                    <table id="tbl_reporte" runat="server" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <div style="width: 692px;" class="theader2"><asp:Label ID="lbl_tituloA" runat="server"></asp:Label></div>
                                <asp:GridView ID="GV_reportes" runat="server" 
                                    CellPadding="1" 
                                    ForeColor="#333333" 
                                    GridLines="both" BorderColor="#a0acc0" BorderStyle="solid" BorderWidth="1px"
                                    Width="698px" ShowFooter="False" AutoGenerateColumns="False" >
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" CssClass="GV_rowpadREP1" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="NHC">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_NHC" runat="server" Text='<%# Bind("NHC") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="50px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="50px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Género">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_IdGenero" runat="server" Text='<%# Bind("IdGenero") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="50px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="50px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="GrupoVulnerabilidad">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGV" runat="server" Text='<%# Bind("GrupoVulnerabilidad") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="150px" HorizontalAlign="Left" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="150px" HorizontalAlign="Left" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Edad">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Edad" runat="server" Text='<%# Bind("Edad") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="116px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="116px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Embarazo">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Embarazo" runat="server" Text='<%# Bind("Embarazo") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="116px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="116px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Fecha Entrega">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_FechaEntrega" runat="server" Text='<%# Bind("FechaEntrega") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="116px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="116px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Esquema">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_IdEsquema" runat="server" Text='<%# Bind("IdEsquema") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="118px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="118px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" CssClass="GV_headerREP1" HorizontalAlign="Center" />
                                    <PagerStyle BackColor="#aba392" ForeColor="#333333" HorizontalAlign="Right" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" CssClass="GV_headerREP1" />
                                    <EditRowStyle BackColor="#999999" />
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" CssClass="GV_rowpadREP1" />
                                </asp:GridView>
                                <br />
                                <div style="width: 692px;" class="theader2"><asp:Label ID="lbl_tituloN" runat="server"></asp:Label></div>
                                <asp:GridView ID="GV_reportesP" runat="server" 
                                    CellPadding="1" 
                                    ForeColor="#333333" 
                                    GridLines="both" BorderColor="#a0acc0" BorderStyle="solid" BorderWidth="1px"
                                    Width="698px" ShowFooter="False" AutoGenerateColumns="False" >
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" CssClass="GV_rowpadREP1" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="NHC">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_NHC" runat="server" Text='<%# Bind("NHC") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="116px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="116px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Género">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_IdGenero" runat="server" Text='<%# Bind("Genero") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="116px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="116px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Edad">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Edad" runat="server" Text='<%# Bind("Edad") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="116px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="116px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Embarazo">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Embarazo" runat="server" Text='<%# Bind("Embarazo") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="116px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="116px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Fecha Entrega">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_FechaEntrega" runat="server" Text='<%# Bind("FechaEntrega") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="116px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="116px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Esquema">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_IdEsquema" runat="server" Text='<%# Bind("IdEsquema") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="118px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="118px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" CssClass="GV_headerREP1" HorizontalAlign="Center" />
                                    <PagerStyle BackColor="#aba392" ForeColor="#333333" HorizontalAlign="Right" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" CssClass="GV_headerREP1" />
                                    <EditRowStyle BackColor="#999999" />
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" CssClass="GV_rowpadREP1" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btn_grabar" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
