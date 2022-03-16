Imports System.Data

Partial Class lprof
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
                LlenaGv()
            End If
        End If
    End Sub

    Private Sub LlenaGv()
        Try
            db.Cn1 = cn1
            usuario = Session("usuario").ToString()
            Dim tbcodprof As DataTable = db.busquedaCod("2", usuario)
            Session("dsffarv") = tbcodprof
            GV_codprof.DataSource = tbcodprof
            GV_codprof.DataBind()
            GV_codprof.SelectedIndex = 0
            'lbl_gv.Text = FormatNumber(tbfiltros.Rows.Count, 0, TriState.True, TriState.False, TriState.True).ToString() + " Encontrados"
        Catch ex As Exception
            'lbl_error.Text = "Hubo un error al mostrar las Propiedades."
            errores = (usuario & "|lprof.LLenaGv()|" & ex.ToString() & "|") + ex.Message
            db.GrabarErrores(errores)
        End Try
    End Sub

    Protected Sub btn_grabar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_grabar.Click
        db.Cn1 = cn1
        usuario = Session("usuario").ToString()
        If btn_grabar.Text = "GRABAR" Then
            db.GrabaLProf(txt_nomProf.Text, usuario)
            Response.Redirect("~/lprof.aspx", False)
        ElseIf btn_grabar.Text = "MODIFICAR" Then
            db.ActualizaLProf(Session("idFFARV").ToString(), txt_nomProf.Text.ToString(), usuario)
            Response.Redirect("~/lprof.aspx", False)
        End If
    End Sub

    Protected Sub btn_cancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_cancelar.Click
        Response.Redirect("~/lprof.aspx", False)
    End Sub

    Protected Sub GV_codprof_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GV_codprof.PageIndexChanging
        GV_codprof.DataSource = Session("dsffarv")
        GV_codprof.PageIndex = e.NewPageIndex
        GV_codprof.DataBind()
    End Sub

    Protected Sub GV_codarv_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles GV_codprof.PreRender
        Dim n As Integer = 0
        For Each nrow As GridViewRow In GV_codprof.Rows
            For columnIndex As Integer = n To Convert.ToInt32(GV_codprof.Rows.Count)
                Dim irow1 As ImageButton = DirectCast(nrow.FindControl("IB_editar"), ImageButton)
                irow1.CommandArgument = Convert.ToString(n)
            Next
            n += 1
        Next
    End Sub

    Protected Sub GV_codprof_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GV_codprof.RowCommand
        If e.CommandName = "Editar" Then
            Try
                db.Cn1 = cn1
                usuario = Session("usuario").ToString()
                Dim gv As GridView = DirectCast(sender, GridView)
                Dim rowIndex As Int32 = Convert.ToInt32(e.CommandArgument.ToString())
                Dim IdProf As String = gv.DataKeys(rowIndex)(0).ToString()
                Dim x As String = db.ContenidoCodProf(IdProf, usuario)
                Dim rp As String() = x.Split("|")
                If rp(0).ToString() = "True" Then
                    Session("idFFARV") = IdProf
                    txt_nomProf.Text = rp(2).ToString()
                    btn_grabar.Text = "MODIFICAR"
                    txt_nomProf.Focus()
                    'Lbl_etiqueta.Text = "MODIFICAR USUARIO"
                Else
                    lbl_error.Text = rp(1)
                End If
            Catch ex As Exception
                lbl_error.Text = ex.Message
            End Try
        End If
    End Sub
End Class
