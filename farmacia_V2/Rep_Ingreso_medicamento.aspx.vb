Imports System.Data
Partial Class Rep_Ingreo_medicamento
    Inherits System.Web.UI.Page
    Private db As New BusinessLogicDB()
    Private revisar As New Rsesion()
    Public cn1 As String = ConfigurationManager.ConnectionStrings("conStringFarmacia").ConnectionString
    'Public cn2 As String = ConfigurationManager.ConnectionStrings("conString").ConnectionString

    Public errores As String = ""
    Public usuario As String = ""
    Public strnhc As String
    Public existenhc As Boolean


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
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
                obtieneDias(DDL_Mes.SelectedValue, DLL_Año.SelectedValue)
            End If
        End If
    End Sub

    Private Sub LlenaAnos()
        Dim currentYear As Integer = Year(Today)
        Dim firtsyear As Integer = 2016
        DLL_Año.Items.Clear()
        For y As Integer = firtsyear To currentYear
            DLL_Año.Items.Add(New ListItem(y, y))
        Next
        DLL_Año.SelectedValue = Year(Today)
    End Sub

    Private Sub selmes()
        DDL_Mes.SelectedValue = Month(Today)
    End Sub

    Function obtieneDias(ByVal mes As Integer, ByVal ano As Integer) As Integer
        Return DateTime.DaysInMonth(ano, mes)
    End Function

    Private Sub LlenaGv_RMedicamento()
        Dim fechaA As String = DLL_Año.SelectedValue.ToString()
        Dim fechaM As String = DDL_Mes.SelectedValue.ToString()
    End Sub

    Protected Sub btn_Generar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btn_Generar.Click

        Select Case ddl_Medicamento.SelectedValue
            Case 1
                Dim fechaA As String = DLL_Año.SelectedValue.ToString()
                Dim fechaM As String = DDL_Mes.SelectedValue.ToString.ToString()
                Dim ultimodia As String = obtieneDias(DDL_Mes.SelectedValue, DLL_Año.SelectedValue)
                usuario = Session("usuario").ToString()
                db.Cn1 = cn1
                Dim RepIngresoMedicamento As DataTable = db.RepIngresoMedicamentoARV(fechaA, fechaM, ultimodia, usuario)
                Try
                    Session("dspacA") = RepIngresoMedicamento
                    RIM_reportes.DataSource = RepIngresoMedicamento
                    RIM_reportes.DataBind()
                    lbl_titulo.Text = "Ingreso Medicamento  " & nombremes(DDL_Mes.SelectedValue) & " " & DLL_Año.SelectedValue
                Catch ex As Exception
                    lbl_error.Text = ex.Message
                End Try

            Case 2
                Dim fechaA As String = DLL_Año.SelectedValue.ToString()
                Dim fechaM As String = DDL_Mes.SelectedValue.ToString.ToString()
                Dim ultimodia As String = obtieneDias(DDL_Mes.SelectedValue, DLL_Año.SelectedValue)
                usuario = Session("usuario").ToString()
                db.Cn1 = cn1
                Dim RepIngresoMedicamento As DataTable = db.RepIngresoMedicamentoPROF(fechaA, fechaM, ultimodia, usuario)
                Try
                    Session("dspacA") = RepIngresoMedicamento
                    RIM_reportes.DataSource = RepIngresoMedicamento
                    RIM_reportes.DataBind()
                    lbl_titulo.Text = "Ingreso Medicamento  " & nombremes(DDL_Mes.SelectedValue) & " " & DLL_Año.SelectedValue
                Catch ex As Exception
                    lbl_error.Text = ex.Message
                End Try
        End Select
    End Sub

    Protected Sub IB_exportar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_exportar.Click
        exportar()
    End Sub

    Sub exportar()
        Dim nombre As String = "Rep_Ingreso_medicamento" & nombremes(DDL_Mes.SelectedValue) & DLL_Año.SelectedValue()
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

    Public Overloads Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
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

    Protected Sub RIM_reportes_SelectedIndexChanged(sender As Object, e As EventArgs) Handles RIM_reportes.SelectedIndexChanged

    End Sub
End Class
