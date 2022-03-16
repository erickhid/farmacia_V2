Imports System.Data
Imports System.Text
Imports System.Drawing
Imports System.IO

Partial Class Corte_DiarioARV

    Inherits System.Web.UI.Page
    Private revisar As New Rsesion()
    Private db As New BusinessLogicDB()
    Public cn1 As String = ConfigurationManager.ConnectionStrings("conStringFarmacia").ConnectionString
    Public cn2 As String = ConfigurationManager.ConnectionStrings("conString").ConnectionString
    Public usuario As String = ""
    Public errores As String = ""
    Public fechasistema As String = DateTime.Now.ToString("dd/MM/yyyy")
    'Public strnhc As String
    'Public existenhc As Boolean
    'Public ufecha As Boolean
    'Public idufecha As String

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
                'llenaMedARV()
                'llenaMedARV(fechasistema)
                'llenacodigo()
                'ufecha = False
                'txt_asi.Focus()
                ''setcampos(0)
                'llenaTipoRelacion()
                'llenaNivelEducativo()
                'llenaSituacionLaboral()
                'llenaConoceDx()
                'llenaQuienesConocenDx()
                'llenaTipoVivienda()
                'llenaServicios()
                'llenaTipoConstruccion()
                'calculaIngresosEgresos()
                'llenaProblemasIdentificados()
                'pnl_CircuitoAdherencia.Visible = False ''panel de adherencia oculto
            End If
            'ElseIf Page.IsPostBack Then
            '    Dim wcICausedPostBack As WebControl = CType(GetControlThatCausedPostBack(TryCast(sender, Page)), WebControl)
            '    Dim indx As Integer = wcICausedPostBack.TabIndex
            '    Dim ctrl = From control In wcICausedPostBack.Parent.Controls.OfType(Of WebControl)() Where control.TabIndex > indx Order By control.TabIndex Select control
            '    ctrl.DefaultIfEmpty(wcICausedPostBack).First().Focus()
        End If
    End Sub

    Sub llenaMedARV()
        Try
            db.Cn1 = cn1
            usuario = Session("usuario").ToString()
            Dim Existencia_ARV As DataTable = db.ObtieneARV_FF_Existencias_D(usuario)
            Session("datosExistenciaARV") = Existencia_ARV
            GV_pnl_CorteARV.DataSource = Existencia_ARV
            GV_pnl_CorteARV.DataBind()
            'If GrupoFamiliar.Rows.Count > 0 Then
            '    llenafecha(1, "M")
            '    llenaARV(1)
            'Else
            '    llenafecha(0, "M")
            '    llenaARV(0)
            'End If
            'llenaMOTIVOCAMBIO()
        Catch ex As Exception
            'lbl_error.Text = "Hubo un error al mostrar listado de Pacientes."
            errores = (usuario & "|Existencia_ARV.Inventario()|" & ex.ToString() & "|") + ex.Message
            db.GrabarErrores(errores)
        End Try
    End Sub

    Protected Sub ibt_generar_Click(sender As Object, e As ImageClickEventArgs) Handles ibt_generar.Click
        usuario = Session("usuario").ToString()
        llenaMedARV()
        pnl_CorteARV.Visible = True
        db.Cn1 = cn1
        db.Graba_Corte_ARV_Existencia(usuario)

    End Sub
    Public Overloads Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
    End Sub

    Protected Sub IB_exportar_Click(sender As Object, e As ImageClickEventArgs) Handles IB_exportar.Click
        'Dim nombre As String = DDL_tipoR.SelectedItem.ToString()
        Dim html As String = "<p>There was a <b>.NET</b> programmer " + "and he stripped the <i>HTML</i> tags.</p>"
        Response.Clear()
        Response.AddHeader("content-disposition", "attachment;filename=" & "Corte_ARV" & "_" & fechasistema & ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.ContentType = "application/vnd.xls"
        'Response.ContentType = "text/plain"
        Dim stringwrite As System.IO.StringWriter = New System.IO.StringWriter
        Dim htmlwrite As System.Web.UI.HtmlTextWriter = New HtmlTextWriter(stringwrite)
        pnl_CorteARV.RenderControl(htmlwrite)

        Response.Write(stringwrite.ToString)

        ' Call Function.
        Dim tagless As String = StripTags(html)

        ' Write.
        Console.WriteLine(tagless)

        Response.End()

    End Sub

    Function StripTags(ByVal html As String) As String
        Return Regex.Replace(html, "<.*?>", "")
    End Function

End Class
