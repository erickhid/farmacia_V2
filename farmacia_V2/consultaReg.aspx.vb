Imports System.Data
Imports System.Data.SqlClient

Partial Class consultaReg
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
    Public idarv As String
    Public idprof As String
    Public datos As String
    Public rol As String = ""
    Public edicion As Boolean = False	

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
                Dim v As Boolean = False
                rol = Convert.ToString(Session("pusuario"))
                Select Case rol
                    Case "1", "2", "6" 'Master, Administrador, Supervisor
                        v = True
                    Case "3", "4", "7" 'Digitador, Consulta, Digitador+Reportes
                        v = True
                    Case "5" 'Reportes
                        v = False
                        Response.Redirect("~/acceso.aspx", False)
                End Select
                If v Then
                    llenadatos(Request.QueryString("nhc").ToUpper())
                    Llena_regPac(Request.QueryString("nhc").ToUpper())

                    Select Case rol
                        Case "1", "2", "6" 'Master, Administrador, Supervisor
                            For Each row As GridViewRow In GV_regPacA.Rows
                                CType(row.FindControl("IB_eliminar"), ImageButton).Visible = True
                            Next
                            For Each row As GridViewRow In GV_regPacP.Rows
                                CType(row.FindControl("IB_eliminar"), ImageButton).Visible = True
                            Next


                        Case "3", "4", "7" 'Digitador, Consulta, Digitador+Reportes
                            For Each row As GridViewRow In GV_regPacA.Rows
                                CType(row.FindControl("IB_eliminar"), ImageButton).Visible = False
                            Next
                            For Each row As GridViewRow In GV_regPacP.Rows
                                CType(row.FindControl("IB_eliminar"), ImageButton).Visible = False
                            Next

                    End Select

                End If
            End If
        End If
    End Sub

    Private Sub Llena_regPac(ByVal nhc As String)
        Try
            db.Cn1 = cn1
            usuario = Session("usuario").ToString()
            Dim tbpacA As DataTable = db.RegistrosPacienteARV(nhc, usuario)
            Session("dspacA") = tbpacA
            GV_regPacA.DataSource = tbpacA
            GV_regPacA.DataBind()
            GV_regPacA.SelectedIndex = 0
            Dim tbpacP As DataTable = db.RegistrosPacienteProf(nhc, usuario)
            Session("dspacP") = tbpacP
            GV_regPacP.DataSource = tbpacP
            GV_regPacP.DataBind()
            GV_regPacP.SelectedIndex = 0
        Catch ex As Exception
            'lbl_error.Text = "Hubo un error al mostrar las Propiedades."
            errores = (usuario & "|Pacientes.Llena_regPac()|" & ex.ToString() & "|") + ex.Message
            db.GrabarErrores(errores)
        End Try
    End Sub

    Sub llenaregistro(ByVal id As String)
        db.Cn1 = cn1
        Dim y As String = db.ObtieneRegARV(id, usuario)
        Dim rpU As String() = y.Split("|")
        If rpU(0).ToString() = "True" Then

            lbl_usuario.Text = usuario
            idarv = rpU(1).ToString()
            lbl_id.Text = rpU(1).ToString()
            lbl_fe.Text = rpU(2).Substring(0, 10).ToString()
            DDL_esquema.SelectedValue = rpU(69).ToString()
            lbl_esquema.Text = db.ObtieneEsquema(rpU(69).ToString(), usuario)
            If rpU(70).ToString() = String.Empty Then
                llenacodigo()
            Else
                llenasesquema(rpU(69).ToString(), rpU(70).ToString())
                DDL_sesquema.SelectedValue = rpU(70).ToString()
            End If
            DDL_esquemaestatus.SelectedValue = rpU(71).ToString()
            DDL_cod1.SelectedValue = rpU(3).ToString()
            txt_cant1.Text = rpU(4).ToString()
            lbl_cantidad1.Text = rpU(4).ToString()
            txt_dx1.Text = rpU(5).ToString()
            DDL_fx1.SelectedValue = rpU(6).ToString()
            txt_uecant1.Text = rpU(7).ToString()
            DDL_earv1.SelectedValue = rpU(8).ToString()
            txt_devcant1.Text = rpU(9).ToString()
            DDL_cod2.SelectedValue = rpU(10).ToString()
            txt_cant2.Text = rpU(11).ToString()
            lbl_cantidad2.Text = rpU(11).ToString()
            txt_dx2.Text = rpU(12).ToString()
            DDL_fx2.SelectedValue = rpU(13).ToString()
            txt_uecant2.Text = rpU(14).ToString()
            DDL_earv2.SelectedValue = rpU(15).ToString()
            txt_devcant2.Text = rpU(16).ToString()
            DDL_cod3.SelectedValue = rpU(17).ToString()
            txt_cant3.Text = rpU(18).ToString()
            lbl_cantidad3.Text = rpU(18).ToString()
            txt_dx3.Text = rpU(19).ToString()
            DDL_fx3.SelectedValue = rpU(20).ToString()
            txt_uecant3.Text = rpU(21).ToString()
            DDL_earv3.SelectedValue = rpU(22).ToString()
            txt_devcant3.Text = rpU(23).ToString()
            DDL_cod4.SelectedValue = rpU(24).ToString()
            txt_cant4.Text = rpU(25).ToString()
            lbl_cantidad4.Text = rpU(25).ToString()
            txt_dx4.Text = rpU(26).ToString()
            DDL_fx4.SelectedValue = rpU(27).ToString()
            txt_uecant4.Text = rpU(28).ToString()
            DDL_earv4.SelectedValue = rpU(29).ToString()
            txt_devcant4.Text = rpU(30).ToString()
            DDL_cod5.SelectedValue = rpU(31).ToString()
            txt_cant5.Text = rpU(32).ToString()
            lbl_cantidad5.Text = rpU(32).ToString()
            txt_dx5.Text = rpU(33).ToString()
            lbl_cantidad5.Text = rpU(33).ToString()
            DDL_fx5.SelectedValue = rpU(34).ToString()
            txt_uecant5.Text = rpU(35).ToString()
            DDL_earv5.SelectedValue = rpU(36).ToString()
            txt_devcant5.Text = rpU(37).ToString()
            DDL_cod6.SelectedValue = rpU(38).ToString()
            txt_cant6.Text = rpU(39).ToString()
            txt_dx6.Text = rpU(40).ToString()
            lbl_cantidad6.Text = rpU(40).ToString()
            DDL_fx6.SelectedValue = rpU(41).ToString()
            txt_uecant6.Text = rpU(42).ToString()
            DDL_earv6.SelectedValue = rpU(43).ToString()
            txt_devcant6.Text = rpU(44).ToString()
            DDL_cod7.SelectedValue = rpU(45).ToString()
            txt_cant7.Text = rpU(46).ToString()
            lbl_cantidad7.Text = rpU(46).ToString()
            txt_dx7.Text = rpU(47).ToString()
            DDL_fx7.SelectedValue = rpU(48).ToString()
            txt_uecant7.Text = rpU(49).ToString()
            DDL_earv7.SelectedValue = rpU(50).ToString()
            txt_devcant7.Text = rpU(51).ToString()
            DDL_cod8.SelectedValue = rpU(52).ToString()
            txt_cant8.Text = rpU(53).ToString()
            lbl_cantidad8.Text = rpU(53).ToString()
            txt_dx8.Text = rpU(54).ToString()
            DDL_fx8.SelectedValue = rpU(55).ToString()
            txt_uecant8.Text = rpU(56).ToString()
            DDL_earv8.SelectedValue = rpU(57).ToString()
            txt_devcant8.Text = rpU(58).ToString()
            txt_fr_dd.Text = rpU(59).Substring(0, 2).ToString()
            txt_fr_mm.Text = rpU(59).Substring(3, 2).ToString()
            txt_fr_yy.Text = rpU(59).Substring(6, 4).ToString()
            txt_tarvdias.Text = rpU(60).ToString()
            CB_citaMx.Checked = cita(rpU(61).ToString())
            CB_citaFx.Checked = cita(rpU(62).ToString())
            DDL_embarazo.SelectedValue = rpU(63).ToString()
            txt_retornodias.Text = rpU(64).ToString()
            txt_adherencia.Text = rpU(65).ToString()
            txt_cd4.Text = rpU(66).ToString()
            txt_cv.Text = rpU(67).ToString()
            txt_observaciones.Text = rpU(68).ToString()
            If Not String.IsNullOrEmpty(rpU(72).ToString()) Then
                ddl_auto_adherencia.SelectedValue = rpU(72).ToString()
            Else
                ddl_auto_adherencia.SelectedIndex = 0
            End If

        Else
            idarv = String.Empty
            lbl_fe.Text = String.Empty
            DDL_cod1.SelectedValue = String.Empty
            txt_cant1.Text = String.Empty
            lbl_cantidad1.Text = String.Empty
            txt_dx1.Text = String.Empty
            DDL_fx1.SelectedValue = String.Empty
            txt_uecant1.Text = String.Empty
            DDL_earv1.SelectedValue = String.Empty
            txt_devcant1.Text = String.Empty
            DDL_cod2.SelectedValue = String.Empty
            txt_cant2.Text = String.Empty
            lbl_cantidad2.Text = String.Empty
            txt_dx2.Text = String.Empty
            DDL_fx2.SelectedValue = String.Empty
            txt_uecant2.Text = String.Empty
            DDL_earv2.SelectedValue = String.Empty
            txt_devcant2.Text = String.Empty
            DDL_cod3.SelectedValue = String.Empty
            txt_cant3.Text = String.Empty
            lbl_cantidad3.Text = String.Empty
            txt_dx3.Text = String.Empty
            DDL_fx3.SelectedValue = String.Empty
            txt_uecant3.Text = String.Empty
            DDL_earv3.SelectedValue = String.Empty
            txt_devcant3.Text = String.Empty
            DDL_cod4.SelectedValue = String.Empty
            txt_cant4.Text = String.Empty
            lbl_cantidad4.Text = String.Empty
            txt_dx4.Text = String.Empty
            DDL_fx4.SelectedValue = String.Empty
            txt_uecant4.Text = String.Empty
            DDL_earv4.SelectedValue = String.Empty
            txt_devcant4.Text = String.Empty
            DDL_cod5.SelectedValue = String.Empty
            txt_cant5.Text = String.Empty
            lbl_cantidad5.Text = String.Empty
            txt_dx5.Text = String.Empty
            DDL_fx5.SelectedValue = String.Empty
            txt_uecant5.Text = String.Empty
            DDL_earv5.SelectedValue = String.Empty
            txt_devcant5.Text = String.Empty
            DDL_cod6.SelectedValue = String.Empty
            txt_cant6.Text = String.Empty
            lbl_cantidad6.Text = String.Empty
            txt_dx6.Text = String.Empty
            DDL_fx6.SelectedValue = String.Empty
            txt_uecant6.Text = String.Empty
            DDL_earv6.SelectedValue = String.Empty
            txt_devcant6.Text = String.Empty
            DDL_cod7.SelectedValue = String.Empty
            txt_cant7.Text = String.Empty
            lbl_cantidad7.Text = String.Empty
            txt_dx7.Text = String.Empty
            DDL_fx7.SelectedValue = String.Empty
            txt_uecant7.Text = String.Empty
            DDL_earv7.SelectedValue = String.Empty
            txt_devcant7.Text = String.Empty
            DDL_cod8.SelectedValue = String.Empty
            txt_cant8.Text = String.Empty
            lbl_cantidad8.Text = String.Empty
            txt_dx8.Text = String.Empty
            DDL_fx8.SelectedValue = String.Empty
            txt_uecant8.Text = String.Empty
            DDL_earv8.SelectedValue = String.Empty
            txt_devcant8.Text = String.Empty
            txt_fr_dd.Text = String.Empty
            txt_fr_mm.Text = String.Empty
            txt_fr_yy.Text = String.Empty
            txt_tarvdias.Text = String.Empty
            CB_citaMx.Checked = False
            CB_citaFx.Checked = False
            DDL_embarazo.SelectedValue = String.Empty
            txt_retornodias.Text = String.Empty
            txt_adherencia.Text = String.Empty
            txt_cd4.Text = String.Empty
            txt_cv.Text = String.Empty
            txt_observaciones.Text = String.Empty
            ddl_auto_adherencia.SelectedValue = String.Empty
        End If
    End Sub

    Function cita(ByVal n As String) As Boolean
        Dim r As Boolean = False
        Select Case n
            Case "1"
                r = True
            Case "2"
                r = False
        End Select
        Return r
    End Function

    Sub llenaregistroP(ByVal id As String)
        db.Cn1 = cn1
        Dim y As String = db.ObtieneRegProf(id, usuario)
        Dim rpU As String() = y.Split("|")
        If rpU(0).ToString() = "True" Then
            lbl_usuarioP.Text = usuario
            lbl_idP.Text = rpU(1).ToString()
            lbl_feP.Text = rpU(2).Substring(0, 10).ToString()
            RBL_tipopaciente.SelectedValue = rpU(3).ToString()
            '**Prof1**
            DDL_cod1P.SelectedValue = rpU(4).ToString()
            lbl_prof1.Text = rpU(5).ToString()
            txt_cant1P.Text = rpU(6).ToString()
            lbl_cantidadP1.Text = rpU(6).ToString()
            txt_dx1P.Text = rpU(7).ToString()
            DDL_Via1.SelectedValue = rpU(8).ToString()
            DDL_fx1P.SelectedValue = rpU(9).ToString()
            DDL_t1.SelectedValue = rpU(10).ToString()
            If rpU(11).ToString() = String.Empty Then
                DDL_Trat1.SelectedValue = "1"
            Else
                DDL_Trat1.SelectedValue = rpU(11).ToString()
                If DDL_t1.SelectedValue = "2" Then
                    DDL_Trat1.Visible = True
                    DDL_Trat1.Enabled = False
                Else
                    DDL_Trat1.Visible = False
                    DDL_Trat1.Enabled = False
                End If
            End If
            DDL_e1.SelectedValue = rpU(12).ToString()
            txt_tdias1.Text = rpU(13).ToString()
            txt_obs1.Text = rpU(14).ToString()
            '**Prof2**
            DDL_cod2P.SelectedValue = rpU(15).ToString()
            lbl_prof2.Text = rpU(16).ToString()
            txt_cant2P.Text = rpU(17).ToString()
            lbl_cantidadP2.Text = rpU(17).ToString()
            txt_dx2P.Text = rpU(18).ToString()
            DDL_Via2.SelectedValue = rpU(19).ToString()
            DDL_fx2P.SelectedValue = rpU(20).ToString()
            DDL_t2.SelectedValue = rpU(21).ToString()
            If rpU(22).ToString() = String.Empty Then
                DDL_Trat2.SelectedValue = "1"
            Else
                DDL_Trat2.SelectedValue = rpU(22).ToString()
                If DDL_t2.SelectedValue = "2" Then
                    DDL_Trat2.Visible = True
                    DDL_Trat2.Enabled = False
                Else
                    DDL_Trat2.Visible = False
                    DDL_Trat2.Enabled = False
                End If
            End If
            DDL_e2.SelectedValue = rpU(23).ToString()
            txt_tdias2.Text = rpU(24).ToString()
            txt_obs2.Text = rpU(25).ToString()
            '**Prof3**
            DDL_cod3P.SelectedValue = rpU(26).ToString()
            lbl_prof3.Text = rpU(27).ToString()
            txt_cant3P.Text = rpU(28).ToString()
            lbl_cantidadP3.Text = rpU(28).ToString()
            txt_dx3P.Text = rpU(29).ToString()
            DDL_Via3.SelectedValue = rpU(30).ToString()
            DDL_fx3P.SelectedValue = rpU(31).ToString()
            DDL_t3.SelectedValue = rpU(32).ToString()
            If rpU(33).ToString() = String.Empty Then
                DDL_Trat3.SelectedValue = "1"
            Else
                DDL_Trat3.SelectedValue = rpU(33).ToString()
                If DDL_t3.SelectedValue = "2" Then
                    DDL_Trat3.Visible = True
                    DDL_Trat3.Enabled = False
                Else
                    DDL_Trat3.Visible = False
                    DDL_Trat3.Enabled = False
                End If
            End If
            DDL_e3.SelectedValue = rpU(34).ToString()
            txt_tdias3.Text = rpU(35).ToString()
            txt_obs3.Text = rpU(36).ToString()
            '**Prof4**
            DDL_cod4P.SelectedValue = rpU(37).ToString()
            lbl_prof4.Text = rpU(38).ToString()
            lbl_cantidadP4.Text = rpU(39).ToString()
            txt_cant4P.Text = rpU(39).ToString()
            txt_dx4P.Text = rpU(40).ToString()
            DDL_Via4.SelectedValue = rpU(41).ToString()
            DDL_fx4P.SelectedValue = rpU(42).ToString()
            DDL_t4.SelectedValue = rpU(43).ToString()
            If rpU(44).ToString() = String.Empty Then
                DDL_Trat4.SelectedValue = "1"
            Else
                DDL_Trat4.SelectedValue = rpU(44).ToString()
                If DDL_t4.SelectedValue = "2" Then
                    DDL_Trat4.Visible = True
                    DDL_Trat4.Enabled = False
                Else
                    DDL_Trat4.Visible = False
                    DDL_Trat4.Enabled = False
                End If
            End If
            DDL_e4.SelectedValue = rpU(45).ToString()
            txt_tdias4.Text = rpU(46).ToString()
            txt_obs4.Text = rpU(47).ToString()
            '**Prof5**
            DDL_cod5P.SelectedValue = rpU(48).ToString()
            lbl_prof5.Text = rpU(49).ToString()
            txt_cant5P.Text = rpU(50).ToString()
            lbl_cantidadP5.Text = rpU(50).ToString()
            txt_dx5P.Text = rpU(51).ToString()
            DDL_Via5.SelectedValue = rpU(52).ToString()
            DDL_fx5P.SelectedValue = rpU(53).ToString()
            DDL_t5.SelectedValue = rpU(54).ToString()
            If rpU(55).ToString() = String.Empty Then
                DDL_Trat5.SelectedValue = "1"
            Else
                DDL_Trat5.SelectedValue = rpU(55).ToString()
                If DDL_t5.SelectedValue = "2" Then
                    DDL_Trat5.Visible = True
                    DDL_Trat5.Enabled = False
                Else
                    DDL_Trat5.Visible = False
                    DDL_Trat5.Enabled = False
                End If
            End If
            DDL_e5.SelectedValue = rpU(56).ToString()
            txt_tdias5.Text = rpU(57).ToString()
            txt_obs5.Text = rpU(58).ToString()
            '**Prof6**
            DDL_cod6P.SelectedValue = rpU(59).ToString()
            lbl_prof6.Text = rpU(60).ToString()
            txt_cant6P.Text = rpU(61).ToString()
            lbl_cantidadP6.Text = rpU(61).ToString()
            txt_dx6P.Text = rpU(62).ToString()
            DDL_Via6.SelectedValue = rpU(63).ToString()
            DDL_fx6P.SelectedValue = rpU(64).ToString()
            DDL_t6.SelectedValue = rpU(65).ToString()
            If rpU(66).ToString() = String.Empty Then
                DDL_Trat6.SelectedValue = "1"
            Else
                DDL_Trat6.SelectedValue = rpU(66).ToString()
                If DDL_t6.SelectedValue = "2" Then
                    DDL_Trat6.Visible = True
                    DDL_Trat6.Enabled = False
                Else
                    DDL_Trat6.Visible = False
                    DDL_Trat6.Enabled = False
                End If
            End If
            DDL_e6.SelectedValue = rpU(67).ToString()
            txt_tdias6.Text = rpU(68).ToString()
            txt_obs6.Text = rpU(69).ToString()
            txt_CD4P.Text = rpU(70).ToString()
        Else
            lbl_usuarioP.Text = String.Empty
            lbl_idP.Text = String.Empty
            lbl_feP.Text = String.Empty
            RBL_tipopaciente.SelectedValue = String.Empty
            '**Prof1**
            DDL_cod1P.SelectedValue = String.Empty
            lbl_prof1.Text = String.Empty
            txt_cant1P.Text = String.Empty
            lbl_cantidadP1.Text = String.Empty
            txt_dx1P.Text = String.Empty
            DDL_Via1.SelectedValue = String.Empty
            DDL_fx1P.SelectedValue = String.Empty
            DDL_t1.SelectedValue = String.Empty
            DDL_Trat1.SelectedValue = String.Empty
            DDL_e1.SelectedValue = String.Empty
            txt_tdias1.Text = String.Empty
            txt_obs1.Text = String.Empty
            '**Prof2**
            DDL_cod2P.SelectedValue = String.Empty
            lbl_prof2.Text = String.Empty
            txt_cant2P.Text = String.Empty
            lbl_cantidadP2.Text = String.Empty
            txt_dx2P.Text = String.Empty
            DDL_Via2.SelectedValue = String.Empty
            DDL_fx2P.SelectedValue = String.Empty
            DDL_t2.SelectedValue = String.Empty
            DDL_Trat2.SelectedValue = String.Empty
            DDL_e2.SelectedValue = String.Empty
            txt_tdias2.Text = String.Empty
            txt_obs2.Text = String.Empty
            '**Prof3**
            DDL_cod3P.SelectedValue = String.Empty
            lbl_prof3.Text = String.Empty
            txt_cant3P.Text = String.Empty
            lbl_cantidadP3.Text = String.Empty
            txt_dx3P.Text = String.Empty
            DDL_Via3.SelectedValue = String.Empty
            DDL_fx3P.SelectedValue = String.Empty
            DDL_t3.SelectedValue = String.Empty
            DDL_Trat3.SelectedValue = String.Empty
            DDL_e3.SelectedValue = String.Empty
            txt_tdias3.Text = String.Empty
            txt_obs3.Text = String.Empty
            '**Prof4**
            DDL_cod4P.SelectedValue = String.Empty
            lbl_prof4.Text = String.Empty
            txt_cant4P.Text = String.Empty
            lbl_cantidadP4.Text = String.Empty
            txt_dx4P.Text = String.Empty
            DDL_Via4.SelectedValue = String.Empty
            DDL_fx4P.SelectedValue = String.Empty
            DDL_t4.SelectedValue = String.Empty
            DDL_Trat4.SelectedValue = String.Empty
            DDL_e4.SelectedValue = String.Empty
            txt_tdias4.Text = String.Empty
            txt_obs4.Text = String.Empty
            '**Prof5**
            DDL_cod5P.SelectedValue = String.Empty
            lbl_prof5.Text = String.Empty
            txt_cant5P.Text = String.Empty
            lbl_cantidadP5.Text = String.Empty
            txt_dx5P.Text = String.Empty
            DDL_Via5.SelectedValue = String.Empty
            DDL_fx5P.SelectedValue = String.Empty
            DDL_t5.SelectedValue = String.Empty
            DDL_Trat5.SelectedValue = String.Empty
            DDL_e5.SelectedValue = String.Empty
            txt_tdias5.Text = String.Empty
            txt_obs5.Text = String.Empty
            '**Prof6**
            DDL_cod6P.SelectedValue = String.Empty
            lbl_prof6.Text = String.Empty
            txt_cant6P.Text = String.Empty
            lbl_cantidad6.Text = String.Empty
            txt_dx6P.Text = String.Empty
            DDL_Via6.SelectedValue = String.Empty
            DDL_fx6P.SelectedValue = String.Empty
            DDL_t6.SelectedValue = String.Empty
            DDL_Trat6.SelectedValue = String.Empty
            DDL_e6.SelectedValue = String.Empty
            txt_tdias6.Text = String.Empty
            txt_obs6.Text = String.Empty
            txt_cd4.Text = String.Empty
        End If
    End Sub

    'Sub llenaregistroP(ByVal id As String)
    '    db.Cn1 = cn1
    '    Dim y As String = db.ObtieneRegProf(id, usuario)
    '    Dim rpU As String() = y.Split("|")
    '    If rpU(0).ToString() = "True" Then
    '        idprof = rpU(1).ToString()
    '        lbl_feP.Text = rpU(2).Substring(0, 10).ToString()
    '        DDL_cod1P.SelectedValue = rpU(3).ToString()
    '        txt_cant1P.Text = rpU(4).ToString()
    '        txt_dx1P.Text = rpU(5).ToString()
    '        DDL_fx1P.SelectedValue = rpU(6).ToString()
    '        DDL_earv1P.SelectedValue = rpU(7).ToString()
    '        txt_devcant1P.Text = rpU(8).ToString()
    '        DDL_cod2P.SelectedValue = rpU(9).ToString()
    '        txt_cant2P.Text = rpU(10).ToString()
    '        txt_dx2P.Text = rpU(11).ToString()
    '        DDL_fx2P.SelectedValue = rpU(12).ToString()
    '        DDL_earv2P.SelectedValue = rpU(13).ToString()
    '        txt_devcant2P.Text = rpU(14).ToString()
    '        DDL_cod3P.SelectedValue = rpU(15).ToString()
    '        txt_cant3P.Text = rpU(16).ToString()
    '        txt_dx3P.Text = rpU(17).ToString()
    '        DDL_fx3P.SelectedValue = rpU(18).ToString()
    '        DDL_earv3P.SelectedValue = rpU(19).ToString()
    '        txt_devcant3P.Text = rpU(20).ToString()
    '        DDL_cod4P.SelectedValue = rpU(21).ToString()
    '        txt_cant4P.Text = rpU(22).ToString()
    '        txt_dx4P.Text = rpU(23).ToString()
    '        DDL_fx4P.SelectedValue = rpU(24).ToString()
    '        DDL_earv4P.SelectedValue = rpU(25).ToString()
    '        txt_devcant4P.Text = rpU(26).ToString()
    '        txt_fr_ddP.Text = rpU(27).Substring(0, 2).ToString()
    '        txt_fr_mmP.Text = rpU(27).Substring(3, 2).ToString()
    '        txt_fr_yyP.Text = rpU(27).Substring(6, 4).ToString()
    '        txt_tarvdiasP.Text = rpU(28).ToString()
    '        CB_citaMxP.Checked = rpU(29).ToString()
    '        CB_citaFxP.Checked = rpU(30).ToString()
    '        DDL_embarazoP.SelectedValue = rpU(31).ToString()
    '        txt_retornodiasP.Text = rpU(32).ToString()
    '        txt_cd4P.Text = rpU(33).ToString()
    '        txt_cvP.Text = rpU(34).ToString()
    '        txt_observacionesP.Text = rpU(35).ToString()
    '    Else
    '        idprof = String.Empty
    '        lbl_feP.Text = String.Empty
    '        DDL_cod1P.SelectedValue = String.Empty
    '        txt_cant1P.Text = String.Empty
    '        txt_dx1P.Text = String.Empty
    '        DDL_fx1P.SelectedValue = String.Empty
    '        DDL_earv1P.SelectedValue = String.Empty
    '        txt_devcant1P.Text = String.Empty
    '        DDL_cod2P.SelectedValue = String.Empty
    '        txt_cant2P.Text = String.Empty
    '        txt_dx2P.Text = String.Empty
    '        DDL_fx2P.SelectedValue = String.Empty
    '        DDL_earv2P.SelectedValue = String.Empty
    '        txt_devcant2P.Text = String.Empty
    '        DDL_cod3P.SelectedValue = String.Empty
    '        txt_cant3P.Text = String.Empty
    '        txt_dx3P.Text = String.Empty
    '        DDL_fx3P.SelectedValue = String.Empty
    '        DDL_earv3P.SelectedValue = String.Empty
    '        txt_devcant3P.Text = String.Empty
    '        DDL_cod4P.SelectedValue = String.Empty
    '        txt_cant4P.Text = String.Empty
    '        txt_dx4P.Text = String.Empty
    '        DDL_fx4P.SelectedValue = String.Empty
    '        DDL_earv4P.SelectedValue = String.Empty
    '        txt_devcant4P.Text = String.Empty
    '        txt_fr_ddP.Text = String.Empty
    '        txt_fr_mmP.Text = String.Empty
    '        txt_fr_yyP.Text = String.Empty
    '        txt_tarvdiasP.Text = String.Empty
    '        CB_citaMxP.Checked = String.Empty
    '        CB_citaFxP.Checked = String.Empty
    '        DDL_embarazoP.SelectedValue = String.Empty
    '        txt_retornodiasP.Text = String.Empty
    '        txt_cd4P.Text = String.Empty
    '        txt_cvP.Text = String.Empty
    '        txt_observacionesP.Text = String.Empty
    '    End If
    'End Sub

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

    Sub llenasesquema(ByVal id As String, ByVal ids As String)
        db.Cn1 = cn1
        usuario = Session("usuario").ToString()
        Dim tbE As DataTable = db.ListaSEsquemas(id, usuario)
        If tbE IsNot Nothing Then
            DDL_sesquema.DataSource = tbE
            DDL_sesquema.DataTextField = "SCodigo"
            DDL_sesquema.DataValueField = "IdSEsquema"
            DDL_sesquema.DataBind()
            If ids <> String.Empty Then
                DDL_sesquema.SelectedValue = ids
            End If
            llenacodigo(DDL_sesquema.SelectedValue.ToString())
            DDL_esquemaestatus.Focus()
        Else
            DDL_sesquema.Items.Insert(0, New ListItem("", "0"))
            DDL_esquema.Focus()
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
            'ddl_auto_adherencia.SelectedValue = id
            ddl_auto_adherencia.Items.Insert(0, New ListItem("", "0"))
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
        txt_cant5.Text = ""
        txt_dx5.Text = ""
        txt_uecant5.Text = ""
        txt_cant6.Text = ""
        txt_dx6.Text = ""
        txt_uecant6.Text = ""
        txt_cant7.Text = ""
        txt_dx7.Text = ""
        txt_uecant7.Text = ""
        txt_cant8.Text = ""
        txt_dx8.Text = ""
        txt_uecant8.Text = ""
        Select Case valor
            Case 1
                txt_cant1.Enabled = False
                txt_dx1.Enabled = False
                DDL_fx1.Enabled = False
                txt_uecant1.Enabled = False
                DDL_earv1.Enabled = False
                txt_devcant1.Enabled = False
            Case 2
                txt_cant2.Enabled = False
                txt_dx2.Enabled = False
                DDL_fx2.Enabled = False
                txt_uecant2.Enabled = False
                DDL_earv2.Enabled = False
                txt_devcant2.Enabled = False
            Case 3
                txt_cant3.Enabled = False
                txt_dx3.Enabled = False
                DDL_fx3.Enabled = False
                txt_uecant3.Enabled = False
                DDL_earv3.Enabled = False
                txt_devcant3.Enabled = False
            Case 4
                txt_cant4.Enabled = False
                txt_dx4.Enabled = False
                DDL_fx4.Enabled = False
                txt_uecant4.Enabled = False
                DDL_earv4.Enabled = False
                txt_devcant4.Enabled = False
            Case 5
                txt_cant5.Enabled = False
                txt_dx5.Enabled = False
                DDL_fx5.Enabled = False
                txt_uecant5.Enabled = False
                DDL_earv5.Enabled = False
                txt_devcant5.Enabled = False
            Case 6
                txt_cant6.Enabled = False
                txt_dx6.Enabled = False
                DDL_fx6.Enabled = False
                txt_uecant6.Enabled = False
                DDL_earv6.Enabled = False
                txt_devcant6.Enabled = False
            Case 7
                txt_cant7.Enabled = False
                txt_dx7.Enabled = False
                DDL_fx7.Enabled = False
                txt_uecant7.Enabled = False
                DDL_earv7.Enabled = False
                txt_devcant7.Enabled = False
            Case 8
                txt_cant8.Enabled = False
                txt_dx8.Enabled = False
                DDL_fx8.Enabled = False
                txt_uecant8.Enabled = False
                DDL_earv8.Enabled = False
                txt_devcant8.Enabled = False
            Case 0
                DDL_esquema.Enabled = False
                DDL_sesquema.Enabled = False
                DDL_esquemaestatus.Enabled = False
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
                txt_devcant1.Enabled = False
                txt_devcant2.Enabled = False
                txt_devcant3.Enabled = False
                txt_devcant4.Enabled = False
                txt_devcant5.Enabled = False
                txt_devcant6.Enabled = False
                txt_devcant7.Enabled = False
                txt_devcant8.Enabled = False
                txt_fr_dd.Enabled = False
                txt_fr_mm.Enabled = False
                txt_fr_yy.Enabled = False
                txt_tarvdias.Enabled = False
                CB_citaFx.Enabled = False
                CB_citaMx.Enabled = False
                DDL_embarazo.Enabled = False
                txt_retornodias.Enabled = False
                txt_adherencia.Enabled = False
                txt_observaciones.Enabled = False
                txt_cd4.Enabled = False
                txt_cv.Enabled = False
                DDL_cod1.Enabled = False
                DDL_cod2.Enabled = False
                DDL_cod3.Enabled = False
                DDL_cod4.Enabled = False
                DDL_cod5.Enabled = False
                DDL_cod6.Enabled = False
                DDL_cod7.Enabled = False
                DDL_cod8.Enabled = False
            Case 9
                'txt_cant1P.Enabled = False
                'txt_dx1P.Enabled = False
                'DDL_fx1P.Enabled = False
                'DDL_earv1P.Enabled = False
                'txt_cant2P.Enabled = False
                'txt_dx2P.Enabled = False
                'DDL_fx2P.Enabled = False
                'DDL_earv2P.Enabled = False
                'txt_cant3P.Enabled = False
                'txt_dx3P.Enabled = False
                'DDL_fx3P.Enabled = False
                'DDL_earv3P.Enabled = False
                'txt_cant4P.Enabled = False
                'txt_dx4P.Enabled = False
                'DDL_fx4P.Enabled = False
                'DDL_earv4P.Enabled = False
                'txt_devcant1P.Enabled = False
                'txt_devcant2P.Enabled = False
                'txt_devcant3P.Enabled = False
                'txt_devcant4P.Enabled = False
                'txt_fr_ddP.Enabled = False
                'txt_fr_mmP.Enabled = False
                'txt_fr_yyP.Enabled = False
                'txt_tarvdiasP.Enabled = False
                'CB_citaFxP.Enabled = False
                'CB_citaMxP.Enabled = False
                'DDL_embarazoP.Enabled = False
                'txt_retornodiasP.Enabled = False
                'txt_observacionesP.Enabled = False
                'txt_cd4P.Enabled = False
                'txt_cvP.Enabled = False
                'DDL_cod1P.Enabled = False
                'DDL_cod2P.Enabled = False
                'DDL_cod3P.Enabled = False
                'DDL_cod4P.Enabled = False
        End Select
    End Sub

    Sub setcamposP(ByVal valor As Integer)
        Select Case valor
            Case 1
                txt_dx1P.Enabled = False
                DDL_Via1.Enabled = False
                DDL_fx1P.Enabled = False
                txt_cant1P.Enabled = False
                DDL_t1.Enabled = False
                DDL_Trat1.Visible = False
                DDL_e1.Enabled = False
                txt_tdias1.Enabled = False
                txt_obs1.Enabled = False
            Case 2
                txt_dx2P.Enabled = False
                DDL_Via2.Enabled = False
                DDL_fx2P.Enabled = False
                txt_cant2P.Enabled = False
                DDL_t2.Enabled = False
                DDL_Trat2.Visible = False
                DDL_e2.Enabled = False
                txt_tdias2.Enabled = False
                txt_obs2.Enabled = False
            Case 3
                txt_dx3P.Enabled = False
                DDL_Via3.Enabled = False
                DDL_fx3P.Enabled = False
                txt_cant3P.Enabled = False
                DDL_t3.Enabled = False
                DDL_Trat3.Visible = False
                DDL_e3.Enabled = False
                txt_tdias3.Enabled = False
                txt_obs3.Enabled = False
            Case 4
                txt_dx4P.Enabled = False
                DDL_Via4.Enabled = False
                DDL_fx4P.Enabled = False
                txt_cant4P.Enabled = False
                DDL_t4.Enabled = False
                DDL_Trat4.Visible = False
                DDL_e4.Enabled = False
                txt_tdias4.Enabled = False
                txt_obs4.Enabled = False
            Case 5
                txt_dx5P.Enabled = False
                DDL_Via5.Enabled = False
                DDL_fx5P.Enabled = False
                txt_cant5P.Enabled = False
                DDL_t5.Enabled = False
                DDL_Trat5.Visible = False
                DDL_e5.Enabled = False
                txt_tdias5.Enabled = False
                txt_obs5.Enabled = False
            Case 6
                txt_dx6P.Enabled = False
                DDL_Via6.Enabled = False
                DDL_fx6P.Enabled = False
                txt_cant6P.Enabled = False
                DDL_t6.Enabled = False
                DDL_Trat6.Visible = False
                DDL_e6.Enabled = False
                txt_tdias6.Enabled = False
                txt_obs6.Enabled = False
            Case 0
                txt_CD4P.Enabled = False
                RBL_tipopaciente.Enabled = False
                DDL_cod1P.Enabled = False
                txt_dx1P.Enabled = False
                DDL_Via1.Enabled = False
                DDL_fx1P.Enabled = False
                txt_cant1P.Enabled = False
                DDL_t1.Enabled = False
                DDL_Trat1.Visible = False
                DDL_e1.Enabled = False
                txt_tdias1.Enabled = False
                txt_obs1.Enabled = False
                DDL_cod2P.Enabled = False
                txt_dx2P.Enabled = False
                DDL_Via2.Enabled = False
                DDL_fx2P.Enabled = False
                txt_cant2P.Enabled = False
                DDL_t2.Enabled = False
                DDL_Trat2.Visible = False
                DDL_e2.Enabled = False
                txt_tdias2.Enabled = False
                txt_obs2.Enabled = False
                DDL_cod3P.Enabled = False
                txt_dx3P.Enabled = False
                DDL_Via3.Enabled = False
                DDL_fx3P.Enabled = False
                txt_cant3P.Enabled = False
                DDL_t3.Enabled = False
                DDL_Trat3.Visible = False
                DDL_e3.Enabled = False
                txt_tdias3.Enabled = False
                txt_obs3.Enabled = False
                DDL_cod4P.Enabled = False
                txt_dx4P.Enabled = False
                DDL_Via4.Enabled = False
                DDL_fx4P.Enabled = False
                txt_cant4P.Enabled = False
                DDL_t4.Enabled = False
                DDL_Trat4.Visible = False
                DDL_e4.Enabled = False
                txt_tdias4.Enabled = False
                txt_obs4.Enabled = False
                DDL_cod5P.Enabled = False
                txt_dx5P.Enabled = False
                DDL_Via5.Enabled = False
                DDL_fx5P.Enabled = False
                txt_cant5P.Enabled = False
                DDL_t5.Enabled = False
                DDL_Trat5.Visible = False
                DDL_e5.Enabled = False
                txt_tdias5.Enabled = False
                txt_obs5.Enabled = False
                DDL_cod6P.Enabled = False
                txt_dx6P.Enabled = False
                DDL_Via6.Enabled = False
                DDL_fx6P.Enabled = False
                txt_cant6P.Enabled = False
                DDL_t6.Enabled = False
                DDL_Trat6.Visible = False
                DDL_e6.Enabled = False
                txt_tdias6.Enabled = False
                txt_obs6.Enabled = False
        End Select
    End Sub

    Sub setcamposActivar(ByVal valor As Integer)
        Select Case valor
            Case 0
				lbl_fe.Enabled = True
                DDL_esquema.Enabled = False
                DDL_sesquema.Enabled = False
                DDL_esquemaestatus.Enabled = True
                txt_cant1.Enabled = True
                txt_dx1.Enabled = True
                DDL_fx1.Enabled = True
                txt_uecant1.Enabled = True
                DDL_earv1.Enabled = True
                txt_cant2.Enabled = True
                txt_dx2.Enabled = True
                DDL_fx2.Enabled = True
                txt_uecant2.Enabled = True
                DDL_earv2.Enabled = True
                txt_cant3.Enabled = True
                txt_dx3.Enabled = True
                DDL_fx3.Enabled = True
                txt_uecant3.Enabled = True
                DDL_earv3.Enabled = True
                txt_cant4.Enabled = True
                txt_dx4.Enabled = True
                DDL_fx4.Enabled = True
                txt_uecant4.Enabled = True
                DDL_earv4.Enabled = True
                txt_cant5.Enabled = True
                txt_dx5.Enabled = True
                DDL_fx5.Enabled = True
                txt_uecant5.Enabled = True
                DDL_earv5.Enabled = True
                txt_cant6.Enabled = True
                txt_dx6.Enabled = True
                DDL_fx6.Enabled = True
                txt_uecant6.Enabled = True
                DDL_earv6.Enabled = True
                txt_cant7.Enabled = True
                txt_dx7.Enabled = True
                DDL_fx7.Enabled = True
                txt_uecant7.Enabled = True
                DDL_earv7.Enabled = True
                txt_cant8.Enabled = True
                txt_dx8.Enabled = True
                DDL_fx8.Enabled = True
                txt_uecant8.Enabled = True
                DDL_earv8.Enabled = True
                txt_devcant1.Enabled = True
                txt_devcant2.Enabled = True
                txt_devcant3.Enabled = True
                txt_devcant4.Enabled = True
                txt_devcant5.Enabled = True
                txt_devcant6.Enabled = True
                txt_devcant7.Enabled = True
                txt_devcant8.Enabled = True
                txt_fr_dd.Enabled = True
                txt_fr_mm.Enabled = True
                txt_fr_yy.Enabled = True
                txt_tarvdias.Enabled = True
                CB_citaFx.Enabled = True
                CB_citaMx.Enabled = True
                DDL_embarazo.Enabled = True
                txt_retornodias.Enabled = True
                txt_adherencia.Enabled = True
                txt_observaciones.Enabled = True
                txt_cd4.Enabled = True
                txt_cv.Enabled = True
                DDL_cod1.Enabled = False
                DDL_cod2.Enabled = False
                DDL_cod3.Enabled = False
                DDL_cod4.Enabled = False
                DDL_cod5.Enabled = False
                DDL_cod6.Enabled = False
                DDL_cod7.Enabled = False
                DDL_cod8.Enabled = False
            Case 1
                DDL_esquema.Enabled = False
                DDL_sesquema.Enabled = False
                DDL_esquemaestatus.Enabled = False
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
                txt_devcant1.Enabled = False
                txt_devcant2.Enabled = False
                txt_devcant3.Enabled = False
                txt_devcant4.Enabled = False
                txt_devcant5.Enabled = False
                txt_devcant6.Enabled = False
                txt_devcant7.Enabled = False
                txt_devcant8.Enabled = False
                txt_fr_dd.Enabled = False
                txt_fr_mm.Enabled = False
                txt_fr_yy.Enabled = False
                txt_tarvdias.Enabled = False
                CB_citaFx.Enabled = False
                CB_citaMx.Enabled = False
                DDL_embarazo.Enabled = False
                txt_retornodias.Enabled = False
                txt_adherencia.Enabled = False
                txt_observaciones.Enabled = False
                txt_cd4.Enabled = False
                txt_cv.Enabled = False
                DDL_cod1.Enabled = False
                DDL_cod2.Enabled = False
                DDL_cod3.Enabled = False
                DDL_cod4.Enabled = False
                DDL_cod5.Enabled = False
                DDL_cod6.Enabled = False
                DDL_cod7.Enabled = False
                DDL_cod8.Enabled = False
            Case 9
                'txt_cant1P.Enabled = False
                'txt_dx1P.Enabled = False
                'DDL_fx1P.Enabled = False
                'DDL_earv1P.Enabled = False
                'txt_cant2P.Enabled = False
                'txt_dx2P.Enabled = False
                'DDL_fx2P.Enabled = False
                'DDL_earv2P.Enabled = False
                'txt_cant3P.Enabled = False
                'txt_dx3P.Enabled = False
                'DDL_fx3P.Enabled = False
                'DDL_earv3P.Enabled = False
                'txt_cant4P.Enabled = False
                'txt_dx4P.Enabled = False
                'DDL_fx4P.Enabled = False
                'DDL_earv4P.Enabled = False
                'txt_devcant1P.Enabled = False
                'txt_devcant2P.Enabled = False
                'txt_devcant3P.Enabled = False
                'txt_devcant4P.Enabled = False
                'txt_fr_ddP.Enabled = False
                'txt_fr_mmP.Enabled = False
                'txt_fr_yyP.Enabled = False
                'txt_tarvdiasP.Enabled = False
                'CB_citaFxP.Enabled = False
                'CB_citaMxP.Enabled = False
                'DDL_embarazoP.Enabled = False
                'txt_retornodiasP.Enabled = False
                'txt_observacionesP.Enabled = False
                'txt_cd4P.Enabled = False
                'txt_cvP.Enabled = False
                'DDL_cod1P.Enabled = False
                'DDL_cod2P.Enabled = False
                'DDL_cod3P.Enabled = False
                'DDL_cod4P.Enabled = False
        End Select
    End Sub

    Sub setcamposActivarP(ByVal valor As Integer)
        Select Case valor
            Case 1
				lbl_feP.Enabled = True
                txt_CD4P.Enabled = False
                RBL_tipopaciente.Enabled = False
                DDL_cod1P.Enabled = False
                txt_dx1P.Enabled = False
                DDL_Via1.Enabled = False
                DDL_fx1P.Enabled = False
                txt_cant1P.Enabled = False
                DDL_t1.Enabled = False
                If DDL_t1.SelectedValue = "2" Then
                    DDL_Trat1.Visible = True
                    DDL_Trat1.Enabled = False
                Else
                    DDL_Trat1.Visible = False
                    DDL_Trat1.Enabled = False
                End If
                DDL_e1.Enabled = False
                txt_tdias1.Enabled = False
                txt_obs1.Enabled = False
                DDL_cod2P.Enabled = False
                txt_dx2P.Enabled = False
                DDL_Via2.Enabled = False
                DDL_fx2P.Enabled = False
                txt_cant2P.Enabled = False
                DDL_t2.Enabled = False
                If DDL_t2.SelectedValue = "2" Then
                    DDL_Trat2.Visible = True
                    DDL_Trat2.Enabled = False
                Else
                    DDL_Trat2.Visible = False
                    DDL_Trat2.Enabled = False
                End If
                DDL_e2.Enabled = False
                txt_tdias2.Enabled = False
                txt_obs2.Enabled = False
                DDL_cod3P.Enabled = False
                txt_dx3P.Enabled = False
                DDL_Via3.Enabled = False
                DDL_fx3P.Enabled = False
                txt_cant3P.Enabled = False
                DDL_t3.Enabled = False
                If DDL_t3.SelectedValue = "2" Then
                    DDL_Trat3.Visible = True
                    DDL_Trat3.Enabled = False
                Else
                    DDL_Trat3.Visible = False
                    DDL_Trat3.Enabled = False
                End If
                DDL_e3.Enabled = False
                txt_tdias3.Enabled = False
                txt_obs3.Enabled = False
                DDL_cod4P.Enabled = False
                txt_dx4P.Enabled = False
                DDL_Via4.Enabled = False
                DDL_fx4P.Enabled = False
                txt_cant4P.Enabled = False
                DDL_t4.Enabled = False
                If DDL_t4.SelectedValue = "2" Then
                    DDL_Trat4.Visible = True
                    DDL_Trat4.Enabled = False
                Else
                    DDL_Trat4.Visible = False
                    DDL_Trat4.Enabled = False
                End If
                DDL_e4.Enabled = False
                txt_tdias4.Enabled = False
                txt_obs4.Enabled = False
                DDL_cod5P.Enabled = False
                txt_dx5P.Enabled = False
                DDL_Via5.Enabled = False
                DDL_fx5P.Enabled = False
                txt_cant5P.Enabled = False
                DDL_t5.Enabled = False
                If DDL_t5.SelectedValue = "2" Then
                    DDL_Trat5.Visible = True
                    DDL_Trat5.Enabled = False
                Else
                    DDL_Trat5.Visible = False
                    DDL_Trat5.Enabled = False
                End If
                DDL_e5.Enabled = False
                txt_tdias5.Enabled = False
                txt_obs5.Enabled = False
                DDL_cod6P.Enabled = False
                txt_dx6P.Enabled = False
                DDL_Via6.Enabled = False
                DDL_fx6P.Enabled = False
                txt_cant6P.Enabled = False
                DDL_t6.Enabled = False
                If DDL_t6.SelectedValue = "2" Then
                    DDL_Trat6.Visible = True
                    DDL_Trat6.Enabled = False
                Else
                    DDL_Trat6.Visible = False
                    DDL_Trat6.Enabled = False
                End If
                DDL_e6.Enabled = False
                txt_tdias6.Enabled = False
                txt_obs6.Enabled = False
            Case 0
                txt_CD4P.Enabled = True
                RBL_tipopaciente.Enabled = True
                DDL_cod1P.Enabled = True
                txt_dx1P.Enabled = True
                DDL_Via1.Enabled = True
                DDL_fx1P.Enabled = True
                txt_cant1P.Enabled = True
                DDL_t1.Enabled = True
                If DDL_t1.SelectedValue = "2" Then
                    DDL_Trat1.Visible = True
                    DDL_Trat1.Enabled = True
                Else
                    DDL_Trat1.Visible = False
                    DDL_Trat1.Enabled = False
                End If
                DDL_e1.Enabled = True
                txt_tdias1.Enabled = True
                txt_obs1.Enabled = True
                DDL_cod2P.Enabled = True
                txt_dx2P.Enabled = True
                DDL_Via2.Enabled = True
                DDL_fx2P.Enabled = True
                txt_cant2P.Enabled = True
                DDL_t2.Enabled = True
                If DDL_t2.SelectedValue = "2" Then
                    DDL_Trat2.Visible = True
                    DDL_Trat2.Enabled = True
                Else
                    DDL_Trat2.Visible = False
                    DDL_Trat2.Enabled = False
                End If
                DDL_e2.Enabled = True
                txt_tdias2.Enabled = True
                txt_obs2.Enabled = True
                DDL_cod3P.Enabled = True
                txt_dx3P.Enabled = True
                DDL_Via3.Enabled = True
                DDL_fx3P.Enabled = True
                txt_cant3P.Enabled = True
                DDL_t3.Enabled = True
                If DDL_t3.SelectedValue = "2" Then
                    DDL_Trat3.Visible = True
                    DDL_Trat3.Enabled = True
                Else
                    DDL_Trat3.Visible = False
                    DDL_Trat3.Enabled = False
                End If
                DDL_e3.Enabled = True
                txt_tdias3.Enabled = True
                txt_obs3.Enabled = True
                DDL_cod4P.Enabled = True
                txt_dx4P.Enabled = True
                DDL_Via4.Enabled = True
                DDL_fx4P.Enabled = True
                txt_cant4P.Enabled = True
                DDL_t4.Enabled = True
                If DDL_t4.SelectedValue = "2" Then
                    DDL_Trat4.Visible = True
                    DDL_Trat4.Enabled = True
                Else
                    DDL_Trat4.Visible = False
                    DDL_Trat4.Enabled = False
                End If
                DDL_e4.Enabled = True
                txt_tdias4.Enabled = True
                txt_obs4.Enabled = True
                DDL_cod5P.Enabled = True
                txt_dx5P.Enabled = True
                DDL_Via5.Enabled = True
                DDL_fx5P.Enabled = True
                txt_cant5P.Enabled = True
                DDL_t5.Enabled = True
                If DDL_t5.SelectedValue = "2" Then
                    DDL_Trat5.Visible = True
                    DDL_Trat5.Enabled = True
                Else
                    DDL_Trat5.Visible = False
                    DDL_Trat5.Enabled = False
                End If
                DDL_e5.Enabled = True
                txt_tdias5.Enabled = True
                txt_obs5.Enabled = True
                DDL_cod6P.Enabled = True
                txt_dx6P.Enabled = True
                DDL_Via6.Enabled = True
                DDL_fx6P.Enabled = True
                txt_cant6P.Enabled = True
                DDL_t6.Enabled = True
                If DDL_t6.SelectedValue = "2" Then
                    DDL_Trat6.Visible = True
                    DDL_Trat6.Enabled = True
                Else
                    DDL_Trat6.Visible = False
                    DDL_Trat6.Enabled = False
                End If
                DDL_e6.Enabled = True
                txt_tdias6.Enabled = True
                txt_obs6.Enabled = True
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
                lbl_asi.Text = nhc
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
                lbl_asi.Text = nhc
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
    End Sub

    Function tip(ByVal estatus As String, ByVal cantidad As String) As String
        Return "Estatus: " & estatus & " - Cantidad: " & cantidad
    End Function

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

    Sub llenacodigoP()
        db.Cn1 = cn1
        usuario = Session("usuario").ToString()
        Dim tbMed As DataTable = db.ObtieneARVMedicamento("2", usuario)
        If tbMed IsNot Nothing Then
            DDL_cod1P.DataSource = tbMed
            DDL_cod1P.DataTextField = "Codigo"
            DDL_cod1P.DataValueField = "IdFFProf"
            DDL_cod1P.DataBind()
            DDL_cod1P.Items.Insert(0, New ListItem("", "0"))
            DDL_cod2P.DataSource = tbMed
            DDL_cod2P.DataTextField = "Codigo"
            DDL_cod2P.DataValueField = "IdFFProf"
            DDL_cod2P.DataBind()
            DDL_cod2P.Items.Insert(0, New ListItem("", "0"))
            DDL_cod3P.DataSource = tbMed
            DDL_cod3P.DataTextField = "Codigo"
            DDL_cod3P.DataValueField = "IdFFProf"
            DDL_cod3P.DataBind()
            DDL_cod3P.Items.Insert(0, New ListItem("", "0"))
            DDL_cod4P.DataSource = tbMed
            DDL_cod4P.DataTextField = "Codigo"
            DDL_cod4P.DataValueField = "IdFFProf"
            DDL_cod4P.DataBind()
            DDL_cod4P.Items.Insert(0, New ListItem("", "0"))
            DDL_cod5P.DataSource = tbMed
            DDL_cod5P.DataTextField = "Codigo"
            DDL_cod5P.DataValueField = "IdFFProf"
            DDL_cod5P.DataBind()
            DDL_cod5P.Items.Insert(0, New ListItem("", "0"))
            DDL_cod6P.DataSource = tbMed
            DDL_cod6P.DataTextField = "Codigo"
            DDL_cod6P.DataValueField = "IdFFProf"
            DDL_cod6P.DataBind()
            DDL_cod6P.Items.Insert(0, New ListItem("", "0"))
        End If
    End Sub

    'Sub llenacodigoP()
    '    db.Cn1 = cn1
    '    usuario = Session("usuario").ToString()
    '    Dim tbMed As DataTable = db.ObtieneARVMedicamento("2", usuario)
    '    If tbMed IsNot Nothing Then
    '        DDL_cod1P.DataSource = tbMed
    '        DDL_cod1P.DataTextField = "Codigo"
    '        DDL_cod1P.DataValueField = "IdFFProf"
    '        DDL_cod1P.DataBind()
    '        DDL_cod1P.Items.Insert(0, New ListItem("", "0"))
    '        DDL_cod2P.DataSource = tbMed
    '        DDL_cod2P.DataTextField = "Codigo"
    '        DDL_cod2P.DataValueField = "IdFFProf"
    '        DDL_cod2P.DataBind()
    '        DDL_cod2P.Items.Insert(0, New ListItem("", "0"))
    '        DDL_cod3P.DataSource = tbMed
    '        DDL_cod3P.DataTextField = "Codigo"
    '        DDL_cod3P.DataValueField = "IdFFProf"
    '        DDL_cod3P.DataBind()
    '        DDL_cod3P.Items.Insert(0, New ListItem("", "0"))
    '        DDL_cod4P.DataSource = tbMed
    '        DDL_cod4P.DataTextField = "Codigo"
    '        DDL_cod4P.DataValueField = "IdFFProf"
    '        DDL_cod4P.DataBind()
    '        DDL_cod4P.Items.Insert(0, New ListItem("", "0"))
    '    End If
    'End Sub

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

    'Sub llenafrecuenciaP()
    '    db.Cn1 = cn1
    '    usuario = Session("usuario").ToString()
    '    Dim tbFx As DataTable = db.ObtieneFrecuenia(usuario)
    '    If tbFx IsNot Nothing Then
    '        DDL_fx1P.DataSource = tbFx
    '        DDL_fx1P.DataTextField = "IdFrecuencia"
    '        DDL_fx1P.DataValueField = "IdFrecuencia"
    '        DDL_fx1P.DataBind()
    '        DDL_fx1P.Items.Insert(0, New ListItem("", "0"))
    '        DDL_fx2P.DataSource = tbFx
    '        DDL_fx2P.DataTextField = "IdFrecuencia"
    '        DDL_fx2P.DataValueField = "IdFrecuencia"
    '        DDL_fx2P.DataBind()
    '        DDL_fx2P.Items.Insert(0, New ListItem("", "0"))
    '        DDL_fx3P.DataSource = tbFx
    '        DDL_fx3P.DataTextField = "IdFrecuencia"
    '        DDL_fx3P.DataValueField = "IdFrecuencia"
    '        DDL_fx3P.DataBind()
    '        DDL_fx3P.Items.Insert(0, New ListItem("", "0"))
    '        DDL_fx4P.DataSource = tbFx
    '        DDL_fx4P.DataTextField = "IdFrecuencia"
    '        DDL_fx4P.DataValueField = "IdFrecuencia"
    '        DDL_fx4P.DataBind()
    '        DDL_fx4P.Items.Insert(0, New ListItem("", "0"))
    '    End If
    'End Sub

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

    'Sub llenaEstatusP()
    '    db.Cn1 = cn1
    '    usuario = Session("usuario").ToString()
    '    Dim tbE As DataTable = db.ObtieneEstatus(usuario)
    '    If tbE IsNot Nothing Then
    '        DDL_earv1P.DataSource = tbE
    '        DDL_earv1P.DataTextField = "Codigo"
    '        DDL_earv1P.DataValueField = "IdEstatus"
    '        DDL_earv1P.DataBind()
    '        DDL_earv1P.Items.Insert(0, New ListItem("", "0"))
    '        DDL_earv2P.DataSource = tbE
    '        DDL_earv2P.DataTextField = "Codigo"
    '        DDL_earv2P.DataValueField = "IdEstatus"
    '        DDL_earv2P.DataBind()
    '        DDL_earv2P.Items.Insert(0, New ListItem("", "0"))
    '        DDL_earv3P.DataSource = tbE
    '        DDL_earv3P.DataTextField = "Codigo"
    '        DDL_earv3P.DataValueField = "IdEstatus"
    '        DDL_earv3P.DataBind()
    '        DDL_earv3P.Items.Insert(0, New ListItem("", "0"))
    '        DDL_earv4P.DataSource = tbE
    '        DDL_earv4P.DataTextField = "Codigo"
    '        DDL_earv4P.DataValueField = "IdEstatus"
    '        DDL_earv4P.DataBind()
    '        DDL_earv4P.Items.Insert(0, New ListItem("", "0"))
    '    End If
    'End Sub

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

    'Sub llenaEmbarazoP()
    '    db.Cn1 = cn1
    '    Dim tbEmb As DataTable = db.ObtieneEmbarazo(usuario)
    '    If tbEmb IsNot Nothing Then
    '        DDL_embarazoP.DataSource = tbEmb
    '        DDL_embarazoP.DataTextField = "IdEmbarazo"
    '        DDL_embarazoP.DataValueField = "IdEmbarazo"
    '        DDL_embarazoP.DataBind()
    '        DDL_embarazoP.Items.Insert(0, New ListItem("", "0"))
    '    End If
    'End Sub

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

    Protected Sub btn_grabar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_grabar.Click
        Try
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
            usuario = Session("usuario").ToString()
            datos = lbl_id.Text.ToString() + "|"
            datos += str(txt_cant1.Text.ToString()) + "|" + txt_dx1.Text.ToString + "|" + DDL_fx1.SelectedValue.ToString() + "|" + str(txt_uecant1.Text.ToString()) + "|" + str(txt_devcant1.Text.ToString()) + "|"
            datos += str(txt_cant2.Text.ToString()) + "|" + txt_dx2.Text.ToString() + "|" + DDL_fx2.SelectedValue.ToString() + "|" + str(txt_uecant2.Text.ToString()) + "|" + str(txt_devcant2.Text.ToString()) + "|"
            datos += str(txt_cant3.Text.ToString()) + "|" + txt_dx3.Text.ToString() + "|" + DDL_fx3.SelectedValue.ToString() + "|" + str(txt_uecant3.Text.ToString()) + "|" + str(txt_devcant3.Text.ToString()) + "|"
            datos += str(txt_cant4.Text.ToString()) + "|" + txt_dx4.Text.ToString() + "|" + DDL_fx4.SelectedValue.ToString() + "|" + str(txt_uecant4.Text.ToString()) + "|" + str(txt_devcant4.Text.ToString()) + "|"
            datos += str(txt_cant5.Text.ToString()) + "|" + txt_dx5.Text.ToString() + "|" + DDL_fx5.SelectedValue.ToString() + "|" + str(txt_uecant5.Text.ToString()) + "|" + str(txt_devcant5.Text.ToString()) + "|"
            datos += str(txt_cant6.Text.ToString()) + "|" + txt_dx6.Text.ToString() + "|" + DDL_fx6.SelectedValue.ToString() + "|" + str(txt_uecant6.Text.ToString()) + "|" + str(txt_devcant6.Text.ToString()) + "|"
            datos += str(txt_cant7.Text.ToString()) + "|" + txt_dx7.Text.ToString() + "|" + DDL_fx7.SelectedValue.ToString() + "|" + str(txt_uecant7.Text.ToString()) + "|" + str(txt_devcant7.Text.ToString()) + "|"
            datos += str(txt_cant8.Text.ToString()) + "|" + txt_dx8.Text.ToString() + "|" + DDL_fx8.SelectedValue.ToString() + "|" + str(txt_uecant8.Text.ToString()) + "|" + str(txt_devcant8.Text.ToString()) + "|"
            datos += FechaRetorno + "|" + str(txt_tarvdias.Text.ToString()) + "|" + CitaMx + "|" + CitaFx + "|" + DDL_embarazo.SelectedValue.ToString() + "|"
            datos += str(txt_retornodias.Text.ToString()) + "|" + str(txt_adherencia.Text.ToString()) + "|" + txt_cd4.Text.ToString() + "|" + txt_cv.Text.ToString() + "|" + txt_observaciones.Text.ToString() + "|" + ddl_auto_adherencia.SelectedValue.ToString()
            db.Cn1 = cn1

            '*/Modifica/actualiza exsistencias ARV
            '*/ Revisa Código 1 cantidad entrega para actualizar existencia
            If DDL_cod1.SelectedValue IsNot Nothing Then
                Dim tipo_ingreso_med As String = 1
                Dim producto As String = DDL_cod1.SelectedValue.ToString()
                Dim qty_ingreso As String = lbl_cantidad1.Text.ToString()
                Dim qty_salida As String = txt_cant1.Text.ToString()
                Dim tipo_mov As String = 3
                db.Update_Existencia_Egreso(tipo_ingreso_med, FechaRetorno, producto, qty_ingreso, qty_salida, tipo_mov, usuario, 0, 0, "01/01/1900")
            End If

            '*/ Revisa Código 2 cantidad entrega para actualizar existencia
            If DDL_cod2.SelectedValue IsNot Nothing Then
                Dim tipo_ingreso_med As String = 1
                Dim producto As String = DDL_cod2.SelectedValue.ToString()
                Dim qty_ingreso As String = lbl_cantidad2.Text.ToString()
                Dim qty_salida As String = txt_cant2.Text.ToString()
                Dim tipo_mov As String = 3
                db.Update_Existencia_Egreso(tipo_ingreso_med, FechaRetorno, producto, qty_ingreso, qty_salida, tipo_mov, usuario, 0, 0, "01/01/1900")
            End If

            '*/ Revisa Código 3 cantidad entrega para actualizar existencia
            If DDL_cod3.SelectedValue IsNot Nothing Then
                Dim tipo_ingreso_med As String = 1
                Dim producto As String = DDL_cod3.SelectedValue.ToString()
                Dim qty_ingreso As String = lbl_cantidad3.Text.ToString()
                Dim qty_salida As String = txt_cant3.Text.ToString()
                Dim tipo_mov As String = 3
                db.Update_Existencia_Egreso(tipo_ingreso_med, FechaRetorno, producto, qty_ingreso, qty_salida, tipo_mov, usuario, 0, 0, "01/01/1900")
            End If

            '*/ Revisa Código 4 cantidad entrega para actualizar existencia
            If DDL_cod4.SelectedValue IsNot Nothing Then
                Dim tipo_ingreso_med As String = 1
                Dim producto As String = DDL_cod4.SelectedValue.ToString()
                Dim qty_ingreso As String = lbl_cantidad4.Text.ToString()
                Dim qty_salida As String = txt_cant4.Text.ToString()
                Dim tipo_mov As String = 3
                db.Update_Existencia_Egreso(tipo_ingreso_med, FechaRetorno, producto, qty_ingreso, qty_salida, tipo_mov, usuario, 0, 0, "01/01/1900")
            End If

            '*/ Revisa Código 5 cantidad entrega para actualizar existencia
            If DDL_cod5.SelectedValue IsNot Nothing Then
                Dim tipo_ingreso_med As String = 1
                Dim producto As String = DDL_cod5.SelectedValue.ToString()
                Dim qty_ingreso As String = lbl_cantidad5.Text.ToString()
                Dim qty_salida As String = txt_cant5.Text.ToString()
                Dim tipo_mov As String = 3
                db.Update_Existencia_Egreso(tipo_ingreso_med, FechaRetorno, producto, qty_ingreso, qty_salida, tipo_mov, usuario, 0, 0, "01/01/1900")
            End If

            '*/ Revisa Código 6 cantidad entrega para actualizar existencia
            If DDL_cod6.SelectedValue IsNot Nothing Then
                Dim tipo_ingreso_med As String = 1
                Dim producto As String = DDL_cod6.SelectedValue.ToString()
                Dim qty_ingreso As String = lbl_cantidad6.Text.ToString()
                Dim qty_salida As String = txt_cant6.Text.ToString()
                Dim tipo_mov As String = 3
                db.Update_Existencia_Egreso(tipo_ingreso_med, FechaRetorno, producto, qty_ingreso, qty_salida, tipo_mov, usuario, 0, 0, "01/01/1900")
            End If

            '*/ Revisa Código 7 cantidad entrega para actualizar existencia
            If DDL_cod7.SelectedValue IsNot Nothing Then
                Dim tipo_ingreso_med As String = 1
                Dim producto As String = DDL_cod7.SelectedValue.ToString()
                Dim qty_ingreso As String = lbl_cantidad7.Text.ToString()
                Dim qty_salida As String = txt_cant7.Text.ToString()
                Dim tipo_mov As String = 3
                db.Update_Existencia_Egreso(tipo_ingreso_med, FechaRetorno, producto, qty_ingreso, qty_salida, tipo_mov, usuario, 0, 0, "01/01/1900")
            End If

            '*/ Revisa Código 8 cantidad entrega para actualizar existencia
            If DDL_cod8.SelectedValue IsNot Nothing Then
                Dim tipo_ingreso_med As String = 1
                Dim producto As String = DDL_cod8.SelectedValue.ToString()
                Dim qty_ingreso As String = lbl_cantidad8.Text.ToString()
                Dim qty_salida As String = txt_cant8.Text.ToString()
                Dim tipo_mov As String = 3
                db.Update_Existencia_Egreso(tipo_ingreso_med, FechaRetorno, producto, qty_ingreso, qty_salida, tipo_mov, usuario, 0, 0, "01/01/1900")
            End If


            db.ActualizaControlARV(datos, usuario)
            divARV.Visible = True
            divProf.Visible = False
            Llena_regPac(Request.QueryString("nhc").ToUpper())
            UP_registros.Update()
            MPERegistro.Show()
            setcamposActivar(1)
            btn_editar.Visible = True
            btn_grabar.Visible = False
            btn_cancelar.Visible = False
        Catch ex As Exception
            lbl_error.Text = ex.Message
        End Try

        'Procedimiento actualiza esquema
        actualizaMedicamento()

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

    Protected Sub GV_regPacA_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GV_regPacA.RowCommand
        If e.CommandName = "Editar" Then
            Try
                Dim gv As GridView = DirectCast(sender, GridView)
                Dim rowIndex As Int32 = Convert.ToInt32(e.CommandArgument.ToString())
                Dim id As String = gv.DataKeys(rowIndex)(0).ToString()
                Dim nhc As String = gv.DataKeys(rowIndex)(1).ToString()
                'Se almacena valor de variable para actualizar en el procedimiento actualizaMedicamento
                lblIdCCARV.Text = id
                setcampos(0)
                llenafrecuencia()
                llenaEstatus()
                llenaEmbarazo()
                llenaesquema()
                llena_autoadhe()
                llenaregistro(id)
                divARV.Visible = True
                divProf.Visible = False
                btn_grabar.Visible = False
                rol = Convert.ToString(Session("pusuario"))
                edicion = Convert.ToBoolean(Convert.ToInt32(Session("edicion").ToString()))
                Select Case rol
                    Case "1", "2", "3" 'Master, Administrador, Digitador
                        btn_editar.Visible = True
                    Case "4", "5" 'Consulta, Reportes
                        btn_editar.Visible = False
                    Case "6" 'Supervisor
                        If edicion Then
                            btn_editar.Visible = True
                        Else
                            btn_editar.Visible = False
                        End If
                End Select
                btn_cancelar.Visible = False
                MPERegistro.Show()
            Catch ex As Exception
                lbl_error.Text = ex.Message
            End Try

        ElseIf e.CommandName = "Eliminar" Then


            Try
                Dim gv As GridView = DirectCast(sender, GridView)
                Dim rowIndex As Int32 = Convert.ToInt32(e.CommandArgument.ToString())
                Dim IdCCARV As String = gv.DataKeys(rowIndex)(0).ToString()
                Dim nhc As String = gv.DataKeys(rowIndex)(1).ToString()

                rol = Convert.ToString(Session("pusuario"))
                usuario = Session("usuario").ToString()

                db.Cn1 = cn1

                Select Case rol
                    Case "1", "2", "6" 'Master, Administrador, Supervisor
                        Dim x = db.Eliminar_RegistroARV(IdCCARV, usuario)
                        Dim xarv As String() = x.Split("|")

                        If xarv(0).ToString() = "True" Then
                            Response.Redirect("~/consultaReg.aspx?nhc=" + nhc, False)
                        Else
                            lbl_error.Text = "Error al eliminar registro"
                        End If

                    Case Else
                        Response.Redirect("~/acceso.aspx", False)
                End Select


            Catch ex As Exception
                lbl_error.Text = ex.Message
            End Try


        End If
    End Sub

    Protected Sub GV_regPacA_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles GV_regPacA.PreRender
        Dim n As Integer = 0
        For Each nrow As GridViewRow In GV_regPacA.Rows
            For columnIndex As Integer = n To Convert.ToInt32(GV_regPacA.Rows.Count)
                Dim irow1 As ImageButton = DirectCast(nrow.FindControl("IB_editar"), ImageButton)
                irow1.CommandArgument = Convert.ToString(n)

                Dim irow2 As ImageButton = DirectCast(nrow.FindControl("IB_eliminar"), ImageButton)
                irow2.CommandArgument = Convert.ToString(n)
            Next
            n += 1
        Next
    End Sub



    Protected Sub GV_regPacP_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GV_regPacP.RowCommand
        If e.CommandName = "Editar" Then
            Try
                Dim gv As GridView = DirectCast(sender, GridView)
                Dim rowIndex As Int32 = Convert.ToInt32(e.CommandArgument.ToString())
                Dim id As String = gv.DataKeys(rowIndex)(0).ToString()
                Dim nhc As String = gv.DataKeys(rowIndex)(1).ToString()
                'Response.Redirect("~/ConsultaARV.aspx?id=" + id + "&nhc=" + nhc, False)
                setcamposP(0)
                llenacodigoP()
                llenaVIAP()
                llenafrecuenciaP()
                llenaEstatusP()
                llenaregistroP(id)

                divARV.Visible = False
                divProf.Visible = True
                btn_grabarP.Visible = False
                rol = Convert.ToString(Session("pusuario"))
                edicion = Convert.ToBoolean(Convert.ToInt32(Session("edicion").ToString()))
                Select Case rol
                    Case "1", "2", "3" 'Master, Administrador, Digitador
                        btn_editarP.Visible = True
                    Case "4", "5" 'Consulta, Reportes
                        btn_editarP.Visible = False
                    Case "6" 'Supervisor
                        If edicion Then
                            btn_editarP.Visible = True
                        Else
                            btn_editarP.Visible = False
                        End If
                End Select
                btn_cancelarP.Visible = False
                MPERegistro.Show()
            Catch ex As Exception
                lbl_error.Text = ex.Message
            End Try

        ElseIf e.CommandName = "Eliminar" Then

            Try
                Dim gv As GridView = DirectCast(sender, GridView)
                Dim rowIndex As Int32 = Convert.ToInt32(e.CommandArgument.ToString())
                Dim IdCCPROF As String = gv.DataKeys(rowIndex)(0).ToString()
                Dim nhc As String = gv.DataKeys(rowIndex)(1).ToString()

                rol = Convert.ToString(Session("pusuario"))
                usuario = Session("usuario").ToString()

                db.Cn1 = cn1

                Select Case rol
                    Case "1", "2", "6" 'Master, Administrador, Supervisor
                        Dim x = db.Eliminar_RegistroPROF(IdCCPROF, usuario)
                        Dim xprof As String() = x.Split("|")

                        If xprof(0).ToString() = "True" Then
                            Response.Redirect("~/consultaReg.aspx?nhc=" + nhc, False)
                        Else
                            lbl_error.Text = "Error al eliminar registro"
                        End If

                    Case Else
                        Response.Redirect("~/acceso.aspx", False)
                End Select


            Catch ex As Exception
                lbl_error.Text = ex.Message
            End Try


        End If
    End Sub

    Protected Sub GV_regPacP_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles GV_regPacP.PreRender
        Dim n As Integer = 0
        For Each nrow As GridViewRow In GV_regPacP.Rows
            For columnIndex As Integer = n To Convert.ToInt32(GV_regPacP.Rows.Count)
                Dim irow1 As ImageButton = DirectCast(nrow.FindControl("IB_editar"), ImageButton)
                irow1.CommandArgument = Convert.ToString(n)

                Dim irow2 As ImageButton = DirectCast(nrow.FindControl("IB_eliminar"), ImageButton)
                irow2.CommandArgument = Convert.ToString(n)
            Next
            n += 1
        Next
    End Sub

    Sub actualizaMedicamento()

        'Se relaciona sqlConnection con variable conn strin
        Dim cn As SqlConnection = New SqlConnection(cn1)

        'Se manda el nombre del procedimiento + cn
        Dim cmd As SqlCommand = New SqlCommand("ActualizaMedicamento", cn)

        'Se estable la propiedad para el procemiento almacenado
        cmd.CommandType = System.Data.CommandType.StoredProcedure

        'Se manda el listado de parametros al procedimiento
        cmd.Parameters.Add("@IdCCARV", SqlDbType.VarChar).Value = lblIdCCARV.Text
		cmd.Parameters.Add("@FechaEntrega", SqlDbType.VarChar).Value = lbl_fe.Text
		'Esque
        cmd.Parameters.Add("@EsquemaEstatus", SqlDbType.Int).Value = DDL_esquemaestatus.SelectedValue

        'Medicamento 1       
        If DDL_earv1.SelectedValue = "" Then
            cmd.Parameters.Add("@Med1ARVSta", SqlDbType.Int).Value = DBNull.Value
        Else
            cmd.Parameters.Add("@Med1ARVSta", SqlDbType.Int).Value = DDL_earv1.SelectedValue
        End If       
        'Medicamento 2       
        If DDL_earv2.SelectedValue = "" Then
            cmd.Parameters.Add("@Med2ARVSta", SqlDbType.Int).Value = DBNull.Value
        Else
            cmd.Parameters.Add("@Med2ARVSta", SqlDbType.Int).Value = DDL_earv2.SelectedValue
        End If       
        'Medicamento 3       
        If DDL_earv3.SelectedValue = "" Then
            cmd.Parameters.Add("@Med3ARVSta", SqlDbType.Int).Value = DBNull.Value
        Else
            cmd.Parameters.Add("@Med3ARVSta", SqlDbType.Int).Value = DDL_earv3.SelectedValue
        End If       
        'Medicamento 4        
        If DDL_earv4.SelectedValue = "" Then
            cmd.Parameters.Add("@Med4ARVSta", SqlDbType.Int).Value = DBNull.Value
        Else
            cmd.Parameters.Add("@Med4ARVSta", SqlDbType.Int).Value = DDL_earv4.SelectedValue
        End If        
        'Medicamento 5       
        If DDL_earv5.SelectedValue = "" Then
            cmd.Parameters.Add("@Med5ARVSta", SqlDbType.Int).Value = DBNull.Value
        Else
            cmd.Parameters.Add("@Med5ARVSta", SqlDbType.Int).Value = DDL_earv5.SelectedValue
        End If       
        'Medicamento 6       
        If DDL_earv6.SelectedValue = "" Then
            cmd.Parameters.Add("@Med6ARVSta", SqlDbType.Int).Value = DBNull.Value
        Else
            cmd.Parameters.Add("@Med6ARVSta", SqlDbType.Int).Value = DDL_earv6.SelectedValue
        End If        
        'Medicamento 7
        If DDL_earv7.SelectedValue = "" Then
            cmd.Parameters.Add("@Med7ARVSta", SqlDbType.Int).Value = DBNull.Value
        Else
            cmd.Parameters.Add("@Med7ARVSta", SqlDbType.Int).Value = DDL_earv7.SelectedValue
        End If        
        'Medicamento 8
        If DDL_earv8.SelectedValue = "" Then
            cmd.Parameters.Add("@Med8ARVSta", SqlDbType.Int).Value = DBNull.Value
        Else
            cmd.Parameters.Add("@Med8ARVSta", SqlDbType.Int).Value = DDL_earv8.SelectedValue
        End If       

        'Se inicia la validacion para ejecutar el procedimiento
        Try
            'Se abre la conecion
            cn.Open()
            'Se ejecuta el procedimiento
            cmd.ExecuteNonQuery()
        Catch ex As Exception
        Finally
            cn.Close()
        End Try
    End Sub



    Sub llenafrecuenciaP()
        db.Cn1 = cn1
        usuario = Session("usuario").ToString()
        Dim tbFx As DataTable = db.ObtieneFrecuenia(usuario)
        If tbFx IsNot Nothing Then
            DDL_fx1P.DataSource = tbFx
            DDL_fx1P.DataTextField = "IdFrecuencia"
            DDL_fx1P.DataValueField = "IdFrecuencia"
            DDL_fx1P.DataBind()
            DDL_fx1P.Items.Insert(0, New ListItem("", "0"))
            DDL_fx2P.DataSource = tbFx
            DDL_fx2P.DataTextField = "IdFrecuencia"
            DDL_fx2P.DataValueField = "IdFrecuencia"
            DDL_fx2P.DataBind()
            DDL_fx2P.Items.Insert(0, New ListItem("", "0"))
            DDL_fx3P.DataSource = tbFx
            DDL_fx3P.DataTextField = "IdFrecuencia"
            DDL_fx3P.DataValueField = "IdFrecuencia"
            DDL_fx3P.DataBind()
            DDL_fx3P.Items.Insert(0, New ListItem("", "0"))
            DDL_fx4P.DataSource = tbFx
            DDL_fx4P.DataTextField = "IdFrecuencia"
            DDL_fx4P.DataValueField = "IdFrecuencia"
            DDL_fx4P.DataBind()
            DDL_fx4P.Items.Insert(0, New ListItem("", "0"))
            DDL_fx5P.DataSource = tbFx
            DDL_fx5P.DataTextField = "IdFrecuencia"
            DDL_fx5P.DataValueField = "IdFrecuencia"
            DDL_fx5P.DataBind()
            DDL_fx5P.Items.Insert(0, New ListItem("", "0"))
            DDL_fx6P.DataSource = tbFx
            DDL_fx6P.DataTextField = "IdFrecuencia"
            DDL_fx6P.DataValueField = "IdFrecuencia"
            DDL_fx6P.DataBind()
            DDL_fx6P.Items.Insert(0, New ListItem("", "0"))
        End If
    End Sub

    Sub llenaEstatusP()
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

    Sub llenaVIAP()
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

    Protected Sub btn_editar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_editar.Click
        ddl_auto_adherencia.Enabled = True
        divARV.Visible = True
        divProf.Visible = False
        MPERegistro.Show()


        setcamposActivar(0)
        btn_editar.Visible = False
        btn_grabar.Visible = True
        btn_cancelar.Visible = True
    End Sub

    Protected Sub btn_editarP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_editarP.Click
        ddl_auto_adherencia.Enabled = True
        divARV.Visible = False
        divProf.Visible = True
        MPERegistro.Show()
		lbl_feP.Enabled = True
		DDL_cod1P.Enabled = True
        DDL_cod2P.Enabled = True
        DDL_cod3P.Enabled = True
        DDL_cod4P.Enabled = True
        DDL_cod5P.Enabled = True
        DDL_cod6P.Enabled = True
        setcamposActivarP(0)
        btn_editarP.Visible = False
        btn_grabarP.Visible = True
        btn_cancelarP.Visible = True
    End Sub

    Protected Sub btn_cancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_cancelar.Click
        divARV.Visible = True
        divProf.Visible = False
        MPERegistro.Show()
        setcamposActivar(1)
		lbl_fe.Enabled = False
        btn_editar.Visible = True
        btn_grabar.Visible = False
        btn_cancelar.Visible = False
    End Sub

    Protected Sub btn_cancelarP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_cancelarP.Click
        divARV.Visible = False
        divProf.Visible = True
        MPERegistro.Show()
        setcamposActivarP(1)
		lbl_feP.Enabled = False
        btn_editarP.Visible = True
        btn_grabarP.Visible = False
        btn_cancelarP.Visible = False
    End Sub

    Protected Sub btn_cerrar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_cerrar.Click
        Llena_regPac(Request.QueryString("nhc").ToUpper())
    End Sub

    Protected Sub btn_cerrarP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_cerrarP.Click
        Llena_regPac(Request.QueryString("nhc").ToUpper())
    End Sub

    Protected Sub btn_grabarP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_grabarP.Click
        Dim FechaSistema As String = DateTime.Now.ToString("dd/MM/yyyy")
        Try
            usuario = Session("usuario").ToString()
            datos = lbl_idP.Text.ToString() + "|" + txt_CD4P.Text.ToString() + "|" + RBL_tipopaciente.SelectedValue.ToString() + "|"
            datos += str(txt_cant1P.Text.ToString()) + "|" + txt_dx1P.Text.ToString() + "|" + DDL_Via1.SelectedValue.ToString() + "|" + DDL_fx1P.SelectedValue.ToString() + "|" + str2(DDL_cod1P.SelectedValue.ToString(), DDL_t1.SelectedValue.ToString()) + "|" + str3(DDL_t1.SelectedValue.ToString(), DDL_Trat1.SelectedValue.ToString()) + "|" + DDL_e1.SelectedValue.ToString() + "|" + str(txt_tdias1.Text.ToString()) + "|" + txt_obs1.Text.ToString() + "|"
            datos += str(txt_cant2P.Text.ToString()) + "|" + txt_dx2P.Text.ToString() + "|" + DDL_Via2.SelectedValue.ToString() + "|" + DDL_fx2P.SelectedValue.ToString() + "|" + str2(DDL_cod2P.SelectedValue.ToString(), DDL_t2.SelectedValue.ToString()) + "|" + str3(DDL_t2.SelectedValue.ToString(), DDL_Trat2.SelectedValue.ToString()) + "|" + DDL_e2.SelectedValue.ToString() + "|" + str(txt_tdias2.Text.ToString()) + "|" + txt_obs2.Text.ToString() + "|"
            datos += str(txt_cant3P.Text.ToString()) + "|" + txt_dx3P.Text.ToString() + "|" + DDL_Via3.SelectedValue.ToString() + "|" + DDL_fx3P.SelectedValue.ToString() + "|" + str2(DDL_cod3P.SelectedValue.ToString(), DDL_t3.SelectedValue.ToString()) + "|" + str3(DDL_t3.SelectedValue.ToString(), DDL_Trat3.SelectedValue.ToString()) + "|" + DDL_e3.SelectedValue.ToString() + "|" + str(txt_tdias3.Text.ToString()) + "|" + txt_obs3.Text.ToString() + "|"
            datos += str(txt_cant4P.Text.ToString()) + "|" + txt_dx4P.Text.ToString() + "|" + DDL_Via4.SelectedValue.ToString() + "|" + DDL_fx4P.SelectedValue.ToString() + "|" + str2(DDL_cod4P.SelectedValue.ToString(), DDL_t4.SelectedValue.ToString()) + "|" + str3(DDL_t4.SelectedValue.ToString(), DDL_Trat4.SelectedValue.ToString()) + "|" + DDL_e4.SelectedValue.ToString() + "|" + str(txt_tdias4.Text.ToString()) + "|" + txt_obs4.Text.ToString() + "|"
            datos += str(txt_cant5P.Text.ToString()) + "|" + txt_dx5P.Text.ToString() + "|" + DDL_Via5.SelectedValue.ToString() + "|" + DDL_fx5P.SelectedValue.ToString() + "|" + str2(DDL_cod5P.SelectedValue.ToString(), DDL_t5.SelectedValue.ToString()) + "|" + str3(DDL_t5.SelectedValue.ToString(), DDL_Trat5.SelectedValue.ToString()) + "|" + DDL_e5.SelectedValue.ToString() + "|" + str(txt_tdias5.Text.ToString()) + "|" + txt_obs5.Text.ToString() + "|"
            datos += str(txt_cant6P.Text.ToString()) + "|" + txt_dx6P.Text.ToString() + "|" + DDL_Via6.SelectedValue.ToString() + "|" + DDL_fx6P.SelectedValue.ToString() + "|" + str2(DDL_cod6P.SelectedValue.ToString(), DDL_t6.SelectedValue.ToString()) + "|" + str3(DDL_t6.SelectedValue.ToString(), DDL_Trat6.SelectedValue.ToString()) + "|" + DDL_e6.SelectedValue.ToString() + "|" + str(txt_tdias6.Text.ToString()) + "|" + txt_obs6.Text.ToString() + "|"
            datos += lbl_usuarioP.Text.ToString()
            db.Cn1 = cn1

            '*/ Revisa Código 1 cantidad entrega para actualizar existencia
            If DDL_cod1.SelectedValue IsNot Nothing Then
                Dim tipo_ingreso_med As String = 2
                Dim producto As String = DDL_cod1P.SelectedValue.ToString()
                Dim qty_ingreso As String = lbl_cantidadP1.Text.ToString()
                Dim qty_salida As String = txt_cant1P.Text.ToString()
                Dim tipo_mov As String = 3
                db.Update_Existencia_Egreso(tipo_ingreso_med, FechaSistema, producto, qty_ingreso, qty_salida, tipo_mov, usuario, 0, 0, "01/01/1900")
            End If

            '*/ Revisa Código 2 cantidad entrega para actualizar existencia
            If DDL_cod2P.SelectedValue IsNot Nothing Then
                Dim tipo_ingreso_med As String = 2
                Dim producto As String = DDL_cod2P.SelectedValue.ToString()
                Dim qty_ingreso As String = lbl_cantidadP2.Text.ToString()
                Dim qty_salida As String = txt_cant2P.Text.ToString()
                Dim tipo_mov As String = 3
                db.Update_Existencia_Egreso(tipo_ingreso_med, FechaSistema, producto, qty_ingreso, qty_salida, tipo_mov, usuario, 0, 0, "01/01/1900")
            End If

            '*/ Revisa Código 3 cantidad entrega para actualizar existencia
            If DDL_cod3P.SelectedValue IsNot Nothing Then
                Dim tipo_ingreso_med As String = 2
                Dim producto As String = DDL_cod3P.SelectedValue.ToString()
                Dim qty_ingreso As String = lbl_cantidadP3.Text.ToString()
                Dim qty_salida As String = txt_cant3P.Text.ToString()
                Dim tipo_mov As String = 3
                db.Update_Existencia_Egreso(tipo_ingreso_med, FechaSistema, producto, qty_ingreso, qty_salida, tipo_mov, usuario, 0, 0, "01/01/1900")
            End If

            '*/ Revisa Código 4 cantidad entrega para actualizar existencia
            If DDL_cod4P.SelectedValue IsNot Nothing Then
                Dim tipo_ingreso_med As String = 2
                Dim producto As String = DDL_cod4P.SelectedValue.ToString()
                Dim qty_ingreso As String = lbl_cantidadP4.Text.ToString()
                Dim qty_salida As String = txt_cant4P.Text.ToString()
                Dim tipo_mov As String = 3
                db.Update_Existencia_Egreso(tipo_ingreso_med, FechaSistema, producto, qty_ingreso, qty_salida, tipo_mov, usuario, 0, 0, "01/01/1900")
            End If

            '*/ Revisa Código 5 cantidad entrega para actualizar existencia
            If DDL_cod5P.SelectedValue IsNot Nothing Then
                Dim tipo_ingreso_med As String = 2
                Dim producto As String = DDL_cod5P.SelectedValue.ToString()
                Dim qty_ingreso As String = lbl_cantidadP5.Text.ToString()
                Dim qty_salida As String = txt_cant5P.Text.ToString()
                Dim tipo_mov As String = 3
                db.Update_Existencia_Egreso(tipo_ingreso_med, FechaSistema, producto, qty_ingreso, qty_salida, tipo_mov, usuario, 0, 0, "01/01/1900")
            End If

            '*/ Revisa Código 6 cantidad entrega para actualizar existencia
            If DDL_cod6P.SelectedValue IsNot Nothing Then
                Dim tipo_ingreso_med As String = 2
                Dim producto As String = DDL_cod6P.SelectedValue.ToString()
                Dim qty_ingreso As String = lbl_cantidadP6.Text.ToString()
                Dim qty_salida As String = txt_cant6P.Text.ToString()
                Dim tipo_mov As String = 3
                db.Update_Existencia_Egreso(tipo_ingreso_med, FechaSistema, producto, qty_ingreso, qty_salida, tipo_mov, usuario, 0, 0, "01/01/1900")
            End If

            db.ActualizaControlProf(datos, usuario)
            divARV.Visible = False
            divProf.Visible = True
            Llena_regPac(Request.QueryString("nhc").ToUpper())
            UP_registros.Update()
            MPERegistro.Show()
            setcamposActivarP(1)
            btn_editarP.Visible = True
            btn_grabarP.Visible = False
            btn_cancelarP.Visible = False
        Catch ex As Exception
            lbl_error.Text = ex.Message
        End Try
		'Procedimiento actualiza esquema
        ActualizaMedicamentoProf()
		lbl_feP.Enabled = False
    End Sub

	'Funcion para actualizar medicamentos profilaxis
    Sub ActualizaMedicamentoProf()

        'Se relaciona sqlConnection con variable conn strin
        Dim cn As SqlConnection = New SqlConnection(cn1)

        'Se manda el nombre del procedimiento + cn
        Dim cmd As SqlCommand = New SqlCommand("ActualizaMedicamentoProf", cn)

        'Se estable la propiedad para el procemiento almacenado
        cmd.CommandType = System.Data.CommandType.StoredProcedure

        'Se manda el listado de parametros al procedimiento
        cmd.Parameters.Add("@IdCCPROF", SqlDbType.VarChar).Value = lbl_idP.Text
        cmd.Parameters.Add("@FechaEntrega", SqlDbType.VarChar).Value = lbl_feP.Text
        'Medicamento Prof 1
        cmd.Parameters.Add("@Prof1Dosis", SqlDbType.VarChar).Value = txt_dx1P.Text
        If DDL_cod1P.SelectedValue = "" Then
            cmd.Parameters.Add("@Prof1_Codigo", SqlDbType.Int).Value = DBNull.Value
        Else
            cmd.Parameters.Add("@Prof1_Codigo", SqlDbType.Int).Value = DDL_cod1P.SelectedValue
        End If
        If DDL_Via1.SelectedValue = "" Then
            cmd.Parameters.Add("@Prof1Via", SqlDbType.Int).Value = DBNull.Value
        Else
            cmd.Parameters.Add("@Prof1Via", SqlDbType.Int).Value = DDL_Via1.SelectedValue
        End If
        If DDL_fx1P.SelectedValue = "" Then
            cmd.Parameters.Add("@Prof1Frecuencia", SqlDbType.Int).Value = DBNull.Value
        Else
            cmd.Parameters.Add("@Prof1Frecuencia", SqlDbType.Int).Value = DDL_fx1P.SelectedValue
        End If
        If DDL_t1.SelectedValue = "" Then
            cmd.Parameters.Add("@Prof1Tipo", SqlDbType.Int).Value = DBNull.Value
        Else
            cmd.Parameters.Add("@Prof1Tipo", SqlDbType.Int).Value = DDL_t1.SelectedValue
        End If
        If DDL_Trat1.SelectedValue = "" Then
            cmd.Parameters.Add("@Prof1TipoTratamiento", SqlDbType.Int).Value = DBNull.Value
        Else
            cmd.Parameters.Add("@Prof1TipoTratamiento", SqlDbType.Int).Value = DDL_Trat1.SelectedValue
        End If
        If DDL_e1.SelectedValue = "" Then
            cmd.Parameters.Add("@Prof1Estatus", SqlDbType.Int).Value = DBNull.Value
        Else
            cmd.Parameters.Add("@Prof1Estatus", SqlDbType.Int).Value = DDL_e1.SelectedValue
        End If
        cmd.Parameters.Add("@Prof1Cantidad", SqlDbType.VarChar).Value = txt_cant1P.Text
        cmd.Parameters.Add("@Prof1TTMed", SqlDbType.VarChar).Value = txt_tdias1.Text
        cmd.Parameters.Add("@Prof1Observaciones", SqlDbType.VarChar).Value = txt_obs1.Text
        'Medicamento Prof 2
        If DDL_cod2P.SelectedValue = "" Then
            cmd.Parameters.Add("@Prof2_Codigo", SqlDbType.Int).Value = DBNull.Value
        Else
            cmd.Parameters.Add("@Prof2_Codigo", SqlDbType.Int).Value = DDL_cod2P.SelectedValue
        End If
        cmd.Parameters.Add("@Prof2Dosis", SqlDbType.VarChar).Value = txt_dx2P.Text
        If DDL_Via2.SelectedValue = "" Then
            cmd.Parameters.Add("@Prof2Via", SqlDbType.Int).Value = DBNull.Value
        Else
            cmd.Parameters.Add("@Prof2Via", SqlDbType.Int).Value = DDL_Via2.SelectedValue
        End If
        If DDL_fx2P.SelectedValue = "" Then
            cmd.Parameters.Add("@Prof2Frecuencia", SqlDbType.Int).Value = DBNull.Value
        Else
            cmd.Parameters.Add("@Prof2Frecuencia", SqlDbType.Int).Value = DDL_fx2P.SelectedValue
        End If
        If DDL_t2.SelectedValue = "" Then
            cmd.Parameters.Add("@Prof2Tipo", SqlDbType.Int).Value = DBNull.Value
        Else
            cmd.Parameters.Add("@Prof2Tipo", SqlDbType.Int).Value = DDL_t2.SelectedValue
        End If
        If DDL_Trat2.SelectedValue = "" Then
            cmd.Parameters.Add("@Prof2TipoTratamiento", SqlDbType.Int).Value = DBNull.Value
        Else
            cmd.Parameters.Add("@Prof2TipoTratamiento", SqlDbType.Int).Value = DDL_Trat2.SelectedValue
        End If
        If DDL_e2.SelectedValue = "" Then
            cmd.Parameters.Add("@Prof2Estatus", SqlDbType.Int).Value = DBNull.Value
        Else
            cmd.Parameters.Add("@Prof2Estatus", SqlDbType.Int).Value = DDL_e2.SelectedValue
        End If
        cmd.Parameters.Add("@Prof2Cantidad", SqlDbType.VarChar).Value = txt_cant2P.Text
        cmd.Parameters.Add("@Prof2TTMed", SqlDbType.VarChar).Value = txt_tdias2.Text
        cmd.Parameters.Add("@Prof2Observaciones", SqlDbType.VarChar).Value = txt_obs2.Text
        'Medicamento Prof 3
        If DDL_cod3P.SelectedValue = "" Then
            cmd.Parameters.Add("@Prof3_Codigo", SqlDbType.Int).Value = DBNull.Value
        Else
            cmd.Parameters.Add("@Prof3_Codigo", SqlDbType.Int).Value = DDL_cod3P.SelectedValue
        End If
        cmd.Parameters.Add("@Prof3Dosis", SqlDbType.VarChar).Value = txt_dx3P.Text
        If DDL_Via3.SelectedValue = "" Then
            cmd.Parameters.Add("@Prof3Via", SqlDbType.Int).Value = DBNull.Value
        Else
            cmd.Parameters.Add("@Prof3Via", SqlDbType.Int).Value = DDL_Via3.SelectedValue
        End If
        If DDL_fx3P.SelectedValue = "" Then
            cmd.Parameters.Add("@Prof3Frecuencia", SqlDbType.Int).Value = DBNull.Value
        Else
            cmd.Parameters.Add("@Prof3Frecuencia", SqlDbType.Int).Value = DDL_fx3P.SelectedValue
        End If
        If DDL_t3.SelectedValue = "" Then
            cmd.Parameters.Add("@Prof3Tipo", SqlDbType.Int).Value = DBNull.Value
        Else
            cmd.Parameters.Add("@Prof3Tipo", SqlDbType.Int).Value = DDL_t3.SelectedValue
        End If
        If DDL_Trat3.SelectedValue = "" Then
            cmd.Parameters.Add("@Prof3TipoTratamiento", SqlDbType.Int).Value = DBNull.Value
        Else
            cmd.Parameters.Add("@Prof3TipoTratamiento", SqlDbType.Int).Value = DDL_Trat3.SelectedValue
        End If
        If DDL_e3.SelectedValue = "" Then
            cmd.Parameters.Add("@Prof3Estatus", SqlDbType.Int).Value = DBNull.Value
        Else
            cmd.Parameters.Add("@Prof3Estatus", SqlDbType.Int).Value = DDL_e3.SelectedValue
        End If
        cmd.Parameters.Add("@Prof3Cantidad", SqlDbType.VarChar).Value = txt_cant3P.Text
        cmd.Parameters.Add("@Prof3TTMed", SqlDbType.VarChar).Value = txt_tdias3.Text
        cmd.Parameters.Add("@Prof3Observaciones", SqlDbType.VarChar).Value = txt_obs3.Text
        'Medicamento Prof 4
        If DDL_cod4P.SelectedValue = "" Then
            cmd.Parameters.Add("@Prof4_Codigo", SqlDbType.Int).Value = DBNull.Value
        Else
            cmd.Parameters.Add("@Prof4_Codigo", SqlDbType.Int).Value = DDL_cod4P.SelectedValue
        End If
        cmd.Parameters.Add("@Prof4Dosis", SqlDbType.VarChar).Value = txt_dx4P.Text
        If DDL_Via4.SelectedValue = "" Then
            cmd.Parameters.Add("@Prof4Via", SqlDbType.Int).Value = DBNull.Value
        Else
            cmd.Parameters.Add("@Prof4Via", SqlDbType.Int).Value = DDL_Via4.SelectedValue
        End If
        If DDL_fx4P.SelectedValue = "" Then
            cmd.Parameters.Add("@Prof4Frecuencia", SqlDbType.Int).Value = DBNull.Value
        Else
            cmd.Parameters.Add("@Prof4Frecuencia", SqlDbType.Int).Value = DDL_fx4P.SelectedValue
        End If
        If DDL_t4.SelectedValue = "" Then
            cmd.Parameters.Add("@Prof4Tipo", SqlDbType.Int).Value = DBNull.Value
        Else
            cmd.Parameters.Add("@Prof4Tipo", SqlDbType.Int).Value = DDL_t4.SelectedValue
        End If
        If DDL_Trat4.SelectedValue = "" Then
            cmd.Parameters.Add("@Prof4TipoTratamiento", SqlDbType.Int).Value = DBNull.Value
        Else
            cmd.Parameters.Add("@Prof4TipoTratamiento", SqlDbType.Int).Value = DDL_Trat4.SelectedValue
        End If
        If DDL_e4.SelectedValue = "" Then
            cmd.Parameters.Add("@Prof4Estatus", SqlDbType.Int).Value = DBNull.Value
        Else
            cmd.Parameters.Add("@Prof4Estatus", SqlDbType.Int).Value = DDL_e4.SelectedValue
        End If
        cmd.Parameters.Add("@Prof4Cantidad", SqlDbType.VarChar).Value = txt_cant4P.Text
        cmd.Parameters.Add("@Prof4TTMed", SqlDbType.VarChar).Value = txt_tdias4.Text
        cmd.Parameters.Add("@Prof4Observaciones", SqlDbType.VarChar).Value = txt_obs4.Text
        'Medicamento Prof 5
        If DDL_cod5P.SelectedValue = "" Then
            cmd.Parameters.Add("@Prof5_Codigo", SqlDbType.Int).Value = DBNull.Value
        Else
            cmd.Parameters.Add("@Prof5_Codigo", SqlDbType.Int).Value = DDL_cod5P.SelectedValue
        End If
        cmd.Parameters.Add("@Prof5Dosis", SqlDbType.VarChar).Value = txt_dx5P.Text
        If DDL_Via5.SelectedValue = "" Then
            cmd.Parameters.Add("@Prof5Via", SqlDbType.Int).Value = DBNull.Value
        Else
            cmd.Parameters.Add("@Prof5Via", SqlDbType.Int).Value = DDL_Via5.SelectedValue
        End If
        If DDL_fx5P.SelectedValue = "" Then
            cmd.Parameters.Add("@Prof5Frecuencia", SqlDbType.Int).Value = DBNull.Value
        Else
            cmd.Parameters.Add("@Prof5Frecuencia", SqlDbType.Int).Value = DDL_fx5P.SelectedValue
        End If
        If DDL_t5.SelectedValue = "" Then
            cmd.Parameters.Add("@Prof5Tipo", SqlDbType.Int).Value = DBNull.Value
        Else
            cmd.Parameters.Add("@Prof5Tipo", SqlDbType.Int).Value = DDL_t5.SelectedValue
        End If
        If DDL_Trat5.SelectedValue = "" Then
            cmd.Parameters.Add("@Prof5TipoTratamiento", SqlDbType.Int).Value = DBNull.Value
        Else
            cmd.Parameters.Add("@Prof5TipoTratamiento", SqlDbType.Int).Value = DDL_Trat5.SelectedValue
        End If
        If DDL_e5.SelectedValue = "" Then
            cmd.Parameters.Add("@Prof5Estatus", SqlDbType.Int).Value = DBNull.Value
        Else
            cmd.Parameters.Add("@Prof5Estatus", SqlDbType.Int).Value = DDL_e5.SelectedValue
        End If
        cmd.Parameters.Add("@Prof5Cantidad", SqlDbType.VarChar).Value = txt_cant5P.Text
        cmd.Parameters.Add("@Prof5TTMed", SqlDbType.VarChar).Value = txt_tdias5.Text
        cmd.Parameters.Add("@Prof5Observaciones", SqlDbType.VarChar).Value = txt_obs5.Text
        'Medicamento Prof 6
        If DDL_cod6P.SelectedValue = "" Then
            cmd.Parameters.Add("@Prof6_Codigo", SqlDbType.Int).Value = DBNull.Value
        Else
            cmd.Parameters.Add("@Prof6_Codigo", SqlDbType.Int).Value = DDL_cod6P.SelectedValue
        End If
        cmd.Parameters.Add("@Prof6Dosis", SqlDbType.VarChar).Value = txt_dx6P.Text
        If DDL_Via6.SelectedValue = "" Then
            cmd.Parameters.Add("@Prof6Via", SqlDbType.Int).Value = DBNull.Value
        Else
            cmd.Parameters.Add("@Prof6Via", SqlDbType.Int).Value = DDL_Via6.SelectedValue
        End If
        If DDL_fx6P.SelectedValue = "" Then
            cmd.Parameters.Add("@Prof6Frecuencia", SqlDbType.Int).Value = DBNull.Value
        Else
            cmd.Parameters.Add("@Prof6Frecuencia", SqlDbType.Int).Value = DDL_fx6P.SelectedValue
        End If
        If DDL_t6.SelectedValue = "" Then
            cmd.Parameters.Add("@Prof6Tipo", SqlDbType.Int).Value = DBNull.Value
        Else
            cmd.Parameters.Add("@Prof6Tipo", SqlDbType.Int).Value = DDL_t6.SelectedValue
        End If
        If DDL_Trat6.SelectedValue = "" Then
            cmd.Parameters.Add("@Prof6TipoTratamiento", SqlDbType.Int).Value = DBNull.Value
        Else
            cmd.Parameters.Add("@Prof6TipoTratamiento", SqlDbType.Int).Value = DDL_Trat6.SelectedValue
        End If
        If DDL_e6.SelectedValue = "" Then
            cmd.Parameters.Add("@Prof6Estatus", SqlDbType.Int).Value = DBNull.Value
        Else
            cmd.Parameters.Add("@Prof6Estatus", SqlDbType.Int).Value = DDL_e6.SelectedValue
        End If
        cmd.Parameters.Add("@Prof6Cantidad", SqlDbType.VarChar).Value = txt_cant6P.Text
        cmd.Parameters.Add("@Prof6TTMed", SqlDbType.VarChar).Value = txt_tdias6.Text
        cmd.Parameters.Add("@Prof6Observaciones", SqlDbType.VarChar).Value = txt_obs6.Text

        'Se inicia la validacion para ejecutar el procedimiento
        Try
            'Se abre la conecion
            cn.Open()
            'Se ejecuta el procedimiento
            cmd.ExecuteNonQuery()
        Catch ex As Exception
        Finally
            cn.Close()
        End Try
    End Sub
	
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
End Class
