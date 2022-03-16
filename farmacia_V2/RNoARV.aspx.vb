Imports System.Data
Imports System.ComponentModel

Partial Class RNoARV
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
                LlenaAnos()
                selmes()
                ObtieneDias(DDL_mes.SelectedValue, DDL_ano.SelectedValue)
            End If
        End If
    End Sub

    Private Sub selmes()
        DDL_mes.SelectedValue = Month(Today)
    End Sub

    Private Sub LlenaAnos()
        Dim currentYear As Integer = Year(Today)
        Dim firstyear As Integer = 2011
        DDL_ano.Items.Clear()
        For y As Integer = firstyear To currentYear
            DDL_ano.Items.Add(New ListItem(y, y))
        Next
        DDL_ano.SelectedValue = Year(Today)
    End Sub

    Function ObtieneDias(ByVal mes As Integer, ByVal ano As Integer) As Integer
        Return DateTime.DaysInMonth(ano, mes)
    End Function

    Function ObtieneFechaAnterior(ByVal mes As Integer, ByVal ano As Integer) As String
        Dim fecha As String = "01/" + Convert.ToString(mes) + "/" + Convert.ToString(ano)
        Dim f As DateTime = Convert.ToDateTime(fecha).ToString("dd/MM/yyyy")
        Return f.AddMonths(-1).ToString("MMyyyy")
    End Function

    Protected Sub btn_grabar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_grabar.Click
        Dim fechaA As String = DDL_ano.SelectedValue.ToString()
        Dim fechaM As String = DDL_mes.SelectedValue.ToString()
        Dim ultimodia As String = ObtieneDias(DDL_mes.SelectedValue, DDL_ano.SelectedValue)
        Dim fechaAA As String = ObtieneFechaAnterior(DDL_mes.SelectedValue, DDL_ano.SelectedValue).Substring(2, 4)
        Dim fechaMA As String = ObtieneFechaAnterior(DDL_mes.SelectedValue, DDL_ano.SelectedValue).Substring(0, 2)
        Dim ultimodiaA As String = ObtieneDias(Convert.ToInt32(fechaMA), Convert.ToInt32(fechaAA))
        db.Cn2 = cn2
        db.Cn1 = cn1
        usuario = Session("usuario").ToString()
        Dim tbpac1014 As DataTable = db.ReporteNoARV1("1014", fechaA, fechaM, ultimodia, fechaAA, fechaMA, ultimodiaA, usuario)
        GV_rnoarv10_14.DataSource = tbpac1014
        GV_rnoarv10_14.DataBind()
        Dim tbpac1518 As DataTable = db.ReporteNoARV1("1518", fechaA, fechaM, ultimodia, fechaAA, fechaMA, ultimodiaA, usuario)
        GV_rnoarv15_18.DataSource = tbpac1518
        GV_rnoarv15_18.DataBind()
        Dim tbpac1924 As DataTable = db.ReporteNoARV1("1924", fechaA, fechaM, ultimodia, fechaAA, fechaMA, ultimodiaA, usuario)
        GV_rnoarv19_24.DataSource = tbpac1924
        GV_rnoarv19_24.DataBind()
        Dim tbpac2549 As DataTable = db.ReporteNoARV1("2549", fechaA, fechaM, ultimodia, fechaAA, fechaMA, ultimodiaA, usuario)
        GV_rnoarv25_49.DataSource = tbpac2549
        GV_rnoarv25_49.DataBind()
        Dim tbpac50 As DataTable = db.ReporteNoARV1("50", fechaA, fechaM, ultimodia, fechaAA, fechaMA, ultimodiaA, usuario)
        GV_rnoarv50.DataSource = tbpac50
        GV_rnoarv50.DataBind()
        Dim tbpacTotal As DataTable = db.ReporteNoARV1("Total", fechaA, fechaM, ultimodia, fechaAA, fechaMA, ultimodiaA, usuario)
        GV_rnoarvTotal.DataSource = tbpacTotal
        GV_rnoarvTotal.DataBind()
    End Sub

    Public Overloads Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
    End Sub

    Protected Sub IB_exportar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_exportar.Click
        exportar()
    End Sub

    Sub exportar()
        Dim nombre As String = "NoARV_" & nombremes(DDL_mes.SelectedValue) & DDL_ano.SelectedValue
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

    Function nombremes(ByVal m As String) As String
        Dim x As String = String.Empty
        Select Case m
            Case "1"
                x = "Enero"
            Case "2"
                x = "Febrero"
            Case "3"
                x = "Marzo"
            Case "4"
                x = "Abril"
            Case "5"
                x = "Mayo"
            Case "6"
                x = "Junio"
            Case "7"
                x = "Julio"
            Case "8"
                x = "Agosto"
            Case "9"
                x = "Septiembre"
            Case "10"
                x = "Octubre"
            Case "11"
                x = "Noviembre"
            Case "12"
                x = "Diciembre"
        End Select
        Return x
    End Function

End Class
