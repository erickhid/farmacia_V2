<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="EBPediatrico.aspx.vb" Inherits="EBPediatrico" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div style="width: 700px; border: solid 1px #5d7b9d; text-align:left;">
        <table id="tblbasal" border="0" cellpadding="2" cellspacing="1">
            <tr>
                <th colspan="2" class="theader">
                    EDICION DE REGISTRO PEDIATRICO</th>
            </tr>
            <tr>
                <td style="width: 150px; background-color: #5d7b9d; color: #ffffff;">
                    Número ASI:
                </td>
                <td style="width: 550px; background-color: #e9ecf1;">
                    <asp:Label ID="lbl_asi" runat="server" CssClass="NHClbl" MaxLength="7" Width="64px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 150px; background-color: #5d7b9d; color: #ffffff;">
                    Primer Nombre:
                </td>
                <td style="width: 550px; background-color: #e9ecf1;">
                    <asp:TextBox ID="txt_pnombre" runat="server" CssClass="datos" Width="200px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RFV_txt_pnombre" runat="server" ControlToValidate="txt_pnombre"  
                        ErrorMessage="*"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td style="width: 150px; background-color: #5d7b9d; color: #ffffff;">
                    Segundo Nombre:
                </td>
                <td style="width: 550px; background-color: #e9ecf1;">
                    <asp:TextBox ID="txt_snombre" runat="server" CssClass="datos" Width="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 150px; background-color: #5d7b9d; color: #ffffff;">
                    Primer Apellido:
                </td>
                <td style="width: 550px; background-color: #e9ecf1;">
                    <asp:TextBox ID="txt_papellido" runat="server" CssClass="datos" Width="200px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_papellido"  
                        ErrorMessage="*"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td style="width: 150px; background-color: #5d7b9d; color: #ffffff;">
                    Segundo Apellido:
                </td>
                <td style="width: 550px; background-color: #e9ecf1;">
                    <asp:TextBox ID="txt_sapellido" runat="server" CssClass="datos" Width="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 150px; background-color: #5d7b9d; color: #ffffff;">
                    Género:
                </td>
                <td style="width: 550px; background-color: #e9ecf1;">
                    <asp:DropDownList ID="DDL_genero" runat="server" CssClass="datos">
                       
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 150px; background-color: #5d7b9d; color: #ffffff;">
                    Fecha Nacimiento:
                </td>
                <td style="width:550px; background-color: #e9ecf1; padding:0px;">
                    <table id="tbl_cal" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <asp:TextBox ID="txt_nacimiento" runat="server" CssClass="datos" Width="65px"></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="txt_nacimiento_FilteredTextBoxExtender" 
                                    runat="server" TargetControlID="txt_nacimiento" ValidChars="0123456789/">
                                </cc1:FilteredTextBoxExtender>
                                <cc1:TextBoxWatermarkExtender ID="txt_nacimiento_TextBoxWatermarkExtender" 
                                    runat="server" TargetControlID="txt_nacimiento" WatermarkText="dd/mm/aaaa">
                                </cc1:TextBoxWatermarkExtender>
                                <cc1:CalendarExtender ID="txt_nacimiento_CalendarExtender" runat="server" 
                                    Format="dd/MM/yyyy" PopupButtonID="img_cal" PopupPosition="BottomRight" 
                                    TargetControlID="txt_nacimiento">
                                </cc1:CalendarExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_nacimiento"  
                                    ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:Image ID="img_cal" runat="server" ImageUrl="~/images/datePickerPopupHover.gif" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="width: 150px; background-color: #5d7b9d; color: #ffffff;">
                    Teléfono:
                </td>
                <td style="width: 550px; background-color: #e9ecf1;">
                    <asp:TextBox ID="txt_telefono" runat="server" CssClass="datos" Width="200px"></asp:TextBox>
                    <cc1:FilteredTextBoxExtender ID="txt_telefono_FilteredTextBoxExtender" 
                        runat="server" TargetControlID="txt_telefono" ValidChars="0123456789 -,">
                    </cc1:FilteredTextBoxExtender>
                </td>
            </tr>
            <tr>
                <td style="width: 150px; background-color: #5d7b9d; color: #ffffff;">
                    Domicilio Actual:
                </td>
                <td style="width: 550px; background-color: #e9ecf1;">
                    <asp:TextBox ID="txt_domicilio" runat="server" CssClass="datos" Width="400px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 150px; background-color: #5d7b9d; color: #ffffff;">
                    Estatus:
                </td>
                <td style="width: 550px; background-color: #e9ecf1;">
                    <asp:DropDownList ID="DDL_estatus" runat="server" CssClass="datos">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="2">&nbsp;<asp:Label ID="lbl_error" runat="server" CssClass="error"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="2" style="padding:5px 5px 5px 5px;">
                    <asp:Button ID="btn_grabar" runat="server" Text="GRABAR" TabIndex="45" CssClass="button" />
                    <asp:Button ID="btn_cancelar" runat="server" Text="CANCELAR" TabIndex="46" CssClass="button" CausesValidation="false" />
                </td>
            </tr>
        </table>
        
    </div>
</asp:Content>

