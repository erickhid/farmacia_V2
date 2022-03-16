Imports System.Data

Partial Class ingresoProf
    Inherits System.Web.UI.Page
    Private revisar As New Rsesion()
    Private db As New BusinessLogicDB()
    Public cn1 As String = ConfigurationManager.ConnectionStrings("conStringFarmacia").ConnectionString
    Public cn2 As String = ConfigurationManager.ConnectionStrings("conString").ConnectionString
    Public usuario As String = ""
    Public errores As String = ""
    Public strnhc As String
    Public existenhc As Boolean
    Public ufecha As Boolean
    Public idufecha As String

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
                lblUsuario.Value = usuario
                lblUnidadAtencion.Value = Session("ua").ToString()
                ufecha = False
                txt_asi.Focus()
                setcampos(0)
                llenacodigo()
                llenafrecuencia()
                llenaEstatus()
                llenaVIA()
            End If
        End If
    End Sub

    Sub setcampos(ByVal valor As Integer)
        'txt_cant1.Text = ""
        'txt_dx1.Text = ""
        'txt_cant2.Text = ""
        'txt_dx2.Text = ""
        'txt_cant3.Text = ""
        'txt_dx3.Text = ""
        'txt_cant4.Text = ""
        'txt_dx4.Text = ""
        Select Case valor
            Case 1
                txt_dx1.Enabled = False
                DDL_Via1.Enabled = False
                DDL_fx1.Enabled = False
                txt_cant1.Enabled = False
                DDL_t1.Enabled = False
                DDL_Trat1.Visible = False
                DDL_e1.Enabled = False
                txt_tdias1.Enabled = False
                txt_obs1.Enabled = False
            Case 2
                txt_dx2.Enabled = False
                DDL_Via2.Enabled = False
                DDL_fx2.Enabled = False
                txt_cant2.Enabled = False
                DDL_t2.Enabled = False
                DDL_Trat2.Visible = False
                DDL_e2.Enabled = False
                txt_tdias2.Enabled = False
                txt_obs2.Enabled = False
            Case 3
                txt_dx3.Enabled = False
                DDL_Via3.Enabled = False
                DDL_fx3.Enabled = False
                txt_cant3.Enabled = False
                DDL_t3.Enabled = False
                DDL_Trat3.Visible = False
                DDL_e3.Enabled = False
                txt_tdias3.Enabled = False
                txt_obs3.Enabled = False
            Case 4
                txt_dx4.Enabled = False
                DDL_Via4.Enabled = False
                DDL_fx4.Enabled = False
                txt_cant4.Enabled = False
                DDL_t4.Enabled = False
                DDL_Trat4.Visible = False
                DDL_e4.Enabled = False
                txt_tdias4.Enabled = False
                txt_obs4.Enabled = False
            Case 5
                txt_dx5.Enabled = False
                DDL_Via5.Enabled = False
                DDL_fx5.Enabled = False
                txt_cant5.Enabled = False
                DDL_t5.Enabled = False
                DDL_Trat5.Visible = False
                DDL_e5.Enabled = False
                txt_tdias5.Enabled = False
                txt_obs5.Enabled = False
            Case 6
                txt_dx6.Enabled = False
                DDL_Via6.Enabled = False
                DDL_fx6.Enabled = False
                txt_cant6.Enabled = False
                DDL_t6.Enabled = False
                DDL_Trat6.Visible = False
                DDL_e6.Enabled = False
                txt_tdias6.Enabled = False
                txt_obs6.Enabled = False
            Case 0
                txt_dx1.Enabled = False
                DDL_Via1.Enabled = False
                DDL_fx1.Enabled = False
                txt_cant1.Enabled = False
                DDL_t1.Enabled = False
                DDL_Trat1.Visible = False
                DDL_e1.Enabled = False
                txt_tdias1.Enabled = False
                txt_obs1.Enabled = False
                txt_dx2.Enabled = False
                DDL_Via2.Enabled = False
                DDL_fx2.Enabled = False
                txt_cant2.Enabled = False
                DDL_t2.Enabled = False
                DDL_Trat2.Visible = False
                DDL_e2.Enabled = False
                txt_tdias2.Enabled = False
                txt_obs2.Enabled = False
                txt_dx3.Enabled = False
                DDL_Via3.Enabled = False
                DDL_fx3.Enabled = False
                txt_cant3.Enabled = False
                DDL_t3.Enabled = False
                DDL_Trat3.Visible = False
                DDL_e3.Enabled = False
                txt_tdias3.Enabled = False
                txt_obs3.Enabled = False
                txt_dx4.Enabled = False
                DDL_Via4.Enabled = False
                DDL_fx4.Enabled = False
                txt_cant4.Enabled = False
                DDL_t4.Enabled = False
                DDL_Trat4.Visible = False
                DDL_e4.Enabled = False
                txt_tdias4.Enabled = False
                txt_obs4.Enabled = False
                txt_dx5.Enabled = False
                DDL_Via5.Enabled = False
                DDL_fx5.Enabled = False
                txt_cant5.Enabled = False
                DDL_t5.Enabled = False
                DDL_Trat5.Visible = False
                DDL_e5.Enabled = False
                txt_tdias5.Enabled = False
                txt_obs5.Enabled = False
                txt_dx6.Enabled = False
                DDL_Via6.Enabled = False
                DDL_fx6.Enabled = False
                txt_cant6.Enabled = False
                DDL_t6.Enabled = False
                DDL_Trat6.Visible = False
                DDL_e6.Enabled = False
                txt_tdias6.Enabled = False
                txt_obs6.Enabled = False
        End Select
    End Sub

    Sub llenadatos(ByVal nhc As String)
	 'Llena las cajas de texto de fecha entrega
        Dim fechayear As String
        fechayear = DateTime.Now.ToString("yy")
        txt_fe_yy.Text = fechayear
        Dim tipo As String
        usuario = Session("usuario").ToString()
        If nhc.Substring(1, 1).ToUpper.ToString() = "P" Then
            tipo = "P"
        Else
            tipo = "A"
        End If
        If tipo = "A" Then
            db.Cn2 = cn2
            Dim x As String = db.ObtieneBasales(nhc, usuario)
            Dim rp As String() = x.Split("|")
            If rp(0).ToString() = "True" Then
                strnhc = nhc
                Session("nhc") = nhc
                existenhc = True
                lbl_genero.Text = String.Empty
                lbl_nombre.Text = String.Empty
                lbl_telefono.Text = String.Empty
                lbl_nacimiento.Text = String.Empty
                lbl_domicilio.Text = String.Empty
                lbl_estatus.Text = String.Empty
                lbl_genero.Text = rp(1).ToString()
                lbl_nombre.Text = rp(2).ToString()
                lbl_telefono.Text = rp(3).ToString()
                lbl_nacimiento.Text = rp(4).ToString()
                lbl_domicilio.Text = rp(5).ToString()
                lbl_estatus.Text = rp(6).ToString()
                lbl_error.Text = String.Empty
                btn_editar.Visible = False
                btn_agregar.Visible = False
            Else
                lbl_error.Text = rp(1)
                existenhc = False
                btn_editar.Visible = False
                btn_agregar.Visible = False
            End If
        ElseIf tipo = "P" Then
            db.Cn1 = cn1
            Dim x As String = db.ObtieneBasalesP(nhc, usuario)
            Dim rpP As String() = x.Split("|")
            If rpP(0).ToString() = "True" Then
                strnhc = nhc
                Session("nhc") = nhc
                existenhc = True
                lbl_genero.Text = String.Empty
                lbl_nombre.Text = String.Empty
                lbl_telefono.Text = String.Empty
                lbl_nacimiento.Text = String.Empty
                lbl_domicilio.Text = String.Empty
                lbl_estatus.Text = String.Empty
                lbl_genero.Text = rpP(1).ToString()
                lbl_nombre.Text = rpP(2).ToString()
                lbl_telefono.Text = rpP(3).ToString()
                lbl_nacimiento.Text = rpP(4).ToString()
                lbl_domicilio.Text = rpP(5).ToString()
                lbl_estatus.Text = rpP(6).ToString()
                lbl_error.Text = String.Empty
                btn_editar.Visible = True
                btn_agregar.Visible = False
            Else
                lbl_error.Text = rpP(1)
                existenhc = False
                btn_editar.Visible = False
                btn_agregar.Visible = True
            End If
        End If
        db.Cn1 = cn1
        Dim y As String = db.ObtieneUltimoRegProf(nhc, usuario)
        Dim rpU As String() = y.Split("|")
        If rpU(0).ToString() = "True" Then
            ufecha = True
            Session("ufecha") = True
            lbl_ultimafechaentrega.Text = String.Empty
            idufecha = rpU(1).ToString()
            Session("idufecha") = rpU(1).ToString()
            lbl_ultimafechaentrega.Text = rpU(2).ToString()
        Else
            ufecha = False
            Session("ufecha") = False
            Session("idufecha") = ""
            idufecha = ""
            lbl_ultimafechaentrega.Text = rpU(1)
        End If
        db.Cn1 = cn1
        Dim z As String = db.ObtieneUltimoReg(nhc, usuario)
        Dim rpUP As String() = z.Split("|")
        If rpUP(0).ToString() = "True" Then
            lbl_estatusfarmacia.Text = String.Empty
            lbl_estatusfarmacia.Text = rpUP(11).ToString()
        Else
            lbl_estatusfarmacia.Text = "N/A"
        End If
    End Sub

    Function dev(ByVal valor As String) As Boolean
        If valor = String.Empty Then
            Return False
        Else
            Return True
        End If
    End Function

    Sub llenacodigo()
        db.Cn1 = cn1
        usuario = Session("usuario").ToString()
        Dim tbMed As DataTable = db.ObtieneARVMedicamento("2", usuario)
        If tbMed IsNot Nothing Then
            DDL_cod1.DataSource = tbMed
            DDL_cod1.DataTextField = "Codigo"
            DDL_cod1.DataValueField = "IdFFProf"
            DDL_cod1.DataBind()
            DDL_cod1.Items.Insert(0, New ListItem("", "0"))
            DDL_cod2.DataSource = tbMed
            DDL_cod2.DataTextField = "Codigo"
            DDL_cod2.DataValueField = "IdFFProf"
            DDL_cod2.DataBind()
            DDL_cod2.Items.Insert(0, New ListItem("", "0"))
            DDL_cod3.DataSource = tbMed
            DDL_cod3.DataTextField = "Codigo"
            DDL_cod3.DataValueField = "IdFFProf"
            DDL_cod3.DataBind()
            DDL_cod3.Items.Insert(0, New ListItem("", "0"))
            DDL_cod4.DataSource = tbMed
            DDL_cod4.DataTextField = "Codigo"
            DDL_cod4.DataValueField = "IdFFProf"
            DDL_cod4.DataBind()
            DDL_cod4.Items.Insert(0, New ListItem("", "0"))
            DDL_cod5.DataSource = tbMed
            DDL_cod5.DataTextField = "Codigo"
            DDL_cod5.DataValueField = "IdFFProf"
            DDL_cod5.DataBind()
            DDL_cod5.Items.Insert(0, New ListItem("", "0"))
            DDL_cod6.DataSource = tbMed
            DDL_cod6.DataTextField = "Codigo"
            DDL_cod6.DataValueField = "IdFFProf"
            DDL_cod6.DataBind()
            DDL_cod6.Items.Insert(0, New ListItem("", "0"))
        End If
    End Sub

    Sub llenafrecuencia()
        db.Cn1 = cn1
        usuario = Session("usuario").ToString()
        Dim tbFx As DataTable = db.ObtieneFrecuenia(usuario)
        If tbFx IsNot Nothing Then
            DDL_fx1.DataSource = tbFx
            DDL_fx1.DataTextField = "IdFrecuencia"
            DDL_fx1.DataValueField = "IdFrecuencia"
            DDL_fx1.DataBind()
            DDL_fx1.Items.Insert(0, New ListItem("", "0"))
            DDL_fx2.DataSource = tbFx
            DDL_fx2.DataTextField = "IdFrecuencia"
            DDL_fx2.DataValueField = "IdFrecuencia"
            DDL_fx2.DataBind()
            DDL_fx2.Items.Insert(0, New ListItem("", "0"))
            DDL_fx3.DataSource = tbFx
            DDL_fx3.DataTextField = "IdFrecuencia"
            DDL_fx3.DataValueField = "IdFrecuencia"
            DDL_fx3.DataBind()
            DDL_fx3.Items.Insert(0, New ListItem("", "0"))
            DDL_fx4.DataSource = tbFx
            DDL_fx4.DataTextField = "IdFrecuencia"
            DDL_fx4.DataValueField = "IdFrecuencia"
            DDL_fx4.DataBind()
            DDL_fx4.Items.Insert(0, New ListItem("", "0"))
            DDL_fx5.DataSource = tbFx
            DDL_fx5.DataTextField = "IdFrecuencia"
            DDL_fx5.DataValueField = "IdFrecuencia"
            DDL_fx5.DataBind()
            DDL_fx5.Items.Insert(0, New ListItem("", "0"))
            DDL_fx6.DataSource = tbFx
            DDL_fx6.DataTextField = "IdFrecuencia"
            DDL_fx6.DataValueField = "IdFrecuencia"
            DDL_fx6.DataBind()
            DDL_fx6.Items.Insert(0, New ListItem("", "0"))
        End If
    End Sub

    Sub llenaEstatus()
        db.Cn1 = cn1
        usuario = Session("usuario").ToString()
        Dim tbE As DataTable = db.ObtieneEstatusProf(usuario)
        If tbE IsNot Nothing Then
            DDL_e1.DataSource = tbE
            DDL_e1.DataTextField = "Codigo"
            DDL_e1.DataValueField = "IdEstatus"
            DDL_e1.DataBind()
            DDL_e1.Items.Insert(0, New ListItem("", "0"))
            DDL_e2.DataSource = tbE
            DDL_e2.DataTextField = "Codigo"
            DDL_e2.DataValueField = "IdEstatus"
            DDL_e2.DataBind()
            DDL_e2.Items.Insert(0, New ListItem("", "0"))
            DDL_e3.DataSource = tbE
            DDL_e3.DataTextField = "Codigo"
            DDL_e3.DataValueField = "IdEstatus"
            DDL_e3.DataBind()
            DDL_e3.Items.Insert(0, New ListItem("", "0"))
            DDL_e4.DataSource = tbE
            DDL_e4.DataTextField = "Codigo"
            DDL_e4.DataValueField = "IdEstatus"
            DDL_e4.DataBind()
            DDL_e4.Items.Insert(0, New ListItem("", "0"))
            DDL_e5.DataSource = tbE
            DDL_e5.DataTextField = "Codigo"
            DDL_e5.DataValueField = "IdEstatus"
            DDL_e5.DataBind()
            DDL_e5.Items.Insert(0, New ListItem("", "0"))
            DDL_e6.DataSource = tbE
            DDL_e6.DataTextField = "Codigo"
            DDL_e6.DataValueField = "IdEstatus"
            DDL_e6.DataBind()
            DDL_e6.Items.Insert(0, New ListItem("", "0"))
        End If
    End Sub

    Sub llenaVIA()
        db.Cn1 = cn1
        usuario = Session("usuario").ToString()
        Dim tbVia As DataTable = db.ObtieneVIA(usuario)
        If tbVia IsNot Nothing Then
            DDL_Via1.DataSource = tbVia
            DDL_Via1.DataTextField = "NomViaAdministracion"
            DDL_Via1.DataValueField = "IdViaAdministracion"
            DDL_Via1.DataBind()
            DDL_Via1.Items.Insert(0, New ListItem("", "0"))
            DDL_Via2.DataSource = tbVia
            DDL_Via2.DataTextField = "NomViaAdministracion"
            DDL_Via2.DataValueField = "IdViaAdministracion"
            DDL_Via2.DataBind()
            DDL_Via2.Items.Insert(0, New ListItem("", "0"))
            DDL_Via3.DataSource = tbVia
            DDL_Via3.DataTextField = "NomViaAdministracion"
            DDL_Via3.DataValueField = "IdViaAdministracion"
            DDL_Via3.DataBind()
            DDL_Via3.Items.Insert(0, New ListItem("", "0"))
            DDL_Via4.DataSource = tbVia
            DDL_Via4.DataTextField = "NomViaAdministracion"
            DDL_Via4.DataValueField = "IdViaAdministracion"
            DDL_Via4.DataBind()
            DDL_Via4.Items.Insert(0, New ListItem("", "0"))
            DDL_Via5.DataSource = tbVia
            DDL_Via5.DataTextField = "NomViaAdministracion"
            DDL_Via5.DataValueField = "IdViaAdministracion"
            DDL_Via5.DataBind()
            DDL_Via5.Items.Insert(0, New ListItem("", "0"))
            DDL_Via6.DataSource = tbVia
            DDL_Via6.DataTextField = "NomViaAdministracion"
            DDL_Via6.DataValueField = "IdViaAdministracion"
            DDL_Via6.DataBind()
            DDL_Via6.Items.Insert(0, New ListItem("", "0"))
        End If
    End Sub

    Protected Sub txt_asi_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_asi.TextChanged
        buscaNHC()
    End Sub

    Protected Sub DDL_cod1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDL_cod1.SelectedIndexChanged
        If DDL_cod1.SelectedValue <> "0" Then
            txt_dx1.Enabled = True
            DDL_Via1.Enabled = True
            DDL_fx1.Enabled = True
            txt_cant1.Enabled = True
            DDL_t1.Enabled = True
            DDL_Trat1.Visible = False
            DDL_e1.Enabled = True
            txt_tdias1.Enabled = True
            txt_obs1.Enabled = True
            buscaMED(DDL_cod1.SelectedValue, 1)
            DDL_t1.SelectedValue = 1
            txt_dx1.Focus()
        Else
            setcampos(1)
            lbl_prof1.Text = ""
            DDL_Via1.SelectedValue = 0
            DDL_fx1.SelectedValue = 0
            DDL_e1.SelectedValue = 0
            DDL_t1.SelectedValue = 0
            DDL_cod1.Focus()
        End If
    End Sub

    Protected Sub DDL_cod2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDL_cod2.SelectedIndexChanged
        If DDL_cod2.SelectedValue <> "0" Then
            txt_dx2.Enabled = True
            DDL_Via2.Enabled = True
            DDL_fx2.Enabled = True
            txt_cant2.Enabled = True
            DDL_t2.Enabled = True
            DDL_Trat2.Visible = False
            DDL_e2.Enabled = True
            txt_tdias2.Enabled = True
            txt_obs2.Enabled = True
            buscaMED(DDL_cod2.SelectedValue, 2)
            DDL_t2.SelectedValue = 1
            txt_dx2.Focus()
        Else
            setcampos(2)
            lbl_prof2.Text = ""
            DDL_Via2.SelectedValue = 0
            DDL_fx2.SelectedValue = 0
            DDL_e2.SelectedValue = 0
            DDL_t2.SelectedValue = 0
            DDL_cod2.Focus()
        End If
    End Sub

    Protected Sub DDL_cod3_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDL_cod3.SelectedIndexChanged
        If DDL_cod3.SelectedValue <> "0" Then
            txt_dx3.Enabled = True
            DDL_Via3.Enabled = True
            DDL_fx3.Enabled = True
            txt_cant3.Enabled = True
            DDL_t3.Enabled = True
            DDL_Trat3.Visible = False
            DDL_e3.Enabled = True
            txt_tdias3.Enabled = True
            txt_obs3.Enabled = True
            buscaMED(DDL_cod3.SelectedValue, 3)
            DDL_t3.SelectedValue = 1
            txt_dx3.Focus()
        Else
            setcampos(3)
            lbl_prof3.Text = ""
            DDL_Via3.SelectedValue = 0
            DDL_fx3.SelectedValue = 0
            DDL_e3.SelectedValue = 0
            DDL_t3.SelectedValue = 0
            DDL_cod3.Focus()
        End If
    End Sub

    Protected Sub DDL_cod4_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDL_cod4.SelectedIndexChanged
        If DDL_cod4.SelectedValue <> "0" Then
            txt_dx4.Enabled = True
            DDL_Via4.Enabled = True
            DDL_fx4.Enabled = True
            txt_cant4.Enabled = True
            DDL_t4.Enabled = True
            DDL_Trat4.Visible = False
            DDL_e4.Enabled = True
            txt_tdias4.Enabled = True
            txt_obs4.Enabled = True
            buscaMED(DDL_cod4.SelectedValue, 4)
            DDL_t4.SelectedValue = 1
            txt_dx4.Focus()
        Else
            setcampos(4)
            lbl_prof4.Text = ""
            DDL_Via4.SelectedValue = 0
            DDL_fx4.SelectedValue = 0
            DDL_e4.SelectedValue = 0
            DDL_t4.SelectedValue = 0
            DDL_cod4.Focus()
        End If
    End Sub

    Protected Sub DDL_cod5_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDL_cod5.SelectedIndexChanged
        If DDL_cod5.SelectedValue <> "0" Then
            txt_dx5.Enabled = True
            DDL_Via5.Enabled = True
            DDL_fx5.Enabled = True
            txt_cant5.Enabled = True
            DDL_t5.Enabled = True
            DDL_Trat5.Visible = False
            DDL_e5.Enabled = True
            txt_tdias5.Enabled = True
            txt_obs5.Enabled = True
            buscaMED(DDL_cod5.SelectedValue, 5)
            DDL_t5.SelectedValue = 1
            txt_dx5.Focus()
        Else
            setcampos(5)
            lbl_prof5.Text = ""
            DDL_Via5.SelectedValue = 0
            DDL_fx5.SelectedValue = 0
            DDL_e5.SelectedValue = 0
            DDL_t5.SelectedValue = 0
            DDL_cod5.Focus()
        End If
    End Sub

    Protected Sub DDL_cod6_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDL_cod6.SelectedIndexChanged
        If DDL_cod6.SelectedValue <> "0" Then
            txt_dx6.Enabled = True
            DDL_Via6.Enabled = True
            DDL_fx6.Enabled = True
            txt_cant6.Enabled = True
            DDL_t6.Enabled = True
            DDL_Trat6.Visible = False
            DDL_e6.Enabled = True
            txt_tdias6.Enabled = True
            txt_obs6.Enabled = True
            buscaMED(DDL_cod6.SelectedValue, 6)
            DDL_t6.SelectedValue = 1
            txt_dx6.Focus()
        Else
            setcampos(6)
            lbl_prof6.Text = ""
            DDL_Via6.SelectedValue = 0
            DDL_fx6.SelectedValue = 0
            DDL_e6.SelectedValue = 0
            DDL_t6.SelectedValue = 0
            DDL_cod6.Focus()
        End If
    End Sub

    Private Sub RegistrarIngreso(ByVal varFechaEntrega As String)

        If Session("nhc").ToString() = String.Empty Or Session("nhc").ToString() = "" Then
            Session("nhc") = txt_asi.Text.ToUpper()
        End If
        usuario = Session("usuario").ToString()
        Dim datos As String = Session("nhc").ToString().ToUpper() + "|" + varFechaEntrega + "|" + RBL_tipopaciente.SelectedValue.ToString() + "|"
        datos += DDL_cod1.SelectedValue.ToString() + "|" + str(txt_cant1.Text.ToString()) + "|" + txt_dx1.Text.ToString() + "|" + DDL_Via1.SelectedValue.ToString() + "|" + DDL_fx1.SelectedValue.ToString() + "|" + str2(DDL_cod1.SelectedValue.ToString(), DDL_t1.SelectedValue.ToString()) + "|" + str3(DDL_t1.SelectedValue.ToString(), DDL_Trat1.SelectedValue.ToString()) + "|" + DDL_e1.SelectedValue.ToString() + "|" + str(txt_tdias1.Text.ToString()) + "|" + txt_obs1.Text.ToString() + "|"
        datos += DDL_cod2.SelectedValue.ToString() + "|" + str(txt_cant2.Text.ToString()) + "|" + txt_dx2.Text.ToString() + "|" + DDL_Via2.SelectedValue.ToString() + "|" + DDL_fx2.SelectedValue.ToString() + "|" + str2(DDL_cod2.SelectedValue.ToString(), DDL_t2.SelectedValue.ToString()) + "|" + str3(DDL_t2.SelectedValue.ToString(), DDL_Trat2.SelectedValue.ToString()) + "|" + DDL_e2.SelectedValue.ToString() + "|" + str(txt_tdias2.Text.ToString()) + "|" + txt_obs2.Text.ToString() + "|"
        datos += DDL_cod3.SelectedValue.ToString() + "|" + str(txt_cant3.Text.ToString()) + "|" + txt_dx3.Text.ToString() + "|" + DDL_Via3.SelectedValue.ToString() + "|" + DDL_fx3.SelectedValue.ToString() + "|" + str2(DDL_cod3.SelectedValue.ToString(), DDL_t3.SelectedValue.ToString()) + "|" + str3(DDL_t3.SelectedValue.ToString(), DDL_Trat3.SelectedValue.ToString()) + "|" + DDL_e3.SelectedValue.ToString() + "|" + str(txt_tdias3.Text.ToString()) + "|" + txt_obs3.Text.ToString() + "|"
        datos += DDL_cod4.SelectedValue.ToString() + "|" + str(txt_cant4.Text.ToString()) + "|" + txt_dx4.Text.ToString() + "|" + DDL_Via4.SelectedValue.ToString() + "|" + DDL_fx4.SelectedValue.ToString() + "|" + str2(DDL_cod4.SelectedValue.ToString(), DDL_t4.SelectedValue.ToString()) + "|" + str3(DDL_t4.SelectedValue.ToString(), DDL_Trat4.SelectedValue.ToString()) + "|" + DDL_e4.SelectedValue.ToString() + "|" + str(txt_tdias4.Text.ToString()) + "|" + txt_obs4.Text.ToString() + "|"
        datos += DDL_cod5.SelectedValue.ToString() + "|" + str(txt_cant5.Text.ToString()) + "|" + txt_dx5.Text.ToString() + "|" + DDL_Via5.SelectedValue.ToString() + "|" + DDL_fx5.SelectedValue.ToString() + "|" + str2(DDL_cod5.SelectedValue.ToString(), DDL_t5.SelectedValue.ToString()) + "|" + str3(DDL_t5.SelectedValue.ToString(), DDL_Trat5.SelectedValue.ToString()) + "|" + DDL_e5.SelectedValue.ToString() + "|" + str(txt_tdias5.Text.ToString()) + "|" + txt_obs5.Text.ToString() + "|"
        datos += DDL_cod6.SelectedValue.ToString() + "|" + str(txt_cant6.Text.ToString()) + "|" + txt_dx6.Text.ToString() + "|" + DDL_Via6.SelectedValue.ToString() + "|" + DDL_fx6.SelectedValue.ToString() + "|" + str2(DDL_cod6.SelectedValue.ToString(), DDL_t6.SelectedValue.ToString()) + "|" + str3(DDL_t6.SelectedValue.ToString(), DDL_Trat6.SelectedValue.ToString()) + "|" + DDL_e6.SelectedValue.ToString() + "|" + str(txt_tdias6.Text.ToString()) + "|" + txt_obs6.Text.ToString() + "|"
        datos += txt_cd4.Text.ToString()
        db.Cn1 = cn1
        db.GrabaControlPROF(datos, usuario)

        '*/ Revisa Código 1 cantidad entrega para existencia
        If DDL_cod1.SelectedValue IsNot Nothing Then
            Dim tipo_ingreso_med As String = 2
            Dim producto As String = DDL_cod1.SelectedValue.ToString()
            'Dim qty_ingreso As String = txt_cantidadARV.Text.ToString()
            Dim qty_salida As String = txt_cant1.Text.ToString()
            Dim tipo_mov As String = 2
            db.Update_Existencia_Egreso(tipo_ingreso_med, varFechaEntrega, producto, 0, qty_salida, tipo_mov, usuario, 0, 0, "01/01/1900")
        End If

        '*/ Revisa Código 2 cantidad entrega para existencia
        If DDL_cod2.SelectedValue IsNot Nothing Then
            Dim tipo_ingreso_med As String = 2
            Dim producto As String = DDL_cod2.SelectedValue.ToString()
            'Dim qty_ingreso As String = txt_cantidadARV.Text.ToString()
            Dim qty_salida As String = txt_cant2.Text.ToString()
            Dim tipo_mov As String = 2
            db.Update_Existencia_Egreso(tipo_ingreso_med, varFechaEntrega, producto, 0, qty_salida, tipo_mov, usuario, 0, 0, "01/01/1900")
        End If
        '*/ Revisa Código 3 cantidad entrega para existencia
        If DDL_cod3.SelectedValue IsNot Nothing Then
            Dim tipo_ingreso_med As String = 2
            Dim producto As String = DDL_cod3.SelectedValue.ToString()
            'Dim qty_ingreso As String = txt_cantidadARV.Text.ToString()
            Dim qty_salida As String = txt_cant3.Text.ToString()
            Dim tipo_mov As String = 2
            db.Update_Existencia_Egreso(tipo_ingreso_med, varFechaEntrega, producto, 0, qty_salida, tipo_mov, usuario, 0, 0, "01/01/1900")
        End If
        '*/ Revisa Código 4 cantidad entrega para existencia
        If DDL_cod4.SelectedValue IsNot Nothing Then
            Dim tipo_ingreso_med As String = 2
            Dim producto As String = DDL_cod4.SelectedValue.ToString()
            'Dim qty_ingreso As String = txt_cantidadARV.Text.ToString()
            Dim qty_salida As String = txt_cant4.Text.ToString()
            Dim tipo_mov As String = 2
            db.Update_Existencia_Egreso(tipo_ingreso_med, varFechaEntrega, producto, 0, qty_salida, tipo_mov, usuario, 0, 0, "01/01/1900")
        End If
        '*/ Revisa Código 5 cantidad entrega para existencia
        If DDL_cod5.SelectedValue IsNot Nothing Then
            Dim tipo_ingreso_med As String = 2
            Dim producto As String = DDL_cod5.SelectedValue.ToString()
            'Dim qty_ingreso As String = txt_cantidadARV.Text.ToString()
            Dim qty_salida As String = txt_cant5.Text.ToString()
            Dim tipo_mov As String = 2
            db.Update_Existencia_Egreso(tipo_ingreso_med, varFechaEntrega, producto, 0, qty_salida, tipo_mov, usuario, 0, 0, "01/01/1900")
        End If
        '*/ Revisa Código 6 cantidad entrega para existencia
        If DDL_cod6.SelectedValue IsNot Nothing Then
            Dim tipo_ingreso_med As String = 2
            Dim producto As String = DDL_cod6.SelectedValue.ToString()
            'Dim qty_ingreso As String = txt_cantidadARV.Text.ToString()
            Dim qty_salida As String = txt_cant6.Text.ToString()
            Dim tipo_mov As String = 2
            db.Update_Existencia_Egreso(tipo_ingreso_med, varFechaEntrega, producto, 0, qty_salida, tipo_mov, usuario, 0, 0, "01/01/1900")
        End If
        Response.Redirect("~/ingresoProf.aspx", False)

    End Sub

    Protected Sub btn_grabar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_grabar.Click

        If Not revisar.RevisaSesion(Session("conexion").ToString(), Session("usuario").ToString()) Then
            Response.Redirect("~/inicio.aspx", False)
        Else
            Dim FechaEntrega As String = Convert.ToString(txt_fe_dd.Text) + "/" + Convert.ToString(txt_fe_mm.Text) + "/" + Convert.ToString(txt_fe_yy.Text)
            Try
                Convert.ToDateTime(FechaEntrega).ToString("dd/MM/yy")

            Catch ex As Exception
                lbl_error.Text = "Fecha Entrega no es correcta, favor verificar"
                txt_fe_dd.Focus()
                Exit Sub
            End Try

            Try
                'jchete 11-01-2021
                db.Cn1 = cn1
                Dim fechaValidaFormato As String = "20" + Convert.ToString(txt_fe_yy.Text) + "/" + Convert.ToString(txt_fe_mm.Text) + "/" + Convert.ToString(txt_fe_dd.Text)
                Dim val As String = db.ValidacionProf(txt_asi.Text.ToUpper(), fechaValidaFormato, Session("usuario").ToString())
                If val.ToString() = "True" Then
                    RegistrarIngreso(FechaEntrega)
                End If
                If val.ToString() = "False" Then
                    lbl_error.Text = "Ya existe un registro, con fecha y estatus identico"

                End If
                'RegistrarIngreso(FechaEntrega)
            Catch ex As Exception
                lbl_error.Text = ex.Message
                Exit Sub
            End Try

        End If
		
    End Sub


    Function str(ByVal x As String) As String
        Dim z As String
        If x = String.Empty Then
            z = "NULL"
        Else
            z = x
        End If
        Return z
    End Function

    Function str2(ByVal x As String, ByVal y As String) As String
        Dim z As String
        If x = String.Empty Then
            z = "NULL"
        Else
            z = y
        End If
        Return z
    End Function

    Function str3(ByVal x As String, ByVal y As String) As String
        Dim z As String
        If x <> "2" Then
            z = "NULL"
        Else
            z = y
        End If
        Return z
    End Function

    Protected Sub btn_buscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_buscar.Click
        buscaNHC()
    End Sub

    Sub buscaNHC()
        If txt_asi.Text.Trim <> String.Empty Then
            llenadatos(txt_asi.Text.ToUpper())
            If existenhc Then
                If ufecha Then
                    divingreso.Visible = True
                    'txt_devcant1.Focus()
                Else
                    divingreso.Visible = True
                    txt_fe_dd.Focus()
                End If
            Else
                lbl_genero.Text = String.Empty
                lbl_nombre.Text = String.Empty
                lbl_telefono.Text = String.Empty
                lbl_nacimiento.Text = String.Empty
                lbl_domicilio.Text = String.Empty
                lbl_estatus.Text = String.Empty
                divingreso.Visible = False
                txt_asi.Focus()
            End If
        Else
            lbl_genero.Text = String.Empty
            lbl_nombre.Text = String.Empty
            lbl_telefono.Text = String.Empty
            lbl_nacimiento.Text = String.Empty
            lbl_domicilio.Text = String.Empty
            lbl_estatus.Text = String.Empty
            divingreso.Visible = False
            txt_asi.Focus()
        End If
    End Sub

    Sub buscaMED(ByVal codigo As String, ByVal med As Integer)
        db.Cn1 = cn1
        Dim x As String = db.ObtieneMED(codigo, usuario)
        Dim rp As String() = x.Split("|")
        If rp(0).ToString() = "True" Then
            Select Case med
                Case 1
                    lbl_prof1.Text = String.Empty
                    lbl_prof1.Text = rp(1).ToString()
                Case 2
                    lbl_prof2.Text = String.Empty
                    lbl_prof2.Text = rp(1).ToString()
                Case 3
                    lbl_prof3.Text = String.Empty
                    lbl_prof3.Text = rp(1).ToString()
                Case 4
                    lbl_prof4.Text = String.Empty
                    lbl_prof4.Text = rp(1).ToString()
                Case 5
                    lbl_prof5.Text = String.Empty
                    lbl_prof5.Text = rp(1).ToString()
                Case 6
                    lbl_prof6.Text = String.Empty
                    lbl_prof6.Text = rp(1).ToString()
            End Select
            lbl_error.Text = String.Empty
            btn_editar.Visible = False
            btn_agregar.Visible = False
        Else
            lbl_error.Text = rp(1)
        End If
    End Sub

    Protected Sub btn_editar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_editar.Click
        Response.Redirect("~/EBPediatrico.aspx?nhc=" + txt_asi.Text.Trim.ToUpper(), False)
    End Sub

    Protected Sub btn_agregar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_agregar.Click
        Response.Redirect("~/NBPediatrico.aspx", False)
    End Sub

    Protected Sub DDL_t1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDL_t1.SelectedIndexChanged
        If DDL_t1.SelectedValue = "2" Then
            DDL_Trat1.Visible = True
            DDL_Trat1.Focus()
        Else
            DDL_Trat1.Visible = False
            DDL_e1.Focus()
        End If
    End Sub

    Protected Sub DDL_t2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDL_t2.SelectedIndexChanged
        If DDL_t2.SelectedValue = "2" Then
            DDL_Trat2.Visible = True
            DDL_Trat2.Focus()
        Else
            DDL_Trat2.Visible = False
            DDL_e2.Focus()
        End If
    End Sub

    Protected Sub DDL_t3_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDL_t3.SelectedIndexChanged
        If DDL_t3.SelectedValue = "2" Then
            DDL_Trat3.Visible = True
            DDL_Trat3.Focus()
        Else
            DDL_Trat3.Visible = False
            DDL_e3.Focus()
        End If
    End Sub

    Protected Sub DDL_t4_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDL_t4.SelectedIndexChanged
        If DDL_t4.SelectedValue = "2" Then
            DDL_Trat4.Visible = True
            DDL_Trat4.Focus()
        Else
            DDL_Trat4.Visible = False
            DDL_e4.Focus()
        End If
    End Sub

    Protected Sub DDL_t5_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDL_t5.SelectedIndexChanged
        If DDL_t5.SelectedValue = "2" Then
            DDL_Trat5.Visible = True
            DDL_Trat5.Focus()
        Else
            DDL_Trat5.Visible = False
            DDL_e5.Focus()
        End If
    End Sub

    Protected Sub DDL_t6_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDL_t6.SelectedIndexChanged
        If DDL_t6.SelectedValue = "2" Then
            DDL_Trat6.Visible = True
            DDL_Trat6.Focus()
        Else
            DDL_Trat6.Visible = False
            DDL_e6.Focus()
        End If
    End Sub

    Protected Sub btn_search_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_search.Click

        buscaNHC()

    End Sub

    Protected Sub btn_end_Click(ByVal sender As Object, ByVal e As System.EventArgs)



    End Sub

    Protected Sub btn_init_Click(ByVal sender As Object, ByVal e As System.EventArgs)



    End Sub

    Protected Sub btn_crear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_crear.Click

        If Not revisar.RevisaSesion(Session("conexion").ToString(), Session("usuario").ToString()) Then
            Response.Redirect("~/inicio.aspx", False)
        Else
            Dim FechaEntrega As String = Convert.ToString(txt_fe_dd.Text) + "/" + Convert.ToString(txt_fe_mm.Text) + "/" + Convert.ToString(txt_fe_yy.Text)
            Try
                Convert.ToDateTime(FechaEntrega).ToString("dd/MM/yy")
            Catch ex As Exception
                lbl_error.Text = "Fecha Entrega no es correcta, favor verificar"
                txt_fe_dd.Focus()
                Exit Sub
            End Try

            Try
                RegistrarIngreso(FechaEntrega)
            Catch ex As Exception
                lbl_error.Text = ex.Message
                Exit Sub
            End Try

        End If

    End Sub

End Class
