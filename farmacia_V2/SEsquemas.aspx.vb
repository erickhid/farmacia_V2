Imports System.Data

Partial Class SEsquemas
    Inherits System.Web.UI.Page
    Private revisar As New Rsesion()
    Private db As New BusinessLogicDB()
    Public cn1 As String = ConfigurationManager.ConnectionStrings("conStringFarmacia").ConnectionString
    Public cn2 As String = ConfigurationManager.ConnectionStrings("conString").ConnectionString
    Public usuario As String = ""
    Public idE As String = ""
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
                idE = Session("idEsquema").ToString()
                If String.IsNullOrEmpty(idE) Then
                    Response.Redirect("~/Esquemas.aspx", False)
                Else
                    lbl_nesquema.Text = obesquema(idE)
                    LlenaGv(idE)
                    llenaARV(idE)
                End If
            End If
        End If
    End Sub

    Private Sub LlenaGv(ByVal id As String)
        Try
            db.Cn1 = cn1
            usuario = Session("usuario").ToString()
            Dim tbffarv As DataTable = db.SEsquemas(id, usuario)
            Session("dsffarv") = tbffarv
            GV_ffarv.DataSource = tbffarv
            GV_ffarv.DataBind()
            GV_ffarv.SelectedIndex = 0
            'lbl_gv.Text = FormatNumber(tbfiltros.Rows.Count, 0, TriState.True, TriState.False, TriState.True).ToString() + " Encontrados"
        Catch ex As Exception
            'lbl_error.Text = "Hubo un error al mostrar las Propiedades."
            errores = (usuario & "|farv.LLenaGv()|" & ex.ToString() & "|") + ex.Message
            db.GrabarErrores(errores)
        End Try
    End Sub

    Sub llenaARV(ByVal id As String)
        db.Cn1 = cn1
        usuario = Session("usuario").ToString()
        Dim tbMed As DataTable = db.ObtieneARVMedicamentoXid3(id, usuario)
        If tbMed IsNot Nothing Then
            DDL_arv.DataSource = tbMed
            DDL_arv.DataTextField = "Med"
            DDL_arv.DataValueField = "IdFFARV"
            DDL_arv.DataBind()
        End If
    End Sub

    Protected Sub btn_grabar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_grabar.Click
        db.Cn1 = cn1
        usuario = Session("usuario").ToString()
        'Dim datos As String
        'If btn_grabar.Text = "GRABAR" Then
        'datos = DDL_arv.SelectedValue.ToString() + "|" + DDL_ff.SelectedValue.ToString() + "|" + txt_concentracion.Text.ToString()
        db.GrabaSEsquemas(Session("idEsquema").ToString(), lbl_nsesquema.Text, lbl_sesquema.Text, usuario)
        Response.Redirect("~/SEsquemas.aspx", False)
        'ElseIf btn_grabar.Text = "MODIFICAR" Then
        '    datos = txt_concentracion.Text.ToString()
        '    db.ActualizaLARV(Session("idFFARV").ToString(), datos, usuario)
        '    Response.Redirect("~/farv.aspx", False)
        'End If
    End Sub

    Protected Sub btn_cancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_cancelar.Click
        Response.Redirect("~/Esquemas.aspx", False)
    End Sub

    Protected Sub GV_ffarv_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GV_ffarv.PageIndexChanging
        GV_ffarv.DataSource = Session("dsffarv")
        GV_ffarv.PageIndex = e.NewPageIndex
        GV_ffarv.DataBind()
    End Sub

    'Protected Sub GV_ffarv_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles GV_ffarv.PreRender
    '    Dim n As Integer = 0
    '    For Each nrow As GridViewRow In GV_ffarv.Rows
    '        For columnIndex As Integer = n To Convert.ToInt32(GV_ffarv.Rows.Count)
    '            Dim irow1 As ImageButton = DirectCast(nrow.FindControl("IB_editar"), ImageButton)
    '            irow1.CommandArgument = Convert.ToString(n)
    '        Next
    '        n += 1
    '    Next
    'End Sub

    Protected Sub GV_ffarv_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GV_ffarv.RowCommand
        'If e.CommandName = "Editar" Then
        '    Try
        '        db.Cn1 = cn1
        '        usuario = Session("usuario").ToString()
        '        Dim gv As GridView = DirectCast(sender, GridView)
        '        Dim rowIndex As Int32 = Convert.ToInt32(e.CommandArgument.ToString())
        '        Dim idFFARV As String = gv.DataKeys(rowIndex)(0).ToString()
        '        Dim x As String = db.ContenidoFFARV(idFFARV, usuario)
        '        Dim rp As String() = x.Split("|")
        '        If rp(0).ToString() = "True" Then
        '            Session("idFFARV") = idFFARV
        '            DDL_ARV.SelectedValue = rp(1).ToString()
        '            DDL_ARV.Enabled = False
        '            DDL_ff.SelectedValue = rp(2).ToString()
        '            DDL_ff.Enabled = False
        '            txt_concentracion.Text = rp(3).ToString()
        '            btn_grabar.Text = "MODIFICAR"
        '            txt_concentracion.Focus()
        '            'Lbl_etiqueta.Text = "MODIFICAR USUARIO"
        '        Else
        '            lbl_error.Text = rp(1)
        '        End If
        '    Catch ex As Exception
        '        lbl_error.Text = ex.Message
        '    End Try
        'End If
    End Sub

    Protected Sub btn_agregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_agregar.Click
        If lbl_sesquema.Text = String.Empty Then
            lbl_sesquema.Text = DDL_arv.SelectedValue()
        Else
            lbl_sesquema.Text += "," & DDL_arv.SelectedValue
        End If
        Dim ne As String = DDL_arv.SelectedItem.ToString() 'ARV(DDL_arv.SelectedValue())
        If lbl_nsesquema.Text = String.Empty Then
            lbl_nsesquema.Text = ne
        Else
            lbl_nsesquema.Text += " + " & ne
        End If
    End Sub

    Function ARV(ByVal id As String) As String
        db.Cn1 = cn1
        Dim x As String = db.ObtieneMedARV(id, usuario)
        Dim rp As String() = x.Split("|")
        Dim r As String = String.Empty
        If rp(0).ToString() = "True" Then
            r = rp(1).ToString()
        Else
            lbl_error.Text = rp(1)
            r = String.Empty
        End If
        Return r
    End Function

    Function obesquema(ByVal id As String) As String
        db.Cn1 = cn1
        Return db.ObtieneEsquema(id, usuario)
    End Function

    Protected Sub btn_limpiar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_limpiar.Click
        lbl_nsesquema.Text = String.Empty
        lbl_sesquema.Text = String.Empty
    End Sub
End Class
