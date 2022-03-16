<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false"
    CodeFile="consultaARV.aspx.vb" Inherits="inicio" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
        <div style="width: 700px; border: solid 1px #5d7b9d; text-align:left;">
            <table id="tblbasal" border="0" cellpadding="2" cellspacing="1">
                <tr>
                    <th colspan="4" class="theader">CONSULTA CONSUMO DE ARVS</th>
                </tr>
                <tr>
                    <td style="width: 100px; background-color: #5d7b9d; color: #ffffff;">
                        Número ASI:
                    </td>
                    <td style="width: 230px; background-color: #e9ecf1; padding:0px;">
                        <table id="tblNHC" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_asi" runat="server" CssClass="NHClbl" MaxLength="7" Width="64px"></asp:Label>
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
                    </td>
                </tr>
            </table>
            &nbsp;<asp:Label ID="lbl_error" runat="server" CssClass="error"></asp:Label>
        </div>
        <div id="divingreso" runat="server" visible="false">
            <table border="0" cellpadding="0" cellspacing="1">

            </table>
            <table border="0" cellpadding="0" cellspacing="1">
                <tr>
                    <td>
                        <div style="border: solid 1px #5d7b9d; width: 700px; text-align:left;">
                        <table border="0" cellpadding="2" cellspacing="1">
                            <tr>
                                <td>
                                    Fecha Entrega:
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_fe_dd" runat="server" Width="20px" CssClass="datos" MaxLength="2"
                                        TabIndex="7"></asp:TextBox>/
                                    <cc1:TextBoxWatermarkExtender ID="txt_fe_dd_TextBoxWatermarkExtender" runat="server"
                                        TargetControlID="txt_fe_dd" WatermarkText="dd">
                                    </cc1:TextBoxWatermarkExtender>
                                    <cc1:FilteredTextBoxExtender ID="txt_fe_dd_FilteredTextBoxExtender" runat="server"
                                        TargetControlID="txt_fe_dd" ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                    <asp:TextBox ID="txt_fe_mm" runat="server" Width="20px" CssClass="datos" MaxLength="2"
                                        TabIndex="8"></asp:TextBox>/
                                    <cc1:TextBoxWatermarkExtender ID="txt_fe_mm_TextBoxWatermarkExtender" runat="server"
                                        TargetControlID="txt_fe_mm" WatermarkText="mm">
                                    </cc1:TextBoxWatermarkExtender>
                                    <cc1:FilteredTextBoxExtender ID="txt_fe_mm_FilteredTextBoxExtender" runat="server"
                                        TargetControlID="txt_fe_mm" ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                    <asp:TextBox ID="txt_fe_yy" runat="server" Width="20px" CssClass="datos" MaxLength="2"
                                        TabIndex="9"></asp:TextBox>
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
                        <div style="border: solid 1px #5d7b9d; width:700px;">
                        <table border="0" cellpadding="2" cellspacing="1" style="width: 700px;">
                            <tr>
                                <th colspan="10" style="background-color: #5d7b9d; color: #ffffff;">
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
                            </tr>
                            <tr>
                                <td>
                                    <span class="numeracion">#1</span>
                                </td>
                                <td style="text-align:center;">
                                    <asp:DropDownList ID="DDL_cod1" runat="server" CssClass="datos" TabIndex="10" 
                                        AutoPostBack="True">
                                    </asp:DropDownList>
                                </td>
                                <td style="text-align:center;">
                                    <asp:TextBox ID="txt_cant1" runat="server" CssClass="datos" MaxLength="3" TabIndex="11"
                                        Width="25px"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="txt_cant1_FilteredTextBoxExtender" runat="server"
                                        TargetControlID="txt_cant1" ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                                <td style="text-align:center;">
                                    <asp:TextBox ID="txt_dx1" runat="server" CssClass="datos" MaxLength="9" TabIndex="12"
                                        Width="65px"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="txt_dx1_FilteredTextBoxExtender" runat="server"
                                        TargetControlID="txt_dx1" ValidChars="0123456789/">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                                <td style="text-align:center;">
                                    <asp:DropDownList ID="DDL_fx1" runat="server" CssClass="datos" TabIndex="13">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <span class="numeracion">#2</span>
                                </td>
                                <td style="text-align:center;">
                                    <asp:DropDownList ID="DDL_cod2" runat="server" CssClass="datos" TabIndex="14" AutoPostBack="True">
                                    </asp:DropDownList>
                                </td>
                                <td style="text-align:center;">
                                    <asp:TextBox ID="txt_cant2" runat="server" CssClass="datos" MaxLength="3" TabIndex="15"
                                        Width="25px"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="txt_cant2_FilteredTextBoxExtender" runat="server"
                                        TargetControlID="txt_cant2" ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                                <td style="text-align:center;">
                                    <asp:TextBox ID="txt_dx2" runat="server" CssClass="datos" MaxLength="9" TabIndex="16"
                                        Width="65px"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="txt_dx2_FilteredTextBoxExtender" runat="server"
                                        TargetControlID="txt_dx2" ValidChars="0123456789/">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                                <td style="text-align:center;">
                                    <asp:DropDownList ID="DDL_fx2" runat="server" CssClass="datos" TabIndex="17">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <span class="numeracion">#3</span>
                                </td>
                                <td style="text-align:center;">
                                    <asp:DropDownList ID="DDL_cod3" runat="server" CssClass="datos" TabIndex="18" AutoPostBack="True">
                                    </asp:DropDownList>
                                </td>
                                <td style="text-align:center;">
                                    <asp:TextBox ID="txt_cant3" runat="server" CssClass="datos" MaxLength="3" TabIndex="19"
                                        Width="25px"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="txt_cant3_FilteredTextBoxExtender" runat="server"
                                        TargetControlID="txt_cant3" ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                                <td style="text-align:center;">
                                    <asp:TextBox ID="txt_dx3" runat="server" CssClass="datos" MaxLength="9" TabIndex="20"
                                        Width="65px"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="txt_dx3_FilteredTextBoxExtender" runat="server"
                                        TargetControlID="txt_dx3" ValidChars="0123456789/">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                                <td style="text-align:center;">
                                    <asp:DropDownList ID="DDL_fx3" runat="server" CssClass="datos" TabIndex="21">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <span class="numeracion">#4</span>
                                </td>
                                <td style="text-align:center;">
                                    <asp:DropDownList ID="DDL_cod4" runat="server" CssClass="datos" TabIndex="22" AutoPostBack="True">
                                    </asp:DropDownList>
                                </td>
                                <td style="text-align:center;">
                                    <asp:TextBox ID="txt_cant4" runat="server" CssClass="datos" MaxLength="3" TabIndex="23"
                                        Width="25px"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="txt_cant4_FilteredTextBoxExtender" runat="server"
                                        TargetControlID="txt_cant4" ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                                <td style="text-align:center;">
                                    <asp:TextBox ID="txt_dx4" runat="server" CssClass="datos" MaxLength="9" TabIndex="24"
                                        Width="65px"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="txt_dx4_FilteredTextBoxExtender" runat="server"
                                        TargetControlID="txt_dx4" ValidChars="0123456789/">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                                <td style="text-align:center;">
                                    <asp:DropDownList ID="DDL_fx4" runat="server" CssClass="datos" TabIndex="25">
                                    </asp:DropDownList>
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
                                    <asp:TextBox ID="txt_fr_yy" runat="server" Width="20px" CssClass="datos" MaxLength="2"
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
                        <div style="border: solid 1px #5d7b9d; width:700px;">
                        <table border="0" cellpadding="2" cellspacing="1" style="width: 700px;">
                            <tr>
                                <th colspan="12" style="background-color: #5d7b9d; color: #ffffff;">
                                    UNIDADES EXTRA
                                </th>
                            </tr>
                            <tr>
                                <td style="background-color: #e9ecf1; text-align:center;" colspan="2">Código</td>
                                <td style="background-color: #e9ecf1; text-align:center;">Cantidad</td>
                                <td style="background-color: #e9ecf1; text-align:center;" colspan="2">Código</td>
                                <td style="background-color: #e9ecf1; text-align:center;">Cantidad</td>
                                <td style="background-color: #e9ecf1; text-align:center;" colspan="2">Código</td>
                                <td style="background-color: #e9ecf1; text-align:center;">Cantidad</td>
                                <td style="background-color: #e9ecf1; text-align:center;" colspan="2">Código</td>
                                <td style="background-color: #e9ecf1; text-align:center;">Cantidad</td>
                            </tr>
                            <tr>
                                <td>
                                    <span class="numeracion">#1</span>
                                </td>
                                <td style="width:60px; text-align:center;">
                                    <asp:Label ID="lbl_uecod1" runat="server" CssClass="datos1"></asp:Label>
                                </td>
                                <td style="text-align:center;">
                                    <asp:TextBox ID="txt_uecant1" runat="server" CssClass="datos" MaxLength="3" TabIndex="32"
                                        Width="25px"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="txt_uecant1_FilteredTextBoxExtender" runat="server"
                                        TargetControlID="txt_uecant1" ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                                <td>
                                    <span class="numeracion">#2</span>
                                </td>
                                <td style="width:60px; text-align:center;">
                                    <asp:Label ID="lbl_uecod2" runat="server" CssClass="datos1"></asp:Label>
                                </td>
                                <td style="text-align:center;">
                                    <asp:TextBox ID="txt_uecant2" runat="server" CssClass="datos" MaxLength="3" TabIndex="33"
                                        Width="25px"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="txt_uecant2_FilteredTextBoxExtender" runat="server"
                                        TargetControlID="txt_uecant2" ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                                <td>
                                    <span class="numeracion">#3</span>
                                </td>
                                <td style="width:60px; text-align:center;">
                                    <asp:Label ID="lbl_uecod3" runat="server" CssClass="datos1"></asp:Label>
                                </td>
                                <td style="text-align:center;">
                                    <asp:TextBox ID="txt_uecant3" runat="server" CssClass="datos" MaxLength="3" TabIndex="34"
                                        Width="25px"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="txt_uecant3_FilteredTextBoxExtender" runat="server"
                                        TargetControlID="txt_uecant3" ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                                <td>
                                    <span class="numeracion">#4</span>
                                </td>
                                <td style="width:60px; text-align:center;">
                                    <asp:Label ID="lbl_uecod4" runat="server" CssClass="datos1"></asp:Label>
                                </td>
                                <td style="text-align:center;">
                                    <asp:TextBox ID="txt_uecant4" runat="server" CssClass="datos" MaxLength="3" TabIndex="35"
                                        Width="25px"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="txt_uecant4_FilteredTextBoxExtender" runat="server"
                                        TargetControlID="txt_uecant4" ValidChars="0123456789">
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
                        <div style="border: solid 1px #5d7b9d; width:700px;">
                        <table border="0" cellpadding="2" cellspacing="1" style="width: 700px;">
                            <tr>
                                <th colspan="12" style="background-color: #5d7b9d; color: #ffffff;">
                                    ESTATUS USO DE ARV
                                </th>
                            </tr>
                            <tr>
                                <td style="background-color: #e9ecf1; text-align:center;" colspan="2">Código</td>
                                <td style="background-color: #e9ecf1; text-align:center;">Estatus</td>
                                <td style="background-color: #e9ecf1; text-align:center;" colspan="2">Código</td>
                                <td style="background-color: #e9ecf1; text-align:center;">Estatus</td>
                                <td style="background-color: #e9ecf1; text-align:center;" colspan="2">Código</td>
                                <td style="background-color: #e9ecf1; text-align:center;">Estatus</td>
                                <td style="background-color: #e9ecf1; text-align:center;" colspan="2">Código</td>
                                <td style="background-color: #e9ecf1; text-align:center;">Estatus</td>
                            </tr>
                            <tr>
                                <td>
                                    <span class="numeracion">#1</span>
                                </td>
                                <td style="width:60px; text-align:center;">
                                    <asp:Label ID="lbl_earvcod1" runat="server" CssClass="datos1"></asp:Label>
                                </td>
                                <td style="text-align:center;">
                                    <asp:DropDownList ID="DDL_earv1" runat="server" CssClass="datos" TabIndex="38">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <span class="numeracion">#2</span>
                                </td>
                                <td style="width:60px; text-align:center;">
                                    <asp:Label ID="lbl_earvcod2" runat="server" CssClass="datos1"></asp:Label>
                                </td>
                                <td style="text-align:center;">
                                    <asp:DropDownList ID="DDL_earv2" runat="server" CssClass="datos" TabIndex="39">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <span class="numeracion">#3</span>
                                </td>
                                <td style="width:60px; text-align:center;">
                                    <asp:Label ID="lbl_earvcod3" runat="server" CssClass="datos1"></asp:Label>
                                </td>
                                <td style="text-align:center;">
                                    <asp:DropDownList ID="DDL_earv3" runat="server" CssClass="datos" TabIndex="40">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <span class="numeracion">#4</span>
                                </td>
                                <td style="width:60px; text-align:center;">
                                    <asp:Label ID="lbl_earvcod4" runat="server" CssClass="datos1"></asp:Label>
                                </td>
                                <td style="text-align:center;">
                                    <asp:DropDownList ID="DDL_earv4" runat="server" CssClass="datos" TabIndex="41">
                                    </asp:DropDownList>
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
                                <th colspan="12" style="background-color: #5d7b9d; color: #ffffff;">
                                    DEVOLUCIONES
                                </th>
                            </tr>
                            <tr>
                                <td style="background-color: #e9ecf1; text-align:center;" colspan="2">Código</td>
                                <td style="background-color: #e9ecf1; text-align:center;">Cantidad</td>
                                <td style="background-color: #e9ecf1; text-align:center;" colspan="2">Código</td>
                                <td style="background-color: #e9ecf1; text-align:center;">Cantidad</td>
                                <td style="background-color: #e9ecf1; text-align:center;" colspan="2">Código</td>
                                <td style="background-color: #e9ecf1; text-align:center;">Cantidad</td>
                                <td style="background-color: #e9ecf1; text-align:center;" colspan="2">Código</td>
                                <td style="background-color: #e9ecf1; text-align:center;">Cantidad</td>
                            </tr>
                            <tr>
                                <td>
                                    <span class="numeracion">#1</span>
                                </td>
                                <td style="width:60px; text-align:center;">
                                    <asp:Label ID="lbl_devcod1" runat="server" CssClass="datos2"></asp:Label>
                                </td>
                                <td style="text-align:center;">
                                    <asp:TextBox ID="txt_devcant1" runat="server" CssClass="datos" MaxLength="3" TabIndex="2"
                                        Width="25px"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="txt_devcant1_FilteredTextBoxExtender" runat="server"
                                        TargetControlID="txt_devcant1" ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                                <td>
                                    <span class="numeracion">#2</span>
                                </td>
                                <td style="width:60px; text-align:center;">
                                    <asp:Label ID="lbl_devcod2" runat="server" CssClass="datos2"></asp:Label>
                                </td>
                                <td style="text-align:center;">
                                    <asp:TextBox ID="txt_devcant2" runat="server" CssClass="datos" MaxLength="3" TabIndex="3"
                                        Width="25px"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="txt_devcant2_FilteredTextBoxExtender" runat="server"
                                        TargetControlID="txt_devcant2" ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                                <td>
                                    <span class="numeracion">#3</span>
                                </td>
                                <td style="width:60px; text-align:center;">
                                    <asp:Label ID="lbl_devcod3" runat="server" CssClass="datos2"></asp:Label>
                                </td>
                                <td style="text-align:center;">
                                    <asp:TextBox ID="txt_devcant3" runat="server" CssClass="datos" MaxLength="3" TabIndex="4"
                                        Width="25px"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="txt_devcant3_FilteredTextBoxExtender" runat="server"
                                        TargetControlID="txt_devcant3" ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                                <td>
                                    <span class="numeracion">#4</span>
                                </td>
                                <td style="width:60px; text-align:center;">
                                    <asp:Label ID="lbl_devcod4" runat="server" CssClass="datos2"></asp:Label>
                                </td>
                                <td style="text-align:center;">
                                    <asp:TextBox ID="txt_devcant4" runat="server" CssClass="datos" MaxLength="3" TabIndex="5"
                                        Width="25px"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="txt_devcant4_FilteredTextBoxExtender" runat="server"
                                        TargetControlID="txt_devcant4" ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                            </tr>
                        </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                    <div style="border: solid 1px #333333; width: 700px; text-align:left;">
                        
                    </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div style="border: solid 1px #5d7b9d; width: 700px; text-align:left;">
                        <table border="0" cellpadding="2" cellspacing="1" >
                            <tr style=" vertical-align:top;">
                                <td>
                                    Adherencia:
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_adherencia" runat="server" Width="30px" CssClass="datos" MaxLength="5"
                                        TabIndex="6"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="txt_adherencia_FilteredTextBoxExtender" runat="server" TargetControlID="txt_adherencia"
                                        ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                                <td> %</td>
                                <td>
                                    Observaciones:
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_observaciones" runat="server" CssClass="datosM" TabIndex="42"
                                        TextMode="MultiLine" Width="325px" Rows="3"></asp:TextBox>
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
                <tr>
                    <td style="padding:5px 5px 5px 0px;">
                        <asp:Button ID="btn_grabar" runat="server" Text="GRABAR" TabIndex="45" CssClass="button" />
                    </td>
                </tr>
            </table>
            </div>
        
</asp:Content>
