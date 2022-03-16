Imports System.Data
Partial Class IngresoMed_Inventario_PROF

    Inherits System.Web.UI.Page
    Private revisar As New Rsesion()
    Private db As New BusinessLogicDB()
    Public cn1 As String = ConfigurationManager.ConnectionStrings("conStringFarmacia").ConnectionString
    Public cn2 As String = ConfigurationManager.ConnectionStrings("conString").ConnectionString
    Public usuario As String = ""
    Public errores As String = ""
    Public fechasistema As String = DateTime.Now.ToString("dd/MM/yyyy")
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
                ' llenaMedPROF(fechasistema)
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
    Sub llenaMedPROF(ByVal fecha_mes_min As String, ByVal fecha_mes_max As String, ByVal fecha_anio As String)
        Try
            db.Cn1 = cn1
            Dim I_MedPROF As DataTable = db.Obtiene_ingresos_med_PROF(fecha_mes_min, fecha_mes_max, fecha_anio, usuario)
            Session("datosIMedPROF") = I_MedPROF
            GV_pnl_IngresoPROF.DataSource = I_MedPROF
            GV_pnl_IngresoPROF.DataBind()
            'If GrupoFamiliar.Rows.Count > 0 Then
            '    llenafecha(1, "M")
            '    llenaARV(1)
            'Else
            '    llenafecha(0, "M")
            '    llenaARV(0)
            'End If
            'llenaMOTIVOCAMBIO()
        Catch ex As Exception
            'lbl_error.Text = "Hubo un error al mostrar listado de Pacientes."
            errores = (usuario & "|IngresoARV.llenaIngresos()|" & ex.ToString() & "|") + ex.Message
            db.GrabarErrores(errores)
        End Try
    End Sub
    Sub llenacodigo()
        db.Cn1 = cn1
        usuario = Session("usuario").ToString()
        Dim tbMed As DataTable = db.ObtienePROFMedicamento("2", usuario)
        If tbMed IsNot Nothing Then
            DDL_PROF.DataSource = tbMed
            DDL_PROF.DataTextField = "Codigo"
            DDL_PROF.DataValueField = "IdFFProf"
            DDL_PROF.DataBind()
            DDL_PROF.Items.Insert(0, New ListItem("", "0"))
        End If
    End Sub

    Sub buscaMED(ByVal codigo As String, ByVal med As Integer)
        db.Cn1 = cn1
        Dim x As String = db.ObtienePROF_FF_Concentracion(codigo, usuario)
        Dim rp As String() = x.Split("|")
        If rp(0).ToString() = "True" Then
            NomPROF.Text = String.Empty
            NomPROF.Text = rp(1).ToString() + "/" + rp(2).ToString() + "/" + rp(3).ToString()
        Else
            lbl_error.Text = "Error al buscar medicamento"
        End If
    End Sub

    Protected Sub DDL_PROF_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DDL_PROF.SelectedIndexChanged
        If DDL_PROF.SelectedValue <> "0" Then

            buscaMED(DDL_PROF.SelectedValue, 1)
            'DDL_cod1.SelectedValue = 1
        Else
            'setcampos(1)
            NomPROF.Text = ""
            DDL_PROF.Focus()
        End If
    End Sub

    Protected Sub ibt_Agregar_Click(sender As Object, e As ImageClickEventArgs) Handles ibt_Agregar.Click
        '*Existencia medicamento
        ' Dim existencia As String
        usuario = Session("usuario").ToString()
        Dim FechaEntrega As String = Convert.ToString(txt_fe_dd.Text) + "/" + Convert.ToString(txt_fe_mm.Text) + "/" + Convert.ToString(txt_fe_yy.Text)
        Dim FechaVencimiento As String = Convert.ToString(txt_fe_dd_ven.Text) + "/" + Convert.ToString(txt_fe_mm_ven.Text) + "/" + Convert.ToString(txt_fe_yy_ven.Text)
        Try
            Convert.ToDateTime(FechaEntrega).ToString("dd/MM/yy")
        Catch ex As Exception
            lbl_error.Text = "Fecha Entrega no es correcta, favor verificar"
            txt_fe_dd.Focus()
            Exit Sub
        End Try

        Dim tipo_ingreso_med As String = 2
        Dim producto As String = DDL_PROF.SelectedValue.ToString()
        Dim qty_ingreso As String = txt_cantidadPROF.Text.ToString()
        Dim no_req As String = txt_no_req.Text.ToString()
        Dim no_lote As String = txt_lote.Text.ToString()
        'Dim qty_salida As String
        Dim tipo_mov As String = 1

        If FechaEntrega <> Nothing Then
            db.Cn1 = cn1
            db.Ingreso_ARV_PROF_Existencia_Modificar(tipo_ingreso_med, FechaEntrega, producto, qty_ingreso, 0, tipo_mov, usuario, no_req, no_lote, FechaVencimiento)
            'Response.Redirect("~/IngresoMed_Inventario.aspx", False)
            txt_fe_dd.Text = Nothing
            txt_fe_mm.Text = Nothing
            txt_fe_yy.Text = Nothing
            DDL_PROF.SelectedValue = Nothing
            NomPROF.Text = Nothing
            txt_cantidadPROF.Text = Nothing
            llenaMedPROF(DDL_mes.SelectedValue.ToString(), DDL_mes.SelectedValue.ToString(), DDL_anio.SelectedValue.ToString())
        End If
    End Sub

    Protected Sub GV_pnl_IngresoPROF_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GV_pnl_IngresoPROF.RowCommand

        'Dim FechaEntrega As String = Convert.ToString(txt_fe_dd.Text) + "/" + Convert.ToString(txt_fe_mm.Text) + "/" + Convert.ToString(txt_fe_yy.Text)
        usuario = Session("usuario").ToString()
        If e.CommandName = "Borrar" Then
            If Not revisar.RevisaSesion(Session("conexion").ToString(), Session("usuario").ToString()) Then
                Response.Redirect("~/inicio.aspx", False)
            Else
                db.Cn1 = cn1
                Dim gv As GridView = DirectCast(sender, GridView)
                Dim rowIndex As Int32 = Convert.ToInt32(e.CommandArgument.ToString())
                Dim idPROF As String = gv.DataKeys(rowIndex)(0).ToString()

                '*Variables para actualizar existencias al eliminar
                Dim tipo_ingreso_med As String = 2

                'Dim qty_ingreso As String = txt_cantidadARV.Text.ToString()
                Dim X_ingreso As String = db.Obtiene_ingresos_med_PROF_Update(idPROF, usuario)
                Dim rp_ingreso As String() = X_ingreso.Split("|")
                'If rp_ingreso(0) = "True" Then
                '    Dim cantidad_arv As String = rp_ingreso(2)
                'End If
                Dim producto As String = rp_ingreso(4).ToString()
                Dim qty_salida As String = rp_ingreso(3).ToString()
                Dim tipo_mov As String = 3
                db.Update_Existencia_Eliminar_Ingreso(tipo_ingreso_med, fechasistema, producto, 0, qty_salida, tipo_mov, usuario)
                Dim x As String = db.Eliminar_Ingreso_ARV(idPROF, Session("usuario").ToString())
                Dim rp As String() = x.Split("|")
                If rp(0) = "True" Then
                    llenaMedPROF(DDL_mes.SelectedValue.ToString(), DDL_mes.SelectedValue.ToString(), DDL_anio.SelectedValue.ToString())
                    lbl_error.Text = rp(1)
                Else
                    lbl_error.Text = rp(1)
                End If
            End If
        End If
        'If e.CommandName = "Editar" Then
        '    If Not revisar.RevisaSesion(Session("conexion").ToString(), Session("usuario").ToString()) Then
        '        Response.Redirect("~/inicio.aspx", False)
        '    Else
        '        Try
        '            db.Cn1 = cn1
        '            Dim gv As GridView = DirectCast(sender, GridView)
        '            Dim rowIndex As Int32 = Convert.ToInt32(e.CommandArgument.ToString())
        '            Dim idGF As String = gv.DataKeys(rowIndex)(0).ToString()
        '            Dim x As String = db.ContenidoGrupoFamiliar(idGF, Session("usuario").ToString())
        '            Dim rp As String() = x.Split("|")
        '            If rp(0).ToString() = "True" Then
        '                Session("idGrupoFamiliar") = idGF
        '                txt_Nombre.Text = rp(1).ToString()
        '                DDL_TipoRelacion.SelectedValue = rp(2).ToString()
        '                txt_Edad.Text = rp(3).ToString()
        '                DDL_NivelEducativo.SelectedValue = rp(4).ToString()
        '                DDL_SituacionLaboral.SelectedValue = rp(5).ToString()
        '                txt_Ingreso.Text = rp(6).ToString()
        '                DDL_ConoceDx.SelectedValue = rp(7).ToString()
        '                txt_Nombre.Focus()
        '                ibt_Agregar.Visible = False
        '                ibt_Modificar.Visible = True
        '                ibt_Cancelar.Visible = True
        '            Else
        '                lbl_error.Text = rp(1)
        '            End If
        '        Catch ex As Exception
        '            lbl_error.Text = ex.Message
        '        End Try
        '    End If
        'End If
    End Sub

    Protected Sub GV_pnl_IngresoPROF_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles GV_pnl_IngresoPROF.PreRender
        Dim n As Integer = 0
        For Each nrow As GridViewRow In GV_pnl_IngresoPROF.Rows
            For columnIndex As Integer = n To Convert.ToInt32(GV_pnl_IngresoPROF.Rows.Count)
                Dim irow1 As ImageButton = DirectCast(nrow.FindControl("ibtBorrar"), ImageButton)
                irow1.CommandArgument = Convert.ToString(n)
                Dim irow2 As ImageButton = DirectCast(nrow.FindControl("ibtEditar"), ImageButton)
                irow2.CommandArgument = Convert.ToString(n)
            Next
            n += 1
        Next
    End Sub

    Protected Sub GV_pnl_IngresoPROF_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GV_pnl_IngresoPROF.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim ib As ImageButton = DirectCast(e.Row.FindControl("ibtBorrar"), ImageButton)
            ib.Attributes.Add("onclick", "javascript:return confirm('Esta seguro que quiere Eliminar este Ingreso Profilaxis?')")
        End If
    End Sub
    Protected Sub btn_generar_Click(sender As Object, e As EventArgs) Handles btn_generar.Click
        llenaMedPROF(DDL_mes.SelectedValue.ToString(), DDL_mes.SelectedValue.ToString(), DDL_anio.SelectedValue.ToString())
    End Sub
End Class
