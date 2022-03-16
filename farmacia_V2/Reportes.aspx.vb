Imports System.Data

Partial Class Reportes
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
                resultado.Visible = False
            End If
        End If
    End Sub

    Function titulo(ByVal t As String) As String
        Dim x As String = String.Empty
        Select Case t
            Case "1"
                x = "EMBARAZADAS"
            Case "2"
                x = "POSTPARTO"
            Case "3"
                x = "FALLECIDOS"
            Case "4"
                x = "ABANDONOS"
            Case "5"
                x = "TRASLADOS"
            Case "6"
                x = "INICIOS"
            Case "7"
                x = "REINICIOS"
            Case "8"
                x = "CAMBIOS"
            Case "9"
                x = "REFERIDOS"
            Case "10"
                x = "REINGRESOS"
            Case "11"
                x = "CAMBIOS FF"
        End Select
        Return x
    End Function

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

    Protected Sub btn_grabar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_grabar.Click
        Dim fechaI As String = DDL_ano.SelectedValue.ToString() & "-" & DDL_mes.SelectedValue.ToString() & "-01"
        Dim fechaF As String = DDL_ano.SelectedValue.ToString() & "-" & DDL_mes.SelectedValue.ToString() & "-" & ObtieneDias(DDL_mes.SelectedValue, DDL_ano.SelectedValue)
        db.Cn1 = cn1
        usuario = Session("usuario").ToString()
        resultado.Visible = True
        tipoR = DDL_tipoR.SelectedValue
        Dim tbpacA As DataTable = db.ReportesMensual(tipoR, fechaI, fechaF, usuario)
        Session("dspacA") = tbpacA
        GV_reportes.DataSource = tbpacA
        GV_reportes.DataBind()
        lbl_tituloA.Text = titulo(tipoR) & " - ADULTOS - (" & tbpacA.Rows.Count.ToString() & ")"
        Dim tbpacP As DataTable = db.ReportesMensual(tipoR & "P", fechaI, fechaF, usuario)
        Session("dspacP") = tbpacP
        GV_reportesP.DataSource = tbpacP
        GV_reportesP.DataBind()
        If tipoR.ToString() = "1" Or tipoR.ToString() = "2" Then
            lbl_tituloN.Text = String.Empty
        Else
            lbl_tituloN.Text = titulo(tipoR) & " - NIÑOS - (" & tbpacP.Rows.Count.ToString() & ")"
        End If
    End Sub

    'Private _TOTAL As Integer
    'Protected Sub GV_reportes_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GV_reportes.RowDataBound
    '    If e.Row.RowType = DataControlRowType.DataRow Then
    '        Dim xTOTAL As Integer = 1 'CInt(DataBinder.Eval(e.Row.DataItem, "IdGenero"))
    '        _TOTAL += xTOTAL
    '    End If
    '    If e.Row.RowType = DataControlRowType.Footer Then
    '        e.Row.Cells(0).Text = "TOTAL"
    '        e.Row.Cells(1).Text = FormatNumber(_TOTAL.ToString(), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
    '    End If
    'End Sub

    'Private _TOTALP As Integer
    'Protected Sub GV_reportesP_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GV_reportesP.RowDataBound
    '    If e.Row.RowType = DataControlRowType.DataRow Then
    '        Dim xTOTAL As Integer = 1 'CInt(DataBinder.Eval(e.Row.DataItem, "IdGenero"))
    '        _TOTALP += xTOTAL
    '    End If
    '    If e.Row.RowType = DataControlRowType.Footer Then
    '        e.Row.Cells(0).Text = "TOTAL"
    '        e.Row.Cells(1).Text = FormatNumber(_TOTALP.ToString(), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
    '    End If
    'End Sub

    Public Overloads Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
    End Sub

    Protected Sub IB_exportar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_exportar.Click
        Dim nombre As String = "Reporte_" & titulo(DDL_tipoR.SelectedValue) & "_" & nombremes(DDL_mes.SelectedValue) & DDL_ano.SelectedValue
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
