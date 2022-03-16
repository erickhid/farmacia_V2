Imports System.Data

Partial Class Pacientes
    Inherits System.Web.UI.Page
    Private revisar As New Rsesion()
    Private db As New BusinessLogicDB()
    Public cn1 As String = ConfigurationManager.ConnectionStrings("conStringFarmacia").ConnectionString
    Public cn2 As String = ConfigurationManager.ConnectionStrings("conString").ConnectionString
    Public usuario As String = ""
    Public errores As String = ""
    Public strnhc As String

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
                llenadatos(Request.QueryString("E").ToUpper())
            End If
        End If
    End Sub

    Private Sub llenadatos(ByVal opcion As String)
        Select Case opcion
            Case "A"
                A1.Visible = True
                A2.Visible = False
                P.Visible = True
                P1.Visible = True
                P2.Visible = False
                LlenaGvA()
                LlenaGvP()
            Case "P"
                A1.Visible = True
                A2.Visible = False
                P.Visible = False
                P1.Visible = False
                P2.Visible = False
                LlenaGvA_P()
                'LlenaGvP()
            Case "B"
                A1.Visible = False
                A2.Visible = True
                P.Visible = True
                P1.Visible = False
                P2.Visible = True
                LlenaGvA_A()
                LlenaGvP_A()
            Case "T"
                A1.Visible = False
                A2.Visible = True
                P.Visible = True
                P1.Visible = False
                P2.Visible = True
                LlenaGvA_T()
                LlenaGvP_T()
            Case "F"
                A1.Visible = False
                A2.Visible = True
                P.Visible = True
                P1.Visible = False
                P2.Visible = True
                LlenaGvA_F()
                LlenaGvP_F()
        End Select
    End Sub

    Private Sub LlenaGvA()
        Try
            db.Cn1 = cn1
            usuario = Session("usuario").ToString()
            Dim tbpacA As DataTable = db.ListaPacientes("A", "AC", usuario)
            Session("dspacA") = tbpacA
            GV_pacA.DataSource = tbpacA
            GV_pacA.DataBind()
            GV_pacA.SelectedIndex = 0
            lbl_cpacA.Text = "ACTIVOS - " + FormatNumber(tbpacA.Rows.Count, 0, TriState.True, TriState.False, TriState.True).ToString() + " Pacientes"
        Catch ex As Exception
            'lbl_error.Text = "Hubo un error al mostrar las Propiedades."
            errores = (usuario & "|Pacientes.LLenaGvA()|" & ex.ToString() & "|") + ex.Message
            db.GrabarErrores(errores)
        End Try
    End Sub

    Private Sub LlenaGvA_P()
        Try
            db.Cn1 = cn1
            usuario = Session("usuario").ToString()
            Dim tbpacA As DataTable = db.ListaPacientes("A", "PP", usuario)
            Session("dspacA") = tbpacA
            GV_pacA.DataSource = tbpacA
            GV_pacA.DataBind()
            GV_pacA.SelectedIndex = 0
            lbl_cpacA.Text = "POST-PARTO - " + FormatNumber(tbpacA.Rows.Count, 0, TriState.True, TriState.False, TriState.True).ToString() + " Pacientes"
        Catch ex As Exception
            'lbl_error.Text = "Hubo un error al mostrar las Propiedades."
            errores = (usuario & "|Pacientes.LLenaGvA_P()|" & ex.ToString() & "|") + ex.Message
            db.GrabarErrores(errores)
        End Try
    End Sub

    Private Sub LlenaGvA_A()
        Try
            db.Cn1 = cn1
            usuario = Session("usuario").ToString()
            Dim tbpacAA As DataTable = db.ListaPacientes("A", "AB", usuario)
            Session("dspacA") = tbpacAA
            GV_pacA_A.DataSource = tbpacAA
            GV_pacA_A.DataBind()
            GV_pacA_A.SelectedIndex = 0
            lbl_cpacA_A.Text = "ABANDONOS - " + FormatNumber(tbpacAA.Rows.Count, 0, TriState.True, TriState.False, TriState.True).ToString() + " Pacientes"
        Catch ex As Exception
            'lbl_error.Text = "Hubo un error al mostrar las Propiedades."
            errores = (usuario & "|Pacientes.LLenaGvA_A()|" & ex.ToString() & "|") + ex.Message
            db.GrabarErrores(errores)
        End Try
    End Sub

    Private Sub LlenaGvA_T()
        Try
            db.Cn1 = cn1
            usuario = Session("usuario").ToString()
            Dim tbpacAT As DataTable = db.ListaPacientes("A", "TR", usuario)
            Session("dspacA") = tbpacAT
            GV_pacA_A.DataSource = tbpacAT
            GV_pacA_A.DataBind()
            GV_pacA_A.SelectedIndex = 0
            lbl_cpacA_A.Text = "TRASLADOS - " + FormatNumber(tbpacAT.Rows.Count, 0, TriState.True, TriState.False, TriState.True).ToString() + " Pacientes"
        Catch ex As Exception
            'lbl_error.Text = "Hubo un error al mostrar las Propiedades."
            errores = (usuario & "|Pacientes.LLenaGvA_T()|" & ex.ToString() & "|") + ex.Message
            db.GrabarErrores(errores)
        End Try
    End Sub

    Private Sub LlenaGvA_F()
        Try
            db.Cn1 = cn1
            usuario = Session("usuario").ToString()
            Dim tbpacAF As DataTable = db.ListaPacientes("A", "FA", usuario)
            Session("dspacA") = tbpacAF
            GV_pacA_A.DataSource = tbpacAF
            GV_pacA_A.DataBind()
            GV_pacA_A.SelectedIndex = 0
            lbl_cpacA_A.Text = "FALLECIDOS - " + FormatNumber(tbpacAF.Rows.Count, 0, TriState.True, TriState.False, TriState.True).ToString() + " Pacientes"
        Catch ex As Exception
            'lbl_error.Text = "Hubo un error al mostrar las Propiedades."
            errores = (usuario & "|Pacientes.LLenaGvA_F()|" & ex.ToString() & "|") + ex.Message
            db.GrabarErrores(errores)
        End Try
    End Sub

    Private Sub LlenaGvP()
        Try
            db.Cn1 = cn1
            usuario = Session("usuario").ToString()
            Dim tbpacP As DataTable = db.ListaPacientes("P", "AC", usuario)
            Session("dspacP") = tbpacP
            GV_pacP.DataSource = tbpacP
            GV_pacP.DataBind()
            GV_pacP.SelectedIndex = 0
            lbl_cpacP.Text = "ACTIVOS - " + FormatNumber(tbpacP.Rows.Count, 0, TriState.True, TriState.False, TriState.True).ToString() + " Pacientes"
        Catch ex As Exception
            'lbl_error.Text = "Hubo un error al mostrar las Propiedades."
            errores = (usuario & "|Pacientes.LLenaGvP()|" & ex.ToString() & "|") + ex.Message
            db.GrabarErrores(errores)
        End Try
    End Sub

    Private Sub LlenaGvP_A()
        Try
            db.Cn1 = cn1
            usuario = Session("usuario").ToString()
            Dim tbpacPA As DataTable = db.ListaPacientes("P", "AB", usuario)
            Session("dspacP") = tbpacPA
            GV_pacP_A.DataSource = tbpacPA
            GV_pacP_A.DataBind()
            GV_pacP_A.SelectedIndex = 0
            lbl_cpacP_A.Text = "ABANDONOS - " + FormatNumber(tbpacPA.Rows.Count, 0, TriState.True, TriState.False, TriState.True).ToString() + " Pacientes"
        Catch ex As Exception
            'lbl_error.Text = "Hubo un error al mostrar las Propiedades."
            errores = (usuario & "|Pacientes.LLenaGvP_A()|" & ex.ToString() & "|") + ex.Message
            db.GrabarErrores(errores)
        End Try
    End Sub

    Private Sub LlenaGvP_T()
        Try
            db.Cn1 = cn1
            usuario = Session("usuario").ToString()
            Dim tbpacPT As DataTable = db.ListaPacientes("P", "TR", usuario)
            Session("dspacP") = tbpacPT
            GV_pacP_A.DataSource = tbpacPT
            GV_pacP_A.DataBind()
            GV_pacP_A.SelectedIndex = 0
            lbl_cpacP_A.Text = "TRASLADOS - " + FormatNumber(tbpacPT.Rows.Count, 0, TriState.True, TriState.False, TriState.True).ToString() + " Pacientes"
        Catch ex As Exception
            'lbl_error.Text = "Hubo un error al mostrar las Propiedades."
            errores = (usuario & "|Pacientes.LLenaGvP_T()|" & ex.ToString() & "|") + ex.Message
            db.GrabarErrores(errores)
        End Try
    End Sub

    Private Sub LlenaGvP_F()
        Try
            db.Cn1 = cn1
            usuario = Session("usuario").ToString()
            Dim tbpacPF As DataTable = db.ListaPacientes("P", "FA", usuario)
            Session("dspacP") = tbpacPF
            GV_pacP_A.DataSource = tbpacPF
            GV_pacP_A.DataBind()
            GV_pacP_A.SelectedIndex = 0
            lbl_cpacP_A.Text = "FALLECIDOS - " + FormatNumber(tbpacPF.Rows.Count, 0, TriState.True, TriState.False, TriState.True).ToString() + " Pacientes"
        Catch ex As Exception
            'lbl_error.Text = "Hubo un error al mostrar las Propiedades."
            errores = (usuario & "|Pacientes.LLenaGvP_F()|" & ex.ToString() & "|") + ex.Message
            db.GrabarErrores(errores)
        End Try
    End Sub

    Protected Sub GV_pacA_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles GV_pacA.PreRender
        Dim n As Integer = 0
        For Each nrow As GridViewRow In GV_pacA.Rows
            For columnIndex As Integer = n To Convert.ToInt32(GV_pacA.Rows.Count)
                Dim irow1 As ImageButton = DirectCast(nrow.FindControl("IB_Ver"), ImageButton)
                irow1.CommandArgument = Convert.ToString(n)
            Next
            n += 1
        Next
    End Sub

    Protected Sub GV_pacA_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GV_pacA.RowCommand
        If e.CommandName = "Ver" Then
            Try
                db.Cn1 = cn1
                usuario = Session("usuario").ToString()
                Dim gv As GridView = DirectCast(sender, GridView)
                Dim rowIndex As Int32 = Convert.ToInt32(e.CommandArgument.ToString())
                Dim NHC As String = gv.DataKeys(rowIndex)(0).ToString()
                Response.Redirect("~/consultaReg.aspx?nhc=" + NHC, False)
            Catch ex As Exception
                lbl_error.Text = ex.Message
            End Try
        End If
    End Sub

    Protected Sub GV_pacP_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles GV_pacP.PreRender
        Dim n As Integer = 0
        For Each nrow As GridViewRow In GV_pacP.Rows
            For columnIndex As Integer = n To Convert.ToInt32(GV_pacP.Rows.Count)
                Dim irow1 As ImageButton = DirectCast(nrow.FindControl("IB_Ver"), ImageButton)
                irow1.CommandArgument = Convert.ToString(n)
            Next
            n += 1
        Next
    End Sub

    Protected Sub GV_pacP_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GV_pacP.RowCommand
        If e.CommandName = "Ver" Then
            Try
                db.Cn1 = cn1
                usuario = Session("usuario").ToString()
                Dim gv As GridView = DirectCast(sender, GridView)
                Dim rowIndex As Int32 = Convert.ToInt32(e.CommandArgument.ToString())
                Dim NHC As String = gv.DataKeys(rowIndex)(0).ToString()
                Response.Redirect("~/consultaReg.aspx?nhc=" + NHC, False)
            Catch ex As Exception
                lbl_error.Text = ex.Message
            End Try
        End If
    End Sub

    Protected Sub GV_pacA_A_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles GV_pacA_A.PreRender
        Dim n As Integer = 0
        For Each nrow As GridViewRow In GV_pacA_A.Rows
            For columnIndex As Integer = n To Convert.ToInt32(GV_pacA_A.Rows.Count)
                Dim irow1 As ImageButton = DirectCast(nrow.FindControl("IB_Ver"), ImageButton)
                irow1.CommandArgument = Convert.ToString(n)
            Next
            n += 1
        Next
    End Sub

    Protected Sub GV_pacA_ALL_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GV_pacA_A.RowCommand
        If e.CommandName = "Ver" Then
            Try
                db.Cn1 = cn1
                usuario = Session("usuario").ToString()
                Dim gv As GridView = DirectCast(sender, GridView)
                Dim rowIndex As Int32 = Convert.ToInt32(e.CommandArgument.ToString())
                Dim NHC As String = gv.DataKeys(rowIndex)(0).ToString()
                Response.Redirect("~/consultaReg.aspx?nhc=" + NHC, False)
            Catch ex As Exception
                lbl_error.Text = ex.Message
            End Try
        End If
    End Sub

    Protected Sub GV_pacP_A_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles GV_pacP_A.PreRender
        Dim n As Integer = 0
        For Each nrow As GridViewRow In GV_pacP_A.Rows
            For columnIndex As Integer = n To Convert.ToInt32(GV_pacP_A.Rows.Count)
                Dim irow1 As ImageButton = DirectCast(nrow.FindControl("IB_Ver"), ImageButton)
                irow1.CommandArgument = Convert.ToString(n)
            Next
            n += 1
        Next
    End Sub

    Protected Sub GV_pacP_ALL_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GV_pacP_A.RowCommand
        If e.CommandName = "Ver" Then
            Try
                db.Cn1 = cn1
                usuario = Session("usuario").ToString()
                Dim gv As GridView = DirectCast(sender, GridView)
                Dim rowIndex As Int32 = Convert.ToInt32(e.CommandArgument.ToString())
                Dim NHC As String = gv.DataKeys(rowIndex)(0).ToString()
                Response.Redirect("~/consultaReg.aspx?nhc=" + NHC, False)
            Catch ex As Exception
                lbl_error.Text = ex.Message
            End Try
        End If
    End Sub

End Class
