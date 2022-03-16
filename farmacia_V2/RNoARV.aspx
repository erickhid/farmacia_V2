<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="RNoARV.aspx.vb" EnableSessionState="True" Inherits="RNoARV" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div style="width: 700px; border: solid 1px #5d7b9d; text-align:left;">
        <table id="tblbasal" border="0" cellpadding="2" cellspacing="1">
            <tr>
                <th colspan="7" class="theader">
                    PACIENTES SIN ARV</th>
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
                <td style="width: 360px; text-align:center;">
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
        <center>
        <table id="tbl_reporte" runat="server" border="0" cellpadding="0" cellspacing="0" style="width: 680px;">
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div style="width: 674px;" class="theader2">PACIENTES SIN ARV 10-14</div>
                            <asp:GridView ID="GV_rnoarv10_14" runat="server" 
                                CellPadding="1" 
                                ForeColor="#333333" 
                                GridLines="both" BorderColor="#a0acc0" BorderStyle="solid" BorderWidth="1px"
                                Width="680px" ShowFooter="False" AutoGenerateColumns="False" RowStyle-Wrap="false" HeaderStyle-Wrap="false" >
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" CssClass="GV_rowpadREP" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Género">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_NomGenero" runat="server" Text='<%# Bind("NomGenero") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="80px" HorizontalAlign="Left" CssClass="GV_rowpad" />
                                        <HeaderStyle Width="80px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Nuevos">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Nuevos" runat="server" Text='<%# Bind("Nuevos") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        <HeaderStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Reingresos">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Reingresos" runat="server" Text='<%# Bind("Reingresos") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        <HeaderStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Abandonos">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Abandonos" runat="server" Text='<%# Bind("Abandonos") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        <HeaderStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Fallecidos">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Fallecidos" runat="server" Text='<%# Bind("Fallecidos") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        <HeaderStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Traslados">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Traslados" runat="server" Text='<%# Bind("Traslados") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        <HeaderStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="- Edad">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_SalenCambioEdad" runat="server" Text='<%# Bind("SalenCambioEdad") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        <HeaderStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="+ Edad">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_EntranCambioEdad" runat="server" Text='<%# Bind("EntranCambioEdad") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        <HeaderStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Inician TARV">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_InicianTARV" runat="server" Text='<%# Bind("InicianTARV") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        <HeaderStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" CssClass="GV_headerREP" HorizontalAlign="Right" />
                                <PagerStyle BackColor="#aba392" ForeColor="#333333" HorizontalAlign="Right" />
                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" CssClass="GV_headerREP" />
                                <EditRowStyle BackColor="#999999" />
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" CssClass="GV_rowpadREP" />
                            </asp:GridView><br />
                            <div style="width: 674px;" class="theader2">PACIENTES SIN ARV 15-18</div>
                            <asp:GridView ID="GV_rnoarv15_18" runat="server" 
                                CellPadding="1" 
                                ForeColor="#333333" 
                                GridLines="both" BorderColor="#a0acc0" BorderStyle="solid" BorderWidth="1px"
                                Width="680px" ShowFooter="False" AutoGenerateColumns="False" RowStyle-Wrap="false" HeaderStyle-Wrap="false" >
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" CssClass="GV_rowpadREP" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Género">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_NomGenero" runat="server" Text='<%# Bind("NomGenero") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="80px" HorizontalAlign="Left" CssClass="GV_rowpad" />
                                        <HeaderStyle Width="80px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Nuevos">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Nuevos" runat="server" Text='<%# Bind("Nuevos") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        <HeaderStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Reingresos">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Reingresos" runat="server" Text='<%# Bind("Reingresos") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        <HeaderStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Abandonos">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Abandonos" runat="server" Text='<%# Bind("Abandonos") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        <HeaderStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Fallecidos">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Fallecidos" runat="server" Text='<%# Bind("Fallecidos") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        <HeaderStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Traslados">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Traslados" runat="server" Text='<%# Bind("Traslados") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        <HeaderStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="- Edad">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_SalenCambioEdad" runat="server" Text='<%# Bind("SalenCambioEdad") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        <HeaderStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="+ Edad">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_EntranCambioEdad" runat="server" Text='<%# Bind("EntranCambioEdad") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        <HeaderStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Inician TARV">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_InicianTARV" runat="server" Text='<%# Bind("InicianTARV") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        <HeaderStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" CssClass="GV_headerREP" HorizontalAlign="Right" />
                                <PagerStyle BackColor="#aba392" ForeColor="#333333" HorizontalAlign="Right" />
                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" CssClass="GV_headerREP" />
                                <EditRowStyle BackColor="#999999" />
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" CssClass="GV_rowpadREP" />
                            </asp:GridView><br />
                            <div style="width: 674px;" class="theader2">PACIENTES SIN ARV 19-24</div>
                            <asp:GridView ID="GV_rnoarv19_24" runat="server" 
                                CellPadding="1" 
                                ForeColor="#333333" 
                                GridLines="both" BorderColor="#a0acc0" BorderStyle="solid" BorderWidth="1px"
                                Width="680px" ShowFooter="False" AutoGenerateColumns="False" RowStyle-Wrap="false" HeaderStyle-Wrap="false" >
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" CssClass="GV_rowpadREP" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Género">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_NomGenero" runat="server" Text='<%# Bind("NomGenero") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="80px" HorizontalAlign="Left" CssClass="GV_rowpad" />
                                        <HeaderStyle Width="80px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Nuevos">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Nuevos" runat="server" Text='<%# Bind("Nuevos") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        <HeaderStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Reingresos">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Reingresos" runat="server" Text='<%# Bind("Reingresos") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        <HeaderStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Abandonos">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Abandonos" runat="server" Text='<%# Bind("Abandonos") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        <HeaderStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Fallecidos">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Fallecidos" runat="server" Text='<%# Bind("Fallecidos") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        <HeaderStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Traslados">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Traslados" runat="server" Text='<%# Bind("Traslados") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        <HeaderStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="- Edad">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_SalenCambioEdad" runat="server" Text='<%# Bind("SalenCambioEdad") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        <HeaderStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="+ Edad">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_EntranCambioEdad" runat="server" Text='<%# Bind("EntranCambioEdad") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        <HeaderStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Inician TARV">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_InicianTARV" runat="server" Text='<%# Bind("InicianTARV") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        <HeaderStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" CssClass="GV_headerREP" HorizontalAlign="Right" />
                                <PagerStyle BackColor="#aba392" ForeColor="#333333" HorizontalAlign="Right" />
                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" CssClass="GV_headerREP" />
                                <EditRowStyle BackColor="#999999" />
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" CssClass="GV_rowpadREP" />
                            </asp:GridView><br />
                            <div style="width: 674px;" class="theader2">PACIENTES SIN ARV 25-49</div>
                            <asp:GridView ID="GV_rnoarv25_49" runat="server" 
                                CellPadding="1" 
                                ForeColor="#333333" 
                                GridLines="both" BorderColor="#a0acc0" BorderStyle="solid" BorderWidth="1px"
                                Width="680px" ShowFooter="False" AutoGenerateColumns="False" RowStyle-Wrap="false" HeaderStyle-Wrap="false" >
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" CssClass="GV_rowpadREP" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Género">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_NomGenero" runat="server" Text='<%# Bind("NomGenero") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="80px" HorizontalAlign="Left" CssClass="GV_rowpad" />
                                        <HeaderStyle Width="80px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Nuevos">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Nuevos" runat="server" Text='<%# Bind("Nuevos") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        <HeaderStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Reingresos">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Reingresos" runat="server" Text='<%# Bind("Reingresos") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        <HeaderStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Abandonos">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Abandonos" runat="server" Text='<%# Bind("Abandonos") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        <HeaderStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Fallecidos">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Fallecidos" runat="server" Text='<%# Bind("Fallecidos") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        <HeaderStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Traslados">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Traslados" runat="server" Text='<%# Bind("Traslados") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        <HeaderStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="- Edad">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_SalenCambioEdad" runat="server" Text='<%# Bind("SalenCambioEdad") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        <HeaderStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="+ Edad">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_EntranCambioEdad" runat="server" Text='<%# Bind("EntranCambioEdad") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        <HeaderStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Inician TARV">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_InicianTARV" runat="server" Text='<%# Bind("InicianTARV") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        <HeaderStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" CssClass="GV_headerREP" HorizontalAlign="Right" />
                                <PagerStyle BackColor="#aba392" ForeColor="#333333" HorizontalAlign="Right" />
                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" CssClass="GV_headerREP" />
                                <EditRowStyle BackColor="#999999" />
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" CssClass="GV_rowpadREP" />
                            </asp:GridView><br />
                            <div style="width: 674px;" class="theader2">PACIENTES SIN ARV >50</div>
                            <asp:GridView ID="GV_rnoarv50" runat="server" 
                                CellPadding="1" 
                                ForeColor="#333333" 
                                GridLines="both" BorderColor="#a0acc0" BorderStyle="solid" BorderWidth="1px"
                                Width="680px" ShowFooter="False" AutoGenerateColumns="False" RowStyle-Wrap="false" HeaderStyle-Wrap="false" >
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" CssClass="GV_rowpadREP" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Género">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_NomGenero" runat="server" Text='<%# Bind("NomGenero") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="80px" HorizontalAlign="Left" CssClass="GV_rowpad" />
                                        <HeaderStyle Width="80px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Nuevos">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Nuevos" runat="server" Text='<%# Bind("Nuevos") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        <HeaderStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Reingresos">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Reingresos" runat="server" Text='<%# Bind("Reingresos") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        <HeaderStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Abandonos">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Abandonos" runat="server" Text='<%# Bind("Abandonos") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        <HeaderStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Fallecidos">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Fallecidos" runat="server" Text='<%# Bind("Fallecidos") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        <HeaderStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Traslados">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Traslados" runat="server" Text='<%# Bind("Traslados") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        <HeaderStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="- Edad">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_SalenCambioEdad" runat="server" Text='<%# Bind("SalenCambioEdad") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        <HeaderStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="+ Edad">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_EntranCambioEdad" runat="server" Text='<%# Bind("EntranCambioEdad") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        <HeaderStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Inician TARV">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_InicianTARV" runat="server" Text='<%# Bind("InicianTARV") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        <HeaderStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" CssClass="GV_headerREP" HorizontalAlign="Right" />
                                <PagerStyle BackColor="#aba392" ForeColor="#333333" HorizontalAlign="Right" />
                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" CssClass="GV_headerREP" />
                                <EditRowStyle BackColor="#999999" />
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" CssClass="GV_rowpadREP" />
                            </asp:GridView><br />
                            <div style="width: 674px;" class="theader2">PACIENTES SIN ARV TOTAL</div>
                            <asp:GridView ID="GV_rnoarvTotal" runat="server" 
                                CellPadding="1" 
                                ForeColor="#333333" 
                                GridLines="both" BorderColor="#a0acc0" BorderStyle="solid" BorderWidth="1px"
                                Width="680px" ShowFooter="False" AutoGenerateColumns="False" RowStyle-Wrap="false" HeaderStyle-Wrap="false" >
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" CssClass="GV_rowpadREP" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Género">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_NomGenero" runat="server" Text='<%# Bind("NomGenero") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="230px" HorizontalAlign="Left" CssClass="GV_rowpad" />
                                        <HeaderStyle Width="230px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Nuevos">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Nuevos" runat="server" Text='<%# Bind("Nuevos") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        <HeaderStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Reingresos">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Reingresos" runat="server" Text='<%# Bind("Reingresos") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        <HeaderStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Abandonos">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Abandonos" runat="server" Text='<%# Bind("Abandonos") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        <HeaderStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Fallecidos">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Fallecidos" runat="server" Text='<%# Bind("Fallecidos") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        <HeaderStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Traslados">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Traslados" runat="server" Text='<%# Bind("Traslados") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        <HeaderStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Inician TARV">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_InicianTARV" runat="server" Text='<%# Bind("InicianTARV") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        <HeaderStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" CssClass="GV_headerREP" HorizontalAlign="Right" />
                                <PagerStyle BackColor="#aba392" ForeColor="#333333" HorizontalAlign="Right" />
                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" CssClass="GV_headerREP" />
                                <EditRowStyle BackColor="#999999" />
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" CssClass="GV_rowpadREP" />
                            </asp:GridView><br />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btn_grabar" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
        </center>
    </div>
</asp:Content>
