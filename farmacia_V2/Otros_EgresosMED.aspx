<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Otros_EgresosMED.aspx.vb" Inherits="Otros_EgresosMED" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
        <div style="width: 770px; border: solid 1px #5d7b9d; text-align:center;">
            <table id="tbl_otros_egresos" border="0" cellpadding="2" cellspacing="1" style="width: 770px">
                <tr style="width:770px;">
                    <th colspan="16" class="theader">OTROS EGRESOS ARV / PROFILAXIS</th>
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
                    <asp:DropDownList ID="DDL_anio" runat="server" CssClass="datos">
                    </asp:DropDownList>
                </td>
                    <td style="text-align:center;">
                    <asp:Button ID="btn_generar" runat="server" Text="GENERAR" TabIndex="45" CssClass="button" />
                </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;<asp:Label ID="lbl_error" runat="server" CssClass="error"></asp:Label>
                    </td>
                </tr>
                </table>
            </div>
        <div>
            <asp:Panel ID="pnl_EGRESOS1" runat="server">
                <table id="tbl_EGRESOS" border="0" cellpadding="0" cellspacing="1" style="width:770px;">
                    <tr>
                        <td>
                            <table id="Table8" border="0" cellpadding="0" cellspacing="0" style="width:768px;">
                                <tr>
                                    <th class="theader" style="width:20px;"><asp:ImageButton ID="ib_egresos" ImageUrl="~/images/plus2.png" runat="server" BorderWidth="0"  Height="10px" Width="10px"/></th>
                                    <th class="theader4">EGRESOS</th>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
            <div>
            <asp:Panel ID="pnl_Egresos" runat="server">
                <asp:UpdatePanel ID="up_pnl_Egresos" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table id="tblpnl_Egresos" border="0" cellpadding="0" cellspacing="0" style="width:770px;">
                            <tr>
                                <td>
                                    <asp:GridView ID="GV_pnl_Egresos" runat="server" ForeColor="#333333"
                                        EmptyDataText="No se existe ningún egreso del día de hoy." 
                                        Font-Names="Trebuchet MS" Font-Size="8pt" GridLines="None"
                                        CellPadding="0" CellSpacing="1" Width="770px" AutoGenerateColumns="False" 
                                        ShowFooter="False" DataKeyNames="Id_Otros_EgresosMed" TabIndex="3">
                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" Wrap="False" />
                                        <Columns>
                                            <asp:TemplateField ShowHeader="False" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_IdEgresoMed" runat="server" Text='<%# Bind("Id_Otros_EgresosMed")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Fecha Egreso">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_fechaegreso" runat="server" Text='<%# Bind("Fecha_Egreso")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="90px" CssClass="GV_rowpad" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Tipo Medicamento">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_tipoMed" runat="server" Text='<%# Bind("Tipo_Medicamento")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="50px" CssClass="GV_rowpad" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Código">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_codigo" runat="server" Text='<%# Bind("IdFF")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="40px" CssClass="GV_rowpad" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Medicamento ARV">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_nom_med" runat="server" Text='<%# Bind("Medicamento")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="130px" CssClass="GV_rowpad" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cantidad">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_cantidad" runat="server" Text='<%# Bind("Cantidad")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="35px" CssClass="GV_rowpad" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Tipo Egreso">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_tipo_egreso" runat="server" Text='<%# Bind("Nom_TipoEgreso")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="75px" CssClass="GV_rowpad" />
                                            </asp:TemplateField>
                                            <%--<asp:TemplateField HeaderText="Nivel Educativo">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_NivelEducativo" runat="server" Text='<%# Bind("NivelEducativo")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="158px" CssClass="GV_rowpad" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Situación Laboral">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_SituacionLaboral" runat="server" Text='<%# Bind("SituacionLaboral")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="150px" CssClass="GV_rowpad" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Ingreso">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Ingreso" runat="server" Text='<%# Bind("Ingreso")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="55px" CssClass="GV_rowpad" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Conoce Dx">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_ConoceDx" runat="server" Text='<%# Bind("NomConoceDx")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="75px" CssClass="GV_rowpad" />
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField ShowHeader="False">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ibtEditar" runat="server" ImageUrl="~/images/file_edit.png" CommandName="Editar" ToolTip="Editar" visible="false"/>
                                                    <asp:ImageButton ID="ibtBorrar" runat="server" ImageUrl="~/images/delete.png" CommandName="Borrar" ToolTip="Borrar" visible="true"/>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="right" Width="41px" CssClass="GV_rowpad" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataRowStyle Font-Bold="True" />
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                        <EditRowStyle BackColor="#999999" />
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                        <EmptyDataTemplate>
                                            <table border="0" cellspacing="1" cellpadding="0" width="770px">
                                                <tr>
                                                    <td colspan="8" style="font-weight:bold; color:#333333; text-align:Left;">No existen ingresos de hoy.</td>
                                                </tr>
                                                <tr>
                                                    <td style="background-color:#5D7B9D; font-weight:bold; color:#ffffff; text-align:center; width:90px;">Fecha Egreso</td>
                                                    <td style="background-color:#5D7B9D; font-weight:bold; color:#ffffff; text-align:center; width:50px;">Tipo Medicamento</td>
                                                    <td style="background-color:#5D7B9D; font-weight:bold; color:#ffffff; text-align:center; width:40px;">Código</td>
                                                    <td style="background-color:#5D7B9D; font-weight:bold; color:#ffffff; text-align:center; width:130px;">Medicamento</td>
                                                    <td style="background-color:#5D7B9D; font-weight:bold; color:#ffffff; text-align:center; width:35px;">Cantidad</td>
                                                    <td style="background-color:#5D7B9D; font-weight:bold; color:#ffffff; text-align:center; width:75px;">Tipo Egreso</td>
                                                    <%--<td style="background-color:#5D7B9D; font-weight:bold; color:#ffffff; text-align:center; width:35px;">Fecha Vencimiento</td>--%>
<%--                                                    <td style="background-color:#5D7B9D; font-weight:bold; color:#ffffff; text-align:center; width:162px;">Nivel Educativo</td>
                                                    <td style="background-color:#5D7B9D; font-weight:bold; color:#ffffff; text-align:center; width:150px;">Situación Laboral</td>
                                                    <td style="background-color:#5D7B9D; font-weight:bold; color:#ffffff; text-align:center; width:55px;">Ingreso</td>
                                                    <td style="background-color:#5D7B9D; font-weight:bold; color:#ffffff; text-align:center; width:75px;">Conoce Dx</td>--%>
                                                    <td style="background-color:#5D7B9D; font-weight:bold; color:#ffffff; text-align:center; width:41px;"></td>
                                                </tr>
                                            </table> 
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                    

                                    <asp:GridView ID="GV_pnl_egresos_P" runat="server" ForeColor="#333333"
                                        EmptyDataText="No se existe ningún egreso del día de hoy." 
                                        Font-Names="Trebuchet MS" Font-Size="8pt" GridLines="None"
                                        CellPadding="0" CellSpacing="1" Width="770px" AutoGenerateColumns="False" 
                                        ShowFooter="False" DataKeyNames="Id_Otros_EgresosMed" TabIndex="3">
                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" Wrap="False" />
                                        <Columns>
                                            <asp:TemplateField ShowHeader="False" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_IdEgresoMed" runat="server" Text='<%# Bind("Id_Otros_EgresosMed")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Fecha Egreso">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_fechaegreso" runat="server" Text='<%# Bind("Fecha_Egreso")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="90px" CssClass="GV_rowpad" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Tipo Medicamento">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_tipoMed" runat="server" Text='<%# Bind("Tipo_Medicamento")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="50px" CssClass="GV_rowpad" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Código">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_codigo" runat="server" Text='<%# Bind("IdFF")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="40px" CssClass="GV_rowpad" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Medicamento PROF">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_nom_med" runat="server" Text='<%# Bind("Medicamento")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="130px" CssClass="GV_rowpad" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cantidad">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_cantidad" runat="server" Text='<%# Bind("Cantidad")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="35px" CssClass="GV_rowpad" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Tipo Egreso">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_tipo_egreso" runat="server" Text='<%# Bind("Nom_TipoEgreso")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="75px" CssClass="GV_rowpad" />
                                            </asp:TemplateField>
                                            <%--<asp:TemplateField HeaderText="Nivel Educativo">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_NivelEducativo" runat="server" Text='<%# Bind("NivelEducativo")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="158px" CssClass="GV_rowpad" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Situación Laboral">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_SituacionLaboral" runat="server" Text='<%# Bind("SituacionLaboral")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="150px" CssClass="GV_rowpad" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Ingreso">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Ingreso" runat="server" Text='<%# Bind("Ingreso")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="55px" CssClass="GV_rowpad" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Conoce Dx">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_ConoceDx" runat="server" Text='<%# Bind("NomConoceDx")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="75px" CssClass="GV_rowpad" />
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField ShowHeader="False">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ibtEditar" runat="server" ImageUrl="~/images/file_edit.png" CommandName="Editar" ToolTip="Editar" Visible="false" />
                                                    <asp:ImageButton ID="ibtBorrar" runat="server" ImageUrl="~/images/delete.png" CommandName="Borrar" ToolTip="Borrar" visible="true" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="right" Width="41px" CssClass="GV_rowpad" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataRowStyle Font-Bold="True" />
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                        <EditRowStyle BackColor="#999999" />
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                        <EmptyDataTemplate>
                                            <table border="0" cellspacing="1" cellpadding="0" width="770px">
                                                <tr>
                                                    <td colspan="8" style="font-weight:bold; color:#333333; text-align:Left;">No existen ingresos de hoy.</td>
                                                </tr>
                                                <tr>
                                                     <td style="background-color:#5D7B9D; font-weight:bold; color:#ffffff; text-align:center; width:90px;">Fecha Egreso</td>
                                                    <td style="background-color:#5D7B9D; font-weight:bold; color:#ffffff; text-align:center; width:50px;">Tipo Medicamento</td>
                                                    <td style="background-color:#5D7B9D; font-weight:bold; color:#ffffff; text-align:center; width:40px;">Código</td>
                                                    <td style="background-color:#5D7B9D; font-weight:bold; color:#ffffff; text-align:center; width:130px;">Medicamento</td>
                                                    <td style="background-color:#5D7B9D; font-weight:bold; color:#ffffff; text-align:center; width:35px;">Cantidad</td>
                                                    <td style="background-color:#5D7B9D; font-weight:bold; color:#ffffff; text-align:center; width:75px;">Tipo Egreso</td>
                                                    <td style="background-color:#5D7B9D; font-weight:bold; color:#ffffff; text-align:center; width:41px;"></td>
                                                </tr>
                                            </table> 
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td style="border-top:2px solid #5d7b9d; border-bottom:2px solid #5d7b9d; background-color:#e9ecf1;">
                                    <div id="divingresoGF" runat="server" visible="true">
                                        <table border="0" cellspacing="1" cellpadding="0" width="770px">
                                            <tr>
                                                <td style="text-align:center; width:216px;" class="GV_rowpad">
<%--                                                    <asp:TextBox ID="txt_fechaingreso" runat="server" Width="60px" CssClass="texto2" TabIndex="10"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txt_fechaingreso" ValidChars="0123456789/">
                                                    </cc1:FilteredTextBoxExtender>--%>
                                     <asp:TextBox ID="txt_fe_dd" runat="server" Width="20px" CssClass="datos" MaxLength="2"
                                        TabIndex="2"></asp:TextBox>/
                                    <cc1:TextBoxWatermarkExtender ID="txt_fe_dd_TextBoxWatermarkExtender" runat="server"
                                        TargetControlID="txt_fe_dd" WatermarkText="dd">
                                    </cc1:TextBoxWatermarkExtender>
                                    <cc1:FilteredTextBoxExtender ID="txt_fe_dd_FilteredTextBoxExtender" runat="server"
                                        TargetControlID="txt_fe_dd" ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                    <asp:TextBox ID="txt_fe_mm" runat="server" Width="20px" CssClass="datos" MaxLength="2"
                                        TabIndex="3"></asp:TextBox>/
                                    <cc1:TextBoxWatermarkExtender ID="txt_fe_mm_TextBoxWatermarkExtender" runat="server"
                                        TargetControlID="txt_fe_mm" WatermarkText="mm">
                                    </cc1:TextBoxWatermarkExtender>
                                    <cc1:FilteredTextBoxExtender ID="txt_fe_mm_FilteredTextBoxExtender" runat="server"
                                        TargetControlID="txt_fe_mm" ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                    <asp:TextBox ID="txt_fe_yy" runat="server" Width="20px" CssClass="datos" MaxLength="2"
                                        TabIndex="4"></asp:TextBox>
                                    <cc1:TextBoxWatermarkExtender ID="txt_fe_yy_TextBoxWatermarkExtender" runat="server"
                                        TargetControlID="txt_fe_yy" WatermarkText="aa">
                                    </cc1:TextBoxWatermarkExtender>
                                    <cc1:FilteredTextBoxExtender ID="txt_fe_yy_FilteredTextBoxExtender" runat="server"
                                        TargetControlID="txt_fe_yy" ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                                </td>                                              
                                            
                                            
                                                <td style="text-align:center; width:106px;" class="GV_rowpad">
                                                    <asp:DropDownList ID="DDL_TIPO_MED" runat="server" CssClass="datos" 
                                                        AutoPostBack="true" TabIndex="6">
                                                        <asp:ListItem></asp:ListItem>
                                                        <asp:ListItem Value="1">ARV</asp:ListItem>
                                                        <asp:ListItem Value="2">PROFILAXIS</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="text-align:center; width:109px;" class="GV_rowpad">
                                                    <asp:DropDownList ID="DDL_ARV" runat="server" CssClass="datos" 
                                                        AutoPostBack="true" TabIndex="6" Visible="false">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="DDL_PROF" runat="server" CssClass="datos" 
                                                        AutoPostBack="true" TabIndex="7" Visible="false">
                                                    </asp:DropDownList>
                                                </td>                                                
                                                <td style="text-align:center; width:278px;" class="GV_rowpad">
                                                    <asp:Label ID="NomMedicamento" runat="server" Text=""></asp:Label>
                                                </td>
                                              
                                                
                                                <td style="text-align:center; width:84px;" class="GV_rowpad">
                                                    <asp:TextBox ID="txt_cantidadmed" runat="server" Width="40px" CssClass="texto2" TabIndex="10">0</asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="ftbe_ingresosARV" runat="server" TargetControlID="txt_cantidadmed" ValidChars="0123456789">
                                                    </cc1:FilteredTextBoxExtender>
                                                </td>     
                                                <td style="text-align:center; width:193px;" class="GV_rowpad">
                                                    <asp:DropDownList ID="DDL_TipoEgreso" runat="server" CssClass="datos" 
                                                        AutoPostBack="true" TabIndex="6" >
                                                    </asp:DropDownList>
                                                    <asp:TextBox ID="txt_NHC_TV" runat="server" Width="40px" CssClass="texto2" TabIndex="10" Visible="false" MaxLength="20"></asp:TextBox>
                                                   <%-- <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txt_NHC_TV" ValidChars="0123456789">
                                                    </cc1:FilteredTextBoxExtender>--%>
                                                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server"
                                                       TargetControlID="txt_NHC_TV" WatermarkText="NHC">
                                                     </cc1:TextBoxWatermarkExtender>
                                               
                                                </td>                                                                                                                                          
                                                <td style="text-align:right; width:50px;" class="GV_rowpad">
                                                    <asp:ImageButton ID="ibt_Agregar" runat="server" ImageUrl="~/images/add.png" OnClick="ibt_Agregar_Click" ToolTip="Agregar" style="height: 16px" />
                                                    <asp:ImageButton ID="ibt_Modificar" runat="server" ImageUrl="~/images/edit.png" OnClick="ibt_Modificar_Click" ToolTip="Modificar" Visible="false"  />
                                                    <asp:ImageButton ID="ibt_Cancelar" runat="server" ImageUrl="~/images/no.png" OnClick="ibt_Cancelar_Click" ToolTip="Cancelar"  />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                       <%-- <asp:AsyncPostBackTrigger ControlID="btn_buscar" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="txt_asi" EventName="TextChanged" />--%>
                    </Triggers>
                </asp:UpdatePanel>
            </asp:Panel>
        </div>
            <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtender3" runat="server" 
            SuppressPostBack="true" ExpandedImage="~/images/minus2.png" TargetControlID="pnl_Egresos"
            CollapseControlID="pnl_EGRESOS1" ExpandControlID="pnl_EGRESOS1" CollapsedImage="~/images/plus2.png"
            Collapsed="true" ImageControlID="ib_egresos">
        </cc1:CollapsiblePanelExtender>
</asp:Content>
