Imports System.Data
Partial Class RepFar
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
            End If
        End If
    End Sub

    Function titulo(ByVal t As String) As String
        Dim x As String = String.Empty
        Select Case t
            Case "1"
                x = "MEDICAMENTOS CON FRECUENCIA"
            Case "2"
                x = "VISITAS"
            Case "3"
                x = "ESQUEMA"
            Case "3"
                x = "ESQUEMA PEDIATRICO"
        End Select
        Return x
    End Function

    'Function ObtieneDias(ByVal mes As Integer, ByVal ano As Integer) As Integer
    '    Return DateTime.DaysInMonth(ano, mes)
    'End Function



    'Protected Sub GV_reporte_DataBound(ByVal sender As Object, ByVal e As EventArgs) Handles GV_reporte.DataBound
    '    If GV_reporte.Rows.Count > 0 Then
    '        Dim TotalRows As Integer = GV_reporte.Rows.Count
    '        Dim TotalCol As Integer = GV_reporte.Rows(0).Cells.Count
    '        Dim FixedCol As Integer = 5
    '        Dim ComputedCol As Integer = TotalCol - FixedCol
    '        GV_reporte.FooterRow.Cells(FixedCol - 1).Text = "Total: "
    '        For i As Integer = FixedCol To TotalCol - 1
    '            Dim sum As Integer = 0
    '            For j As Integer = 0 To TotalRows - 1
    '                sum += If(GV_reporte.Rows(j).Cells(i).Text <> "&nbsp;", Integer.Parse(GV_reporte.Rows(j).Cells(i).Text), 0)
    '            Next
    '            GV_reporte.FooterRow.Cells(i).Text = FormatNumber(sum.ToString("0"), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
    '            'GV_rconsumo.FooterRow.Cells(i).Width = New Unit("60px")
    '        Next
    '    End If
    'End Sub

    Public Overloads Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
    End Sub

    Protected Sub IB_exportar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_exportar.Click
        Dim nombre As String = "Reporte_"
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

    'Protected Sub GV_reporte_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles GV_reporte.PreRender
    '    For Each r As GridViewRow In GV_reporte.Rows
    '        If r.RowType = DataControlRowType.DataRow Then
    '            Dim TotalCol As Integer = GV_reporte.Rows(0).Cells.Count
    '            Dim FixedCol As Integer = 5
    '            Dim ComputedCol As Integer = TotalCol - FixedCol
    '            For i As Integer = FixedCol To TotalCol - 1
    '                If r.Cells(i).Text <> "&nbsp;" Then
    '                    r.Cells(i).Text = FormatNumber(r.Cells(i).Text, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
    '                Else
    '                    r.Cells(i).Text = 0
    '                End If
    '                r.Cells(i).HorizontalAlign = HorizontalAlign.Right
    '            Next
    '            r.Cells(TotalCol - 1).Font.Bold = True

    '        End If
    '    Next
    'End Sub


    Protected Sub btn_generar_Click(sender As Object, e As EventArgs) Handles btn_generar.Click
        Dim FechaI As String = txt_fechai.Text
        Dim FechaF As String = txt_fechaf.Text
        db.Cn1 = cn1
        usuario = Session("usuario").ToString()
        tipoR = DDL_tipoR.SelectedValue
        Dim tb As DataTable = db.RepFarmacia(tipoR, FechaI, FechaF, usuario)
        Session("reporte") = tb
        GV_reporte.DataSource = tb
        GV_reporte.DataBind()
        lbl_titulo.Text = titulo(tipoR)
    End Sub
End Class
