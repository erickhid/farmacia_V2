Imports System.Data

Partial Class codfx
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
            Dim tbfx As DataTable = db.busquedaFx(usuario)
            Session("dsff") = tbfx
            GV_fx.DataSource = tbfx
            GV_fx.DataBind()
            GV_fx.SelectedIndex = -1
            'lbl_gv.Text = FormatNumber(tbfiltros.Rows.Count, 0, TriState.True, TriState.False, TriState.True).ToString() + " Encontrados"
        Catch ex As Exception
            'lbl_error.Text = "Hubo un error al mostrar las Propiedades."
            errores = (usuario & "|ff.LLenaGv()|" & ex.ToString() & "|") + ex.Message
            db.GrabarErrores(errores)
        End Try
    End Sub

    Protected Sub GV_fx_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GV_fx.PageIndexChanging
        GV_fx.DataSource = Session("dsff")
        GV_fx.PageIndex = e.NewPageIndex
        GV_fx.DataBind()
    End Sub

    Protected Sub btn_grabar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_grabar.Click
        db.Cn1 = cn1
        usuario = Session("usuario").ToString()
        If btn_grabar.Text = "GRABAR" Then
            db.GrabaFx(txt_codigo.Text, txt_descripcion.Text, usuario)
            Response.Redirect("~/codfx.aspx", False)
        ElseIf btn_grabar.Text = "MODIFICAR" Then
            db.ActualizaFx(Session("idFFARV").ToString(), txt_codigo.Text.ToString(), txt_descripcion.Text, usuario)
            Response.Redirect("~/codfx.aspx", False)
        End If
    End Sub

    Protected Sub btn_cancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_cancelar.Click
        Response.Redirect("~/ingresoARV.aspx", False)
    End Sub

    Protected Sub GV_fx_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles GV_fx.PreRender
        Dim n As Integer = 0
        For Each nrow As GridViewRow In GV_fx.Rows
            For columnIndex As Integer = n To Convert.ToInt32(GV_fx.Rows.Count)
                Dim irow1 As ImageButton = DirectCast(nrow.FindControl("IB_editar"), ImageButton)
                irow1.CommandArgument = Convert.ToString(n)
            Next
            n += 1
        Next
    End Sub

    Protected Sub GV_fx_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GV_fx.RowCommand
        If e.CommandName = "Editar" Then
            Try
                db.Cn1 = cn1
                usuario = Session("usuario").ToString()
                Dim gv As GridView = DirectCast(sender, GridView)
                Dim rowIndex As Int32 = Convert.ToInt32(e.CommandArgument.ToString())
                Dim IdFx As String = gv.DataKeys(rowIndex)(0).ToString()
                Dim x As String = db.ContenidoFx(IdFx, usuario)
                Dim rp As String() = x.Split("|")
                If rp(0).ToString() = "True" Then
                    Session("idFFARV") = IdFx
                    txt_codigo.Text = rp(2).ToString()
                    txt_descripcion.Text = rp(3).ToString()
                    btn_grabar.Text = "MODIFICAR"
                    txt_codigo.Focus()
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
