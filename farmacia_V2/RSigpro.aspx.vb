Imports System.Data

Partial Class RSigpro
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

    Protected Sub btn_grabar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_grabar.Click
        btn_grabar.Enabled = False
        IB_exportar.Enabled = False
        DDL_mes.Enabled = False
        DDL_ano.Enabled = False
        Dim fecha As String = DDL_ano.SelectedValue.ToString() & "-" & DDL_mes.SelectedValue.ToString() & "-" & ObtieneDias(DDL_mes.SelectedValue, DDL_ano.SelectedValue)
        db.Cn1 = cn1
        usuario = Session("usuario").ToString()
        Dim tbpacA As DataTable = db.RepMensualSIGPRO(fecha, usuario)
        Session("dspacA") = tbpacA
        GV_rsigpro.DataSource = tbpacA
        GV_rsigpro.Visible = True
        GV_rsigpro.DataBind()
        btn_grabar.Enabled = True
        IB_exportar.Enabled = True
        DDL_mes.Enabled = True
        DDL_ano.Enabled = True
        lbl_titulo.Text = "REPORTE SIGPRO " & nombremes(DDL_mes.SelectedValue).ToUpper & " " & DDL_ano.SelectedValue
    End Sub

    Private _M1014, _F1014, _T1014, _M1518, _F1518, _T1518, _M1924, _F1924, _T1924, _M2549, _F2549, _T2549, _M50, _F50, _T50, _MT, _FT, _TT, _SUBT1, _PP1014, _PP1518, _PP1924, _PP2549, _PP50, _SUBT2, _EMB1014, _EMB1518, _EMB1924, _EMB2549, _EMB50, _SUBT3, _TOTAL As Integer
    Protected Sub GV_rsigpro_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GV_rsigpro.RowDataBound
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
            Dim xMT As Integer = CInt(DataBinder.Eval(e.Row.DataItem, "MT"))
            _MT += xMT
            Dim xFT As Integer = CInt(DataBinder.Eval(e.Row.DataItem, "FT"))
            _FT += xFT
            Dim xTT As Integer = CInt(DataBinder.Eval(e.Row.DataItem, "TT"))
            _TT += xTT
            Dim xSUBT1 As Integer = CInt(DataBinder.Eval(e.Row.DataItem, "SUBT1"))
            _SUBT1 += xSUBT1
            Dim xPP1014 As Integer = CInt(DataBinder.Eval(e.Row.DataItem, "PP1014"))
            _PP1014 += xPP1014
            Dim xPP1518 As Integer = CInt(DataBinder.Eval(e.Row.DataItem, "PP1518"))
            _PP1518 += xPP1518
            Dim xPP1924 As Integer = CInt(DataBinder.Eval(e.Row.DataItem, "PP1924"))
            _PP1924 += xPP1924
            Dim xPP2549 As Integer = CInt(DataBinder.Eval(e.Row.DataItem, "PP2549"))
            _PP2549 += xPP2549
            Dim xPP50 As Integer = CInt(DataBinder.Eval(e.Row.DataItem, "PP50"))
            _PP50 += xPP50
            Dim xSUBT2 As Integer = CInt(DataBinder.Eval(e.Row.DataItem, "SUBT2"))
            _SUBT2 += xSUBT2
            Dim xEMB1014 As Integer = CInt(DataBinder.Eval(e.Row.DataItem, "EMB1014"))
            _EMB1014 += xEMB1014
            Dim xEMB1518 As Integer = CInt(DataBinder.Eval(e.Row.DataItem, "EMB1518"))
            _EMB1518 += xEMB1518
            Dim xEMB1924 As Integer = CInt(DataBinder.Eval(e.Row.DataItem, "EMB1924"))
            _EMB1924 += xEMB1924
            Dim xEMB2549 As Integer = CInt(DataBinder.Eval(e.Row.DataItem, "EMB2549"))
            _EMB2549 += xEMB2549
            Dim xEMB50 As Integer = CInt(DataBinder.Eval(e.Row.DataItem, "EMB50"))
            _EMB50 += xEMB50
            Dim xSUBT3 As Integer = CInt(DataBinder.Eval(e.Row.DataItem, "SUBT3"))
            _SUBT3 += xSUBT3
            Dim xTOTAL As Integer = CInt(DataBinder.Eval(e.Row.DataItem, "TOTAL"))
            _TOTAL += xTOTAL
        End If
        If e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(1).Text = "TOTAL"
            e.Row.Cells(2).Text = FormatNumber(CInt(IIf(String.IsNullOrEmpty(_M1014.ToString()), "0", _M1014)), 0, , , True)
            e.Row.Cells(3).Text = FormatNumber(CInt(IIf(String.IsNullOrEmpty(_F1014.ToString()), "0", _F1014)), 0, , , True)
            e.Row.Cells(4).Text = FormatNumber(CInt(IIf(String.IsNullOrEmpty(_T1014.ToString()), "0", _T1014)), 0, , , True)
            e.Row.Cells(5).Text = FormatNumber(CInt(IIf(String.IsNullOrEmpty(_M1518.ToString()), "0", _M1518)), 0, , , True)
            e.Row.Cells(6).Text = FormatNumber(CInt(IIf(String.IsNullOrEmpty(_F1518.ToString()), "0", _F1518)), 0, , , True)
            e.Row.Cells(7).Text = FormatNumber(CInt(IIf(String.IsNullOrEmpty(_T1518.ToString()), "0", _T1518)), 0, , , True)
            e.Row.Cells(8).Text = FormatNumber(CInt(IIf(String.IsNullOrEmpty(_M1924.ToString()), "0", _M1924)), 0, , , True)
            e.Row.Cells(9).Text = FormatNumber(CInt(IIf(String.IsNullOrEmpty(_F1924.ToString()), "0", _F1924)), 0, , , True)
            e.Row.Cells(10).Text = FormatNumber(CInt(IIf(String.IsNullOrEmpty(_T1924.ToString()), "0", _T1924)), 0, , , True)
            e.Row.Cells(11).Text = FormatNumber(CInt(IIf(String.IsNullOrEmpty(_M2549.ToString()), "0", _M2549)), 0, , , True)
            e.Row.Cells(12).Text = FormatNumber(CInt(IIf(String.IsNullOrEmpty(_F2549.ToString()), "0", _F2549)), 0, , , True)
            e.Row.Cells(13).Text = FormatNumber(CInt(IIf(String.IsNullOrEmpty(_T2549.ToString()), "0", _T2549)), 0, , , True)
            e.Row.Cells(14).Text = FormatNumber(CInt(IIf(String.IsNullOrEmpty(_M50.ToString()), "0", _M50)), 0, , , True)
            e.Row.Cells(15).Text = FormatNumber(CInt(IIf(String.IsNullOrEmpty(_F50.ToString()), "0", _F50)), 0, , , True)
            e.Row.Cells(16).Text = FormatNumber(CInt(IIf(String.IsNullOrEmpty(_T50.ToString()), "0", _T50)), 0, , , True)
            e.Row.Cells(17).Text = FormatNumber(CInt(IIf(String.IsNullOrEmpty(_MT.ToString()), "0", _MT)), 0, , , True)
            e.Row.Cells(18).Text = FormatNumber(CInt(IIf(String.IsNullOrEmpty(_FT.ToString()), "0", _FT)), 0, , , True)
            e.Row.Cells(19).Text = FormatNumber(CInt(IIf(String.IsNullOrEmpty(_TT.ToString()), "0", _TT)), 0, , , True)
            e.Row.Cells(20).Text = FormatNumber(CInt(IIf(String.IsNullOrEmpty(_SUBT1.ToString()), "0", _SUBT1)), 0, , , True)
            e.Row.Cells(21).Text = FormatNumber(CInt(IIf(String.IsNullOrEmpty(_PP1014.ToString()), "0", _PP1014)), 0, , , True)
            e.Row.Cells(22).Text = FormatNumber(CInt(IIf(String.IsNullOrEmpty(_PP1518.ToString()), "0", _PP1518)), 0, , , True)
            e.Row.Cells(23).Text = FormatNumber(CInt(IIf(String.IsNullOrEmpty(_PP1924.ToString()), "0", _PP1924)), 0, , , True)
            e.Row.Cells(24).Text = FormatNumber(CInt(IIf(String.IsNullOrEmpty(_PP2549.ToString()), "0", _PP2549)), 0, , , True)
            e.Row.Cells(25).Text = FormatNumber(CInt(IIf(String.IsNullOrEmpty(_PP50.ToString()), "0", _PP50)), 0, , , True)
            e.Row.Cells(26).Text = FormatNumber(CInt(IIf(String.IsNullOrEmpty(_SUBT2.ToString()), "0", _SUBT2)), 0, , , True)
            e.Row.Cells(27).Text = FormatNumber(CInt(IIf(String.IsNullOrEmpty(_EMB1014.ToString()), "0", _EMB1014)), 0, , , True)
            e.Row.Cells(28).Text = FormatNumber(CInt(IIf(String.IsNullOrEmpty(_EMB1518.ToString()), "0", _EMB1518)), 0, , , True)
            e.Row.Cells(29).Text = FormatNumber(CInt(IIf(String.IsNullOrEmpty(_EMB1924.ToString()), "0", _EMB1924)), 0, , , True)
            e.Row.Cells(30).Text = FormatNumber(CInt(IIf(String.IsNullOrEmpty(_EMB2549.ToString()), "0", _EMB2549)), 0, , , True)
            e.Row.Cells(31).Text = FormatNumber(CInt(IIf(String.IsNullOrEmpty(_EMB50.ToString()), "0", _EMB50)), 0, , , True)
            e.Row.Cells(32).Text = FormatNumber(CInt(IIf(String.IsNullOrEmpty(_SUBT3.ToString()), "0", _SUBT3)), 0, , , True)
            e.Row.Cells(33).Text = FormatNumber(CInt(IIf(String.IsNullOrEmpty(_TOTAL.ToString()), "0", _TOTAL)), 0, , , True)
        End If
    End Sub

    Public Overloads Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
    End Sub

    Protected Sub IB_exportar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_exportar.Click
        Dim nombre As String = "ReporteSigpro_" & nombremes(DDL_mes.SelectedValue) & DDL_ano.SelectedValue
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
