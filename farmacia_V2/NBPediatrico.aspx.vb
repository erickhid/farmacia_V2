Imports System.Data

Partial Class NBPediatrico
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
                llenaGenero()
                llenaBaja()
            End If
        End If
    End Sub

    Sub llenaGenero()
        DDL_genero.Items.Insert(0, New ListItem("[sin asignar]", ""))
        DDL_genero.Items.Insert(1, New ListItem("Hombre", "1"))
        DDL_genero.Items.Insert(2, New ListItem("Mujer", "2"))
    End Sub

    Sub llenaBaja()
        db.Cn1 = cn1
        usuario = Session("usuario").ToString()
        Dim tbB As DataTable = db.ObtieneBaja(usuario)
        If tbB IsNot Nothing Then
            DDL_estatus.DataSource = tbB
            DDL_estatus.DataTextField = "NomMotivoBaja"
            DDL_estatus.DataValueField = "IdMotivoBaja"
            DDL_estatus.DataBind()
            DDL_estatus.Items.Insert(0, New ListItem("[sin asignar]", ""))
        End If
    End Sub

    Protected Sub btn_grabar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_grabar.Click
        'Dim FechaNacimiento As String = txt_nacimiento.Text.Substring(3, 2) + "/" + txt_nacimiento.Text.Substring(0, 2) + "/" + txt_nacimiento.Text.Substring(6, 4)
        Dim FechaNacimiento As String = txt_nacimiento.Text
        Dim datos As String = txt_asi.Text.ToUpper().ToString() + "|" + txt_pnombre.Text.ToString() + "|" + txt_snombre.Text.ToString() + "|" + txt_papellido.Text.ToString() + "|" + txt_sapellido.Text.ToString() + "|" + DDL_genero.SelectedValue.ToString() + "|" + FechaNacimiento.ToString() + "|" + txt_telefono.Text.ToString() + "|" + txt_domicilio.Text.ToString() + "|" + DDL_estatus.SelectedValue.ToString()
        db.Cn1 = cn1
        usuario = Session("usuario").ToString()
        Dim x As String = db.AgregaBPediatrico(txt_asi.Text.ToUpper().ToString(), datos, usuario)
        Dim xy As String() = x.Split("|")
        If xy(0).ToString() = "True" Then
            Response.Redirect("~/ingresoARV.aspx", False)
        Else
            lbl_error.Text = xy(1).ToString()
        End If
    End Sub

    Protected Sub btn_cancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_cancelar.Click
        Response.Redirect("~/ingresoARV.aspx", False)
    End Sub
End Class
