Imports System.Data

Partial Class IOS_ITS
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
                x = "REPORTE IOS"
            Case "2"
                x = "LISTA PX IOS"
            Case "3"
                x = "REPORTE ITS"
            Case "4"
                x = "LISTA PX ITS"
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
        Dim fechaI As String = "01/" & DDL_mes.SelectedValue.ToString() & "/" & DDL_ano.SelectedValue.ToString()
        Dim fechaF As String = ObtieneDias(DDL_mes.SelectedValue, DDL_ano.SelectedValue) & "/" & DDL_mes.SelectedValue.ToString() & "/" & DDL_ano.SelectedValue.ToString()
        'Dim fechaI As String = DDL_ano.SelectedValue.ToString() & "-" & DDL_mes.SelectedValue.ToString() & "-01"
        'Dim fechaF As String = DDL_ano.SelectedValue.ToString() & "-" & DDL_mes.SelectedValue.ToString() & "-" & ObtieneDias(DDL_mes.SelectedValue, DDL_ano.SelectedValue)
        db.Cn2 = cn2
        usuario = Session("usuario").ToString()
        resultado.Visible = True
        tipoR = DDL_tipoR.SelectedValue
        Dim tbpacA As DataTable = db.ReportesMensualIOS_ITS(tipoR, fechaI, fechaF, usuario)
        Session("dspac") = tbpacA
        Select Case tipoR
            Case "1"
                GV_repIOS.DataSource = tbpacA
                GV_repIOS.DataBind()
                GV_repIOS.Visible = True
                GV_pxIOS.Visible = False
                GV_repITS.Visible = False
                GV_pxITS.Visible = False
            Case "2"
                GV_pxIOS.DataSource = tbpacA
                GV_pxIOS.DataBind()
                GV_repIOS.Visible = False
                GV_pxIOS.Visible = True
                GV_repITS.Visible = False
                GV_pxITS.Visible = False
            Case "3"
                GV_repITS.DataSource = tbpacA
                GV_repITS.DataBind()
                GV_repIOS.Visible = False
                GV_pxIOS.Visible = False
                GV_repITS.Visible = True
                GV_pxITS.Visible = False
            Case "4"
                GV_pxITS.DataSource = tbpacA
                GV_pxITS.DataBind()
                GV_repIOS.Visible = False
                GV_pxIOS.Visible = False
                GV_repITS.Visible = False
                GV_pxITS.Visible = True
        End Select
        lbl_titulo.Text = titulo(tipoR) & " - (" & tbpacA.Rows.Count.ToString() & ")"
    End Sub

    Private _M1014, _F1014, _T1014, _M1518, _F1518, _T1518, _M1924, _F1924, _T1924, _M2549, _F2549, _T2549, _M50, _F50, _T50, _TOTAL As Integer
    Protected Sub GV_repIOS_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GV_repIOS.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim xM1014 As Integer = CInt(DataBinder.Eval(e.Row.DataItem, "M1014"))
            _M1014 += xM1014
            Dim xF1014 As Integer = CInt(DataBinder.Eval(e.Row.DataItem, "F1014"))
            _F1014 += xF1014
            Dim xT1014 As Integer = CInt(DataBinder.Eval(e.Row.DataItem, "T1014"))
            _T1014 += xT1014
            Dim xM1518 As Integer = CInt(DataBinder.Eval(e.Row.DataItem, "M1518"))
            _M1518 += xM1518
            Dim xF1518 As Integer = CInt(DataBinder.Eval(e.Row.DataItem, "F1518"))
            _F1518 += xF1518
            Dim xT1518 As Integer = CInt(DataBinder.Eval(e.Row.DataItem, "T1518"))
            _T1518 += xT1518
            Dim xM1924 As Integer = CInt(DataBinder.Eval(e.Row.DataItem, "M1924"))
            _M1924 += xM1924
            Dim xF1924 As Integer = CInt(DataBinder.Eval(e.Row.DataItem, "F1924"))
            _F1924 += xF1924
            Dim xT1924 As Integer = CInt(DataBinder.Eval(e.Row.DataItem, "T1924"))
            _T1924 += xT1924
            Dim xM2549 As Integer = CInt(DataBinder.Eval(e.Row.DataItem, "M2549"))
            _M2549 += xM2549
            Dim xF2549 As Integer = CInt(DataBinder.Eval(e.Row.DataItem, "F2549"))
            _F2549 += xF2549
            Dim xT2549 As Integer = CInt(DataBinder.Eval(e.Row.DataItem, "T2549"))
            _T2549 += xT2549
            Dim xM50 As Integer = CInt(DataBinder.Eval(e.Row.DataItem, "M50"))
            _M50 += xM50
            Dim xF50 As Integer = CInt(DataBinder.Eval(e.Row.DataItem, "F50"))
            _F50 += xF50
            Dim xT50 As Integer = CInt(DataBinder.Eval(e.Row.DataItem, "T50"))
            _T50 += xT50
            Dim xTOTAL As Integer = CInt(DataBinder.Eval(e.Row.DataItem, "TOTAL"))
            _TOTAL += xTOTAL
        End If
        If e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(0).HorizontalAlign = HorizontalAlign.Left
            e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(4).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(5).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(6).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(7).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(8).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(9).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(10).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(11).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(12).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(13).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(14).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(15).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(16).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(17).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(0).Text = "TOTAL:"
            e.Row.Cells(2).Text = FormatNumber(_M1014.ToString(), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            e.Row.Cells(3).Text = FormatNumber(_F1014.ToString(), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            e.Row.Cells(4).Text = FormatNumber(_T1014.ToString(), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            e.Row.Cells(5).Text = FormatNumber(_M1518.ToString(), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            e.Row.Cells(6).Text = FormatNumber(_F1518.ToString(), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            e.Row.Cells(7).Text = FormatNumber(_T1518.ToString(), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            e.Row.Cells(8).Text = FormatNumber(_M1924.ToString(), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            e.Row.Cells(9).Text = FormatNumber(_F1924.ToString(), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            e.Row.Cells(10).Text = FormatNumber(_T1924.ToString(), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            e.Row.Cells(11).Text = FormatNumber(_M2549.ToString(), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            e.Row.Cells(12).Text = FormatNumber(_F2549.ToString(), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            e.Row.Cells(13).Text = FormatNumber(_T2549.ToString(), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            e.Row.Cells(14).Text = FormatNumber(_M50.ToString(), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            e.Row.Cells(15).Text = FormatNumber(_F50.ToString(), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            e.Row.Cells(16).Text = FormatNumber(_T50.ToString(), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            e.Row.Cells(17).Text = FormatNumber(_TOTAL.ToString(), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        End If
    End Sub

    Protected Sub GV_repITS_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GV_repITS.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim xM1014 As Integer = CInt(DataBinder.Eval(e.Row.DataItem, "M1014"))
            _M1014 += xM1014
            Dim xF1014 As Integer = CInt(DataBinder.Eval(e.Row.DataItem, "F1014"))
            _F1014 += xF1014
            Dim xT1014 As Integer = CInt(DataBinder.Eval(e.Row.DataItem, "T1014"))
            _T1014 += xT1014
            Dim xM1518 As Integer = CInt(DataBinder.Eval(e.Row.DataItem, "M1518"))
            _M1518 += xM1518
            Dim xF1518 As Integer = CInt(DataBinder.Eval(e.Row.DataItem, "F1518"))
            _F1518 += xF1518
            Dim xT1518 As Integer = CInt(DataBinder.Eval(e.Row.DataItem, "T1518"))
            _T1518 += xT1518
            Dim xM1924 As Integer = CInt(DataBinder.Eval(e.Row.DataItem, "M1924"))
            _M1924 += xM1924
            Dim xF1924 As Integer = CInt(DataBinder.Eval(e.Row.DataItem, "F1924"))
            _F1924 += xF1924
            Dim xT1924 As Integer = CInt(DataBinder.Eval(e.Row.DataItem, "T1924"))
            _T1924 += xT1924
            Dim xM2549 As Integer = CInt(DataBinder.Eval(e.Row.DataItem, "M2549"))
            _M2549 += xM2549
            Dim xF2549 As Integer = CInt(DataBinder.Eval(e.Row.DataItem, "F2549"))
            _F2549 += xF2549
            Dim xT2549 As Integer = CInt(DataBinder.Eval(e.Row.DataItem, "T2549"))
            _T2549 += xT2549
            Dim xM50 As Integer = CInt(DataBinder.Eval(e.Row.DataItem, "M50"))
            _M50 += xM50
            Dim xF50 As Integer = CInt(DataBinder.Eval(e.Row.DataItem, "F50"))
            _F50 += xF50
            Dim xT50 As Integer = CInt(DataBinder.Eval(e.Row.DataItem, "T50"))
            _T50 += xT50
            Dim xTOTAL As Integer = CInt(DataBinder.Eval(e.Row.DataItem, "TOTAL"))
            _TOTAL += xTOTAL
        End If
        If e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(0).HorizontalAlign = HorizontalAlign.Left
            e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(4).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(5).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(6).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(7).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(8).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(9).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(10).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(11).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(12).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(13).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(14).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(15).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(16).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(0).Text = "TOTAL:"
            e.Row.Cells(1).Text = FormatNumber(_M1014.ToString(), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            e.Row.Cells(2).Text = FormatNumber(_F1014.ToString(), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            e.Row.Cells(3).Text = FormatNumber(_T1014.ToString(), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            e.Row.Cells(4).Text = FormatNumber(_M1518.ToString(), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            e.Row.Cells(5).Text = FormatNumber(_F1518.ToString(), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            e.Row.Cells(6).Text = FormatNumber(_T1518.ToString(), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            e.Row.Cells(7).Text = FormatNumber(_M1924.ToString(), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            e.Row.Cells(8).Text = FormatNumber(_F1924.ToString(), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            e.Row.Cells(9).Text = FormatNumber(_T1924.ToString(), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            e.Row.Cells(10).Text = FormatNumber(_M2549.ToString(), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            e.Row.Cells(11).Text = FormatNumber(_F2549.ToString(), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            e.Row.Cells(12).Text = FormatNumber(_T2549.ToString(), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            e.Row.Cells(13).Text = FormatNumber(_M50.ToString(), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            e.Row.Cells(14).Text = FormatNumber(_F50.ToString(), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            e.Row.Cells(15).Text = FormatNumber(_T50.ToString(), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            e.Row.Cells(16).Text = FormatNumber(_TOTAL.ToString(), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        End If
    End Sub

    Public Overloads Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
    End Sub

    Protected Sub IB_exportar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_exportar.Click
        Dim nombre As String = titulo(DDL_tipoR.SelectedValue) & "_" & nombremes(DDL_mes.SelectedValue) & DDL_ano.SelectedValue
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
