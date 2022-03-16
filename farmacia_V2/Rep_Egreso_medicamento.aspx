<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Rep_Egreso_medicamento.aspx.vb" Inherits="Rep_Egreso_medicamento" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div style="width: 775px; border: solid 1px #5d7b9d; text-align:left;">
            <table id="tbl_ingresomed_inventario" border="0" cellpadding="2" cellspacing="1">
                <tr>
                    <th colspan="9" class="theader">
                        REPORTE OTROS EGRESOS DE MEDICAMENTOS</th>
                </tr>                            
                <tr>
                    <td style="width:50px; background-color:#5d7b9d; color:#ffffff;">
                        Medicamento:
                    </td>
                    <td style="width: 100px; background-color: #e9ecf1;">
                        <asp:DropDownList ID="ddl_Medicamento" runat="server" CssClass="datos">
                            <asp:ListItem Value="1">ARV</asp:ListItem>
                            <asp:ListItem Value="2">PROF</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="width:50px; background-color:#5d7b9d; color:#ffffff;">
                        Mes:
                    </td>
                    <td style="width:100px; background-color:#e9ecf1">
                        <asp:DropDownList ID="DDL_Mes" runat="server" CssClass="datos">
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
                    <td style="width:50px; background-color:#5d7b9d; color:#ffffff;">
                        Año:
                    </td>
                    <td style="width: 100px; background-color: #e9ecf1;">
                        <asp:DropDownList ID="DLL_Año" runat="server" CssClass="datos">
                        </asp:DropDownList>
                    </td>
                    <td style="width:451px; text-align:center;">
                        <asp:Button ID="btn_Generar" runat="server" Text="GENERAR" TabIndex="45" CssClass="button1" />
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
                    <td colspan="11">
                        &nbsp;<asp:Label ID="lbl_error" runat="server" CssClass="error"></asp:Label>
                    </td>
                </tr>  
            </table>
            <asp:Panel ID="pnl_reporte" runat="server" BorderColor="#5d7b9d" BorderStyle="solid" BorderWidth="0px" Height="500px"  ScrollBars="Auto" Width="100%">
                <table id="tbl_reporte" runat="server" border="0" cellpadding="0" cellspacing="0"  width="850px">
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode ="Conditional">
                                <ContentTemplate>
                                    <div style="text-align:center" class="theader2"><asp:Label ID="lbl_titulo" runat="server"></asp:Label></div>
                                    <asp:GridView ID="ROM_reportes" runat="server" CellPadding="1" ForeColor="#333333" GridLines="both" BorderColor="#a0acc0" BorderStyle="Solid" BorderWidth="1px" With="950px" ShowFooter="False" AutoGenerateColumns="False"> <RowStyle BackColor="#F7F6F3" ForeColor="#333333" CssClass="GV_rowpadREP1" />
                                        <Columns>
                                             <asp:TemplateField HeaderText="Fecha Egreso">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Fecha_Egreso" runat="server" Text='<%# Bind("Fecha_Egreso")%>'></asp:Label>
                                                </ItemTemplate>
                                                    <ItemStyle Width="110px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                                    <HeaderStyle Width="110px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                             </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Tipo Medicamento">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Tipo_Medicamento" runat="server" Text='<%# Bind("Tipo_Medicamento")%>'></asp:Label>
                                                </ItemTemplate>
                                                    <ItemStyle Width="125px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                                    <HeaderStyle Width="125px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                             </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Id FF">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_ID_FF" runat="server" Text='<%# Bind("IdFF")%>'></asp:Label>
                                                </ItemTemplate>
                                                    <ItemStyle Width="30px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                                    <HeaderStyle Width="30px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                             </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Nombre Tipo Egreso">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Nom_TipoEgreso" runat="server" Text='<%# Bind("Nom_TipoEgreso")%>'></asp:Label>
                                                </ItemTemplate>
                                                    <ItemStyle Width="125px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                                    <HeaderStyle Width="125px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                             </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Forma Farmaceutica">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_NomFF" runat="server" Text='<%# Bind("NomFF")%>'></asp:Label>
                                                </ItemTemplate>
                                                    <ItemStyle Width="150px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                                    <HeaderStyle Width="150px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                             </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Medicamento">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Medicamento" runat="server" Text='<%# Bind("Medicamento")%>'></asp:Label>
                                                </ItemTemplate>
                                                    <ItemStyle Width="250px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                                    <HeaderStyle Width="250px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                             </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cantidad">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Cantidad" runat="server" Text='<%# Bind("Cantidad")%>'></asp:Label>
                                                </ItemTemplate>
                                                    <ItemStyle Width="50px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                                    <HeaderStyle Width="50px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                             </asp:TemplateField>
                                            <asp:TemplateField HeaderText="NHC TV">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_NHC_TV" runat="server" Text='<%# Bind("NHC_TV")%>'></asp:Label>
                                                </ItemTemplate>
                                                    <ItemStyle Width="50px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                                    <HeaderStyle Width="50px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                             </asp:TemplateField>                                            
                                        </Columns>
                                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" CssClass="GV_headerREP1 " HorizontalAlign="Center"/>
                                            <PagerStyle BackColor="#aba392" ForeColor="#333333" HorizontalAlign="Right" />                                   
                                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" CssClass="GV_headerREP1" />
                                            <EditRowStyle BackColor="#999999" />
                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" CssClass="GV_headerREP1" />
                                    </asp:GridView>
                                </ContentTemplate>
                                 <Triggers>
                                       <asp:AsyncPostBackTrigger ControlID="btn_Generar" EventName="Click" />
                                   </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
</asp:Content>

