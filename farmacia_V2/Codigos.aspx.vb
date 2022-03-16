Imports System.Data

Partial Class Codigos
    Inherits System.Web.UI.Page
    Private revisar As New Rsesion()
    Private db As New BusinessLogicDB()
    Public cn1 As String = ConfigurationManager.ConnectionStrings("conStringFarmacia").ConnectionString
    Public cn2 As String = ConfigurationManager.ConnectionStrings("conString").ConnectionString
    Public usuario As String = ""
    Public errores As String = ""
    Public tipoR As String = String.Empty

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Response.Buffer = True
            Response.ExpiresAbsolute = DateTime.Now.AddDays(-1.0)
            Response.Expires = -1500
            Response.CacheControl = "no-cache"
            If Not revisar.RevisaSesion(Session("conexion").ToString(), Session("usuario").ToString()) Then
                Response.Redirect("~/inicio.aspx", False)
            Else
                usuario = Session("usuario").ToString()
                llenadatos(Request.QueryString("E").ToUpper())
                Session("codigo") = Request.QueryString("E").ToUpper()
            End If
        End If
    End Sub

    Private Sub llenadatos(ByVal tipo As String)
        Try
            db.Cn1 = cn1
            usuario = Session("usuario").ToString()
            Dim tb As DataTable = db.Codigos(tipo, usuario)
            Session("dspacA") = tb
            GV_codigos.DataSource = tb
            GV_codigos.DataBind()
            lbl_titulo.Text = titulo(tipo)
        Catch ex As Exception
            errores = (usuario & "|Codigos.llenadatos()|" & ex.ToString() & "|") + ex.Message
            db.GrabarErrores(errores)
        End Try
    End Sub

    Function titulo(ByVal tipo As String) As String
        Dim x As String = String.Empty
        Select Case tipo
            Case "1"
                x = "CODIGO ESTATUS"
            Case "2"
                x = "CODIGO FORMA FARMACEUTICA"
            Case "3"
                x = "CODIGO ARV"
            Case "4"
                x = "CODIGO FF ARV"
            Case "5"
                x = "CODIGO ESQUEMAS"
            Case "6"
                x = "CODIGO SUBESQUEMAS"
            Case "7"
                x = "CODIGO PROFILAXIS"
            Case "8"
                x = "CODIGO FF PROFILAXIS"
        End Select
        Return x
    End Function

    Public Overloads Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
    End Sub

    Protected Sub IB_exportar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_exportar.Click
        Dim nombre As String = "Reporte_" & titulo(Session("codigo").ToString())
        Response.Clear()
        Response.AddHeader("content-disposition", "attachment;filename=" & nombre & ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.ContentType = "application/vnd.xls"
        Dim stringWrite As System.IO.StringWriter = New System.IO.StringWriter
        Dim htmlWrite As System.Web.UI.HtmlTextWriter = New HtmlTextWriter(stringWrite)
        tbl_reporte.RenderControl(htmlWrite)
        Response.Write(stringWrite.ToString)
        Response.End()
    End Sub

End Class
