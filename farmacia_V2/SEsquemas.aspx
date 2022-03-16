<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="SEsquemas.aspx.vb" Inherits="SEsquemas" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div style="width: 700px; border: solid 1px #5d7b9d; text-align:left;">
        <table id="tblbasal" border="0" cellpadding="0" cellspacing="1">
            <tr>
                <th colspan="2" class="theader">
                    SUB-ESQUEMAS ARV</th>
            </tr>
            <tr>
                <td style="width: 150px; background-color: #5d7b9d; color: #ffffff;">
                    ESQUEMA:
                </td>
                <td style="width: 550px; background-color: #e9ecf1;">
                    <asp:Label ID="lbl_nesquema" runat="server" CssClass="NHClbl"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 150px; background-color: #5d7b9d; color: #ffffff;">
                    ARV´s y FF:
                </td>
                <td style="width: 550px; background-color: #e9ecf1;">
                    <asp:DropDownList ID="DDL_arv" runat="server" CssClass="datos">
                    </asp:DropDownList>
                    &nbsp;<asp:Button ID="btn_agregar" runat="server" Text="AGREGAR [+]" CssClass="button" />
                    &nbsp;<asp:Button ID="btn_limpiar" runat="server" Text="LIMPIAR" CssClass="button" />
                </td>
            </tr>
            <tr>
                <td style="width: 150px; background-color: #5d7b9d; color: #ffffff;">
                    Nombre Sub-Esquema:
                </td>
                <td style="width: 550px; background-color: #e9ecf1;">
                    <asp:Label ID="lbl_nsesquema" runat="server" CssClass="datoslbl"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 150px; background-color: #5d7b9d; color: #ffffff;">
                    Códigos Sub-Esquema:
                </td>
                <td style="width: 550px; background-color: #e9ecf1;">
                    <asp:Label ID="lbl_sesquema" runat="server" CssClass="datoslbl"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="padding:5px 5px 5px 5px;">
                    <asp:Button ID="btn_grabar" runat="server" Text="GRABAR" TabIndex="45" CssClass="button" />
                    <cc1:ConfirmButtonExtender ID="btn_grabar_ConfirmButtonExtender" runat="server"  
                        ConfirmText="Esta seguro de grabar este Sub-Esquema?" 
                        TargetControlID="btn_grabar">
                    </cc1:ConfirmButtonExtender>
                    <asp:Button ID="btn_cancelar" runat="server" Text="CANCELAR" TabIndex="46" CssClass="button" CausesValidation="false" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;<asp:Label ID="lbl_error" runat="server" CssClass="error"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div style="width: 700px; border: solid 1px #5d7b9d; text-align:left;">
        <table id="Table1" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="2">
                    <asp:GridView ID="GV_ffarv" runat="server" 
                        CellPadding="2" 
                        ForeColor="#333333" 
                        GridLines="None" 
                        Width="700px" 
                        AllowPaging="True" 
                        PageSize="20" 
                        AutoGenerateColumns="False"
                        EmptyDataText="No Existen Sub-Esquemas" >
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <Columns>
                            <asp:TemplateField HeaderText="ID">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_SCodigo" runat="server" Text='<%# Bind("SCodigo") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" CssClass="GV_rowpad" Font-Bold="true" Width="40px" />
                                <HeaderStyle HorizontalAlign="Center" CssClass="GV_rowpad" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Nombre">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Descripcion" runat="server" Text='<%# Bind("Descripcion") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" CssClass="GV_rowpad" />
                                <HeaderStyle HorizontalAlign="Center" CssClass="GV_rowpad" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Códigos">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Codigos" runat="server" Text='<%# Bind("Codigos") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" CssClass="GV_rowpad" Width="75px" />
                                <HeaderStyle HorizontalAlign="Center" CssClass="GV_rowpad" Width="75px" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#aba392" ForeColor="#333333" HorizontalAlign="Right" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#999999" />
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
