
Imports System.Data
Partial Class Otros_EgresosMED

    Inherits System.Web.UI.Page
    Private revisar As New Rsesion()
    Private db As New BusinessLogicDB()
    Public cn1 As String = ConfigurationManager.ConnectionStrings("conStringFarmacia").ConnectionString
    Public cn2 As String = ConfigurationManager.ConnectionStrings("conString").ConnectionString
    Public usuario As String = ""
    Public errores As String = ""
    Public fechasistema As String = DateTime.Now.ToString("dd/MM/yyyy")
    Public rol As String = ""
    'Public strnhc As String
    'Public existenhc As Boolean
    'Public ufecha As Boolean
    'Public idufecha As String

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
                'llenaEgresos(fechasistema)
                llenacodigo()
                LlenaAnos()
                'ufecha = False
                'txt_asi.Focus()
                ''setcampos(0)
                'llenaTipoRelacion()
                'llenaNivelEducativo()
                'llenaSituacionLaboral()
                'llenaConoceDx()
                'llenaQuienesConocenDx()
                'llenaTipoVivienda()
                'llenaServicios()
                'llenaTipoConstruccion()
                'calculaIngresosEgresos()
                'llenaProblemasIdentificados()
                'pnl_CircuitoAdherencia.Visible = False ''panel de adherencia oculto
            End If
            'ElseIf Page.IsPostBack Then
            '    Dim wcICausedPostBack As WebControl = CType(GetControlThatCausedPostBack(TryCast(sender, Page)), WebControl)
            '    Dim indx As Integer = wcICausedPostBack.TabIndex
            '    Dim ctrl = From control In wcICausedPostBack.Parent.Controls.OfType(Of WebControl)() Where control.TabIndex > indx Order By control.TabIndex Select control
            '    ctrl.DefaultIfEmpty(wcICausedPostBack).First().Focus()
        End If
    End Sub

    Private Sub LlenaAnos()
        Dim currentYear As Integer = Year(Today)
        Dim firstyear As Integer = 2011
        DDL_anio.Items.Clear()
        For y As Integer = firstyear To currentYear
            DDL_anio.Items.Add(New ListItem(y, y))
        Next
        DDL_anio.SelectedValue = Year(Today)
    End Sub

    Protected Sub ibt_Modificar_Click(sender As Object, e As ImageClickEventArgs) Handles ibt_Modificar.Click

    End Sub

    Protected Sub ibt_Cancelar_Click(sender As Object, e As ImageClickEventArgs) Handles ibt_Cancelar.Click

    End Sub
    Sub llenaEgresos()
        Try
            db.Cn1 = cn1

            Dim E_MedA As DataTable = db.Obtiene_egresos_med(DDL_mes.SelectedValue.ToString(), DDL_mes.SelectedValue.ToString(), DDL_anio.SelectedValue.ToString(), 1, usuario)
            Session("datosEMedARV") = E_MedA
            GV_pnl_Egresos.DataSource = E_MedA
            GV_pnl_Egresos.DataBind()

            Dim E_MedP As DataTable = db.Obtiene_egresos_med(DDL_mes.SelectedValue.ToString(), DDL_mes.SelectedValue.ToString(), DDL_anio.SelectedValue.ToString(), 2, usuario)
            Session("datosEMedPROF") = E_MedP
            GV_pnl_egresos_P.DataSource = E_MedP
            GV_pnl_egresos_P.DataBind()
        Catch ex As Exception
            'lbl_error.Text = "Hubo un error al mostrar listado de Pacientes."
            errores = (usuario & "|OtrosEgresos.LlenaEgresos()|" & ex.ToString() & "|") + ex.Message
            db.GrabarErrores(errores)
        End Try
    End Sub
    Sub llenacodigo()
        db.Cn1 = cn1
        usuario = Session("usuario").ToString()
        'If DDL_TIPO_MED.SelectedValue = 1 Then
        '    DDL_ARV.Visible = True
        '    DDL_PROF.Visible = False
        Dim tbMed As DataTable = db.ObtienePROFMedicamento("1", usuario)
        If tbMed IsNot Nothing Then
            DDL_ARV.DataSource = tbMed
            DDL_ARV.DataTextField = "Codigo"
            DDL_ARV.DataValueField = "IdFFARV"
            DDL_ARV.DataBind()
            DDL_ARV.Items.Insert(0, New ListItem("", "0"))
        End If
        'Else
        'DDL_PROF.Visible = True
        'DDL_ARV.Visible = False
        Dim tbMed2 As DataTable = db.ObtienePROFMedicamento("2", usuario)
        If tbMed2 IsNot Nothing Then
            DDL_PROF.DataSource = tbMed2
            DDL_PROF.DataTextField = "Codigo"
            DDL_PROF.DataValueField = "IdFFProf"
            DDL_PROF.DataBind()
            DDL_PROF.Items.Insert(0, New ListItem("", "0"))
        End If

        Dim tbTipoEg As DataTable = db.ObtieneTipoEgresoMed(usuario)
        If tbTipoEg IsNot Nothing Then
            DDL_TipoEgreso.DataSource = tbTipoEg
            DDL_TipoEgreso.DataTextField = "Nom_TipoEgreso"
            DDL_TipoEgreso.DataValueField = "Id_TipoEgreso"
            DDL_TipoEgreso.DataBind()
            DDL_TipoEgreso.Items.Insert(0, New ListItem("", "0"))
        End If
        'End If
    End Sub

    Sub buscaMED(ByVal codigo As String, ByVal med As Integer)
        db.Cn1 = cn1
        If DDL_TIPO_MED.SelectedValue = 1 Then
            Dim x As String = db.ObtieneARV_FF_Concentracion(codigo, usuario)
            Dim rp As String() = x.Split("|")
            If rp(0).ToString() = "True" Then
                NomMedicamento.Text = String.Empty
                NomMedicamento.Text = rp(1).ToString() + "/" + rp(2).ToString() + "/" + rp(3).ToString()
            Else
                lbl_error.Text = "Error al buscar medicamento"
            End If
        Else
            Dim x As String = db.ObtienePROF_FF_Concentracion(codigo, usuario)
            Dim rp As String() = x.Split("|")
            If rp(0).ToString() = "True" Then
                NomMedicamento.Text = String.Empty
                NomMedicamento.Text = rp(1).ToString() + "/" + rp(2).ToString() + "/" + rp(3).ToString()
            Else
                lbl_error.Text = "Error al buscar medicamento"
            End If
        End If
    End Sub

    Protected Sub DDL_ARV_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DDL_ARV.SelectedIndexChanged
        If DDL_ARV.SelectedValue <> "0" Then

            buscaMED(DDL_ARV.SelectedValue, 1)
            'DDL_cod1.SelectedValue = 1
        Else
            'setcampos(1)
            NomMedicamento.Text = ""
            DDL_ARV.Focus()

        End If
    End Sub



    Protected Sub ibt_Agregar_Click(sender As Object, e As ImageClickEventArgs) Handles ibt_Agregar.Click
        '*Existencia medicamento
        ' Dim existencia As String
        usuario = Session("usuario").ToString()
        Dim FechaEgreso As String = Convert.ToString(txt_fe_dd.Text) + "/" + Convert.ToString(txt_fe_mm.Text) + "/" + Convert.ToString(txt_fe_yy.Text)

        Try
            Convert.ToDateTime(FechaEgreso).ToString("dd/MM/yy")
        Catch ex As Exception
            lbl_error.Text = "Fecha Egreso no es correcta, favor verificar"
            txt_fe_dd.Focus()
            Exit Sub
        End Try
        Dim tipomed As String = DDL_TIPO_MED.SelectedValue.ToString()
        Dim ffarv As String = DDL_ARV.SelectedValue.ToString()
        Dim ffprof As String = DDL_PROF.SelectedValue.ToString()
        Dim cantidad As String = txt_cantidadmed.Text.ToString()
        Dim tipoegreso As String = DDL_TipoEgreso.SelectedValue.ToString()
        Dim nhctv As String = txt_NHC_TV.Text.ToString.ToString()


        If FechaEgreso <> Nothing Then
            db.Cn1 = cn1
            If DDL_ARV.SelectedValue <> 0 Then
                db.Graba_OtrosEgresos(FechaEgreso, tipomed, ffarv, cantidad, tipoegreso, nhctv, usuario)

                '*/Actualiza Existencias med
                Dim tipo_ingreso_med As String = 1
                Dim producto As String = DDL_ARV.SelectedValue.ToString()
                Dim qty_ingreso As String = 0
                Dim qty_egreso As String = txt_cantidadmed.Text.ToString()
                'Dim qty_salida As String
                Dim tipo_mov As String = 2
                db.Update_Existencia_Egreso(tipo_ingreso_med, FechaEgreso, producto, qty_ingreso, qty_egreso, tipo_mov, usuario, 0, 0, "01/01/1900")
                DDL_TIPO_MED.SelectedValue = Nothing
                DDL_ARV.SelectedValue = Nothing
                txt_cantidadmed.Text = Nothing
                DDL_TipoEgreso.SelectedValue = Nothing
                txt_NHC_TV.Text = Nothing



            ElseIf DDL_PROF.SelectedValue <> 0 Then
                db.Graba_OtrosEgresos(FechaEgreso, tipomed, ffprof, cantidad, tipoegreso, nhctv, usuario)

                '*/Actualiza Existencias med
                Dim tipo_ingreso_med As String = 2
                Dim producto As String = DDL_PROF.SelectedValue.ToString()
                Dim qty_ingreso As String = 0
                Dim qty_egreso As String = txt_cantidadmed.Text.ToString()
                'Dim qty_salida As String
                Dim tipo_mov As String = 2
                db.Update_Existencia_Egreso(tipo_ingreso_med, FechaEgreso, producto, qty_ingreso, qty_egreso, tipo_mov, usuario, 0, 0, "01/01/1900")
                DDL_TIPO_MED.SelectedValue = Nothing
                DDL_PROF.SelectedValue = Nothing
                txt_cantidadmed.Text = Nothing
                DDL_TipoEgreso.SelectedValue = Nothing
                txt_NHC_TV.Text = Nothing


            End If
            llenaEgresos()
        End If
    End Sub

    'Protected Sub GV_pnl_Egresos_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GV_pnl_Egresos.RowCommand

    '    'Dim FechaEntrega As String = Convert.ToString(txt_fe_dd.Text) + "/" + Convert.ToString(txt_fe_mm.Text) + "/" + Convert.ToString(txt_fe_yy.Text)
    '    usuario = Session("usuario").ToString()
    '    If e.CommandName = "Borrar" Then
    '        If Not revisar.RevisaSesion(Session("conexion").ToString(), Session("usuario").ToString()) Then
    '            Response.Redirect("~/inicio.aspx", False)
    '        Else
    '            db.Cn1 = cn1
    '            Dim gv As GridView = DirectCast(sender, GridView)
    '            Dim rowIndex As Int32 = Convert.ToInt32(e.CommandArgument.ToString())
    '            Dim idARV As String = gv.DataKeys(rowIndex)(0).ToString()

    '            '*Variables para actualizar existencias al eliminar
    '            Dim tipo_ingreso_med As String = 1

    '            'Dim qty_ingreso As String = txt_cantidadARV.Text.ToString()
    '            Dim X_ingreso As String = db.Obtiene_ingresos_med_ARV_Update(idARV, usuario)
    '            Dim rp_ingreso As String() = X_ingreso.Split("|")
    '            'If rp_ingreso(0) = "True" Then
    '            '    Dim cantidad_arv As String = rp_ingreso(2)
    '            'End If
    '            Dim producto As String = rp_ingreso(4).ToString()
    '            Dim qty_salida As String = rp_ingreso(3).ToString()
    '            Dim tipo_mov As String = 3
    '            db.Update_Existencia_Eliminar_Ingreso(tipo_ingreso_med, fechasistema, producto, 0, qty_salida, tipo_mov, usuario)
    '            Dim x As String = db.Eliminar_Ingreso_ARV(idARV, Session("usuario").ToString())
    '            Dim rp As String() = x.Split("|")
    '            If rp(0) = "True" Then
    '                llenaEgresos(fechasistema)
    '                lbl_error.Text = rp(1)
    '            Else
    '                lbl_error.Text = rp(1)
    '            End If
    '        End If
    '    End If
    '    'If e.CommandName = "Editar" Then
    '    '    If Not revisar.RevisaSesion(Session("conexion").ToString(), Session("usuario").ToString()) Then
    '    '        Response.Redirect("~/inicio.aspx", False)
    '    '    Else
    '    '        Try
    '    '            db.Cn1 = cn1
    '    '            Dim gv As GridView = DirectCast(sender, GridView)
    '    '            Dim rowIndex As Int32 = Convert.ToInt32(e.CommandArgument.ToString())
    '    '            Dim idGF As String = gv.DataKeys(rowIndex)(0).ToString()
    '    '            Dim x As String = db.ContenidoGrupoFamiliar(idGF, Session("usuario").ToString())
    '    '            Dim rp As String() = x.Split("|")
    '    '            If rp(0).ToString() = "True" Then
    '    '                Session("idGrupoFamiliar") = idGF
    '    '                txt_Nombre.Text = rp(1).ToString()
    '    '                DDL_TipoRelacion.SelectedValue = rp(2).ToString()
    '    '                txt_Edad.Text = rp(3).ToString()
    '    '                DDL_NivelEducativo.SelectedValue = rp(4).ToString()
    '    '                DDL_SituacionLaboral.SelectedValue = rp(5).ToString()
    '    '                txt_Ingreso.Text = rp(6).ToString()
    '    '                DDL_ConoceDx.SelectedValue = rp(7).ToString()
    '    '                txt_Nombre.Focus()
    '    '                ibt_Agregar.Visible = False
    '    '                ibt_Modificar.Visible = True
    '    '                ibt_Cancelar.Visible = True
    '    '            Else
    '    '                lbl_error.Text = rp(1)
    '    '            End If
    '    '        Catch ex As Exception
    '    '            lbl_error.Text = ex.Message
    '    '        End Try
    '    '    End If
    '    'End If
    'End Sub

    Protected Sub GV_pnl_Egresos_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles GV_pnl_Egresos.PreRender
        Dim n As Integer = 0
        rol = Convert.ToString(Session("pusuario"))
        For Each nrow As GridViewRow In GV_pnl_Egresos.Rows


            For columnIndex As Integer = n To Convert.ToInt32(GV_pnl_Egresos.Rows.Count)
                Dim irow1 As ImageButton = DirectCast(nrow.FindControl("ibtBorrar"), ImageButton)
                irow1.CommandArgument = Convert.ToString(n)
                Dim irow2 As ImageButton = DirectCast(nrow.FindControl("ibtEditar"), ImageButton)
                irow2.CommandArgument = Convert.ToString(n)

                Select Case rol
                    Case "1", "2", "6" 'Master, Administrador, Supervisor
                        irow1.Visible = True
                        irow2.Visible = True

                End Select
            Next
            n += 1
        Next
    End Sub

    Protected Sub GV_pnl_Egresos_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GV_pnl_Egresos.RowCommand


        If e.CommandName = "Borrar" Then


            rol = Convert.ToString(Session("pusuario"))
            usuario = Session("usuario").ToString()

            Select Case rol
                Case "1", "2", "6" 'Master, Administrador, Supervisor
                    db.Cn1 = cn1
                    Dim gv As GridView = DirectCast(sender, GridView)
                    Dim rowIndex As Int32 = Convert.ToInt32(e.CommandArgument.ToString())
                    Dim id_otro_egreso As String = gv.DataKeys(rowIndex)(0).ToString()
                    db.Eliminar_otro_egreso_ARV(id_otro_egreso, usuario)

            End Select


        End If
    End Sub


    Protected Sub GV_pnl_Egresos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GV_pnl_Egresos.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim ib As ImageButton = DirectCast(e.Row.FindControl("ibtBorrar"), ImageButton)
            ib.Attributes.Add("onclick", "javascript:return confirm('Esta seguro que quiere Eliminar este Ingreso ARV?')")
        End If
    End Sub

    Protected Sub GV_pnl_egresos_P_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles GV_pnl_egresos_P.PreRender
        Dim n As Integer = 0
        rol = Convert.ToString(Session("pusuario"))

        For Each nrow As GridViewRow In GV_pnl_egresos_P.Rows
            For columnIndex As Integer = n To Convert.ToInt32(GV_pnl_egresos_P.Rows.Count)
                Dim irow1 As ImageButton = DirectCast(nrow.FindControl("ibtBorrar"), ImageButton)
                irow1.CommandArgument = Convert.ToString(n)
                Dim irow2 As ImageButton = DirectCast(nrow.FindControl("ibtEditar"), ImageButton)
                irow2.CommandArgument = Convert.ToString(n)

                Select Case rol
                    Case "1", "2", "6" 'Master, Administrador, Supervisor
                        irow1.Visible = True
                        irow2.Visible = True

                End Select
            Next
            n += 1
        Next
    End Sub

    Protected Sub GV_pnl_egresos_P_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GV_pnl_egresos_P.RowCommand


        If e.CommandName = "Borrar" Then


            rol = Convert.ToString(Session("pusuario"))
            usuario = Session("usuario").ToString()

            Select Case rol
                Case "1", "2", "6" 'Master, Administrador, Supervisor
                    db.Cn1 = cn1
                    Dim gv As GridView = DirectCast(sender, GridView)
                    Dim rowIndex As Int32 = Convert.ToInt32(e.CommandArgument.ToString())
                    Dim id_otro_egreso As String = gv.DataKeys(rowIndex)(0).ToString()
                    db.Eliminar_otro_egreso_ARV(id_otro_egreso, usuario)

            End Select


        End If
    End Sub

    Protected Sub GV_pnl_egresos_P_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GV_pnl_egresos_P.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim ib As ImageButton = DirectCast(e.Row.FindControl("ibtBorrar"), ImageButton)
            ib.Attributes.Add("onclick", "javascript:return confirm('Esta seguro que quiere Eliminar este Ingreso Profilaxis?')")
        End If
    End Sub

    Protected Sub DDL_PROF_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DDL_PROF.SelectedIndexChanged
        If DDL_PROF.SelectedValue <> "0" Then
            buscaMED(DDL_PROF.SelectedValue, 1)
            'DDL_cod1.SelectedValue = 1
        Else
            'setcampos(1)
            NomMedicamento.Text = ""
            DDL_PROF.Focus()
        End If
    End Sub

    Protected Sub DDL_TIPO_MED_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DDL_TIPO_MED.SelectedIndexChanged
        If DDL_TIPO_MED.SelectedValue = 1 Then
            DDL_ARV.Visible = True
            DDL_PROF.Visible = False
            NomMedicamento.Text = Nothing
            DDL_PROF.SelectedValue = Nothing
        Else
            DDL_PROF.Visible = True
            DDL_ARV.Visible = False
            NomMedicamento.Text = Nothing
            DDL_ARV.SelectedValue = Nothing

        End If
    End Sub

    Protected Sub DDL_TipoEgreso_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DDL_TipoEgreso.SelectedIndexChanged
        If DDL_TipoEgreso.SelectedValue = 4 Then
            txt_NHC_TV.Visible = False
            txt_NHC_TV.Text = Nothing
        ElseIf DDL_TipoEgreso.SelectedValue = 5 Then
            txt_NHC_TV.Visible = False
            txt_NHC_TV.Text = Nothing
        ElseIf DDL_TipoEgreso.SelectedValue = 6 Then
            txt_NHC_TV.Visible = False
            txt_NHC_TV.Text = Nothing
        ElseIf DDL_TipoEgreso.SelectedValue = 10 Then
            txt_NHC_TV.Visible = False
            txt_NHC_TV.Text = Nothing
        Else
            txt_NHC_TV.Visible = True
        End If


    End Sub
    Protected Sub btn_generar_Click(sender As Object, e As EventArgs) Handles btn_generar.Click
        llenaEgresos()
    End Sub
End Class
