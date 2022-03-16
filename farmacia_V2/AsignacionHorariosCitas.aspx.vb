Imports System.Data
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Data.SqlClient

Partial Class CalendarioCitas
    Inherits System.Web.UI.Page
    Private db As New BusinessLogicDB()
    Private revisar As New Rsesion()
    Public cn1 As String = ConfigurationManager.ConnectionStrings("conString").ConnectionString
    Public cn2 As String = ConfigurationManager.ConnectionStrings("conStringFarmacia").ConnectionString
    Public errores As String = ""
    Public usuario As String = ""
    Public strnhc As String
    Public existenhc As Boolean


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Response.Buffer = True
            Response.ExpiresAbsolute = DateTime.Now.AddDays(-1.0)
            Response.Expires = -1500
            Response.CacheControl = "no-cache"
            If Not revisar.RevisaSesion(Session("conexion").ToString(), Session("usuario").ToString()) Then
                Response.Redirect("~/inicio.aspx", False)
            Else
                usuario = Session("usuario").ToString()
                'Declaracion de funciones
                llenaJornada()
                txt_asi.Focus()
            End If
        End If
    End Sub

    Sub llenaJornada()
        db.Cn2 = cn2
        db.Cn1 = cn1
        Dim datos As DataTable = db.Lista_Horarios(usuario)
        ddl_horario_cita.DataSource = datos
        ddl_horario_cita.DataTextField = "Desc_Horario"
        ddl_horario_cita.DataValueField = "Id_Bloque_H"
        ddl_horario_cita.DataBind()
        ddl_horario_cita.Items.Insert(0, New ListItem("", "0"))
        ddl_horario_cita.SelectedIndex = 0
    End Sub

    Sub llenadatos(ByVal nhc As String)
        Dim tipo As String
        usuario = Session("usuario").ToString()
        If nhc.Substring(1, 1).ToUpper.ToString = "P" Then
            tipo = "P"
        Else
            tipo = "A"

        End If
        If tipo = "A" Then
            db.Cn1 = cn1
            Dim n As String = db.DatosBasales(nhc, usuario)
            Dim p As String() = n.Split("|")
            If p(0).ToString() = "True" Then
                strnhc = nhc
                Session("nhc") = nhc
                lbl_nombre.Text = p(2).ToString()
                lbl_genero.Text = p(3).ToString()
                lbl_edad.Text = p(4).ToString()
                lbl_FEntrega.Text = p(5).ToString()
                lbl_FRetorno.Text = p(6).ToString()
                txt_fecha.Text = lbl_FRetorno.Text
            Else
                lbl_nombre.Text = String.Empty
                lbl_genero.Text = String.Empty
                lbl_edad.Text = String.Empty
                lbl_FEntrega.Text = String.Empty
                lbl_FRetorno.Text = String.Empty
                ltl_error.Text = "PACIENTE NO EXISTE, CONFIRME SU NHC"
            End If
        ElseIf tipo = "P" Then
            db.Cn2 = cn2
            Dim n As String = db.DatosBasalesP(nhc, usuario)
            Dim pP As String() = n.Split("|")
            If pP(0).ToString() = "True" Then
                'strnhc = nhc.ToUpper()
                'Session("nhc") = nhc.ToUpper()
                'existenhc = True
                'lbl_nombre.Text = String.Empty
                'lbl_genero.Text = String.Empty
                'lbl_edad.Text = String.Empty
                'lbl_FEntrega.Text = String.Empty
                'lbl_FRetorno.Text = String.Empty
                lbl_nombre.Text = pP(2).ToString()
                lbl_genero.Text = pP(3).ToString()
                lbl_edad.Text = pP(4).ToString()
                lbl_FEntrega.Text = pP(5).ToString()
                lbl_FRetorno.Text = pP(6).ToString()
                txt_fecha.Text = lbl_FRetorno.Text
            Else
                lbl_nombre.Text = String.Empty
                lbl_genero.Text = String.Empty
                lbl_edad.Text = String.Empty
                lbl_FEntrega.Text = String.Empty
                lbl_FRetorno.Text = String.Empty
                ltl_error.Text = "PACIENTE NO EXISTE, CONFIRME SU NHC"
            End If
        End If


    End Sub

    Sub buscaNHC()
        If txt_asi.Text.ToUpper().Trim <> String.Empty Then
            llenadatos(txt_asi.Text.ToUpper())
            If existenhc Then
                'llenadatos(txt_asi.Text.ToUpper())
                'lbl_nombre.Text = String.Empty
                'lbl_genero.Text = String.Empty
                'lbl_edad.Text = String.Empty
                'lbl_FEntrega.Text = String.Empty
                'lbl_FRetorno.Text = String.Empty
                'txt_fecha.Text = String.Empty
                txt_asi.Focus()
            Else
                'lbl_nombre.Text = String.Empty
                'lbl_genero.Text = String.Empty
                'lbl_edad.Text = String.Empty
                'lbl_FEntrega.Text = String.Empty
                'lbl_FRetorno.Text = String.Empty
                'txt_fecha.Text = String.Empty
                txt_asi.Focus()
            End If
        End If
    End Sub

    Sub resumen_citas(ByVal fecha As String)
        fecha = lbl_FRetorno.Text
        usuario = Session("usuario").ToString()
        db.Cn2 = cn2
        Dim resumencitas As DataTable = db.Resumen(fecha, usuario)
        Try
            Session("dspacA") = resumencitas
            RC_reportes.DataSource = resumencitas
            RC_reportes.DataBind()
            lbl_titulo.Text = "RESUMEN CITAS  "
        Catch ex As Exception
            ltl_error.Text = ex.Message
        End Try
    End Sub

    Protected Sub txt_asi_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_asi.TextChanged
        buscaNHC()
        resumen_citas(lbl_FRetorno.Text)
        Dim dia As String = txt_fecha.Text.ToString()
        lbl_dia_horario.Text = dia
        ObtieneNoCitas()
    End Sub
   
    Protected Sub btn_buscar_Click(sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_buscar.Click
        'buscaNHC()
        ltl_output.Text = String.Empty
        Dim nhc_ASI As String
        nhc_ASI = txt_asi.Text.ToString()
        llenadatos(nhc_ASI)
        resumen_citas(lbl_FRetorno.Text)
        ObtieneNoCitas()
    End Sub
   
    Protected Sub btn_agregar_Click(sender As Object, e As EventArgs) Handles btn_agregar.Click
        Dim datos As String
        usuario = Session("usuario")
        datos = txt_asi.Text.ToString() + "|" + lbl_FEntrega.Text.ToString() + "|" + lbl_FRetorno.Text.ToString() + "|" + txt_fecha.Text.ToString() + "|" + ddl_horario_cita.SelectedValue.ToString()
        db.Cn2 = cn2
        db.Insert_HorarioCitaPaciente(datos, usuario)
        Response.Redirect("~/AsignacionHorariosCitas.aspx", False)
    End Sub

    Sub limpiar()
        txt_asi.Text = String.Empty
        lbl_nombre.Text = String.Empty
        lbl_genero.Text = String.Empty
        lbl_edad.Text = String.Empty
        lbl_FEntrega.Text = String.Empty
        lbl_FRetorno.Text = String.Empty
        txt_fecha.Text = String.Empty
        lbl_dia_cita.Text = String.Empty
        ddl_horario_cita.SelectedValue = Nothing
        Response.Redirect("~/AsignacionHorariosCitas.aspx", False)
    End Sub

    Protected Sub btn_limpiar_Click(sender As Object, e As EventArgs) Handles btn_limpiar.Click
        limpiar()
    End Sub

    Function diaSemana(ByVal fecha As String) As String
        Dim A As Integer = Convert.ToInt32(fecha.Substring(6, 4))
        Dim M As Integer = Convert.ToInt32(fecha.Substring(3, 2))
        Dim D As Integer = Convert.ToInt32(fecha.Substring(0, 2))
        Dim diaFecha As New DateTime(A, M, D)
        Return diaFecha.ToString("dddd", New CultureInfo("es-ES"))
    End Function

    Sub obtienediacita()
        If txt_fecha.Text <> String.Empty Then
            Dim dia As String
            dia = diaSemana(txt_fecha.Text)
            lbl_dia_cita.Text = dia.ToString()
        Else
            ltl_error2.Text = "<span class='error'>" & "Ingrese Fecha Próxima Cita" & "</span>"
        End If
    End Sub

    Sub ObtieneNoCitas()
        DivNoCitasPaciente.Visible = True
        db.Cn2 = cn2

        'Capturando el dia de la semana en la caja de texto lbl_dia_horario
        Dim dia As String = txt_fecha.Text.ToString()
        lbl_dia_horario.Text = dia

        'Validando el Horario 0
        Dim horario As String = db.NoHorario(dia, usuario)
        LblCantidad.Text = horario.ToString()

        If horario >= 8 Then
            LblStatus.Text = "<span class='error_hc'> Completo </span>"
        Else
            LblStatus.Text = "<span class='status_BH_disponible'> Disponible </span>"
        End If

        'Validando el Horario 1
        Dim horario1 As String = db.NoHorario1(dia, usuario)
        LblCantidad1.Text = horario1.ToString()

        If horario1 >= 8 Then
            LblStatus1.Text = "<span class='error_hc'> Completo </span>"
        Else
            LblStatus1.Text = "<span class='status_BH_disponible'> Disponible </span>"
        End If

        'Validando el Horario 2
        Dim horario2 As String = db.NoHorario2(dia, usuario)
        LblCantidad2.Text = horario2.ToString()

        If horario1 >= 8 Then
            LblStatus2.Text = "<span class='error_hc'> Completo </span>"
        Else
            LblStatus2.Text = "<span class='status_BH_disponible'> Disponible </span>"
        End If

        'Validando el Horario 3
        Dim horario3 As String = db.NoHorario3(dia, usuario)
        LblCantidad3.Text = horario3.ToString()

        If horario3 >= 8 Then
            LblStatus3.Text = "<span class='error_hc'> Completo </span>"
        Else
            LblStatus3.Text = "<span class='status_BH_disponible'> Disponible </span>"
        End If

        'Validando el Horario 4
        Dim horario4 As String = db.NoHorario4(dia, usuario)
        LblCantidad4.Text = horario4.ToString()

        If horario4 >= 8 Then
            LblStatus4.Text = "<span class='error_hc'> Completo </span>"
        Else
            LblStatus4.Text = "<span class='status_BH_disponible'> Disponible </span>"
        End If

        'Validando el Horario 5
        Dim horario5 As String = db.NoHorario5(dia, usuario)
        LblCantidad5.Text = horario5.ToString()

        If horario5 >= 8 Then
            LblStatus5.Text = "<span class='error_hc'> Completo </span>"
        Else
            LblStatus5.Text = "<span class='status_BH_disponible'> Disponible </span>"
        End If

        'Validando el Horario 6
        Dim horario6 As String = db.NoHorario6(dia, usuario)
        LblCantidad6.Text = horario6.ToString()

        If horario6 >= 8 Then
            LblStatus6.Text = "<span class='error_hc'> Completo </span>"
        Else
            LblStatus6.Text = "<span class='status_BH_disponible'> Disponible </span>"
        End If

        'Validando el Horario 7
        Dim horario7 As String = db.NoHorario7(dia, usuario)
        LblCantidad7.Text = horario7.ToString()

        If horario7 >= 8 Then
            LblStatus7.Text = "<span class='error_hc'> Completo </span>"
        Else
            LblStatus7.Text = "<span class='status_BH_disponible'> Disponible </span>"
        End If

        'Validando el Horario 8
        Dim horario8 As String = db.NoHorario8(dia, usuario)
        LblCantidad8.Text = horario8.ToString()

        If horario8 >= 8 Then
            LblStatus8.Text = "<span class='error_hc'> Completo </span>"
        Else
            LblStatus8.Text = "<span class='status_BH_disponible'> Disponible </span>"
        End If
    End Sub

    'Protected Sub txt_fecha_TextChanged(sender As Object, e As EventArgs) Handles txt_fecha.TextChanged
    '    obtienediacita()
    '    ObtieneNoCitas()
    'End Sub

End Class
