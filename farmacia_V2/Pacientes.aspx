<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false"
    CodeFile="Pacientes.aspx.vb" Inherits="Pacientes" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Label ID="lbl_error" runat="server" CssClass="error"></asp:Label>
    <div style="width: 700px; border: solid 1px #544e41; background: #544e41; color: #FFFFFF;
        text-align: left;">
        <b>&nbsp;&nbsp;&nbsp;ADULTOS</b></div>
    <div style="width: 700px; border: solid 1px #5d7b9d; text-align: left;">
        <table id="A1" runat="server" border="0" cellpadding="0" cellspacing="0" style="width: 700px;">
            <tr>
                <td style="background-color: #f7f6f3; font-weight: bold; color: #333333;">
                    &nbsp;&nbsp;<asp:Label ID="lbl_cpacA" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="border: solid 1px #b5b8ba;">
                    <div style="width: 700px; height: 22px; background-color: #b5b8ba; text-align: left;">
                        <table id="tbl_gvfapp" border="0" cellpadding="2" cellspacing="0" style="width: 700px;
                            height: 22px;">
                            <tr>
                                <th style="background-color: #5D7B9D; font-weight: bold; color: #ffffff; width: 16px;">
                                    &nbsp;
                                </th>
                                <th style="background-color: #5D7B9D; font-weight: bold; color: #ffffff; width: 60px;
                                    text-align: center;">
                                    NHC
                                </th>
                                <th style="background-color: #5D7B9D; font-weight: bold; color: #ffffff; width: 50px;
                                    text-align: center;">
                                    Cohorte
                                </th>
                                <th style="background-color: #5D7B9D; font-weight: bold; color: #ffffff;">
                                    Paciente
                                </th>
                                <th style="background-color: #5D7B9D; font-weight: bold; color: #ffffff; width: 80px;
                                    text-align: center;">
                                    Ultima Entrega
                                </th>
                                <th style="background-color: #5D7B9D; font-weight: bold; color: #ffffff; width: 70px;
                                    text-align: center;">
                                    Retorno
                                </th>
                                <th style="background-color: #5D7B9D; font-weight: bold; color: #ffffff; width: 40px;
                                    text-align: center;">
                                    R.Dias
                                </th>
                                <th style="background-color: #5D7B9D; font-weight: bold; color: #ffffff; width: 20px;">
                                    &nbsp;
                                </th>
                            </tr>
                        </table>
                    </div>
                    <asp:Panel ID="Panel1" runat="server" Height="200px" Width="700px" Wrap="False" ScrollBars="Vertical">
                        <div style="width: 680px;">
                            <asp:GridView ID="GV_pacA" runat="server" CellPadding="2" ForeColor="#333333" GridLines="None"
                                Width="680px" AutoGenerateColumns="False" DataKeyNames="NHC" ShowHeader="False">
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <Columns>
                                    <asp:TemplateField ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="IB_ver" runat="server" CausesValidation="False" CommandName="Ver"
                                                ImageUrl="~/images/magnify-clip.png" ToolTip="Ver" CssClass="cursor" Height="16px"
                                                TabIndex="100" />
                                        </ItemTemplate>
                                        <ItemStyle Width="18px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="NHC">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_NHC" runat="server" Text='<%# Bind("NHC") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" CssClass="GV_rowpad" Width="60px" />
                                        <HeaderStyle HorizontalAlign="Center" CssClass="GV_rowpad" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cohorte">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Cohorte" runat="server" Text='<%# Bind("Cohorte") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" CssClass="GV_rowpad" Width="50px" />
                                        <HeaderStyle HorizontalAlign="Center" CssClass="GV_rowpad" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Paciente">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Paciente" runat="server" Text='<%# Bind("Paciente") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" CssClass="GV_rowpad" />
                                        <HeaderStyle HorizontalAlign="Left" CssClass="GV_rowpad" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Ultima Entrega">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_UltimaFechaEntrega" runat="server" Text='<%# Bind("UltimaFechaEntrega") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" CssClass="GV_rowpad" Width="80px" />
                                        <HeaderStyle HorizontalAlign="Left" CssClass="GV_rowpad" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Retorno">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_FechaRetorno" runat="server" Text='<%# Bind("FechaRetorno") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" CssClass="GV_rowpad" Width="70px" />
                                        <HeaderStyle HorizontalAlign="Left" CssClass="GV_rowpad" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="R. Días">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Dias" runat="server" Text='<%# Bind("Dias") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" CssClass="GV_rowpad" Width="40px" />
                                        <HeaderStyle HorizontalAlign="Left" CssClass="GV_rowpad" />
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
                </td>
            </tr>
        </table>
        <table id="A2" runat="server" border="0" cellpadding="0" cellspacing="0" style="width: 700px;">
            <tr>
                <td style="background-color: #f7f6f3; font-weight: bold; color: #333333;">
                    &nbsp;&nbsp;<asp:Label ID="lbl_cpacA_A" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="border: solid 1px #b5b8ba;">
                    <div style="width: 700px; height: 22px; background-color: #b5b8ba; text-align: left;">
                        <table id="Table3" border="0" cellpadding="2" cellspacing="0" style="width: 700px;
                            height: 22px;">
                            <tr>
                                <th style="background-color: #5D7B9D; font-weight: bold; color: #ffffff; width: 16px;">
                                    &nbsp;
                                </th>
                                <th style="background-color: #5D7B9D; font-weight: bold; color: #ffffff; width: 60px;
                                    text-align: center;">
                                    NHC
                                </th>
                                <th style="background-color: #5D7B9D; font-weight: bold; color: #ffffff; width: 50px;
                                    text-align: center;">
                                    Cohorte
                                </th>
                                <th style="background-color: #5D7B9D; font-weight: bold; color: #ffffff;">
                                    Paciente
                                </th>
                                <th style="background-color: #5D7B9D; font-weight: bold; color: #ffffff; width: 80px;
                                    text-align: center;">
                                    Ultima Entrega
                                </th>
                                <th style="background-color: #5D7B9D; font-weight: bold; color: #ffffff; width: 20px;
                                    text-align: center;">
                                    &nbsp;
                                </th>
                            </tr>
                        </table>
                    </div>
                    <asp:Panel ID="Panel4" runat="server" Height="200px" Width="700px" Wrap="False" ScrollBars="Vertical">
                        <div style="width: 680px;">
                            <asp:GridView ID="GV_pacA_A" runat="server" CellPadding="2" ForeColor="#333333" GridLines="None"
                                Width="680px" AutoGenerateColumns="False" DataKeyNames="NHC" ShowHeader="False">
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <Columns>
                                    <asp:TemplateField ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="IB_Ver" runat="server" CausesValidation="False" CommandName="Ver"
                                                ImageUrl="~/images/magnify-clip.png" ToolTip="Ver" CssClass="cursor" Height="16px"
                                                TabIndex="100" />
                                        </ItemTemplate>
                                        <ItemStyle Width="18px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="NHC">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_NHC" runat="server" Text='<%# Bind("NHC") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" CssClass="GV_rowpad" Width="60px" />
                                        <HeaderStyle HorizontalAlign="Center" CssClass="GV_rowpad" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cohorte">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Cohorte" runat="server" Text='<%# Bind("Cohorte") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" CssClass="GV_rowpad" Width="50px" />
                                        <HeaderStyle HorizontalAlign="Center" CssClass="GV_rowpad" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Paciente">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Paciente" runat="server" Text='<%# Bind("Paciente") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" CssClass="GV_rowpad" />
                                        <HeaderStyle HorizontalAlign="Left" CssClass="GV_rowpad" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Ultima Entrega">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_UltimaFechaEntrega" runat="server" Text='<%# Bind("UltimaFechaEntrega") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" CssClass="GV_rowpad" Width="80px" />
                                        <HeaderStyle HorizontalAlign="Left" CssClass="GV_rowpad" />
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
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div id="P" runat="server" style="width: 700px;">
        <div style="width: 700px; border: solid 1px #544e41; background: #544e41; color: #FFFFFF;
            text-align: left;">
            <b>&nbsp;&nbsp;&nbsp;PEDIATRIA</b></div>
        <div style="width: 700px; border: solid 1px #5d7b9d; text-align: left;">
            <table id="P1" runat="server" border="0" cellpadding="0" cellspacing="0" style="width: 700px;">
                <tr>
                    <td style="background-color: #f7f6f3; font-weight: bold; color: #333333;">
                        &nbsp;&nbsp;<asp:Label ID="lbl_cpacP" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="border: solid 1px #b5b8ba;">
                        <div style="width: 700px; height: 22px; background-color: #b5b8ba; text-align: left;">
                            <table id="Table1" border="0" cellpadding="2" cellspacing="0" style="width: 700px;
                                height: 22px;">
                                <tr>
                                    <th style="background-color: #5D7B9D; font-weight: bold; color: #ffffff; width: 16px;">
                                        &nbsp;
                                    </th>
                                    <th style="background-color: #5D7B9D; font-weight: bold; color: #ffffff; width: 60px;
                                        text-align: center;">
                                        NHC
                                    </th>
                                    <th style="background-color: #5D7B9D; font-weight: bold; color: #ffffff; width: 50px;
                                        text-align: center;">
                                        Cohorte
                                    </th>
                                    <th style="background-color: #5D7B9D; font-weight: bold; color: #ffffff; text-align: center;">
                                        Paciente
                                    </th>
                                    <th style="background-color: #5D7B9D; font-weight: bold; color: #ffffff; width: 80px;
                                        text-align: center;">
                                        Ultima Entrega
                                    </th>
                                    <th style="background-color: #5D7B9D; font-weight: bold; color: #ffffff; width: 70px;
                                        text-align: center;">
                                        Retorno
                                    </th>
                                    <th style="background-color: #5D7B9D; font-weight: bold; color: #ffffff; width: 40px;
                                        text-align: center;">
                                        R.Dias
                                    </th>
                                    <th style="background-color: #5D7B9D; font-weight: bold; color: #ffffff; width: 20px;">
                                        &nbsp;
                                    </th>
                                </tr>
                            </table>
                        </div>
                        <asp:Panel ID="Panel2" runat="server" Height="200px" Width="700px" Wrap="False" ScrollBars="Vertical">
                            <div style="width: 680px;">
                                <asp:GridView ID="GV_pacP" runat="server" CellPadding="2" ForeColor="#333333" GridLines="None"
                                    Width="680px" AutoGenerateColumns="False" DataKeyNames="NHC" ShowHeader="False">
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <Columns>
                                        <asp:TemplateField ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="IB_Ver" runat="server" CausesValidation="False" CommandName="Ver"
                                                    ImageUrl="~/images/magnify-clip.png" ToolTip="Ver" CssClass="cursor" Height="16px"
                                                    TabIndex="100" />
                                            </ItemTemplate>
                                            <ItemStyle Width="18px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="NHC">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_NHC" runat="server" Text='<%# Bind("NHC") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" CssClass="GV_rowpad" Width="60px" />
                                            <HeaderStyle HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cohorte">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Cohorte" runat="server" Text='<%# Bind("Cohorte") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" CssClass="GV_rowpad" Width="50px" />
                                            <HeaderStyle HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Paciente">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Paciente" runat="server" Text='<%# Bind("Paciente") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" CssClass="GV_rowpad" />
                                            <HeaderStyle HorizontalAlign="Left" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Ultima Entrega">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_UltimaFechaEntrega" runat="server" Text='<%# Bind("UltimaFechaEntrega") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" CssClass="GV_rowpad" Width="80px" />
                                            <HeaderStyle HorizontalAlign="Left" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Retorno">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_FechaRetorno" runat="server" Text='<%# Bind("FechaRetorno") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" CssClass="GV_rowpad" Width="70px" />
                                            <HeaderStyle HorizontalAlign="Left" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="R. Días">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Dias" runat="server" Text='<%# Bind("Dias") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" CssClass="GV_rowpad" Width="40px" />
                                            <HeaderStyle HorizontalAlign="Left" CssClass="GV_rowpad" />
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
                    </td>
                </tr>
            </table>
            <table id="P2" runat="server" border="0" cellpadding="0" cellspacing="0" style="width: 700px;">
                <tr>
                    <td style="background-color: #f7f6f3; font-weight: bold; color: #333333;">
                        &nbsp;&nbsp;<asp:Label ID="lbl_cpacP_A" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="border: solid 1px #b5b8ba;">
                        <div style="width: 700px; height: 22px; background-color: #b5b8ba; text-align: left;">
                            <table id="Table6" border="0" cellpadding="2" cellspacing="0" style="width: 700px;
                                height: 22px;">
                                <tr>
                                    <th style="background-color: #5D7B9D; font-weight: bold; color: #ffffff; width: 16px;">
                                        &nbsp;
                                    </th>
                                    <th style="background-color: #5D7B9D; font-weight: bold; color: #ffffff; width: 60px;
                                        text-align: center;">
                                        NHC
                                    </th>
                                    <th style="background-color: #5D7B9D; font-weight: bold; color: #ffffff; width: 50px;
                                        text-align: center;">
                                        Cohorte
                                    </th>
                                    <th style="background-color: #5D7B9D; font-weight: bold; color: #ffffff;">
                                        Paciente
                                    </th>
                                    <th style="background-color: #5D7B9D; font-weight: bold; color: #ffffff; width: 80px;
                                        text-align: center;">
                                        Ultima Entrega
                                    </th>
                                    <th style="background-color: #5D7B9D; font-weight: bold; color: #ffffff; width: 20px;
                                        text-align: center;">
                                        &nbsp;
                                    </th>
                                </tr>
                            </table>
                        </div>
                        <asp:Panel ID="Panel7" runat="server" Height="200px" Width="700px" Wrap="False" ScrollBars="Vertical">
                            <div style="width: 680px;">
                                <asp:GridView ID="GV_pacP_A" runat="server" CellPadding="2" ForeColor="#333333" GridLines="None"
                                    Width="680px" AutoGenerateColumns="False" DataKeyNames="NHC" ShowHeader="False">
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <Columns>
                                        <asp:TemplateField ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="IB_Ver" runat="server" CausesValidation="False" CommandName="Ver"
                                                    ImageUrl="~/images/magnify-clip.png" ToolTip="Ver" CssClass="cursor" Height="16px"
                                                    TabIndex="100" />
                                            </ItemTemplate>
                                            <ItemStyle Width="18px" HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="NHC">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_NHC" runat="server" Text='<%# Bind("NHC") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" CssClass="GV_rowpad" Width="60px" />
                                            <HeaderStyle HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cohorte">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Cohorte" runat="server" Text='<%# Bind("Cohorte") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" CssClass="GV_rowpad" Width="50px" />
                                            <HeaderStyle HorizontalAlign="Center" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Paciente">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Paciente" runat="server" Text='<%# Bind("Paciente") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" CssClass="GV_rowpad" />
                                            <HeaderStyle HorizontalAlign="Left" CssClass="GV_rowpad" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Ultima Entrega">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_UltimaFechaEntrega" runat="server" Text='<%# Bind("UltimaFechaEntrega") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" CssClass="GV_rowpad" Width="80px" />
                                            <HeaderStyle HorizontalAlign="Left" CssClass="GV_rowpad" />
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
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
