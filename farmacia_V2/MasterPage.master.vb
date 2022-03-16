
Partial Class MasterPage
    Inherits System.Web.UI.MasterPage
    Private db As New BusinessLogicDB()
    Public cn1 As String = ConfigurationManager.ConnectionStrings("conStringFarmacia").ConnectionString
    Public cn2 As String = ConfigurationManager.ConnectionStrings("conString").ConnectionString
    Public errores As String = ""
    Private revisar As New Rsesion()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Dim sessionTimeout As Integer = Session.Timeout
        Dim valortimeout As Integer = Convert.ToInt16(ConfigurationManager.AppSettings("timeout"))
        sessionTimeout = sessionTimeout * valortimeout
        Dim value As String = sessionTimeout.ToString() & "; url=logout.aspx"
        Response.AppendHeader("Refresh", value)
        If Not Page.IsPostBack Then
            Response.Buffer = True
            Response.ExpiresAbsolute = DateTime.Now.AddDays(-1.0)
            Response.Expires = -1500
            Response.CacheControl = "no-cache"
            Dim rol As String = Convert.ToString(Session("pusuario"))
            lbl_nombre.Text = Session("usuario").ToString()
            Session("ip") = Request.UserHostAddress
            If Not revisar.RevisaSesion(Session("conexion").ToString(), Session("usuario").ToString()) Then
                Response.Redirect("~/inicio.aspx", False)
            Else
                Select Case rol
                    Case "1" 'Master
                        img_usuario.ToolTip = "Master"
                        GIngreso.Visible = True
                        GPacientes.Visible = True
                        GReportes.Visible = True
                        GAdministracion.Visible = True
                        GCodigos.Visible = True
                    Case "2" 'Administrador
                        img_usuario.ToolTip = "Administrador"
                        GIngreso.Visible = True
                        GPacientes.Visible = True
                        GReportes.Visible = True
                        GAdministracion.Visible = True
                        GCodigos.Visible = True
                    Case "3" 'Digitador
                        img_usuario.ToolTip = "Digitador"
                        GIngreso.Visible = True
                        GPacientes.Visible = True
                        GReportes.Visible = False
                        GAdministracion.Visible = False
                        GCodigos.Visible = True
                    Case "4" 'Consulta
                        img_usuario.ToolTip = "Consulta"
                        GIngreso.Visible = False
                        GPacientes.Visible = True
                        GReportes.Visible = False
                        GAdministracion.Visible = False
                        GCodigos.Visible = True
                    Case "5" 'Reportes
                        img_usuario.ToolTip = "Reportes"
                        GIngreso.Visible = False
                        GPacientes.Visible = False
                        GReportes.Visible = True
                        GAdministracion.Visible = False
                        GCodigos.Visible = True
                    Case "6" 'Supervisor
                        img_usuario.ToolTip = "Supervisor"
                        GIngreso.Visible = False
                        GPacientes.Visible = True
                        GReportes.Visible = True
                        GAdministracion.Visible = False
                        GCodigos.Visible = True
                    Case "7" 'Digitador+Reportes
                        img_usuario.ToolTip = "Digitador+Reportes"
                        GIngreso.Visible = True
                        GPacientes.Visible = True
                        GReportes.Visible = True
                        GAdministracion.Visible = False
                        GCodigos.Visible = True
                End Select
            End If
        End If
    End Sub

    Function rol(ByVal r As String) As String
        Dim x As String = ""
        Select Case r
            Case "1"
                x = "Master"
            Case "2"
                x = "Administrador"
            Case "3"
                x = "Digitador"
            Case "4"
                x = "Consulta"
            Case "5"
                x = "Reportes"
            Case "6"
                x = "Supervisor"
            Case "7"
                x = "Digitador+Reportes"
        End Select
        Return x
    End Function

    Protected Sub IB_conexion_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_conexion.Click
        Response.Redirect("~/logout.aspx", False)
    End Sub

    Protected Sub btn_buscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_buscar.Click
        If txt_asi.Text.ToUpper().Trim <> String.Empty Then
            Dim NHC As String = txt_asi.Text.ToUpper()
            Response.Redirect("~/consultaReg.aspx?nhc=" + NHC, False)
        End If
    End Sub
End Class

