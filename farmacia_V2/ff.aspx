<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="ff.aspx.vb" Inherits="ff" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div style="width: 700px; border: solid 1px #5d7b9d; text-align:left;">
        <table id="tblbasal" border="0" cellpadding="2" cellspacing="1">
            <tr>
                <th colspan="2" class="theader">FORMA FARMACEUTICA</th>
            </tr>
            <tr>
                <td style="width: 150px; background-color: #5d7b9d; color: #ffffff;">
                    Nombre:
                </td>
                <td style="width: 550px; background-color: #e9ecf1;">
                    <asp:TextBox ID="txt_ff" runat="server" CssClass="datos" Width="200px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RFV_txt_ff" runat="server" ControlToValidate="txt_ff"
                        ErrorMessage="*"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="padding:5px 5px 5px 5px;">
                    <asp:Button ID="btn_grabar" runat="server" Text="GRABAR" TabIndex="45" CssClass="button" />
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
                    <asp:GridView ID="GV_ff" runat="server" 
                        CellPadding="2" 
                        ForeColor="#333333" 
                        GridLines="None" 
                        Width="700px" 
                        AllowPaging="True" 
                        PageSize="20" 
                        AutoGenerateColumns="False"
                        DataKeyNames="IdFF">
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <Columns>
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:ImageButton ID="IB_editar" runat="server" CausesValidation="False" 
                                        CommandName="Editar" ImageUrl="~/images/file_edit.png" 
                                        ToolTip="Editar" CssClass="cursor" Height="16px" TabIndex="100" />
                                </ItemTemplate>
                                <ItemStyle Width="18px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Id">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_id" runat="server" Text='<%# Bind("IdFF") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" CssClass="GV_rowpad" Width="40px" />
                                <HeaderStyle HorizontalAlign="Center" CssClass="GV_rowpad" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Forma Farmaceútica">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_NomFF" runat="server" Text='<%# Bind("NomFF") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" CssClass="GV_rowpad" />
                                <HeaderStyle HorizontalAlign="Left" CssClass="GV_rowpad" />
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

