<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="RepNoARV.aspx.vb" EnableSessionState="True" Inherits="RepNoARV" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div style="width: 700px; border: solid 1px #5d7b9d; text-align:left;">
        <table id="tblbasal" border="0" cellpadding="2" cellspacing="1" width="700">
            <tr>
                <th colspan="9" class="theader">
                    REPORTES PACIENTES SIN ARV</th>
            </tr>
            <tr>
                <td style="width: 50px; background-color: #5d7b9d; color: #ffffff;">
                    TIPO:
                </td>
                <td style="width: 100px; background-color: #e9ecf1;">
                    <asp:DropDownList ID="DDL_tipoR" runat="server" CssClass="datos">
                        <asp:ListItem Value="1">NUEVOS</asp:ListItem>
                        <asp:ListItem Value="2">REINGRESOS</asp:ListItem>
                        <asp:ListItem Value="3">ABANDONOS</asp:ListItem>
                        <asp:ListItem Value="4">FALLECIDOS</asp:ListItem>
                        <asp:ListItem Value="5">TRASLADOS</asp:ListItem>
                        <asp:ListItem Value="6">CAMBIO EDAD</asp:ListItem>
                        <asp:ListItem Value="7">INICIAN TARV</asp:ListItem>
                        <asp:ListItem Value="8">TOTAL ACTIVOS</asp:ListItem>
                        <asp:ListItem Value="9">NO TARV ACTIVO</asp:ListItem>
                        <asp:ListItem Value="10">NO TARV/CONSULTA</asp:ListItem>
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
                            <td colspan="2">
                                <div id="divreporte" runat="server" style="width: 698px;">
                                <div style="width: 694px;" class="theader2"><asp:Label ID="lbl_tituloA" runat="server"></asp:Label></div>
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
                                            <ItemStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cohorte">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Cohorte" runat="server" Text='<%# Bind("Cohorte") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Género">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_IdGenero" runat="server" Text='<%# Bind("IdGenero") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="30px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="30px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="GrupoVulnerabilidad">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGV" runat="server" Text='<%# Bind("GrupoVulnerabilidad") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="200px" HorizontalAlign="left" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="200px" HorizontalAlign="center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Edad">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Edad" runat="server" Text='<%# Bind("Edad") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="30px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="30px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Paciente">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Paciente" runat="server" Text='<%# Bind("Paciente") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" CssClass="GV_rowpad" />
                                            <HeaderStyle HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" CssClass="GV_headerREP1" HorizontalAlign="Center" />
                                    <PagerStyle BackColor="#aba392" ForeColor="#333333" HorizontalAlign="Right" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" CssClass="GV_headerREP1" />
                                    <EditRowStyle BackColor="#999999" />
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" CssClass="GV_rowpadREP1" />
                                </asp:GridView>
                                <asp:GridView ID="GV_reportes2" runat="server" 
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
                                            <ItemStyle Width="30px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="30px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cohorte">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Cohorte" runat="server" Text='<%# Bind("Cohorte") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="30px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="30px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Género">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_IdGenero" runat="server" Text='<%# Bind("IdGenero") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="30px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="30px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="GrupoVulnerabilidad">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGV" runat="server" Text='<%# Bind("GrupoVulnerabilidad") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="200px" HorizontalAlign="left" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="200px" HorizontalAlign="center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Fecha Nacimiento">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_FechaNacimiento" runat="server" Text='<%# Bind("FechaNacimiento") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="100px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="100px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Edad Sale">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_EdadAnterior" runat="server" Text='<%# Bind("EdadAnterior") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="50px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="50px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Edad Entra">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Edad" runat="server" Text='<%# Bind("Edad") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="50px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="50px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Paciente">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Paciente" runat="server" Text='<%# Bind("Paciente") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" CssClass="GV_rowpad" />
                                            <HeaderStyle HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" CssClass="GV_headerREP1" HorizontalAlign="Center" />
                                    <PagerStyle BackColor="#aba392" ForeColor="#333333" HorizontalAlign="Right" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" CssClass="GV_headerREP1" />
                                    <EditRowStyle BackColor="#999999" />
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" CssClass="GV_rowpadREP1" />
                                </asp:GridView>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style=" width:698px; text-align:left;">
                                <br />
                                <div id="divresumen" runat="server" style="width: 698px;">
                                <div style="width: 698px;" class="theader3">&nbsp;&nbsp;RESUMEN</div>
                                <asp:GridView ID="GV_resumen" runat="server" 
                                    CellPadding="1" 
                                    ForeColor="#333333" 
                                    GridLines="both" BorderColor="#a0acc0" BorderStyle="solid" BorderWidth="1px"
                                    Width="698px" ShowFooter="True" AutoGenerateColumns="False" RowStyle-Wrap="false" HeaderStyle-Wrap="false" >
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" CssClass="GV_rowpadREP1" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Género">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_NomGenero" runat="server" Text='<%# Bind("NomGenero") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="100px" HorizontalAlign="Left" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="100px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Edad 10-14">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_R1014" runat="server" Text='<%# Bind("R1014")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Edad 15-18">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_R1518" runat="server" Text='<%# Bind("R1518") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Edad 19-24">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_R1924" runat="server" Text='<%# Bind("R1924")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Edad 25-49">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_R2549" runat="server" Text='<%# Bind("R2549") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Edad 50 +">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_R50" runat="server" Text='<%# Bind("R50") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_total" runat="server" Text='<%# Bind("total")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" CssClass="GV_headerREP1" HorizontalAlign="Center" />
                                    <PagerStyle BackColor="#aba392" ForeColor="#333333" HorizontalAlign="Right" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" CssClass="GV_headerREP1" />
                                    <EditRowStyle BackColor="#999999" />
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" CssClass="GV_rowpadREP1" />
                                </asp:GridView>
                                <asp:GridView ID="GV_resumen2" runat="server" 
                                    CellPadding="1" 
                                    ForeColor="#333333" 
                                    GridLines="both" BorderColor="#a0acc0" BorderStyle="solid" BorderWidth="1px"
                                    Width="698px" ShowFooter="True" AutoGenerateColumns="False" RowStyle-Wrap="false" HeaderStyle-Wrap="false" >
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" CssClass="GV_rowpadREP1" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Género">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_NomGenero" runat="server" Text='<%# Bind("NomGenero") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="100px" HorizontalAlign="Left" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="100px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="S10-14">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_S1014" runat="server" Text='<%# Bind("S1014")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="S15-18">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_S1518" runat="server" Text='<%# Bind("S1518")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="S19-24">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_S1924" runat="server" Text='<%# Bind("S1924")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="S25-49">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_S2549" runat="server" Text='<%# Bind("S2549")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="S50 +">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_S50" runat="server" Text='<%# Bind("S50")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="E10-14">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_E1014" runat="server" Text='<%# Bind("E1014")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="E15-18">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_E1518" runat="server" Text='<%# Bind("E1518")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="E19-24">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_E1924" runat="server" Text='<%# Bind("E1924")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="E25-49">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_E2549" runat="server" Text='<%# Bind("E2549")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="E50 +">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_E50" runat="server" Text='<%# Bind("E50")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" CssClass="GV_headerREP1" HorizontalAlign="Center" />
                                    <PagerStyle BackColor="#aba392" ForeColor="#333333" HorizontalAlign="Right" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" CssClass="GV_headerREP1" />
                                    <EditRowStyle BackColor="#999999" />
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" CssClass="GV_rowpadREP1" />
                                </asp:GridView>
                                </div>
                            </td>
                            <td></td>
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
