Imports System.Data

Partial Class fprof
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
                llenaProf()
                llenaff()
            End If
        End If
    End Sub

    Private Sub LlenaGv()
        Try
            db.Cn1 = cn1
            usuario = Session("usuario").ToString()
            Dim tbffprof As DataTable = db.busquedaMedProf("2", usuario)
            Session("dsffprof") = tbffprof
            GV_ffprof.DataSource = tbffprof
            GV_ffprof.DataBind()
            GV_ffprof.SelectedIndex = 0
            'lbl_gv.Text = FormatNumber(tbfiltros.Rows.Count, 0, TriState.True, TriState.False, TriState.True).ToString() + " Encontrados"
        Catch ex As Exception
            'lbl_error.Text = "Hubo un error al mostrar las Propiedades."
            errores = (usuario & "|fprof.LLenaGv()|" & ex.ToString() & "|") + ex.Message
            db.GrabarErrores(errores)
        End Try
    End Sub

    Sub llenaProf()
        db.Cn1 = cn1
        usuario = Session("usuario").ToString()
        Dim tbMed As DataTable = db.ObtieneMedARVProf("2", usuario)
        If tbMed IsNot Nothing Then
            DDL_prof.DataSource = tbMed
            DDL_prof.DataTextField = "NomProfilaxis"
            DDL_prof.DataValueField = "IdProf"
            DDL_prof.DataBind()
        End If
    End Sub

    Sub llenaff()
        db.Cn1 = cn1
        usuario = Session("usuario").ToString()
        Dim tbFF As DataTable = db.ObtieneFF(usuario)
        If tbFF IsNot Nothing Then
            DDL_ff.DataSource = tbFF
            DDL_ff.DataTextField = "NomFF"
            DDL_ff.DataValueField = "IdFF"
            DDL_ff.DataBind()
        End If
    End Sub

    Protected Sub btn_grabar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_grabar.Click
        db.Cn1 = cn1
        usuario = Session("usuario").ToString()
        Dim datos As String
        If btn_grabar.Text = "GRABAR" Then
            datos = DDL_prof.SelectedValue.ToString() + "|" + DDL_ff.SelectedValue.ToString() + "|" + txt_concentracion.Text.ToString()
            db.GrabaFFProf(datos, usuario)
            Response.Redirect("~/fprof.aspx", False)
        ElseIf btn_grabar.Text = "MODIFICAR" Then
            datos = txt_concentracion.Text.ToString()
            db.ActualizaFFProf(Session("idFFARV").ToString(), datos, usuario)
            Response.Redirect("~/fprof.aspx", False)
        End If
    End Sub

    Protected Sub btn_cancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_cancelar.Click
        Response.Redirect("~/fprof.aspx", False)
    End Sub

    Protected Sub GV_ffprof_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GV_ffprof.PageIndexChanging
        GV_ffprof.DataSource = Session("dsffprof")
        GV_ffprof.PageIndex = e.NewPageIndex
        GV_ffprof.DataBind()
    End Sub

    Protected Sub GV_ffprof_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles GV_ffprof.PreRender
        Dim n As Integer = 0
        For Each nrow As GridViewRow In GV_ffprof.Rows
            For columnIndex As Integer = n To Convert.ToInt32(GV_ffprof.Rows.Count)
                Dim irow1 As ImageButton = DirectCast(nrow.FindControl("IB_editar"), ImageButton)
                irow1.CommandArgument = Convert.ToString(n)
            Next
            n += 1
        Next
    End Sub

    Protected Sub GV_ffprof_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GV_ffprof.RowCommand
        If e.CommandName = "Editar" Then
            Try
                db.Cn1 = cn1
                usuario = Session("usuario").ToString()
                Dim gv As GridView = DirectCast(sender, GridView)
                Dim rowIndex As Int32 = Convert.ToInt32(e.CommandArgument.ToString())
                Dim idFFProf As String = gv.DataKeys(rowIndex)(0).ToString()
                Dim x As String = db.ContenidoFFProf(idFFProf, usuario)
                Dim rp As String() = x.Split("|")
                If rp(0).ToString() = "True" Then
                    Session("idFFARV") = idFFProf
                    DDL_prof.SelectedValue = rp(1).ToString()
                    DDL_prof.Enabled = False
                    DDL_ff.SelectedValue = rp(2).ToString()
                    DDL_ff.Enabled = False
                    txt_concentracion.Text = rp(3).ToString()
                    btn_grabar.Text = "MODIFICAR"
                    txt_concentracion.Focus()
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
