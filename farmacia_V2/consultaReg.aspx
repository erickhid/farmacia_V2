<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false"
    CodeFile="consultaReg.aspx.vb" Inherits="consultaReg" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div style="width: 700px; border: solid 1px #5d7b9d; text-align:left;">
        <table id="tblbasal" border="0" cellpadding="2" cellspacing="1">
            <tr>
                <th colspan="4" class="theader">CONSULTA DE PACIENTE</th>
            </tr>
            <tr>
                <td style="width: 100px; background-color: #5d7b9d; color: #ffffff;">
                    Número ASI:
                </td>
                <td style="width: 230px; background-color: #e9ecf1; padding:0px;">
                    <table id="tblNHC" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <asp:Label ID="lbl_asi" runat="server" CssClass="NHClbl"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 100px; background-color: #5d7b9d; color: #ffffff;">
                    Estatus:
                </td>
                <td style="width: 270px; background-color: #e9ecf1;">
                    <asp:Label ID="lbl_estatus" runat="server" CssClass="paciente"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 100px; background-color: #5d7b9d; color: #ffffff;">
                    Nombre:
                </td>
                <td style="width: 230px; background-color: #e9ecf1;">
                    <asp:Label ID="lbl_nombre" runat="server"></asp:Label>
                </td>
                <td style="width: 100px; background-color: #5d7b9d; color: #ffffff;">
                    Género:
                </td>
                <td style="width: 270px; background-color: #e9ecf1;">
                    <asp:Label ID="lbl_genero" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 100px; background-color: #5d7b9d; color: #ffffff;">
                    Fecha Nacimiento:
                </td>
                <td style="width: 230px; background-color: #e9ecf1;">
                    <asp:Label ID="lbl_nacimiento" runat="server"></asp:Label>
                </td>
                <td style="width: 100px; background-color: #5d7b9d; color: #ffffff;">
                    Teléfono:
                </td>
                <td style="width: 270px; background-color: #e9ecf1;">
                    <asp:Label ID="lbl_telefono" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 100px; background-color: #5d7b9d; color: #ffffff;">
                    Domicilio Actual:
                </td>
                <td colspan="3" style="background-color: #e9ecf1;">
                    <asp:Label ID="lbl_domicilio" runat="server"></asp:Label>
					 <asp:Label ID="lblIdCCARV" runat="server" Visible="false"></asp:Label>
                </td>
            </tr>
        </table>
        &nbsp;<asp:Label ID="lbl_error" runat="server" CssClass="error"></asp:Label>
    </div>
    <div style="width: 700px; border: solid 1px #5d7b9d; text-align:left; font-family: 'Trebuchet MS', Arial, Helvetica, sans-serif; font-size:8pt; font-weight:normal;">
        <table border="0" cellpadding="0" cellspacing="0" style="width:700px;">
            <tr>
                <td style="border: solid 1px #b5b8ba;">
                    <asp:UpdatePanel ID="UP_registros" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btn_grabar" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btn_cerrar" EventName="Click" />
                        </Triggers>
                        <ContentTemplate>
                            <div style="width:698px; height:38px; background-color:#ffffff;">
                                <table id="Table2" border="0" cellpadding="1" cellspacing="1" style="width:698px;">
                                    <tr>
                                        <th colspan="13">REGISTRO ARV's</th>
                                    </tr>
                                    <tr>
                                        <th style="background-color:#5D7B9D; font-weight:bold; color:#ffffff; width:54px;">&nbsp;</th>
                                        <th style="background-color:#5D7B9D; font-weight:bold; color:#ffffff; text-align:center; width:80px;">Entrega</th>
                                        <th style="background-color:#5D7B9D; font-weight:bold; color:#ffffff; text-align:center; width:80px;">Retorno</th>
                                        <th style="background-color:#5D7B9D; font-weight:bold; color:#ffffff; text-align:center; width:67px;">Esquema</th>
                                        <th style="background-color:#5D7B9D; font-weight:bold; color:#ffffff; text-align:center; width:67px;">Estatus</th>
                                        <th style="background-color:#5D7B9D; font-weight:bold; color:#ffffff; text-align:center; width:46px;">TTARV</th>
                                        <th style="background-color:#5D7B9D; font-weight:bold; color:#ffffff; text-align:center; width:40px;">M1C</th>
                                        <th style="background-color:#5D7B9D; font-weight:bold; color:#ffffff; text-align:center; width:40px;">M2C</th>
                                        <th style="background-color:#5D7B9D; font-weight:bold; color:#ffffff; text-align:center; width:40px;">M3C</th>
                                        <th style="background-color:#5D7B9D; font-weight:bold; color:#ffffff; text-align:center; width:40px;">M4C</th>
                                        <th style="background-color:#5D7B9D; font-weight:bold; color:#ffffff; text-align:center; width:40px;">M5C</th>
                                        <th style="background-color:#5D7B9D; font-weight:bold; color:#ffffff; text-align:center; width:40px;">M6C</th>
                                        <th style="background-color:#5D7B9D; font-weight:bold; color:#ffffff; text-align:center; width:40px;">M7C</th>
                                        <th style="background-color:#5D7B9D; font-weight:bold; color:#ffffff; text-align:center; width:40px;">M8C</th>
                                        <th style="background-color:#5D7B9D; font-weight:bold; color:#ffffff; text-align:center; width:18px;">&nbsp;</th>
                                    </tr>
                                </table>
                            </div>
                            <asp:Panel ID="Panel3" runat="server" Height="150px" Width="698px" Wrap="False" ScrollBars="Vertical" >
                                <div style="width:680px;">
                                        <asp:GridView ID="GV_regPacA" runat="server" 
                                            CellPadding="1" 
                                            ForeColor="#333333" 
                                            GridLines="none" 
                                            Width="680px" 
                                            AutoGenerateColumns="False" 
                                            ShowHeader="False" 
                                            DataKeyNames="IdCCARV,NHC" >
                                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                            <Columns>
                                                <asp:TemplateField ShowHeader="False">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="IB_editar" runat="server" CausesValidation="False" 
                                                            CommandName="Editar" ImageUrl="~/images/file_edit.png" 
                                                            ToolTip="Editar" CssClass="cursor" Height="16px" TabIndex="100" />
                                            
                                                        <asp:ImageButton ID="IB_eliminar" runat="server" CausesValidation="False" 
                                                            CommandName="Eliminar" ImageUrl="~/images/delete.png" 
                                                            ToolTip="Eliminar" CssClass="cursor" Height="16px" TabIndex="100" 
                                                            OnClientClick="return confirm('¿Seguro desea eliminar el registro?');" 
                                                            />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="36px" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Entrega">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_FechaEntrega" runat="server" Text='<%#Bind("FechaEntrega","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                                                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Retorno">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_FechaRetorno" runat="server" Text='<%# Bind("FechaRetorno","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                                                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Esquema">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_SCodigo" runat="server" Text='<%# Bind("SCodigo") %>'  ToolTip='<%# Eval("Descripcion").ToString()%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="65px" HorizontalAlign="Center" />
                                                    <HeaderStyle Width="65px" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Estatus">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_EsquemaEstatus" runat="server" Text='<%# Bind("EsquemaEstatus") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="65px" HorizontalAlign="Center" />
                                                    <HeaderStyle Width="65px" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="TTARV">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_TiempoTARV" runat="server" Text='<%# Bind("TiempoTARV") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="46px" HorizontalAlign="Center" />
                                                    <HeaderStyle Width="46px" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="M1C">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_M1C" runat="server" Text='<%# Bind("M1C") %>' ToolTip='<%# tip(Eval("M1E").ToString(),Eval("M1N").ToString())%>' CssClass="cursor" ></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="40px" HorizontalAlign="Center" />
                                                    <HeaderStyle Width="40px" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="M1C">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_M2C" runat="server" Text='<%# Bind("M2C") %>' ToolTip='<%# tip(Eval("M2E").ToString(),Eval("M2N").ToString())%>' CssClass="cursor" ></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="40px" HorizontalAlign="Center" />
                                                    <HeaderStyle Width="40px" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="M1C">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_M3C" runat="server" Text='<%# Bind("M3C") %>' ToolTip='<%# tip(Eval("M3E").ToString(),Eval("M3N").ToString())%>' CssClass="cursor" ></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="40px" HorizontalAlign="Center" />
                                                    <HeaderStyle Width="40px" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="M1C">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_M4C" runat="server" Text='<%# Bind("M4C") %>' ToolTip='<%# tip(Eval("M4E").ToString(),Eval("M4N").ToString())%>' CssClass="cursor" ></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="40px" HorizontalAlign="Center" />
                                                    <HeaderStyle Width="40px" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="M1C">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_M5C" runat="server" Text='<%# Bind("M5C") %>' ToolTip='<%# tip(Eval("M5E").ToString(),Eval("M5N").ToString())%>' CssClass="cursor" ></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="40px" HorizontalAlign="Center" />
                                                    <HeaderStyle Width="40px" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="M1C">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_M6C" runat="server" Text='<%# Bind("M6C") %>' ToolTip='<%# tip(Eval("M6E").ToString(),Eval("M6N").ToString())%>' CssClass="cursor" ></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="40px" HorizontalAlign="Center" />
                                                    <HeaderStyle Width="40px" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="M1C">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_M7C" runat="server" Text='<%# Bind("M7C") %>' ToolTip='<%# tip(Eval("M7E").ToString(),Eval("M7N").ToString())%>' CssClass="cursor" ></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="40px" HorizontalAlign="Center"/>
                                                    <HeaderStyle Width="40px" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="M1C">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_M8C" runat="server" Text='<%# Bind("M8C") %>' ToolTip='<%# tip(Eval("M8E").ToString(),Eval("M8N").ToString())%>' CssClass="cursor" ></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="40px" HorizontalAlign="Center" />
                                                    <HeaderStyle Width="40px" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#aba392" ForeColor="#333333" HorizontalAlign="Right" />
                                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                            <EditRowStyle BackColor="#999999" />
                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                        </asp:GridView>
                                </div>
                            </asp:Panel>
                            <div style="width:698px; height:38px; background-color:#ffffff;">
                                <table id="Table9" border="0" cellpadding="1" cellspacing="1" style="width:698px;">
                                    <tr>
                                        <th colspan="9">REGISTRO PROFILAXIS</th>
                                    </tr>
                                    <tr>
                                        <th style="background-color:#5D7B9D; font-weight:bold; color:#ffffff; width:43px;">&nbsp;</th>
                                        <th style="background-color:#5D7B9D; font-weight:bold; color:#ffffff; text-align:center; width:90px;">Entrega</th>
                                        <th style="background-color:#5D7B9D; font-weight:bold; color:#ffffff; text-align:center; width:92px;">Prof1</th>
                                        <th style="background-color:#5D7B9D; font-weight:bold; color:#ffffff; text-align:center; width:92px;">Prof2</th>
                                        <th style="background-color:#5D7B9D; font-weight:bold; color:#ffffff; text-align:center; width:92px;">Prof3</th>
                                        <th style="background-color:#5D7B9D; font-weight:bold; color:#ffffff; text-align:center; width:92px;">Prof4</th>
                                        <th style="background-color:#5D7B9D; font-weight:bold; color:#ffffff; text-align:center; width:92px;">Prof5</th>
                                        <th style="background-color:#5D7B9D; font-weight:bold; color:#ffffff; text-align:center; width:92px;">Prof6</th>
                                        <th style="background-color:#5D7B9D; font-weight:bold; color:#ffffff; text-align:center; width:18px;">&nbsp;</th>
                                    </tr>
                                </table>
                            </div>
                            <asp:Panel ID="Panel10" runat="server" Height="150px" Width="698px" Wrap="False" ScrollBars="Vertical" >
                                <div style="width:680px;">
                                        <asp:GridView ID="GV_regPacP" runat="server" 
                                            CellPadding="0" 
                                            ForeColor="#333333" 
                                            GridLines="none" 
                                            Width="680px" 
                                            AutoGenerateColumns="False" 
                                            ShowHeader="False" 
                                            DataKeyNames="IdCCProf,NHC" >
                                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                            <Columns>
                                                <asp:TemplateField ShowHeader="False">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="IB_editar" runat="server" CausesValidation="False" 
                                                            CommandName="Editar" ImageUrl="~/images/file_edit.png" 
                                                            ToolTip="Editar" CssClass="cursor" Height="16px" TabIndex="100" />
                                                     
                                                        <asp:ImageButton ID="IB_eliminar" runat="server" CausesValidation="False" 
                                                            CommandName="Eliminar" ImageUrl="~/images/delete.png" 
                                                            ToolTip="Eliminar" CssClass="cursor" Height="16px" TabIndex="100" 
                                                            OnClientClick="return confirm('¿Seguro desea eliminar el registro?');" 
                                                            />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="36px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                                    <HeaderStyle Width="36px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Entrega">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_FechaEntrega" runat="server" Text='<%#Bind("FechaEntrega","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="90px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                                    <HeaderStyle Width="90px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                                </asp:TemplateField>
                                                <asp:TemplateField ShowHeader="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_P1" runat="server" Text='<%# Bind("P1") %>' ToolTip='<%# tip(Eval("P1E").ToString(),Eval("P1C").ToString())%>' CssClass="cursor" ></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="92px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="GV_rowpad" />
                                                </asp:TemplateField>
                                                <asp:TemplateField ShowHeader="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_P2" runat="server" Text='<%# Bind("P2") %>' ToolTip='<%# tip(Eval("P2E").ToString(),Eval("P2C").ToString())%>' CssClass="cursor" ></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="92px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="GV_rowpad" />
                                                </asp:TemplateField>
                                                <asp:TemplateField ShowHeader="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_P3" runat="server" Text='<%# Bind("P3") %>' ToolTip='<%# tip(Eval("P3E").ToString(),Eval("P3C").ToString())%>' CssClass="cursor" ></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="92px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="GV_rowpad" />
                                                </asp:TemplateField>
                                                <asp:TemplateField ShowHeader="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_P4" runat="server" Text='<%# Bind("P4") %>' ToolTip='<%# tip(Eval("P4E").ToString(),Eval("P4C").ToString())%>' CssClass="cursor" ></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="92px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="GV_rowpad" />
                                                </asp:TemplateField>
                                                <asp:TemplateField ShowHeader="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_P5" runat="server" Text='<%# Bind("P5") %>' ToolTip='<%# tip(Eval("P5E").ToString(),Eval("P5C").ToString())%>' CssClass="cursor" ></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="92px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="GV_rowpad" />
                                                </asp:TemplateField>
                                                <asp:TemplateField ShowHeader="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_P6" runat="server" Text='<%# Bind("P6") %>' ToolTip='<%# tip(Eval("P6E").ToString(),Eval("P6C").ToString())%>' CssClass="cursor" ></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="92px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="GV_rowpad" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#aba392" ForeColor="#333333" HorizontalAlign="Right" />
                                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                            <EditRowStyle BackColor="#999999" />
                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                        </asp:GridView>
                                    </div>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <asp:UpdatePanel ID="UP_Nuevo" runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btn_editar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btn_grabar" EventName="Click" />
        </Triggers>
        <ContentTemplate>
            <asp:Button ID="BtnShowPopup" runat="server" style="display:none;" />
            <cc1:ModalPopupExtender ID="MPERegistro" runat="server"
                TargetControlID="BtnShowPopup"
                PopupControlID="PnlPopup" 
                BehaviorID="poppop" 
                BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="PnlPopup" runat="server" CssClass="modalPopup" style="display:block;">
                <asp:UpdatePanel ID="UpdPnlDetalle" runat="server">
                    <ContentTemplate>
                        <div style="background-color:#ffffff; width:700px; border: solid 1px #888888; padding:5px; font-family: 'trebuchet MS';	font-size: 8pt;	font-weight: normal;">
                            <div id="divARV" runat="server">
                                <table border="0" cellpadding="0" cellspacing="1">
                                    <tr>
                                        <td>
                                            <div style="border: solid 1px #5d7b9d; width: 700px; text-align:left; background-color:#e9ecf1;">
                                                <table border="0" cellpadding="2" cellspacing="1">
                                                    <tr>
                                                        <td style="width:85px;">
                                                            Fecha Entrega:
                                                        </td>
                                                        <td style="width:115px;">
                                                            <asp:TextBox ID="lbl_fe" runat="server"  CssClass="lbl_fe" placeholder="dd/mm/yyyy"  Enabled="false"></asp:TextBox></asp:Label><asp:Label ID="lbl_id" runat="server" CssClass="datoslbl" Visible="false"></asp:Label><asp:Label ID="lbl_usuario" runat="server" CssClass="datoslbl" Visible="false"></asp:Label>
                                                        </td>
                                                        <td style="width:500px;">
                                                            <div style="text-align:right; width:500px;">
                                                                <table border="0" cellpadding="0" cellspacing="0">
                                                                    <tr>
                                                                        <td style="padding:5px 5px 5px 0px; text-align:right; width:495px;">
                                                                            <asp:Button ID="btn_editar" runat="server" Text="EDITAR" CssClass="button" />
                                                                            <asp:Button ID="btn_grabar" runat="server" CssClass="button" Text="GRABAR" />
                                                                            <asp:Button ID="btn_cancelar" runat="server" CssClass="button" Text="CANCELAR" />
                                                                            <asp:Button ID="btn_cerrar" runat="server" CssClass="button2" Text="X" ToolTip="CERRAR" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div style="border: solid 1px #5d7b9d; width: 700px; text-align:left;">
                                                <table border="0" cellpadding="2" cellspacing="1">
                                                    <tr>
                                                        <td>
                                                            <b>ESQUEMA:</b>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="DDL_esquema" runat="server" CssClass="datos" TabIndex="12" AutoPostBack="True">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="DDL_sesquema" runat="server" CssClass="datos" TabIndex="13" AutoPostBack="True">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            Estatus:
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="DDL_esquemaestatus" runat="server" CssClass="datos" TabIndex="13">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lbl_esquema" runat="server" CssClass="datos2"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div style="border: solid 1px #5d7b9d; width:700px;">
                                            <table border="0" cellpadding="2" cellspacing="1" style="width: 700px;">
                                                <tr>
                                                    <th colspan="8" style="background-color: #5d7b9d; color: #ffffff;">
                                                        MEDICAMENTOS
                                                    </th>
                                                </tr>
                                                <tr>
                                                    <td style="background-color: #e9ecf1; text-align:center;" colspan="2">
                                                        Código
                                                    </td>
                                                    <td style="background-color: #e9ecf1; text-align:center;">
                                                        Cantidad
                                                    </td>
                                                    <td style="background-color: #e9ecf1; text-align:center;">
                                                        Dósis
                                                    </td>
                                                    <td style="background-color: #e9ecf1; text-align:center;">
                                                        Frecuencia
                                                    </td>
                                                    <td style="background-color: #e9ecf1; text-align:center;">
                                                        Extras
                                                    </td>
                                                    <td style="background-color: #e9ecf1; text-align:center;">
                                                        Estatus
                                                    </td>
                                                    <td style="background-color: #e9ecf1; text-align:center;">
                                                        Devolución
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <span class="numeracion">#1</span>
                                                    </td>
                                                    <td style="text-align:center;">
                                                        <asp:DropDownList ID="DDL_cod1" runat="server" CssClass="datos" TabIndex="14" 
                                                            AutoPostBack="True">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="text-align:center;">
                                                        <asp:TextBox ID="txt_cant1" runat="server" CssClass="datos" MaxLength="3" TabIndex="15"
                                                            Width="25px"></asp:TextBox><asp:Label ID="lbl_cantidad1" runat="server" Text="" Visible="false"></asp:Label>
                                                        <cc1:FilteredTextBoxExtender ID="txt_cant1_FilteredTextBoxExtender" runat="server"
                                                            TargetControlID="txt_cant1" ValidChars="0123456789">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </td>
                                                    <td style="text-align:center;">
                                                        <asp:TextBox ID="txt_dx1" runat="server" CssClass="datos" MaxLength="10" TabIndex="16"
                                                            Width="65px"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="txt_dx1_FilteredTextBoxExtender" runat="server"
                                                            TargetControlID="txt_dx1" ValidChars="0123456789/">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </td>
                                                    <td style="text-align:center;">
                                                        <asp:DropDownList ID="DDL_fx1" runat="server" CssClass="datos" TabIndex="17">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="text-align:center;">
                                                        <asp:TextBox ID="txt_uecant1" runat="server" CssClass="datos" MaxLength="3" TabIndex="52"
                                                            Width="25px"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="txt_uecant1_FilteredTextBoxExtender" runat="server"
                                                            TargetControlID="txt_uecant1" ValidChars="0123456789">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </td>
                                                    <td style="text-align:center;">
                                                        <asp:DropDownList ID="DDL_earv1" runat="server" CssClass="datos" TabIndex="62">
                                                        </asp:DropDownList>
                                                    
                                                    </td>
                                                    <td style="text-align:center;">
                                                        <asp:TextBox ID="txt_devcant1" runat="server" CssClass="datos" MaxLength="3" TabIndex="2"
                                                            Width="25px"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="txt_devcant1_FilteredTextBoxExtender" runat="server"
                                                            TargetControlID="txt_devcant1" ValidChars="0123456789">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <span class="numeracion">#2</span>
                                                    </td>
                                                    <td style="text-align:center;">
                                                        <asp:DropDownList ID="DDL_cod2" runat="server" CssClass="datos" TabIndex="18" AutoPostBack="True">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="text-align:center;">
                                                        <asp:TextBox ID="txt_cant2" runat="server" CssClass="datos" MaxLength="3" TabIndex="19"
                                                            Width="25px"></asp:TextBox><asp:Label ID="lbl_cantidad2" runat="server" Text="" Visible="false"></asp:Label>
                                                        <cc1:FilteredTextBoxExtender ID="txt_cant2_FilteredTextBoxExtender" runat="server"
                                                            TargetControlID="txt_cant2" ValidChars="0123456789">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </td>
                                                    <td style="text-align:center;">
                                                        <asp:TextBox ID="txt_dx2" runat="server" CssClass="datos" MaxLength="10" TabIndex="20"
                                                            Width="65px"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="txt_dx2_FilteredTextBoxExtender" runat="server"
                                                            TargetControlID="txt_dx2" ValidChars="0123456789/">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </td>
                                                    <td style="text-align:center;">
                                                        <asp:DropDownList ID="DDL_fx2" runat="server" CssClass="datos" TabIndex="21">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="text-align:center;">
                                                        <asp:TextBox ID="txt_uecant2" runat="server" CssClass="datos" MaxLength="3" TabIndex="53"
                                                            Width="25px"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="txt_uecant2_FilteredTextBoxExtender" runat="server"
                                                            TargetControlID="txt_uecant2" ValidChars="0123456789">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </td>
                                                    <td style="text-align:center;">
                                                        <asp:DropDownList ID="DDL_earv2" runat="server" CssClass="datos" TabIndex="63">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="text-align:center;">
                                                        <asp:TextBox ID="txt_devcant2" runat="server" CssClass="datos" MaxLength="3" TabIndex="3"
                                                            Width="25px"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="txt_devcant2_FilteredTextBoxExtender" runat="server"
                                                            TargetControlID="txt_devcant2" ValidChars="0123456789">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <span class="numeracion">#3</span>
                                                    </td>
                                                    <td style="text-align:center;">
                                                        <asp:DropDownList ID="DDL_cod3" runat="server" CssClass="datos" TabIndex="22" AutoPostBack="True">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="text-align:center;">
                                                        <asp:TextBox ID="txt_cant3" runat="server" CssClass="datos" MaxLength="3" TabIndex="23"
                                                            Width="25px"></asp:TextBox><asp:Label ID="lbl_cantidad3" runat="server" Text="" Visible="false"></asp:Label>
                                                        <cc1:FilteredTextBoxExtender ID="txt_cant3_FilteredTextBoxExtender" runat="server"
                                                            TargetControlID="txt_cant3" ValidChars="0123456789">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </td>
                                                    <td style="text-align:center;">
                                                        <asp:TextBox ID="txt_dx3" runat="server" CssClass="datos" MaxLength="10" TabIndex="24"
                                                            Width="65px"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="txt_dx3_FilteredTextBoxExtender" runat="server"
                                                            TargetControlID="txt_dx3" ValidChars="0123456789/">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </td>
                                                    <td style="text-align:center;">
                                                        <asp:DropDownList ID="DDL_fx3" runat="server" CssClass="datos" TabIndex="25">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="text-align:center;">
                                                        <asp:TextBox ID="txt_uecant3" runat="server" CssClass="datos" MaxLength="3" TabIndex="54"
                                                            Width="25px"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="txt_uecant3_FilteredTextBoxExtender" runat="server"
                                                            TargetControlID="txt_uecant3" ValidChars="0123456789">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </td>
                                                    <td style="text-align:center;">
                                                        <asp:DropDownList ID="DDL_earv3" runat="server" CssClass="datos" TabIndex="64">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="text-align:center;">
                                                        <asp:TextBox ID="txt_devcant3" runat="server" CssClass="datos" MaxLength="3" TabIndex="4"
                                                            Width="25px"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="txt_devcant3_FilteredTextBoxExtender" runat="server"
                                                            TargetControlID="txt_devcant3" ValidChars="0123456789">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <span class="numeracion">#4</span>
                                                    </td>
                                                    <td style="text-align:center;">
                                                        <asp:DropDownList ID="DDL_cod4" runat="server" CssClass="datos" TabIndex="26" AutoPostBack="True">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="text-align:center;">
                                                        <asp:TextBox ID="txt_cant4" runat="server" CssClass="datos" MaxLength="3" TabIndex="27"
                                                            Width="25px"></asp:TextBox><asp:Label ID="lbl_cantidad4" runat="server" Text="" Visible="false"></asp:Label>
                                                        <cc1:FilteredTextBoxExtender ID="txt_cant4_FilteredTextBoxExtender" runat="server"
                                                            TargetControlID="txt_cant4" ValidChars="0123456789">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </td>
                                                    <td style="text-align:center;">
                                                        <asp:TextBox ID="txt_dx4" runat="server" CssClass="datos" MaxLength="10" TabIndex="28"
                                                            Width="65px"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="txt_dx4_FilteredTextBoxExtender" runat="server"
                                                            TargetControlID="txt_dx4" ValidChars="0123456789/">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </td>
                                                    <td style="text-align:center;">
                                                        <asp:DropDownList ID="DDL_fx4" runat="server" CssClass="datos" TabIndex="29">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="text-align:center;">
                                                        <asp:TextBox ID="txt_uecant4" runat="server" CssClass="datos" MaxLength="3" TabIndex="55"
                                                            Width="25px"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="txt_uecant4_FilteredTextBoxExtender" runat="server"
                                                            TargetControlID="txt_uecant4" ValidChars="0123456789">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </td>
                                                    <td style="text-align:center;">
                                                        <asp:DropDownList ID="DDL_earv4" runat="server" CssClass="datos" TabIndex="65">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="text-align:center;">
                                                        <asp:TextBox ID="txt_devcant4" runat="server" CssClass="datos" MaxLength="3" TabIndex="5"
                                                            Width="25px"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="txt_devcant4_FilteredTextBoxExtender" runat="server"
                                                            TargetControlID="txt_devcant4" ValidChars="0123456789">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <span class="numeracion">#5</span>
                                                    </td>
                                                    <td style="text-align:center;">
                                                        <asp:DropDownList ID="DDL_cod5" runat="server" CssClass="datos" TabIndex="30" AutoPostBack="True">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="text-align:center;">
                                                        <asp:TextBox ID="txt_cant5" runat="server" CssClass="datos" MaxLength="3" TabIndex="31"
                                                            Width="25px"></asp:TextBox><asp:Label ID="lbl_cantidad5" runat="server" Text="" Visible="false"></asp:Label>
                                                        <cc1:FilteredTextBoxExtender ID="txt_cant5_FilteredTextBoxExtender" runat="server"
                                                            TargetControlID="txt_cant5" ValidChars="0123456789">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </td>
                                                    <td style="text-align:center;">
                                                        <asp:TextBox ID="txt_dx5" runat="server" CssClass="datos" MaxLength="10" TabIndex="32"
                                                            Width="65px"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="txt_dx5FilteredTextBoxExtender" runat="server"
                                                            TargetControlID="txt_dx5" ValidChars="0123456789/">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </td>
                                                    <td style="text-align:center;">
                                                        <asp:DropDownList ID="DDL_fx5" runat="server" CssClass="datos" TabIndex="33">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="text-align:center;">
                                                        <asp:TextBox ID="txt_uecant5" runat="server" CssClass="datos" MaxLength="3" TabIndex="56"
                                                            Width="25px"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="txt_uecant5_FilteredTextBoxExtender" runat="server"
                                                            TargetControlID="txt_uecant5" ValidChars="0123456789">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </td>
                                                    <td style="text-align:center;">
                                                        <asp:DropDownList ID="DDL_earv5" runat="server" CssClass="datos" TabIndex="66">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="text-align:center;">
                                                        <asp:TextBox ID="txt_devcant5" runat="server" CssClass="datos" MaxLength="3" TabIndex="6"
                                                            Width="25px"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="txt_devcant5_FilteredTextBoxExtender" runat="server"
                                                            TargetControlID="txt_devcant5" ValidChars="0123456789">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <span class="numeracion">#6</span>
                                                    </td>
                                                    <td style="text-align:center;">
                                                        <asp:DropDownList ID="DDL_cod6" runat="server" CssClass="datos" TabIndex="34" AutoPostBack="True">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="text-align:center;">
                                                        <asp:TextBox ID="txt_cant6" runat="server" CssClass="datos" MaxLength="3" TabIndex="35"
                                                            Width="25px"></asp:TextBox><asp:Label ID="lbl_cantidad6" runat="server" Text="" Visible="false"></asp:Label>
                                                        <cc1:FilteredTextBoxExtender ID="txt_cant6_FilteredTextBoxExtender" runat="server"
                                                            TargetControlID="txt_cant6" ValidChars="0123456789">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </td>
                                                    <td style="text-align:center;">
                                                        <asp:TextBox ID="txt_dx6" runat="server" CssClass="datos" MaxLength="10" TabIndex="36"
                                                            Width="65px"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="txt_dx6_FilteredTextBoxExtender" runat="server"
                                                            TargetControlID="txt_dx6" ValidChars="0123456789/">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </td>
                                                    <td style="text-align:center;">
                                                        <asp:DropDownList ID="DDL_fx6" runat="server" CssClass="datos" TabIndex="37">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="text-align:center;">
                                                        <asp:TextBox ID="txt_uecant6" runat="server" CssClass="datos" MaxLength="3" TabIndex="57"
                                                            Width="25px"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="txt_uecant6_FilteredTextBoxExtender" runat="server"
                                                            TargetControlID="txt_uecant6" ValidChars="0123456789">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </td>
                                                    <td style="text-align:center;">
                                                        <asp:DropDownList ID="DDL_earv6" runat="server" CssClass="datos" TabIndex="67">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="text-align:center;">
                                                        <asp:TextBox ID="txt_devcant6" runat="server" CssClass="datos" MaxLength="3" TabIndex="7"
                                                            Width="25px"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="txt_devcant6_FilteredTextBoxExtender" runat="server"
                                                            TargetControlID="txt_devcant6" ValidChars="0123456789">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <span class="numeracion">#7</span>
                                                    </td>
                                                    <td style="text-align:center;">
                                                        <asp:DropDownList ID="DDL_cod7" runat="server" CssClass="datos" TabIndex="38" AutoPostBack="True">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="text-align:center;">
                                                        <asp:TextBox ID="txt_cant7" runat="server" CssClass="datos" MaxLength="3" TabIndex="39"
                                                            Width="25px"></asp:TextBox><asp:Label ID="lbl_cantidad7" runat="server" Text="" Visible="false"></asp:Label>
                                                        <cc1:FilteredTextBoxExtender ID="txt_cant7_FilteredTextBoxExtender" runat="server"
                                                            TargetControlID="txt_cant7" ValidChars="0123456789">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </td>
                                                    <td style="text-align:center;">
                                                        <asp:TextBox ID="txt_dx7" runat="server" CssClass="datos" MaxLength="10" TabIndex="40"
                                                            Width="65px"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="txt_dx7_FilteredTextBoxExtender" runat="server"
                                                            TargetControlID="txt_dx7" ValidChars="0123456789/">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </td>
                                                    <td style="text-align:center;">
                                                        <asp:DropDownList ID="DDL_fx7" runat="server" CssClass="datos" TabIndex="41">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="text-align:center;">
                                                        <asp:TextBox ID="txt_uecant7" runat="server" CssClass="datos" MaxLength="3" TabIndex="58"
                                                            Width="25px"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="txt_uecant7_FilteredTextBoxExtender" runat="server"
                                                            TargetControlID="txt_uecant7" ValidChars="0123456789">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </td>
                                                    <td style="text-align:center;">
                                                        <asp:DropDownList ID="DDL_earv7" runat="server" CssClass="datos" TabIndex="68">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="text-align:center;">
                                                        <asp:TextBox ID="txt_devcant7" runat="server" CssClass="datos" MaxLength="3" TabIndex="8"
                                                            Width="25px"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="txt_devcant7_FilteredTextBoxExtender" runat="server"
                                                            TargetControlID="txt_devcant7" ValidChars="0123456789">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <span class="numeracion">#8</span>
                                                    </td>
                                                    <td style="text-align:center;">
                                                        <asp:DropDownList ID="DDL_cod8" runat="server" CssClass="datos" TabIndex="42" AutoPostBack="True">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="text-align:center;">
                                                        <asp:TextBox ID="txt_cant8" runat="server" CssClass="datos" MaxLength="3" TabIndex="43"
                                                            Width="25px"></asp:TextBox><asp:Label ID="lbl_cantidad8" runat="server" Text="" Visible="false"></asp:Label>
                                                        <cc1:FilteredTextBoxExtender ID="txt_cant8_FilteredTextBoxExtender" runat="server"
                                                            TargetControlID="txt_cant8" ValidChars="0123456789">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </td>
                                                    <td style="text-align:center;">
                                                        <asp:TextBox ID="txt_dx8" runat="server" CssClass="datos" MaxLength="10" TabIndex="44"
                                                            Width="65px"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="txt_dx8_FilteredTextBoxExtender" runat="server"
                                                            TargetControlID="txt_dx8" ValidChars="0123456789/">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </td>
                                                    <td style="text-align:center;">
                                                        <asp:DropDownList ID="DDL_fx8" runat="server" CssClass="datos" TabIndex="45">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="text-align:center;">
                                                        <asp:TextBox ID="txt_uecant8" runat="server" CssClass="datos" MaxLength="3" TabIndex="59"
                                                            Width="25px"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="txt_uecant8_FilteredTextBoxExtender" runat="server"
                                                            TargetControlID="txt_uecant8" ValidChars="0123456789">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </td>
                                                    <td style="text-align:center;">
                                                        <asp:DropDownList ID="DDL_earv8" runat="server" CssClass="datos" TabIndex="69">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="text-align:center;">
                                                        <asp:TextBox ID="txt_devcant8" runat="server" CssClass="datos" MaxLength="3" TabIndex="9"
                                                            Width="25px"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="txt_devcant8_FilteredTextBoxExtender" runat="server"
                                                            TargetControlID="txt_devcant8" ValidChars="0123456789">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </td>
                                                </tr>
                                            </table>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div style="border: solid 1px #5d7b9d; width: 700px; text-align:left;">
                                            <table border="0" cellpadding="2" cellspacing="1">
                                                <tr>
                                                    <td>
                                                        Fecha Retorno:
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_fr_dd" runat="server" Width="20px" CssClass="datos" MaxLength="2"
                                                            TabIndex="26"></asp:TextBox>/
                                                        <cc1:TextBoxWatermarkExtender ID="txt_fr_dd_TextBoxWatermarkExtender" runat="server"
                                                            TargetControlID="txt_fr_dd" WatermarkText="dd">
                                                        </cc1:TextBoxWatermarkExtender>
                                                        <cc1:FilteredTextBoxExtender ID="txt_fr_dd_FilteredTextBoxExtender" runat="server"
                                                            TargetControlID="txt_fr_dd" ValidChars="0123456789">
                                                        </cc1:FilteredTextBoxExtender>
                                                        <asp:TextBox ID="txt_fr_mm" runat="server" Width="20px" CssClass="datos" MaxLength="2"
                                                            TabIndex="27"></asp:TextBox>/
                                                        <cc1:TextBoxWatermarkExtender ID="txt_fr_mm_TextBoxWatermarkExtender" runat="server"
                                                            TargetControlID="txt_fr_mm" WatermarkText="mm">
                                                        </cc1:TextBoxWatermarkExtender>
                                                        <cc1:FilteredTextBoxExtender ID="txt_fr_mm_FilteredTextBoxExtender" runat="server"
                                                            TargetControlID="txt_fr_mm" ValidChars="0123456789">
                                                        </cc1:FilteredTextBoxExtender>
                                                        <asp:TextBox ID="txt_fr_yy" runat="server" Width="30px" CssClass="datos" MaxLength="4"
                                                            TabIndex="28"></asp:TextBox>
                                                        <cc1:TextBoxWatermarkExtender ID="txt_fr_yy_TextBoxWatermarkExtender2" runat="server"
                                                            TargetControlID="txt_fr_yy" WatermarkText="aa">
                                                        </cc1:TextBoxWatermarkExtender>
                                                        <cc1:FilteredTextBoxExtender ID="txt_fr_yy_FilteredTextBoxExtender" runat="server"
                                                            TargetControlID="txt_fr_yy" ValidChars="0123456789">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </td>
                                                    <td style="border-left:solid 1px #5d7b9d;">
                                                        Tiempo de TARV en Días:
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_tarvdias" runat="server" Width="20px" CssClass="datos" MaxLength="3"
                                                            TabIndex="29"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="txt_tarvdias_FilteredTextBoxExtender" runat="server"
                                                            TargetControlID="txt_tarvdias" ValidChars="0123456789">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </td>
                                                    <td style="border-left:solid 1px #5d7b9d;">
                                                        Cita:
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="CB_citaFx" runat="server" CausesValidation="false" Text="Farmacia"
                                                            TabIndex="30" />
                                                        <asp:CheckBox ID="CB_citaMx" runat="server" CausesValidation="false" Text="Médica"
                                                            TabIndex="31" />
                                                    </td>
                                                </tr>
                                            </table>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div style="border: solid 1px #5d7b9d; width: 700px; text-align:left;">
                                            <table border="0" cellpadding="2" cellspacing="1">
                                                <tr>
                                                    <td>
                                                        Embarazo:
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="DDL_embarazo" runat="server" CssClass="datos" TabIndex="36">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="border-left:solid 1px #5d7b9d;">
                                                        Tiempo de Retorno en Días:
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_retornodias" runat="server" Width="20px" CssClass="datos" MaxLength="3"
                                                            TabIndex="37"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="txt_retornodias_FilteredTextBoxExtender" runat="server"
                                                            TargetControlID="txt_retornodias" ValidChars="0123456789">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </td>
                                                </tr>
                                            </table>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div style="border: solid 1px #5d7b9d; width: 700px; text-align:left;">
                                            <table border="0" cellpadding="2" cellspacing="1" >
                                                <tr style=" vertical-align:top;">
                                                    <td>
                                                        Adherencia:<br/><br/>
                                                        Auto-Adherencia:
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_adherencia" runat="server" Width="35px" CssClass="datos" MaxLength="6"
                                                            TabIndex="6"></asp:TextBox> <br/><br/>
                                                        <cc1:FilteredTextBoxExtender ID="txt_adherencia_FilteredTextBoxExtender" runat="server" TargetControlID="txt_adherencia"
                                                            ValidChars="0123456789.">
                                                        </cc1:FilteredTextBoxExtender>
                                                            <asp:DropDownList ID="ddl_auto_adherencia" runat="server" CssClass="datos" >
                                                            </asp:DropDownList>
                                                    </td>
                                                    <td> %</td>
<%--                                                    <td style="border-left:solid 1px #5d7b9d;">
                                                        Observaciones:
                                                    </td>--%>
                                                    <td style="border-left:solid 1px #5d7b9d;">
                                                        Observaciones:
                                                        <asp:TextBox ID="txt_observaciones" runat="server" CssClass="datosM" TabIndex="42"
                                                            TextMode="MultiLine" Width="315px" Rows="3"></asp:TextBox>
                                                    </td>
                                                    <td style="border-left:solid 1px #5d7b9d;">
                                                        CD4:
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_cd4" runat="server" Width="40px" CssClass="datos" MaxLength="6"
                                                            TabIndex="43"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="txt_cd4_FilteredTextBoxExtender" runat="server"
                                                            TargetControlID="txt_cd4" ValidChars="0123456789">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </td>
                                                    <td style="border-left:solid 1px #5d7b9d;">
                                                        CV:
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_cv" runat="server" Width="65px" CssClass="datos" MaxLength="10"
                                                            TabIndex="44"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="txt_cv_FilteredTextBoxExtender" runat="server" TargetControlID="txt_cv"
                                                            ValidChars="0123456789<>BDLNbdln">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </td>
                                                </tr>
                                            </table>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div id="divProf" runat="server">
                                <table border="0" cellpadding="0" cellspacing="1">
                                    <tr>
                                        <td>
                                            <div style="border: solid 1px #5d7b9d; width: 700px; text-align:left; background-color:#e9ecf1;">
                                                <table border="0" cellpadding="2" cellspacing="1">
                                                    <tr>
                                                        <td style="width:85px;">
                                                            Fecha Entrega:
                                                        </td>
                                                        <td style="width:115px;">
                                                            <asp:TextBox ID="lbl_feP" runat="server" CssClass="lbl_fe" placeholder="dd/mm/yyyy"  Enabled="false"></asp:TextBox></asp:Label><asp:Label ID="lbl_idP" runat="server" CssClass="datoslbl" Visible="false"></asp:Label><asp:Label ID="lbl_usuarioP" runat="server" CssClass="datoslbl" Visible="false"></asp:Label>
                                                        </td>
                                                        <td style="width:500px;">
                                                            <div style="text-align:right; width:500px;">
                                                                <table border="0" cellpadding="0" cellspacing="0">
                                                                    <tr>
                                                                        <td style="padding:5px 5px 5px 0px; text-align:right; width:495px;">
                                                                            <asp:Button ID="btn_editarP" runat="server" Text="EDITAR" CssClass="button" />
                                                                            <asp:Button ID="btn_grabarP" runat="server" CssClass="button" Text="GRABAR" />
                                                                            <asp:Button ID="btn_cancelarP" runat="server" CssClass="button" Text="CANCELAR" />
                                                                            <asp:Button ID="btn_cerrarP" runat="server" CssClass="button2" Text="X" ToolTip="CERRAR" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div style="border: solid 1px #5d7b9d; width: 700px; text-align:left;">
                                                <table border="0" cellpadding="2" cellspacing="1">
                                                    <tr>
                                                        <td>
                                                            CD4:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_CD4P" runat="server" Width="40px" CssClass="datosN" MaxLength="6"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server"
                                                                TargetControlID="txt_CD4P" ValidChars="0123456789">
                                                            </cc1:FilteredTextBoxExtender>
                                                        </td>
                                                        <td style="padding:0px 2px 0px 4px;">
                                                            Tipo Paciente:
                                                        </td>
                                                        <td>
                                                            <asp:RadioButtonList ID="RBL_tipopaciente" runat="server" 
                                                                RepeatDirection="Horizontal">
                                                                <asp:ListItem Value="1" Selected="True">Ambulatorio</asp:ListItem>
                                                                <asp:ListItem Value="2">Hospitalizado</asp:ListItem>
                                                                <asp:ListItem Value="3">Emergencia</asp:ListItem>
                                                                <asp:ListItem Value="4">PPL</asp:ListItem>
                                                                <asp:ListItem Value="5">Cargo Express</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div style="border: solid 1px #5d7b9d; width: 700px; text-align:left;">
                                                <table border="0" cellpadding="1" cellspacing="1" style="width: 700px;">
                                                    <tr>
                                                        <th colspan="8" style="background-color: #5d7b9d; color: #ffffff;">
                                                            PROFILAXIS
                                                        </th>
                                                    </tr>
                                                    <tr>
                                                        <td style="background-color: #e9ecf1; text-align:center; width:82px;" colspan="2">
                                                            Código
                                                        </td>
                                                        <td style="background-color: #e9ecf1; text-align:center; width:170px;">
                                                            Medicamento
                                                        </td>
                                                        <td style="background-color: #e9ecf1; text-align:center; width:50px;">
                                                            Dósis
                                                        </td>
                                                        <td style="background-color: #e9ecf1; text-align:center;">
                                                            VIA
                                                        </td>
                                                        <td style="background-color: #e9ecf1; text-align:center; width:38px;">
                                                            Freq.
                                                        </td>
                                                        <td style="background-color: #e9ecf1; text-align:center; width:146px;">
                                                            Tipo
                                                        </td>
                                                        <td style="background-color: #e9ecf1; text-align:center; width:50px;">
                                                            Estatus
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <span class="numeracion">#1</span>
                                                        </td>
                                                        <td style="text-align:center;">
                                                            <asp:DropDownList ID="DDL_cod1P" runat="server" CssClass="datos" Width="60" AutoPostBack="False">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="text-align:left;">
                                                            <asp:Label ID="lbl_prof1" runat="server" CssClass="datos2"></asp:Label>
                                                        <asp:Label ID="lbl_cantidadP1" runat="server" Text="" Visible="false"></asp:Label>
                                                        </td>
                                                        <td style="text-align:center;">
                                                            <asp:TextBox ID="txt_dx1P" runat="server" Width="50px" CssClass="datosdx" MaxLength="12"></asp:TextBox>
                                                        </td>
                                                        <td style="text-align:center;">
                                                            <asp:DropDownList ID="DDL_Via1" runat="server" CssClass="datos">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="text-align:center;">
                                                            <asp:DropDownList ID="DDL_fx1P" runat="server" CssClass="datos">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="text-align:left;">
                                                            <asp:DropDownList ID="DDL_t1" runat="server" CssClass="datos" Width="88" AutoPostBack="false">
                                                                <asp:ListItem Text="" Value="0" Selected="True"></asp:ListItem>
                                                                <asp:ListItem Text="Profilaxis" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="Tratamiento" Value="2"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="DDL_Trat1" runat="server" CssClass="datos" Width="54" Visible="false">
                                                                <asp:ListItem Text="ITS" Value="1" Selected="True"></asp:ListItem>
                                                                <asp:ListItem Text="IO" Value="2"></asp:ListItem>
                                                                <asp:ListItem Text="Otros" Value="3"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="text-align:left;">
                                                            <asp:DropDownList ID="DDL_e1" runat="server" CssClass="datos">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align:right;"></td>
                                                        <td colspan="7" style="text-align:left; border-bottom:solid 1px #5d7b9d;">
                                                            <table border="0" cellpadding="0" cellspacing="2">
                                                                <tr>
                                                                    <td style="text-align:right;">Cantidad:</td>
                                                                    <td style="text-align:left;">
                                                                        <asp:TextBox ID="txt_cant1P" runat="server" CssClass="datosN" MaxLength="3" Width="25px">0</asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                                            TargetControlID="txt_cant1P" ValidChars="0123456789">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                    <td style="text-align:right;">Días:</td>
                                                                    <td style="text-align:left;">
                                                                        <asp:TextBox ID="txt_tdias1" runat="server" Width="25px" CssClass="datosN" MaxLength="3"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="txt_tdias1_FilteredTextBoxExtender" runat="server"
                                                                            TargetControlID="txt_tdias1" ValidChars="0123456789">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                    <td style="text-align:right;">Observaciones:</td>
                                                                    <td style="text-align:left;">
                                                                    <asp:TextBox ID="txt_obs1" runat="server" Width="436" CssClass="datosObs"></asp:TextBox></td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
	                                                    <td>
		                                                    <span class="numeracion">#2</span>
	                                                    </td>
	                                                    <td style="text-align:center;">
		                                                    <asp:DropDownList ID="DDL_cod2P" runat="server" CssClass="datos" Width="60" AutoPostBack="False">
		                                                    </asp:DropDownList>
	                                                    </td>
	                                                    <td style="text-align:left;">
		                                                    <asp:Label ID="lbl_prof2" runat="server" CssClass="datos2"></asp:Label>
	                                                        <asp:Label ID="lbl_cantidadP2" runat="server" Text="" Visible="false"></asp:Label>
                                                        </td>
	                                                    <td style="text-align:center;">
		                                                    <asp:TextBox ID="txt_dx2P" runat="server" Width="50px" CssClass="datosdx" MaxLength="12"></asp:TextBox>
	                                                    </td>
	                                                    <td style="text-align:center;">
		                                                    <asp:DropDownList ID="DDL_Via2" runat="server" CssClass="datos">
		                                                    </asp:DropDownList>
	                                                    </td>
	                                                    <td style="text-align:center;">
                                                            <asp:DropDownList ID="DDL_fx2P" runat="server" CssClass="datos">
		                                                    </asp:DropDownList>
	                                                    </td>
	                                                    <td style="text-align:left;">
		                                                    <asp:DropDownList ID="DDL_t2" runat="server" CssClass="datos" Width="88" AutoPostBack="false">
			                                                    <asp:ListItem Text="" Value="0" Selected="True"></asp:ListItem>
                                                                <asp:ListItem Text="Profilaxis" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="Tratamiento" Value="2"></asp:ListItem>
		                                                    </asp:DropDownList>
		                                                    <asp:DropDownList ID="DDL_Trat2" runat="server" CssClass="datos" Width="54" Visible="false">
			                                                    <asp:ListItem Text="ITS" Value="1" Selected="True"></asp:ListItem>
			                                                    <asp:ListItem Text="IO" Value="2"></asp:ListItem>
			                                                    <asp:ListItem Text="Otros" Value="3"></asp:ListItem>
		                                                    </asp:DropDownList>
	                                                    </td>
	                                                    <td style="text-align:left;">
                                                            <asp:DropDownList ID="DDL_e2" runat="server" CssClass="datos">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align:right;"></td>
                                                        <td colspan="7" style="text-align:left; border-bottom:solid 1px #5d7b9d;">
                                                            <table border="0" cellpadding="0" cellspacing="2">
                                                                <tr>
                                                                    <td style="text-align:right;">Cantidad:</td>
                                                                    <td style="text-align:left;">
                                                                        <asp:TextBox ID="txt_cant2P" runat="server" CssClass="datosN" MaxLength="3" Width="25px">0</asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                                                            TargetControlID="txt_cant2P" ValidChars="0123456789">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                    <td style="text-align:right;">Días:</td>
                                                                    <td style="text-align:left;">
                                                                        <asp:TextBox ID="txt_tdias2" runat="server" Width="25px" CssClass="datosN" MaxLength="3"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="txt_tdias2_FilteredTextBoxExtender" runat="server"
                                                                            TargetControlID="txt_tdias2" ValidChars="0123456789">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                    <td style="text-align:right;">Observaciones:</td>
                                                                    <td style="text-align:left;">
                                                                    <asp:TextBox ID="txt_obs2" runat="server" Width="436" CssClass="datosObs"></asp:TextBox></td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
	                                                    <td>
		                                                    <span class="numeracion">#3</span>
	                                                    </td>
	                                                    <td style="text-align:center;">
		                                                    <asp:DropDownList ID="DDL_cod3P" runat="server" CssClass="datos" Width="60" AutoPostBack="False">
		                                                    </asp:DropDownList>
	                                                    </td>
	                                                    <td style="text-align:left;">
		                                                    <asp:Label ID="lbl_prof3" runat="server" CssClass="datos2"></asp:Label>
                                                            <asp:Label ID="lbl_cantidadP3" runat="server" Text="" Visible="false"></asp:Label>	                                                   
                                                         </td>
	                                                    <td style="text-align:center;">
		                                                    <asp:TextBox ID="txt_dx3P" runat="server" Width="50px" CssClass="datosdx" MaxLength="12"></asp:TextBox>
	                                                    </td>
	                                                    <td style="text-align:center;">
		                                                    <asp:DropDownList ID="DDL_Via3" runat="server" CssClass="datos">
		                                                    </asp:DropDownList>
	                                                    </td>
	                                                    <td style="text-align:center;">
		                                                    <asp:DropDownList ID="DDL_fx3P" runat="server" CssClass="datos">
		                                                    </asp:DropDownList>
	                                                    </td>
	                                                    <td style="text-align:left;">
		                                                    <asp:DropDownList ID="DDL_t3" runat="server" CssClass="datos" Width="88" AutoPostBack="false">
			                                                    <asp:ListItem Text="" Value="0" Selected="True"></asp:ListItem>
                                                                <asp:ListItem Text="Profilaxis" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="Tratamiento" Value="2"></asp:ListItem>
		                                                    </asp:DropDownList>
		                                                    <asp:DropDownList ID="DDL_Trat3" runat="server" CssClass="datos" Width="54" Visible="false">
			                                                    <asp:ListItem Text="ITS" Value="1" Selected="True"></asp:ListItem>
			                                                    <asp:ListItem Text="IO" Value="2"></asp:ListItem>
			                                                    <asp:ListItem Text="Otros" Value="3"></asp:ListItem>
		                                                    </asp:DropDownList>
	                                                    </td>
                                                        <td style="text-align:left;">
                                                            <asp:DropDownList ID="DDL_e3" runat="server" CssClass="datos">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align:right;"></td>
                                                        <td colspan="7" style="text-align:left; border-bottom:solid 1px #5d7b9d;">
                                                            <table border="0" cellpadding="0" cellspacing="2">
                                                                <tr>
                                                                    <td style="text-align:right;">Cantidad:</td>
                                                                    <td style="text-align:left;">
                                                                        <asp:TextBox ID="txt_cant3P" runat="server" CssClass="datosN" MaxLength="3" Width="25px">0</asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                                                            TargetControlID="txt_cant3P" ValidChars="0123456789">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                    <td style="text-align:right;">Días:</td>
                                                                    <td style="text-align:left;">
                                                                        <asp:TextBox ID="txt_tdias3" runat="server" Width="25px" CssClass="datosN" MaxLength="3"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="txt_tdias3_FilteredTextBoxExtender" runat="server"
                                                                            TargetControlID="txt_tdias3" ValidChars="0123456789">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                    <td style="text-align:right;">Observaciones:</td>
                                                                    <td style="text-align:left;">
                                                                    <asp:TextBox ID="txt_obs3" runat="server" Width="436" CssClass="datosObs"></asp:TextBox></td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
	                                                    <td>
		                                                    <span class="numeracion">#4</span>
	                                                    </td>
	                                                    <td style="text-align:center;">
		                                                    <asp:DropDownList ID="DDL_cod4P" runat="server" CssClass="datos" Width="60" AutoPostBack="False">
		                                                    </asp:DropDownList>
	                                                    </td>
	                                                    <td style="text-align:left;">
		                                                    <asp:Label ID="lbl_prof4" runat="server" CssClass="datos2"></asp:Label>
	                                                        <asp:Label ID="lbl_cantidadP4" runat="server" Text="" Visible="false"></asp:Label>
                                                        </td>
	                                                    <td style="text-align:center;">
		                                                    <asp:TextBox ID="txt_dx4P" runat="server" Width="50px" CssClass="datosdx" MaxLength="12"></asp:TextBox>
	                                                    </td>
	                                                    <td style="text-align:center;">
		                                                    <asp:DropDownList ID="DDL_Via4" runat="server" CssClass="datos">
		                                                    </asp:DropDownList>
	                                                    </td>
	                                                    <td style="text-align:center;">
                                                            <asp:DropDownList ID="DDL_fx4P" runat="server" CssClass="datos">
		                                                    </asp:DropDownList>
	                                                    </td>
	                                                    <td style="text-align:left;">
		                                                    <asp:DropDownList ID="DDL_t4" runat="server" CssClass="datos" Width="88" AutoPostBack="false">
			                                                    <asp:ListItem Text="" Value="0" Selected="True"></asp:ListItem>
                                                                <asp:ListItem Text="Profilaxis" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="Tratamiento" Value="2"></asp:ListItem>
		                                                    </asp:DropDownList>
		                                                    <asp:DropDownList ID="DDL_Trat4" runat="server" CssClass="datos" Width="54" Visible="false">
			                                                    <asp:ListItem Text="ITS" Value="1" Selected="True"></asp:ListItem>
			                                                    <asp:ListItem Text="IO" Value="2"></asp:ListItem>
			                                                    <asp:ListItem Text="Otros" Value="3"></asp:ListItem>
		                                                    </asp:DropDownList>
	                                                    </td>
	                                                    <td style="text-align:left;">
                                                            <asp:DropDownList ID="DDL_e4" runat="server" CssClass="datos">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align:right;"></td>
                                                        <td colspan="7" style="text-align:left; border-bottom:solid 1px #5d7b9d;">
                                                            <table border="0" cellpadding="0" cellspacing="2">
                                                                <tr>
                                                                    <td style="text-align:right;">Cantidad:</td>
                                                                    <td style="text-align:left;">
                                                                        <asp:TextBox ID="txt_cant4P" runat="server" CssClass="datosN" MaxLength="3" Width="25px">0</asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server"
                                                                            TargetControlID="txt_cant4P" ValidChars="0123456789">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                    <td style="text-align:right;">Días:</td>
                                                                    <td style="text-align:left;">
                                                                        <asp:TextBox ID="txt_tdias4" runat="server" Width="25px" CssClass="datosN" MaxLength="3"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="txt_tdias4_FilteredTextBoxExtender" runat="server"
                                                                            TargetControlID="txt_tdias4" ValidChars="0123456789">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                    <td style="text-align:right;">Observaciones:</td>
                                                                    <td style="text-align:left;">
                                                                    <asp:TextBox ID="txt_obs4" runat="server" Width="436" CssClass="datosObs"></asp:TextBox></td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
	                                                    <td>
		                                                    <span class="numeracion">#5</span>
	                                                    </td>
	                                                    <td style="text-align:center;">
		                                                    <asp:DropDownList ID="DDL_cod5P" runat="server" CssClass="datos" Width="60" AutoPostBack="False">
		                                                    </asp:DropDownList>
	                                                    </td>
	                                                    <td style="text-align:left;">
		                                                    <asp:Label ID="lbl_prof5" runat="server" CssClass="datos2"></asp:Label>
	                                                        <asp:Label ID="lbl_cantidadP5" runat="server" Text="" Visible="false"></asp:Label>
                                                        </td>
	                                                    <td style="text-align:center;">
		                                                    <asp:TextBox ID="txt_dx5P" runat="server" Width="50px" CssClass="datosdx" MaxLength="12"></asp:TextBox>
	                                                    </td>
	                                                    <td style="text-align:center;">
		                                                    <asp:DropDownList ID="DDL_Via5" runat="server" CssClass="datos">
		                                                    </asp:DropDownList>
	                                                    </td>
	                                                    <td style="text-align:center;">
                                                            <asp:DropDownList ID="DDL_fx5P" runat="server" CssClass="datos">
		                                                    </asp:DropDownList>
	                                                    </td>
	                                                    <td style="text-align:left;">
		                                                    <asp:DropDownList ID="DDL_t5" runat="server" CssClass="datos" Width="88" AutoPostBack="false">
			                                                    <asp:ListItem Text="" Value="0" Selected="True"></asp:ListItem>
                                                                <asp:ListItem Text="Profilaxis" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="Tratamiento" Value="2"></asp:ListItem>
		                                                    </asp:DropDownList>
		                                                    <asp:DropDownList ID="DDL_Trat5" runat="server" CssClass="datos" Width="54" Visible="false">
			                                                    <asp:ListItem Text="ITS" Value="1" Selected="True"></asp:ListItem>
			                                                    <asp:ListItem Text="IO" Value="2"></asp:ListItem>
			                                                    <asp:ListItem Text="Otros" Value="3"></asp:ListItem>
		                                                    </asp:DropDownList>
	                                                    </td>
                                                        <td style="text-align:left;">
                                                            <asp:DropDownList ID="DDL_e5" runat="server" CssClass="datos">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align:right;"></td>
                                                        <td colspan="7" style="text-align:left; border-bottom:solid 1px #5d7b9d;">
                                                            <table border="0" cellpadding="0" cellspacing="2">
                                                                <tr>
                                                                    <td style="text-align:right;">Cantidad:</td>
                                                                    <td style="text-align:left;">
                                                                        <asp:TextBox ID="txt_cant5P" runat="server" CssClass="datosN" MaxLength="3" Width="25px">0</asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server"
                                                                            TargetControlID="txt_cant5P" ValidChars="0123456789">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                    <td style="text-align:right;">Días:</td>
                                                                    <td style="text-align:left;">
                                                                        <asp:TextBox ID="txt_tdias5" runat="server" Width="25px" CssClass="datosN" MaxLength="3"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="txt_tdias5_FilteredTextBoxExtender" runat="server"
                                                                            TargetControlID="txt_tdias5" ValidChars="0123456789">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                    <td style="text-align:right;">Observaciones:</td>
                                                                    <td style="text-align:left;">
                                                                    <asp:TextBox ID="txt_obs5" runat="server" Width="436" CssClass="datosObs"></asp:TextBox></td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
	                                                    <td>
		                                                    <span class="numeracion">#6</span>
	                                                    </td>
	                                                    <td style="text-align:center;">
		                                                    <asp:DropDownList ID="DDL_cod6P" runat="server" CssClass="datos" Width="60" AutoPostBack="False">
		                                                    </asp:DropDownList>
	                                                    </td>
	                                                    <td style="text-align:left;">
		                                                    <asp:Label ID="lbl_prof6" runat="server" CssClass="datos2"></asp:Label>
	                                                        <asp:Label ID="lbl_cantidadP6" runat="server" Text="" Visible="false"></asp:Label>
                                                        </td>
	                                                    <td style="text-align:center;">
		                                                    <asp:TextBox ID="txt_dx6P" runat="server" Width="50px" CssClass="datosdx" MaxLength="12"></asp:TextBox>
	                                                    </td>
	                                                    <td style="text-align:center;">
		                                                    <asp:DropDownList ID="DDL_Via6" runat="server" CssClass="datos">
		                                                    </asp:DropDownList>
	                                                    </td>
	                                                    <td style="text-align:center;">
                                                            <asp:DropDownList ID="DDL_fx6P" runat="server" CssClass="datos">
		                                                    </asp:DropDownList>
	                                                    </td>
	                                                    <td style="text-align:left;">
		                                                    <asp:DropDownList ID="DDL_t6" runat="server" CssClass="datos" Width="88" AutoPostBack="false">
			                                                    <asp:ListItem Text="" Value="0" Selected="True"></asp:ListItem>
                                                                <asp:ListItem Text="Profilaxis" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="Tratamiento" Value="2"></asp:ListItem>
		                                                    </asp:DropDownList>
		                                                    <asp:DropDownList ID="DDL_Trat6" runat="server" CssClass="datos" Width="54" Visible="false">
			                                                    <asp:ListItem Text="ITS" Value="1" Selected="True"></asp:ListItem>
			                                                    <asp:ListItem Text="IO" Value="2"></asp:ListItem>
			                                                    <asp:ListItem Text="Otros" Value="3"></asp:ListItem>
		                                                    </asp:DropDownList>
	                                                    </td>
	                                                    <td style="text-align:left;">
                                                            <asp:DropDownList ID="DDL_e6" runat="server" CssClass="datos">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align:right;"></td>
                                                        <td colspan="7" style="text-align:left;">
                                                            <table border="0" cellpadding="0" cellspacing="2">
                                                                <tr>
                                                                    <td style="text-align:right;">Cantidad:</td>
                                                                    <td style="text-align:left;">
                                                                        <asp:TextBox ID="txt_cant6P" runat="server" CssClass="datosN" MaxLength="3" Width="25px">0</asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server"
                                                                            TargetControlID="txt_cant6P" ValidChars="0123456789">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                    <td style="text-align:right;">Días:</td>
                                                                    <td style="text-align:left;">
                                                                        <asp:TextBox ID="txt_tdias6" runat="server" Width="25px" CssClass="datosN" MaxLength="3"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="txt_tdias6_FilteredTextBoxExtender" runat="server"
                                                                            TargetControlID="txt_tdias6" ValidChars="0123456789">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                    <td style="text-align:right;">Observaciones:</td>
                                                                    <td style="text-align:left;">
                                                                    <asp:TextBox ID="txt_obs6" runat="server" Width="436" CssClass="datosObs"></asp:TextBox></td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
