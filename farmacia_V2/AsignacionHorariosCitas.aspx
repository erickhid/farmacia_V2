<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="AsignacionHorariosCitas.aspx.vb" Inherits="CalendarioCitas" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div style="width: 780px; border: solid 1px #5d7b9d; text-align:left;">
        <center>
            <table id="tblbasal" border="0" cellpadding="2" cellspacing="1" style="width:760px; text-align:left;">
            <tr>
                <th colspan="2" class="theader">ASIGNACION DE HORARIO POR CITAS</th>                
            </tr>
                <tr>
                    <td colspan="2"></td>
                </tr>
            <tr> 
                <td style="width:380px; vertical-align:top;"> 
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate> 
                            <asp:Panel ID="pnl_calendar" runat="server">                        
                            <table border="0" cellpadding="2" cellspacing="1" style="width:423px">
                                <tr>
                                    <th colspan="4" class="theader">HORARIO PROXIMA CITA</th>
                                    <%--<td colspan="4" class="calendarMonthSelector" style="height:24px; color: #ffffff; font-size:10pt; text-align:center;"></td>--%>
                                </tr>
                                <tr>
                    <td></td>
                </tr>
                                <tr>
                                    <td style="width:90px; background-color: #5d7b9d; color: #ffffff;">
                                        Número ASI:
                                    </td>                                     
                                    <td colspan="3" style="background-color: #e9ecf1; padding:0px; text-align:left;">
                                        <table id="tblNHC" border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txt_asi" runat="server" CssClass="NHC3" MaxLength="7" Width="64px"
                                                    TabIndex="1" AutoPostBack="True"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="txt_asi_FilteredTextBoxExtender" runat="server"
                                                        TargetControlID="txt_asi" ValidChars="0123456789Pp">
                                                    </cc1:FilteredTextBoxExtender>
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="btn_buscar" runat="server" ToolTip="BUSCAR" CausesValidation="False" ImageUrl="~/images/magnify-clip.png" TabIndex="2" style="width: 16px; height: 16px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr style="height:24px;">
                                    <td style="width:90px; background-color: #5d7b9d; color: #ffffff;">
                                    Nombre:
                                    </td>
                                    <td colspan="3" style="background-color: #e9ecf1;">
                                        <input id="hd_idpaciente" type="hidden" runat="server" />
                                        <input id="hd_idsignosvitales" type="hidden" runat="server" />
                                        <input id="hd_idcitas" type="hidden" runat="server" />
                                        <asp:Label ID="lbl_nombre" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width:90px; background-color: #5d7b9d; color: #ffffff;">
                                        Género:
                                    </td>
                                    <td style="width: 70px; background-color: #e9ecf1;">
                                        <asp:Label ID="lbl_genero" runat="server"></asp:Label>
                                    </td>
                                    <td style="width: 60px; background-color: #5d7b9d; color: #ffffff;">
                                        Edad:
                                    </td>
                                    <td style="width: 180px; background-color: #e9ecf1;">
                                        <asp:Label ID="lbl_edad" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                 <tr>
                                    <td style="width:200px; background-color: #5d7b9d; color: #ffffff; border-top:solid 1px #FFFFFF;">Fecha Entrega:</td>
                                    <td style=" border-top:solid 1px #FFFFFF; background-color: #e9ecf1;">
                                        <asp:Label ID="lbl_FEntrega" runat="server" ></asp:Label>
                                    </td>
                                     <td style="width:200px; background-color: #5d7b9d; color: #ffffff; border-top:solid 1px #FFFFFF;">Fecha Retorno:</td>
                                    <td style=" border-top:solid 1px #FFFFFF;background-color: #e9ecf1;">
                                        <asp:Label ID="lbl_FRetorno" runat="server" ></asp:Label>
                                    </td> 
                                </tr>
                                <tr>
                                    <td style="width:200px; background-color: #5d7b9d; color: #ffffff; border-top:solid 1px #FFFFFF;">Proxima Cita:</td>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <table border="0" cellpadding="0" cellspacing="0">
                                                    <tr>                                                        
                                                        <td style="text-align:center;">                                                            
                                                            <asp:TextBox ID="txt_fecha" runat="server" style="width:70px" AutoPostBack="True"></asp:TextBox>
                                                        </td>
                                                        <td style="text-align:left;" >
                                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="ibtn_calendario" TargetControlID="txt_fecha" Format="dd/MM/yyyy"  CssClass="ajax__calendar"></cc1:CalendarExtender>
                                                            <cc1:FilteredTextBoxExtender ID="txt_fecha_filtered" runat="server" TargetControlID="txt_fecha" ValidChars="0123456789/"> </cc1:FilteredTextBoxExtender>
                                                            <asp:ImageButton ID="ibtn_calendario" runat="server" ImageUrl="~/images/datePickerPopupHover.gif" CausesValidation="False" BorderWidth="0"/>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td> 
                                    <td style="width:90px; background-color: #5d7b9d; color: #ffffff;">&nbsp;Horario:</td>
                                        <td><asp:DropDownList ID="ddl_horario_cita" runat="server" AppendDataBoundItems="True" AutoPostBack="True" >
                                        </asp:DropDownList>
                                        </td>
                                </tr>                                                            
                                <tr>
                                    <%--<td style="width:90px; background-color: #5d7b9d; color: #ffffff;">&nbsp;Día:</td>--%>
                                    <td style="width:100px; padding-left:5px;"  colspan="1" ><asp:Label ID="lbl_dia_cita" runat="server" Text="" AutoPostBack="True" Visible="False" ></asp:Label></td>
                                    <td style="width:100px; padding-left:5px; font-weight: bold;"  colspan="4" >
                                        <asp:Label ID="lbl_horario_asignado" runat="server" Text="" Visible="False"></asp:Label>
                                    </td>
                                </tr>
                                <tr>

                                    <td colspan="4" style="background-color:#e9ecf1; padding-top:2px; padding-bottom:2px; padding-right:2px; text-align:right;">                                       
                                        <asp:Button ID="btn_agregar" runat="server" Text="Agregar" CssClass="button1" />
                                        <asp:Button ID="btn_limpiar" runat="server" Text="Limpiar" CausesValidation="False" CssClass="button1"  />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2"><asp:Literal runat="server" ID="ltl_error"/></td>
                                    <td colspan="2"><asp:Literal runat="server" ID="ltl_error2"/></td>
                                </tr>
                                 <div style="clear:both; height:0; overflow:hidden">&nbsp;</div> 

                                 <div id="DivNoCitasPaciente" runat="server" visible="true">
                                     <table  style="width: 423px; vertical-align:top;">
                                  <tr>
                                    <td colspan="4" class="calendarMonthSelector" style="height:24px; color: #ffffff; font-size:10pt; text-align:center;">
                                        HORARIO CITAS
                                        <asp:Label ID="lbl_dia_horario" runat="server" Text=""></asp:Label>
                                    </td>
                                  </tr>                                  
                                  <tr>
                                      <td style="width:50px; background-color: #5d7b9d; color: #ffffff; height: 20px; text-align:center; font-size:15px">
                                          <asp:Label ID="lbl_THorario" runat="server" Text="Horario"></asp:Label>
                                      </td>
                                      <td style="width:30px; background-color: #5d7b9d; color: #ffffff; height: 20px; text-align:center; font-size:15px">
                                          <asp:Label ID="lbl_Tcantidad" runat="server" Text="Cantidad"></asp:Label>
                                      </td>
                                      <td style="width:120px; background-color: #5d7b9d; color: #ffffff; height: 20px; text-align:center; font-size:15px">
                                          <asp:Label ID="lbl_TStatuHorario" runat="server" Text="Status Horario"></asp:Label>
                                      </td>
                                  </tr> 
                                  <tr>
                                      <td style="background-color: #e9ecf1; font-size:12px; font-weight:bold" >
                                          <asp:Label ID="LblHorario" runat="server" Text="07:00 - 08:00"></asp:Label>
                                      </td>
                                      <td style="background-color: #e9ecf1; font-size:12px; font-weight:bold; width: 32px; text-align:center;"> 
                                            <asp:Label ID="LblCantidad" runat="server" text=""  ></asp:Label>
                                        </td>
                                        <td style="background-color: #e9ecf1;font-size:12px; width: 62px; text-align:center;">
                                            <asp:Label ID="LblStatus" runat="server" text="" ></asp:Label>
                                        </td>
                                  </tr> 
                                  <tr>
                                      <td style="background-color: #e9ecf1; font-size:12px; font-weight:bold">
                                          <asp:Label ID="LblHorario1" runat="server" Text="08:00 - 09:00"></asp:Label>
                                      </td>
                                      <td style="background-color: #e9ecf1; font-size:12px; font-weight:bold; width: 32px; text-align:center;"> 
                                            <asp:Label ID="LblCantidad1" runat="server" text="" ></asp:Label>
                                        </td>
                                        <td style="background-color: #e9ecf1; font-size:12px; width: 62px; text-align:center;">
                                            <asp:Label ID="LblStatus1" runat="server" Visible="true" ></asp:Label>
                                        </td>
                                  </tr>
                                  <tr>
                                      <td style="background-color: #e9ecf1; font-size:12px; font-weight:bold">
                                          <asp:Label ID="LblHorario2" runat="server" Text="09:00 - 10:00"></asp:Label>
                                      </td>
                                      <td style="background-color: #e9ecf1; font-size:12px; font-weight:bold; width: 32px; text-align:center;"> 
                                            <asp:Label ID="LblCantidad2" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td style="background-color: #e9ecf1; font-size:12px; width: 62px; text-align:center;">
                                            <asp:Label ID="LblStatus2" runat="server" Text=""></asp:Label>
                                        </td>
                                  </tr> 
                                  <tr>
                                      <td style="background-color: #e9ecf1; font-size:12px; font-weight:bold">
                                          <asp:Label ID="LblHorario3" runat="server" Text="10:00 - 11:00"></asp:Label>
                                      </td>
                                      <td style="background-color: #e9ecf1; font-size:12px; font-weight:bold; width: 32px; text-align:center;"> 
                                            <asp:Label ID="LblCantidad3" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td style="background-color: #e9ecf1; font-size:12px; width: 62px; text-align:center;">
                                            <asp:Label ID="LblStatus3" runat="server" Text=""></asp:Label>
                                        </td>
                                  </tr>
                                  <tr>
                                      <td style="background-color: #e9ecf1; font-size:12px; font-weight:bold;">
                                          <asp:Label ID="LblHorario4" runat="server" Text="11:00 - 12:00"></asp:Label>
                                      </td>
                                      <td style="background-color: #e9ecf1; font-size:12px; font-weight:bold; width: 32px; text-align:center;"> 
                                            <asp:Label ID="LblCantidad4" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td style="background-color: #e9ecf1; font-size:12px; width: 62px; text-align:center;">
                                            <asp:Label ID="LblStatus4" runat="server" Text=""></asp:Label>
                                        </td>
                                  </tr>
                                  <tr>
                                      <td style="background-color: #e9ecf1; font-size:12px; font-weight:bold">
                                          <asp:Label ID="LblHorario5" runat="server" Text="12:00 - 13:00"></asp:Label>
                                      </td>
                                      <td style="background-color: #e9ecf1; font-size:12px; font-weight:bold; width: 32px; text-align:center;"> 
                                            <asp:Label ID="LblCantidad5" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td style="background-color: #e9ecf1; font-size:12px; width: 62px; text-align:center;">
                                            <asp:Label ID="LblStatus5" runat="server" Text=""></asp:Label>
                                        </td>
                                  </tr>
                                  <tr>
                                      <td style="background-color: #e9ecf1; font-size:12px; font-weight:bold">
                                          <asp:Label ID="LblHorario6" runat="server" Text="13:00 - 14:00"></asp:Label>
                                      </td>
                                      <td style="background-color: #e9ecf1; font-size:12px; font-weight:bold; width: 32px; text-align:center;"> 
                                            <asp:Label ID="LblCantidad6" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td style="background-color: #e9ecf1; font-size:12px; width: 62px; text-align:center;">
                                            <asp:Label ID="LblStatus6" runat="server" Text=""></asp:Label>
                                        </td>
                                  </tr>
                                  <tr>
                                      <td style="background-color: #e9ecf1; font-size:12px; font-weight:bold">
                                          <asp:Label ID="LblHorario7" runat="server" Text="14:00 - 15:00"></asp:Label>
                                      </td>
                                      <td style="background-color: #e9ecf1; font-size:12px; font-weight:bold; width: 32px; text-align:center;"> 
                                            <asp:Label ID="LblCantidad7" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td style="background-color: #e9ecf1; font-size:12px; width: 62px; text-align:center;">
                                            <asp:Label ID="LblStatus7" runat="server" Text=""></asp:Label>
                                        </td>
                                  </tr>
                                  <tr>
                                      <td style="background-color: #e9ecf1; font-size:12px; font-weight:bold;">
                                          <asp:Label ID="LblHorario8" runat="server" Text="15:00 - 16:00"></asp:Label>
                                      </td>
                                      <td style="background-color: #e9ecf1; font-size:12px; font-weight:bold; width: 32px; text-align:center;"> 
                                            <asp:Label ID="LblCantidad8" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td style="background-color: #e9ecf1; font-size:12px; width: 62px; text-align:center;">
                                            <asp:Label ID="LblStatus8" runat="server" Text=""></asp:Label>
                                        </td>
                                  </tr>                                                                    
                              </table>           
                                 </div>
                                </asp:Panel>  
                            <asp:Literal runat="server" ID="ltl_output" />                       
                        </ContentTemplate>
                         <Triggers>
                           <%-- <asp:AsyncPostBackTrigger ControlID="txt_fecha" EventName="TextChanged" />--%>
                            <%--<asp:AsyncPostBackTrigger ControlID="txt_asi" EventName="TextChanged" />--%>
                            <asp:AsyncPostBackTrigger ControlID="btn_buscar" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btn_limpiar" EventName="Click" />
                        </Triggers>                        
                    </asp:UpdatePanel>                                         
                </td>

                <td style="width:380px; vertical-align:top;">
                    <div id="div_resumen" runat="server" visible="true"> 
                    <asp:UpdatePanel ID="UpdatePanel0" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table border="0" cellpadding="2" cellspacing="1" class="cita" style="width: 327px; margin-left: 10px;">
                                <tr>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode ="Conditional">                                            
                                            <ContentTemplate>
                                                <div style="text-align:center; width: 264px; background-color: #5d7b9d; color: #ffffff; font-size:10pt;"><asp:Label ID="lbl_titulo" runat="server"></asp:Label></div>
                                                <asp:GridView ID="RC_reportes" runat="server" CellPadding="1" ForeColor="#333333" GridLines="both" BorderColor="#a0acc0" BorderStyle="Solid" BorderWidth="1px" With="327px" ShowFooter="False" AutoGenerateColumns="False">
                                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" CssClass="GV_rowpadREP1" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Fecha Entrega">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_Fecha_Entrega" runat="server" Text='<%# Bind("Fecha_Retorno")%>'></asp:Label>
                                                                 </ItemTemplate>
                                                                    <ItemStyle Width="150px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                                                    <HeaderStyle Width="150px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                                                </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Horario Cita">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_Horario_Cita" runat="server" Text='<%# Bind("Horario")%>'></asp:Label>
                                                                 </ItemTemplate>
                                                                    <ItemStyle Width="150px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                                                    <HeaderStyle Width="150px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                                                </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Total">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_Total" runat="server" Text='<%# Bind("Total")%>'></asp:Label>
                                                                 </ItemTemplate>
                                                                    <ItemStyle Width="25px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                                                    <HeaderStyle Width="25px" HorizontalAlign="Center" CssClass="GV_rowpad" />
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
                                                <asp:AsyncPostBackTrigger ControlID="btn_buscar" EventName="Click" />
                                                <asp:AsyncPostBackTrigger ControlID="txt_asi" EventName="TextChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                 <tr>
                                    <td></td>
                            </tr>                                
                                <tr>
                                    <td colspan="2"><asp:Literal runat="server" ID="Literal1"/></td>
                                    <td colspan="2" style="width: 268435520px"><asp:Literal runat="server" ID="Literal2"/></td>
                                </tr>                          
                            </table>
                        </ContentTemplate>                        
                    </asp:UpdatePanel>  
                        </div>                                       
                </td>                                      
          </tr>                
         </table>            
        </center>
        
    </div>
</asp:Content>
