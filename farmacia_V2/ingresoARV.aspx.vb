Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Security.Policy
Imports System.Threading

Partial Class ingresoARV
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
                llenaesquema()
                'llenacodigo()
                llenafrecuencia()
                llenaEstatus()
                llenaEmbarazo()
                llena_autoadhe()
            End If
        End If
    End Sub

    Sub setcampos(ByVal valor As Integer)
        txt_cant1.Text = 0
        txt_dx1.Text = ""
        txt_uecant1.Text = 0
        txt_cant2.Text = 0
        txt_dx2.Text = ""
        txt_uecant2.Text = 0
        txt_cant3.Text = 0
        txt_dx3.Text = ""
        txt_uecant3.Text = 0
        txt_cant4.Text = 0
        txt_dx4.Text = ""
        txt_uecant4.Text = 0
        txt_cant5.Text = 0
        txt_dx5.Text = ""
        txt_uecant5.Text = 0
        txt_cant6.Text = 0
        txt_dx6.Text = ""
        txt_uecant6.Text = 0
        txt_cant7.Text = 0
        txt_dx7.Text = ""
        txt_uecant7.Text = 0
        txt_cant8.Text = 0
        txt_dx8.Text = ""
        txt_uecant8.Text = 0
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
            Case 5
                txt_cant5.Enabled = False
                txt_dx5.Enabled = False
                DDL_fx5.Enabled = False
                txt_uecant5.Enabled = False
                DDL_earv5.Enabled = False
            Case 6
                txt_cant6.Enabled = False
                txt_dx6.Enabled = False
                DDL_fx6.Enabled = False
                txt_uecant6.Enabled = False
                DDL_earv6.Enabled = False
            Case 7
                txt_cant7.Enabled = False
                txt_dx7.Enabled = False
                DDL_fx7.Enabled = False
                txt_uecant7.Enabled = False
                DDL_earv7.Enabled = False
            Case 8
                txt_cant8.Enabled = False
                txt_dx8.Enabled = False
                DDL_fx8.Enabled = False
                txt_uecant8.Enabled = False
                DDL_earv8.Enabled = False
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
                txt_cant5.Enabled = False
                txt_dx5.Enabled = False
                DDL_fx5.Enabled = False
                txt_uecant5.Enabled = False
                DDL_earv5.Enabled = False
                txt_cant6.Enabled = False
                txt_dx6.Enabled = False
                DDL_fx6.Enabled = False
                txt_uecant6.Enabled = False
                DDL_earv6.Enabled = False
                txt_cant7.Enabled = False
                txt_dx7.Enabled = False
                DDL_fx7.Enabled = False
                txt_uecant7.Enabled = False
                DDL_earv7.Enabled = False
                txt_cant8.Enabled = False
                txt_dx8.Enabled = False
                DDL_fx8.Enabled = False
                txt_uecant8.Enabled = False
                DDL_earv8.Enabled = False
        End Select
    End Sub

    Sub llenadatos(ByVal nhc As String)
        
		  'jchete 18-12-2020
        DDL_esquema.Enabled = True
        DDL_sesquema.Enabled = True
        DDL_esquemaestatus.Enabled = True
        txt_fe_dd.Enabled = True
        txt_fe_mm.Enabled = True
        txt_fe_yy.Enabled = True
        txt_fr_dd.Enabled = True
        txt_fr_mm.Enabled = True
        txt_fr_yy.Enabled = True
        btn_dias_retorno.Enabled = True
        DDL_embarazo.Enabled = True
        DDL_earv1.Enabled = True
        DDL_earv2.Enabled = True
        DDL_earv3.Enabled = True
        DDL_earv4.Enabled = True
        DDL_earv5.Enabled = True
        DDL_earv6.Enabled = True
        DDL_earv7.Enabled = True
        DDL_earv8.Enabled = True
        txt_retornodias.Enabled = True
        txt_devcant1.Enabled = True
        txt_devcant2.Enabled = True
        txt_devcant3.Enabled = True
        txt_devcant4.Enabled = True
        txt_devcant5.Enabled = True
        txt_devcant6.Enabled = True
        txt_devcant7.Enabled = True
        txt_devcant8.Enabled = True
        DDL_cod1.Enabled = True
        DDL_cod2.Enabled = True
        DDL_cod3.Enabled = True
        DDL_cod4.Enabled = True
        DDL_cod5.Enabled = True
        DDL_cod6.Enabled = True
        DDL_cod7.Enabled = True
        DDL_cod8.Enabled = True
        txt_cant1.Enabled = True
        txt_cant2.Enabled = True
        txt_cant3.Enabled = True
        txt_cant4.Enabled = True
        txt_cant5.Enabled = True
        txt_cant6.Enabled = True
        txt_cant7.Enabled = True
        txt_cant8.Enabled = True
        txt_dx1.Enabled = True
        txt_dx2.Enabled = True
        txt_dx3.Enabled = True
        txt_dx4.Enabled = True
        txt_dx5.Enabled = True
        txt_dx6.Enabled = True
        txt_dx7.Enabled = True
        txt_dx8.Enabled = True
        btn_calcular_adherencia.Enabled = True
        btn_validar.Enabled = True
        DDL_fx1.Enabled = True
        DDL_fx2.Enabled = True
        DDL_fx3.Enabled = True
        DDL_fx4.Enabled = True
        DDL_fx5.Enabled = True
        DDL_fx6.Enabled = True
        DDL_fx7.Enabled = True
        DDL_fx8.Enabled = True
        txt_retornodias.Enabled = True
        cbl_paciente_hospitalizado.Enabled = True
        txt_adherencia.Enabled = True
        ddl_auto_adherencia.Enabled = True
        CB_citaFx.Enabled = True
        CB_citaMx.Enabled = True
        txt_tarvdias.Enabled = True
        DDL_embarazo.Enabled = True
        'hasta aqui

        Dim tipo As String
        usuario = Session("usuario").ToString()
        If nhc.Substring(1, 1).ToUpper.ToString() = "P" Then
            tipo = "P"
        Else
            tipo = "A"
        End If
        If tipo = "A" Then
            DDL_cod3.Enabled = False
            DDL_cod4.Enabled = False
            DDL_cod5.Enabled = False
            DDL_cod6.Enabled = False
            DDL_cod7.Enabled = False
            DDL_cod8.Enabled = False
            db.Cn2 = cn2
            db.Cn1 = cn1
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

                DDL_embarazo.SelectedValue = 2

                lbl_nombre.Text = rp(2).ToString()
                lbl_telefono.Text = rp(3).ToString()
                lbl_nacimiento.Text = rp(4).ToString()
                lbl_domicilio.Text = rp(5).ToString()
                lbl_estatus.Text = rp(6).ToString()
                lbl_tyt_pac.Text = rp(7).ToString()
                lbl_error.Text = String.Empty
                btn_editar.Visible = False
                btn_agregar.Visible = False
                lbl_proximacitaTS.Text = rp(8).ToString()
                lbl_proximacitaMangua.Text = rp(9).ToString()

                calculadiasretornototal()
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
                strnhc = nhc.ToUpper()
                Session("nhc") = nhc.ToUpper()
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
                'DDL_cod5.Enabled = True
                DDL_cod6.Enabled = True
                DDL_cod7.Enabled = True
                DDL_cod8.Enabled = True
            Else
                lbl_error.Text = rpP(1)
                existenhc = False
                btn_editar.Visible = False
                btn_agregar.Visible = True
                'DDL_cod5.Enabled = False
                DDL_cod6.Enabled = False
                DDL_cod7.Enabled = False
                DDL_cod8.Enabled = False
            End If


        End If


        db.Cn1 = cn1
        Dim y As String = db.ObtieneUltimoReg(nhc, usuario)
        Dim rpU As String() = y.Split("|")
        If rpU(0).ToString() = "True" Then
            'llenaEstatus()

            ufecha = True
            Session("ufecha") = True
            lbl_ultimafechaentrega.Text = String.Empty
            lbl_estatusfarmacia.Text = String.Empty
            lbl_devcod1.Text = String.Empty
            lbl_devcod2.Text = String.Empty
            lbl_devcod3.Text = String.Empty
            lbl_devcod4.Text = String.Empty
            lbl_devcod5.Text = String.Empty
            lbl_devcod6.Text = String.Empty
            lbl_devcod7.Text = String.Empty
            lbl_devcod8.Text = String.Empty


            idufecha = rpU(1).ToString()
            Session("idufecha") = rpU(1).ToString()
            lbl_ultimafechaentrega.Text = rpU(2).ToString()
            DDL_esquema.SelectedValue = rpU(3).ToString()
            Session("id_subesquema_llenacodigo") = rpU(4).ToString()
            llenasesquema(rpU(3).ToString())
            DDL_sesquema.SelectedValue = rpU(4).ToString()

            If rpU(5).ToString() = 1 Then
                llenaEstatusPacSiContinua() 'jchete 07-01-2021
                ' DDL_esquemaestatus.SelectedValue = rpU(5).ToString()
                'DDL_esquema.Enabled = False
                'DDL_sesquema.Enabled = False
                'DDL_earv1.Enabled = False
                'DDL_earv2.Enabled = False
                'DDL_earv3.Enabled = False
                'DDL_earv4.Enabled = False
                'DDL_earv5.Enabled = False
                'DDL_earv6.Enabled = False
                'DDL_earv7.Enabled = False
                'DDL_earv8.Enabled = False

            End If

            Select Case rpU(5).ToString()
                Case 8
                    llenaEstatusPacXsuspendido()
                Case 9
                    llenaEstatusPacXsuspendido()
                Case 10
                    LlenaEstatusComplementoSuspendido()
                Case 11
                    LlenaEstatusComplementoSuspendido()
                Case 12
                    DDL_esquema.Enabled = False
                    DDL_sesquema.Enabled = False
                    DDL_esquemaestatus.Enabled = False
                    txt_devcant1.Enabled = False
                    txt_devcant2.Enabled = False
                    txt_devcant3.Enabled = False
                    txt_devcant4.Enabled = False
                    txt_devcant5.Enabled = False
                    txt_devcant6.Enabled = False
                    txt_devcant7.Enabled = False
                    txt_devcant8.Enabled = False
                    txt_fe_dd.Enabled = False
                    txt_fe_mm.Enabled = False
                    txt_fe_yy.Enabled = False
                    txt_fr_dd.Enabled = False
                    txt_fr_mm.Enabled = False
                    txt_fr_yy.Enabled = False
                    btn_dias_retorno.Enabled = False
                    btn_validar.Enabled = False
                    DDL_earv1.Enabled = False
                    DDL_earv2.Enabled = False
                    DDL_earv3.Enabled = False
                    DDL_earv4.Enabled = False
                    DDL_earv5.Enabled = False
                    DDL_earv6.Enabled = False
                    DDL_earv7.Enabled = False
                    DDL_earv8.Enabled = False
                    txt_retornodias.Enabled = False

                    DDL_cod1.Enabled = False
                    DDL_cod2.Enabled = False
                    DDL_cod3.Enabled = False
                    DDL_cod4.Enabled = False
                    DDL_cod5.Enabled = False
                    DDL_cod6.Enabled = False
                    DDL_cod7.Enabled = False
                    DDL_cod8.Enabled = False
                    txt_cant1.Enabled = False
                    txt_cant2.Enabled = False
                    txt_cant3.Enabled = False
                    txt_cant4.Enabled = False
                    txt_cant5.Enabled = False
                    txt_cant6.Enabled = False
                    txt_cant7.Enabled = False
                    txt_cant8.Enabled = False
                    txt_dx1.Enabled = False
                    txt_dx2.Enabled = False
                    txt_dx3.Enabled = False
                    txt_dx4.Enabled = False
                    txt_dx5.Enabled = False
                    txt_dx6.Enabled = False
                    txt_dx7.Enabled = False
                    txt_dx8.Enabled = False
                    btn_calcular_adherencia.Enabled = False
                    btn_dias_retorno.Enabled = False
                    btn_validar.Enabled = False
                    DDL_fx1.Enabled = False
                    DDL_fx2.Enabled = False
                    DDL_fx3.Enabled = False
                    DDL_fx4.Enabled = False
                    DDL_fx5.Enabled = False
                    DDL_fx6.Enabled = False
                    DDL_fx7.Enabled = False
                    DDL_fx8.Enabled = False
                    txt_retornodias.Enabled = False
                    cbl_paciente_hospitalizado.Enabled = False
                    txt_adherencia.Enabled = False
                    ddl_auto_adherencia.Enabled = False
                    CB_citaFx.Enabled = False
                    CB_citaMx.Enabled = False
                    txt_tarvdias.Enabled = False
                    DDL_embarazo.Enabled = False
                Case 16
                    LlenaEstatusComplementoSuspendido()
                Case 19
                    llenaEstatusPacXsuspendido()
                Case 21
                    LlenaEstatusComplementoSuspendido()
                Case 23
                    llenaEstatusPacXsuspendido()

            End Select

            Select Case lbl_genero.Text
                Case "Hombre"
                    DDL_embarazo.Enabled = False
                Case "Mujer"
                    DDL_embarazo.Enabled = True
            End Select

            DDL_cod1.SelectedValue = rpU(6).ToString()
            DDL_fx1.SelectedValue = rpU(7).ToString()
            txt_dx1.Text = rpU(31).ToString()
            DDL_earv1.SelectedValue = rpU(32).ToString()
            DDL_cod2.SelectedValue = rpU(8).ToString()
            DDL_fx2.SelectedValue = rpU(9).ToString()
            txt_dx2.Text = rpU(33).ToString()
            DDL_earv2.SelectedValue = rpU(34).ToString()
            DDL_cod3.SelectedValue = rpU(10).ToString()
            DDL_fx3.SelectedValue = rpU(11).ToString()
            txt_dx3.Text = rpU(35).ToString()
            DDL_earv3.SelectedValue = rpU(36).ToString()
            DDL_cod4.SelectedValue = rpU(12).ToString()
            DDL_fx4.SelectedValue = rpU(13).ToString()
            txt_dx4.Text = rpU(37).ToString()
            DDL_earv4.SelectedValue = rpU(38).ToString()
            DDL_cod5.SelectedValue = rpU(14).ToString()
            DDL_fx5.SelectedValue = rpU(15).ToString()
            txt_dx6.Text = rpU(39).ToString()
            DDL_earv6.SelectedValue = rpU(40).ToString()
            DDL_cod6.SelectedValue = rpU(16).ToString()
            DDL_fx6.SelectedValue = rpU(17).ToString()
            txt_dx6.Text = rpU(41).ToString()
            DDL_earv6.SelectedValue = rpU(42).ToString()
            DDL_cod7.SelectedValue = rpU(18).ToString()
            DDL_fx7.SelectedValue = rpU(19).ToString()
            txt_dx7.Text = rpU(43).ToString()
            DDL_earv7.SelectedValue = rpU(44).ToString()
            DDL_cod8.SelectedValue = rpU(20).ToString()
            DDL_fx8.SelectedValue = rpU(21).ToString()
            txt_dx8.Text = rpU(45).ToString()
            DDL_earv8.SelectedValue = rpU(46).ToString()


            lbl_estatusfarmacia.Text = rpU(22).ToString()
            lbl_devcod1.Text = rpU(23).ToString()

            lbl_devcod2.Text = rpU(24).ToString()
            lbl_devcod3.Text = rpU(25).ToString()
            lbl_devcod4.Text = rpU(26).ToString()
            lbl_devcod5.Text = rpU(27).ToString()
            lbl_devcod6.Text = rpU(28).ToString()
            lbl_devcod7.Text = rpU(29).ToString()
            lbl_devcod8.Text = rpU(30).ToString()

            'txt_devcant1.Enabled = dev(rpU(23).ToString())
            'txt_devcant2.Enabled = dev(rpU(24).ToString())
            'txt_devcant3.Enabled = dev(rpU(25).ToString())
            'txt_devcant4.Enabled = dev(rpU(26).ToString())
            'txt_devcant5.Enabled = dev(rpU(27).ToString())
            'txt_devcant6.Enabled = dev(rpU(28).ToString())
            'txt_devcant7.Enabled = dev(rpU(29).ToString())
            'txt_devcant8.Enabled = dev(rpU(30).ToString())
            'txt_retornodias.Enabled = True
            'txt_adherencia.Enabled = True


            'jchete 07/01/2021

            'If DDL_cod1.SelectedValue <> Nothing Then
            '    DDL_cod1.Enabled = False
            '    txt_cant1.Enabled = True
            '    txt_dx1.Enabled = True
            '    DDL_fx1.Enabled = True
            '    'DDL_earv1.Enabled = True
            '    txt_uecant1.Enabled = True

            'End If

            'If DDL_cod1.SelectedValue <> Nothing Then
            '    DDL_cod1.Enabled = False
            '    txt_cant1.Enabled = True
            '    txt_dx1.Enabled = True
            '    DDL_fx1.Enabled = True
            '    ' DDL_earv1.Enabled = True
            '    txt_uecant1.Enabled = True

            'End If

            'If DDL_cod2.SelectedValue <> Nothing Then
            '    DDL_cod2.Enabled = False
            '    txt_cant2.Enabled = True
            '    txt_dx2.Enabled = True
            '    DDL_fx2.Enabled = True
            '    'DDL_earv2.Enabled = True
            '    txt_uecant2.Enabled = True

            'End If

            'If DDL_cod3.SelectedValue <> Nothing Then
            '    DDL_cod3.Enabled = False
            '    txt_cant3.Enabled = True
            '    txt_dx3.Enabled = True
            '    DDL_fx3.Enabled = True
            '    ' DDL_earv3.Enabled = True
            '    txt_uecant3.Enabled = True

            'End If

            'If DDL_cod4.SelectedValue <> Nothing Then
            '    DDL_cod4.Enabled = False
            '    txt_cant4.Enabled = True
            '    txt_dx4.Enabled = True
            '    DDL_fx4.Enabled = True
            '    'DDL_earv4.Enabled = True
            '    txt_uecant4.Enabled = True

            'End If

            'If DDL_cod5.SelectedValue <> Nothing Then
            '    DDL_cod5.Enabled = False
            '    txt_cant5.Enabled = True
            '    txt_dx5.Enabled = True
            '    DDL_fx5.Enabled = True
            '    'DDL_earv5.Enabled = True
            '    txt_uecant5.Enabled = True

            'End If

            'If DDL_cod6.SelectedValue <> Nothing Then
            '    DDL_cod6.Enabled = False
            '    txt_cant6.Enabled = True
            '    txt_dx6.Enabled = True
            '    DDL_fx6.Enabled = True
            '    'DDL_earv6.Enabled = True
            '    txt_uecant6.Enabled = True

            'End If

            'If DDL_cod7.SelectedValue <> Nothing Then
            '    DDL_cod7.Enabled = False
            '    txt_cant7.Enabled = True
            '    txt_dx7.Enabled = True
            '    DDL_fx7.Enabled = True
            '    ' DDL_earv7.Enabled = True
            '    txt_uecant7.Enabled = True

            'End If

            'If DDL_cod8.SelectedValue <> Nothing Then
            '    DDL_cod8.Enabled = False
            '    txt_cant8.Enabled = True
            '    txt_dx8.Enabled = True
            '    DDL_fx8.Enabled = True
            '    ' DDL_earv8.Enabled = True
            '    txt_uecant8.Enabled = True

            'End If
            'Llena las cajas de texto de fecha entrega y fecha retorno
            Dim fechaday As String
            Dim fechamonth As String
            Dim fechayear As String
            fechaday = DateTime.Now.ToString("dd")
            fechamonth = DateTime.Now.Month
            fechayear = DateTime.Now.ToString("yy")
            txt_fe_dd.Text = fechaday
            If fechamonth < 10 Then
                txt_fe_mm.Text = "0" + fechamonth
            Else
                txt_fe_mm.Text = fechamonth
            End If

            txt_fe_yy.Text = fechayear
            txt_fr_yy.Text = fechayear
            txt_fe_dd.Enabled = False
            txt_fe_mm.Enabled = False
            txt_fe_yy.Enabled = False
            'Datos de Laboratorio
            DatosLab(nhc)
        Else
            llenasesquema(0)
            DDL_esquema.ClearSelection()
            DDL_esquemaestatus.ClearSelection()
            llenaEstatusPacNuevo()
            DDL_esquema.Enabled = True
            DDL_sesquema.Enabled = True
            ufecha = False
            Session("ufecha") = False
            Session("idufecha") = ""
            idufecha = ""
            lbl_ultimafechaentrega.Text = rpU(1)
            lbl_estatusfarmacia.Text = "N/A"
            lbl_devcod1.Text = String.Empty
            lbl_devcod2.Text = String.Empty
            lbl_devcod3.Text = String.Empty
            lbl_devcod4.Text = String.Empty
            lbl_devcod5.Text = String.Empty
            lbl_devcod6.Text = String.Empty
            lbl_devcod7.Text = String.Empty
            lbl_devcod8.Text = String.Empty
            txt_devcant1.Enabled = False
            txt_devcant2.Enabled = False
            txt_devcant3.Enabled = False
            txt_devcant4.Enabled = False
            txt_devcant5.Enabled = False
            txt_devcant6.Enabled = False
            txt_devcant7.Enabled = False
            txt_devcant8.Enabled = False
            txt_retornodias.Enabled = False
            txt_adherencia.Enabled = False
        End If
		
    End Sub

    Sub llena_autoadhe()
        db.Cn1 = cn1
        usuario = Session("usuario").ToString()
        Dim tb_autoad As DataTable = db.Auto_adh()
        If tb_autoad IsNot Nothing Then
            ddl_auto_adherencia.DataSource = tb_autoad
            ddl_auto_adherencia.DataTextField = "Nom_autoadherencia"
            ddl_auto_adherencia.DataValueField = "Id_auto_adherencia"
            ddl_auto_adherencia.DataBind()
            'ddl_auto_adherencia.SelectedValue = 23
            ddl_auto_adherencia.Items.Insert(0, New ListItem("", "0"))
        End If
    End Sub


    Function dev(ByVal valor As String) As Boolean
        If valor = String.Empty Then
            Return False
        Else
            Return True
        End If
    End Function

    Sub llenaesquema()
        db.Cn1 = cn1
        usuario = Session("usuario").ToString()
        Dim tbE As DataTable = db.ListaEsquemas(usuario)
        If tbE IsNot Nothing Then
            DDL_esquema.DataSource = tbE
            DDL_esquema.DataTextField = "IdEsquema"
            DDL_esquema.DataValueField = "IdEsquema"
            DDL_esquema.DataBind()
            DDL_esquema.Items.Insert(0, New ListItem("", "0"))
        End If
    End Sub

    Sub llenasesquema(ByVal id As String)
        db.Cn1 = cn1
        usuario = Session("usuario").ToString()
        Dim tbE As DataTable = db.ListaSEsquemas(id, usuario)
        If tbE IsNot Nothing Then
            DDL_sesquema.DataSource = tbE
            DDL_sesquema.DataTextField = "SCodigo"
            DDL_sesquema.DataValueField = "IdSEsquema"
            DDL_sesquema.DataBind()
            
            
			If Session("id_subesquema_llenacodigo") = Nothing Then

                llenacodigo(DDL_esquema.SelectedValue.ToString())
            Else
                llenacodigo(Session("id_subesquema_llenacodigo"))
            End If
			DDL_esquemaestatus.Focus()
			
        Else
            DDL_sesquema.Items.Insert(0, New ListItem("", "0"))
            DDL_esquema.Focus()
        End If
    End Sub

    Sub llenacodigo()
        db.Cn1 = cn1
        usuario = Session("usuario").ToString()
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
            DDL_cod5.DataSource = tbMed
            DDL_cod5.DataTextField = "Codigo"
            DDL_cod5.DataValueField = "IdFFARV"
            DDL_cod5.DataBind()
            DDL_cod5.Items.Insert(0, New ListItem("", "0"))
            DDL_cod6.DataSource = tbMed
            DDL_cod6.DataTextField = "Codigo"
            DDL_cod6.DataValueField = "IdFFARV"
            DDL_cod6.DataBind()
            DDL_cod6.Items.Insert(0, New ListItem("", "0"))
            DDL_cod7.DataSource = tbMed
            DDL_cod7.DataTextField = "Codigo"
            DDL_cod7.DataValueField = "IdFFARV"
            DDL_cod7.DataBind()
            DDL_cod7.Items.Insert(0, New ListItem("", "0"))
            DDL_cod8.DataSource = tbMed
            DDL_cod8.DataTextField = "Codigo"
            DDL_cod8.DataValueField = "IdFFARV"
            DDL_cod8.DataBind()
            DDL_cod8.Items.Insert(0, New ListItem("", "0"))
        End If
    End Sub

    Sub llenacodigo(ByVal id As String)
        db.Cn1 = cn1
        usuario = Session("usuario").ToString()
        'Dim tbMed As DataTable = db.ObtieneARVMedicamentoXid(id, usuario)
        Dim tbMed As DataTable = db.ObtieneARVMedicamentoXidSE(id, usuario)
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
            DDL_cod5.DataSource = tbMed
            DDL_cod5.DataTextField = "Codigo"
            DDL_cod5.DataValueField = "IdFFARV"
            DDL_cod5.DataBind()
            DDL_cod5.Items.Insert(0, New ListItem("", "0"))
            DDL_cod6.DataSource = tbMed
            DDL_cod6.DataTextField = "Codigo"
            DDL_cod6.DataValueField = "IdFFARV"
            DDL_cod6.DataBind()
            DDL_cod6.Items.Insert(0, New ListItem("", "0"))
            DDL_cod7.DataSource = tbMed
            DDL_cod7.DataTextField = "Codigo"
            DDL_cod7.DataValueField = "IdFFARV"
            DDL_cod7.DataBind()
            DDL_cod7.Items.Insert(0, New ListItem("", "0"))
            DDL_cod8.DataSource = tbMed
            DDL_cod8.DataTextField = "Codigo"
            DDL_cod8.DataValueField = "IdFFARV"
            DDL_cod8.DataBind()
            DDL_cod8.Items.Insert(0, New ListItem("", "0"))
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
            DDL_fx7.DataSource = tbFx
            DDL_fx7.DataTextField = "IdFrecuencia"
            DDL_fx7.DataValueField = "IdFrecuencia"
            DDL_fx7.DataBind()
            DDL_fx7.Items.Insert(0, New ListItem("", "0"))
            DDL_fx8.DataSource = tbFx
            DDL_fx8.DataTextField = "IdFrecuencia"
            DDL_fx8.DataValueField = "IdFrecuencia"
            DDL_fx8.DataBind()
            DDL_fx8.Items.Insert(0, New ListItem("", "0"))
        End If
    End Sub

    Sub llenaEstatus()
        db.Cn1 = cn1
        usuario = Session("usuario").ToString()
        Dim tbE As DataTable = db.ObtieneEstatus(usuario)
        If tbE IsNot Nothing Then
            DDL_esquemaestatus.DataSource = tbE
            DDL_esquemaestatus.DataTextField = "Codigo"
            DDL_esquemaestatus.DataValueField = "IdEstatus"
            DDL_esquemaestatus.DataBind()
            DDL_esquemaestatus.SelectedValue = 1
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
            DDL_earv5.DataSource = tbE
            DDL_earv5.DataTextField = "Codigo"
            DDL_earv5.DataValueField = "IdEstatus"
            DDL_earv5.DataBind()
            DDL_earv5.Items.Insert(0, New ListItem("", "0"))
            DDL_earv6.DataSource = tbE
            DDL_earv6.DataTextField = "Codigo"
            DDL_earv6.DataValueField = "IdEstatus"
            DDL_earv6.DataBind()
            DDL_earv6.Items.Insert(0, New ListItem("", "0"))
            DDL_earv7.DataSource = tbE
            DDL_earv7.DataTextField = "Codigo"
            DDL_earv7.DataValueField = "IdEstatus"
            DDL_earv7.DataBind()
            DDL_earv7.Items.Insert(0, New ListItem("", "0"))
            DDL_earv8.DataSource = tbE
            DDL_earv8.DataTextField = "Codigo"
            DDL_earv8.DataValueField = "IdEstatus"
            DDL_earv8.DataBind()
            DDL_earv8.Items.Insert(0, New ListItem("", "0"))
        End If
    End Sub

    Sub llenaEmbarazo()
        db.Cn1 = cn1
        usuario = Session("usuario").ToString()
        Dim tbEmb As DataTable = db.ObtieneEmbarazo(usuario)
        If tbEmb IsNot Nothing Then
            DDL_embarazo.DataSource = tbEmb
            DDL_embarazo.DataTextField = "IdEmbarazo"
            DDL_embarazo.DataValueField = "IdEmbarazo"
            DDL_embarazo.DataBind()
            'DDL_embarazo.Items.Insert(0, New ListItem("", "0"))
        End If
    End Sub

    'Protected Sub txt_asi_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_asi.TextChanged
    '    buscaNHC()
    'End Sub

    Protected Sub DDL_cod1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDL_cod1.SelectedIndexChanged
        If DDL_cod1.SelectedValue <> "0" Then
            txt_cant1.Enabled = True
            txt_dx1.Enabled = True
            DDL_fx1.Enabled = True
            txt_uecant1.Enabled = True
            DDL_earv1.Enabled = True
            txt_cant1.Focus()
        Else
            setcampos(1)
            DDL_fx1.SelectedValue = 0
            DDL_earv1.SelectedValue = 0
            DDL_cod1.Focus()
        End If
    End Sub

    Protected Sub DDL_cod2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDL_cod2.SelectedIndexChanged
        If DDL_cod2.SelectedValue <> "0" Then
            txt_cant2.Enabled = True
            txt_dx2.Enabled = True
            DDL_fx2.Enabled = True
            txt_uecant2.Enabled = True
            DDL_earv2.Enabled = True
            txt_cant2.Focus()
        Else
            setcampos(2)
            DDL_fx2.SelectedValue = 0
            DDL_earv2.SelectedValue = 0
            DDL_cod2.Focus()
        End If
    End Sub

    Protected Sub DDL_cod3_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDL_cod3.SelectedIndexChanged
        If DDL_cod3.SelectedValue <> "0" Then
            txt_cant3.Enabled = True
            txt_dx3.Enabled = True
            DDL_fx3.Enabled = True
            txt_uecant3.Enabled = True
            DDL_earv3.Enabled = True
            txt_cant3.Focus()
        Else
            setcampos(3)
            DDL_fx3.SelectedValue = 0
            DDL_earv3.SelectedValue = 0
            DDL_cod3.Focus()
        End If
    End Sub

    Protected Sub DDL_cod4_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDL_cod4.SelectedIndexChanged
        If DDL_cod4.SelectedValue <> "0" Then
            txt_cant4.Enabled = True
            txt_dx4.Enabled = True
            DDL_fx4.Enabled = True
            txt_uecant4.Enabled = True
            DDL_earv4.Enabled = True
            txt_cant4.Focus()
        Else
            setcampos(4)
            DDL_fx4.SelectedValue = 0
            DDL_earv4.SelectedValue = 0
            DDL_cod4.Focus()
        End If
    End Sub

    Protected Sub DDL_cod5_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDL_cod5.SelectedIndexChanged
        If DDL_cod5.SelectedValue <> "0" Then
            txt_cant5.Enabled = True
            txt_dx5.Enabled = True
            DDL_fx5.Enabled = True
            txt_uecant5.Enabled = True
            DDL_earv5.Enabled = True
            txt_cant5.Focus()
        Else
            setcampos(5)
            DDL_fx5.SelectedValue = 0
            DDL_earv5.SelectedValue = 0
            DDL_cod5.Focus()
        End If
    End Sub

    Protected Sub DDL_cod6_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDL_cod6.SelectedIndexChanged
        If DDL_cod6.SelectedValue <> "0" Then
            txt_cant6.Enabled = True
            txt_dx6.Enabled = True
            DDL_fx6.Enabled = True
            txt_uecant6.Enabled = True
            DDL_earv6.Enabled = True
            txt_cant6.Focus()
        Else
            setcampos(6)
            DDL_fx6.SelectedValue = 0
            DDL_earv6.SelectedValue = 0
            DDL_cod6.Focus()
        End If
    End Sub

    Protected Sub DDL_cod7_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDL_cod7.SelectedIndexChanged
        If DDL_cod7.SelectedValue <> "0" Then
            txt_cant7.Enabled = True
            txt_dx7.Enabled = True
            DDL_fx7.Enabled = True
            txt_uecant7.Enabled = True
            DDL_earv7.Enabled = True
            txt_cant7.Focus()
        Else
            setcampos(7)
            DDL_fx7.SelectedValue = 0
            DDL_earv7.SelectedValue = 0
            DDL_cod7.Focus()
        End If
    End Sub

    Protected Sub DDL_cod8_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDL_cod8.SelectedIndexChanged
        If DDL_cod8.SelectedValue <> "0" Then
            txt_cant8.Enabled = True
            txt_dx8.Enabled = True
            DDL_fx8.Enabled = True
            txt_uecant8.Enabled = True
            DDL_earv8.Enabled = True
            txt_cant8.Focus()
        Else
            setcampos(8)
            DDL_fx8.SelectedValue = 0
            DDL_earv8.SelectedValue = 0
            DDL_cod8.Focus()
        End If
    End Sub

    Private Sub ValidarIngreso()


		Dim FechaEntrega As String = Convert.ToString(txt_fe_dd.Text) + "/" + Convert.ToString(txt_fe_mm.Text) + "/" + Convert.ToString(txt_fe_yy.Text)
     
		Dim FechaRetorno As String = Convert.ToString(txt_fr_dd.Text) + "/" + Convert.ToString(txt_fr_mm.Text) + "/" + Convert.ToString(txt_fr_yy.Text)
        Dim vfecha As String
        Dim otrafecha As String
        Try
            Convert.ToDateTime(FechaEntrega).ToString("dd/MM/yy")
            vfecha = Convert.ToDateTime(FechaEntrega).ToString("dd/MM/yy")
            otrafecha = Convert.ToDateTime(System.DateTime.Now).ToString("dd/MM/yy")

            If vfecha > otrafecha Then
                lbl_error.Text = "Fecha Entrega no es correcta, favor verificar"
                txt_fe_dd.Focus()
            End If
        Catch ex As Exception

            Dim argEx As System.ArgumentException = New System.ArgumentException("Fecha Entrega no es correcta, favor verificar")
            Throw argEx
            'lbl_error.Text = "Fecha Entrega no es correcta, favor verificar"
            'txt_fe_dd.Focus()                
        End Try

        Try
            Convert.ToDateTime(FechaRetorno).ToString("dd/MM/yy")
        Catch ex As Exception

            Dim argEx As System.ArgumentException = New System.ArgumentException("Fecha Retorno no es correcta, favor verificar")
            Throw argEx

        End Try
        If Not revisafechas(Convert.ToDateTime(FechaEntrega).ToString("dd/MM/yy"), Convert.ToDateTime(FechaRetorno).ToString("dd/MM/yy")) Then

            Dim argEx As System.ArgumentException = New System.ArgumentException("Favor de revisar las fechas")
            Throw argEx
            'lbl_error.Text = "Favor de revisar las fechas"
            'txt_fe_dd.Focus()
            'Exit Sub
        End If

    End Sub

    Protected Sub btn_validar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_validar.Click

        If DDL_esquema.SelectedValue = 0 Then
            lbl_error.Text = "Debe asignar un Esquema!"
            DDL_esquema.Focus()
            Return

        End If

        Try
            ValidarIngreso()
        Catch ex As Exception
            lbl_error.Text = ex.Message
            btn_grabar.Visible = False
            Return

            btn_validar.Visible = False

        End Try

        btn_grabar.Visible = True
        lbl_error.Text = ""




    End Sub


    Private Sub RegistrarIngreso()

        If DDL_esquema.SelectedValue = 0 Then
            lbl_error.Text = "Debe asignar un Esquema!"
            DDL_esquema.Focus()
            Return
        End If

        Try
            ValidarIngreso()
        Catch ex As Exception
            Dim argEx As System.ArgumentException = New System.ArgumentException(ex.Message)
            Throw argEx
        End Try

        Dim FechaEntrega As String = Convert.ToString(txt_fe_dd.Text) + "/" + Convert.ToString(txt_fe_mm.Text) + "/" + Convert.ToString(txt_fe_yy.Text)
        Dim FechaRetorno As String = Convert.ToString(txt_fr_dd.Text) + "/" + Convert.ToString(txt_fr_mm.Text) + "/" + Convert.ToString(txt_fr_yy.Text)

        Dim fechaprueba As String = Format(CDate(FechaEntrega), "MM/dd/yyyy")
        Dim fechaprueba2 As String = Format(CDate(FechaRetorno), "MM/dd/yyyy")

        ''Validacion si existe registro de ARV con igual esquemastatus, fecha y nhc
        db.Cn1 = cn1
        'jchete 11-01-2021
        Dim fechaValidaFormato As String = "20" + Convert.ToString(txt_fe_yy.Text) + "/" + Convert.ToString(txt_fe_mm.Text) + "/" + Convert.ToString(txt_fe_dd.Text)
        Dim val As String = db.ValidacionARV(txt_asi.Text.ToUpper(), fechaValidaFormato, DDL_esquemaestatus.SelectedValue.ToString(), Session("usuario").ToString())

        If val.ToString() = "True" Then


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
                Session("nhc") = txt_asi.Text.ToUpper()
            End If
            usuario = Session("usuario").ToString()
            Dim datos As String = Session("nhc").ToString() + "|" + FechaEntrega + "|" + DDL_esquema.SelectedValue.ToString() + "|" + DDL_sesquema.SelectedValue.ToString() + "|" + DDL_esquemaestatus.SelectedValue.ToString() + "|"
            datos += DDL_cod1.SelectedValue.ToString() + "|" + str(txt_cant1.Text.ToString()) + "|" + txt_dx1.Text.ToString() + "|" + DDL_fx1.SelectedValue.ToString() + "|" + str(txt_uecant1.Text.ToString()) + "|" + DDL_earv1.SelectedValue.ToString() + "|"
            datos += DDL_cod2.SelectedValue.ToString() + "|" + str(txt_cant2.Text.ToString()) + "|" + txt_dx2.Text.ToString() + "|" + DDL_fx2.SelectedValue.ToString() + "|" + str(txt_uecant2.Text.ToString()) + "|" + DDL_earv2.SelectedValue.ToString() + "|"
            datos += DDL_cod3.SelectedValue.ToString() + "|" + str(txt_cant3.Text.ToString()) + "|" + txt_dx3.Text.ToString() + "|" + DDL_fx3.SelectedValue.ToString() + "|" + str(txt_uecant3.Text.ToString()) + "|" + DDL_earv3.SelectedValue.ToString() + "|"
            datos += DDL_cod4.SelectedValue.ToString() + "|" + str(txt_cant4.Text.ToString()) + "|" + txt_dx4.Text.ToString() + "|" + DDL_fx4.SelectedValue.ToString() + "|" + str(txt_uecant4.Text.ToString()) + "|" + DDL_earv4.SelectedValue.ToString() + "|"
            datos += DDL_cod5.SelectedValue.ToString() + "|" + str(txt_cant5.Text.ToString()) + "|" + txt_dx5.Text.ToString() + "|" + DDL_fx5.SelectedValue.ToString() + "|" + str(txt_uecant5.Text.ToString()) + "|" + DDL_earv5.SelectedValue.ToString() + "|"
            datos += DDL_cod6.SelectedValue.ToString() + "|" + str(txt_cant6.Text.ToString()) + "|" + txt_dx6.Text.ToString() + "|" + DDL_fx6.SelectedValue.ToString() + "|" + str(txt_uecant6.Text.ToString()) + "|" + DDL_earv6.SelectedValue.ToString() + "|"
            datos += DDL_cod7.SelectedValue.ToString() + "|" + str(txt_cant7.Text.ToString()) + "|" + txt_dx7.Text.ToString() + "|" + DDL_fx7.SelectedValue.ToString() + "|" + str(txt_uecant7.Text.ToString()) + "|" + DDL_earv7.SelectedValue.ToString() + "|"
            datos += DDL_cod8.SelectedValue.ToString() + "|" + str(txt_cant8.Text.ToString()) + "|" + txt_dx8.Text.ToString() + "|" + DDL_fx8.SelectedValue.ToString() + "|" + str(txt_uecant8.Text.ToString()) + "|" + DDL_earv8.SelectedValue.ToString() + "|"
            datos += FechaRetorno + "|" + str(txt_tarvdias.Text.ToString()) + "|" + CitaMx + "|" + CitaFx + "|" + DDL_embarazo.SelectedValue.ToString() + "|" + txt_cd4.Text.ToString() + "|" + txt_cv.Text.ToString() + "|" + txt_observaciones.Text.ToString()

            ufecha = Session("ufecha")
            Dim pac_hosp As String
            If cbl_paciente_hospitalizado.Checked = True Then
                pac_hosp = 1
            Else
                pac_hosp = 0
            End If

			Dim envio_med As String
            If cbl_enviomed.Checked = True Then
                envio_med = 1
            Else
                envio_med = 0
            End If
			
            If ufecha Then
                Dim ufdatos As String = Session("idufecha").ToString() + "|" + str(txt_devcant1.Text.ToString()) + "|" + str(txt_devcant2.Text.ToString()) + "|" + str(txt_devcant3.Text.ToString()) + "|" + str(txt_devcant4.Text.ToString()) + "|"
                ufdatos += str(txt_devcant5.Text.ToString()) + "|" + str(txt_devcant6.Text.ToString()) + "|" + str(txt_devcant7.Text.ToString()) + "|" + str(txt_devcant8.Text.ToString()) + "|" + str(txt_adherencia.Text.ToString()) + "|" + str(txt_retornodias.Text.ToString() + "|" + ddl_auto_adherencia.SelectedValue.ToString()) + "|" + pac_hosp  + "|" + envio_med
                db.GrabaUFechaControlARV(ufdatos, usuario)
            End If
            '*/ Revisa Código 1 cantidad entrega para existencia
            If DDL_cod1.SelectedValue IsNot Nothing Then
                Dim tipo_ingreso_med As String = 1
                Dim producto As String = DDL_cod1.SelectedValue.ToString()
                'Dim qty_ingreso As String = txt_cantidadARV.Text.ToString()
                Dim qty_salida As String = txt_cant1.Text.ToString()
                Dim tipo_mov As String = 2
                db.Update_Existencia_Egreso(tipo_ingreso_med, FechaEntrega, producto, 0, qty_salida, tipo_mov, usuario, 0, 0, "01/01/1900")
            End If
            '*/ Revisa Código 2 cantidad entrega para existencia
            If DDL_cod2.SelectedValue IsNot Nothing Then
                Dim tipo_ingreso_med As String = 1
                Dim producto As String = DDL_cod2.SelectedValue.ToString()
                'Dim qty_ingreso As String = txt_cantidadARV.Text.ToString()
                Dim qty_salida As String = txt_cant2.Text.ToString()
                Dim tipo_mov As String = 2
                db.Update_Existencia_Egreso(tipo_ingreso_med, FechaEntrega, producto, 0, qty_salida, tipo_mov, usuario, 0, 0, "01/01/1900")
            End If
            '*/ Revisa Código 3 cantidad entrega para existencia
            If DDL_cod3.SelectedValue IsNot Nothing Then
                Dim tipo_ingreso_med As String = 1
                Dim producto As String = DDL_cod3.SelectedValue.ToString()
                'Dim qty_ingreso As String = txt_cantidadARV.Text.ToString()
                Dim qty_salida As String = txt_cant3.Text.ToString()
                Dim tipo_mov As String = 2
                db.Update_Existencia_Egreso(tipo_ingreso_med, FechaEntrega, producto, 0, qty_salida, tipo_mov, usuario, 0, 0, "01/01/1900")
            End If
            '*/ Revisa Código 4 cantidad entrega para existencia
            If DDL_cod4.SelectedValue IsNot Nothing Then
                Dim tipo_ingreso_med As String = 1
                Dim producto As String = DDL_cod4.SelectedValue.ToString()
                'Dim qty_ingreso As String = txt_cantidadARV.Text.ToString()
                Dim qty_salida As String = txt_cant4.Text.ToString()
                Dim tipo_mov As String = 2
                db.Update_Existencia_Egreso(tipo_ingreso_med, FechaEntrega, producto, 0, qty_salida, tipo_mov, usuario, 0, 0, "01/01/1900")
            End If
            '*/ Revisa Código 5 cantidad entrega para existencia
            If DDL_cod5.SelectedValue IsNot Nothing Then
                Dim tipo_ingreso_med As String = 1
                Dim producto As String = DDL_cod5.SelectedValue.ToString()
                'Dim qty_ingreso As String = txt_cantidadARV.Text.ToString()
                Dim qty_salida As String = txt_cant5.Text.ToString()
                Dim tipo_mov As String = 2
                db.Update_Existencia_Egreso(tipo_ingreso_med, FechaEntrega, producto, 0, qty_salida, tipo_mov, usuario, 0, 0, "01/01/1900")
            End If
            '*/ Revisa Código 6 cantidad entrega para existencia
            If DDL_cod6.SelectedValue IsNot Nothing Then
                Dim tipo_ingreso_med As String = 1
                Dim producto As String = DDL_cod6.SelectedValue.ToString()
                'Dim qty_ingreso As String = txt_cantidadARV.Text.ToString()
                Dim qty_salida As String = txt_cant6.Text.ToString()
                Dim tipo_mov As String = 2
                db.Update_Existencia_Egreso(tipo_ingreso_med, FechaEntrega, producto, 0, qty_salida, tipo_mov, usuario, 0, 0, "01/01/1900")
            End If
            '*/ Revisa Código 7 cantidad entrega para existencia
            If DDL_cod7.SelectedValue IsNot Nothing Then
                Dim tipo_ingreso_med As String = 1
                Dim producto As String = DDL_cod7.SelectedValue.ToString()
                'Dim qty_ingreso As String = txt_cantidadARV.Text.ToString()
                Dim qty_salida As String = txt_cant7.Text.ToString()
                Dim tipo_mov As String = 2
                db.Update_Existencia_Egreso(tipo_ingreso_med, FechaEntrega, producto, 0, qty_salida, tipo_mov, usuario, 0, 0, "01/01/1900")
            End If
            '*/ Revisa Código 8 cantidad entrega para existencia
            If DDL_cod8.SelectedValue IsNot Nothing Then
                Dim tipo_ingreso_med As String = 1
                Dim producto As String = DDL_cod8.SelectedValue.ToString()
                'Dim qty_ingreso As String = txt_cantidadARV.Text.ToString()
                Dim qty_salida As String = txt_cant8.Text.ToString()
                Dim tipo_mov As String = 2
                db.Update_Existencia_Egreso(tipo_ingreso_med, FechaEntrega, producto, 0, qty_salida, tipo_mov, usuario, 0, 0, "01/01/1900")
            End If
            'Procedimiento para alamcenar datos arv
            'Se relaciona sqlConnection con variable conn string

            db.GrabaControlARV(datos, usuario)
            Response.Redirect("~/ingresoARV.aspx", False)

            'Else
            '          lbl_error.Text = "Ya existe un registro, con fecha y estatus identico"
        End If
        'jchete 11-01-2021
        If val.ToString() = "False" Then
            lbl_error.Text = "Ya existe un registro, con fecha y estatus identico"
        End If
		
    End Sub

    Protected Sub btn_grabar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_grabar.Click

        If Not revisar.RevisaSesion(Session("conexion").ToString(), Session("usuario").ToString()) Then
            Response.Redirect("~/inicio.aspx", False)
        Else
            Dim responseError As String = ""
            Try
                ValidarIngreso()
            Catch ex As Exception
                lbl_error.Text = ex.Message
                txt_fe_dd.Focus()
                Return
            End Try

            Try
                RegistrarIngreso()

            Catch ex As Exception
                lbl_error.Text = ex.Message
                DDL_esquema.Focus()
                Return
            End Try

            lbl_error.Text = ""
        End If
    End Sub

    Protected Sub btn_crear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_crear.Click

        If Not revisar.RevisaSesion(Session("conexion").ToString(), Session("usuario").ToString()) Then
            Response.Redirect("~/inicio.aspx", False)
        Else
            Dim responseError As String = ""
            Try
                ValidarIngreso()
            Catch ex As Exception
                lbl_error.Text = ex.Message
                txt_fe_dd.Focus()
                Return

            End Try

            Try
                RegistrarIngreso()

            Catch ex As Exception
                lbl_error.Text = ex.Message
                DDL_esquema.Focus()
                Return

            End Try

        End If

    End Sub




    Function revisafechas(ByVal fe As DateTime, ByVal fr As DateTime) As Boolean
        Dim hoy As DateTime = Convert.ToDateTime(System.DateTime.Now).ToString("dd/MM/yy")
        If fe > hoy Then
            Return False
        Else
            If fe <= fr Then
                Return True
            Else
                Return False
            End If
        End If
    End Function

    'Function armafecha(ByVal dd As String, ByVal mm As String, ByVal aa As String) As String
    '    Dim fecha As String
    '    Dim d, m, a As String
    '    If dd.ToString() <> String.Empty Then

    '    End If
    '    Convert.ToString(txt_fe_dd.Text) + "/" + Convert.ToString(txt_fe_mm.Text) + "/" + Convert.ToString(txt_fe_yy.Text)
    '    Return fecha
    'End Function

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

    Sub buscaNHC()
        If txt_asi.Text.ToUpper().Trim <> String.Empty Then
            llenadatos(txt_asi.Text.ToUpper())
            If existenhc Then
                If ufecha Then
                    divingreso.Visible = True
                    txt_retornodias.Focus()
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

    Protected Sub btn_editar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_editar.Click
        Response.Redirect("~/EBPediatrico.aspx?nhc=" + txt_asi.Text.Trim, False)
    End Sub

    Protected Sub btn_agregar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_agregar.Click
        Response.Redirect("~/NBPediatrico.aspx", False)
    End Sub

    Protected Sub DDL_esquema_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDL_esquema.SelectedIndexChanged
        If DDL_esquema.SelectedValue <> "0" Then

            txt_cant1.Text = Nothing
            txt_dx1.Text = Nothing
            txt_uecant1.Text = Nothing
            DDL_fx1.SelectedValue = Nothing
            DDL_earv1.SelectedValue = Nothing
            txt_cant2.Text = Nothing
            txt_dx2.Text = Nothing
            txt_uecant2.Text = Nothing
            DDL_fx2.SelectedValue = Nothing
            DDL_earv2.SelectedValue = Nothing
            txt_cant3.Text = Nothing
            txt_dx3.Text = Nothing
            txt_uecant3.Text = Nothing
            DDL_fx3.SelectedValue = Nothing
            DDL_earv3.SelectedValue = Nothing
            txt_cant4.Text = Nothing
            txt_dx4.Text = Nothing
            txt_uecant4.Text = Nothing
            DDL_fx4.SelectedValue = Nothing
            DDL_earv4.SelectedValue = Nothing
            txt_cant5.Text = Nothing
            txt_dx5.Text = Nothing
            txt_uecant5.Text = Nothing
            DDL_fx5.SelectedValue = Nothing
            DDL_earv5.SelectedValue = Nothing
            txt_cant6.Text = Nothing
            txt_dx6.Text = Nothing
            txt_uecant6.Text = Nothing
            DDL_fx6.SelectedValue = Nothing
            DDL_earv6.SelectedValue = Nothing
            txt_cant7.Text = Nothing
            txt_dx7.Text = Nothing
            txt_uecant7.Text = Nothing
            DDL_fx7.SelectedValue = Nothing
            DDL_earv7.SelectedValue = Nothing
            txt_cant8.Text = Nothing
            txt_dx8.Text = Nothing
            txt_uecant8.Text = Nothing
            DDL_fx8.SelectedValue = Nothing
            DDL_earv8.SelectedValue = Nothing

            db.Cn1 = cn1
            usuario = Session("usuario").ToString()
            lbl_esquema.Text = db.ObtieneEsquema(DDL_esquema.SelectedValue.ToString(), usuario)
            llenasesquema(DDL_esquema.SelectedValue.ToString())
            DDL_sesquema.Focus()
        Else
            lbl_esquema.Text = String.Empty
            DDL_esquema.Focus()
        End If
        'llena medicamentos cuando solo hay sub-esquma
        Dim Med As Integer = CStr(DDL_sesquema.Items.Count)
        If Med = 1 Then
			 llenacodigo(DDL_sesquema.SelectedValue.ToString())
            LlenaMedicamentos()
        End If
        If Med > 1 Then
		   llenacodigo(DDL_sesquema.SelectedValue.ToString())
            LlenaMedicamentos()
        End If
    End Sub

    Protected Sub DDL_esquemaestatus_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDL_esquemaestatus.SelectedIndexChanged
        If DDL_esquemaestatus.SelectedValue <> "0" Then
            DDL_cod1.Focus()
        Else
            DDL_esquemaestatus.Focus()
        End If

        If DDL_esquemaestatus.SelectedValue = 6 Then
            txt_fe_dd.Text = ""
            txt_fe_mm.Text = ""
            txt_fe_dd.Enabled = True
            txt_fe_mm.Enabled = True
        ElseIf DDL_esquemaestatus.SelectedValue = 7 Then
            txt_fe_dd.Text = ""
            txt_fe_mm.Text = ""
            txt_fe_dd.Enabled = True
            txt_fe_mm.Enabled = True
        ElseIf DDL_esquemaestatus.SelectedValue = 12 Then
            txt_fe_dd.Text = ""
            txt_fe_mm.Text = ""
            txt_fe_dd.Enabled = True
            txt_fe_mm.Enabled = True
        Else
            txt_fe_dd.Text = DateTime.Now.ToString("dd")
            txt_fe_mm.Text = DateTime.Now.ToString("MM")
            txt_fe_dd.Enabled = False
            txt_fe_mm.Enabled = False

        End If
    End Sub

    Protected Sub btn_buscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_buscar.Click
        DDL_cod1.SelectedValue = Nothing
        DDL_cod2.SelectedValue = Nothing
        DDL_cod3.SelectedValue = Nothing
        DDL_cod4.SelectedValue = Nothing
        DDL_cod5.SelectedValue = Nothing
        DDL_cod6.SelectedValue = Nothing
        DDL_cod7.SelectedValue = Nothing
        DDL_cod8.SelectedValue = Nothing
        DDL_fx1.SelectedValue = Nothing
        DDL_fx2.SelectedValue = Nothing
        DDL_fx3.SelectedValue = Nothing
        DDL_fx4.SelectedValue = Nothing
        DDL_fx5.SelectedValue = Nothing
        DDL_fx6.SelectedValue = Nothing
        DDL_fx7.SelectedValue = Nothing
        DDL_fx8.SelectedValue = Nothing
        DDL_earv1.SelectedValue = Nothing
        DDL_earv2.SelectedValue = Nothing
        DDL_earv3.SelectedValue = Nothing
        DDL_earv4.SelectedValue = Nothing
        DDL_earv5.SelectedValue = Nothing
        DDL_earv6.SelectedValue = Nothing
        DDL_earv7.SelectedValue = Nothing
        DDL_earv8.SelectedValue = Nothing

        txt_devcant1.Text = Nothing
        txt_devcant2.Text = Nothing
        txt_devcant3.Text = Nothing
        txt_devcant4.Text = Nothing
        txt_devcant5.Text = Nothing
        txt_devcant6.Text = Nothing
        txt_devcant7.Text = Nothing
        txt_devcant8.Text = Nothing



        txt_cant1.Text = Nothing
        txt_dx1.Text = Nothing
        txt_uecant1.Text = Nothing
        txt_cant2.Text = Nothing
        txt_dx2.Text = Nothing
        txt_uecant2.Text = Nothing
        txt_cant3.Text = Nothing
        txt_dx3.Text = Nothing
        txt_uecant3.Text = Nothing
        txt_cant4.Text = Nothing
        txt_dx4.Text = Nothing
        txt_uecant4.Text = Nothing
        txt_cant5.Text = Nothing
        txt_dx5.Text = Nothing
        txt_uecant5.Text = Nothing
        txt_cant6.Text = Nothing
        txt_dx6.Text = Nothing
        txt_uecant6.Text = Nothing
        txt_cant7.Text = Nothing
        txt_dx7.Text = Nothing
        txt_uecant7.Text = Nothing
        txt_cant8.Text = Nothing
        txt_dx8.Text = Nothing
        txt_uecant8.Text = Nothing
        txt_adherencia.Text = Nothing

        lbl_proximacitaTS.Text = String.Empty
        lbl_proximacitaMangua.Text = String.Empty

        buscaNHC()
    End Sub

    Protected Sub DDL_sesquema_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDL_sesquema.SelectedIndexChanged
        If DDL_esquema.SelectedValue <> "0" Then
            llenacodigo(DDL_sesquema.SelectedValue.ToString())
            DDL_esquemaestatus.Focus()
            LlenaMedicamentos()
        End If
    End Sub

    Sub LlenaMedicamentos()
        Dim Med1 As Integer = CStr(DDL_cod1.Items.Count)
        Dim Med2 As Integer = CStr(DDL_cod2.Items.Count)
        Dim Med3 As Integer = CStr(DDL_cod3.Items.Count)
        Dim Med4 As Integer = CStr(DDL_cod4.Items.Count)
        Dim Med5 As Integer = CStr(DDL_cod5.Items.Count)
        Dim Med6 As Integer = CStr(DDL_cod6.Items.Count)
        Dim Med7 As Integer = CStr(DDL_cod7.Items.Count)
        Dim Med8 As Integer = CStr(DDL_cod8.Items.Count)

        'Medicamento 1
        If Med1 > 1 Then
            DDL_cod1.SelectedIndex = 1
            DDL_cod1.Enabled = False
            txt_cant1.Enabled = True
            txt_dx1.Enabled = True
            DDL_fx1.Enabled = True
            DDL_earv1.Enabled = True
            txt_uecant1.Enabled = True
        Else
            DDL_cod1.SelectedIndex = 0
        End If
        'Medicamento 2
        If Med2 > 2 Then
            DDL_cod2.SelectedIndex = 2
            DDL_cod2.Enabled = False
            txt_cant2.Enabled = True
            txt_dx2.Enabled = True
            DDL_fx2.Enabled = True
            DDL_earv2.Enabled = True
            txt_uecant2.Enabled = True
        Else
            DDL_cod2.SelectedIndex = 0
        End If
        'Medicamento 3
        If Med3 > 3 Then
            DDL_cod3.SelectedIndex = 3
            DDL_cod3.Enabled = False
            txt_cant3.Enabled = True
            txt_dx3.Enabled = True
            DDL_fx3.Enabled = True
            DDL_earv3.Enabled = True
            txt_uecant3.Enabled = True
        Else
            DDL_cod3.SelectedIndex = 0
        End If
        'Medicamento 4
        If Med4 > 4 Then
            DDL_cod4.SelectedIndex = 4
            DDL_cod4.Enabled = False
            txt_cant4.Enabled = True
            txt_dx4.Enabled = True
            DDL_fx4.Enabled = True
            DDL_earv4.Enabled = True
            txt_uecant4.Enabled = True
        Else
            DDL_cod4.SelectedIndex = 0
        End If
        'Medicamento 5
        If Med5 > 5 Then
            DDL_cod5.SelectedIndex = 5
            DDL_cod5.Enabled = False
            txt_cant5.Enabled = True
            txt_dx5.Enabled = True
            DDL_fx5.Enabled = True
            DDL_earv5.Enabled = True
            txt_uecant5.Enabled = True
        Else
            DDL_cod5.SelectedIndex = 0
        End If
        'Medicamento 6
        If Med6 > 6 Then
            DDL_cod6.SelectedIndex = 6
            DDL_cod6.Enabled = False
            txt_cant6.Enabled = True
            txt_dx6.Enabled = True
            DDL_fx6.Enabled = True
            DDL_earv6.Enabled = True
            txt_uecant6.Enabled = True
        Else
            DDL_cod6.SelectedIndex = 0
        End If
        'Medicamento 7
        If Med3 > 7 Then
            DDL_cod7.SelectedIndex = 7
            DDL_cod7.Enabled = False
            txt_cant7.Enabled = True
            txt_dx7.Enabled = True
            DDL_fx7.Enabled = True
            DDL_earv7.Enabled = True
            txt_uecant7.Enabled = True
        Else
            DDL_cod7.SelectedIndex = 0
        End If
        'Medicamento 8
        If Med8 > 8 Then
            DDL_cod8.SelectedIndex = 8
            DDL_cod8.Enabled = False
            txt_cant8.Enabled = True
            txt_dx8.Enabled = True
            DDL_fx8.Enabled = True
            DDL_earv8.Enabled = True
            txt_uecant8.Enabled = True
        Else
            DDL_cod8.SelectedIndex = 0
        End If
    End Sub
    'Datos CD4 y CV
    Sub DatosLab(ByVal NHC As String)
        db.Cn2 = cn2
        Dim x As String = db.ResultadoCD4CV(NHC, usuario)
        Dim rp As String() = x.Split("|")
        If rp(0).ToString() = "True" Then
            strnhc = NHC
            Session("nhc") = NHC
            existenhc = True
            txt_cd4.Text = rp(1).ToString()
            txt_cv.Text = rp(2).ToString()
            txt_cd4.Enabled = False
            txt_cv.Enabled = False
        End If
    End Sub
    Protected Sub btn_calcular_adherencia_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_calcular_adherencia.Click
        'Dim en As New CultureInfo("en-US")
        'Thread.CurrentThread.CurrentCulture = en
        'Dim format As [String] = "dd/MM/yyyy"
        db.Cn1 = cn1
        Dim fecha_retorno As String
        Dim fecha_entrega As String
        Dim fecha3 As String = DateTime.Now.ToString("dd/MM/yyyy")
        Dim date3 As Date = DateTime.ParseExact(fecha3, "dd/MM/yyyy", Nothing)
        Dim total_entregado As Integer
        Dim adherencia_calculo As Integer
        Dim medicamento_tomado As Integer
        Dim medicamento_por_dia As Integer
        Dim total_medicamento_dias_tarde As Integer
        Dim total_medicamento_dias_antes As Integer

        Dim dev1 As String = txt_devcant1.Text
        Dim dev2 As String = txt_devcant2.Text
        Dim dev3 As String = txt_devcant3.Text
        Dim dev4 As String = txt_devcant4.Text
        Dim dev5 As String = txt_devcant5.Text
        Dim dev6 As String = txt_devcant6.Text
        Dim dev7 As String = txt_devcant7.Text
        Dim dev8 As String = txt_devcant8.Text


        If dev1 = "" Then
            dev1 = 0
        End If

        If dev2 = "" Then
            dev2 = 0
        End If

        If dev3 = "" Then
            dev3 = 0
        End If

        If dev4 = "" Then
            dev4 = 0
        End If

        If dev5 = "" Then
            dev5 = 0
        End If

        If dev6 = "" Then
            dev6 = 0
        End If


        If dev7 = "" Then
            dev7 = 0
        End If

        If dev8 = "" Then
            dev8 = 0
        End If

        Dim total_med_devuelto As Int32 = (CDbl(dev1) + CDbl(dev2) + CDbl(dev3) + CDbl(dev4) + CDbl(dev5) + CDbl(dev6) + CDbl(dev7) + CDbl(dev8))
        Dim x As String = db.ObtieneUltimoReg_calculo_Adherencia(txt_asi.Text.ToString(), usuario)
        Dim rp As String() = x.Split("|")
        If rp(0).ToString() = "True" Then
            fecha_retorno = Convert.ToDateTime(rp(3)) '.ToString()
            ' fecha_retorno = "10/08/2019"
            fecha_entrega = Convert.ToDateTime(rp(2)) '.ToString()d
            Dim provider As IFormatProvider = CultureInfo.InvariantCulture


            'Dim date_fecha_ultima_entrega As Date = Date.ParseExact(fecha_entrega, "dd/MM/yyyy", provider)


            'Dim date_retorno As Date = Date.ParseExact(fecha_retorno, "dd/MM/yyyy", provider)
            Dim date_fecha_ultima_entrega As Date = fecha_entrega


            Dim date_retorno As Date = fecha_retorno


            Dim dia_retorno As Long = DateDiff(DateInterval.Day, date_retorno, date3)

            Dim total_dias_entregado As Long = DateDiff(DateInterval.Day, date_fecha_ultima_entrega, date_retorno)

            total_entregado = rp(4).ToString()

            If dia_retorno = 0 Then


                medicamento_tomado = total_entregado - total_med_devuelto
                adherencia_calculo = medicamento_tomado / total_entregado * 100
                txt_adherencia.Text = adherencia_calculo

            ElseIf dia_retorno < 0 Then
                'calculo de medicamento por dia 
                medicamento_por_dia = total_entregado / total_dias_entregado
                'total de medicamento  por la cantidad de dias entre ultima fecha entrega y fecha de presentarse a cita / tuvo que haber tomado

                Dim valor_dias_retorno_positivo As UInteger = Math.Abs(dia_retorno)

                Dim total_med_devuelto_dias_anteriores As Integer = valor_dias_retorno_positivo * medicamento_por_dia

                Dim real_devuelto As Integer = total_med_devuelto_dias_anteriores - total_med_devuelto

                Dim nuevo_total_dias_entregado As Integer = total_dias_entregado - valor_dias_retorno_positivo
                Dim real_med_entregado_x_dias As Integer = nuevo_total_dias_entregado * medicamento_por_dia

                medicamento_tomado = real_med_entregado_x_dias - real_devuelto


                adherencia_calculo = medicamento_tomado / real_med_entregado_x_dias * 100
                txt_adherencia.Text = adherencia_calculo

            ElseIf dia_retorno > 0 Then

                medicamento_por_dia = total_entregado / total_dias_entregado
                'total de medicamento  por la cantidad de dias entre ultima fecha entrega y fecha de presentarse a cita / tuvo que haber tomado

                Dim valor_dias_retorno_positivo As UInteger = Math.Abs(dia_retorno)

                Dim total_med_devuelto_dias_retrasados As Integer = valor_dias_retorno_positivo * medicamento_por_dia

                'Dim real_devuelto As Integer = total_med_devuelto - total_med_devuelto_dias_retrasados

                Dim nuevo_total_dias_entregado As Integer = total_dias_entregado + valor_dias_retorno_positivo
                Dim real_med_entregado_x_dias As Integer = nuevo_total_dias_entregado * medicamento_por_dia

                medicamento_tomado = real_med_entregado_x_dias - total_med_devuelto_dias_retrasados - total_med_devuelto


                adherencia_calculo = medicamento_tomado / real_med_entregado_x_dias * 100
                txt_adherencia.Text = adherencia_calculo



                'medicamento_por_dia = total_entregado / total_dias_entregado
                'total_medicamento_dias_antes = (total_dias_entregado - dia_retorno)
                'medicamento_tomado = total_medicamento_dias_antes - total_med_devuelto
                'adherencia_calculo = medicamento_tomado / total_medicamento_dias_antes * 100
                'txt_adherencia.Text = adherencia_calculo


            End If


        End If



    End Sub

    Protected Sub btn_end_Click(ByVal sender As Object, ByVal e As System.EventArgs)



    End Sub


    Protected Sub btn_init_Click(ByVal sender As Object, ByVal e As System.EventArgs)



    End Sub



    Protected Sub txt_asi_TextChanged1(sender As Object, e As EventArgs)

    End Sub
    Protected Sub btn_dias_retorno_Click(sender As Object, e As EventArgs) Handles btn_dias_retorno.Click
        Dim date_ret As Date
        Dim date_entrega As Date
        Dim FechaEntrega As String = Convert.ToString(txt_fe_dd.Text) + "/" + Convert.ToString(txt_fe_mm.Text) + "/" + Convert.ToString(txt_fe_yy.Text)
        Dim FechaRetorno As String = Convert.ToString(txt_fr_dd.Text) + "/" + Convert.ToString(txt_fr_mm.Text) + "/" + Convert.ToString(txt_fr_yy.Text)

        date_entrega = Convert.ToDateTime(FechaEntrega)
        date_ret = Convert.ToDateTime(FechaRetorno)

        Dim dia_retorno As Long = DateDiff(DateInterval.Day, date_entrega, date_ret)
        txt_tarvdias.Text = dia_retorno
    End Sub
	
	'15-12-2020 jchete
    Sub llenaEstatusPacNuevo()
        db.Cn1 = cn1
        usuario = Session("usuario").ToString()
        Dim tbE As DataTable = db.ObtieneEstatusPacienteNuevo(usuario)
        If tbE IsNot Nothing Then
            DDL_esquemaestatus.DataSource = tbE
            DDL_esquemaestatus.DataTextField = "Codigo"
            DDL_esquemaestatus.DataValueField = "IdEstatus"
            DDL_esquemaestatus.DataBind()
            DDL_esquemaestatus.SelectedValue = 2
        End If
    End Sub

    'jchete 11-01-2021
    Sub LlenaEstatusComplementoSuspendido()
        db.Cn1 = cn1
        usuario = Session("usuario").ToString()
        Dim tbE As DataTable = db.ObtieneEstatusComplementoSuspendido(usuario)
        If tbE IsNot Nothing Then
            DDL_esquemaestatus.DataSource = tbE
            DDL_esquemaestatus.DataTextField = "Codigo"
            DDL_esquemaestatus.DataValueField = "IdEstatus"
            DDL_esquemaestatus.DataBind()
            'DDL_esquemaestatus.SelectedValue = 2
        End If
    End Sub

    '17-12-20 jchete
    Sub llenaEstatusPacXsuspendido()
        db.Cn1 = cn1
        usuario = Session("usuario").ToString()
        Dim tbE As DataTable = db.ObtieneEstatusXpaciSuspendido(usuario)
        If tbE IsNot Nothing Then
            DDL_esquemaestatus.DataSource = tbE
            DDL_esquemaestatus.DataTextField = "Codigo"
            DDL_esquemaestatus.DataValueField = "IdEstatus"
            DDL_esquemaestatus.DataBind()
            'DDL_esquemaestatus.SelectedValue = 2
        End If
    End Sub

    'jchete 07-01-2021
    Sub llenaEstatusPacSiContinua()
        db.Cn1 = cn1
        usuario = Session("usuario").ToString()
        Dim tbE As DataTable = db.ObtieneEstatusSiEstadoContinua(usuario)
        If tbE IsNot Nothing Then
            DDL_esquemaestatus.DataSource = tbE
            DDL_esquemaestatus.DataTextField = "Codigo"
            DDL_esquemaestatus.DataValueField = "IdEstatus"
            DDL_esquemaestatus.DataBind()
            'DDL_esquemaestatus.SelectedValue = 2
        End If
    End Sub
	
	
	
Protected Sub cbl_enviomed_CheckedChanged(sender As Object, e As EventArgs) Handles cbl_enviomed.CheckedChanged
        If cbl_enviomed.Checked = True Then
            cbl_paciente_hospitalizado.Checked = False
        Else
            cbl_paciente_hospitalizado.Checked = False
        End If
    End Sub

    Protected Sub cbl_paciente_hospitalizado_CheckedChanged(sender As Object, e As EventArgs) Handles cbl_paciente_hospitalizado.CheckedChanged
        If cbl_paciente_hospitalizado.Checked = True Then
            cbl_enviomed.Checked = False
        Else
            cbl_enviomed.Checked = False
        End If
    End Sub

    Sub calculadiasretornototal()

        Dim fechaproxcita As Date = Convert.ToDateTime(lbl_proximacitaMangua.Text)
        Dim fechahoy As Date = Today()

        Dim dias_proximacita As Long = DateDiff(DateInterval.Day, fechahoy, fechaproxcita)

        lbl_diasparacita.Text = dias_proximacita.ToString() + " dias"
        lbl_tiemporetornooficial.Text = dias_proximacita.ToString() + " dias"


    End Sub



End Class
