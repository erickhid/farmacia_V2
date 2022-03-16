<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="IOS_ITS.aspx.vb" EnableSessionState="True" Inherits="IOS_ITS" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div style="width: 700px; border: solid 1px #5d7b9d; text-align:left;">
        <table id="tblbasal" border="0" cellpadding="2" cellspacing="1" width="700">
            <tr>
                <th colspan="9" class="theader">
                    IOS - ITS</th>
            </tr>
            <tr>
                <td style="width: 50px; background-color: #5d7b9d; color: #ffffff;">
                    TIPO:
                </td>
                <td style="width: 100px; background-color: #e9ecf1;">
                    <asp:DropDownList ID="DDL_tipoR" runat="server" CssClass="datos">
                        <asp:ListItem Value="1">REPORTE IOS</asp:ListItem>
                        <asp:ListItem Value="2">LISTA PX IOS</asp:ListItem>
                        <asp:ListItem Value="3">REPORTE ITS</asp:ListItem>
                        <asp:ListItem Value="4">LISTA PX ITS</asp:ListItem>
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
                <div style="width: 694px;" class="theader2">
                    <asp:Label ID="lbl_titulo" runat="server"></asp:Label>
                </div>
                <asp:Panel ID="pnl_reporte" runat="server" BorderColor="#5d7b9d" BorderStyle="solid" BorderWidth="0px" ScrollBars="Auto" Width="698px">
                    <table id="tbl_reporte" runat="server" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <asp:GridView ID="GV_repIOS" runat="server" 
                                    CellPadding="1" 
                                    ForeColor="#333333" 
                                    GridLines="both" BorderColor="#a0acc0" BorderStyle="solid" BorderWidth="1px"
                                    Width="1160px" ShowFooter="true" AutoGenerateColumns="False" >
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" CssClass="GV_rowpadREP" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Infección Oportunista">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_NomEnfermedad" runat="server" Text='<%# Bind("NomEnfermedad")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="500px" HorizontalAlign="Left" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="500px" HorizontalAlign="Left" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Código">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Enfermedad" runat="server" Text='<%# Bind("Enfermedad")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="50px" HorizontalAlign="Left" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="50px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="M1014">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_M1014" runat="server" Text='<%# Bind("M1014")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="F1014">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_F1014" runat="server" Text='<%# Bind("F1014")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="T1014">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_T1014" runat="server" Text='<%# Bind("T1014")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="M1518">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_M1518" runat="server" Text='<%# Bind("M1518")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="F1518">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_F1518" runat="server" Text='<%# Bind("F1518")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="T1518">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_T1518" runat="server" Text='<%# Bind("T1518")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="M1924">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_M1924" runat="server" Text='<%# Bind("M1924")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="F1924">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_F1924" runat="server" Text='<%# Bind("F1924")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="T1924">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_T1924" runat="server" Text='<%# Bind("T1924")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="M2549">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_M2549" runat="server" Text='<%# Bind("M2549")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="F2549">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_F2549" runat="server" Text='<%# Bind("F2549")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="T2549">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_T2549" runat="server" Text='<%# Bind("T2549")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="M50">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_M50" runat="server" Text='<%# Bind("M50")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="F50">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_F50" runat="server" Text='<%# Bind("F50")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="T50">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_T50" runat="server" Text='<%# Bind("T50")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="TOTAL">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_TOTAL" runat="server" Text='<%# Bind("TOTAL")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="50px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="50px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" CssClass="GV_header" HorizontalAlign="Right" />
                                    <PagerStyle BackColor="#aba392" ForeColor="#333333" HorizontalAlign="Right" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" CssClass="GV_header" />
                                    <EditRowStyle BackColor="#999999" />
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" CssClass="GV_rowpadREP" />
                                </asp:GridView>
                                <asp:GridView ID="GV_pxIOS" runat="server" 
                                    CellPadding="1" 
                                    ForeColor="#333333" 
                                    GridLines="both" BorderColor="#a0acc0" BorderStyle="solid" BorderWidth="1px"
                                    Width="698px" ShowFooter="False" AutoGenerateColumns="False" >
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" CssClass="GV_rowpad" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="NHC">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_NHC" runat="server" Text='<%# Bind("NHC")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cohorte">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Cohorte" runat="server" Text='<%# Bind("Cohorte")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="45px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="45px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Edad">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Edad" runat="server" Text='<%# Bind("Edad")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="35px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="35px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Género">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_IdGenero" runat="server" Text='<%# Bind("IdGenero")%>'></asp:Label>
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
                                        <asp:TemplateField HeaderText="Fecha">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_FechaEnfermedad" runat="server" Text='<%# Bind("FechaEnfermedad")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="60px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="60px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Código">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Enfermedad" runat="server" Text='<%# Bind("Enfermedad")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="45px" HorizontalAlign="Left" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="45px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Infección Oportunista">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_NomEnfermedad" runat="server" Text='<%# Bind("NomEnfermedad")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="428px" HorizontalAlign="Left" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="428px" HorizontalAlign="Left" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" CssClass="GV_header" HorizontalAlign="Center" />
                                    <PagerStyle BackColor="#aba392" ForeColor="#333333" HorizontalAlign="Right" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" CssClass="GV_header" />
                                    <EditRowStyle BackColor="#999999" />
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" CssClass="GV_rowpad" />
                                </asp:GridView>
                                <asp:GridView ID="GV_repITS" runat="server" 
                                    CellPadding="1" 
                                    ForeColor="#333333" 
                                    GridLines="both" BorderColor="#a0acc0" BorderStyle="solid" BorderWidth="1px"
                                    Width="1010px" ShowFooter="true" AutoGenerateColumns="False" >
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" CssClass="GV_rowpadREP" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="ITS">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_ITS" runat="server" Text='<%# Bind("ITS")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="400px" HorizontalAlign="Left" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="400px" HorizontalAlign="Left" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="M1014">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_M1014" runat="server" Text='<%# Bind("M1014")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="F1014">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_F1014" runat="server" Text='<%# Bind("F1014")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="T1014">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_T1014" runat="server" Text='<%# Bind("T1014")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="M1518">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_M1518" runat="server" Text='<%# Bind("M1518")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="F1518">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_F1518" runat="server" Text='<%# Bind("F1518")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="T1518">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_T1518" runat="server" Text='<%# Bind("T1518")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="M1924">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_M1924" runat="server" Text='<%# Bind("M1924")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="F1924">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_F1924" runat="server" Text='<%# Bind("F1924")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="T1924">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_T1924" runat="server" Text='<%# Bind("T1924")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="M2549">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_M2549" runat="server" Text='<%# Bind("M2549")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="F2549">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_F2549" runat="server" Text='<%# Bind("F2549")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="T2549">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_T2549" runat="server" Text='<%# Bind("T2549")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="M50">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_M50" runat="server" Text='<%# Bind("M50")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="F50">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_F50" runat="server" Text='<%# Bind("F50")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="T50">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_T50" runat="server" Text='<%# Bind("T50")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="TOTAL">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_TOTAL" runat="server" Text='<%# Bind("TOTAL")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="50px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="50px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" CssClass="GV_header" HorizontalAlign="Right" />
                                    <PagerStyle BackColor="#aba392" ForeColor="#333333" HorizontalAlign="Right" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" CssClass="GV_header" />
                                    <EditRowStyle BackColor="#999999" />
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" CssClass="GV_rowpadREP" />
                                </asp:GridView>
                                <asp:GridView ID="GV_pxITS" runat="server" 
                                    CellPadding="1" 
                                    ForeColor="#333333" 
                                    GridLines="both" BorderColor="#a0acc0" BorderStyle="solid" BorderWidth="1px"
                                    Width="698px" ShowFooter="False" AutoGenerateColumns="False" >
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" CssClass="GV_rowpad" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="NHC">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_NHC" runat="server" Text='<%# Bind("NHC")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cohorte">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Cohorte" runat="server" Text='<%# Bind("Cohorte")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="45px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="45px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Edad">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Edad" runat="server" Text='<%# Bind("Edad")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="35px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="35px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Género">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_IdGenero" runat="server" Text='<%# Bind("IdGenero")%>'></asp:Label>
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
                                        <asp:TemplateField HeaderText="Fecha">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_FechaITS" runat="server" Text='<%# Bind("FechaITS")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="60px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="60px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Código">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_AgenteITS" runat="server" Text='<%# Bind("AgenteITS")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="45px" HorizontalAlign="Left" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="45px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Infección Oportunista">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_ITS" runat="server" Text='<%# Bind("ITS")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="428px" HorizontalAlign="Left" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="428px" HorizontalAlign="Left" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" CssClass="GV_header" HorizontalAlign="Center" />
                                    <PagerStyle BackColor="#aba392" ForeColor="#333333" HorizontalAlign="Right" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" CssClass="GV_header" />
                                    <EditRowStyle BackColor="#999999" />
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" CssClass="GV_rowpad" />
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
