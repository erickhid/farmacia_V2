Imports System.Data

Partial Class RConsumo
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

    Function titulo(ByVal t As String) As String
        Dim x As String = String.Empty
        Select Case t
            Case "1"
                x = "ARVS"
            Case "2"
                x = "PROFILAXIS"
            Case "3"
                x = "ITS"
            Case "4"
                x = "IO"
            Case "5"
                x = "OTROS"
            Case "6"
                x = "MEDICAMENTOS"
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
        Dim fechaA As String = DDL_ano.SelectedValue.ToString()
        Dim fechaM As String = DDL_mes.SelectedValue.ToString()
        Dim ultimodia As String = ObtieneDias(DDL_mes.SelectedValue, DDL_ano.SelectedValue)
        db.Cn1 = cn1
        usuario = Session("usuario").ToString()
        tipoR = DDL_tipoR.SelectedValue
        Dim tbpacA As DataTable = db.ReporteConsumos(tipoR, fechaA, fechaM, ultimodia, usuario)
        Session("dspacA") = tbpacA
        GV_rconsumo.DataSource = tbpacA
        GV_rconsumo.DataBind()
        lbl_titulo.Text = titulo(tipoR)
    End Sub

    Protected Sub GV_rconsumo_DataBound(ByVal sender As Object, ByVal e As EventArgs) Handles GV_rconsumo.DataBound
        If GV_rconsumo.Rows.Count > 0 Then
            Dim TotalRows As Integer = GV_rconsumo.Rows.Count
            Dim TotalCol As Integer = GV_rconsumo.Rows(0).Cells.Count
            Dim FixedCol As Integer = 5
            Dim ComputedCol As Integer = TotalCol - FixedCol
            GV_rconsumo.FooterRow.Cells(FixedCol - 1).Text = "Total: "
            For i As Integer = FixedCol To TotalCol - 1
                Dim sum As Integer = 0
                For j As Integer = 0 To TotalRows - 1
                    sum += If(GV_rconsumo.Rows(j).Cells(i).Text <> "&nbsp;", Integer.Parse(GV_rconsumo.Rows(j).Cells(i).Text), 0)
                Next
                GV_rconsumo.FooterRow.Cells(i).Text = FormatNumber(sum.ToString("0"), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                'GV_rconsumo.FooterRow.Cells(i).Width = New Unit("60px")
            Next
        End If
    End Sub

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

    Protected Sub GV_rconsumo_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles GV_rconsumo.PreRender
        For Each r As GridViewRow In GV_rconsumo.Rows
            If r.RowType = DataControlRowType.DataRow Then
                Dim TotalCol As Integer = GV_rconsumo.Rows(0).Cells.Count
                Dim FixedCol As Integer = 5
                Dim ComputedCol As Integer = TotalCol - FixedCol
                For i As Integer = FixedCol To TotalCol - 1
                    If r.Cells(i).Text <> "&nbsp;" Then
                        r.Cells(i).Text = FormatNumber(r.Cells(i).Text, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                    Else
                        r.Cells(i).Text = 0
                    End If
                    r.Cells(i).HorizontalAlign = HorizontalAlign.Right
                Next
                r.Cells(TotalCol - 1).Font.Bold = True
                'ElseIf r.RowType = DataControlRowType.Header Then
                '    Dim TotalCol1 As Integer = GV_rconsumo.Rows(0).Cells.Count
                '    For k As Integer = 0 To TotalCol1 - 1
                '        r.Cells(k).Width = New Unit("60px")
                '    Next
            End If
        Next
    End Sub

End Class
