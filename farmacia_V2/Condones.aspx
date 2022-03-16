
<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Condones.aspx.vb"   Inherits="Condones" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
	<script type="text/jscript">         
         function Mensaje() {
            //debugger;
             var Mensaje = document.getElementById('<%=txtMessage.ClientID%>').value;
             Swal.fire(Mensaje).then(function(){
                                                    window.location.href = "/Farmacia/Condones.aspx";
                                                }); 
         }
     </script>
        <div style="width: 700px; border: solid 1px #5d7b9d; text-align:left;">
            <table id="tblbasal" border="0" cellpadding="2" cellspacing="1">
                <tr>
                    <th colspan="4" class="theader">INGRESO CONDONES Y LUBRICANTES</th>
                </tr>
                <tr>
                    <td style="width: 100px; background-color: #5d7b9d; color: #ffffff;">
                        Número ASI:
                    </td>
                    <td style="width: 230px; background-color: #e9ecf1; padding:0px;">
                        <table id="tblNHC" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txt_asi" runat="server" CssClass="NHC" MaxLength="7" Width="64px"
                                        TabIndex="1" AutoPostBack="True"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="txt_asi_FilteredTextBoxExtender" runat="server"
                                        TargetControlID="txt_asi" ValidChars="0123456789Pp">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                                <td>
                                    <asp:ImageButton ID="btn_buscar" runat="server" ToolTip="BUSCAR" CausesValidation="false" ImageUrl="~/images/magnify-clip.png" />
                                    <asp:ImageButton ID="btn_editar" runat="server" ToolTip="EDITAR" CausesValidation="false" ImageUrl="~/images/file_edit.png" Visible="False" />
                                    <asp:ImageButton ID="btn_agregar" runat="server" ToolTip="AGREGAR" CausesValidation="false" ImageUrl="~/images/add.png" Visible="False" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 100px; background-color: #5d7b9d; color: #ffffff;">
                        Estatus MANGUA:
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
                <tr>
                    <td>
                        <div style="border: solid 1px #333333; width: 700px; text-align:left;">
                        <table border="0" cellpadding="2" cellspacing="1">
                            <tr>
                                <td>Última Fecha de Entrega:</td>
                                <td>
                                    <asp:Label ID="lbl_ultimafechaentrega" runat="server" CssClass="datos1"></asp:Label>
                                </td>
                                <td style="padding-left:10px;">Estatus FARMACIA:</td>
                                <td>
                                    <asp:Label ID="lbl_estatusfarmacia" runat="server" CssClass="datos1"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        </div>
                    </td>
                </tr>
             </table> 
            <div id="condones" style="border: solid 1px #333333; width: 700px; text-align:left;">
                <table style="width:700px" border="0" cellpadding="2" cellspacing="1">
                    <tr>
                        <td style=" width:100px;  "> Fecha de Entrega:</td>
                        <td>
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
                    </tr>
                    </table>
                <div style="border: solid 1px #5d7b9d; width:700px;">
                <table style="width:700px" border="0" cellpadding="2" cellspacing="1">

                    <tr>
                        <td style="background-color: #e9ecf1; text-align:center; width:82px;" colspan="2">
                            Código: 
                        </td>
                        <td style="background-color: #e9ecf1; text-align:center; width:125px;" colspan="2">
                            Descripción: 
                        </td>
                        <td  style="background-color: #e9ecf1; text-align:center; width:75px;" colspan="2">
                            Cantidad: 
                        </td>
                       <%-- <td style="background-color: #e9ecf1; text-align:center; width:400px;" colspan="2"> </td>--%>
                        <td style="background-color: #e9ecf1; text-align:center; width:82px;" colspan="2">
                            Código: 
                        </td>
                        <td style="background-color: #e9ecf1; text-align:center; width:125px;" colspan="2">
                            Descripción: 
                        </td>
                        <td  style="background-color: #e9ecf1; text-align:center; width:75px;" colspan="2">
                            Cantidad: 
                        </td>
                    </tr>
                    <tr>
                            <td colspan="1" style="text-align:start; width: 25px;">
                                <span class="numeracion">#1</span>
                            </td>
                        <td colspan="1" style="text-align:start;">
                                <asp:DropDownList ID="DDL_cod1" runat="server" CssClass="datos" Width="60" AutoPostBack="True">
                                </asp:DropDownList>
                        </td>
                        <td style="text-align:center; width:125px;" colspan="2">
                            <asp:Label ID="lbl_descripcion1" runat="server" CssClass="datos2"></asp:Label>
                        </td>
                        <td style="text-align:center;" colspan="2">
                             <asp:TextBox ID="txt_cant1" runat="server" CssClass="datosN" MaxLength="3" Width="25px"></asp:TextBox>
                        </td>

                            <td colspan="1" style="text-align:start; width: 25px;">
                                <span class="numeracion">#2</span>
                            </td>
                        <td colspan="1" style="text-align:start;">
                                <asp:DropDownList ID="DDL_cod2" runat="server" CssClass="datos" Width="60" AutoPostBack="True">
                                </asp:DropDownList>
                        </td>
                        <td style="text-align:center; width:125px;" colspan="2">
                            <asp:Label ID="lbl_descripcion2" runat="server" CssClass="datos2"></asp:Label>
                        </td>
                        <td style="text-align:center;">
                             <asp:TextBox ID="txt_cant2" runat="server" CssClass="datosN" MaxLength="3" Width="25px"></asp:TextBox>
                        </td>
                    </tr>
					<tr>
                        
                        <td colspan="1" style="text-align:start; width: 25px;">
                                <span class="numeracion">#3</span>
                            </td>
                        <td colspan="1" style="text-align:start;">
                                <asp:DropDownList ID="DDL_cod3" runat="server" CssClass="datos" Width="60" AutoPostBack="True">
                                </asp:DropDownList>
                        </td>
                        <td style="text-align:center; width:125px;" colspan="2">
                            <asp:Label ID="lbl_descripcion3" runat="server" CssClass="datos2"></asp:Label>
                        </td>
                        <td style="text-align:center;">
                             <asp:TextBox ID="txt_cant3" runat="server" CssClass="datosN" MaxLength="3" Width="25px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: #e9ecf1; text-align:center; width:75px;" colspan="2">
                            Observaciones:
                        </td>
                        <td colspan="9" style="background-color: #e9ecf1;">
                            <asp:TextBox ID="txt_obs" runat="server" Width="440" Height="35" CssClass="datosObs"></asp:TextBox>
                        </td>
                    </tr>
                    </table>
                    </div>

                    <tr>
                                <td style=" padding:5px 5px 5px 0px;" colspan="9">
                                         <asp:Button ID="btn_grabar" runat="server" Text="GRABAR" CssClass="button" />
                                 </td>
								 <td>
                            <asp:TextBox ID="txtMessage" Enabled="false"  runat="server" Width="440"></asp:TextBox>
                        </td>
                   </tr>

                    </table> 

            </div>
     </div> 

        
</asp:Content>
