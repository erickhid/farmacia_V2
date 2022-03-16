<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="RSigpro.aspx.vb" EnableSessionState="True" Inherits="RSigpro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div style="width: 700px; border: solid 1px #5d7b9d; text-align:left;">
        <table id="tblbasal" border="0" cellpadding="2" cellspacing="1">
            <tr>
                <th colspan="7" class="theader">
                    REPORTE MENSUAL SIGPRO</th>
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
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div style="width: 694px;" class="theader2"><asp:Label ID="lbl_titulo" runat="server"></asp:Label></div>
                <asp:Panel ID="pnl_reporte" runat="server" BorderColor="#5d7b9d" BorderStyle="solid" BorderWidth="0px" ScrollBars="Auto" Width="698px">
                    <table id="tbl_reporte" runat="server" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <asp:GridView ID="GV_rsigpro" runat="server" CellPadding="1" ForeColor="#333333" 
                                    GridLines="both" BorderColor="#a0acc0" BorderStyle="solid" BorderWidth="1px"
                                    Width="1600px" ShowFooter="true" AutoGenerateColumns="False" >
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" CssClass="GV_rowpadREP" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="ID">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_ID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="20px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="20px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ESQUEMA">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_ESQUEMA" runat="server" Text='<%# Bind("ESQUEMA") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="300px" HorizontalAlign="Left" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="300px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="M10-14">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_M1014" runat="server" Text='<%# Bind("M1014") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="40px" HorizontalAlign="Right" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="F10-14">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_F1014" runat="server" Text='<%# Bind("F1014") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="40px" HorizontalAlign="Right" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="T10-14">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_T1014" runat="server" Text='<%# Bind("T1014") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="40px" HorizontalAlign="Right" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="M15-18">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_M1518" runat="server" Text='<%# Bind("M1518")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="40px" HorizontalAlign="Right" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="F15-18">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_F1518" runat="server" Text='<%# Bind("F1518")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="40px" HorizontalAlign="Right" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="T15-18">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_T1518" runat="server" Text='<%# Bind("T1518")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="40px" HorizontalAlign="Right" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="M19-24">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_M1924" runat="server" Text='<%# Bind("M1924")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="40px" HorizontalAlign="Right" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="F19-24">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_F1924" runat="server" Text='<%# Bind("F1924")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="40px" HorizontalAlign="Right" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="T19-24">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_T1924" runat="server" Text='<%# Bind("T1924")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="40px" HorizontalAlign="Right" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="M25-49">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_M2549" runat="server" Text='<%# Bind("M2549") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="40px" HorizontalAlign="Right" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="F25-49">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_F2549" runat="server" Text='<%# Bind("F2549") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="40px" HorizontalAlign="Right" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="T25-49">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_T2549" runat="server" Text='<%# Bind("T2549") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="40px" HorizontalAlign="Right" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="M>50">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_M50" runat="server" Text='<%# Bind("M50") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="40px" HorizontalAlign="Right" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="F>50">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_F50" runat="server" Text='<%# Bind("F50") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="40px" HorizontalAlign="Right" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="T>50">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_T50" runat="server" Text='<%# Bind("T50") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="40px" HorizontalAlign="Right" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="MT">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_MT" runat="server" Text='<%# Bind("MT") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="40px" HorizontalAlign="Right" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="FT">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_FT" runat="server" Text='<%# Bind("FT") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="40px" HorizontalAlign="Right" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="TT">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_TT" runat="server" Text='<%# Bind("TT") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="40px" HorizontalAlign="Right" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ST">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_SUBT1" runat="server" Text='<%# Bind("SUBT1") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="40px" HorizontalAlign="Right" Font-Bold="true" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PP10-14">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_PP1014" runat="server" Text='<%# Bind("PP1014")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="40px" HorizontalAlign="Right" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PP15-18">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_PP1518" runat="server" Text='<%# Bind("PP1518")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="40px" HorizontalAlign="Right" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="PP19-24">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_PP1924" runat="server" Text='<%# Bind("PP1924") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="40px" HorizontalAlign="Right" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PP25-49">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_PP2549" runat="server" Text='<%# Bind("PP2549") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="40px" HorizontalAlign="Right" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PP>50">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_PP50" runat="server" Text='<%# Bind("PP50") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="40px" HorizontalAlign="Right" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="STPP">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_SUBT2" runat="server" Text='<%# Bind("SUBT2") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="40px" HorizontalAlign="Right" Font-Bold="true" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="EMB10-14">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_EMB1014" runat="server" Text='<%# Bind("EMB1014")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="45px" HorizontalAlign="Right" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="45px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="EMB15-18">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_EMB1518" runat="server" Text='<%# Bind("EMB1518")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="45px" HorizontalAlign="Right" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="45px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="EMB19-24">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_EMB1924" runat="server" Text='<%# Bind("EMB1924")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="45px" HorizontalAlign="Right" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="45px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="EMB25-49">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_EMB2549" runat="server" Text='<%# Bind("EMB2549") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="45px" HorizontalAlign="Right" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="45px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="EMB>50">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_EMB50" runat="server" Text='<%# Bind("EMB50") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="40px" HorizontalAlign="Right" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="STEMB">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_SUBT3" runat="server" Text='<%# Bind("SUBT3") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="40px" HorizontalAlign="Right" Font-Bold="true" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="TOTAL">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_TOTAL" runat="server" Text='<%# Bind("TOTAL") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="40px" HorizontalAlign="Right" Font-Bold="true" BackColor="#e9ecf1" CssClass="GV_rowpad" />
                                            <HeaderStyle Width="40px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" CssClass="GV_headerREP" HorizontalAlign="Right" />
                                    <PagerStyle BackColor="#aba392" ForeColor="#333333" HorizontalAlign="Right" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" CssClass="GV_headerREP" />
                                    <EditRowStyle BackColor="#999999" />
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" CssClass="GV_rowpadREP" />
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
