Imports System.Data
Imports System.Data.SqlClient

Partial Class Condones
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
                txt_asi.Focus()
                'setcampos(0)
                llenacodigo()
                'llenafrecuencia()
                'llenaEstatus()
                'llenaVIA()
            End If
        End If
    End Sub

    Sub llenadatos(ByVal nhc As String)
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
        txt_fe_yy.Text = fechayear
        txt_fe_dd.Enabled = True
        txt_fe_mm.Enabled = True
        txt_fe_yy.Enabled = False
		
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
        'db.Cn1 = cn1
        'Dim y As String = db.ObtieneUltimoRegProf(nhc, usuario)
        'Dim rpU As String() = y.Split("|")
        'If rpU(0).ToString() = "True" Then
        '    ufecha = True
        '    Session("ufecha") = True
        '    lbl_ultimafechaentrega.Text = String.Empty
        '    idufecha = rpU(1).ToString()
        '    Session("idufecha") = rpU(1).ToString()
        '    lbl_ultimafechaentrega.Text = rpU(2).ToString()
        'Else
        '    ufecha = False
        '    Session("ufecha") = False
        '    Session("idufecha") = ""
        '    idufecha = ""
        '    lbl_ultimafechaentrega.Text = rpU(1)
        'End If
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
    Protected Sub txt_asi_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_asi.TextChanged
        buscaNHC()
		LlenaCondonLubricante()
    End Sub

    Protected Sub btn_buscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_buscar.Click
        buscaNHC()
		LlenaCondonLubricante()
    End Sub

    Sub buscaNHC()
        If txt_asi.Text.Trim <> String.Empty Then
            llenadatos(txt_asi.Text.ToUpper())
            UltimaEntrega(txt_asi.Text.ToUpper())
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
    Sub llenacodigo()
        db.Cn1 = cn1
        usuario = Session("usuario").ToString()
        Dim tbCondones As DataTable = db.ObtieneCodCondones("2", usuario)
        If tbCondones IsNot Nothing Then
            DDL_cod1.DataSource = tbCondones
            DDL_cod1.DataTextField = "Codigo"
            DDL_cod1.DataValueField = "IdFFProf"
            DDL_cod1.DataBind()
            DDL_cod1.Items.Insert(0, New ListItem("", "0"))

            DDL_cod2.DataSource = tbCondones
            DDL_cod2.DataTextField = "Codigo"
            DDL_cod2.DataValueField = "IdFFProf"
            DDL_cod2.DataBind()
            DDL_cod2.Items.Insert(0, New ListItem("", "0"))
			
			DDL_cod3.DataSource = tbCondones
            DDL_cod3.DataTextField = "Codigo"
            DDL_cod3.DataValueField = "IdFFProf"
            DDL_cod3.DataBind()
            DDL_cod3.Items.Insert(0, New ListItem("", "0"))
        End If
    End Sub
    Sub buscaMED(ByVal codigo As String, ByVal med As Integer)
        db.Cn1 = cn1
        Dim x As String = db.ObtieneMED(codigo, usuario)
        Dim rp As String() = x.Split("|")
        If rp(0).ToString() = "True" Then
            Select Case med
                Case 1
                    lbl_descripcion1.Text = String.Empty
                    lbl_descripcion1.Text = rp(1).ToString()
                Case 2
                    lbl_descripcion2.Text = String.Empty
                    lbl_descripcion2.Text = rp(1).ToString()
            End Select
            lbl_error.Text = String.Empty
            btn_editar.Visible = False
            btn_agregar.Visible = False
        Else
            lbl_error.Text = rp(1)
        End If
    End Sub

    Protected Sub DDL_cod1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDL_cod1.SelectedIndexChanged
        If DDL_cod1.SelectedValue <> "0" Then

            buscaMED(DDL_cod1.SelectedValue, 1)
            'DDL_cod1.SelectedValue = 1
        Else
            'setcampos(1)
            lbl_descripcion1.Text = ""
            DDL_cod1.Focus()
        End If
    End Sub

    Protected Sub DDL_cod2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDL_cod2.SelectedIndexChanged
        If DDL_cod2.SelectedValue <> "0" Then

            buscaMED(DDL_cod2.SelectedValue, 2)
            'DDL_cod1.SelectedValue = 1
        Else
            'setcampos(1)
            lbl_descripcion1.Text = ""
            DDL_cod2.Focus()
        End If
    End Sub
	
	Protected Sub DDL_cod3_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDL_cod3.SelectedIndexChanged
        If DDL_cod3.SelectedIndex > 0 Then
            lbl_descripcion3.Text = "Lubricante"
        Else
            lbl_descripcion3.Text = ""
        End If
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
            If Session("nhc").ToString() = String.Empty Or Session("nhc").ToString() = "" Then
                Session("nhc") = txt_asi.Text.ToUpper()
            End If
            If FechaEntrega <> Nothing Then
                If DDL_cod1.SelectedValue <> 0 Then
                     usuario = Session("usuario").ToString()
                    'Dim datos As String
                    'datos = Session("nhc").ToString().ToUpper() + "|" + FechaEntrega + "|" + DDL_cod1.SelectedValue.ToString() + "|" + str(txt_cant1.Text.ToString()) + "|" + DDL_cod2.SelectedValue.ToString() + "|" + str(txt_cant2.Text.ToString()) + "|" + txt_obs.Text.ToString()
                    db.Cn1 = cn1
                    'Procedimiento para alamcenar datos Control Condones-Lubricantes
                    'Se relaciona sqlConnection con variable conn string
                    Dim cn As SqlConnection = New SqlConnection(cn1)

                    'Se manda el nombre del procedimiento + cn
                    Dim cmd As SqlCommand = New SqlCommand("Proc_SaveCondoms", cn)

                    'Se estable la propiedad para el procemiento almacenado
                    cmd.CommandType = System.Data.CommandType.StoredProcedure

                    'Se manda el listado de parametros al procedimiento
                    cmd.Parameters.Add("@NHC", SqlDbType.VarChar).Value = txt_asi.Text
                    cmd.Parameters.Add("@FechaEntrega", SqlDbType.VarChar).Value = FechaEntrega
                    If DDL_cod1.SelectedValue = "" Then
                        cmd.Parameters.Add("@Codigo1", SqlDbType.Int).Value = DBNull.Value
                    Else
                        cmd.Parameters.Add("@Codigo1", SqlDbType.Int).Value = DDL_cod1.SelectedValue
                    End If
                    cmd.Parameters.Add("@Cantidad1", SqlDbType.VarChar).Value = txt_cant1.Text
                    If DDL_cod2.SelectedValue = "" Then
                        cmd.Parameters.Add("@Codigo2", SqlDbType.Int).Value = DBNull.Value
                    Else
                        cmd.Parameters.Add("@Codigo2", SqlDbType.Int).Value = DDL_cod2.SelectedValue
                    End If
                    cmd.Parameters.Add("@Cantidad2", SqlDbType.VarChar).Value = txt_cant2.Text
                    If DDL_cod3.SelectedValue = "" Then
                        cmd.Parameters.Add("@Codigo3", SqlDbType.Int).Value = DBNull.Value
                    Else
                        cmd.Parameters.Add("@Codigo3", SqlDbType.Int).Value = DDL_cod3.SelectedValue
                    End If
                    cmd.Parameters.Add("@Cantidad3", SqlDbType.VarChar).Value = txt_cant3.Text

                    cmd.Parameters.Add("@Observaciones", SqlDbType.VarChar).Value = txt_obs.Text
                    cmd.Parameters.Add("@NomUsuario", SqlDbType.VarChar).Value = usuario

                    cmd.Parameters.Add("@Message", SqlDbType.VarChar, 100)
                    cmd.Parameters("@Message").Direction = ParameterDirection.Output

                    'Se inicia la validacion para ejecutar el procedimiento
                    Try
                        'Se abre la conecion
                        cn.Open()
                        'Se ejecuta el procedimiento
                        cmd.ExecuteNonQuery()
                        txtMessage.Text = cmd.Parameters("@Message").Value.ToString()
						Page.ClientScript.RegisterStartupScript(Me.GetType(), "Scripts", "<script>Mensaje();</script>")
                    Catch ex As Exception
                    Finally
                        cn.Close()
                    End Try
                    'db.GrabaControlCONDONES(datos, usuario)
                    'Response.Redirect("~/Condones.aspx", False)
                Else
                    lbl_error.Text = "Codigo en blanco, verifique"
                End If
            Else
                lbl_error.Text = "Fecha Entrega no es correcta, favor verificar"
            End If

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

    Sub UltimaEntrega(ByVal nhc As String)
        db.Cn1 = cn1
        Dim x As String = db.ObtieneUltimaFecha_EntregaCondones(nhc, usuario)
        Dim rp As String() = x.Split("|")
        If rp(0).ToString() = "True" Then
            lbl_ultimafechaentrega.Text = rp(2).ToString()
        Else
            lbl_ultimafechaentrega.Text = String.Empty

        End If
    End Sub
	
	 Sub LlenaCondonLubricante()
        Dim Condon As Integer = CStr(DDL_cod1.Items.Count)
        Dim Lubricante As Integer = CStr(DDL_cod2.Items.Count)
        Dim CondonTubo As Integer = CStr(DDL_cod3.Items.Count)

        'Condon
        If Condon > 1 Then
            DDL_cod1.SelectedIndex = 1
            DDL_cod1.Enabled = False
            lbl_descripcion1.Text = "Condones"
            txt_cant1.Enabled = True
        Else
            DDL_cod1.SelectedIndex = 0
        End If
        'Lubricante
        If Lubricante > 2 Then
            DDL_cod2.SelectedIndex = 2
            DDL_cod2.Enabled = False
            lbl_descripcion2.Text = "Lubricante"
            txt_cant2.Enabled = True
        Else
            DDL_cod2.SelectedIndex = 0
        End If
        'Condon Tubo
        If CondonTubo > 3 Then
            DDL_cod3.Items.RemoveAt(2)
            DDL_cod3.Items.RemoveAt(1)
            DDL_cod3.Enabled = True
            txt_cant2.Enabled = True
        Else
            DDL_cod3.SelectedIndex = 0
        End If
    End Sub
End Class
