<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="fprof.aspx.vb" Inherits="fprof" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div style="width: 700px; border: solid 1px #5d7b9d; text-align:left;">
        <table id="tblbasal" border="0" cellpadding="2" cellspacing="1">
            <tr>
                <th colspan="2" class="theader">
                    FORMA FARMACEUTICA DE PROFILAXIS</th>
            </tr>
            <tr>
                <td style="width: 150px; background-color: #5d7b9d; color: #ffffff;">
                    Profilaxis:
                </td>
                <td style="width: 550px; background-color: #e9ecf1;">
                    <asp:DropDownList ID="DDL_prof" runat="server" CssClass="datos">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 150px; background-color: #5d7b9d; color: #ffffff;">
                    Forma Farmaceutica:
                </td>
                <td style="width: 550px; background-color: #e9ecf1;">
                    <asp:DropDownList ID="DDL_ff" runat="server" CssClass="datos">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 150px; background-color: #5d7b9d; color: #ffffff;">
                    Concentración:
                </td>
                <td style="width: 550px; background-color: #e9ecf1;">
                    <asp:TextBox ID="txt_concentracion" runat="server" CssClass="datos" Width="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="padding:5px 5px 5px 5px;">
                    <asp:Button ID="btn_grabar" runat="server" Text="GRABAR" TabIndex="45" CssClass="button" />
                    <cc1:ConfirmButtonExtender ID="btn_grabar_ConfirmButtonExtender" runat="server" 
                        ConfirmText="Esta seguro de grabar este Medicamento?" 
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
                    <asp:GridView ID="GV_ffprof" runat="server" 
                        CellPadding="2" 
                        ForeColor="#333333" 
                        GridLines="None" 
                        Width="700px" 
                        AllowPaging="True" 
                        PageSize="20" 
                        AutoGenerateColumns="False"
                        DataKeyNames="IdFFProf" >
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
                            <asp:TemplateField HeaderText="Código">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_codigo" runat="server" Text='<%# Bind("Codigo") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" CssClass="GV_rowpad" Width="70px" />
                                <HeaderStyle HorizontalAlign="Center" CssClass="GV_rowpad" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Nombre Profilaxis">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_NomProfilaxis" runat="server" Text='<%# Bind("NomProfilaxis") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" CssClass="GV_rowpad" />
                                <HeaderStyle HorizontalAlign="Left" CssClass="GV_rowpad" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="FF">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_NomFF" runat="server" Text='<%# Bind("NomFF") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" CssClass="GV_rowpad" Width="150px" />
                                <HeaderStyle HorizontalAlign="Left" CssClass="GV_rowpad" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Concentración">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Concentracion" runat="server" Text='<%# Bind("Concentracion")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" CssClass="GV_rowpad" Width="150px" />
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
