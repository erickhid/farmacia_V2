Imports System.Data

Partial Class R90dias
    Inherits System.Web.UI.Page
    Private revisar As New Rsesion()
    Private db As New BusinessLogicDB()
    Public cn1 As String = ConfigurationManager.ConnectionStrings("conStringFarmacia").ConnectionString
    Public cn2 As String = ConfigurationManager.ConnectionStrings("conString").ConnectionString
    Public usuario As String = ""
    Public errores As String = ""

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
                'LlenaAnos()
                'selmes()
                'ObtieneDias(DDL_mes.SelectedValue, DDL_ano.SelectedValue)
            End If
        End If
    End Sub

    'Private Sub selmes()
    '    DDL_mes.SelectedValue = Month(Today)
    'End Sub

    'Private Sub LlenaAnos()
    '    Dim currentYear As Integer = Year(Today)
    '    Dim firstyear As Integer = 2011
    '    DDL_ano.Items.Clear()
    '    For y As Integer = firstyear To currentYear
    '        DDL_ano.Items.Add(New ListItem(y, y))
    '    Next
    '    DDL_ano.SelectedValue = Year(Today)
    'End Sub

    Function ObtieneDias(ByVal mes As Integer, ByVal ano As Integer) As Integer
        Return DateTime.DaysInMonth(ano, mes)
    End Function

    Protected Sub btn_grabar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_grabar.Click
        'Declaracion de variables locales A=año, M=mes
        'Con format se extrae el mes y el año del textbox
        Dim M As String = Format(Month(txt_fecha_I.Text), "00")
        Dim A As String = Format(Year(txt_fecha_I.Text), "0000")

        'Dim fechaA As String = DDL_ano.SelectedValue.ToString()
        'Dim fechaM As String = DDL_mes.SelectedValue.ToString()
		
		Dim Fecha As String = txt_fecha_I.Text
        Dim ultimodia As String = ObtieneDias(M, A)
        db.Cn1 = cn1
        usuario = Session("usuario").ToString()
        Dim tbpacA As DataTable = db.Reporte90dias(Fecha, ultimodia, usuario)
        Session("dspacA") = tbpacA
        GV_reportes.DataSource = tbpacA
        GV_reportes.DataBind()
        lbl_titulo.Text = CStr(tbpacA.Rows.Count.ToString()) & " Pacientes"
    End Sub

    Public Overloads Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
    End Sub

    Protected Sub IB_exportar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_exportar.Click
        Dim nombre As String = "Reporte_90dias_"
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
