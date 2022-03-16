Imports System.Data

Partial Class inicio
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
                ufecha = False
                'txt_asi.Focus()
                setcampos(0)
                llenacodigo()
                llenafrecuencia()
                llenaEstatus()
                llenaEmbarazo()
            End If
        End If
    End Sub

    Sub setcampos(ByVal valor As Integer)
        txt_cant1.Text = ""
        txt_dx1.Text = ""
        txt_uecant1.Text = ""
        txt_cant2.Text = ""
        txt_dx2.Text = ""
        txt_uecant2.Text = ""
        txt_cant3.Text = ""
        txt_dx3.Text = ""
        txt_uecant3.Text = ""
        txt_cant4.Text = ""
        txt_dx4.Text = ""
        txt_uecant4.Text = ""
        Select Case valor
            Case 1
                txt_cant1.Enabled = False
                txt_dx1.Enabled = False
                DDL_fx1.Enabled = False
                txt_uecant1.Enabled = False
                DDL_earv1.Enabled = False
            Case 2
                txt_cant2.Enabled = False
                txt_dx2.Enabled = False
                DDL_fx2.Enabled = False
                txt_uecant2.Enabled = False
                DDL_earv2.Enabled = False
            Case 3
                txt_cant3.Enabled = False
                txt_dx3.Enabled = False
                DDL_fx3.Enabled = False
                txt_uecant3.Enabled = False
                DDL_earv3.Enabled = False
            Case 4
                txt_cant4.Enabled = False
                txt_dx4.Enabled = False
                DDL_fx4.Enabled = False
                txt_uecant4.Enabled = False
                DDL_earv4.Enabled = False
            Case 0
                txt_cant1.Enabled = False
                txt_dx1.Enabled = False
                DDL_fx1.Enabled = False
                txt_uecant1.Enabled = False
                DDL_earv1.Enabled = False
                txt_cant2.Enabled = False
                txt_dx2.Enabled = False
                DDL_fx2.Enabled = False
                txt_uecant2.Enabled = False
                DDL_earv2.Enabled = False
                txt_cant3.Enabled = False
                txt_dx3.Enabled = False
                DDL_fx3.Enabled = False
                txt_uecant3.Enabled = False
                DDL_earv3.Enabled = False
                txt_cant4.Enabled = False
                txt_dx4.Enabled = False
                DDL_fx4.Enabled = False
                txt_uecant4.Enabled = False
                DDL_earv4.Enabled = False
        End Select
    End Sub

    Sub llenadatos(ByVal nhc As String)
        Dim tipo As String
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
                'btn_editar.Visible = False
                'btn_agregar.Visible = False
            Else
                lbl_error.Text = rp(1)
                existenhc = False
                'btn_editar.Visible = False
                'btn_agregar.Visible = False
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
                'btn_editar.Visible = True
                'btn_agregar.Visible = False
            Else
                lbl_error.Text = rpP(1)
                existenhc = False
                'btn_editar.Visible = False
                'btn_agregar.Visible = True
            End If
        End If
        db.Cn1 = cn1
        Dim y As String = db.ObtieneUltimoReg(nhc, usuario)
        Dim rpU As String() = y.Split("|")
        If rpU(0).ToString() = "True" Then
            ufecha = True
            Session("ufecha") = True
            'lbl_ultimafechaentrega.Text = String.Empty
            lbl_devcod1.Text = String.Empty
            lbl_devcod2.Text = String.Empty
            lbl_devcod3.Text = String.Empty
            lbl_devcod4.Text = String.Empty
            idufecha = rpU(1).ToString()
            Session("idufecha") = rpU(1).ToString()
            'lbl_ultimafechaentrega.Text = rpU(2).ToString()
            lbl_devcod1.Text = rpU(3).ToString()
            lbl_devcod2.Text = rpU(4).ToString()
            lbl_devcod3.Text = rpU(5).ToString()
            lbl_devcod4.Text = rpU(6).ToString()
            txt_devcant1.Enabled = dev(rpU(3).ToString())
            txt_devcant2.Enabled = dev(rpU(4).ToString())
            txt_devcant3.Enabled = dev(rpU(5).ToString())
            txt_devcant4.Enabled = dev(rpU(6).ToString())
            txt_adherencia.Enabled = True
        Else
            ufecha = False
            Session("ufecha") = False
            Session("idufecha") = ""
            idufecha = ""
            'lbl_ultimafechaentrega.Text = rpU(1)
            lbl_devcod1.Text = String.Empty
            lbl_devcod2.Text = String.Empty
            lbl_devcod3.Text = String.Empty
            lbl_devcod4.Text = String.Empty
            txt_devcant1.Enabled = False
            txt_devcant2.Enabled = False
            txt_devcant3.Enabled = False
            txt_devcant4.Enabled = False
            txt_adherencia.Enabled = False
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
        Dim tbMed As DataTable = db.ObtieneARVMedicamento("1", usuario)
        If tbMed IsNot Nothing Then
            DDL_cod1.DataSource = tbMed
            DDL_cod1.DataTextField = "Codigo"
            DDL_cod1.DataValueField = "IdFFARV"
            DDL_cod1.DataBind()
            DDL_cod1.Items.Insert(0, New ListItem("", "0"))
            DDL_cod2.DataSource = tbMed
            DDL_cod2.DataTextField = "Codigo"
            DDL_cod2.DataValueField = "IdFFARV"
            DDL_cod2.DataBind()
            DDL_cod2.Items.Insert(0, New ListItem("", "0"))
            DDL_cod3.DataSource = tbMed
            DDL_cod3.DataTextField = "Codigo"
            DDL_cod3.DataValueField = "IdFFARV"
            DDL_cod3.DataBind()
            DDL_cod3.Items.Insert(0, New ListItem("", "0"))
            DDL_cod4.DataSource = tbMed
            DDL_cod4.DataTextField = "Codigo"
            DDL_cod4.DataValueField = "IdFFARV"
            DDL_cod4.DataBind()
            DDL_cod4.Items.Insert(0, New ListItem("", "0"))
        End If
    End Sub

    Sub llenafrecuencia()
        db.Cn1 = cn1
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
        End If
    End Sub

    Sub llenaEstatus()
        db.Cn1 = cn1
        Dim tbE As DataTable = db.ObtieneEstatus(usuario)
        If tbE IsNot Nothing Then
            DDL_earv1.DataSource = tbE
            DDL_earv1.DataTextField = "Codigo"
            DDL_earv1.DataValueField = "IdEstatus"
            DDL_earv1.DataBind()
            DDL_earv1.Items.Insert(0, New ListItem("", "0"))
            DDL_earv2.DataSource = tbE
            DDL_earv2.DataTextField = "Codigo"
            DDL_earv2.DataValueField = "IdEstatus"
            DDL_earv2.DataBind()
            DDL_earv2.Items.Insert(0, New ListItem("", "0"))
            DDL_earv3.DataSource = tbE
            DDL_earv3.DataTextField = "Codigo"
            DDL_earv3.DataValueField = "IdEstatus"
            DDL_earv3.DataBind()
            DDL_earv3.Items.Insert(0, New ListItem("", "0"))
            DDL_earv4.DataSource = tbE
            DDL_earv4.DataTextField = "Codigo"
            DDL_earv4.DataValueField = "IdEstatus"
            DDL_earv4.DataBind()
            DDL_earv4.Items.Insert(0, New ListItem("", "0"))
        End If
    End Sub

    Sub llenaEmbarazo()
        db.Cn1 = cn1
        Dim tbEmb As DataTable = db.ObtieneEmbarazo(usuario)
        If tbEmb IsNot Nothing Then
            DDL_embarazo.DataSource = tbEmb
            DDL_embarazo.DataTextField = "IdEmbarazo"
            DDL_embarazo.DataValueField = "IdEmbarazo"
            DDL_embarazo.DataBind()
            DDL_embarazo.Items.Insert(0, New ListItem("", "0"))
        End If
    End Sub

    'Protected Sub txt_asi_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_asi.TextChanged
    '    buscaNHC()
    'End Sub

    Protected Sub DDL_cod1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDL_cod1.SelectedIndexChanged
        If DDL_cod1.SelectedValue <> "0" Then
            lbl_uecod1.Text = DDL_cod1.SelectedItem.ToString()
            lbl_earvcod1.Text = DDL_cod1.SelectedItem.ToString()
            txt_cant1.Enabled = True
            txt_dx1.Enabled = True
            DDL_fx1.Enabled = True
            txt_uecant1.Enabled = True
            DDL_earv1.Enabled = True
            txt_cant1.Focus()
        Else
            lbl_uecod1.Text = String.Empty
            lbl_earvcod1.Text = String.Empty
            setcampos(1)
            DDL_fx1.SelectedValue = 0
            DDL_earv1.SelectedValue = 0
            DDL_cod1.Focus()
        End If
    End Sub

    Protected Sub DDL_cod2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDL_cod2.SelectedIndexChanged
        If DDL_cod2.SelectedValue <> "0" Then
            lbl_uecod2.Text = DDL_cod2.SelectedItem.ToString()
            lbl_earvcod2.Text = DDL_cod2.SelectedItem.ToString()
            txt_cant2.Enabled = True
            txt_dx2.Enabled = True
            DDL_fx2.Enabled = True
            txt_uecant2.Enabled = True
            DDL_earv2.Enabled = True
            txt_cant2.Focus()
        Else
            lbl_uecod2.Text = String.Empty
            lbl_earvcod2.Text = String.Empty
            setcampos(2)
            DDL_fx2.SelectedValue = 0
            DDL_earv2.SelectedValue = 0
            DDL_cod2.Focus()
        End If
    End Sub

    Protected Sub DDL_cod3_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDL_cod3.SelectedIndexChanged
        If DDL_cod3.SelectedValue <> "0" Then
            lbl_uecod3.Text = DDL_cod3.SelectedItem.ToString()
            lbl_earvcod3.Text = DDL_cod3.SelectedItem.ToString()
            txt_cant3.Enabled = True
            txt_dx3.Enabled = True
            DDL_fx3.Enabled = True
            txt_uecant3.Enabled = True
            DDL_earv3.Enabled = True
            txt_cant3.Focus()
        Else
            lbl_uecod3.Text = String.Empty
            lbl_earvcod3.Text = String.Empty
            setcampos(3)
            DDL_fx3.SelectedValue = 0
            DDL_earv3.SelectedValue = 0
            DDL_cod3.Focus()
        End If
    End Sub

    Protected Sub DDL_cod4_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDL_cod4.SelectedIndexChanged
        If DDL_cod4.SelectedValue <> "0" Then
            lbl_uecod4.Text = DDL_cod4.SelectedItem.ToString()
            lbl_earvcod4.Text = DDL_cod4.SelectedItem.ToString()
            txt_cant4.Enabled = True
            txt_dx4.Enabled = True
            DDL_fx4.Enabled = True
            txt_uecant4.Enabled = True
            DDL_earv4.Enabled = True
            txt_cant4.Focus()
        Else
            lbl_uecod4.Text = String.Empty
            lbl_earvcod4.Text = String.Empty
            setcampos(4)
            DDL_fx4.SelectedValue = 0
            DDL_earv4.SelectedValue = 0
            DDL_cod4.Focus()
        End If
    End Sub

    Protected Sub btn_grabar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_grabar.Click
        'NHC, FechaEntrega, Med1_Codigo, Med1_Cantidad, Med1_Dosis, Med1_Frecuencia, Med1_UExCantidad, Med1_ARVEstatus, Med2_Codigo, Med2_Cantidad, Med2_Dosis, Med2_Frecuencia, Med2_UExCantidad, Med2_ARVEstatus, Med3_Codigo, Med3_Cantidad, Med3_Dosis, Med3_Frecuencia, Med3_UExCantidad, Med3_ARVEstatus, Med4_Codigo, Med4_Cantidad, Med4_Dosis, Med4_Frecuencia, Med4_UExCantidad, Med4_ARVEstatus, FechaRetorno, TiempoTARV, CitaMedica, CitaFarmacia, Embarazo, TiempoRetorno, CD4, CV, Observaciones
        Dim FechaEntrega As String = Convert.ToString(txt_fe_dd.Text) + "/" + Convert.ToString(txt_fe_mm.Text) + "/" + Convert.ToString(txt_fe_yy.Text)
        Dim FechaRetorno As String = Convert.ToString(txt_fr_dd.Text) + "/" + Convert.ToString(txt_fr_mm.Text) + "/" + Convert.ToString(txt_fr_yy.Text)
        Dim CitaMx As String
        If CB_citaMx.Checked Then
            CitaMx = "1"
        Else
            CitaMx = "2"
        End If
        Dim CitaFx As String
        If CB_citaFx.Checked Then
            CitaFx = "1"
        Else
            CitaFx = "2"
        End If
        If Session("nhc").ToString() = String.Empty Or Session("nhc").ToString() = "" Then
            Session("nhc") = lbl_asi.Text.ToUpper()
        End If
        Dim datos As String = Session("nhc").ToString() + "|" + FechaEntrega + "|" + DDL_cod1.SelectedValue.ToString() + "|" + str(txt_cant1.Text.ToString()) + "|" + txt_dx1.Text.ToString() + "|" + DDL_fx1.SelectedValue.ToString() + "|" + str(txt_uecant1.Text.ToString()) + "|" + DDL_earv1.SelectedValue.ToString() + "|" + DDL_cod2.SelectedValue.ToString() + "|" + str(txt_cant2.Text.ToString()) + "|" + txt_dx2.Text.ToString() + "|" + DDL_fx2.SelectedValue.ToString() + "|" + str(txt_uecant2.Text.ToString()) + "|" + DDL_earv2.SelectedValue.ToString() + "|" + DDL_cod3.SelectedValue.ToString() + "|" + str(txt_cant3.Text.ToString()) + "|" + txt_dx3.Text.ToString() + "|" + DDL_fx3.SelectedValue.ToString() + "|" + str(txt_uecant3.Text.ToString()) + "|" + DDL_earv3.SelectedValue.ToString() + "|" + DDL_cod4.SelectedValue.ToString() + "|" + str(txt_cant4.Text.ToString()) + "|" + txt_dx4.Text.ToString() + "|" + DDL_fx4.SelectedValue.ToString() + "|" + str(txt_uecant4.Text.ToString()) + "|" + DDL_earv4.SelectedValue.ToString() + "|" + FechaRetorno + "|" + str(txt_tarvdias.Text.ToString()) + "|" + CitaMx + "|" + CitaFx + "|" + DDL_embarazo.SelectedValue.ToString() + "|" + str(txt_retornodias.Text.ToString()) + "|" + txt_cd4.Text.ToString() + "|" + txt_cv.Text.ToString() + "|" + txt_observaciones.Text.ToString()
        db.Cn1 = cn1
        ufecha = Session("ufecha")
        If ufecha Then
            Dim ufdatos As String = Session("idufecha").ToString() + "|" + str(txt_devcant1.Text.ToString()) + "|" + str(txt_devcant2.Text.ToString()) + "|" + str(txt_devcant3.Text.ToString()) + "|" + str(txt_devcant4.Text.ToString()) + "|" + str(txt_adherencia.Text.ToString())
            db.GrabaUFechaControlARV(ufdatos, usuario)
        End If
        db.GrabaControlARV(datos, usuario)
        Response.Redirect("~/ingresoARV.aspx", False)
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

    'Protected Sub btn_buscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_buscar.Click
    '    buscaNHC()
    'End Sub

    'Sub buscaNHC()
    '    If txt_asi.Text.Trim <> String.Empty Then
    '        llenadatos(txt_asi.Text.ToUpper())
    '        If existenhc Then
    '            If ufecha Then
    '                divingreso.Visible = True
    '                txt_devcant1.Focus()
    '            Else
    '                divingreso.Visible = True
    '                txt_fe_dd.Focus()
    '            End If
    '        Else
    '            lbl_genero.Text = String.Empty
    '            lbl_nombre.Text = String.Empty
    '            lbl_telefono.Text = String.Empty
    '            lbl_nacimiento.Text = String.Empty
    '            lbl_domicilio.Text = String.Empty
    '            lbl_estatus.Text = String.Empty
    '            divingreso.Visible = False
    '            txt_asi.Focus()
    '        End If
    '    Else
    '        lbl_genero.Text = String.Empty
    '        lbl_nombre.Text = String.Empty
    '        lbl_telefono.Text = String.Empty
    '        lbl_nacimiento.Text = String.Empty
    '        lbl_domicilio.Text = String.Empty
    '        lbl_estatus.Text = String.Empty
    '        divingreso.Visible = False
    '        txt_asi.Focus()
    '    End If
    'End Sub

    'Protected Sub btn_editar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_editar.Click
    '    Response.Redirect("~/EBPediatrico.aspx?nhc=" + txt_asi.Text.Trim.ToUpper(), False)
    'End Sub

    'Protected Sub btn_agregar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_agregar.Click
    '    Response.Redirect("~/NBPediatrico.aspx", False)
    'End Sub
End Class
