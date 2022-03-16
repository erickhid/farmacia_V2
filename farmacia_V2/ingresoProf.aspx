<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="ingresoProf.aspx.vb" Inherits="ingresoProf" ValidateRequest="false" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="content_head" ContentPlaceHolderID="head_contentholder" runat="server">



    <link href="CSS/bootstrap.css" rel="stylesheet" />
    <link href="CSS/med.css" rel="stylesheet" />
    <link href="CSS/Custom-Cs.css" rel="stylesheet" />
    <link href="CSS/app.css" rel="stylesheet" />


</asp:Content>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">



    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" ScriptMode="Release">
        <%--<Scripts>
            <asp:ScriptReference Path="Scripts/cancelPostBack.js" />
        </Scripts> --%>
    </asp:ScriptManager>


    <asp:UpdatePanel ID="up_datospaciente" runat="server">
        <ContentTemplate>
            <div style="width: 700px; border: solid 1px #5d7b9d; text-align: left;">
                <table id="tblbasal" border="0" cellpadding="2" cellspacing="1">
                    <tr>
                        <%--<th colspan="4" class="theader">INGRESO CONSUMO DE PROFILAXIS</th>--%>
                    </tr>
                    <tr>
                        <td style="width: 100px; background-color: #5d7b9d; color: #ffffff;">Número ASI:
                        </td>
                        <td style="width: 230px; background-color: #e9ecf1; padding: 0px;">
                            <table id="tblNHC" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txt_asi" runat="server" CssClass="NHC" MaxLength="7" Width="64px"></asp:TextBox>
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
                                        <asp:ImageButton ID="btn_buscar" runat="server" ToolTip="BUSCAR" CausesValidation="false" ImageUrl="~/images/magnify-clip.png" />
                                        <asp:ImageButton ID="btn_editar" runat="server" ToolTip="EDITAR" CausesValidation="false" ImageUrl="~/images/file_edit.png" Visible="False" />
                                        <asp:ImageButton ID="btn_agregar" runat="server" ToolTip="AGREGAR" CausesValidation="false" ImageUrl="~/images/add.png" Visible="False" />
                                        <asp:Button ID="btn_search" runat="server" Text="Button" Visible="false" />

                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 100px; background-color: #5d7b9d; color: #ffffff;">Estatus MANGUA:
                        </td>
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
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="up_grabardatos" runat="server">
        <ContentTemplate>
            <div id="divingreso" runat="server" visible="false">
                <table border="0" cellpadding="0" cellspacing="1">
                    <tr>
                        <td>
                            <div style="border: solid 1px #333333; width: 700px; text-align: left;">
                                <table border="0" cellpadding="2" cellspacing="1">
                                    <tr>
                                        <td>Ultima Fecha Registrada:
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_ultimafechaentrega" runat="server" CssClass="datos1"></asp:Label>
                                        </td>
                                        <td style="padding-left: 10px;">Estatus FARMACIA:</td>
                                        <td>
                                            <asp:Label ID="lbl_estatusfarmacia" runat="server" CssClass="datos1"></asp:Label>
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
                            <div style="border: solid 1px #5d7b9d; width: 700px; text-align: left;">
                                <table border="0" cellpadding="2" cellspacing="1">
                                    <tr>
                                        <td>Fecha Entrega:
                                        </td>
                                        <td>
                                            <table cellpadding="0" cellspacing="0" class="datos">
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txt_fe_dd" runat="server" Width="18px" CssClass="datosfecha" MaxLength="2"></asp:TextBox>/
                                            <cc1:TextBoxWatermarkExtender ID="txt_fe_dd_TextBoxWatermarkExtender" runat="server"
                                                TargetControlID="txt_fe_dd" WatermarkText="dd">
                                            </cc1:TextBoxWatermarkExtender>
                                                        <cc1:FilteredTextBoxExtender ID="txt_fe_dd_FilteredTextBoxExtender" runat="server"
                                                            TargetControlID="txt_fe_dd" ValidChars="0123456789">
                                                        </cc1:FilteredTextBoxExtender>
                                                        <asp:TextBox ID="txt_fe_mm" runat="server" Width="18px" CssClass="datosfecha" MaxLength="2"></asp:TextBox>/
                                            <cc1:TextBoxWatermarkExtender ID="txt_fe_mm_TextBoxWatermarkExtender" runat="server"
                                                TargetControlID="txt_fe_mm" WatermarkText="mm">
                                            </cc1:TextBoxWatermarkExtender>
                                                        <cc1:FilteredTextBoxExtender ID="txt_fe_mm_FilteredTextBoxExtender" runat="server"
                                                            TargetControlID="txt_fe_mm" ValidChars="0123456789">
                                                        </cc1:FilteredTextBoxExtender>
                                                        <asp:TextBox ID="txt_fe_yy" runat="server" Width="18px" CssClass="datosfecha" MaxLength="2"></asp:TextBox>
                                                        <cc1:TextBoxWatermarkExtender ID="txt_fe_yy_TextBoxWatermarkExtender" runat="server"
                                                            TargetControlID="txt_fe_yy" WatermarkText="aa">
                                                        </cc1:TextBoxWatermarkExtender>
                                                        <cc1:FilteredTextBoxExtender ID="txt_fe_yy_FilteredTextBoxExtender" runat="server"
                                                            TargetControlID="txt_fe_yy" ValidChars="0123456789">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>CD4:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txt_cd4" runat="server" Width="40px" CssClass="datos" MaxLength="6"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="txt_cd4_FilteredTextBoxExtender" runat="server"
                                                TargetControlID="txt_cd4" ValidChars="0123456789">
                                            </cc1:FilteredTextBoxExtender>
                                        </td>
                                        <td style="padding: 0px 2px 0px 4px;">Tipo Paciente:
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
                            <div style="border: solid 1px #5d7b9d; width: 700px;">
                                <table border="0" cellpadding="1" cellspacing="1" style="width: 700px;">
                                    <tr>
                                        <th colspan="14" style="background-color: #5d7b9d; color: #ffffff;">MEDICAMENTOS
                                        </th>
                                    </tr>
                                    <tr>
                                        <td style="background-color: #e9ecf1; text-align: center; width: 82px;" colspan="2">Código
                                        </td>
                                        <td style="background-color: #e9ecf1; text-align: center; width: 180px;">Medicamento
                                        </td>
                                        <td style="background-color: #e9ecf1; text-align: center; width: 50px;">Dósis
                                        </td>
                                        <td style="background-color: #e9ecf1; text-align: center;">VIA
                                        </td>
                                        <td style="background-color: #e9ecf1; text-align: center; width: 38px;">Freq.
                                        </td>
                                        <td style="background-color: #e9ecf1; text-align: center; width: 146px;">Tipo
                                        </td>
                                        <td style="background-color: #e9ecf1; text-align: center; width: 50px;">Estatus
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span class="numeracion">#1</span>
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:DropDownList ID="DDL_cod1" runat="server" CssClass="datos" Width="60" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:Label ID="lbl_prof1" runat="server" CssClass="datos2"></asp:Label>
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:TextBox ID="txt_dx1" runat="server" Width="50px" CssClass="datosdx" MaxLength="12"></asp:TextBox>
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:DropDownList ID="DDL_Via1" runat="server" CssClass="datos">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:DropDownList ID="DDL_fx1" runat="server" CssClass="datos">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:DropDownList ID="DDL_t1" runat="server" CssClass="datos" Width="88" AutoPostBack="true">
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
                                        <td style="text-align: left;">
                                            <asp:DropDownList ID="DDL_e1" runat="server" CssClass="datos">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right;"></td>
                                        <td colspan="7" style="text-align: left; border-bottom: solid 1px #5d7b9d;">
                                            <table border="0" cellpadding="0" cellspacing="2">
                                                <tr>
                                                    <td style="text-align: right;">Cantidad:</td>
                                                    <td style="text-align: left;">
                                                        <asp:TextBox ID="txt_cant1" runat="server" CssClass="datosN" MaxLength="3" Width="25px">0</asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="txt_cant1_FilteredTextBoxExtender" runat="server"
                                                            TargetControlID="txt_cant1" ValidChars="0123456789">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </td>
                                                    <td style="text-align: right;">Días:</td>
                                                    <td style="text-align: left;">
                                                        <asp:TextBox ID="txt_tdias1" runat="server" Width="25px" CssClass="datosN" MaxLength="3"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="txt_tdias1_FilteredTextBoxExtender" runat="server"
                                                            TargetControlID="txt_tdias1" ValidChars="0123456789">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </td>
                                                    <td style="text-align: right;">Observaciones:</td>
                                                    <td style="text-align: left;">
                                                        <asp:TextBox ID="txt_obs1" runat="server" Width="440" CssClass="datosObs"></asp:TextBox></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span class="numeracion">#2</span>
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:DropDownList ID="DDL_cod2" runat="server" CssClass="datos" Width="60" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:Label ID="lbl_prof2" runat="server" CssClass="datos2"></asp:Label>
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:TextBox ID="txt_dx2" runat="server" Width="50px" CssClass="datosdx" MaxLength="12"></asp:TextBox>
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:DropDownList ID="DDL_Via2" runat="server" CssClass="datos">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:DropDownList ID="DDL_fx2" runat="server" CssClass="datos">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:DropDownList ID="DDL_t2" runat="server" CssClass="datos" Width="88" AutoPostBack="true">
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
                                        <td style="text-align: left;">
                                            <asp:DropDownList ID="DDL_e2" runat="server" CssClass="datos">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right;"></td>
                                        <td colspan="7" style="text-align: left; border-bottom: solid 1px #5d7b9d;">
                                            <table border="0" cellpadding="0" cellspacing="2">
                                                <tr>
                                                    <td style="text-align: right;">Cantidad:</td>
                                                    <td style="text-align: left;">
                                                        <asp:TextBox ID="txt_cant2" runat="server" CssClass="datosN" MaxLength="3" Width="25px">0</asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="txt_cant2_FilteredTextBoxExtender" runat="server"
                                                            TargetControlID="txt_cant2" ValidChars="0123456789">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </td>
                                                    <td style="text-align: right;">Días:</td>
                                                    <td style="text-align: left;">
                                                        <asp:TextBox ID="txt_tdias2" runat="server" Width="25px" CssClass="datosN" MaxLength="3"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="txt_tdias2_FilteredTextBoxExtender" runat="server"
                                                            TargetControlID="txt_tdias2" ValidChars="0123456789">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </td>
                                                    <td style="text-align: right;">Observaciones:</td>
                                                    <td style="text-align: left;">
                                                        <asp:TextBox ID="txt_obs2" runat="server" Width="440" CssClass="datosObs"></asp:TextBox></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span class="numeracion">#3</span>
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:DropDownList ID="DDL_cod3" runat="server" CssClass="datos" Width="60" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:Label ID="lbl_prof3" runat="server" CssClass="datos2"></asp:Label>
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:TextBox ID="txt_dx3" runat="server" Width="50px" CssClass="datosdx" MaxLength="12"></asp:TextBox>
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:DropDownList ID="DDL_Via3" runat="server" CssClass="datos">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:DropDownList ID="DDL_fx3" runat="server" CssClass="datos">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:DropDownList ID="DDL_t3" runat="server" CssClass="datos" Width="88" AutoPostBack="true">
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
                                        <td style="text-align: left;">
                                            <asp:DropDownList ID="DDL_e3" runat="server" CssClass="datos">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right;"></td>
                                        <td colspan="7" style="text-align: left; border-bottom: solid 1px #5d7b9d;">
                                            <table border="0" cellpadding="0" cellspacing="2">
                                                <tr>
                                                    <td style="text-align: right;">Cantidad:</td>
                                                    <td style="text-align: left;">
                                                        <asp:TextBox ID="txt_cant3" runat="server" CssClass="datosN" MaxLength="3" Width="25px">0</asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="txt_cant3_FilteredTextBoxExtender" runat="server"
                                                            TargetControlID="txt_cant3" ValidChars="0123456789">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </td>
                                                    <td style="text-align: right;">Días:</td>
                                                    <td style="text-align: left;">
                                                        <asp:TextBox ID="txt_tdias3" runat="server" Width="25px" CssClass="datosN" MaxLength="3"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="txt_tdias3_FilteredTextBoxExtender" runat="server"
                                                            TargetControlID="txt_tdias3" ValidChars="0123456789">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </td>
                                                    <td style="text-align: right;">Observaciones:</td>
                                                    <td style="text-align: left;">
                                                        <asp:TextBox ID="txt_obs3" runat="server" Width="440" CssClass="datosObs"></asp:TextBox></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span class="numeracion">#4</span>
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:DropDownList ID="DDL_cod4" runat="server" CssClass="datos" Width="60" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:Label ID="lbl_prof4" runat="server" CssClass="datos2"></asp:Label>
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:TextBox ID="txt_dx4" runat="server" Width="50px" CssClass="datosdx" MaxLength="12"></asp:TextBox>
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:DropDownList ID="DDL_Via4" runat="server" CssClass="datos">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:DropDownList ID="DDL_fx4" runat="server" CssClass="datos">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:DropDownList ID="DDL_t4" runat="server" CssClass="datos" Width="88" AutoPostBack="true">
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
                                        <td style="text-align: left;">
                                            <asp:DropDownList ID="DDL_e4" runat="server" CssClass="datos">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right;"></td>
                                        <td colspan="7" style="text-align: left; border-bottom: solid 1px #5d7b9d;">
                                            <table border="0" cellpadding="0" cellspacing="2">
                                                <tr>
                                                    <td style="text-align: right;">Cantidad:</td>
                                                    <td style="text-align: left;">
                                                        <asp:TextBox ID="txt_cant4" runat="server" CssClass="datosN" MaxLength="3" Width="25px">0</asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="txt_cant4_FilteredTextBoxExtender" runat="server"
                                                            TargetControlID="txt_cant4" ValidChars="0123456789">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </td>
                                                    <td style="text-align: right;">Días:</td>
                                                    <td style="text-align: left;">
                                                        <asp:TextBox ID="txt_tdias4" runat="server" Width="25px" CssClass="datosN" MaxLength="3"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="txt_tdias4_FilteredTextBoxExtender" runat="server"
                                                            TargetControlID="txt_tdias4" ValidChars="0123456789">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </td>
                                                    <td style="text-align: right;">Observaciones:</td>
                                                    <td style="text-align: left;">
                                                        <asp:TextBox ID="txt_obs4" runat="server" Width="440" CssClass="datosObs"></asp:TextBox></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span class="numeracion">#5</span>
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:DropDownList ID="DDL_cod5" runat="server" CssClass="datos" Width="60" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:Label ID="lbl_prof5" runat="server" CssClass="datos2"></asp:Label>
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:TextBox ID="txt_dx5" runat="server" Width="50px" CssClass="datosdx" MaxLength="12"></asp:TextBox>
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:DropDownList ID="DDL_Via5" runat="server" CssClass="datos">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:DropDownList ID="DDL_fx5" runat="server" CssClass="datos">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:DropDownList ID="DDL_t5" runat="server" CssClass="datos" Width="88" AutoPostBack="true">
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
                                        <td style="text-align: left;">
                                            <asp:DropDownList ID="DDL_e5" runat="server" CssClass="datos">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right;"></td>
                                        <td colspan="7" style="text-align: left; border-bottom: solid 1px #5d7b9d;">
                                            <table border="0" cellpadding="0" cellspacing="2">
                                                <tr>
                                                    <td style="text-align: right;">Cantidad:</td>
                                                    <td style="text-align: left;">
                                                        <asp:TextBox ID="txt_cant5" runat="server" CssClass="datosN" MaxLength="3" Width="25px">0</asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="txt_cant5_FilteredTextBoxExtender" runat="server"
                                                            TargetControlID="txt_cant5" ValidChars="0123456789">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </td>
                                                    <td style="text-align: right;">Días:</td>
                                                    <td style="text-align: left;">
                                                        <asp:TextBox ID="txt_tdias5" runat="server" Width="25px" CssClass="datosN" MaxLength="3"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="txt_tdias5_FilteredTextBoxExtender" runat="server"
                                                            TargetControlID="txt_tdias5" ValidChars="0123456789">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </td>
                                                    <td style="text-align: right;">Observaciones:</td>
                                                    <td style="text-align: left;">
                                                        <asp:TextBox ID="txt_obs5" runat="server" Width="440" CssClass="datosObs"></asp:TextBox></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span class="numeracion">#6</span>
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:DropDownList ID="DDL_cod6" runat="server" CssClass="datos" Width="60" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:Label ID="lbl_prof6" runat="server" CssClass="datos2"></asp:Label>
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:TextBox ID="txt_dx6" runat="server" Width="50px" CssClass="datosdx" MaxLength="12"></asp:TextBox>
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:DropDownList ID="DDL_Via6" runat="server" CssClass="datos">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:DropDownList ID="DDL_fx6" runat="server" CssClass="datos">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:DropDownList ID="DDL_t6" runat="server" CssClass="datos" Width="88" AutoPostBack="true">
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
                                        <td style="text-align: left;">
                                            <asp:DropDownList ID="DDL_e6" runat="server" CssClass="datos">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right;"></td>
                                        <td colspan="7" style="text-align: left;">
                                            <table border="0" cellpadding="0" cellspacing="2">
                                                <tr>
                                                    <td style="text-align: right;">Cantidad:</td>
                                                    <td style="text-align: left;">
                                                        <asp:TextBox ID="txt_cant6" runat="server" CssClass="datosN" MaxLength="3" Width="25px">0</asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="txt_cant6_FilteredTextBoxExtender" runat="server"
                                                            TargetControlID="txt_cant6" ValidChars="0123456789">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </td>
                                                    <td style="text-align: right;">Días:</td>
                                                    <td style="text-align: left;">
                                                        <asp:TextBox ID="txt_tdias6" runat="server" Width="25px" CssClass="datosN" MaxLength="3"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="txt_tdias6_FilteredTextBoxExtender" runat="server"
                                                            TargetControlID="txt_tdias6" ValidChars="0123456789">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </td>
                                                    <td style="text-align: right;">Observaciones:</td>
                                                    <td style="text-align: left;">
                                                        <asp:TextBox ID="txt_obs6" runat="server" Width="440" CssClass="datosObs"></asp:TextBox></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding: 5px 5px 5px 0px;">
                            <asp:Button ID="btn_grabar" runat="server" Text="GRABAR" CssClass="button" />
                            <asp:Button ID="btn_crear" runat="server" Text="Button" CssClass="button" Visible="false" />
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
    <script type="text/javascript" src="Scripts/validationsBeforePostBack.js"></script>
    <script type="text/javascript" src="Scripts/beforeCancelPostBack.js"></script>




    <script type="text/javascript">

        $("input[name*='btn_grabar']").on('click', function () {
            debugger;


            var FechaInput = $("input[name*='txt_fe_dd']").val() + "/" +
                $("input[name*='txt_fe_mm']").val() + "/" +
                "20" + $("input[name*='txt_fe_yy']").val()
            var resultado = validarFecha(FechaInput);
            //contains = FechaInput.includes("undefined");

            if (!resultado) {

                if (!resultado) {
                    $('#<%=lbl_error.ClientID%>').html(" Fecha Entrega no es correcta, favor verificar")
                    return false;
                }
            }
        });

    </script>

    <script type="text/javascript">
        $("input[name*='txt_asi']").on('change', function (e) {
            debugger;

            $("input[name*='lblnhc']")[0].value = $("input[name*='txt_asi']")[1].value;
        });

    </script>


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

        window.addEventListener("keypress", function (event) {
            if (event.keyCode == 13) {
                event.preventDefault();
            }
        }, false);

    </script>

    <script type="text/javascript">

        $(document).ready(function () {
            debugger;
            $('[id$="page_titulo"]').html("INGRESO CONSUMO DE PROFILAXIS");
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
