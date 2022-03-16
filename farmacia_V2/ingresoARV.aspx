<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ingresoARV.aspx.vb" Inherits="ingresoARV" ValidateRequest="false" EnableEventValidation="false"   %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="content_head" ContentPlaceHolderID="head_contentholder" runat="server">

    <link href="CSS/bootstrap.css" rel="stylesheet" />
    <link href="CSS/med.css" rel="stylesheet" />
    <link href="CSS/Custom-Cs.css" rel="stylesheet" />
    <link href="CSS/app.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">



    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" ScriptMode="Release">
        <Scripts>
           <%-- <asp:ScriptReference Path="Scripts/cancelPostBack.js" /> --%>
        </Scripts>
    </asp:ScriptManager>
	<script type="text/jscript">
         
         function Mensaje() {
            //debugger;
             var Mensaje = document.getElementById('<%=txtMessage.ClientID%>').value;
             Swal.fire(Mensaje).then(function(){
                                                    window.location.href = "/Farmacia/ingresoARV.aspx";
                                                }); 
         }
     </script>

    <asp:UpdatePanel ID="arvPanel" runat="server">
        <ContentTemplate>


            <div id="datospac" runat="server" style="margin-top: 5px; width: 700px; border: solid 1px #5d7b9d; text-align: left;">

                <table id="tblbasal" border="0" cellpadding="2" cellspacing="1">
                    <%--  <tr>
                    <th colspan="4" class="theader">INGRESO CONSUMO DE ARVS</th>
                </tr>--%>
                    <tr>
                        <td style="width: 100px; background-color: #5d7b9d; color: #ffffff;">Número ASI:
                        </td>


                        <td style="width: 230px; background-color: #e9ecf1; padding: 0px;">


                            <table id="tblNHC" border="0" cellpadding="0" cellspacing="0">

                                <tr>

                                    <td>
                                        <asp:TextBox ID="txt_asi" runat="server" CssClass="NHC" MaxLength="7" Width="64px" TabIndex="1"> </asp:TextBox>

                                        <asp:Label ID="Label1" runat="server" CssClass="paciente"></asp:Label>

                                        <asp:TextBox ID="controlID" Style="display: none" runat="server"></asp:TextBox>
                                        <asp:HiddenField ID="lblnhc" runat="server" />
                                        <asp:HiddenField ID="lblUsuario" runat="server" />
                                        <asp:HiddenField ID="lblUnidadAtencion" runat="server" />
                                        <asp:button ID="btn_end" runat="server" style="display:none" />
                                        <asp:button ID="btn_init" runat="server" style="display:none" />
                                        <cc1:FilteredTextBoxExtender ID="txt_asi_FilteredTextBoxExtender" runat="server"
                                            TargetControlID="txt_asi" ValidChars="0123456789Pp">
                                        </cc1:FilteredTextBoxExtender>
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="btn_buscar" runat="server"  ImageUrl="~/images/magnify-clip.png" />
                                        <asp:ImageButton ID="btn_editar" runat="server" ToolTip="EDITAR" CausesValidation="false" ImageUrl="~/images/file_edit.png" Visible="False" />
                                        <asp:ImageButton ID="btn_agregar" runat="server" ToolTip="AGREGAR" CausesValidation="false" ImageUrl="~/images/add.png" Visible="False" />

                                        <%--  <asp:Button ID="btn_search" runat="server"  AutoPostback = "false" Text="Button" Visible="false" />--%>
                                        <asp:Button ID="btn_crear" runat="server" AutoPostback="false" Text="Button" Visible="false" />
                                    </td>

                                </tr>

                            </table>
                        </td>
                        <td style="width: 100px; background-color: #5d7b9d; color: #ffffff;">Estatus MANGUA:  </td>
                        <td style="width: 270px; background-color: #e9ecf1;">
                            <asp:Label ID="lbl_estatus" runat="server" CssClass="paciente"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100px; background-color: #5d7b9d; color: #ffffff;">Nombre:
                        </td>
                        <td style="width: 230px; background-color: #e9ecf1;">
                            <asp:Label ID="lbl_nombre" runat="server"></asp:Label>
                        </td>
                        <td style="width: 100px; background-color: #5d7b9d; color: #ffffff;">Género:
                        </td>
                        <td style="width: 270px; background-color: #e9ecf1;">
                            <asp:Label ID="lbl_genero" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100px; background-color: #5d7b9d; color: #ffffff;">Fecha Nacimiento:
                        </td>
                        <td style="width: 230px; background-color: #e9ecf1;">
                            <asp:Label ID="lbl_nacimiento" runat="server"></asp:Label>
                        </td>
                        <td style="width: 100px; background-color: #5d7b9d; color: #ffffff;">Teléfono:
                        </td>
                        <td style="width: 270px; background-color: #e9ecf1;">
                            <asp:Label ID="lbl_telefono" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100px; background-color: #5d7b9d; color: #ffffff;">Domicilio Actual:
                        </td>
                        <td colspan="3" style="background-color: #e9ecf1;">
                            <asp:Label ID="lbl_domicilio" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                &nbsp;<asp:Label ID="lbl_error" runat="server" CssClass="error"></asp:Label>
            </div>

            <div id="divingreso" runat="server" visible="false">
                <table border="0" cellpadding="0" cellspacing="1">
                    <tr>
                        <td>
                            <div style="border: solid 1px #333333; width: 700px; text-align: left;">
                                <table border="0" cellpadding="2" cellspacing="1">
                                    <tr>
                                        <td>Última Fecha:</td>
                                        <td>
                                            <asp:Label ID="lbl_ultimafechaentrega" runat="server" CssClass="datos1"></asp:Label>
                                        </td>
                                        <td style="padding-left: 10px;">Estatus FARMACIA:</td>
                                        <td>
                                            <asp:Label ID="lbl_estatusfarmacia" runat="server" CssClass="datos1"></asp:Label>
                                        </td>
                                        <td style="padding-left: 10px;">TyT:</td>
                                        <td>
                                            <asp:Label ID="lbl_tyt_pac" runat="server" CssClass="datos1"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div style="border: solid 1px #333333; width: 700px; text-align: left;">
                                <table border="0" cellpadding="2" cellspacing="1">
                                    <tr>
                                        <td>Tiempo de Retorno en Días:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txt_retornodias" runat="server" Width="20px" CssClass="datosN" MaxLength="3"
                                                TabIndex="2">0</asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="txt_retornodias_FilteredTextBoxExtender" runat="server"
                                                TargetControlID="txt_retornodias" ValidChars="0123456789">
                                            </cc1:FilteredTextBoxExtender>
                                        </td>
                                        <td>
                                            Tiempo de Retorno Referencia:
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_tiemporetornooficial" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div style="border: solid 1px #333333; width: 700px;">
                                <table border="0" cellpadding="2" cellspacing="1" style="width: 700px;">
                                    <tr>
                                        <th colspan="12" style="background-color: #4f4f4f; color: #ffffff;">DEVOLUCIONES
                                        </th>
                                    </tr>
                                    <tr>
                                        <td style="background-color: #c1c1c1; text-align: center; width: 91px;" colspan="2">Código</td>
                                        <td style="background-color: #c1c1c1; text-align: center;">Cantidad</td>
                                        <td style="background-color: #c1c1c1; text-align: center; width: 91px;" colspan="2">Código</td>
                                        <td style="background-color: #c1c1c1; text-align: center;">Cantidad</td>
                                        <td style="background-color: #c1c1c1; text-align: center; width: 91px;" colspan="2">Código</td>
                                        <td style="background-color: #c1c1c1; text-align: center;">Cantidad</td>
                                        <td style="background-color: #c1c1c1; text-align: center; width: 91px;" colspan="2">Código</td>
                                        <td style="background-color: #c1c1c1; text-align: center;">Cantidad</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span class="numeracion1">#1</span>
                                        </td>
                                        <td style="width: 60px; text-align: center;">
                                            <asp:Label ID="lbl_devcod1" runat="server" CssClass="datos2"></asp:Label>
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:TextBox ID="txt_devcant1" runat="server" CssClass="datosN" MaxLength="3" TabIndex="3"
                                                Width="25px">0</asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="txt_devcant1_FilteredTextBoxExtender" runat="server"
                                                TargetControlID="txt_devcant1" ValidChars="0123456789">
                                            </cc1:FilteredTextBoxExtender>
                                        </td>
                                        <td>
                                            <span class="numeracion1">#2</span>
                                        </td>
                                        <td style="width: 60px; text-align: center;">
                                            <asp:Label ID="lbl_devcod2" runat="server" CssClass="datos2"></asp:Label>
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:TextBox ID="txt_devcant2" runat="server" CssClass="datosN" MaxLength="3" TabIndex="4"
                                                Width="25px">0</asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="txt_devcant2_FilteredTextBoxExtender" runat="server"
                                                TargetControlID="txt_devcant2" ValidChars="0123456789">
                                            </cc1:FilteredTextBoxExtender>
                                        </td>
                                        <td>
                                            <span class="numeracion1">#3</span>
                                        </td>
                                        <td style="width: 60px; text-align: center;">
                                            <asp:Label ID="lbl_devcod3" runat="server" CssClass="datos2"></asp:Label>
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:TextBox ID="txt_devcant3" runat="server" CssClass="datosN" MaxLength="3" TabIndex="5"
                                                Width="25px">0</asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="txt_devcant3_FilteredTextBoxExtender" runat="server"
                                                TargetControlID="txt_devcant3" ValidChars="0123456789">
                                            </cc1:FilteredTextBoxExtender>
                                        </td>
                                        <td>
                                            <span class="numeracion1">#4</span>
                                        </td>
                                        <td style="width: 60px; text-align: center;">
                                            <asp:Label ID="lbl_devcod4" runat="server" CssClass="datos2"></asp:Label>
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:TextBox ID="txt_devcant4" runat="server" CssClass="datosN" MaxLength="3" TabIndex="6"
                                                Width="25px">0</asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="txt_devcant4_FilteredTextBoxExtender" runat="server"
                                                TargetControlID="txt_devcant4" ValidChars="0123456789">
                                            </cc1:FilteredTextBoxExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span class="numeracion1">#5</span>
                                        </td>
                                        <td style="width: 60px; text-align: center;">
                                            <asp:Label ID="lbl_devcod5" runat="server" CssClass="datos2"></asp:Label>
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:TextBox ID="txt_devcant5" runat="server" CssClass="datosN" MaxLength="3" TabIndex="7"
                                                Width="25px">0</asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="txt_devcant5_FilteredTextBoxExtender" runat="server"
                                                TargetControlID="txt_devcant5" ValidChars="0123456789">
                                            </cc1:FilteredTextBoxExtender>
                                        </td>
                                        <td>
                                            <span class="numeracion1">#6</span>
                                        </td>
                                        <td style="width: 60px; text-align: center;">
                                            <asp:Label ID="lbl_devcod6" runat="server" CssClass="datos2"></asp:Label>
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:TextBox ID="txt_devcant6" runat="server" CssClass="datosN" MaxLength="3" TabIndex="8"
                                                Width="25px">0</asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="txt_devcant6_FilteredTextBoxExtender" runat="server"
                                                TargetControlID="txt_devcant6" ValidChars="0123456789">
                                            </cc1:FilteredTextBoxExtender>
                                        </td>
                                        <td>
                                            <span class="numeracion1">#7</span>
                                        </td>
                                        <td style="width: 60px; text-align: center;">
                                            <asp:Label ID="lbl_devcod7" runat="server" CssClass="datos2"></asp:Label>
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:TextBox ID="txt_devcant7" runat="server" CssClass="datosN" MaxLength="3" TabIndex="9"
                                                Width="25px">0</asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="txt_devcant7_FilteredTextBoxExtender" runat="server"
                                                TargetControlID="txt_devcant7" ValidChars="0123456789">
                                            </cc1:FilteredTextBoxExtender>
                                        </td>
                                        <td>
                                            <span class="numeracion1">#8</span>
                                        </td>
                                        <td style="width: 60px; text-align: center;">
                                            <asp:Label ID="lbl_devcod8" runat="server" CssClass="datos2"></asp:Label>
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:TextBox ID="txt_devcant8" runat="server" CssClass="datosN" MaxLength="3" TabIndex="10"
                                                Width="25px">0</asp:TextBox>
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
                            <div style="border: solid 1px #333333; width: 700px; text-align: left;">
                                <table border="0" cellpadding="2" cellspacing="1">
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="cbl_paciente_hospitalizado" Text="Paciente Hospitalizado" runat="server" />
											<asp:CheckBox ID="cbl_enviomed" Text=" Cargo Expreso" runat="server" AutoPostBack="true" />
										<%--<asp:CheckBoxList ID="cbl_paciente_hospitalizado" runat="server">
                                            <asp:ListItem Value="1">Paciente Hospitalizado</asp:ListItem>
                                        </asp:CheckBoxList>--%>
                                        <asp:Button ID="btn_calcular_adherencia" runat="server" Text="ADHERENCIA" CssClass="button" />
                                            </td>
                                        <td>Adherencia:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txt_adherencia" runat="server" Width="30px" CssClass="datosN" MaxLength="5"
                                                TabIndex="11">0</asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="txt_adherencia_FilteredTextBoxExtender" runat="server" TargetControlID="txt_adherencia"
                                                ValidChars="0123456789.">
                                            </cc1:FilteredTextBoxExtender>
                                        </td>
                                        <td>%</td>
                                        <td>Auto-Adherencia:</td>
                                        <td>
                                            <asp:DropDownList ID="ddl_auto_adherencia" runat="server" CssClass="datos" TabIndex="11">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
                <table border="0" cellpadding="0" cellspacing="1">
                    <tr>
                        <td>
                            <div style="background-color: #e9ecf1; border: solid 1px #5d7b9d; width: 700px; text-align: left;">
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
                                        <td>Estatus:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DDL_esquemaestatus" runat="server" CssClass="datos" TabIndex="13"  AutoPostBack="True">
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
                            <div style="border: solid 1px #5d7b9d; width: 700px; text-align: left;">
                                <table border="0" cellpadding="2" cellspacing="1">
                                    <tr>
                                        <td>Fecha Entrega:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txt_fe_dd" runat="server" Width="20px" CssClass="datos" MaxLength="2"
                                                TabIndex="14"></asp:TextBox>/
                                        <cc1:TextBoxWatermarkExtender ID="txt_fe_dd_TextBoxWatermarkExtender" runat="server"
                                            TargetControlID="txt_fe_dd" WatermarkText="dd">
                                        </cc1:TextBoxWatermarkExtender>
                                            <cc1:FilteredTextBoxExtender ID="txt_fe_dd_FilteredTextBoxExtender" runat="server"
                                                TargetControlID="txt_fe_dd" ValidChars="0123456789">
                                            </cc1:FilteredTextBoxExtender>
                                            <asp:TextBox ID="txt_fe_mm" runat="server" Width="20px" CssClass="datos" MaxLength="2"
                                                TabIndex="15"></asp:TextBox>/
                                        <cc1:TextBoxWatermarkExtender ID="txt_fe_mm_TextBoxWatermarkExtender" runat="server"
                                            TargetControlID="txt_fe_mm" WatermarkText="mm">
                                        </cc1:TextBoxWatermarkExtender>
                                            <cc1:FilteredTextBoxExtender ID="txt_fe_mm_FilteredTextBoxExtender" runat="server"
                                                TargetControlID="txt_fe_mm" ValidChars="0123456789">
                                            </cc1:FilteredTextBoxExtender>
                                            <asp:TextBox ID="txt_fe_yy" runat="server" Width="20px" CssClass="datos" MaxLength="2"
                                                TabIndex="16"></asp:TextBox>
                                            <cc1:TextBoxWatermarkExtender ID="txt_fe_yy_TextBoxWatermarkExtender" runat="server"
                                                TargetControlID="txt_fe_yy" WatermarkText="aa">
                                            </cc1:TextBoxWatermarkExtender>
                                            <cc1:FilteredTextBoxExtender ID="txt_fe_yy_FilteredTextBoxExtender" runat="server"
                                                TargetControlID="txt_fe_yy" ValidChars="0123456789">
                                            </cc1:FilteredTextBoxExtender>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div style="border: solid 1px #5d7b9d; width: 700px;">
                                <table border="0" cellpadding="2" cellspacing="1" style="width: 700px;">
                                    <tr>
                                        <th colspan="14" style="background-color: #5d7b9d; color: #ffffff;">MEDICAMENTOS
                                        </th>
                                    </tr>
                                    <tr>
                                        <td style="background-color: #e9ecf1; text-align: center; width: 91px;" colspan="2">Código
                                        </td>
                                        <td style="background-color: #e9ecf1; text-align: center; width: 36px;">Cant.
                                        </td>
                                        <td style="background-color: #e9ecf1; text-align: center; width: 78px;">Dósis
                                        </td>
                                        <td style="background-color: #e9ecf1; text-align: center; width: 46px;">Freq.
                                        </td>
                                        <td style="background-color: #e9ecf1; text-align: center; width: 59px;">Estatus
                                        </td>
                                        <td style="background-color: #e9ecf1; text-align: center; width: 46px;">Extras
                                        </td>
                                        <td style="background-color: #e9ecf1; text-align: center; width: 91px;" colspan="2">Código
                                        </td>
                                        <td style="background-color: #e9ecf1; text-align: center; width: 36px;">Cant.
                                        </td>
                                        <td style="background-color: #e9ecf1; text-align: center; width: 78px;">Dósis
                                        </td>
                                        <td style="background-color: #e9ecf1; text-align: center; width: 46px;">Freq.
                                        </td>
                                        <td style="background-color: #e9ecf1; text-align: center; width: 59px;">Estatus
                                        </td>
                                        <td style="background-color: #e9ecf1; text-align: center; width: 46px;">Extras
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span class="numeracion">#1</span>
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:DropDownList ID="DDL_cod1" runat="server" CssClass="datos" TabIndex="17" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:TextBox ID="txt_cant1" runat="server" CssClass="datosN" MaxLength="3" TabIndex="18"
                                                Width="25px">0</asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="txt_cant1_FilteredTextBoxExtender" runat="server"
                                                TargetControlID="txt_cant1" ValidChars="0123456789">
                                            </cc1:FilteredTextBoxExtender>
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:TextBox ID="txt_dx1" runat="server" CssClass="datos" MaxLength="10" TabIndex="19"
                                                Width="65px"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="txt_dx1_FilteredTextBoxExtender" runat="server"
                                                TargetControlID="txt_dx1" ValidChars="0123456789/">
                                            </cc1:FilteredTextBoxExtender>
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:DropDownList ID="DDL_fx1" runat="server" CssClass="datos" TabIndex="20">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:DropDownList ID="DDL_earv1" runat="server" CssClass="datos" TabIndex="21">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:TextBox ID="txt_uecant1" runat="server" CssClass="datosN" MaxLength="3" TabIndex="22"
                                                Width="25px">0</asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="txt_uecant1_FilteredTextBoxExtender" runat="server"
                                                TargetControlID="txt_uecant1" ValidChars="0123456789">
                                            </cc1:FilteredTextBoxExtender>
                                        </td>
                                        <td>
                                            <span class="numeracion">#2</span>
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:DropDownList ID="DDL_cod2" runat="server" CssClass="datos" TabIndex="23" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:TextBox ID="txt_cant2" runat="server" CssClass="datosN" MaxLength="3" TabIndex="24"
                                                Width="25px">0</asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="txt_cant2_FilteredTextBoxExtender" runat="server"
                                                TargetControlID="txt_cant2" ValidChars="0123456789">
                                            </cc1:FilteredTextBoxExtender>
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:TextBox ID="txt_dx2" runat="server" CssClass="datos" MaxLength="10" TabIndex="25"
                                                Width="65px"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="txt_dx2_FilteredTextBoxExtender" runat="server"
                                                TargetControlID="txt_dx2" ValidChars="0123456789/">
                                            </cc1:FilteredTextBoxExtender>
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:DropDownList ID="DDL_fx2" runat="server" CssClass="datos" TabIndex="26">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: center; width: 60px;">
                                            <asp:DropDownList ID="DDL_earv2" runat="server" CssClass="datos" TabIndex="27">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: center; width: 60px;">
                                            <asp:TextBox ID="txt_uecant2" runat="server" CssClass="datosN" MaxLength="3" TabIndex="28"
                                                Width="25px">0</asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="txt_uecant2_FilteredTextBoxExtender" runat="server"
                                                TargetControlID="txt_uecant2" ValidChars="0123456789">
                                            </cc1:FilteredTextBoxExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span class="numeracion">#3</span>
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:DropDownList ID="DDL_cod3" runat="server" CssClass="datos" TabIndex="29" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:TextBox ID="txt_cant3" runat="server" CssClass="datosN" MaxLength="3" TabIndex="30"
                                                Width="25px">0</asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="txt_cant3_FilteredTextBoxExtender" runat="server"
                                                TargetControlID="txt_cant3" ValidChars="0123456789">
                                            </cc1:FilteredTextBoxExtender>
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:TextBox ID="txt_dx3" runat="server" CssClass="datos" MaxLength="10" TabIndex="31"
                                                Width="65px"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="txt_dx3_FilteredTextBoxExtender" runat="server"
                                                TargetControlID="txt_dx3" ValidChars="0123456789/">
                                            </cc1:FilteredTextBoxExtender>
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:DropDownList ID="DDL_fx3" runat="server" CssClass="datos" TabIndex="32">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: center; width: 60px;">
                                            <asp:DropDownList ID="DDL_earv3" runat="server" CssClass="datos" TabIndex="33">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: center; width: 60px;">
                                            <asp:TextBox ID="txt_uecant3" runat="server" CssClass="datosN" MaxLength="3" TabIndex="34"
                                                Width="25px">0</asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="txt_uecant3_FilteredTextBoxExtender" runat="server"
                                                TargetControlID="txt_uecant3" ValidChars="0123456789">
                                            </cc1:FilteredTextBoxExtender>
                                        </td>
                                        <td>
                                            <span class="numeracion">#4</span>
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:DropDownList ID="DDL_cod4" runat="server" CssClass="datos" TabIndex="35" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:TextBox ID="txt_cant4" runat="server" CssClass="datosN" MaxLength="3" TabIndex="36"
                                                Width="25px">0</asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="txt_cant4_FilteredTextBoxExtender" runat="server"
                                                TargetControlID="txt_cant4" ValidChars="0123456789">
                                            </cc1:FilteredTextBoxExtender>
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:TextBox ID="txt_dx4" runat="server" CssClass="datos" MaxLength="10" TabIndex="37"
                                                Width="65px"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="txt_dx4_FilteredTextBoxExtender" runat="server"
                                                TargetControlID="txt_dx4" ValidChars="0123456789/">
                                            </cc1:FilteredTextBoxExtender>
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:DropDownList ID="DDL_fx4" runat="server" CssClass="datos" TabIndex="38">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: center; width: 60px;">
                                            <asp:DropDownList ID="DDL_earv4" runat="server" CssClass="datos" TabIndex="39">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: center; width: 60px;">
                                            <asp:TextBox ID="txt_uecant4" runat="server" CssClass="datosN" MaxLength="3" TabIndex="40"
                                                Width="25px">0</asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="txt_uecant4_FilteredTextBoxExtender" runat="server"
                                                TargetControlID="txt_uecant4" ValidChars="0123456789">
                                            </cc1:FilteredTextBoxExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span class="numeracion">#5</span>
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:DropDownList ID="DDL_cod5" runat="server" CssClass="datos" TabIndex="41" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:TextBox ID="txt_cant5" runat="server" CssClass="datosN" MaxLength="3" TabIndex="42"
                                                Width="25px">0</asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="txt_cant5_FilteredTextBoxExtender" runat="server"
                                                TargetControlID="txt_cant5" ValidChars="0123456789">
                                            </cc1:FilteredTextBoxExtender>
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:TextBox ID="txt_dx5" runat="server" CssClass="datos" MaxLength="10" TabIndex="43"
                                                Width="65px"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="txt_dx5FilteredTextBoxExtender" runat="server"
                                                TargetControlID="txt_dx5" ValidChars="0123456789/">
                                            </cc1:FilteredTextBoxExtender>
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:DropDownList ID="DDL_fx5" runat="server" CssClass="datos" TabIndex="44">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: center; width: 60px;">
                                            <asp:DropDownList ID="DDL_earv5" runat="server" CssClass="datos" TabIndex="45">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: center; width: 60px;">
                                            <asp:TextBox ID="txt_uecant5" runat="server" CssClass="datosN" MaxLength="3" TabIndex="46"
                                                Width="25px">0</asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="txt_uecant5_FilteredTextBoxExtender" runat="server"
                                                TargetControlID="txt_uecant5" ValidChars="0123456789">
                                            </cc1:FilteredTextBoxExtender>
                                        </td>
                                        <td>
                                            <span class="numeracion">#6</span>
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:DropDownList ID="DDL_cod6" runat="server" CssClass="datos" TabIndex="47" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:TextBox ID="txt_cant6" runat="server" CssClass="datosN" MaxLength="3" TabIndex="48"
                                                Width="25px">0</asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="txt_cant6_FilteredTextBoxExtender" runat="server"
                                                TargetControlID="txt_cant6" ValidChars="0123456789">
                                            </cc1:FilteredTextBoxExtender>
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:TextBox ID="txt_dx6" runat="server" CssClass="datos" MaxLength="10" TabIndex="49"
                                                Width="65px"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="txt_dx6_FilteredTextBoxExtender" runat="server"
                                                TargetControlID="txt_dx6" ValidChars="0123456789/">
                                            </cc1:FilteredTextBoxExtender>
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:DropDownList ID="DDL_fx6" runat="server" CssClass="datos" TabIndex="50">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: center; width: 60px;">
                                            <asp:DropDownList ID="DDL_earv6" runat="server" CssClass="datos" TabIndex="51">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: center; width: 60px;">
                                            <asp:TextBox ID="txt_uecant6" runat="server" CssClass="datosN" MaxLength="3" TabIndex="52"
                                                Width="25px">0</asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="txt_uecant6_FilteredTextBoxExtender" runat="server"
                                                TargetControlID="txt_uecant6" ValidChars="0123456789">
                                            </cc1:FilteredTextBoxExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span class="numeracion">#7</span>
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:DropDownList ID="DDL_cod7" runat="server" CssClass="datos" TabIndex="53" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:TextBox ID="txt_cant7" runat="server" CssClass="datosN" MaxLength="3" TabIndex="54"
                                                Width="25px">0</asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="txt_cant7_FilteredTextBoxExtender" runat="server"
                                                TargetControlID="txt_cant7" ValidChars="0123456789">
                                            </cc1:FilteredTextBoxExtender>
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:TextBox ID="txt_dx7" runat="server" CssClass="datos" MaxLength="10" TabIndex="55"
                                                Width="65px"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="txt_dx7_FilteredTextBoxExtender" runat="server"
                                                TargetControlID="txt_dx7" ValidChars="0123456789/">
                                            </cc1:FilteredTextBoxExtender>
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:DropDownList ID="DDL_fx7" runat="server" CssClass="datos" TabIndex="56">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: center; width: 60px;">
                                            <asp:DropDownList ID="DDL_earv7" runat="server" CssClass="datos" TabIndex="57">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: center; width: 60px;">
                                            <asp:TextBox ID="txt_uecant7" runat="server" CssClass="datosN" MaxLength="3" TabIndex="58"
                                                Width="25px">0</asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="txt_uecant7_FilteredTextBoxExtender" runat="server"
                                                TargetControlID="txt_uecant7" ValidChars="0123456789">
                                            </cc1:FilteredTextBoxExtender>
                                        </td>
                                        <td>
                                            <span class="numeracion">#8</span>
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:DropDownList ID="DDL_cod8" runat="server" CssClass="datos" TabIndex="59" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:TextBox ID="txt_cant8" runat="server" CssClass="datosN" MaxLength="3" TabIndex="60"
                                                Width="25px">0</asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="txt_cant8_FilteredTextBoxExtender" runat="server"
                                                TargetControlID="txt_cant8" ValidChars="0123456789">
                                            </cc1:FilteredTextBoxExtender>
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:TextBox ID="txt_dx8" runat="server" CssClass="datos" MaxLength="10" TabIndex="61"
                                                Width="65px"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="txt_dx8_FilteredTextBoxExtender" runat="server"
                                                TargetControlID="txt_dx8" ValidChars="0123456789/">
                                            </cc1:FilteredTextBoxExtender>
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:DropDownList ID="DDL_fx8" runat="server" CssClass="datos" TabIndex="62">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: center; width: 60px;">
                                            <asp:DropDownList ID="DDL_earv8" runat="server" CssClass="datos" TabIndex="63">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: center; width: 60px;">
                                            <asp:TextBox ID="txt_uecant8" runat="server" CssClass="datosN" MaxLength="3" TabIndex="64"
                                                Width="25px">0</asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="txt_uecant8_FilteredTextBoxExtender" runat="server"
                                                TargetControlID="txt_uecant8" ValidChars="0123456789">
                                            </cc1:FilteredTextBoxExtender>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div style="border: solid 1px #5d7b9d; width: 700px; text-align: left;">
                                <table border="0" cellpadding="2" cellspacing="1">
                                    <tr>
                                        <td>Fecha Retorno:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txt_fr_dd" runat="server" Width="20px" CssClass="datos" MaxLength="2"
                                                TabIndex="65"></asp:TextBox>/
                                        <cc1:TextBoxWatermarkExtender ID="txt_fr_dd_TextBoxWatermarkExtender" runat="server"
                                            TargetControlID="txt_fr_dd" WatermarkText="dd">
                                        </cc1:TextBoxWatermarkExtender>
                                            <cc1:FilteredTextBoxExtender ID="txt_fr_dd_FilteredTextBoxExtender" runat="server"
                                                TargetControlID="txt_fr_dd" ValidChars="0123456789">
                                            </cc1:FilteredTextBoxExtender>
                                            <asp:TextBox ID="txt_fr_mm" runat="server" Width="20px" CssClass="datos" MaxLength="2"
                                                TabIndex="66"></asp:TextBox>/
                                        <cc1:TextBoxWatermarkExtender ID="txt_fr_mm_TextBoxWatermarkExtender" runat="server"
                                            TargetControlID="txt_fr_mm" WatermarkText="mm">
                                        </cc1:TextBoxWatermarkExtender>
                                            <cc1:FilteredTextBoxExtender ID="txt_fr_mm_FilteredTextBoxExtender" runat="server"
                                                TargetControlID="txt_fr_mm" ValidChars="0123456789">
                                            </cc1:FilteredTextBoxExtender>
                                            <asp:TextBox ID="txt_fr_yy" runat="server" Width="20px" CssClass="datos" MaxLength="2"
                                                TabIndex="67"></asp:TextBox>
                                            <cc1:TextBoxWatermarkExtender ID="txt_fr_yy_TextBoxWatermarkExtender2" runat="server"
                                                TargetControlID="txt_fr_yy" WatermarkText="aa">
                                            </cc1:TextBoxWatermarkExtender>
                                            <cc1:FilteredTextBoxExtender ID="txt_fr_yy_FilteredTextBoxExtender" runat="server"
                                                TargetControlID="txt_fr_yy" ValidChars="0123456789">
                                            </cc1:FilteredTextBoxExtender>
                                        </td>
                                        <td style="border-left: solid 1px #5d7b9d;">Tiempo de TARV en Días: 
                                        <asp:Button ID="btn_dias_retorno" runat="server" Text="CALCULAR" TabIndex="75" CssClass="button" Visible="True" />							 
                        
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txt_tarvdias" runat="server" Width="20px" CssClass="datosN" MaxLength="3"
                                                TabIndex="68">0</asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="txt_tarvdias_FilteredTextBoxExtender" runat="server"
                                                TargetControlID="txt_tarvdias" ValidChars="0123456789">
                                            </cc1:FilteredTextBoxExtender>
                                        </td>
                                        <td style="border-left: solid 1px #5d7b9d;">Cita:
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="CB_citaFx" runat="server" CausesValidation="false" Text="Farmacia"
                                                TabIndex="69" Checked="True" />
                                            <asp:CheckBox ID="CB_citaMx" runat="server" CausesValidation="false" Text="Médica"
                                                TabIndex="70" />
                                        </td>
                                        <td style="border-left: solid 1px #5d7b9d;">Embarazo:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DDL_embarazo" runat="server" CssClass="datos" TabIndex="71">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div style="border: solid 1px #5d7b9d; width: 700px; text-align: left;">
                                <table>
                                    <tr>
                                        <td>
                                            Fechas proxima cita, Trabajo Social:
                                        </td>
                                        <td colspan="2" align="left">
                                            <asp:Label ID="lbl_proximacitaTS" runat="server" ></asp:Label>
                                        </td>
                                        <td>
                                            Mangua:
                                        </td>
                                        <td colspan="2" align="left">
                                            <asp:Label ID="lbl_proximacitaMangua" runat="server" ></asp:Label>
                                        </td>
                                        <td>
                                            Dias para cita:
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_diasparacita" runat="server" ></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div style="border: solid 1px #5d7b9d; width: 700px; text-align: left;">
                                <table border="0" cellpadding="2" cellspacing="1">
                                    <tr style="vertical-align: top;">
                                        <td>Observaciones:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txt_observaciones" runat="server" CssClass="datosM" TabIndex="72"
                                                TextMode="MultiLine" Width="430px" Rows="3"></asp:TextBox>
                                        </td>
                                        <td style="border-left: solid 1px #5d7b9d;">CD4:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txt_cd4" runat="server" Width="40px" CssClass="datosN" MaxLength="6"
                                                TabIndex="73"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="txt_cd4_FilteredTextBoxExtender" runat="server"
                                                TargetControlID="txt_cd4" ValidChars="0123456789<>">
                                            </cc1:FilteredTextBoxExtender>
                                        </td>
                                        <td style="border-left: solid 1px #5d7b9d;">CV:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txt_cv" runat="server" Width="65px" CssClass="datosN" MaxLength="10"
                                                TabIndex="74"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="txt_cv_FilteredTextBoxExtender" runat="server" TargetControlID="txt_cv"
                                                ValidChars="0123456789<>BDLNbdln">
                                            </cc1:FilteredTextBoxExtender>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding: 5px 5px 5px 0px;">
                            <asp:Button ID="btn_validar" runat="server" Text="VALIDAR" CssClass="button" />
                            <asp:Button ID="btn_grabar" runat="server" Text="GRABAR" TabIndex="75" CssClass="button" Visible="false" />							 
                        </td>
                    </tr>
					<tr>
						<td>
							<asp:TextBox ID="txtMessage" runat="server" Enabled="false"></asp:TextBox>
						</td>
					</tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>



    <script type="text/javascript" src="Scripts/websdk.client.bundle.min.js"></script>
    <script type="text/javascript" src="Scripts/fingerprint.sdk.min.js"></script>
    <script type="text/javascript" src="Scripts/identapp.js"></script>
    <script type="text/javascript" src="Scripts/bootstrap.min.js"></script>
    <script type="text/javascript" src="Scripts/bootbox.min.js"></script>
    <script type="text/javascript" src="Scripts/modalwindow.js"></script>
    <script type="text/javascript" src="Scripts/beforeCancelPostBack.js"></script>




    <script type="text/javascript">

        function ValidateNHC()
        {
            debugger;
            
            if ($("input[name*='txt_asi']")[1].value === "") {
                var msg = "Valor requerido NHC";
                getAlertCircuit(msg);
                return true;
            }
            else {
                return false;
            }
        }


        function IniciarAtencion() {

            debugger;
            $("input[name*='lblnhc']")[0].value = $("input[name*='vfNHC']")[0].value;
            $('#<%=txt_asi.ClientID%>').val($("input[name*='vfNHC']")[0].value);

            if (ValidateNHC() === true)
                return;

            var clickButton = document.getElementById('<%= btn_init.ClientID %>');
            clickButton.click();
        }

        function FinalizarAtencion() {
            debugger;


            $("input[name*='lblnhc']")[0].value = $("input[name*='vfNHC']")[0].value;
            $('#<%=txt_asi.ClientID%>').val($("input[name*='vfNHC']")[0].value);

            if (ValidateNHC() === true)
                return;

            __doPostBack("<%= btn_end.ClientID%>", '');
        }

    </script>



    <script type="text/javascript">
        $("input[name*='txt_asi']").on('change', function (e) {
            debugger;

            $("input[name*='lblnhc']")[0].value = $("input[name*='txt_asi']")[1].value;
        });

    </script>

    <script type="text/javascript">

        window.addEventListener("keypress", function (event) {
            if (event.keyCode == 13) {
                event.preventDefault();
            }
        }, false);

    </script>
    <script type="text/javascript">

        $(document).ready(function () {
            debugger;
            $('[id$="page_titulo"]').html("INGRESO CONSUMO DE ARVS");
            var myDiv = document.querySelector('[id$="DatosPaciente"]');
            myDiv.style.display = 'block';


        });

    </script>

    <script type="text/javascript">
        Sys.Browser.WebKit = {};
        if (navigator.userAgent.indexOf('WebKit/') > -1) {
            Sys.Browser.agent = Sys.Browser.WebKit;
            Sys.Browser.version = parseFloat(navigator.userAgent.match(/WebKit\/(\d+(\.\d+)?)/)[1]);
            Sys.Browser.name = 'WebKit';
        }
    </script>

</asp:Content>
