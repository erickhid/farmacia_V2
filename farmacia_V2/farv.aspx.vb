Imports System.Data

Partial Class farv
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
            Dim tbffarv As DataTable = db.busquedaMedProf("1", usuario)
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

    Sub llenaProf()
        db.Cn1 = cn1
        usuario = Session("usuario").ToString()
        Dim tbMed As DataTable = db.ObtieneMedARVProf("1", usuario)
        If tbMed IsNot Nothing Then
            DDL_ARV.DataSource = tbMed
            DDL_ARV.DataTextField = "NomARV"
            DDL_ARV.DataValueField = "IdARV"
            DDL_ARV.DataBind()
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
            datos = DDL_ARV.SelectedValue.ToString() + "|" + DDL_ff.SelectedValue.ToString() + "|" + txt_concentracion.Text.ToString()
            db.GrabaFFARV(datos, usuario)
            Response.Redirect("~/farv.aspx", False)
        ElseIf btn_grabar.Text = "MODIFICAR" Then
            datos = txt_concentracion.Text.ToString()
            db.ActualizaFFARV(Session("idFFARV").ToString(), datos, usuario)
            Response.Redirect("~/farv.aspx", False)
        End If
    End Sub

    Protected Sub btn_cancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_cancelar.Click
        Response.Redirect("~/farv.aspx", False)
    End Sub

    Protected Sub GV_ffarv_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GV_ffarv.PageIndexChanging
        GV_ffarv.DataSource = Session("dsffarv")
        GV_ffarv.PageIndex = e.NewPageIndex
        GV_ffarv.DataBind()
    End Sub

    Protected Sub GV_ffarv_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles GV_ffarv.PreRender
        Dim n As Integer = 0
        For Each nrow As GridViewRow In GV_ffarv.Rows
            For columnIndex As Integer = n To Convert.ToInt32(GV_ffarv.Rows.Count)
                Dim irow1 As ImageButton = DirectCast(nrow.FindControl("IB_editar"), ImageButton)
                irow1.CommandArgument = Convert.ToString(n)
            Next
            n += 1
        Next
    End Sub

    Protected Sub GV_ffarv_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GV_ffarv.RowCommand
        If e.CommandName = "Editar" Then
            Try
                db.Cn1 = cn1
                usuario = Session("usuario").ToString()
                Dim gv As GridView = DirectCast(sender, GridView)
                Dim rowIndex As Int32 = Convert.ToInt32(e.CommandArgument.ToString())
                Dim idFFARV As String = gv.DataKeys(rowIndex)(0).ToString()
                Dim x As String = db.ContenidoFFARV(idFFARV, usuario)
                Dim rp As String() = x.Split("|")
                If rp(0).ToString() = "True" Then
                    Session("idFFARV") = idFFARV
                    DDL_ARV.SelectedValue = rp(1).ToString()
                    DDL_ARV.Enabled = False
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
