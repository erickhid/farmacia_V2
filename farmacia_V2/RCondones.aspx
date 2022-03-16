<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="RCondones.aspx.vb" Inherits="RCondones" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" contentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"> </asp:ScriptManager>
        <div style="width:700px; border:solid 1px #5d7b9d; text-align:left;">
            <table id="tblbasal" border="0" cellpadding="2" cellspacing="1">
                <tr>
                    <th colspan="11" class="theader" > 
                        Reporte Entrega Condones</th>
                </tr>
                <tr>               
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
                    <td style="width:50px; background-color:#5b7b9d; color:#ffffff;">
                        Año:
                    </td>
                    <td style="width: 100px; background-color: #e9ecf1;">
                        <asp:DropDownList ID="DLL_Año" runat="server" CssClass="datos">
                        </asp:DropDownList>
                    </td>
                    <td style="width:360px; text-align:center;">
                        <asp:Button ID="btn_Generar" runat="server" Text="GENERAR" TabIndex="45" CssClass="button" />
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
            <asp:Panel ID="pnl_reporte" runat="server" BorderColor="#5d7b9d" BorderStyle="solid" BorderWidth="0px" ScrollBars="Auto" Width="700px">
                <table id="tbl_reporte" runat="server" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode ="Conditional">
                                <ContentTemplate>
                                    <div style="text-align:center" class="theader2"><asp:Label ID="lbl_titulo" runat="server"></asp:Label></div>
                                    <asp:GridView ID="RC_reportes" runat="server"
                                        CellPadding="1"
                                        ForeColor="#333333"
                                        GridLines="both" BorderColor="#a0acc0" BorderStyle="Solid" BorderWidth="1px"
                                        With="700px" ShowFooter="False" AutoGenerateColumns="False">                                   
                                   <RowStyle BackColor="#F7F6F3" ForeColor="#333333" CssClass="GV_rowpadREP1" />
                                   <Columns>                                       
                                       <asp:TemplateField HeaderText="Fecha Entrega">
                                           <ItemTemplate>
                                               <asp:Label ID="lbl_Fecha_Entrega" runat="server" Text='<%# Bind("FechaEntrega")%>'></asp:Label>
                                           </ItemTemplate>
                                           <ItemStyle Width="150px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                           <HeaderStyle Width="150px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                       </asp:TemplateField>
                                       <asp:TemplateField HeaderText="NHC">
                                           <ItemTemplate>
                                               <asp:Label ID="lbl_NHC" runat="server" Text='<%# Bind("NHC") %>'></asp:Label>
                                           </ItemTemplate>
                                           <ItemStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                           <HeaderStyle Width="75px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                       </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Iniciales">
                                           <ItemTemplate>
                                               <asp:Label ID="lbl_Iniciales" runat="server" Text='<%# Bind("Iniciales")%>'></asp:Label>
                                           </ItemTemplate>
                                           <ItemStyle Width="60px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                           <HeaderStyle Width="60px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                       </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Genero">
                                           <ItemTemplate>
                                               <asp:Label ID="lbl_Genero" runat="server" Text='<%# Bind("Genero")%>'></asp:Label>
                                           </ItemTemplate>
                                           <ItemStyle Width="60px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                           <HeaderStyle Width="60px" HorizontalAlign="Center" CssClass="GV_rowpad" />
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
                                               <asp:Label ID="lbl_Edad" runat="server" Text='<%# Bind("Edad")%>'></asp:Label>
                                           </ItemTemplate>
                                           <ItemStyle Width="50px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                           <HeaderStyle Width="50px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                       </asp:TemplateField>
                                        <asp:TemplateField HeaderText="GeneroP">
                                           <ItemTemplate>
                                               <asp:Label ID="lbl_Genero_Pediatria" runat="server" Text='<%# Bind("Genero")%>'></asp:Label>
                                           </ItemTemplate>
                                           <ItemStyle Width="60px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                           <HeaderStyle Width="60px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                       </asp:TemplateField>
                                       <asp:TemplateField HeaderText="EdadP">
                                           <ItemTemplate>
                                               <asp:Label ID="lbl_Edad_P" runat="server" Text='<%# Bind("Edad")%>'></asp:Label>
                                           </ItemTemplate>
                                           <ItemStyle Width="50px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                           <HeaderStyle Width="50px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                       </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Condones">
                                           <ItemTemplate>
                                               <asp:Label ID="lbl_Condon" runat="server" Text='<%# Bind("Condones")%>'></asp:Label>
                                           </ItemTemplate>
                                           <ItemStyle Width="116px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                           <HeaderStyle Width="116px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                       </asp:TemplateField>
                                       <asp:TemplateField HeaderText="TotalCondon">
                                           <ItemTemplate>
                                               <asp:Label ID="lbl_TotalCondon" runat="server" Text='<%# Bind("TotalCondon")%>'></asp:Label>
                                           </ItemTemplate>
                                           <ItemStyle Width="100px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                           <HeaderStyle Width="100px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                       </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Lubricante">
                                           <ItemTemplate>
                                               <asp:Label ID="lbl_Lubricante" runat="server" Text='<%# Bind("Lubricante")%>'></asp:Label>
                                           </ItemTemplate>
                                           <ItemStyle Width="116px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                           <HeaderStyle Width="116px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                       </asp:TemplateField>
                                       <asp:TemplateField HeaderText="TotalLubricante">
                                           <ItemTemplate>
                                               <asp:Label ID="lbl_TotalLubricante" runat="server" Text='<%# Bind("TotalLubricante")%>'></asp:Label>
                                           </ItemTemplate>
                                           <ItemStyle Width="100px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                           <HeaderStyle Width="100px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                       </asp:TemplateField>
                                       <asp:TemplateField HeaderText="LubricanteTubo">
                                           <ItemTemplate>
                                               <asp:Label ID="lbl_LubricanteTubo" runat="server" Text='<%# Bind("LubricanteTubo")%>'></asp:Label>
                                           </ItemTemplate>
                                           <ItemStyle Width="116px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                           <HeaderStyle Width="116px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                       </asp:TemplateField>
                                       <asp:TemplateField HeaderText="TotalLubricanteTubo">
                                           <ItemTemplate>
                                               <asp:Label ID="lbl_TotalLubricanteTubo" runat="server" Text='<%# Bind("TotalLubricanteTubo")%>'></asp:Label>
                                           </ItemTemplate>
                                           <ItemStyle Width="100px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                           <HeaderStyle Width="100px" HorizontalAlign="Center" CssClass="GV_rowpad" />
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
