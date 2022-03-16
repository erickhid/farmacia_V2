Imports System.Data

Partial Class RepNoARV
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
                x = "NUEVOS"
            Case "2"
                x = "REINGRESOS"
            Case "3"
                x = "ABANDONOS"
            Case "4"
                x = "FALLECIDOS"
            Case "5"
                x = "TRASLADOS"
            Case "6"
                x = "CAMBIO EDAD"
            Case "7"
                x = "INICIAN TARV"
            Case "8"
                x = "TOTAL ACTIVOS"
            Case "9"
                x = "NO TARV ACTIVO"
            Case "10"
                x = "NO TARV/CONSULTA"
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
        Dim fechaI2 As String = "01/" & DDL_mes.SelectedValue.ToString() & "/" & DDL_ano.SelectedValue.ToString()
        Dim fechaF2 As String = ObtieneDias(DDL_mes.SelectedValue, DDL_ano.SelectedValue) & "/" & DDL_mes.SelectedValue.ToString() & "/" & DDL_ano.SelectedValue.ToString()
        db.Cn2 = cn2
        db.Cn1 = cn1
        usuario = Session("usuario").ToString()
        resultado.Visible = True
        tipoR = DDL_tipoR.SelectedValue
        Dim tbpacA As DataTable
        Dim tbresA As DataTable
        Select Case tipoR
            Case 1, 3, 5
                tbpacA = db.ReportesMensualNoARV(tipoR, fechaI, fechaF, usuario)
                tbresA = db.ReportesMensualNoARVResumen(tipoR, fechaI, fechaF, usuario)
                Session("dspacA") = tbpacA
                Session("dspacresA") = tbresA
                GV_reportes.Visible = True
                GV_reportes2.Visible = False
                GV_reportes.DataSource = tbpacA
                GV_reportes.DataBind()
                divresumen.Visible = True
                GV_resumen2.Visible = False
                GV_resumen.Visible = True
                GV_resumen.DataSource = tbresA
                GV_resumen.DataBind()
            Case 6
                tbpacA = db.ReportesMensualNoARV(tipoR, fechaI, fechaF, usuario)
                tbresA = db.ReportesMensualNoARVResumen(tipoR, fechaI, fechaF, usuario)
                Session("dspacA") = tbpacA
                Session("dspacresA") = tbresA
                GV_reportes.Visible = False
                GV_reportes2.Visible = True
                GV_reportes2.DataSource = tbpacA
                GV_reportes2.DataBind()
                divresumen.Visible = True
                GV_resumen.Visible = False
                GV_resumen2.Visible = True
                GV_resumen2.DataSource = tbresA
                GV_resumen2.DataBind()
            Case 2, 4, 7
                tbpacA = db.ReportesMensualNoARV(tipoR, fechaI2, fechaF2, usuario)
                tbresA = db.ReportesMensualNoARVResumen(tipoR, fechaI2, fechaF2, usuario)
                Session("dspacA") = tbpacA
                Session("dspacresA") = tbresA
                GV_reportes.Visible = True
                GV_reportes2.Visible = False
                GV_reportes.DataSource = tbpacA
                GV_reportes.DataBind()
                divresumen.Visible = True
                GV_resumen2.Visible = False
                GV_resumen.Visible = True
                GV_resumen.DataSource = tbresA
                GV_resumen.DataBind()
            Case 9
                tbpacA = db.ReportesMensualNoARV(tipoR, fechaI2, fechaF2, usuario)
                Session("dspacA") = tbpacA
                GV_reportes.Visible = True
                GV_reportes2.Visible = False
                GV_reportes.DataSource = tbpacA
                GV_reportes.DataBind()
                divresumen.Visible = True
                GV_resumen2.Visible = False
                GV_resumen.Visible = True
                GV_resumen.DataSource = tbresA
                GV_resumen.DataBind()
            Case 10
                tbpacA = db.ReportesMensualNoARV(tipoR, fechaI, fechaF, usuario)
                Session("dspacA") = tbpacA
                GV_reportes.Visible = True
                GV_reportes2.Visible = False
                GV_reportes.DataSource = tbpacA
                GV_reportes.DataBind()
                divresumen.Visible = False
                GV_resumen2.Visible = False
                GV_resumen.Visible = False
            Case 8
                tbpacA = db.ReportesMensualNoARV(tipoR, fechaI2, fechaF2, usuario)
                Session("dspacA") = tbpacA
                GV_reportes.Visible = True
                GV_reportes2.Visible = False
                GV_reportes.DataSource = tbpacA
                GV_reportes.DataBind()
                divresumen.Visible = False
                GV_resumen2.Visible = False
                GV_resumen.Visible = False
        End Select
        lbl_tituloA.Text = titulo(tipoR) & " - (" & tbpacA.Rows.Count.ToString() & ")"
    End Sub

    Private _T1, _T2, _T3, _T4, _T5, _T6 As Integer
    Protected Sub GV_resumen_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GV_resumen.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim xT1 As Integer = CInt(DataBinder.Eval(e.Row.DataItem, "R1014"))
            _T1 += xT1
            Dim xT2 As Integer = CInt(DataBinder.Eval(e.Row.DataItem, "R1518"))
            _T2 += xT2
            Dim xT3 As Integer = CInt(DataBinder.Eval(e.Row.DataItem, "R1924"))
            _T3 += xT3
            Dim xT4 As Integer = CInt(DataBinder.Eval(e.Row.DataItem, "R2549"))
            _T4 += xT4
            Dim xT5 As Integer = CInt(DataBinder.Eval(e.Row.DataItem, "R50"))
            _T5 += xT5
            Dim xT6 As Integer = CInt(DataBinder.Eval(e.Row.DataItem, "total"))
            _T6 += xT6
        End If
        If e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(0).Text = "TOTAL"
            e.Row.Cells(1).Text = FormatNumber(_T1.ToString(), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            e.Row.Cells(2).Text = FormatNumber(_T2.ToString(), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            e.Row.Cells(3).Text = FormatNumber(_T3.ToString(), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            e.Row.Cells(4).Text = FormatNumber(_T4.ToString(), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            e.Row.Cells(5).Text = FormatNumber(_T5.ToString(), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            e.Row.Cells(6).Text = FormatNumber(_T6.ToString(), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        End If
    End Sub

    Private _TE1, _TE2, _TE3, _TE4, _TE5, _TE6, _TE7, _TE8, _TE9, _TE10 As Integer
    Protected Sub GV_resumen2_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GV_resumen2.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim xT1 As Integer = CInt(DataBinder.Eval(e.Row.DataItem, "S1014"))
            _TE1 += xT1
            Dim xT2 As Integer = CInt(DataBinder.Eval(e.Row.DataItem, "S1518"))
            _TE2 += xT2
            Dim xT3 As Integer = CInt(DataBinder.Eval(e.Row.DataItem, "S1924"))
            _TE3 += xT3
            Dim xT4 As Integer = CInt(DataBinder.Eval(e.Row.DataItem, "S2549"))
            _TE4 += xT4
            Dim xT5 As Integer = CInt(DataBinder.Eval(e.Row.DataItem, "S50"))
            _TE5 += xT5
            Dim xT6 As Integer = CInt(DataBinder.Eval(e.Row.DataItem, "E1014"))
            _TE6 += xT6
            Dim xT7 As Integer = CInt(DataBinder.Eval(e.Row.DataItem, "E1518"))
            _TE7 += xT7
            Dim xT8 As Integer = CInt(DataBinder.Eval(e.Row.DataItem, "E1924"))
            _TE8 += xT8
            Dim xT9 As Integer = CInt(DataBinder.Eval(e.Row.DataItem, "E2549"))
            _TE9 += xT9
            Dim xT10 As Integer = CInt(DataBinder.Eval(e.Row.DataItem, "E50"))
            _TE10 += xT10
        End If
        If e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(0).Text = "TOTAL"
            e.Row.Cells(1).Text = FormatNumber(_TE1.ToString(), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            e.Row.Cells(2).Text = FormatNumber(_TE2.ToString(), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            e.Row.Cells(3).Text = FormatNumber(_TE3.ToString(), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            e.Row.Cells(4).Text = FormatNumber(_TE4.ToString(), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            e.Row.Cells(5).Text = FormatNumber(_TE5.ToString(), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            e.Row.Cells(6).Text = FormatNumber(_TE6.ToString(), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            e.Row.Cells(7).Text = FormatNumber(_TE7.ToString(), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            e.Row.Cells(8).Text = FormatNumber(_TE8.ToString(), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            e.Row.Cells(9).Text = FormatNumber(_TE9.ToString(), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            e.Row.Cells(10).Text = FormatNumber(_TE10.ToString(), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        End If
    End Sub

    Public Overloads Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
    End Sub

    Protected Sub IB_exportar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_exportar.Click
        Dim nombre As String = "RepNOARV_" & titulo(DDL_tipoR.SelectedValue) & "_" & nombremes(DDL_mes.SelectedValue) & DDL_ano.SelectedValue
        Response.Clear()
        Response.AddHeader("content-disposition", "attachment;filename=" & nombre & ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.ContentType = "application/vnd.xls"
        Dim stringWrite As System.IO.StringWriter = New System.IO.StringWriter
        Dim htmlWrite As System.Web.UI.HtmlTextWriter = New HtmlTextWriter(stringWrite)
        'PrepareForExport(GV_reportes)
        'PrepareForExport(GV_resumen)
        Dim tb As New Table()
        Dim tr1 As New TableRow()
        Dim cell1 As New TableCell()
        cell1.Controls.Add(divreporte)
        tr1.Cells.Add(cell1)
        Dim cell3 As New TableCell()
        cell3.Controls.Add(divresumen)
        Dim cell2 As New TableCell()
        cell2.Text = "&nbsp;"
        tr1.Cells.Add(cell2)
        tr1.Cells.Add(cell3)
        tb.Rows.Add(tr1)
        tb.RenderControl(htmlWrite)
        'style to format numbers to string
        Dim style As String = "<style> .textmode { mso-number-format:\@; } </style>"
        Response.Write(style)
        Response.Output.Write(stringWrite.ToString())
        Response.Flush()
        Response.End()
        'tbl_reporte.RenderControl(htmlWrite)
        'Response.Write(stringWrite.ToString)
        'Response.End()
    End Sub

    Protected Sub PrepareForExport(ByVal Gridview As GridView)
        'Gridview.AllowPaging = Convert.ToBoolean(rbPaging.SelectedItem.Value)
        'Gridview.DataBind()
        'Change the Header Row back to white color
        Gridview.HeaderRow.Style.Add("background-color", "#FFFFFF")
        'Apply style to Individual Cells
        For k As Integer = 0 To Gridview.HeaderRow.Cells.Count - 1
            Gridview.HeaderRow.Cells(k).Style.Add("background-color", "green")
        Next
        For i As Integer = 0 To Gridview.Rows.Count - 1
            Dim row As GridViewRow = Gridview.Rows(i)
            'Change Color back to white
            row.BackColor = System.Drawing.Color.White
            'Apply text style to each Row
            row.Attributes.Add("class", "textmode")
            'Apply style to Individual Cells of Alternating Row
            If i Mod 2 <> 0 Then
                For j As Integer = 0 To Gridview.Rows(i).Cells.Count - 1
                    row.Cells(j).Style.Add("background-color", "#C2D69B")
                Next
            End If
        Next
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
