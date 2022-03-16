Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.IO
Imports System.Data
Imports System.Data.SqlTypes

Public Class BusinessLogicDB
    Private _cn1 As String
    Private _cn2 As String
    Private _cn3 As String
    Private _error As String
    Private _page As String = ""
    Private _pageO As String = ""
    Private Const TimeoutDB As Integer = 1200

    Public Property PageName() As String
        Get
            Return _page
        End Get
        Set(ByVal value As String)
            _page = value
        End Set
    End Property

    Public ReadOnly Property DB_Error() As String
        Get
            Return _error
        End Get
    End Property

    Public Property Cn1() As String
        Get
            Return _cn1
        End Get
        Set(ByVal value As String)
            _cn1 = value
        End Set
    End Property

    Public Property Cn2() As String
        Get
            Return _cn2
        End Get
        Set(ByVal value As String)
            _cn2 = value
        End Set
    End Property

    Public Property Cn3() As String
        Get
            Return _cn3
        End Get
        Set(ByVal value As String)
            _cn3 = value
        End Set
    End Property

    '*Login, cifrador y decifrador*//

    Public Function LoginValidation(ByVal UserName As String, ByVal Password As String, ByVal app As String) As String
        _page = "LoginValidation"
        Dim x As String = ""
        Dim v As Boolean = False
        Dim idperfil As String = ""
        Dim edicion As String = ""
        Dim claveE As String = Encriptar(Password, "Acuario") 'EncString(Password)
        Try
            Using connection1 As New SqlConnection(_cn3)
                connection1.Open()
                Dim strSQL As String = String.Format("SELECT NomUsuario, IdBDAcceso, IdPerfil, Edicion, Estatus FROM USUARIO WHERE NomUsuario = '{0}'", UserName)
                Dim command As New SqlCommand(strSQL, connection1)
                Dim Dr As SqlDataReader = Nothing
                Dr = command.ExecuteReader()
                If Dr.HasRows Then
                    While Dr.Read()
                        If Dr("IdBDAcceso").ToString() = app Then
                            If Dr("Estatus").ToString() = "A" Then
                                v = True
                                idperfil = Dr("IdPerfil").ToString()
                                edicion = Dr("Edicion").ToString()
                                Exit While
                            Else
                                x = "False|Usuario Desactivado."
                                v = False
                            End If
                        Else
                            x = "False|Usuario NO autorizado."
                            v = False
                        End If
                    End While
                Else
                    x = "False|Usuario NO autorizado."
                End If
                Dr.Close()
                connection1.Close()
            End Using
            If v = True Then
                Using connection As New SqlConnection(_cn2)
                    connection.Open()
                    Dim strSQL As String = String.Format("SELECT IdUsuario, NomUsuario, Clave FROM USUARIO WHERE NomUsuario = '{0}'", UserName)
                    Dim command As New SqlCommand(strSQL, connection)
                    Dim Dr As SqlDataReader = Nothing
                    Dr = command.ExecuteReader()
                    If Dr.HasRows Then
                        While Dr.Read()
                            If claveE = Dr("Clave").ToString() Then
                                x = "True|" & Dr("IdUsuario").ToString() & "|" & Dr("NomUsuario").ToString() & "|" & idperfil & "|" & edicion
                                Exit While
                            Else
                                x = "False|Usuario o Clave Incorrectos, favor intente de Nuevo."
                            End If
                        End While
                    Else
                        x = "False|Usuario o Clave Incorrectos, favor intente de Nuevo."
                    End If
                    Dr.Close()
                End Using
            End If
            Return x
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page
            GrabarErrores(UserName & "|" & _pageO & "|" & ex.Number & "|" & ex.Message)
            x = "False|Hubo un Error al Iniciar Sesion, intente de nuevo."
            Return x
        End Try
    End Function

    Public Function ConvertirClave(ByVal sOriginal As String, ByVal sClave As String, ByVal vAccion As Boolean) As String
        Dim x As String = ""
        Dim LenOri As Long
        Dim LenClave As Long
        Dim i As Long
        Dim j As Long
        Dim cO As Long
        Dim cC As Long
        Dim k As Long
        Dim v As String

        LenOri = Len(sOriginal)
        LenClave = Len(sClave)

        v = Space(LenOri)
        i = 0
        For j = 1 To LenOri
            i = i + 1
            If i > LenClave Then
                i = 1
            End If
            cO = Asc(Mid(sOriginal, j, 1))
            cC = Asc(Mid(sClave, i, 1))
            If vAccion Then
                k = cO + cC
                If k > 255 Then
                    k = k - 255
                End If
            Else
                k = cO - cC
                If k < 0 Then
                    k = k + 255
                End If
            End If
            Mid(v, j, 1) = Chr(k)
        Next
        ConvertirClave = v
    End Function

    Public Function DesEncriptar(ByVal sOriginal As String, ByVal sClave As String) As String
        DesEncriptar = ConvertirClave(sOriginal, sClave, False)
    End Function

    Public Function Encriptar(ByVal sOriginal As String, ByVal sClave As String) As String
        Encriptar = ConvertirClave(sOriginal, sClave, True)
    End Function

    Public Sub GrabaSesion(ByVal id As String, ByVal tipo As String, ByVal ip As String, ByVal usuario As String)
        _page = "db.GrabaSesion"
        Dim sql As String = String.Format("INSERT INTO UActividad (IdUsuario, Tipo, IP) VALUES({0}, '{1}', '{2}')", id, tipo, ip)
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim command As New SqlCommand(sql, connection)
                command.ExecuteNonQuery()
                command.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page & "_" & id
            GrabarErrores(usuario & "|" & _page & "|" & ex.Number & "|" & ex.Message)
        End Try
    End Sub

    Public Function Desconectar(ByVal id As String, ByVal ip As String, ByVal usuario As String) As Boolean
        _page = "db.Desconectar"
        Dim sql As String = String.Format("INSERT INTO UActividad (IdUsuario, Tipo, IP) VALUES({0}, '{1}', '{2}')", id, "O", ip)
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim command As New SqlCommand(sql, connection)
                command.ExecuteNonQuery()
                command.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
            Return True
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page & "_" & id
            GrabarErrores(usuario & "|" & _pageO & "|" & ex.Number & "|" & ex.Message)
            Return False
        End Try
    End Function

    '*Reportes*//
    Public Function Codigos(ByVal tipo As String, ByVal usuario As String) As DataTable
        _page = "db.Codigos"
        Dim Query As String = String.Empty
        Select Case tipo
            Case "1" 'ESTATUS
                Query = "SELECT Codigo AS 'Código', Descripcion AS 'Descripción' FROM Estatus"
            Case "2" 'FORMA FARMACEUTICA
                Query = "SELECT IdFF AS 'ID', NomFF AS 'Nombre' FROM FormaFarmaceutica ORDER BY IdFF"
            Case "3" 'ARV
                Query = "SELECT IdARV AS 'ID', NomARV AS 'ARV', NomCorto AS 'Nombre Corto' FROM MedARV"
            Case "4" 'FF ARV
                Query = "SELECT F.IdFFARV, F.IdARV, M.NomCorto AS 'ARV', F.IdFF, FF.NomFF AS 'Forma Farmacéutica', F.Concentracion AS 'Concentración', F.Codigo AS 'Código' FROM FFARV AS F LEFT OUTER JOIN MedARV AS M ON F.IdARV = M.IdARV LEFT OUTER JOIN FormaFarmaceutica AS FF ON F.IdFF = FF.IdFF"
            Case "5" 'ESQUEMAS
                Query = "SELECT IdEsquema AS 'IdE', Descripcion AS 'Descripción', Codigos AS 'Códigos' FROM Esquemas"
            Case "6" 'SUBESQUEMAS
                Query = "SELECT S.IdSEsquema AS 'IdSE', S.IdEsquema AS 'IdE', E.Descripcion AS 'Esquema', S.Descripcion AS 'SubEsquema', S.SCodigo AS 'Código', S.Codigos AS 'Cód_SE', E.Codigos AS 'Cód_E' FROM SubEsquemas AS S LEFT OUTER JOIN Esquemas AS E ON S.IdEsquema = E.IdEsquema"
            Case "7" 'PROFILAXIS
                Query = "SELECT IdProf AS 'ID', NomProfilaxis AS 'Profilaxis' FROM MedProf"
            Case "8" 'FF PROFILAXIS
                Query = "SELECT F.IdFFProf, F.IdProf, M.NomProfilaxis AS 'Profilaxis', F.IdFF, FF.NomFF AS 'Forma Farmacéutica', F.Concentracion AS 'Concentración', F.Codigo AS 'Código' FROM FFProf AS F LEFT OUTER JOIN MedProf AS M ON F.IdProf = M.IdProf LEFT OUTER JOIN FormaFarmaceutica AS FF ON F.IdFF = FF.IdFF"
        End Select
        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim adapter As New SqlDataAdapter()
                adapter.SelectCommand = New SqlCommand(Query, connection)
                adapter.SelectCommand.CommandTimeout = TimeoutDB
                adapter.Fill(Ds, _page)
                adapter.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
            Return Ds.Tables(0)
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page & "_" & tipo
            GrabarErrores(usuario & "|" & _pageO & "|" & ex.Number & "|" & ex.Message)
            Return Nothing
        End Try
    End Function

    Public Function RepMensualSIGPRO(ByVal fecha As String, ByVal usuario As String) As DataTable
        _page = "db.RepMensualSIGPRO"
        'Dim Q As New StringBuilder()
        'Q.Append("SELECT E.IdEsquema AS 'ID', E.Descripcion AS 'ESQUEMA', ")
        'Q.Append("(SELECT * FROM fn_RepARVXEsquema_1(2, 10, 14, 1, E.IdEsquema, '" & fecha & "')) AS 'M1014', ")
        'Q.Append("(SELECT * FROM fn_RepARVXEsquema_1(2, 10, 14, 2, E.IdEsquema, '" & fecha & "')) AS 'F1014', ")
        'Q.Append("(SELECT * FROM fn_RepARVXEsquema_1(2, 10, 14, 3, E.IdEsquema, '" & fecha & "')) AS 'T1014', ")
        'Q.Append("(SELECT * FROM fn_RepARVXEsquema_1(2, 15, 24, 1, E.IdEsquema, '" & fecha & "')) AS 'M1524', ")
        'Q.Append("(SELECT * FROM fn_RepARVXEsquema_1(2, 15, 24, 2, E.IdEsquema, '" & fecha & "')) AS 'F1524', ")
        'Q.Append("(SELECT * FROM fn_RepARVXEsquema_1(2, 15, 24, 3, E.IdEsquema, '" & fecha & "')) AS 'T1524', ")
        'Q.Append("(SELECT * FROM fn_RepARVXEsquema_1(2, 25, 49, 1, E.IdEsquema, '" & fecha & "')) AS 'M2549', ")
        'Q.Append("(SELECT * FROM fn_RepARVXEsquema_1(2, 25, 49, 2, E.IdEsquema, '" & fecha & "')) AS 'F2549', ")
        'Q.Append("(SELECT * FROM fn_RepARVXEsquema_1(2, 25, 49, 3, E.IdEsquema, '" & fecha & "')) AS 'T2549', ")
        'Q.Append("(SELECT * FROM fn_RepARVXEsquema_1(2, 50, 100, 1, E.IdEsquema, '" & fecha & "')) AS 'M50', ")
        'Q.Append("(SELECT * FROM fn_RepARVXEsquema_1(2, 50, 100, 2, E.IdEsquema, '" & fecha & "')) AS 'F50', ")
        'Q.Append("(SELECT * FROM fn_RepARVXEsquema_1(2, 50, 100, 3, E.IdEsquema, '" & fecha & "')) AS 'T50', ")
        'Q.Append("(SELECT * FROM fn_RepARVXEsquema_2(2, 1, E.IdEsquema, '" & fecha & "')) AS 'MT', ")
        'Q.Append("(SELECT * FROM fn_RepARVXEsquema_2(2, 2, E.IdEsquema, '" & fecha & "')) AS 'FT', ")
        'Q.Append("(SELECT * FROM fn_RepARVXEsquema_2(2, 3, E.IdEsquema, '" & fecha & "')) AS 'TT', ")
        'Q.Append("(SELECT * FROM fn_RepARVXEsquema_3(2, E.IdEsquema, '" & fecha & "')) AS 'SUBT1', ")
        'Q.Append("(SELECT * FROM fn_RepARVXEsquema_1(3, 15, 24, 2, E.IdEsquema, '" & fecha & "')) AS 'PP1524', ")
        'Q.Append("(SELECT * FROM fn_RepARVXEsquema_1(3, 25, 49, 2, E.IdEsquema, '" & fecha & "')) AS 'PP2549', ")
        'Q.Append("(SELECT * FROM fn_RepARVXEsquema_1(3, 50, 100, 2, E.IdEsquema, '" & fecha & "')) AS 'PP50', ")
        'Q.Append("(SELECT * FROM fn_RepARVXEsquema_3(3, E.IdEsquema, '" & fecha & "')) AS 'SUBT2', ")
        'Q.Append("(SELECT * FROM fn_RepARVXEsquema_1(1, 15, 24, 2, E.IdEsquema, '" & fecha & "')) AS 'EMB1524', ")
        'Q.Append("(SELECT * FROM fn_RepARVXEsquema_1(1, 25, 49, 2, E.IdEsquema, '" & fecha & "')) AS 'EMB2549', ")
        'Q.Append("(SELECT * FROM fn_RepARVXEsquema_1(1, 50, 100, 2, E.IdEsquema, '" & fecha & "')) AS 'EMB50', ")
        'Q.Append("(SELECT * FROM fn_RepARVXEsquema_3(1, E.IdEsquema, '" & fecha & "')) AS 'SUBT3', ")
        'Q.Append("(SELECT * FROM fn_RepARVXEsquema_4(E.IdEsquema, '" & fecha & "')) AS 'TOTAL' ")
        'Q.Append("FROM Esquemas AS E ")
        ''Q.Append("WHERE (E.IdEsquema BETWEEN 1 AND 34) ")
        'Q.Append("ORDER BY E.IdEsquema ASC")
        'Q.Append("SELECT E.IdEsquema AS 'ID', E.Descripcion AS 'ESQUEMA', ")
        'Q.Append("(SELECT * FROM fn_RepARVXEsquema_1(2, 10, 14, 1, E.IdEsquema, '" & fecha & "')) AS 'M1014', ")
        'Q.Append("(SELECT * FROM fn_RepARVXEsquema_1(2, 10, 14, 2, E.IdEsquema, '" & fecha & "')) AS 'F1014', ")
        'Q.Append("(SELECT * FROM fn_RepARVXEsquema_1(2, 10, 14, 3, E.IdEsquema, '" & fecha & "')) AS 'T1014', ")
        'Q.Append("(SELECT * FROM fn_RepARVXEsquema_1(2, 15, 18, 1, E.IdEsquema, '" & fecha & "')) AS 'M1518', ")
        'Q.Append("(SELECT * FROM fn_RepARVXEsquema_1(2, 15, 18, 2, E.IdEsquema, '" & fecha & "')) AS 'F1518', ")
        'Q.Append("(SELECT * FROM fn_RepARVXEsquema_1(2, 15, 18, 3, E.IdEsquema, '" & fecha & "')) AS 'T1518', ")
        'Q.Append("(SELECT * FROM fn_RepARVXEsquema_1(2, 19, 24, 1, E.IdEsquema, '" & fecha & "')) AS 'M1924', ")
        'Q.Append("(SELECT * FROM fn_RepARVXEsquema_1(2, 19, 24, 2, E.IdEsquema, '" & fecha & "')) AS 'F1924', ")
        'Q.Append("(SELECT * FROM fn_RepARVXEsquema_1(2, 19, 24, 3, E.IdEsquema, '" & fecha & "')) AS 'T1924', ")
        'Q.Append("(SELECT * FROM fn_RepARVXEsquema_1(2, 25, 49, 1, E.IdEsquema, '" & fecha & "')) AS 'M2549', ")
        'Q.Append("(SELECT * FROM fn_RepARVXEsquema_1(2, 25, 49, 2, E.IdEsquema, '" & fecha & "')) AS 'F2549', ")
        'Q.Append("(SELECT * FROM fn_RepARVXEsquema_1(2, 25, 49, 3, E.IdEsquema, '" & fecha & "')) AS 'T2549', ")
        'Q.Append("(SELECT * FROM fn_RepARVXEsquema_1(2, 50, 100, 1, E.IdEsquema, '" & fecha & "')) AS 'M50', ")
        'Q.Append("(SELECT * FROM fn_RepARVXEsquema_1(2, 50, 100, 2, E.IdEsquema, '" & fecha & "')) AS 'F50', ")
        'Q.Append("(SELECT * FROM fn_RepARVXEsquema_1(2, 50, 100, 3, E.IdEsquema, '" & fecha & "')) AS 'T50', ")
        'Q.Append("(SELECT * FROM fn_RepARVXEsquema_2(2, 1, E.IdEsquema, '" & fecha & "')) AS 'MT', ")
        'Q.Append("(SELECT * FROM fn_RepARVXEsquema_2(2, 2, E.IdEsquema, '" & fecha & "')) AS 'FT', ")
        'Q.Append("(SELECT * FROM fn_RepARVXEsquema_2(2, 3, E.IdEsquema, '" & fecha & "')) AS 'TT', ")
        'Q.Append("(SELECT * FROM fn_RepARVXEsquema_3(2, E.IdEsquema, '" & fecha & "')) AS 'SUBT1', ")
        'Q.Append("(SELECT * FROM fn_RepARVXEsquema_1(3, 10, 14, 2, E.IdEsquema, '" & fecha & "')) AS 'PP1014', ")
        'Q.Append("(SELECT * FROM fn_RepARVXEsquema_1(3, 15, 18, 2, E.IdEsquema, '" & fecha & "')) AS 'PP1518', ")
        'Q.Append("(SELECT * FROM fn_RepARVXEsquema_1(3, 19, 24, 2, E.IdEsquema, '" & fecha & "')) AS 'PP1924', ")
        'Q.Append("(SELECT * FROM fn_RepARVXEsquema_1(3, 25, 49, 2, E.IdEsquema, '" & fecha & "')) AS 'PP2549', ")
        'Q.Append("(SELECT * FROM fn_RepARVXEsquema_1(3, 50, 100, 2, E.IdEsquema, '" & fecha & "')) AS 'PP50', ")
        'Q.Append("(SELECT * FROM fn_RepARVXEsquema_3(3, E.IdEsquema, '" & fecha & "')) AS 'SUBT2', ")
        'Q.Append("(SELECT * FROM fn_RepARVXEsquema_1(1, 10, 14, 2, E.IdEsquema, '" & fecha & "')) AS 'EMB1014', ")
        'Q.Append("(SELECT * FROM fn_RepARVXEsquema_1(1, 15, 18, 2, E.IdEsquema, '" & fecha & "')) AS 'EMB1518', ")
        'Q.Append("(SELECT * FROM fn_RepARVXEsquema_1(1, 19, 24, 2, E.IdEsquema, '" & fecha & "')) AS 'EMB1924', ")
        'Q.Append("(SELECT * FROM fn_RepARVXEsquema_1(1, 25, 49, 2, E.IdEsquema, '" & fecha & "')) AS 'EMB2549', ")
        'Q.Append("(SELECT * FROM fn_RepARVXEsquema_1(1, 50, 100, 2, E.IdEsquema, '" & fecha & "')) AS 'EMB50', ")
        'Q.Append("(SELECT * FROM fn_RepARVXEsquema_3(1, E.IdEsquema, '" & fecha & "')) AS 'SUBT3', ")
        'Q.Append("(SELECT * FROM fn_RepARVXEsquema_4(E.IdEsquema, '" & fecha & "')) AS 'TOTAL' ")
        'Q.Append("FROM Esquemas AS E ")
        'Q.Append("ORDER BY E.IdEsquema ASC")
        'Dim Query As String = Q.ToString()
        Dim Query As String = String.Format("EXECUTE dbo.sp_RepSIGPROXMes '{0}'", fecha)
        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim adapter As New SqlDataAdapter()
                adapter.SelectCommand = New SqlCommand(Query, connection)
                adapter.SelectCommand.CommandTimeout = TimeoutDB
                adapter.Fill(Ds, _page)
                adapter.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
            Return Ds.Tables(0)
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page & "_" & fecha
            GrabarErrores(usuario & "|" & _pageO & "|" & ex.Number & "|" & ex.Message)
            Return Nothing
        End Try
    End Function

    Public Function ReportesMensual(ByVal tipo As String, ByVal fechaI As String, ByVal fechaF As String, ByVal usuario As String) As DataTable
        _page = "db.ReportesMensual"
        Dim Query As String = String.Empty
        Select Case tipo
            Case "1" 'EMBARAZADAS
                Query = "SELECT A.NHC, C.IdGenero, (SELECT TOP(1) GV.NomGrupoVulnerabilidad  FROM dbo.PAC_CONDUCTARIESGO AS C1 LEFT OUTER JOIN dbo.PAC_M_GRUPOVULNERABILIDAD AS GV ON GV.IdGrupoVulnerabilidad = C1.GrupoVulnerabilidad WHERE C1.IdPaciente = B.IdPaciente AND C1.GrupoVulnerabilidad IS NOT NULL ORDER BY C1.FechaConductaRiesgo DESC ) AS 'GrupoVulnerabilidad', "
                Query += "dbo.fn_ObtieneEdad(CONVERT(DATE, C.FechaNacimiento), CONVERT(DATE, '" & fechaF & "')) AS 'Edad', A.Embarazo, "
                Query += "CONVERT(VARCHAR,A.FechaEntrega, 103) AS 'FechaEntrega', A.IdEsquema "
                Query += "FROM ControlARV AS A INNER JOIN "
                Query += "PAC_ID AS B ON A.NHC = B.NHC LEFT OUTER JOIN "
                Query += "PAC_BASALES AS C ON C.IdPaciente = B.IdPaciente "
                'Query += "WHERE A.NHC NOT LIKE '%P%' AND A.Embarazo = '1' AND A.Med1_ARVEstatus NOT IN (6, 7, 12, 21) "
                Query += "WHERE A.NHC NOT LIKE '%P%' AND A.Embarazo = '1' AND A.EsquemaEstatus NOT IN (6, 7, 12, 21) "
                Query += "AND A.IdCCARV = (SELECT TOP(1) B.IdCCARV FROM ControlARV AS B "
                Query += "WHERE B.NHC = A.NHC AND B.FechaEntrega = (SELECT TOP(1) C.FechaEntrega FROM ControlARV AS C "
                Query += "WHERE C.NHC = B.NHC AND C.FechaEntrega <= '" & fechaF & " 23:59:59.999' ORDER BY C.FechaEntrega DESC) "
                Query += "ORDER BY B.IdCCARV DESC)"
            Case "1P"
                Query = ""
            Case "2" 'POSTPARTOS
                Query = "SELECT A.NHC, C.IdGenero, (SELECT TOP(1) GV.NomGrupoVulnerabilidad  FROM dbo.PAC_CONDUCTARIESGO AS C1 LEFT OUTER JOIN dbo.PAC_M_GRUPOVULNERABILIDAD AS GV ON GV.IdGrupoVulnerabilidad = C1.GrupoVulnerabilidad WHERE C1.IdPaciente = B.IdPaciente AND C1.GrupoVulnerabilidad IS NOT NULL ORDER BY C1.FechaConductaRiesgo DESC ) AS 'GrupoVulnerabilidad', "
                Query += "dbo.fn_ObtieneEdad(CONVERT(DATE, C.FechaNacimiento), CONVERT(DATE, '" & fechaF & "')) AS 'Edad', A.Embarazo, "
                Query += "CONVERT(VARCHAR,A.FechaEntrega, 103) AS 'FechaEntrega', A.IdEsquema "
                Query += "FROM ControlARV AS A INNER JOIN "
                Query += "PAC_ID AS B ON A.NHC = B.NHC LEFT OUTER JOIN "
                Query += "PAC_BASALES AS C ON C.IdPaciente = B.IdPaciente "
                'Query += "WHERE A.NHC NOT LIKE '%P%' AND A.Embarazo = '3' AND A.Med1_ARVEstatus NOT IN (6, 7, 12, 21) "
                Query += "WHERE A.NHC NOT LIKE '%P%' AND A.Embarazo = '3' AND A.EsquemaEstatus NOT IN (6, 7, 12, 21) "
                Query += "AND A.IdCCARV = (SELECT TOP(1) B.IdCCARV FROM ControlARV AS B "
                Query += "WHERE B.NHC = A.NHC AND B.FechaEntrega = (SELECT TOP(1) C.FechaEntrega FROM ControlARV AS C "
                Query += "WHERE C.NHC = B.NHC AND C.FechaEntrega <= '" & fechaF & " 23:59:59.999' ORDER BY C.FechaEntrega DESC) "
                Query += "ORDER BY B.IdCCARV DESC)"
            Case "2P"
                Query = ""
            Case "3" 'FALLECIDOS
                Query = "SELECT A.NHC, C.IdGenero, (SELECT TOP(1) GV.NomGrupoVulnerabilidad  FROM dbo.PAC_CONDUCTARIESGO AS C1 LEFT OUTER JOIN dbo.PAC_M_GRUPOVULNERABILIDAD AS GV ON GV.IdGrupoVulnerabilidad = C1.GrupoVulnerabilidad WHERE C1.IdPaciente = B.IdPaciente AND C1.GrupoVulnerabilidad IS NOT NULL ORDER BY C1.FechaConductaRiesgo DESC ) AS 'GrupoVulnerabilidad', "
                Query += "dbo.fn_ObtieneEdad(CONVERT(DATE, C.FechaNacimiento), CONVERT(DATE, '" & fechaF & "')) AS 'Edad', A.Embarazo, "
                Query += "CONVERT(VARCHAR,A.FechaEntrega, 103) AS 'FechaEntrega', A.IdEsquema "
                Query += "FROM ControlARV AS A INNER JOIN "
                Query += "PAC_ID AS B ON A.NHC = B.NHC LEFT OUTER JOIN "
                Query += "PAC_BASALES AS C ON C.IdPaciente = B.IdPaciente "
                'Query += "WHERE A.NHC NOT LIKE '%P%' AND A.Med1_ARVEstatus = 12 "
                Query += "WHERE A.NHC NOT LIKE '%P%' AND A.EsquemaEstatus = 12 "
                Query += "AND A.IdCCARV = (SELECT TOP(1) B.IdCCARV FROM ControlARV AS B "
                Query += "WHERE B.NHC = A.NHC AND B.FechaEntrega = (SELECT TOP(1) C.FechaEntrega FROM ControlARV AS C "
                Query += "WHERE C.NHC = B.NHC AND (C.FechaEntrega BETWEEN '" & fechaI & " 00:00:00.000' AND '" & fechaF & " 23:59:59.999') ORDER BY C.FechaEntrega DESC) "
                Query += "ORDER BY B.IdCCARV DESC)"
            Case "3P" 'FALLECIDOS PEDIATRICO
                Query = "SELECT A.NHC, C.Genero, dbo.fn_ObtieneEdad(CONVERT(DATE, C.FechaNacimiento), CONVERT(DATE, '" & fechaF & "')) AS 'Edad', A.Embarazo, "
                Query += "CONVERT(VARCHAR,A.FechaEntrega, 103) AS 'FechaEntrega', A.IdEsquema "
                Query += "FROM ControlARV AS A INNER JOIN "
                Query += "BasalesPediatria AS C ON C.NHC = A.NHC "
                'Query += "WHERE A.NHC LIKE '%P%' AND A.Med1_ARVEstatus = 12 "
                Query += "WHERE A.NHC LIKE '%P%' AND A.EsquemaEstatus = 12 "
                Query += "AND A.IdCCARV = (SELECT TOP(1) B.IdCCARV FROM ControlARV AS B "
                Query += "WHERE B.NHC = A.NHC AND B.FechaEntrega = (SELECT TOP(1) C.FechaEntrega FROM ControlARV AS C "
                Query += "WHERE C.NHC = B.NHC AND (C.FechaEntrega BETWEEN '" & fechaI & " 00:00:00.000' AND '" & fechaF & " 23:59:59.999') ORDER BY C.FechaEntrega DESC) "
                Query += "ORDER BY B.IdCCARV DESC)"
            Case "4" 'ABANDONOS
                Query = "SELECT A.NHC, (SELECT TOP(1) GV.NomGrupoVulnerabilidad  FROM dbo.PAC_CONDUCTARIESGO AS C1 LEFT OUTER JOIN dbo.PAC_M_GRUPOVULNERABILIDAD AS GV ON GV.IdGrupoVulnerabilidad = C1.GrupoVulnerabilidad WHERE C1.IdPaciente = B.IdPaciente AND C1.GrupoVulnerabilidad IS NOT NULL ORDER BY C1.FechaConductaRiesgo DESC ) AS 'GrupoVulnerabilidad', "
                Query += "C.IdGenero, dbo.fn_ObtieneEdad(CONVERT(DATE, C.FechaNacimiento), CONVERT(DATE, '" & fechaF & "')) AS 'Edad', A.Embarazo, "
                Query += "CONVERT(VARCHAR,A.FechaEntrega, 103) AS 'FechaEntrega', A.IdEsquema "
                Query += "FROM ControlARV AS A INNER JOIN "
                Query += "PAC_ID AS B ON A.NHC = B.NHC LEFT OUTER JOIN "
                Query += "PAC_BASALES AS C ON C.IdPaciente = B.IdPaciente "
                'Query += "WHERE A.NHC NOT LIKE '%P%' AND A.Med1_ARVEstatus = 6 "
                Query += "WHERE A.NHC NOT LIKE '%P%' AND A.EsquemaEstatus = 6 "
                Query += "AND A.IdCCARV = (SELECT TOP(1) B.IdCCARV FROM ControlARV AS B "
                Query += "WHERE B.NHC = A.NHC AND B.FechaEntrega = (SELECT TOP(1) C.FechaEntrega FROM ControlARV AS C "
                Query += "WHERE C.NHC = B.NHC AND (C.FechaEntrega BETWEEN '" & fechaI & " 00:00:00.000' AND '" & fechaF & " 23:59:59.999') ORDER BY C.FechaEntrega DESC) "
                Query += "ORDER BY B.IdCCARV DESC)"
            Case "4P" 'ABANDONOS PEDIATRICO
                Query = "SELECT A.NHC, C.Genero, dbo.fn_ObtieneEdad(CONVERT(DATE, C.FechaNacimiento), CONVERT(DATE, '" & fechaF & "')) AS 'Edad', A.Embarazo, "
                Query += "CONVERT(VARCHAR,A.FechaEntrega, 103) AS 'FechaEntrega', A.IdEsquema "
                Query += "FROM ControlARV AS A INNER JOIN "
                Query += "BasalesPediatria AS C ON C.NHC = A.NHC "
                'Query += "WHERE A.NHC LIKE '%P%' AND A.Med1_ARVEstatus = 6 "
                Query += "WHERE A.NHC LIKE '%P%' AND A.EsquemaEstatus = 6 "
                Query += "AND A.IdCCARV = (SELECT TOP(1) B.IdCCARV FROM ControlARV AS B "
                Query += "WHERE B.NHC = A.NHC AND B.FechaEntrega = (SELECT TOP(1) C.FechaEntrega FROM ControlARV AS C "
                Query += "WHERE C.NHC = B.NHC AND (C.FechaEntrega BETWEEN '" & fechaI & " 00:00:00.000' AND '" & fechaF & " 23:59:59.999') ORDER BY C.FechaEntrega DESC) "
                Query += "ORDER BY B.IdCCARV DESC)"
            Case "5" 'TRASLADOS
                Query = "SELECT A.NHC, C.IdGenero, (SELECT TOP(1) GV.NomGrupoVulnerabilidad  FROM dbo.PAC_CONDUCTARIESGO AS C1 LEFT OUTER JOIN dbo.PAC_M_GRUPOVULNERABILIDAD AS GV ON GV.IdGrupoVulnerabilidad = C1.GrupoVulnerabilidad WHERE C1.IdPaciente = B.IdPaciente AND C1.GrupoVulnerabilidad IS NOT NULL ORDER BY C1.FechaConductaRiesgo DESC ) AS 'GrupoVulnerabilidad', "
                Query += "dbo.fn_ObtieneEdad(CONVERT(DATE, C.FechaNacimiento), CONVERT(DATE, '" & fechaF & "')) AS 'Edad', A.Embarazo, "
                Query += "CONVERT(VARCHAR,A.FechaEntrega, 103) AS 'FechaEntrega', A.IdEsquema "
                Query += "FROM ControlARV AS A INNER JOIN "
                Query += "PAC_ID AS B ON A.NHC = B.NHC LEFT OUTER JOIN "
                Query += "PAC_BASALES AS C ON C.IdPaciente = B.IdPaciente "
                'Query += "WHERE A.NHC NOT LIKE '%P%' AND A.Med1_ARVEstatus = 7 "
                Query += "WHERE A.NHC NOT LIKE '%P%' AND A.EsquemaEstatus = 7 "
                Query += "AND A.IdCCARV = (SELECT TOP(1) B.IdCCARV FROM ControlARV AS B "
                Query += "WHERE B.NHC = A.NHC AND B.FechaEntrega = (SELECT TOP(1) C.FechaEntrega FROM ControlARV AS C "
                Query += "WHERE C.NHC = B.NHC AND (C.FechaEntrega BETWEEN '" & fechaI & " 00:00:00.000' AND '" & fechaF & " 23:59:59.999') ORDER BY C.FechaEntrega DESC) "
                Query += "ORDER BY B.IdCCARV DESC)"
            Case "5P" 'TRASLADOS PEDIATRICO
                Query = "SELECT A.NHC, C.Genero, dbo.fn_ObtieneEdad(CONVERT(DATE, C.FechaNacimiento), CONVERT(DATE, '" & fechaF & "')) AS 'Edad', A.Embarazo, "
                Query += "CONVERT(VARCHAR,A.FechaEntrega, 103) AS 'FechaEntrega', A.IdEsquema "
                Query += "FROM ControlARV AS A INNER JOIN "
                Query += "BasalesPediatria AS C ON C.NHC = A.NHC "
                'Query += "WHERE A.NHC LIKE '%P%' AND A.Med1_ARVEstatus = 7 "
                Query += "WHERE A.NHC LIKE '%P%' AND A.EsquemaEstatus = 7 "
                Query += "AND A.IdCCARV = (SELECT TOP(1) B.IdCCARV FROM ControlARV AS B "
                Query += "WHERE B.NHC = A.NHC AND B.FechaEntrega = (SELECT TOP(1) C.FechaEntrega FROM ControlARV AS C "
                Query += "WHERE C.NHC = B.NHC AND (C.FechaEntrega BETWEEN '" & fechaI & " 00:00:00.000' AND '" & fechaF & " 23:59:59.999') ORDER BY C.FechaEntrega DESC) "
                Query += "ORDER BY B.IdCCARV DESC)"
            Case "6" 'INICIOS
                Query = "SELECT A.NHC, C.IdGenero, (SELECT TOP(1) GV.NomGrupoVulnerabilidad  FROM dbo.PAC_CONDUCTARIESGO AS C1 LEFT OUTER JOIN dbo.PAC_M_GRUPOVULNERABILIDAD AS GV ON GV.IdGrupoVulnerabilidad = C1.GrupoVulnerabilidad WHERE C1.IdPaciente = B.IdPaciente AND C1.GrupoVulnerabilidad IS NOT NULL ORDER BY C1.FechaConductaRiesgo DESC ) AS 'GrupoVulnerabilidad', "
                Query += "dbo.fn_ObtieneEdad(CONVERT(DATE, C.FechaNacimiento), CONVERT(DATE, '" & fechaF & "')) AS 'Edad', A.Embarazo, "
                Query += "CONVERT(VARCHAR,A.FechaEntrega, 103) AS 'FechaEntrega', A.IdEsquema "
                Query += "FROM ControlARV AS A LEFT OUTER JOIN "
                Query += "PAC_ID AS B ON A.NHC = B.NHC LEFT OUTER JOIN "
                Query += "PAC_BASALES AS C ON C.IdPaciente = B.IdPaciente "
                'Query += "WHERE A.NHC NOT LIKE '%P%' AND A.Med1_ARVEstatus = 2 "
                Query += "WHERE A.NHC NOT LIKE '%P%' AND A.EsquemaEstatus = 2 "
                Query += "AND A.FechaEntrega BETWEEN '" & fechaI & " 00:00:00.000' AND '" & fechaF & " 23:59:59.999' "
                Query += "ORDER BY A.FechaEntrega DESC"
            Case "6P" 'INICIOS PEDIATRICO
                Query = "SELECT A.NHC, C.Genero, dbo.fn_ObtieneEdad(CONVERT(DATE, C.FechaNacimiento), CONVERT(DATE, '" & fechaF & "')) AS 'Edad', A.Embarazo, "
                Query += "CONVERT(VARCHAR,A.FechaEntrega, 103) AS 'FechaEntrega', A.IdEsquema "
                Query += "FROM ControlARV AS A LEFT OUTER JOIN "
                Query += "BasalesPediatria AS C ON C.NHC = A.NHC "
                'Query += "WHERE A.NHC LIKE '%P%' AND A.Med1_ARVEstatus = 2 "
                Query += "WHERE A.NHC LIKE '%P%' AND A.EsquemaEstatus = 2 "
                Query += "AND A.FechaEntrega BETWEEN '" & fechaI & " 00:00:00.000' AND '" & fechaF & " 23:59:59.999' "
                Query += "ORDER BY A.FechaEntrega DESC"
            Case "7" 'REINICIOS
                Query = "SELECT A.NHC, C.IdGenero, (SELECT TOP(1) GV.NomGrupoVulnerabilidad  FROM dbo.PAC_CONDUCTARIESGO AS C1 LEFT OUTER JOIN dbo.PAC_M_GRUPOVULNERABILIDAD AS GV ON GV.IdGrupoVulnerabilidad = C1.GrupoVulnerabilidad WHERE C1.IdPaciente = B.IdPaciente AND C1.GrupoVulnerabilidad IS NOT NULL ORDER BY C1.FechaConductaRiesgo DESC ) AS 'GrupoVulnerabilidad', "
                Query += "dbo.fn_ObtieneEdad(CONVERT(DATE, C.FechaNacimiento), CONVERT(DATE, '" & fechaF & "')) AS 'Edad', A.Embarazo, "
                Query += "CONVERT(VARCHAR,A.FechaEntrega, 103) AS 'FechaEntrega', A.IdEsquema "
                Query += "FROM ControlARV AS A LEFT OUTER JOIN "
                Query += "PAC_ID AS B ON A.NHC = B.NHC LEFT OUTER JOIN "
                Query += "PAC_BASALES AS C ON C.IdPaciente = B.IdPaciente "
                'Query += "WHERE A.NHC NOT LIKE '%P%' AND A.Med1_ARVEstatus = 3 "
                Query += "WHERE A.NHC NOT LIKE '%P%' AND A.EsquemaEstatus = 3 "
                Query += "AND A.FechaEntrega BETWEEN '" & fechaI & " 00:00:00.000' AND '" & fechaF & " 23:59:59.999' "
                Query += "ORDER BY A.FechaEntrega DESC"
            Case "7P" 'REINICIOS PEDIATRICO
                Query = "SELECT A.NHC, C.Genero, dbo.fn_ObtieneEdad(CONVERT(DATE, C.FechaNacimiento), CONVERT(DATE, '" & fechaF & "')) AS 'Edad', A.Embarazo, "
                Query += "CONVERT(VARCHAR,A.FechaEntrega, 103) AS 'FechaEntrega', A.IdEsquema "
                Query += "FROM ControlARV AS A INNER JOIN "
                Query += "BasalesPediatria AS C ON C.NHC = A.NHC "
                'Query += "WHERE A.NHC LIKE '%P%' AND A.Med1_ARVEstatus = 3 "
                Query += "WHERE  substring(A.NHC,2,1)= 'P' AND A.EsquemaEstatus = 3 "
                Query += "AND A.FechaEntrega BETWEEN convert(DATE,'" & fechaI & "') AND convert(DATE,'" & fechaF & "') "
               ' Query += "WHERE B.NHC = A.NHC AND B.FechaEntrega = (SELECT TOP(1) C.FechaEntrega FROM ControlARV AS C "
               ' Query += "WHERE C.NHC = B.NHC AND (C.FechaEntrega BETWEEN '" & fechaI & " 00:00:00.000' AND '" & fechaF & " 23:59:59.999') ORDER BY C.FechaEntrega DESC) "
                'Query += "ORDER BY B.IdCCARV DESC)"
            Case "8" 'CAMBIOS
                'Query = "SELECT A.NHC, C.IdGenero, dbo.fn_ObtieneEdad(CONVERT(DATE, C.FechaNacimiento), CONVERT(DATE, '" & fechaF & "')) AS 'Edad', A.Embarazo, "
                'Query += "CONVERT(VARCHAR,A.FechaEntrega, 103) AS 'FechaEntrega', A.IdEsquema "
                'Query += "FROM ControlARV AS A LEFT OUTER JOIN "
                'Query += "PAC_ID AS B ON A.NHC = B.NHC LEFT OUTER JOIN "
                'Query += "PAC_BASALES AS C ON C.IdPaciente = B.IdPaciente "
                'Query += "WHERE A.NHC NOT LIKE '%P%' AND (A.Med1_ARVEstatus IN (18, 14) OR A.Med2_ARVEstatus IN (18, 14) OR A.Med3_ARVEstatus IN (18, 14) OR A.Med4_ARVEstatus IN (18, 14) OR A.Med5_ARVEstatus IN (18, 14)) "
                'Query += "AND A.FechaEntrega BETWEEN '" & fechaI & " 00:00:00.000' AND '" & fechaF & " 23:59:59.999' "
                'Query += "ORDER BY A.FechaEntrega DESC"
                Query = "SELECT A.NHC, C.IdGenero, (SELECT TOP(1) GV.NomGrupoVulnerabilidad  FROM dbo.PAC_CONDUCTARIESGO AS C1 LEFT OUTER JOIN dbo.PAC_M_GRUPOVULNERABILIDAD AS GV ON GV.IdGrupoVulnerabilidad = C1.GrupoVulnerabilidad WHERE C1.IdPaciente = B.IdPaciente AND C1.GrupoVulnerabilidad IS NOT NULL ORDER BY C1.FechaConductaRiesgo DESC ) AS 'GrupoVulnerabilidad', "
                Query += "dbo.fn_ObtieneEdad(CONVERT(DATE, C.FechaNacimiento), CONVERT(DATE, '" & fechaF & "')) AS 'Edad', A.Embarazo, "
                Query += "CONVERT(VARCHAR,A.FechaEntrega, 103) AS 'FechaEntrega', A.IdEsquema "
                Query += "FROM ControlARV AS A LEFT OUTER JOIN "
                Query += "PAC_ID AS B ON A.NHC = B.NHC LEFT OUTER JOIN "
                Query += "PAC_BASALES AS C ON C.IdPaciente = B.IdPaciente "
                Query += "WHERE A.NHC NOT LIKE '%P%' AND A.EsquemaEstatus IN (18, 14) "
                Query += "AND A.FechaEntrega BETWEEN '" & fechaI & " 00:00:00.000' AND '" & fechaF & " 23:59:59.999' "
                Query += "ORDER BY A.FechaEntrega DESC"
            Case "8P" 'CAMBIOS PEDIATRICO
                'Query = "SELECT A.NHC, C.Genero, dbo.fn_ObtieneEdad(CONVERT(DATE, C.FechaNacimiento), CONVERT(DATE, '" & fechaF & "')) AS 'Edad', A.Embarazo, "
                'Query += "CONVERT(VARCHAR,A.FechaEntrega, 103) AS 'FechaEntrega', A.IdEsquema "
                'Query += "FROM ControlARV AS A LEFT OUTER JOIN "
                'Query += "BasalesPediatria AS C ON C.NHC = A.NHC "
                'Query += "WHERE A.NHC LIKE '%P%' AND (A.Med1_ARVEstatus IN (18, 14) OR A.Med2_ARVEstatus IN (18, 14) OR A.Med3_ARVEstatus IN (18, 14) OR A.Med4_ARVEstatus IN (18, 14) OR A.Med5_ARVEstatus IN (18, 14) OR A.Med6_ARVEstatus IN (18, 14) OR A.Med7_ARVEstatus IN (18, 14) OR A.Med8_ARVEstatus IN (18, 14)) "
                'Query += "AND A.FechaEntrega BETWEEN '" & fechaI & " 00:00:00.000' AND '" & fechaF & " 23:59:59.999' "
                'Query += "ORDER BY A.FechaEntrega DESC"
                Query = "SELECT A.NHC, C.Genero, dbo.fn_ObtieneEdad(CONVERT(DATE, C.FechaNacimiento), CONVERT(DATE, '" & fechaF & "')) AS 'Edad', A.Embarazo, "
                Query += "CONVERT(VARCHAR,A.FechaEntrega, 103) AS 'FechaEntrega', A.IdEsquema "
                Query += "FROM ControlARV AS A LEFT OUTER JOIN "
                Query += "BasalesPediatria AS C ON C.NHC = A.NHC "
                Query += "WHERE A.NHC LIKE '%P%' AND A.EsquemaEstatus IN (18, 14, 25)"
                Query += "AND A.FechaEntrega BETWEEN '" & fechaI & " 00:00:00.000' AND '" & fechaF & " 23:59:59.999' "
                Query += "ORDER BY A.FechaEntrega DESC"
            Case "9" 'REFERIDOS
                Query = "SELECT A.NHC, C.IdGenero, (SELECT TOP(1) GV.NomGrupoVulnerabilidad  FROM dbo.PAC_CONDUCTARIESGO AS C1 LEFT OUTER JOIN dbo.PAC_M_GRUPOVULNERABILIDAD AS GV ON GV.IdGrupoVulnerabilidad = C1.GrupoVulnerabilidad WHERE C1.IdPaciente = B.IdPaciente AND C1.GrupoVulnerabilidad IS NOT NULL ORDER BY C1.FechaConductaRiesgo DESC ) AS 'GrupoVulnerabilidad', "
                Query += "dbo.fn_ObtieneEdad(CONVERT(DATE, C.FechaNacimiento), CONVERT(DATE, '" & fechaF & "')) AS 'Edad', A.Embarazo, "
                Query += "CONVERT(VARCHAR,A.FechaEntrega, 103) AS 'FechaEntrega', A.IdEsquema "
                Query += "FROM ControlARV AS A LEFT OUTER JOIN "
                Query += "PAC_ID AS B ON A.NHC = B.NHC LEFT OUTER JOIN "
                Query += "PAC_BASALES AS C ON C.IdPaciente = B.IdPaciente "
                'Query += "WHERE A.NHC NOT LIKE '%P%' AND (A.Med1_ARVEstatus IN (18, 14) OR A.Med2_ARVEstatus IN (18, 14) OR A.Med3_ARVEstatus IN (18, 14) OR A.Med4_ARVEstatus IN (18, 14) OR A.Med5_ARVEstatus IN (18, 14)) "
                Query += "WHERE A.NHC NOT LIKE '%P%' AND A.EsquemaEstatus = 13 "
                Query += "AND A.FechaEntrega BETWEEN '" & fechaI & " 00:00:00.000' AND '" & fechaF & " 23:59:59.999' "
                Query += "ORDER BY A.FechaEntrega DESC"
            Case "9P" 'REFERIDOS PEDIATRICO
                Query = "SELECT A.NHC, C.Genero, dbo.fn_ObtieneEdad(CONVERT(DATE, C.FechaNacimiento), CONVERT(DATE, '" & fechaF & "')) AS 'Edad', A.Embarazo, "
                Query += "CONVERT(VARCHAR,A.FechaEntrega, 103) AS 'FechaEntrega', A.IdEsquema "
                Query += "FROM ControlARV AS A LEFT OUTER JOIN "
                Query += "BasalesPediatria AS C ON C.NHC = A.NHC "
                'Query += "WHERE A.NHC LIKE '%P%' AND (A.Med1_ARVEstatus IN (18, 14) OR A.Med2_ARVEstatus IN (18, 14) OR A.Med3_ARVEstatus IN (18, 14) OR A.Med4_ARVEstatus IN (18, 14) OR A.Med5_ARVEstatus IN (18, 14) OR A.Med6_ARVEstatus IN (18, 14) OR A.Med7_ARVEstatus IN (18, 14) OR A.Med8_ARVEstatus IN (18, 14)) "
                Query += "WHERE A.NHC LIKE '%P%' AND A.EsquemaEstatus = 13 "
                Query += "AND A.FechaEntrega BETWEEN '" & fechaI & " 00:00:00.000' AND '" & fechaF & " 23:59:59.999' "
                Query += "ORDER BY A.FechaEntrega DESC"
            Case "10" 'REINGRESOS
                Query = "SELECT A.NHC, C.IdGenero, (SELECT TOP(1) GV.NomGrupoVulnerabilidad  FROM dbo.PAC_CONDUCTARIESGO AS C1 LEFT OUTER JOIN dbo.PAC_M_GRUPOVULNERABILIDAD AS GV ON GV.IdGrupoVulnerabilidad = C1.GrupoVulnerabilidad WHERE C1.IdPaciente = B.IdPaciente AND C1.GrupoVulnerabilidad IS NOT NULL ORDER BY C1.FechaConductaRiesgo DESC ) AS 'GrupoVulnerabilidad', "
                Query += "dbo.fn_ObtieneEdad(CONVERT(DATE, C.FechaNacimiento), CONVERT(DATE, '" & fechaF & "')) AS 'Edad', A.Embarazo, "
                Query += "CONVERT(VARCHAR,A.FechaEntrega, 103) AS 'FechaEntrega', A.IdEsquema "
                Query += "FROM ControlARV AS A LEFT OUTER JOIN "
                Query += "PAC_ID AS B ON A.NHC = B.NHC LEFT OUTER JOIN "
                Query += "PAC_BASALES AS C ON C.IdPaciente = B.IdPaciente "
                'Query += "WHERE A.NHC NOT LIKE '%P%' AND (A.Med1_ARVEstatus IN (18, 14) OR A.Med2_ARVEstatus IN (18, 14) OR A.Med3_ARVEstatus IN (18, 14) OR A.Med4_ARVEstatus IN (18, 14) OR A.Med5_ARVEstatus IN (18, 14)) "
                Query += "WHERE A.NHC NOT LIKE '%P%' AND A.EsquemaEstatus = 24 "
                Query += "AND A.FechaEntrega BETWEEN '" & fechaI & " 00:00:00.000' AND '" & fechaF & " 23:59:59.999' "
                Query += "ORDER BY A.FechaEntrega DESC"
            Case "10P" 'REINGRESOS PEDIATRICO
                Query = "SELECT A.NHC, C.Genero, dbo.fn_ObtieneEdad(CONVERT(DATE, C.FechaNacimiento), CONVERT(DATE, '" & fechaF & "')) AS 'Edad', A.Embarazo, "
                Query += "CONVERT(VARCHAR,A.FechaEntrega, 103) AS 'FechaEntrega', A.IdEsquema "
                Query += "FROM ControlARV AS A LEFT OUTER JOIN "
                Query += "BasalesPediatria AS C ON C.NHC = A.NHC "
                'Query += "WHERE A.NHC LIKE '%P%' AND (A.Med1_ARVEstatus IN (18, 14) OR A.Med2_ARVEstatus IN (18, 14) OR A.Med3_ARVEstatus IN (18, 14) OR A.Med4_ARVEstatus IN (18, 14) OR A.Med5_ARVEstatus IN (18, 14) OR A.Med6_ARVEstatus IN (18, 14) OR A.Med7_ARVEstatus IN (18, 14) OR A.Med8_ARVEstatus IN (18, 14)) "
                Query += "WHERE A.NHC LIKE '%P%' AND A.EsquemaEstatus = 24 "
                Query += "AND A.FechaEntrega BETWEEN '" & fechaI & " 00:00:00.000' AND '" & fechaF & " 23:59:59.999' "
                Query += "ORDER BY A.FechaEntrega DESC"
            Case "11" 'CAMBIOS FORMA FARMACEUTICA
                Query = "SELECT A.NHC, C.IdGenero, (SELECT TOP(1) GV.NomGrupoVulnerabilidad  FROM dbo.PAC_CONDUCTARIESGO AS C1 LEFT OUTER JOIN dbo.PAC_M_GRUPOVULNERABILIDAD AS GV ON GV.IdGrupoVulnerabilidad = C1.GrupoVulnerabilidad WHERE C1.IdPaciente = B.IdPaciente AND C1.GrupoVulnerabilidad IS NOT NULL ORDER BY C1.FechaConductaRiesgo DESC ) AS 'GrupoVulnerabilidad', "
                Query += "dbo.fn_ObtieneEdad(CONVERT(DATE, C.FechaNacimiento), CONVERT(DATE, '" & fechaF & "')) AS 'Edad', A.Embarazo, "
                Query += "CONVERT(VARCHAR,A.FechaEntrega, 103) AS 'FechaEntrega', A.IdEsquema "
                Query += "FROM ControlARV AS A LEFT OUTER JOIN "
                Query += "PAC_ID AS B ON A.NHC = B.NHC LEFT OUTER JOIN "
                Query += "PAC_BASALES AS C ON C.IdPaciente = B.IdPaciente "
                Query += "WHERE A.NHC NOT LIKE '%P%' AND A.EsquemaEstatus = 20 "
                Query += "AND A.FechaEntrega BETWEEN '" & fechaI & " 00:00:00.000' AND '" & fechaF & " 23:59:59.999' "
                Query += "ORDER BY A.FechaEntrega DESC"
            Case "11P" 'CAMBIOS FORMA FARMACEUTICA PEDIATRICO
                Query = "SELECT A.NHC, C.Genero, dbo.fn_ObtieneEdad(CONVERT(DATE, C.FechaNacimiento), CONVERT(DATE, '" & fechaF & "')) AS 'Edad', A.Embarazo, "
                Query += "CONVERT(VARCHAR,A.FechaEntrega, 103) AS 'FechaEntrega', A.IdEsquema "
                Query += "FROM ControlARV AS A LEFT OUTER JOIN "
                Query += "BasalesPediatria AS C ON C.NHC = A.NHC "
                Query += "WHERE A.NHC LIKE '%P%' AND A.EsquemaEstatus = 20"
                Query += "AND A.FechaEntrega BETWEEN '" & fechaI & " 00:00:00.000' AND '" & fechaF & " 23:59:59.999' "
                Query += "ORDER BY A.FechaEntrega DESC"
        End Select
        Dim Ds As New DataSet()
        If Query <> "" Then
            Try
                Using connection As New SqlConnection(_cn1)
                    connection.Open()
                    Dim adapter As New SqlDataAdapter()
                    adapter.SelectCommand = New SqlCommand(Query, connection)
                    adapter.SelectCommand.CommandTimeout = TimeoutDB
                    adapter.Fill(Ds, _page)
                    adapter.Dispose()
                    connection.Dispose()
                    connection.Close()
                End Using
                Return Ds.Tables(0)
            Catch ex As SqlException
                _error = ex.Message
                _pageO = _page & "_" & tipo
                GrabarErrores(usuario & "|" & _pageO & "|" & ex.Number & "|" & ex.Message)
                Return Nothing
            End Try
        Else
            Return Nothing
        End If
    End Function

    Public Function ReportesMensualIOS_ITS(ByVal tipo As String, ByVal fechaI As String, ByVal fechaF As String, ByVal usuario As String) As DataTable
        _page = "db.ReportesMensualIOS_ITS"
        Dim Query As String = String.Empty
        Select Case tipo
            Case "1" 'REPORTE IOS
                'Query = "SELECT  E.NomEnfermedad, E.Enfermedad, "
                'Query += "SUM(CASE WHEN P.IdGenero = 1 THEN 1 ELSE 0 END) AS 'M', "
                'Query += "SUM(CASE WHEN P.IdGenero = 2 THEN 1 ELSE 0 END) AS 'F', "
                'Query += "SUM(CASE WHEN P.IdGenero = 3 THEN 1 ELSE 0 END) AS 'T', "
                'Query += "COUNT(E.Enfermedad) AS TOTAL "
                'Query += "FROM ENFERMEDAD_PAC AS E INNER JOIN "
                'Query += "PAC_BASALES AS P ON E.IdPaciente = P.IdPaciente "
                'Query += "WHERE (E.FechaEnfermedad BETWEEN '" & fechaI & "' AND '" & fechaF & "') AND (E.Enfermedad LIKE 'ED%' OR E.Enfermedad LIKE 'ER%' OR "
                ''Query += "E.Enfermedad IN ('D50', 'B37.3+', 'D07.2', 'A87.2', 'A07.2', 'L21', 'N87.0', 'N87.1', 'B86', 'B16', 'B16.0', 'B16.1', 'B16.9', 'B16.2', 'B17.1', 'B17.2', 'B18', 'B02', 'B39', 'B39.4', 'B39.9', 'B17.0', 'A60', 'B39.5', 'B39.3', 'B39.0', 'B39.1', 'B39.2', 'B00', 'A81.2', 'G03.0', 'G00', 'G00.9', 'G03.1', 'A87.1+', 'B37.5+', 'B38.4+', 'B02.1+', 'B02.1+', 'G03', 'B01.0+', 'G03.8', 'G01*', 'G02.0*', 'G02.1*', 'G02*', 'G02.8*', 'A87.0', 'G00.3', 'G00.2', 'B00.3+', 'A39.0+', 'G00.1', 'G00.0', 'B26.1+', 'A20.3', 'G03.2', 'A17.0+', 'A17.0+', 'A87', 'A87.9', 'A32.1', 'G03.9', 'B08.1', 'J15', 'J15.9', 'J13', 'J12', 'J12.9', 'J18.9', 'B17', 'B17.8', 'G00.8', 'A87.8', 'J15.8', 'J18.8', 'A19.8', 'B97.7', 'B05.1', 'A50', 'A50.1', 'A50.2', 'A50.0', 'A59', 'A59.8', 'A59.9', 'A18', 'A18.8', 'A19.9', 'N76.0', 'L30.9', 'D50.9', 'D51', 'B16', 'B16.0', 'B16.1', 'B16.2', 'B16.9', 'N77.1*', 'N73', 'N73.8', 'N76', 'A53.9', 'A51.3', 'A51.2', 'A18.3')) "
                'Query += "E.Enfermedad IN ('D50', 'B37.3+', 'D07.2', 'A87.2', 'A07.2', 'L21', 'N87.0', 'N87.1', 'B86', 'B16', 'B16.0', 'B16.1', 'B16.9', 'B16.2', 'B17.1', 'B17.2', 'B18', 'B02', 'B39', 'B39.4', 'B39.9', 'B17.0', 'A60', 'B39.5', 'B39.3', 'B39.0', 'B39.1', 'B39.2', 'B00', 'A81.2', 'G03.0', 'G00', 'G00.9', 'G03.1', 'A87.1+', 'B37.5+', 'B38.4+', 'B02.1+', 'B02.1+', 'G03', 'B01.0+', 'G03.8', 'G01*', 'G02.0*', 'G02.1*', 'G02*', 'G02.8*', 'A87.0', 'G00.3', 'G00.2', 'B00.3+', 'A39.0+', 'G00.1', 'G00.0', 'B26.1+', 'A20.3', 'G03.2', 'A17.0+', 'A17.0+', 'A87', 'A87.9', 'A32.1', 'G03.9', 'B08.1', 'J15', 'J15.9', 'J13', 'J12', 'J12.9', 'J18.9', 'B17', 'B17.8', 'G00.8', 'A87.8', 'J15.8', 'J18.8', 'A19.8', 'B97.7', 'B05.1', 'A50', 'A50.1', 'A50.2', 'A50.0', 'A59', 'A59.8', 'A59.9', 'A18', 'A18.8', 'A19.9', 'N76.0', 'L30.9', 'D50.9', 'D51', 'B16', 'B16.0', 'B16.1', 'B16.2', 'B16.9', 'N77.1*', 'N73', 'N73.8', 'N76', 'A53.9', 'A51.3', 'A51.2', 'A18.3','B02.0+', 'O26.4','B02','B02.8','B02.2+','B02.7', 'B02.3', 'B02.9', 'A60', 'A60.9', 'A60.0', 'A60.1', 'B00.9', 'P35.2', 'B00', 'B02.1+', 'B27.0', 'G53.0*', 'H19.1*','A31.1', 'A31.8', 'A31.9', 'B20.0', 'T37.1', 'Y41.1')) "
                'Query += "GROUP BY E.Enfermedad, E.NomEnfermedad "
                'Query += "ORDER BY TOTAL DESC"
                Query = "SELECT L.NomEnfermedad, L.Enfermedad, "
                Query += "ISNULL((SELECT * FROM dbo.fn_RepIOS(L.Enfermedad, 1, 10, 14,'" & fechaI & "','" & fechaF & "')),0) AS 'M1014', "
                Query += "ISNULL((SELECT * FROM dbo.fn_RepIOS(L.Enfermedad, 2, 10, 14,'" & fechaI & "','" & fechaF & "')),0) AS 'F1014', "
                Query += "ISNULL((SELECT * FROM dbo.fn_RepIOS(L.Enfermedad, 3, 10, 14,'" & fechaI & "','" & fechaF & "')),0) AS 'T1014', "
                Query += "ISNULL((SELECT * FROM dbo.fn_RepIOS(L.Enfermedad, 1, 15, 18,'" & fechaI & "','" & fechaF & "')),0) AS 'M1518', "
                Query += "ISNULL((SELECT * FROM dbo.fn_RepIOS(L.Enfermedad, 2, 15, 18,'" & fechaI & "','" & fechaF & "')),0) AS 'F1518', "
                Query += "ISNULL((SELECT * FROM dbo.fn_RepIOS(L.Enfermedad, 3, 15, 18,'" & fechaI & "','" & fechaF & "')),0) AS 'T1518', "
                Query += "ISNULL((SELECT * FROM dbo.fn_RepIOS(L.Enfermedad, 1, 19, 24,'" & fechaI & "','" & fechaF & "')),0) AS 'M1924', "
                Query += "ISNULL((SELECT * FROM dbo.fn_RepIOS(L.Enfermedad, 2, 19, 24,'" & fechaI & "','" & fechaF & "')),0) AS 'F1924', "
                Query += "ISNULL((SELECT * FROM dbo.fn_RepIOS(L.Enfermedad, 3, 19, 24,'" & fechaI & "','" & fechaF & "')),0) AS 'T1924', "
                Query += "ISNULL((SELECT * FROM dbo.fn_RepIOS(L.Enfermedad, 1, 25, 49,'" & fechaI & "','" & fechaF & "')),0) AS 'M2549', "
                Query += "ISNULL((SELECT * FROM dbo.fn_RepIOS(L.Enfermedad, 2, 25, 49,'" & fechaI & "','" & fechaF & "')),0) AS 'F2549', "
                Query += "ISNULL((SELECT * FROM dbo.fn_RepIOS(L.Enfermedad, 3, 25, 49,'" & fechaI & "','" & fechaF & "')),0) AS 'T2549', "
                Query += "ISNULL((SELECT * FROM dbo.fn_RepIOS(L.Enfermedad, 1, 50, 100,'" & fechaI & "','" & fechaF & "')),0) AS 'M50', "
                Query += "ISNULL((SELECT * FROM dbo.fn_RepIOS(L.Enfermedad, 2, 50, 100,'" & fechaI & "','" & fechaF & "')),0) AS 'F50', "
                Query += "ISNULL((SELECT * FROM dbo.fn_RepIOS(L.Enfermedad, 3, 50, 100,'" & fechaI & "','" & fechaF & "')),0) AS 'T50', "
                Query += "ISNULL((SELECT * FROM dbo.fn_RepIOST(L.Enfermedad, 10, 100,'" & fechaI & "','" & fechaF & "')),0) AS 'TOTAL' "
                Query += "FROM dbo.fn_ListadoIOS('" & fechaI & "','" & fechaF & "') AS L "
                Query += "GROUP BY L.Enfermedad, L.NomEnfermedad "
                Query += "ORDER BY TOTAL DESC"
            Case "2" 'LISTA PX IOS
                'Query = "SELECT I.NHC, DATEPART(YEAR, CONVERT(DATETIME, CONVERT(DATE, '01/01/' + SUBSTRING(I.NHC, 5, 2)))) AS 'Cohorte', "
                'Query += "CONVERT(INTEGER, dbo.fn_ObtieneEdad2(P.FechaNacimiento, '" & fechaF & "')) AS 'Edad', P.IdGenero, "
                'Query += "CONVERT(VARCHAR, E.FechaEnfermedad, 103) AS 'FechaEnfermedad', E.Enfermedad, E.NomEnfermedad "
                'Query += "FROM ENFERMEDAD_PAC AS E INNER JOIN "
                'Query += "PAC_BASALES AS P ON E.IdPaciente = P.IdPaciente INNER JOIN "
                'Query += "PAC_ID AS I ON E.IdPaciente = I.IdPaciente "
                'Query += "WHERE (E.FechaEnfermedad BETWEEN '" & fechaI & "' AND '" & fechaF & "') AND (Enfermedad LIKE 'ED%' OR Enfermedad LIKE 'ER%' OR "
                ''Query += "E.Enfermedad IN ('D50', 'B37.3+', 'D07.2', 'A87.2', 'A07.2', 'L21', 'N87.0', 'N87.1', 'B86', 'B16', 'B16.0', 'B16.1', 'B16.9', 'B16.2', 'B17.1', 'B17.2', 'B18', 'B02', 'B39', 'B39.4', 'B39.9', 'B17.0', 'A60', 'B39.5', 'B39.3', 'B39.0', 'B39.1', 'B39.2', 'B00', 'A81.2', 'G03.0', 'G00', 'G00.9', 'G03.1', 'A87.1+', 'B37.5+', 'B38.4+', 'B02.1+', 'B02.1+', 'G03', 'B01.0+', 'G03.8', 'G01*', 'G02.0*', 'G02.1*', 'G02*', 'G02.8*', 'A87.0', 'G00.3', 'G00.2', 'B00.3+', 'A39.0+', 'G00.1', 'G00.0', 'B26.1+', 'A20.3', 'G03.2', 'A17.0+', 'A17.0+', 'A87', 'A87.9', 'A32.1', 'G03.9', 'B08.1', 'J15', 'J15.9', 'J13', 'J12', 'J12.9', 'J18.9', 'B17', 'B17.8', 'G00.8', 'A87.8', 'J15.8', 'J18.8', 'A19.8', 'B97.7', 'B05.1', 'A50', 'A50.1', 'A50.2', 'A50.0', 'A59', 'A59.8', 'A59.9', 'A18', 'A18.8', 'A19.9', 'N76.0', 'L30.9', 'D50.9', 'D51', 'B16', 'B16.0', 'B16.1', 'B16.2', 'B16.9', 'N77.1*', 'N73', 'N73.8', 'N76', 'A53.9', 'A51.3', 'A51.2', 'A18.3'))"
                'Query += "E.Enfermedad IN ('D50', 'B37.3+', 'D07.2', 'A87.2', 'A07.2', 'L21', 'N87.0', 'N87.1', 'B86', 'B16', 'B16.0', 'B16.1', 'B16.9', 'B16.2', 'B17.1', 'B17.2', 'B18', 'B02', 'B39', 'B39.4', 'B39.9', 'B17.0', 'A60', 'B39.5', 'B39.3', 'B39.0', 'B39.1', 'B39.2', 'B00', 'A81.2', 'G03.0', 'G00', 'G00.9', 'G03.1', 'A87.1+', 'B37.5+', 'B38.4+', 'B02.1+', 'B02.1+', 'G03', 'B01.0+', 'G03.8', 'G01*', 'G02.0*', 'G02.1*', 'G02*', 'G02.8*', 'A87.0', 'G00.3', 'G00.2', 'B00.3+', 'A39.0+', 'G00.1', 'G00.0', 'B26.1+', 'A20.3', 'G03.2', 'A17.0+', 'A17.0+', 'A87', 'A87.9', 'A32.1', 'G03.9', 'B08.1', 'J15', 'J15.9', 'J13', 'J12', 'J12.9', 'J18.9', 'B17', 'B17.8', 'G00.8', 'A87.8', 'J15.8', 'J18.8', 'A19.8', 'B97.7', 'B05.1', 'A50', 'A50.1', 'A50.2', 'A50.0', 'A59', 'A59.8', 'A59.9', 'A18', 'A18.8', 'A19.9', 'N76.0', 'L30.9', 'D50.9', 'D51', 'B16', 'B16.0', 'B16.1', 'B16.2', 'B16.9', 'N77.1*', 'N73', 'N73.8', 'N76', 'A53.9', 'A51.3', 'A51.2', 'A18.3','B02.0+', 'O26.4','B02','B02.8','B02.2+','B02.7', 'B02.3', 'B02.9', 'A60', 'A60.9', 'A60.0', 'A60.1', 'B00.9', 'P35.2', 'B00', 'B02.1+', 'B27.0', 'G53.0*', 'H19.1*','A31.1', 'A31.8', 'A31.9', 'B20.0', 'T37.1', 'Y41.1'))"
                Query = "SELECT * FROM dbo.fn_ListadoIOS('" & fechaI & "' ,'" & fechaF & "')"
            Case "3" 'REPORTE ITS
                'Query = "SELECT A.NomAgenteITS AS 'ITS', "
                'Query += "SUM(CASE WHEN P.IdGenero = 1 THEN 1 ELSE 0 END) AS 'M', "
                'Query += "SUM(CASE WHEN P.IdGenero = 2 THEN 1 ELSE 0 END) AS 'F', "
                'Query += "SUM(CASE WHEN P.IdGenero = 3 THEN 1 ELSE 0 END) AS 'T', "
                'Query += "COUNT(E.AgenteITS) AS TOTAL "
                'Query += "FROM ITS AS E INNER JOIN "
                'Query += "ITS_M_AGENTE AS A ON A.IdAgenteITS = E.AgenteITS INNER JOIN "
                'Query += "PAC_BASALES AS P ON E.IdPaciente = P.IdPaciente "
                'Query += "WHERE (E.FechaITS BETWEEN '" & fechaI & "' AND '" & fechaF & "') "
                'Query += "GROUP BY E.AgenteITS, A.NomAgenteITS "
                'Query += "ORDER BY TOTAL DESC"
                '--
                'Query = "SELECT Z.* FROM "
                'Query += "(SELECT A.NomAgenteITS AS 'ITS', "
                'Query += "SUM(CASE WHEN P.IdGenero = 1 THEN 1 ELSE 0 END) AS 'M', "
                'Query += "SUM(CASE WHEN P.IdGenero = 2 THEN 1 ELSE 0 END) AS 'F', "
                'Query += "SUM(CASE WHEN P.IdGenero = 3 THEN 1 ELSE 0 END) AS 'T', "
                'Query += "COUNT(E.AgenteITS) AS TOTAL "
                'Query += "FROM ITS AS E INNER JOIN "
                'Query += "ITS_M_AGENTE AS A ON A.IdAgenteITS = E.AgenteITS INNER JOIN "
                'Query += "PAC_BASALES AS P ON E.IdPaciente = P.IdPaciente "
                'Query += "WHERE (E.FechaITS BETWEEN '" & fechaI & "' AND '" & fechaF & "') "
                'Query += "GROUP BY E.AgenteITS, A.NomAgenteITS "
                'Query += "UNION ALL "
                'Query += "SELECT  E.NomEnfermedad AS 'ITS', "
                'Query += "SUM(CASE WHEN P.IdGenero = 1 THEN 1 ELSE 0 END) AS 'M', "
                'Query += "SUM(CASE WHEN P.IdGenero = 2 THEN 1 ELSE 0 END) AS 'F', "
                'Query += "SUM(CASE WHEN P.IdGenero = 3 THEN 1 ELSE 0 END) AS 'T', "
                'Query += "COUNT(E.Enfermedad) AS TOTAL "
                'Query += "FROM ENFERMEDAD_PAC AS E INNER JOIN "
                'Query += "PAC_BASALES AS P ON E.IdPaciente = P.IdPaciente "
                'Query += "WHERE (E.FechaEnfermedad BETWEEN '" & fechaI & "' AND '" & fechaF & "') AND (E.Enfermedad IN ('N34','N34.1','N34.2','N37.0*','N73.9','A50', 'A50.0', 'A50.1', 'A50.2', 'A50.4', 'A50.5', 'A50.6', 'A50.7', 'A50.9', 'A51', 'A51.0', 'A51.1', 'A51.2', 'A51.3', 'A51.4', 'A51.5', 'A51.9', 'A52', 'A52.0+', 'A52.1', 'A52.2', 'A52.3', 'A52.7', 'A52.8', 'A52.9', 'A53', 'A53.0', 'A53.9', 'A65', 'I98.0*', 'M03.1*', 'N29.0*', 'N74.2*', 'O98.1', 'R76.2')) "
                'Query += "GROUP BY E.Enfermedad, E.NomEnfermedad) AS Z "
                'Query += "ORDER BY Z.TOTAL DESC"
                Query = "SELECT L.ITS, L.AgenteITS, "
                Query += "ISNULL((SELECT * FROM dbo.fn_RepITS(L.AgenteITS, 1, 10, 14,'" & fechaI & "','" & fechaF & "')),0) AS 'M1014', "
                Query += "ISNULL((SELECT * FROM dbo.fn_RepITS(L.AgenteITS, 2, 10, 14,'" & fechaI & "','" & fechaF & "')),0) AS 'F1014', "
                Query += "ISNULL((SELECT * FROM dbo.fn_RepITS(L.AgenteITS, 3, 10, 14,'" & fechaI & "','" & fechaF & "')),0) AS 'T1014', "
                Query += "ISNULL((SELECT * FROM dbo.fn_RepITS(L.AgenteITS, 1, 15, 18,'" & fechaI & "','" & fechaF & "')),0) AS 'M1518', "
                Query += "ISNULL((SELECT * FROM dbo.fn_RepITS(L.AgenteITS, 2, 15, 18,'" & fechaI & "','" & fechaF & "')),0) AS 'F1518', "
                Query += "ISNULL((SELECT * FROM dbo.fn_RepITS(L.AgenteITS, 3, 15, 18,'" & fechaI & "','" & fechaF & "')),0) AS 'T1518', "
                Query += "ISNULL((SELECT * FROM dbo.fn_RepITS(L.AgenteITS, 1, 19, 24,'" & fechaI & "','" & fechaF & "')),0) AS 'M1924', "
                Query += "ISNULL((SELECT * FROM dbo.fn_RepITS(L.AgenteITS, 2, 19, 24,'" & fechaI & "','" & fechaF & "')),0) AS 'F1924', "
                Query += "ISNULL((SELECT * FROM dbo.fn_RepITS(L.AgenteITS, 3, 19, 24,'" & fechaI & "','" & fechaF & "')),0) AS 'T1924', "
                Query += "ISNULL((SELECT * FROM dbo.fn_RepITS(L.AgenteITS, 1, 25, 49,'" & fechaI & "','" & fechaF & "')),0) AS 'M2549', "
                Query += "ISNULL((SELECT * FROM dbo.fn_RepITS(L.AgenteITS, 2, 25, 49,'" & fechaI & "','" & fechaF & "')),0) AS 'F2549', "
                Query += "ISNULL((SELECT * FROM dbo.fn_RepITS(L.AgenteITS, 3, 25, 49,'" & fechaI & "','" & fechaF & "')),0) AS 'T2549', "
                Query += "ISNULL((SELECT * FROM dbo.fn_RepITS(L.AgenteITS, 1, 50, 100,'" & fechaI & "','" & fechaF & "')),0) AS 'M50', "
                Query += "ISNULL((SELECT * FROM dbo.fn_RepITS(L.AgenteITS, 2, 50, 100,'" & fechaI & "','" & fechaF & "')),0) AS 'F50', "
                Query += "ISNULL((SELECT * FROM dbo.fn_RepITS(L.AgenteITS, 3, 50, 100,'" & fechaI & "','" & fechaF & "')),0) AS 'T50', "
                Query += "ISNULL((SELECT * FROM dbo.fn_RepITST(L.AgenteITS, 10, 100,'" & fechaI & "','" & fechaF & "')),0) AS 'TOTAL' "
                Query += "FROM dbo.fn_ListadoITS('" & fechaI & "','" & fechaF & "') AS L "
                Query += "GROUP BY L.AgenteITS, L.ITS "
                Query += "ORDER BY TOTAL DESC"
            Case "4" 'LISTA PX ITS
                'Query = "SELECT I.NHC, DATEPART(YEAR, CONVERT(DATETIME, CONVERT(DATE, '01/01/' + SUBSTRING(I.NHC, 5, 2)))) AS 'Cohorte', "
                'Query += "CONVERT(INTEGER, dbo.fn_ObtieneEdad2(P.FechaNacimiento, '" & fechaF & "')) AS 'Edad', P.IdGenero, "
                'Query += "CONVERT(VARCHAR, E.FechaITS, 103) AS 'FechaITS', E.AgenteITS, A.NomAgenteITS AS 'ITS' "
                'Query += "FROM ITS AS E INNER JOIN "
                'Query += "ITS_M_AGENTE AS A ON A.IdAgenteITS = E.AgenteITS INNER JOIN "
                'Query += "PAC_BASALES AS P ON E.IdPaciente = P.IdPaciente INNER JOIN "
                'Query += "PAC_ID AS I ON E.IdPaciente = I.IdPaciente "
                'Query += "WHERE (E.FechaITS BETWEEN '" & fechaI & "' AND '" & fechaF & "') "
                '--
                'Query = "SELECT Z.* FROM "
                'Query += "(SELECT I.NHC, DATEPART(YEAR, CONVERT(DATETIME, CONVERT(DATE, '01/01/' + SUBSTRING(I.NHC, 5, 2)))) AS 'Cohorte', "
                'Query += "CONVERT(INTEGER, dbo.fn_ObtieneEdad2(P.FechaNacimiento, '" & fechaF & "')) AS 'Edad', P.IdGenero, "
                'Query += "CONVERT(VARCHAR, E.FechaITS, 103) AS 'FechaITS', CONVERT(VARCHAR, E.AgenteITS) AS 'AgenteITS', A.NomAgenteITS AS 'ITS' "
                'Query += "FROM ITS AS E INNER JOIN "
                'Query += "ITS_M_AGENTE AS A ON A.IdAgenteITS = E.AgenteITS INNER JOIN "
                'Query += "PAC_BASALES AS P ON E.IdPaciente = P.IdPaciente INNER JOIN "
                'Query += "PAC_ID AS I ON E.IdPaciente = I.IdPaciente "
                'Query += "WHERE (E.FechaITS BETWEEN '" & fechaI & "' AND '" & fechaF & "') "
                'Query += "UNION ALL "
                'Query += "SELECT I.NHC, DATEPART(YEAR, CONVERT(DATETIME, CONVERT(DATE, '01/01/' + SUBSTRING(I.NHC, 5, 2)))) AS 'Cohorte', "
                'Query += "CONVERT(INTEGER, dbo.fn_ObtieneEdad2(P.FechaNacimiento, '" & fechaF & "')) AS 'Edad', P.IdGenero, "
                'Query += "CONVERT(VARCHAR, E.FechaEnfermedad, 103) AS 'FechaITS', E.Enfermedad AS 'AgenteITS', E.NomEnfermedad AS 'ITS' "
                'Query += "FROM ENFERMEDAD_PAC AS E INNER JOIN "
                'Query += "PAC_BASALES AS P ON E.IdPaciente = P.IdPaciente INNER JOIN "
                'Query += "PAC_ID AS I ON E.IdPaciente = I.IdPaciente "
                'Query += "WHERE (E.FechaEnfermedad BETWEEN '" & fechaI & "' AND '" & fechaF & "') AND (E.Enfermedad IN ('N34','N34.1','N34.2','N37.0*','N73.9','A50', 'A50.0', 'A50.1', 'A50.2', 'A50.4', 'A50.5', 'A50.6', 'A50.7', 'A50.9', 'A51', 'A51.0', 'A51.1', 'A51.2', 'A51.3', 'A51.4', 'A51.5', 'A51.9', 'A52', 'A52.0+', 'A52.1', 'A52.2', 'A52.3', 'A52.7', 'A52.8', 'A52.9', 'A53', 'A53.0', 'A53.9', 'A65', 'I98.0*', 'M03.1*', 'N29.0*', 'N74.2*', 'O98.1', 'R76.2'))) AS Z"
                Query = "SELECT * FROM dbo.fn_ListadoITS('" & fechaI & "','" & fechaF & "')"
        End Select
        Dim Ds As New DataSet()
        If Query <> "" Then
            Try
                Using connection As New SqlConnection(_cn2)
                    connection.Open()
                    Dim adapter As New SqlDataAdapter()
                    adapter.SelectCommand = New SqlCommand(Query, connection)
                    adapter.SelectCommand.CommandTimeout = TimeoutDB
                    adapter.Fill(Ds, _page)
                    adapter.Dispose()
                    connection.Dispose()
                    connection.Close()
                End Using
                Return Ds.Tables(0)
            Catch ex As SqlException
                _error = ex.Message
                _pageO = _page & "_" & tipo
                GrabarErrores(usuario & "|" & _pageO & "|" & ex.Number & "|" & ex.Message)
                Return Nothing
            End Try
        Else
            Return Nothing
        End If
    End Function

    Public Function ReportesMensualNoARV(ByVal tipo As String, ByVal fechaI As String, ByVal fechaF As String, ByVal usuario As String) As DataTable
        _page = "db.ReportesMensualNoARV"
        Dim Query As String = String.Empty
        Select Case tipo
            Case "1" 'NUEVOS
                Query = String.Format("SELECT NHC, Cohorte, IdGenero, GrupoVulnerabilidad, Edad, Paciente FROM dbo.fn_NARV_Nuevos('{0}','{1}')", fechaI, fechaF)
            Case "2" 'REINGRESOS
                Query = String.Format("SELECT NHC, Cohorte, IdGenero, GrupoVulnerabilidad, Edad, Paciente FROM dbo.fn_NARV_Reinicio('{0}','{1}')", fechaI, fechaF)
            Case "3" 'ABANDONOS
                Query = String.Format("SELECT NHC, Cohorte, IdGenero, GrupoVulnerabilidad, Edad, Paciente FROM dbo.fn_NARV_Abandono('{0}','{1}')", fechaI, fechaF)
            Case "4" 'FALLECIDOS
                Query = String.Format("SELECT NHC, Cohorte, IdGenero, GrupoVulnerabilidad, Edad, Paciente FROM dbo.fn_NARV_Fallecidos('{0}','{1}')", fechaI, fechaF)
            Case "5" 'TRASLADOS
                Query = String.Format("SELECT NHC, Cohorte, IdGenero, GrupoVulnerabilidad, Edad, Paciente FROM dbo.fn_NARV_Traslados('{0}','{1}')", fechaI, fechaF)
            Case "6" 'CAMBIO EDAD
                Query = "SELECT NHC, Cohorte, IdGenero, GrupoVulnerabilidad, CONVERT(VARCHAR,FechaNacimiento,103) AS 'FechaNacimiento', Edad, EdadAnterior, Paciente "
                Query += String.Format("FROM dbo.fn_ListaPacientesNARV2('{0}','{1}') ", fechaI, fechaF)
                Query += "WHERE EdadAnterior <> Edad AND EdadAnterior IN (14,24,49)"
            Case "7" 'INICIAN TARV
                Query = String.Format("SELECT NHC, Cohorte, IdGenero, GrupoVulnerabilidad, Edad, Paciente FROM dbo.fn_NARV_InicianTARV('{0}','{1}') ORDER BY IdPaciente ASC", fechaI, fechaF)
            Case "8" 'TOTAL ACTIVOS
                'Query = String.Format("SELECT NHC, Cohorte, IdGenero, Edad, Paciente FROM dbo.fn_ListaPacientesNARV2('{0}','{1}') WHERE IdPaciente IN (SELECT DISTINCT S.idpaciente FROM SIGNOSVITALES AS S WHERE S.FechaVisita <= CONVERT(DATE,'{1}'))", fechaI, fechaF)
                Query = "SELECT DISTINCT Z.NHC, Z.Cohorte, Z.IdGenero, Z.GrupoVulnerabilidad, Z.Edad, Z.Paciente FROM "
                Query += "(SELECT NHC, Cohorte, IdGenero, GrupoVulnerabilidad, Edad, Paciente "
                Query += String.Format("FROM dbo.fn_ListaPacientesNARV2('{0}','{1}') ", fechaI, fechaF)
                Query += String.Format("WHERE IdPaciente IN (SELECT DISTINCT S.idpaciente FROM SIGNOSVITALES AS S WHERE S.FechaVisita <= CONVERT(DATE,'{0}')) ", fechaF)
                Query += "UNION ALL "
                Query += String.Format("SELECT NHC, Cohorte, IdGenero, GrupoVulnerabilidad, Edad, Paciente FROM dbo.fn_NARV_Reinicio('{0}','{1}') WHERE FechaTARV IS NULL) AS Z ", fechaI, fechaF)
                Query += "ORDER BY Z.Cohorte ASC, Z.NHC ASC"
            Case "9" 'NO TARV ACTIVO
                Query = "SELECT I.NHC, DATEPART(YEAR, CONVERT(DATETIME, CONVERT(DATE, '01/01/' + SUBSTRING(I.NHC, 5, 2)))) AS 'Cohorte', "
                Query += String.Format("B.IdGenero, (SELECT TOP(1) GV.NomGrupoVulnerabilidad  FROM PAC_CONDUCTARIESGO AS C1 LEFT OUTER JOIN PAC_M_GRUPOVULNERABILIDAD AS GV ON GV.IdGrupoVulnerabilidad = C1.GrupoVulnerabilidad WHERE C1.IdPaciente = I.IdPaciente AND C1.GrupoVulnerabilidad IS NOT NULL ORDER BY C1.FechaConductaRiesgo DESC ) AS 'GrupoVulnerabilidad', dbo.fn_ObtieneEdad2(CONVERT(DATE, B.FechaNacimiento), '{0}') AS 'Edad', ", fechaF)
                Query += "LTRIM(RTRIM(B.PrimerNombre)) + (CASE WHEN B.SegundoNombre IS NULL THEN '' WHEN B.SegundoNombre = 'SSN' THEN '' ELSE ' ' + "
                Query += "LTRIM(RTRIM(B.SegundoNombre)) END) + ' ' + LTRIM(RTRIM(B.PrimerApellido)) + (CASE WHEN B.SegundoApellido IS NULL THEN '' "
                Query += "WHEN B.SegundoApellido = 'SSA' THEN '' ELSE ' ' + LTRIM(RTRIM(B.SegundoApellido)) END) AS 'Paciente' "
                Query += String.Format("FROM dbo.fn_PacientesNoARVActual('{0}','{1}') AS A INNER JOIN ", fechaI, fechaF)
                Query += "dbo.PAC_ID AS I ON A.IdPaciente = I.Idpaciente INNER JOIN "
                Query += "dbo.PAC_BASALES AS B ON A.IdPaciente = B.IdPaciente"
            Case "10" 'NO TARV/CONSULTA
                Query = String.Format("SELECT NHC, Cohorte, IdGenero, GrupoVulnerabilidad, Edad, Paciente FROM dbo.fn_ListaPacientesNARV2('{0}','{1}') WHERE IdPaciente NOT IN (SELECT DISTINCT idpaciente FROM SIGNOSVITALES)", fechaI, fechaF)
        End Select
        Dim Ds As New DataSet()
        If Query <> "" Then
            Try
                Dim adapter As New SqlDataAdapter()
                If tipo = "9" Then
                    Using connection As New SqlConnection(_cn1)
                        connection.Open()
                        adapter.SelectCommand = New SqlCommand(Query, connection)
                        adapter.SelectCommand.CommandTimeout = TimeoutDB
                        adapter.Fill(Ds, _page)
                        adapter.Dispose()
                        connection.Dispose()
                        connection.Close()
                    End Using
                Else
                    Using connection As New SqlConnection(_cn2)
                        connection.Open()
                        adapter.SelectCommand = New SqlCommand(Query, connection)
                        adapter.SelectCommand.CommandTimeout = TimeoutDB
                        adapter.Fill(Ds, _page)
                        adapter.Dispose()
                        connection.Dispose()
                        connection.Close()
                    End Using
                End If
                Return Ds.Tables(0)
            Catch ex As SqlException
                _error = ex.Message
                _pageO = _page & "_" & tipo
                GrabarErrores(usuario & "|" & _pageO & "|" & ex.Number & "|" & ex.Message)
                Return Nothing
            End Try
        Else
            Return Nothing
        End If
    End Function

    Public Function ReportesMensualNoARVResumen(ByVal tipo As String, ByVal fechaI As String, ByVal fechaF As String, ByVal usuario As String) As DataTable
        _page = "db.ReportesMensualNoARVResumen"
        Dim Query As String = String.Empty
        Select Case tipo
            Case "1" 'NUEVOS
                Query = "SELECT G.NomGenero, "
                Query += String.Format("ISNULL((SELECT * FROM dbo.fn_RepNARV_Nuevos(G.IdGenero, 10, 14,'{0}','{1}')),0) AS 'R1014', ", fechaI, fechaF)
                Query += String.Format("ISNULL((SELECT * FROM dbo.fn_RepNARV_Nuevos(G.IdGenero, 15, 18,'{0}','{1}')),0) AS 'R1518', ", fechaI, fechaF)
                Query += String.Format("ISNULL((SELECT * FROM dbo.fn_RepNARV_Nuevos(G.IdGenero, 19, 24,'{0}','{1}')),0) AS 'R1924', ", fechaI, fechaF)
                Query += String.Format("ISNULL((SELECT * FROM dbo.fn_RepNARV_Nuevos(G.IdGenero, 25, 49,'{0}','{1}')),0) AS 'R2549', ", fechaI, fechaF)
                Query += String.Format("ISNULL((SELECT * FROM dbo.fn_RepNARV_Nuevos(G.IdGenero, 50, 100,'{0}','{1}')),0) AS 'R50', ", fechaI, fechaF)
                Query += String.Format("ISNULL((SELECT * FROM dbo.fn_RepNARV_Nuevos(G.IdGenero, 10, 100,'{0}','{1}')),0) AS 'total' ", fechaI, fechaF)
                Query += "FROM PAC_M_GENERO AS G"
            Case "2" 'REINGRESOS
                Query = "SELECT G.NomGenero, "
                Query += String.Format("ISNULL((SELECT * FROM dbo.fn_RepNARV_Reinicio(G.IdGenero, 10, 14,'{0}','{1}')),0) AS 'R1014', ", fechaI, fechaF)
                Query += String.Format("ISNULL((SELECT * FROM dbo.fn_RepNARV_Reinicio(G.IdGenero, 15, 18,'{0}','{1}')),0) AS 'R1518', ", fechaI, fechaF)
                Query += String.Format("ISNULL((SELECT * FROM dbo.fn_RepNARV_Reinicio(G.IdGenero, 19, 24,'{0}','{1}')),0) AS 'R1924', ", fechaI, fechaF)
                Query += String.Format("ISNULL((SELECT * FROM dbo.fn_RepNARV_Reinicio(G.IdGenero, 25, 49,'{0}','{1}')),0) AS 'R2549', ", fechaI, fechaF)
                Query += String.Format("ISNULL((SELECT * FROM dbo.fn_RepNARV_Reinicio(G.IdGenero, 50, 100,'{0}','{1}')),0) AS 'R50', ", fechaI, fechaF)
                Query += String.Format("ISNULL((SELECT * FROM dbo.fn_RepNARV_Reinicio(G.IdGenero, 10, 100,'{0}','{1}')),0) AS 'total' ", fechaI, fechaF)
                Query += "FROM PAC_M_GENERO AS G"
            Case "3" 'ABANDONOS
                Query = "SELECT G.NomGenero, "
                Query += String.Format("ISNULL((SELECT * FROM dbo.fn_RepNARV_Abandono(G.IdGenero, 10, 14,'{0}','{1}')),0) AS 'R1014', ", fechaI, fechaF)
                Query += String.Format("ISNULL((SELECT * FROM dbo.fn_RepNARV_Abandono(G.IdGenero, 15, 18,'{0}','{1}')),0) AS 'R1518', ", fechaI, fechaF)
                Query += String.Format("ISNULL((SELECT * FROM dbo.fn_RepNARV_Abandono(G.IdGenero, 19, 24,'{0}','{1}')),0) AS 'R1924', ", fechaI, fechaF)
                Query += String.Format("ISNULL((SELECT * FROM dbo.fn_RepNARV_Abandono(G.IdGenero, 25, 49,'{0}','{1}')),0) AS 'R2549', ", fechaI, fechaF)
                Query += String.Format("ISNULL((SELECT * FROM dbo.fn_RepNARV_Abandono(G.IdGenero, 50, 100,'{0}','{1}')),0) AS 'R50', ", fechaI, fechaF)
                Query += String.Format("ISNULL((SELECT * FROM dbo.fn_RepNARV_Abandono(G.IdGenero, 10, 100,'{0}','{1}')),0) AS 'total' ", fechaI, fechaF)
                Query += "FROM PAC_M_GENERO AS G"
            Case "4" 'FALLECIDOS
                Query = "SELECT G.NomGenero, "
                Query += String.Format("ISNULL((SELECT * FROM dbo.fn_RepNARV_Fallecidos(G.IdGenero, 10, 14,'{0}','{1}')),0) AS 'R1014', ", fechaI, fechaF)
                Query += String.Format("ISNULL((SELECT * FROM dbo.fn_RepNARV_Fallecidos(G.IdGenero, 15, 18,'{0}','{1}')),0) AS 'R1518', ", fechaI, fechaF)
                Query += String.Format("ISNULL((SELECT * FROM dbo.fn_RepNARV_Fallecidos(G.IdGenero, 19, 24,'{0}','{1}')),0) AS 'R1924', ", fechaI, fechaF)
                Query += String.Format("ISNULL((SELECT * FROM dbo.fn_RepNARV_Fallecidos(G.IdGenero, 25, 49,'{0}','{1}')),0) AS 'R2549', ", fechaI, fechaF)
                Query += String.Format("ISNULL((SELECT * FROM dbo.fn_RepNARV_Fallecidos(G.IdGenero, 50, 100,'{0}','{1}')),0) AS 'R50', ", fechaI, fechaF)
                Query += String.Format("ISNULL((SELECT * FROM dbo.fn_RepNARV_Fallecidos(G.IdGenero, 10, 100,'{0}','{1}')),0) AS 'total' ", fechaI, fechaF)
                Query += "FROM PAC_M_GENERO AS G"
            Case "5" 'TRASLADOS
                Query = "SELECT G.NomGenero, "
                Query += String.Format("ISNULL((SELECT * FROM dbo.fn_RepNARV_Traslados(G.IdGenero, 10, 14,'{0}','{1}')),0) AS 'R1014', ", fechaI, fechaF)
                Query += String.Format("ISNULL((SELECT * FROM dbo.fn_RepNARV_Traslados(G.IdGenero, 15, 18,'{0}','{1}')),0) AS 'R1518', ", fechaI, fechaF)
                Query += String.Format("ISNULL((SELECT * FROM dbo.fn_RepNARV_Traslados(G.IdGenero, 19, 24,'{0}','{1}')),0) AS 'R1924', ", fechaI, fechaF)
                Query += String.Format("ISNULL((SELECT * FROM dbo.fn_RepNARV_Traslados(G.IdGenero, 25, 49,'{0}','{1}')),0) AS 'R2549', ", fechaI, fechaF)
                Query += String.Format("ISNULL((SELECT * FROM dbo.fn_RepNARV_Traslados(G.IdGenero, 50, 100,'{0}','{1}')),0) AS 'R50', ", fechaI, fechaF)
                Query += String.Format("ISNULL((SELECT * FROM dbo.fn_RepNARV_Traslados(G.IdGenero, 10, 100,'{0}','{1}')),0) AS 'total' ", fechaI, fechaF)
                Query += "FROM PAC_M_GENERO AS G"
            Case "6" 'CAMBIO EDAD
                Query = "SELECT G.NomGenero, "
                Query += String.Format("ISNULL((SELECT * FROM dbo.fn_RepNARV_SCambioEdad(G.IdGenero, 14,'{0}','{1}')),0) AS 'S1014', ", fechaI, fechaF)
                Query += String.Format("ISNULL((SELECT * FROM dbo.fn_RepNARV_ECambioEdad(G.IdGenero, 10,'{0}','{1}')),0) AS 'E1014', ", fechaI, fechaF)
                Query += String.Format("ISNULL((SELECT * FROM dbo.fn_RepNARV_SCambioEdad(G.IdGenero, 18,'{0}','{1}')),0) AS 'S1518', ", fechaI, fechaF)
                Query += String.Format("ISNULL((SELECT * FROM dbo.fn_RepNARV_ECambioEdad(G.IdGenero, 15,'{0}','{1}')),0) AS 'E1518', ", fechaI, fechaF)
                Query += String.Format("ISNULL((SELECT * FROM dbo.fn_RepNARV_SCambioEdad(G.IdGenero, 24,'{0}','{1}')),0) AS 'S1924', ", fechaI, fechaF)
                Query += String.Format("ISNULL((SELECT * FROM dbo.fn_RepNARV_ECambioEdad(G.IdGenero, 19,'{0}','{1}')),0) AS 'E1924', ", fechaI, fechaF)
                Query += String.Format("ISNULL((SELECT * FROM dbo.fn_RepNARV_SCambioEdad(G.IdGenero, 49,'{0}','{1}')),0) AS 'S2549', ", fechaI, fechaF)
                Query += String.Format("ISNULL((SELECT * FROM dbo.fn_RepNARV_ECambioEdad(G.IdGenero, 25,'{0}','{1}')),0) AS 'E2549', ", fechaI, fechaF)
                Query += String.Format("ISNULL((SELECT * FROM dbo.fn_RepNARV_SCambioEdad(G.IdGenero, 100,'{0}','{1}')),0) AS 'S50', ", fechaI, fechaF)
                Query += String.Format("ISNULL((SELECT * FROM dbo.fn_RepNARV_ECambioEdad(G.IdGenero, 50,'{0}','{1}')),0) AS 'E50' ", fechaI, fechaF)
                Query += "FROM PAC_M_GENERO AS G"
            Case "7" 'INICIAN TARV
                Query = "SELECT G.NomGenero, "
                Query += String.Format("ISNULL((SELECT * FROM dbo.fn_RepNARV_InicianTARV(G.IdGenero, 10, 14,'{0}','{1}')),0) AS 'R1014', ", fechaI, fechaF)
                Query += String.Format("ISNULL((SELECT * FROM dbo.fn_RepNARV_InicianTARV(G.IdGenero, 15, 18,'{0}','{1}')),0) AS 'R1518', ", fechaI, fechaF)
                Query += String.Format("ISNULL((SELECT * FROM dbo.fn_RepNARV_InicianTARV(G.IdGenero, 19, 24,'{0}','{1}')),0) AS 'R1924', ", fechaI, fechaF)
                Query += String.Format("ISNULL((SELECT * FROM dbo.fn_RepNARV_InicianTARV(G.IdGenero, 25, 49,'{0}','{1}')),0) AS 'R2549', ", fechaI, fechaF)
                Query += String.Format("ISNULL((SELECT * FROM dbo.fn_RepNARV_InicianTARV(G.IdGenero, 50, 100,'{0}','{1}')),0) AS 'R50', ", fechaI, fechaF)
                Query += String.Format("ISNULL((SELECT * FROM dbo.fn_RepNARV_InicianTARV(G.IdGenero, 10, 100,'{0}','{1}')),0) AS 'total' ", fechaI, fechaF)
                Query += "FROM PAC_M_GENERO AS G"
            Case "8" 'TOTAL ACTIVOS
                'Query = String.Format("SELECT NHC, Cohorte, IdGenero, Edad, Paciente FROM dbo.fn_ListaPacientesNARV2('{0}','{1}') WHERE IdPaciente IN (SELECT DISTINCT S.idpaciente FROM SIGNOSVITALES AS S WHERE S.FechaVisita <= CONVERT(DATE,'{1}'))", fechaI, fechaF)
            Case "9" 'NO TARV ACTIVO
                'Query = "SELECT I.NHC, DATEPART(YEAR, CONVERT(DATETIME, CONVERT(DATE, '01/01/' + SUBSTRING(I.NHC, 5, 2)))) AS 'Cohorte', "
                'Query += String.Format("B.IdGenero, dbo.fn_ObtieneEdad2(CONVERT(DATE, B.FechaNacimiento), '{0}') AS 'Edad', ", fechaF)
                'Query += "LTRIM(RTRIM(B.PrimerNombre)) + (CASE WHEN B.SegundoNombre IS NULL THEN '' WHEN B.SegundoNombre = 'SSN' THEN '' ELSE ' ' + "
                'Query += "LTRIM(RTRIM(B.SegundoNombre)) END) + ' ' + LTRIM(RTRIM(B.PrimerApellido)) + (CASE WHEN B.SegundoApellido IS NULL THEN '' "
                'Query += "WHEN B.SegundoApellido = 'SSA' THEN '' ELSE ' ' + LTRIM(RTRIM(B.SegundoApellido)) END) AS 'Paciente' "
                'Query += String.Format("FROM dbo.fn_PacientesNoARVActual('{0}','{1}') AS A INNER JOIN ", fechaI, fechaF)
                'Query += "dbo.PAC_ID AS I ON A.IdPaciente = I.Idpaciente INNER JOIN "
                'Query += "dbo.PAC_BASALES AS B ON A.IdPaciente = B.IdPaciente"
            Case "10" 'NO TARV/CONSULTA
                'Query = String.Format("SELECT NHC, Cohorte, IdGenero, Edad, Paciente FROM dbo.fn_ListaPacientesNARV2('{0}','{1}') WHERE IdPaciente NOT IN (SELECT DISTINCT idpaciente FROM SIGNOSVITALES)", fechaI, fechaF)
        End Select
        Dim Ds As New DataSet()
        If Query <> "" Then
            Try
                Dim adapter As New SqlDataAdapter()
                If tipo = "9" Then
                    Using connection As New SqlConnection(_cn1)
                        connection.Open()
                        adapter.SelectCommand = New SqlCommand(Query, connection)
                        adapter.SelectCommand.CommandTimeout = TimeoutDB
                        adapter.Fill(Ds, _page)
                        adapter.Dispose()
                        connection.Dispose()
                        connection.Close()
                    End Using
                Else
                    Using connection As New SqlConnection(_cn2)
                        connection.Open()
                        adapter.SelectCommand = New SqlCommand(Query, connection)
                        adapter.SelectCommand.CommandTimeout = TimeoutDB
                        adapter.Fill(Ds, _page)
                        adapter.Dispose()
                        connection.Dispose()
                        connection.Close()
                    End Using
                End If
                Return Ds.Tables(0)
            Catch ex As SqlException
                _error = ex.Message
                _pageO = _page & "_" & tipo
                GrabarErrores(usuario & "|" & _pageO & "|" & ex.Number & "|" & ex.Message)
                Return Nothing
            End Try
        Else
            Return Nothing
        End If
    End Function

    Public Function ReporteConsumos(ByVal tipo As String, ByVal fechaA As String, ByVal fechaM As String, ByVal ultimodia As String, ByVal usuario As String) As DataTable
        _page = "db.ReporteConsumos"
        Dim Query As String = String.Empty
        Dim fechaI As String = fechaA.ToString() & "-" & fechaM.ToString() & "-01"
        Dim fechaF As String = fechaA.ToString() & "-" & fechaM.ToString() & "-" & ultimodia
        Dim fecha As String = fechaA.ToString() & "-" & fechaM.ToString() & "-"
        Dim diafin As Integer = CInt(ultimodia)
        Select Case tipo
            Case "1" 'ARVS
                Query = "SELECT A.IdFFARV, '0' + A.Codigo AS 'Codigo', B.NomARV, C.NomFF, A.Concentracion, "
                For d As Integer = 1 To diafin
                    Query += "(SELECT * FROM dbo.fn_ObtieneCantidadMedFechas(A.IdFFARV,'" & fecha & d & "')) AS '" & Right("0" + d.ToString(), 2) & "', "
                Next
                Query += "(SELECT * FROM dbo.fn_ObtieneCantidadMed(A.IdFFARV,'" & fechaI & "','" & fechaF & "')) AS 'Total' "
                Query += "FROM FFARV AS A INNER JOIN "
                Query += "MedARV AS B ON A.IdARV = B.IdARV INNER JOIN "
                Query += "FormaFarmaceutica AS C ON A.IdFF = C.IdFF "
                Query += "ORDER BY A.Codigo"
            Case "2" 'PROFILAXIS
                Query = "SELECT A.IdFFProf, A.Codigo, B.NomProfilaxis, C.NomFF, A.Concentracion, "
                For d As Integer = 1 To diafin
                    Query += "(SELECT * FROM dbo.fn_ObtieneCantidadMedProfFechas3(A.IdFFProf,'" & fecha & d & "')) AS '" & Right("0" + d.ToString(), 2) & "', "
                Next
                Query += "(SELECT * FROM dbo.fn_ObtieneCantidadMedProf3(A.IdFFProf,'" & fechaI & "','" & fechaF & "')) AS 'Total' "
                Query += "FROM FFProf AS A INNER JOIN "
                Query += "MedProf AS B ON A.IdProf = B.IdProf INNER JOIN "
                Query += "FormaFarmaceutica AS C ON A.IdFF = C.IdFF "
                Query += "ORDER BY A.Codigo"
            Case "3" 'ITS
                Query = "SELECT A.IdFFProf, A.Codigo, B.NomProfilaxis, C.NomFF, A.Concentracion, "
                For d As Integer = 1 To diafin
                    Query += "(SELECT * FROM dbo.fn_ObtieneCantidadMedProfFechas2(1,A.IdFFProf,'" & fecha & d & "')) AS '" & Right("0" + d.ToString(), 2) & "', "
                Next
                Query += "(SELECT * FROM dbo.fn_ObtieneCantidadMedProf2(1,A.IdFFProf,'" & fechaI & "','" & fechaF & "')) AS 'Total' "
                Query += "FROM FFProf AS A INNER JOIN "
                Query += "MedProf AS B ON A.IdProf = B.IdProf INNER JOIN "
                Query += "FormaFarmaceutica AS C ON A.IdFF = C.IdFF "
                Query += "ORDER BY A.Codigo"
            Case "4" 'IO
                Query = "SELECT A.IdFFProf, A.Codigo, B.NomProfilaxis, C.NomFF, A.Concentracion, "
                For d As Integer = 1 To diafin
                    Query += "(SELECT * FROM dbo.fn_ObtieneCantidadMedProfFechas2(2,A.IdFFProf,'" & fecha & d & "')) AS '" & Right("0" + d.ToString(), 2) & "', "
                Next
                Query += "(SELECT * FROM dbo.fn_ObtieneCantidadMedProf2(2,A.IdFFProf,'" & fechaI & "','" & fechaF & "')) AS 'Total' "
                Query += "FROM FFProf AS A INNER JOIN "
                Query += "MedProf AS B ON A.IdProf = B.IdProf INNER JOIN "
                Query += "FormaFarmaceutica AS C ON A.IdFF = C.IdFF "
                Query += "ORDER BY A.Codigo"
            Case "5" 'OTROS
                Query = "SELECT A.IdFFProf, A.Codigo, B.NomProfilaxis, C.NomFF, A.Concentracion, "
                For d As Integer = 1 To diafin
                    Query += "(SELECT * FROM dbo.fn_ObtieneCantidadMedProfFechas2(3,A.IdFFProf,'" & fecha & d & "')) AS '" & Right("0" + d.ToString(), 2) & "', "
                Next
                Query += "(SELECT * FROM dbo.fn_ObtieneCantidadMedProf2(3,A.IdFFProf,'" & fechaI & "','" & fechaF & "')) AS 'Total' "
                Query += "FROM FFProf AS A INNER JOIN "
                Query += "MedProf AS B ON A.IdProf = B.IdProf INNER JOIN "
                Query += "FormaFarmaceutica AS C ON A.IdFF = C.IdFF "
                Query += "ORDER BY A.Codigo"
            Case "6" 'MEDICAMENTOS
                Query = "SELECT A.IdFFProf, A.Codigo, B.NomProfilaxis, C.NomFF, A.Concentracion, "
                For d As Integer = 1 To diafin
                    Query += "(SELECT * FROM dbo.fn_ObtieneCantidadMedProfFechas(A.IdFFProf,'" & fecha & d & "')) AS '" & Right("0" + d.ToString(), 2) & "', "
                Next
                Query += "(SELECT * FROM dbo.fn_ObtieneCantidadMedProf(A.IdFFProf,'" & fechaI & "','" & fechaF & "')) AS 'Total' "
                Query += "FROM FFProf AS A INNER JOIN "
                Query += "MedProf AS B ON A.IdProf = B.IdProf INNER JOIN "
                Query += "FormaFarmaceutica AS C ON A.IdFF = C.IdFF "
                Query += "ORDER BY A.Codigo"
        End Select
        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim adapter As New SqlDataAdapter()
                adapter.SelectCommand = New SqlCommand(Query, connection)
                adapter.SelectCommand.CommandTimeout = TimeoutDB
                adapter.Fill(Ds, _page)
                adapter.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
            Return Ds.Tables(0)
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page & "_" & tipo
            GrabarErrores(usuario & "|" & _pageO & "|" & ex.Number & "|" & ex.Message)
            Return Nothing
        End Try
    End Function

    Public Function ReporteEnfermedades(ByVal tipo As String, ByVal fechaA As String, ByVal fechaM As String, ByVal ultimodia As String, ByVal usuario As String) As DataTable
        _page = "db.ReporteEnfermedades"
        Dim Query As String = String.Empty
        Dim fechaI As String = "01/" & fechaM.ToString() & "/" & fechaA.ToString()
        Dim fechaF As String = ultimodia & "/" & fechaM.ToString() & "/" & fechaA.ToString()
        Dim fecha As String = "/" & fechaM.ToString() & "/" & fechaA.ToString()
        Dim diafin As Integer = CInt(ultimodia)
        Select Case tipo
            Case "1" 'DEFINITORIAS
                Query = "SELECT E.IdEnfermedadDefinitoria AS 'IdDefinitoria', E.NomEnfermedadDefinitoria AS 'Enfermedad', "
                For d As Integer = 1 To diafin
                    Query += "(SELECT * FROM dbo.fn_ObtieneCantidadEnfFecha(E.IdEnfermedadDefinitoria,'" & d & fecha & "')) AS '" & Right("0" + d.ToString(), 2) & "', "
                Next
                Query += "(SELECT * FROM dbo.fn_ObtieneCantidadEnf(E.IdEnfermedadDefinitoria,'" & fechaI & "','" & fechaF & "')) AS 'Total' "
                Query += "FROM M_ENFERMEDAD_DEFINITORIA AS E "
                Query += "WHERE E.IdEnfermedadDefinitoria IN (SELECT DISTINCT Enfermedad FROM ENFERMEDAD_PAC) "
                Query += "ORDER BY E.NomEnfermedadDefinitoria ASC"
            Case "2" 'RELACIONADAS
                Query = "SELECT E.IdEnfermedadRelacionada AS 'IdRelacionada', E.NomEnfermedadRelacionada AS 'Enfermedad', "
                For d As Integer = 1 To diafin
                    Query += "(SELECT * FROM dbo.fn_ObtieneCantidadEnfFecha(E.IdEnfermedadRelacionada,'" & d & fecha & "')) AS '" & Right("0" + d.ToString(), 2) & "', "
                Next
                Query += "(SELECT * FROM dbo.fn_ObtieneCantidadEnf(E.IdEnfermedadRelacionada,'" & fechaI & "','" & fechaF & "')) AS 'Total' "
                Query += "FROM M_ENFERMEDAD_RELACIONADA AS E "
                Query += "WHERE E.IdEnfermedadRelacionada IN (SELECT DISTINCT Enfermedad FROM ENFERMEDAD_PAC) "
                Query += "ORDER BY E.NomEnfermedadRelacionada ASC"
            Case "3" 'GENERAL
                Query = "SELECT E.IdEnfermedadCIM10 AS 'IdCIM10', E.NomEnfermedadCIM10 AS 'Enfermedad', "
                For d As Integer = 1 To diafin
                    Query += "(SELECT * FROM dbo.fn_ObtieneCantidadEnfFecha(E.IdEnfermedadCIM10,'" & d & fecha & "')) AS '" & Right("0" + d.ToString(), 2) & "', "
                Next
                Query += "(SELECT * FROM dbo.fn_ObtieneCantidadEnf(E.IdEnfermedadCIM10,'" & fechaI & "','" & fechaF & "')) AS 'Total' "
                Query += "FROM M_ENFERMEDAD_CIM10 AS E "
                Query += "WHERE E.IdEnfermedadCIM10 <> 'Z21' "
                Query += "AND E.IdEnfermedadCIM10 IN (SELECT DISTINCT Enfermedad FROM ENFERMEDAD_PAC) "
                Query += "ORDER BY E.NomEnfermedadCIM10 ASC"
            Case "4" 'ITS
                Query = "SELECT A.IdAgenteITS, A.NomAgenteITS AS 'ITS', "
                For d As Integer = 1 To diafin
                    Query += "(SELECT * FROM dbo.fn_ObtieneCantidadITSFecha(A.IdAgenteITS,'" & d & fecha & "')) AS '" & Right("0" + d.ToString(), 2) & "', "
                Next
                Query += "(SELECT * FROM dbo.fn_ObtieneCantidadITS(A.IdAgenteITS,'" & fechaI & "','" & fechaF & "')) AS 'Total' "
                Query += "FROM ITS_M_AGENTE AS A "
                Query += "ORDER BY A.NomAgenteITS ASC"
        End Select
        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn2)
                connection.Open()
                Dim adapter As New SqlDataAdapter()
                adapter.SelectCommand = New SqlCommand(Query, connection)
                adapter.SelectCommand.CommandTimeout = TimeoutDB
                adapter.Fill(Ds, _page)
                adapter.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
            Return Ds.Tables(0)
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page & "_" & tipo
            GrabarErrores(usuario & "|" & _pageO & "|" & ex.Number & "|" & ex.Message)
            Return Nothing
        End Try
    End Function

    Public Function ReporteProyeccion(ByVal tipo As String, ByVal fechaA As String, ByVal fechaM As String, ByVal ultimodia As String, ByVal usuario As String) As DataTable
        _page = "db.ReporteProyeccion"
        Dim Query As String = String.Empty
        Dim fechaI As String = fechaA.ToString() & "-" & fechaM.ToString() & "-01"
        Dim fechaF As String = fechaA.ToString() & "-" & fechaM.ToString() & "-" & ultimodia
        Dim fecha As String = fechaA.ToString() & "-" & fechaM.ToString() & "-"
        Dim diafin As Integer = CInt(ultimodia)
        Select Case tipo
            Case "1" 'ARVS
                Query = "SELECT A.IdFFARV, '0' + A.Codigo AS 'Codigo', B.NomARV, C.NomFF, A.Concentracion, "
                For d As Integer = 1 To diafin
                    Query += "(SELECT * FROM dbo.fn_ObtieneCantidadPxMedFechas(A.IdFFARV,'" & fecha & d & "')) AS '" & Right("0" + d.ToString(), 2) & "', "
                Next
                Query += "(SELECT * FROM dbo.fn_ObtieneCantidadPxMed(A.IdFFARV,'" & fechaI & "','" & fechaF & "')) AS 'Total' "
                Query += "FROM FFARV AS A INNER JOIN "
                Query += "MedARV AS B ON A.IdARV = B.IdARV INNER JOIN "
                Query += "FormaFarmaceutica AS C ON A.IdFF = C.IdFF "
                Query += "ORDER BY A.Codigo"
            Case "2" 'PROFILAXIS
                'Query = "SELECT A.IdFFProf, A.Codigo, B.NomProfilaxis, C.NomFF, A.Concentracion, "
                'For d As Integer = 1 To diafin
                '    Query += "(SELECT * FROM dbo.fn_ObtieneCantidadMedProfFechas(A.IdFFProf,'" & fecha & d & "')) AS '" & Right("0" + d.ToString(), 2) & "', "
                'Next
                'Query += "(SELECT * FROM dbo.fn_ObtieneCantidadMedProf(A.IdFFProf,'" & fechaI & "','" & fechaF & "')) AS 'Total' "
                'Query += "FROM FFProf AS A INNER JOIN "
                'Query += "MedProf AS B ON A.IdProf = B.IdProf INNER JOIN "
                'Query += "FormaFarmaceutica AS C ON A.IdFF = C.IdFF "
                'Query += "ORDER BY A.Codigo"
        End Select
        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim adapter As New SqlDataAdapter()
                adapter.SelectCommand = New SqlCommand(Query, connection)
                adapter.SelectCommand.CommandTimeout = TimeoutDB
                adapter.Fill(Ds, _page)
                adapter.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
            Return Ds.Tables(0)
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page & "_" & tipo
            GrabarErrores(usuario & "|" & _pageO & "|" & ex.Number & "|" & ex.Message)
            Return Nothing
        End Try
    End Function

    Public Function RepPxConsumo(ByVal tipo As String, ByVal fechaA As String, ByVal fechaM As String, ByVal ultimodia As String, ByVal usuario As String) As DataTable
        _page = "db.RepPxConsumo"
        Dim Query As String = String.Empty
        Dim fechaI As String = fechaA.ToString() & "-" & fechaM.ToString() & "-01"
        Dim fechaF As String = fechaA.ToString() & "-" & fechaM.ToString() & "-" & ultimodia
        Select Case tipo
            Case "1" 'ARVS
                Query = "SELECT A.NHC, LTRIM(RTRIM(C.PrimerNombre)) + (CASE WHEN C.SegundoNombre IS NULL THEN '' WHEN C.SegundoNombre = 'SSN' THEN '' ELSE ' ' "
                Query += "+ LTRIM(RTRIM(C.SegundoNombre)) END) + ' ' + LTRIM(RTRIM(C.PrimerApellido)) "
                Query += "+ (CASE WHEN C.SegundoApellido IS NULL THEN '' WHEN C.SegundoApellido = 'SSA' THEN '' ELSE ' ' "
                Query += "+ LTRIM(RTRIM(C.SegundoApellido)) END) AS 'Paciente', C.IdGenero, "
                Query += "(SELECT TOP(1) GV.NomGrupoVulnerabilidad  FROM dbo.PAC_CONDUCTARIESGO AS C1 LEFT OUTER JOIN dbo.PAC_M_GRUPOVULNERABILIDAD AS GV ON GV.IdGrupoVulnerabilidad = C1.GrupoVulnerabilidad WHERE C1.IdPaciente = B.IdPaciente AND C1.GrupoVulnerabilidad IS NOT NULL ORDER BY C1.FechaConductaRiesgo DESC ) AS 'GrupoVulnerabilidad', "
                Query += "dbo.fn_ObtieneEdad(CONVERT(DATE, C.FechaNacimiento), CONVERT(DATE, '" & fechaF & "')) AS 'Edad', A.Embarazo, "
                Query += "CONVERT(VARCHAR,A.FechaEntrega, 103) AS 'FechaEntrega', CONVERT(VARCHAR,A.FechaRetorno, 103) AS 'FechaRetorno', "
                Query += "A.IdEsquema, dbo.fn_ObtieneCodigoSEsquema(A.IdSEsquema) AS 'SEsquema', dbo.fn_ObtieneEstatusMed(A.EsquemaEstatus) AS 'Estatus', "
                Query += "'0' + dbo.fn_ObtieneCodMed('A', A.Med1_Codigo) AS 'M1', A.Med1_Cantidad AS 'MC1', dbo.fn_ObtieneEstatusMed(A.Med1_ARVEstatus) AS 'ME1', "
                Query += "'0' + dbo.fn_ObtieneCodMed('A', A.Med2_Codigo) AS 'M2', A.Med2_Cantidad AS 'MC2', dbo.fn_ObtieneEstatusMed(A.Med2_ARVEstatus) AS 'ME2', "
                Query += "'0' + dbo.fn_ObtieneCodMed('A', A.Med3_Codigo) AS 'M3', A.Med3_Cantidad AS 'MC3', dbo.fn_ObtieneEstatusMed(A.Med3_ARVEstatus) AS 'ME3', "
                Query += "'0' + dbo.fn_ObtieneCodMed('A', A.Med4_Codigo) AS 'M4', A.Med4_Cantidad AS 'MC4', dbo.fn_ObtieneEstatusMed(A.Med4_ARVEstatus) AS 'ME4', "
                Query += "'0' + dbo.fn_ObtieneCodMed('A', A.Med5_Codigo) AS 'M5', A.Med5_Cantidad AS 'MC5', dbo.fn_ObtieneEstatusMed(A.Med5_ARVEstatus) AS 'ME5', "
                Query += "'0' + dbo.fn_ObtieneCodMed('A', A.Med6_Codigo) AS 'M6', A.Med6_Cantidad AS 'MC6', dbo.fn_ObtieneEstatusMed(A.Med6_ARVEstatus) AS 'ME6', "
                Query += "'0' + dbo.fn_ObtieneCodMed('A', A.Med7_Codigo) AS 'M7', A.Med7_Cantidad AS 'MC7', dbo.fn_ObtieneEstatusMed(A.Med7_ARVEstatus) AS 'ME7', "
                Query += "'0' + dbo.fn_ObtieneCodMed('A', A.Med8_Codigo) AS 'M8', A.Med8_Cantidad AS 'MC8', dbo.fn_ObtieneEstatusMed(A.Med8_ARVEstatus) AS 'ME8' "
                Query += "FROM ControlARV AS A LEFT OUTER JOIN "
                Query += "PAC_ID AS B ON A.NHC = B.NHC LEFT OUTER JOIN "
                Query += "PAC_BASALES AS C ON C.IdPaciente = B.IdPaciente "
                Query += "WHERE A.NHC NOT LIKE '%P%' AND (A.FechaEntrega BETWEEN '" & fechaI & " 00:00:00.000' AND '" & fechaF & " 23:59:59.999') "
                Query += "UNION "
                Query += "SELECT A.NHC, LTRIM(RTRIM(C.PrimerNombre)) + (CASE WHEN C.SegundoNombre IS NULL THEN '' WHEN C.SegundoNombre = 'SSN' THEN '' ELSE ' ' "
                Query += "+ LTRIM(RTRIM(C.SegundoNombre)) END) + ' ' + LTRIM(RTRIM(C.PrimerApellido)) "
                Query += "+ (CASE WHEN C.SegundoApellido IS NULL THEN '' WHEN C.SegundoApellido = 'SSA' THEN '' ELSE ' ' "
                Query += "+ LTRIM(RTRIM(C.SegundoApellido)) END) AS 'Paciente', C.Genero, C.Conducta_Riesgo AS 'GRUPOVULNERABILIDAD', "
                Query += "dbo.fn_ObtieneEdad(CONVERT(DATE, C.FechaNacimiento), CONVERT(DATE, '" & fechaF & "')) AS 'Edad', '' AS 'Embarazo', "
                Query += "CONVERT(VARCHAR,A.FechaEntrega, 103) AS 'FechaEntrega', CONVERT(VARCHAR,A.FechaRetorno, 103) AS 'FechaRetorno', "
                Query += "A.IdEsquema, dbo.fn_ObtieneCodigoSEsquema(A.IdSEsquema) AS 'SEsquema', dbo.fn_ObtieneEstatusMed(A.EsquemaEstatus) AS 'Estatus', "
                Query += "'0' + dbo.fn_ObtieneCodMed('A', A.Med1_Codigo) AS 'M1', A.Med1_Cantidad AS 'MC1', dbo.fn_ObtieneEstatusMed(A.Med1_ARVEstatus) AS 'ME1', "
                Query += "'0' + dbo.fn_ObtieneCodMed('A', A.Med2_Codigo) AS 'M2', A.Med2_Cantidad AS 'MC2', dbo.fn_ObtieneEstatusMed(A.Med2_ARVEstatus) AS 'ME2', "
                Query += "'0' + dbo.fn_ObtieneCodMed('A', A.Med3_Codigo) AS 'M3', A.Med3_Cantidad AS 'MC3', dbo.fn_ObtieneEstatusMed(A.Med3_ARVEstatus) AS 'ME3', "
                Query += "'0' + dbo.fn_ObtieneCodMed('A', A.Med4_Codigo) AS 'M4', A.Med4_Cantidad AS 'MC4', dbo.fn_ObtieneEstatusMed(A.Med4_ARVEstatus) AS 'ME4', "
                Query += "'0' + dbo.fn_ObtieneCodMed('A', A.Med5_Codigo) AS 'M5', A.Med5_Cantidad AS 'MC5', dbo.fn_ObtieneEstatusMed(A.Med5_ARVEstatus) AS 'ME5', "
                Query += "'0' + dbo.fn_ObtieneCodMed('A', A.Med6_Codigo) AS 'M6', A.Med6_Cantidad AS 'MC6', dbo.fn_ObtieneEstatusMed(A.Med6_ARVEstatus) AS 'ME6', "
                Query += "'0' + dbo.fn_ObtieneCodMed('A', A.Med7_Codigo) AS 'M7', A.Med7_Cantidad AS 'MC7', dbo.fn_ObtieneEstatusMed(A.Med7_ARVEstatus) AS 'ME7', "
                Query += "'0' + dbo.fn_ObtieneCodMed('A', A.Med8_Codigo) AS 'M8', A.Med8_Cantidad AS 'MC8', dbo.fn_ObtieneEstatusMed(A.Med8_ARVEstatus) AS 'ME8' "
                Query += "FROM ControlARV AS A LEFT OUTER JOIN "
                Query += "BasalesPediatria AS C ON C.NHC = A.NHC "
                Query += "WHERE A.NHC LIKE '%P%' AND (A.FechaEntrega BETWEEN '" & fechaI & " 00:00:00.000' AND '" & fechaF & " 23:59:59.999') "
            Case "2" 'PROFILAXIS
                Query = "SELECT A.NHC, LTRIM(RTRIM(C.PrimerNombre)) + (CASE WHEN C.SegundoNombre IS NULL THEN '' WHEN C.SegundoNombre = 'SSN' THEN '' ELSE ' ' "
                Query += "+ LTRIM(RTRIM(C.SegundoNombre)) END) + ' ' + LTRIM(RTRIM(C.PrimerApellido)) "
                Query += "+ (CASE WHEN C.SegundoApellido IS NULL THEN '' WHEN C.SegundoApellido = 'SSA' THEN '' ELSE ' ' "
                Query += "+ LTRIM(RTRIM(C.SegundoApellido)) END) AS 'Paciente', C.IdGenero, "
                Query += "(SELECT TOP(1) GV.NomGrupoVulnerabilidad  FROM dbo.PAC_CONDUCTARIESGO AS C1 LEFT OUTER JOIN dbo.PAC_M_GRUPOVULNERABILIDAD AS GV ON GV.IdGrupoVulnerabilidad = C1.GrupoVulnerabilidad WHERE C1.IdPaciente = B.IdPaciente AND C1.GrupoVulnerabilidad IS NOT NULL ORDER BY C1.FechaConductaRiesgo DESC ) AS 'GrupoVulnerabilidad', "
                Query += "dbo.fn_ObtieneEdad(CONVERT(DATE, C.FechaNacimiento), CONVERT(DATE, '" & fechaF & "')) AS 'Edad', "
                Query += "CONVERT(VARCHAR,A.FechaEntrega, 103) AS 'FechaEntrega', "
                Query += "dbo.fn_ObtieneCodMed('P', A.Prof1_Codigo) AS 'P1', A.Prof1_Cantidad AS 'PC1', dbo.fn_ObtieneTipoMed(A.Prof1_Tipo) AS 'PT1', dbo.fn_ObtieneTTratamiento(A.Prof1_TipoTratamiento) AS 'PTT1', dbo.fn_ObtieneEstatusMed(A.Prof1_Estatus) AS 'PE1', "
                Query += "dbo.fn_ObtieneCodMed('P', A.Prof2_Codigo) AS 'P2', A.Prof2_Cantidad AS 'PC2', dbo.fn_ObtieneTipoMed(A.Prof2_Tipo) AS 'PT2', dbo.fn_ObtieneTTratamiento(A.Prof2_TipoTratamiento) AS 'PTT2', dbo.fn_ObtieneEstatusMed(A.Prof2_Estatus) AS 'PE2', "
                Query += "dbo.fn_ObtieneCodMed('P', A.Prof3_Codigo) AS 'P3', A.Prof3_Cantidad AS 'PC3', dbo.fn_ObtieneTipoMed(A.Prof3_Tipo) AS 'PT3', dbo.fn_ObtieneTTratamiento(A.Prof3_TipoTratamiento) AS 'PTT3', dbo.fn_ObtieneEstatusMed(A.Prof3_Estatus) AS 'PE3', "
                Query += "dbo.fn_ObtieneCodMed('P', A.Prof4_Codigo) AS 'P4', A.Prof4_Cantidad AS 'PC4', dbo.fn_ObtieneTipoMed(A.Prof4_Tipo) AS 'PT4', dbo.fn_ObtieneTTratamiento(A.Prof4_TipoTratamiento) AS 'PTT4', dbo.fn_ObtieneEstatusMed(A.Prof4_Estatus) AS 'PE4', "
                Query += "dbo.fn_ObtieneCodMed('P', A.Prof5_Codigo) AS 'P5', A.Prof5_Cantidad AS 'PC5', dbo.fn_ObtieneTipoMed(A.Prof5_Tipo) AS 'PT5', dbo.fn_ObtieneTTratamiento(A.Prof5_TipoTratamiento) AS 'PTT5', dbo.fn_ObtieneEstatusMed(A.Prof5_Estatus) AS 'PE5', "
                Query += "dbo.fn_ObtieneCodMed('P', A.Prof6_Codigo) AS 'P6', A.Prof6_Cantidad AS 'PC6', dbo.fn_ObtieneTipoMed(A.Prof6_Tipo) AS 'PT6', dbo.fn_ObtieneTTratamiento(A.Prof6_TipoTratamiento) AS 'PTT6', dbo.fn_ObtieneEstatusMed(A.Prof6_Estatus) AS 'PE6'  "
                Query += "FROM ControlPROF AS A LEFT OUTER JOIN "
                Query += "PAC_ID AS B ON A.NHC = B.NHC LEFT OUTER JOIN "
                Query += "PAC_BASALES AS C ON C.IdPaciente = B.IdPaciente "
                Query += "WHERE A.NHC NOT LIKE '%P%' AND (A.FechaEntrega BETWEEN '" & fechaI & " 00:00:00.000' AND '" & fechaF & " 23:59:59.999') "
                Query += "UNION "
                Query += "SELECT A.NHC, LTRIM(RTRIM(C.PrimerNombre)) + (CASE WHEN C.SegundoNombre IS NULL THEN '' WHEN C.SegundoNombre = 'SSN' THEN '' ELSE ' ' "
                Query += "+ LTRIM(RTRIM(C.SegundoNombre)) END) + ' ' + LTRIM(RTRIM(C.PrimerApellido)) "
                Query += "+ (CASE WHEN C.SegundoApellido IS NULL THEN '' WHEN C.SegundoApellido = 'SSA' THEN '' ELSE ' ' "
                Query += "+ LTRIM(RTRIM(C.SegundoApellido)) END) AS 'Paciente', C.Genero, C.Conducta_Riesgo AS 'GRUPOVULNERABILIDAD', "
                Query += "dbo.fn_ObtieneEdad(CONVERT(DATE, C.FechaNacimiento), CONVERT(DATE, '" & fechaF & "')) AS 'Edad', "
                Query += "CONVERT(VARCHAR,A.FechaEntrega, 103) AS 'FechaEntrega', "
                Query += "dbo.fn_ObtieneCodMed('P', A.Prof1_Codigo) AS 'P1', A.Prof1_Cantidad AS 'PC1', dbo.fn_ObtieneTipoMed(A.Prof1_Tipo) AS 'PT1', dbo.fn_ObtieneTTratamiento(A.Prof1_TipoTratamiento) AS 'PTT1', dbo.fn_ObtieneEstatusMed(A.Prof1_Estatus) AS 'PE1', "
                Query += "dbo.fn_ObtieneCodMed('P', A.Prof2_Codigo) AS 'P2', A.Prof2_Cantidad AS 'PC2', dbo.fn_ObtieneTipoMed(A.Prof2_Tipo) AS 'PT2', dbo.fn_ObtieneTTratamiento(A.Prof2_TipoTratamiento) AS 'PTT2', dbo.fn_ObtieneEstatusMed(A.Prof2_Estatus) AS 'PE2', "
                Query += "dbo.fn_ObtieneCodMed('P', A.Prof3_Codigo) AS 'P3', A.Prof3_Cantidad AS 'PC3', dbo.fn_ObtieneTipoMed(A.Prof3_Tipo) AS 'PT3', dbo.fn_ObtieneTTratamiento(A.Prof3_TipoTratamiento) AS 'PTT3', dbo.fn_ObtieneEstatusMed(A.Prof3_Estatus) AS 'PE3', "
                Query += "dbo.fn_ObtieneCodMed('P', A.Prof4_Codigo) AS 'P4', A.Prof4_Cantidad AS 'PC4', dbo.fn_ObtieneTipoMed(A.Prof4_Tipo) AS 'PT4', dbo.fn_ObtieneTTratamiento(A.Prof4_TipoTratamiento) AS 'PTT4', dbo.fn_ObtieneEstatusMed(A.Prof4_Estatus) AS 'PE4', "
                Query += "dbo.fn_ObtieneCodMed('P', A.Prof5_Codigo) AS 'P5', A.Prof5_Cantidad AS 'PC5', dbo.fn_ObtieneTipoMed(A.Prof5_Tipo) AS 'PT5', dbo.fn_ObtieneTTratamiento(A.Prof5_TipoTratamiento) AS 'PTT5', dbo.fn_ObtieneEstatusMed(A.Prof5_Estatus) AS 'PE5', "
                Query += "dbo.fn_ObtieneCodMed('P', A.Prof6_Codigo) AS 'P6', A.Prof6_Cantidad AS 'PC6', dbo.fn_ObtieneTipoMed(A.Prof6_Tipo) AS 'PT6', dbo.fn_ObtieneTTratamiento(A.Prof6_TipoTratamiento) AS 'PTT6', dbo.fn_ObtieneEstatusMed(A.Prof6_Estatus) AS 'PE6'  "
                Query += "FROM ControlPROF AS A LEFT OUTER JOIN "
                Query += "BasalesPediatria AS C ON C.NHC = A.NHC "
                Query += "WHERE A.NHC LIKE '%P%' AND (A.FechaEntrega BETWEEN '" & fechaI & " 00:00:00.000' AND '" & fechaF & " 23:59:59.999')"
        End Select
        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim adapter As New SqlDataAdapter()
                adapter.SelectCommand = New SqlCommand(Query, connection)
                adapter.SelectCommand.CommandTimeout = TimeoutDB
                adapter.Fill(Ds, _page)
                adapter.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
            Return Ds.Tables(0)
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page & "_" & tipo
            GrabarErrores(usuario & "|" & _pageO & "|" & ex.Number & "|" & ex.Message)
            Return Nothing
        End Try
    End Function
	
Public Function Reporte90dias(ByVal fecha As String, ByVal ultimodia As String, ByVal usuario As String) As DataTable
        _page = "db.Reporte90dias"
        'Dim fecha As String = ultimodia & "/" & fechaM.ToString() & "/" & fechaA.ToString()
        Dim Query As String = "SELECT NHC, Cohorte, Paciente, CONVERT(VARCHAR,UltimaFechaEntrega,103) AS 'UltimaFechaEntrega', CONVERT(VARCHAR,FechaRetorno,103) AS 'FechaRetorno', ME, Dias, CitaMedica, CitaFarmacia "
        Query += "FROM dbo.fn_PacientesAbandono('" & fecha & "') "
        Query += "WHERE NOT EstatusM IN ('SFT','SO','SFF','A','T','F') AND ME NOT IN (6, 7, 12, 21) AND FechaRetorno <= '" & fecha & "' AND Dias <= -1"
        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim adapter As New SqlDataAdapter()
                adapter.SelectCommand = New SqlCommand(Query, connection)
                adapter.SelectCommand.CommandTimeout = TimeoutDB
                adapter.Fill(Ds, _page)
                adapter.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
            Return Ds.Tables(0)
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page & "_" & fecha
            GrabarErrores(usuario & "|" & _pageO & "|" & ex.Number & "|" & ex.Message)
            Return Nothing
        End Try
    End Function
	
    Public Function Reportediario90dias(ByVal fechaA As String, ByVal fechaM As String, ByVal dia As String, ByVal usuario As String) As DataTable
        _page = "db.Reportediario90dias"
        Dim fecha As String = dia & "/" & fechaM.ToString() & "/" & fechaA.ToString()
        Dim Query As String = "SELECT NHC, Cohorte, Paciente, CONVERT(VARCHAR,UltimaFechaEntrega,103) AS 'UltimaFechaEntrega', CONVERT(VARCHAR,FechaRetorno,103) AS 'FechaRetorno', ME, Dias "
        Query += "FROM dbo.fn_PacientesAbandono('" & fecha & "') "
        Query += "WHERE ME NOT IN (6, 7, 12, 21) AND Dias <= -90"
        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim adapter As New SqlDataAdapter()
                adapter.SelectCommand = New SqlCommand(Query, connection)
                adapter.SelectCommand.CommandTimeout = TimeoutDB
                adapter.Fill(Ds, _page)
                adapter.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
            Return Ds.Tables(0)
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page & "_" & fecha
            GrabarErrores(usuario & "|" & _pageO & "|" & ex.Number & "|" & ex.Message)
            Return Nothing
        End Try
    End Function

    Public Function ReporteVisitas(ByVal fecha As String, ByVal usuario As String) As DataTable
        _page = "db.ReporteVisitas"
        Dim Query As String = "SELECT * FROM dbo.fn_VisitasFarmacia('" & fecha & "') ORDER BY Cohorte, NHC"
        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim adapter As New SqlDataAdapter()
                adapter.SelectCommand = New SqlCommand(Query, connection)
                adapter.SelectCommand.CommandTimeout = TimeoutDB
                adapter.Fill(Ds, _page)
                adapter.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
            Return Ds.Tables(0)
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page & "_" & fecha
            GrabarErrores(usuario & "|" & _pageO & "|" & ex.Number & "|" & ex.Message)
            Return Nothing
        End Try
    End Function

    Public Function ReporteNoARV(ByVal tipo As String, ByVal fechaA As String, ByVal fechaM As String, ByVal ultimodia As String, ByVal usuario As String) As DataTable
        _page = "db.ReporteNoARV"
        Dim Query As String = String.Empty
        Dim fechaI As String = fechaA.ToString() & "-" & fechaM.ToString() & "-01"
        Dim fechaF As String = fechaA.ToString() & "-" & fechaM.ToString() & "-" & ultimodia
        Dim fecha As String = fechaA.ToString() & "-" & fechaM.ToString() & "-"
        Dim diafin As Integer = CInt(ultimodia)
        Select Case tipo
            Case "1" 'Reporte
                Query = "SELECT * FROM dbo.fn_ReportePacientesNARV('" & fechaF & "')"
            Case "2" 'Listado
                Query = "SELECT Paciente, NHC, Cohorte, IdGenero, Edad FROM dbo.fn_ListaPacientesNARV('" & fechaF & "')"
        End Select
        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn2)
                connection.Open()
                Dim adapter As New SqlDataAdapter()
                adapter.SelectCommand = New SqlCommand(Query, connection)
                adapter.SelectCommand.CommandTimeout = TimeoutDB
                adapter.Fill(Ds, _page)
                adapter.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
            Return Ds.Tables(0)
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page & "_" & tipo
            GrabarErrores(usuario & "|" & _pageO & "|" & ex.Number & "|" & ex.Message)
            Return Nothing
        End Try
    End Function

    Public Function ReporteNoARV1(ByVal tipo As String, ByVal fechaA As String, ByVal fechaM As String, ByVal ultimodia As String, ByVal fechaAA As String, ByVal fechaMA As String, ByVal ultimodiaA As String, ByVal usuario As String) As DataTable
        _page = "db.ReporteNoARV1"
        Dim Query As String = String.Empty
        'Dim fechaI As String = fechaA.ToString() & "-" & fechaM.ToString() & "-01"
        'Dim fechaF As String = fechaA.ToString() & "-" & fechaM.ToString() & "-" & ultimodia
        'Dim fecha As String = fechaA.ToString() & "-" & fechaM.ToString() & "-"
        'Dim diafin As Integer = CInt(ultimodia)
        'Dim fechaAnterior As String = fechaAA.ToString() & "-" & fechaMA.ToString() & "-" & ultimodiaA
        Dim fechaI As String = "01/" & fechaM.ToString() & "/" & fechaA.ToString()
        Dim fechaF As String = ultimodia & "/" & fechaM.ToString() & "/" & fechaA.ToString()
        Dim fecha As String = "/" & fechaM.ToString() & "/" & fechaA.ToString()
        Dim diafin As Integer = CInt(ultimodia)
        Dim fechaAnterior As String = ultimodiaA & "/" & fechaMA.ToString() & "/" & fechaAA.ToString()
        Select Case tipo
            Case "1014"
                Query = "SELECT G.NomGenero, "
                Query += "ISNULL((SELECT * FROM dbo.fn_RepNARV_Nuevos(G.IdGenero, 10, 14,'" & fechaI & "','" & fechaF & "')),0) AS 'Nuevos', "
                Query += "ISNULL((SELECT * FROM dbo.fn_RepNARV_Reinicio(G.IdGenero, 10, 14,'" & fechaI & "','" & fechaF & "')),0) AS 'Reingresos', "
                Query += "ISNULL((SELECT * FROM dbo.fn_RepNARV_Abandono(G.IdGenero, 10, 14,'" & fechaI & "','" & fechaF & "')),0) AS 'Abandonos', "
                Query += "ISNULL((SELECT * FROM dbo.fn_RepNARV_Fallecidos(G.IdGenero, 10, 14,'" & fechaI & "','" & fechaF & "')),0) AS 'Fallecidos', "
                Query += "ISNULL((SELECT * FROM dbo.fn_RepNARV_Traslados(G.IdGenero, 10, 14,'" & fechaI & "','" & fechaF & "')),0) AS 'Traslados', "
                Query += "ISNULL((SELECT * FROM dbo.fn_RepNARV_SCambioEdad(G.IdGenero, 14,'" & fechaAnterior & "','" & fechaF & "')),0) AS 'SalenCambioEdad', "
                Query += "ISNULL((SELECT * FROM dbo.fn_RepNARV_ECambioEdad(G.IdGenero, 10,'" & fechaAnterior & "','" & fechaF & "')),0) AS 'EntranCambioEdad', "
                Query += "ISNULL((SELECT * FROM dbo.fn_RepNARV_InicianTARV(G.IdGenero, 10, 14,'" & fechaI & "','" & fechaF & "')),0) AS 'InicianTARV' "
                Query += "FROM PAC_M_GENERO AS G"
            Case "1518"
                Query = "SELECT G.NomGenero, "
                Query += "ISNULL((SELECT * FROM dbo.fn_RepNARV_Nuevos(G.IdGenero, 15, 18,'" & fechaI & "','" & fechaF & "')),0) AS 'Nuevos', "
                Query += "ISNULL((SELECT * FROM dbo.fn_RepNARV_Reinicio(G.IdGenero, 15, 18,'" & fechaI & "','" & fechaF & "')),0) AS 'Reingresos', "
                Query += "ISNULL((SELECT * FROM dbo.fn_RepNARV_Abandono(G.IdGenero, 15, 18,'" & fechaI & "','" & fechaF & "')),0) AS 'Abandonos', "
                Query += "ISNULL((SELECT * FROM dbo.fn_RepNARV_Fallecidos(G.IdGenero, 15, 18,'" & fechaI & "','" & fechaF & "')),0) AS 'Fallecidos', "
                Query += "ISNULL((SELECT * FROM dbo.fn_RepNARV_Traslados(G.IdGenero, 15, 18,'" & fechaI & "','" & fechaF & "')),0) AS 'Traslados', "
                Query += "ISNULL((SELECT * FROM dbo.fn_RepNARV_SCambioEdad(G.IdGenero, 18,'" & fechaAnterior & "','" & fechaF & "')),0) AS 'SalenCambioEdad', "
                Query += "ISNULL((SELECT * FROM dbo.fn_RepNARV_ECambioEdad(G.IdGenero, 15,'" & fechaAnterior & "','" & fechaF & "')),0) AS 'EntranCambioEdad', "
                Query += "ISNULL((SELECT * FROM dbo.fn_RepNARV_InicianTARV(G.IdGenero, 15, 18,'" & fechaI & "','" & fechaF & "')),0) AS 'InicianTARV' "
                Query += "FROM PAC_M_GENERO AS G"
            Case "1924"
                Query = "SELECT G.NomGenero, "
                Query += "ISNULL((SELECT * FROM dbo.fn_RepNARV_Nuevos(G.IdGenero, 19, 24,'" & fechaI & "','" & fechaF & "')),0) AS 'Nuevos', "
                Query += "ISNULL((SELECT * FROM dbo.fn_RepNARV_Reinicio(G.IdGenero, 19, 24,'" & fechaI & "','" & fechaF & "')),0) AS 'Reingresos', "
                Query += "ISNULL((SELECT * FROM dbo.fn_RepNARV_Abandono(G.IdGenero, 19, 24,'" & fechaI & "','" & fechaF & "')),0) AS 'Abandonos', "
                Query += "ISNULL((SELECT * FROM dbo.fn_RepNARV_Fallecidos(G.IdGenero, 19, 24,'" & fechaI & "','" & fechaF & "')),0) AS 'Fallecidos', "
                Query += "ISNULL((SELECT * FROM dbo.fn_RepNARV_Traslados(G.IdGenero, 19, 24,'" & fechaI & "','" & fechaF & "')),0) AS 'Traslados', "
                Query += "ISNULL((SELECT * FROM dbo.fn_RepNARV_SCambioEdad(G.IdGenero, 24,'" & fechaAnterior & "','" & fechaF & "')),0) AS 'SalenCambioEdad', "
                Query += "ISNULL((SELECT * FROM dbo.fn_RepNARV_ECambioEdad(G.IdGenero, 19,'" & fechaAnterior & "','" & fechaF & "')),0) AS 'EntranCambioEdad', "
                Query += "ISNULL((SELECT * FROM dbo.fn_RepNARV_InicianTARV(G.IdGenero, 19, 24,'" & fechaI & "','" & fechaF & "')),0) AS 'InicianTARV' "
                Query += "FROM PAC_M_GENERO AS G"
            Case "2549"
                Query = "SELECT G.NomGenero, "
                Query += "ISNULL((SELECT * FROM dbo.fn_RepNARV_Nuevos(G.IdGenero, 25, 49,'" & fechaI & "','" & fechaF & "')),0) AS 'Nuevos', "
                Query += "ISNULL((SELECT * FROM dbo.fn_RepNARV_Reinicio(G.IdGenero, 25, 49,'" & fechaI & "','" & fechaF & "')),0) AS 'Reingresos', "
                Query += "ISNULL((SELECT * FROM dbo.fn_RepNARV_Abandono(G.IdGenero, 25, 49,'" & fechaI & "','" & fechaF & "')),0) AS 'Abandonos', "
                Query += "ISNULL((SELECT * FROM dbo.fn_RepNARV_Fallecidos(G.IdGenero, 25, 49,'" & fechaI & "','" & fechaF & "')),0) AS 'Fallecidos', "
                Query += "ISNULL((SELECT * FROM dbo.fn_RepNARV_Traslados(G.IdGenero, 25, 49,'" & fechaI & "','" & fechaF & "')),0) AS 'Traslados', "
                Query += "ISNULL((SELECT * FROM dbo.fn_RepNARV_SCambioEdad(G.IdGenero, 49,'" & fechaAnterior & "','" & fechaF & "')),0) AS 'SalenCambioEdad', "
                Query += "ISNULL((SELECT * FROM dbo.fn_RepNARV_ECambioEdad(G.IdGenero, 25,'" & fechaAnterior & "','" & fechaF & "')),0) AS 'EntranCambioEdad', "
                Query += "ISNULL((SELECT * FROM dbo.fn_RepNARV_InicianTARV(G.IdGenero, 25, 49,'" & fechaI & "','" & fechaF & "')),0) AS 'InicianTARV' "
                Query += "FROM PAC_M_GENERO AS G"
            Case "50"
                Query = "SELECT G.NomGenero, "
                Query += "ISNULL((SELECT * FROM dbo.fn_RepNARV_Nuevos(G.IdGenero, 50, 100,'" & fechaI & "','" & fechaF & "')),0) AS 'Nuevos', "
                Query += "ISNULL((SELECT * FROM dbo.fn_RepNARV_Reinicio(G.IdGenero, 50, 100,'" & fechaI & "','" & fechaF & "')),0) AS 'Reingresos', "
                Query += "ISNULL((SELECT * FROM dbo.fn_RepNARV_Abandono(G.IdGenero, 50, 100,'" & fechaI & "','" & fechaF & "')),0) AS 'Abandonos', "
                Query += "ISNULL((SELECT * FROM dbo.fn_RepNARV_Fallecidos(G.IdGenero, 50, 100,'" & fechaI & "','" & fechaF & "')),0) AS 'Fallecidos', "
                Query += "ISNULL((SELECT * FROM dbo.fn_RepNARV_Traslados(G.IdGenero, 50, 100,'" & fechaI & "','" & fechaF & "')),0) AS 'Traslados', "
                Query += "ISNULL((SELECT * FROM dbo.fn_RepNARV_SCambioEdad(G.IdGenero, 100,'" & fechaAnterior & "','" & fechaF & "')),0) AS 'SalenCambioEdad', "
                Query += "ISNULL((SELECT * FROM dbo.fn_RepNARV_ECambioEdad(G.IdGenero, 50,'" & fechaAnterior & "','" & fechaF & "')),0) AS 'EntranCambioEdad', "
                Query += "ISNULL((SELECT * FROM dbo.fn_RepNARV_InicianTARV(G.IdGenero, 50, 100,'" & fechaI & "','" & fechaF & "')),0) AS 'InicianTARV' "
                Query += "FROM PAC_M_GENERO AS G"
            Case "Total"
                Query = "SELECT G.NomGenero, "
                Query += "ISNULL((SELECT * FROM dbo.fn_RepNARV_Nuevos(G.IdGenero, 10, 100,'" & fechaI & "','" & fechaF & "')),0) AS 'Nuevos', "
                Query += "ISNULL((SELECT * FROM dbo.fn_RepNARV_Reinicio(G.IdGenero, 10, 100,'" & fechaI & "','" & fechaF & "')),0) AS 'Reingresos', "
                Query += "ISNULL((SELECT * FROM dbo.fn_RepNARV_Abandono(G.IdGenero, 10, 100,'" & fechaI & "','" & fechaF & "')),0) AS 'Abandonos', "
                Query += "ISNULL((SELECT * FROM dbo.fn_RepNARV_Fallecidos(G.IdGenero, 10, 100,'" & fechaI & "','" & fechaF & "')),0) AS 'Fallecidos', "
                Query += "ISNULL((SELECT * FROM dbo.fn_RepNARV_Traslados(G.IdGenero, 10, 100,'" & fechaI & "','" & fechaF & "')),0) AS 'Traslados', "
                Query += "ISNULL((SELECT * FROM dbo.fn_RepNARV_InicianTARV(G.IdGenero, 10, 100,'" & fechaI & "','" & fechaF & "')),0) AS 'InicianTARV' "
                Query += "FROM PAC_M_GENERO AS G"
        End Select
        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn2)
                connection.Open()
                Dim adapter As New SqlDataAdapter()
                adapter.SelectCommand = New SqlCommand(Query, connection)
                adapter.SelectCommand.CommandTimeout = TimeoutDB
                adapter.Fill(Ds, _page)
                adapter.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
            Return Ds.Tables(0)
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page & "_" & tipo
            GrabarErrores(usuario & "|" & _pageO & "|" & ex.Number & "|" & ex.Message)
            Return Nothing
        End Try
    End Function

    Public Function ReporteConsultasPX(ByVal fechaA As String, ByVal fechaM As String, ByVal ultimodia As String, ByVal usuario As String) As DataTable
        _page = "db.ReporteConsultasPX"
        Dim Query As String = String.Empty
        Dim fechaI As String = "01/" & fechaM.ToString().PadLeft(2, "0") & "/" & fechaA.ToString()
        Dim fechaF As String = ultimodia & "/" & fechaM.ToString().PadLeft(2, "0") & "/" & fechaA.ToString()
        Dim fechaT As String = fechaM.ToString().PadLeft(2, "0") & "/" & fechaA.ToString().Substring(2, 2)
        Query = String.Format("SELECT * FROM fn_TotalConsultasPacienteXMES2('{0}','{1}','{2}')", fechaT, fechaI, fechaF)
        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim adapter As New SqlDataAdapter()
                adapter.SelectCommand = New SqlCommand(Query, connection)
                adapter.SelectCommand.CommandTimeout = TimeoutDB
                adapter.Fill(Ds, _page)
                adapter.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
            Return Ds.Tables(0)
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page & "_" & fechaT
            GrabarErrores(usuario & "|" & _pageO & "|" & ex.Number & "|" & ex.Message)
            Return Nothing
        End Try
    End Function

    Public Function ListaConsultasPX(ByVal fechaA As String, ByVal fechaM As String, ByVal ultimodia As String, ByVal usuario As String) As DataTable
        _page = "db.ListaConsultasPX"
        Dim Query As String = String.Empty
        Dim fechaI As String = "01/" & fechaM.ToString().PadLeft(2, "0") & "/" & fechaA.ToString()
        Dim fechaF As String = ultimodia & "/" & fechaM.ToString().PadLeft(2, "0") & "/" & fechaA.ToString()
        Query = String.Format("SELECT NHC, Cohorte, CONVERT(VARCHAR,FechaVisita,103) AS 'FechaVisita', Edad, IdGenero, GrupoVulnerabilidad, Embarazo, ARV FROM fn_ConsultaPacienteXFechas2('{0}','{1}') ORDER BY Cohorte, NHC ASC", fechaI, fechaF)
        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim adapter As New SqlDataAdapter()
                adapter.SelectCommand = New SqlCommand(Query, connection)
                adapter.SelectCommand.CommandTimeout = TimeoutDB
                adapter.Fill(Ds, _page)
                adapter.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
            Return Ds.Tables(0)
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page
            GrabarErrores(usuario & "|" & _pageO & "|" & ex.Number & "|" & ex.Message)
            Return Nothing
        End Try
    End Function

    Public Function ReporteCambioEdad(ByVal fechaI As String, ByVal fechaF As String, ByVal usuario As String) As DataTable
        _page = "db.ReporteCambioEdad"
        Dim Q As New StringBuilder()
        Q.Append("SELECT Z.* FROM ")
        Q.Append("(SELECT A.NHC, LTRIM(RTRIM(C.PrimerNombre)) + (CASE WHEN C.SegundoNombre IS NULL THEN '' WHEN C.SegundoNombre = 'SSN' THEN '' ELSE ' ' ")
        Q.Append("+ LTRIM(RTRIM(C.SegundoNombre)) END) + ' ' + LTRIM(RTRIM(C.PrimerApellido)) ")
        Q.Append("+ (CASE WHEN C.SegundoApellido IS NULL THEN '' WHEN C.SegundoApellido = 'SSA' THEN '' ELSE ' ' ")
        Q.Append("+ LTRIM(RTRIM(C.SegundoApellido)) END) AS 'Paciente', ")
        Q.Append("C.IdGenero, ")
        Q.Append("(SELECT TOP(1) GV.NomGrupoVulnerabilidad  FROM dbo.PAC_CONDUCTARIESGO AS C1 LEFT OUTER JOIN dbo.PAC_M_GRUPOVULNERABILIDAD AS GV ON GV.IdGrupoVulnerabilidad = C1.GrupoVulnerabilidad WHERE C1.IdPaciente = B.IdPaciente AND C1.GrupoVulnerabilidad IS NOT NULL ORDER BY C1.FechaConductaRiesgo DESC ) AS 'GrupoVulnerabilidad', ")
        Q.Append("Convert(DATE, C.FechaNacimiento) AS 'FechaNacimiento', ")
        Q.Append("dbo.fn_ObtieneEdad(CONVERT(DATE, C.FechaNacimiento), CONVERT(DATE, '" & fechaI & "')) AS 'EdadAnterior', ")
        Q.Append("dbo.fn_ObtieneEdad(CONVERT(DATE, C.FechaNacimiento), CONVERT(DATE, '" & fechaF & "')) AS 'Edad' ")
        Q.Append("FROM ControlARV AS A INNER JOIN ")
        Q.Append("PAC_ID AS B ON A.NHC = B.NHC LEFT OUTER JOIN ")
        Q.Append("PAC_BASALES AS C ON C.IdPaciente = B.IdPaciente ")
        Q.Append("WHERE A.NHC NOT LIKE '%P%' AND A.Med1_ARVEstatus NOT IN (6, 7, 12, 21) ")
        Q.Append("AND A.IdCCARV = (SELECT TOP(1) X.IdCCARV FROM ControlARV AS X ")
        Q.Append("WHERE X.NHC = A.NHC AND X.FechaEntrega = (SELECT TOP(1) Y.FechaEntrega FROM ControlARV AS Y ")
        Q.Append("WHERE Y.NHC = X.NHC AND Y.FechaEntrega <= '" & fechaF & " 23:59:59.999' ORDER BY Y.FechaEntrega DESC) ")
        Q.Append("ORDER BY X.IdCCARV DESC)) AS Z ")
        Q.Append("WHERE Z.EdadAnterior <> Z.Edad AND Z.EdadAnterior IN (14,18,24,49)")
        Dim Query As String = Q.ToString()
        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim adapter As New SqlDataAdapter()
                adapter.SelectCommand = New SqlCommand(Query, connection)
                adapter.SelectCommand.CommandTimeout = TimeoutDB
                adapter.Fill(Ds, _page)
                adapter.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
            Return Ds.Tables(0)
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page
            GrabarErrores(usuario & "|" & _pageO & "|" & ex.Number & "|" & ex.Message)
            Return Nothing
        End Try
    End Function

    Public Function RPxActivosTMP(ByVal fechaA As String, ByVal fechaM As String, ByVal ultimodia As String, ByVal usuario As String) As DataTable
        _page = "db.RPxActivosTMP"
        Dim Query As String = String.Empty
        Dim fechaI As String = fechaA.ToString() & "-" & fechaM.ToString() & "-01"
        Dim fechaF As String = fechaA.ToString() & "-" & fechaM.ToString() & "-" & ultimodia
        Dim fecha As String = fechaA.ToString() & "-" & fechaM.ToString() & "-"
        Dim diafin As Integer = CInt(ultimodia)
        Query = "SELECT * FROM dbo.fn_ListaPXActivosTMP('" & fechaF & "') ORDER BY NHC"
        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn2)
                connection.Open()
                Dim adapter As New SqlDataAdapter()
                adapter.SelectCommand = New SqlCommand(Query, connection)
                adapter.SelectCommand.CommandTimeout = TimeoutDB
                adapter.Fill(Ds, _page)
                adapter.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
            Return Ds.Tables(0)
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page
            GrabarErrores(usuario & "|" & _pageO & "|" & ex.Number & "|" & ex.Message)
            Return Nothing
        End Try
    End Function

    Public Function RIndicadorTMP(ByVal fechaA As String, ByVal fechaM As String, ByVal ultimodia As String, ByVal usuario As String) As DataTable
        _page = "db.RIndicadorTMP"
        Dim Query As String = String.Empty
        Dim fechaI As String = "01/" & fechaM.ToString() & "/" & fechaA.ToString()
        Dim fechaF As String = ultimodia & "/" & fechaM.ToString() & "/" & fechaA.ToString()
        Dim fecha As String = "/" & fechaM.ToString() & "/" & fechaA.ToString()
        Dim diafin As Integer = CInt(ultimodia)
        Query = "SELECT * FROM dbo.fn_IndicadorTMP('" & fechaI & "','" & fechaF & "') ORDER BY IdPaciente ASC, FechaAnalitica ASC"
        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn2)
                connection.Open()
                Dim adapter As New SqlDataAdapter()
                adapter.SelectCommand = New SqlCommand(Query, connection)
                adapter.SelectCommand.CommandTimeout = TimeoutDB
                adapter.Fill(Ds, _page)
                adapter.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
            Return Ds.Tables(0)
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page
            GrabarErrores(usuario & "|" & _pageO & "|" & ex.Number & "|" & ex.Message)
            Return Nothing
        End Try
    End Function

    Public Function RPActivosMes(ByVal fechaA As String, ByVal fechaM As String, ByVal ultimodia As String, ByVal usuario As String) As DataTable
        _page = "db.RPActivosMes"
        Dim fechaI As String = fechaA.ToString() & "-" & fechaM.ToString() & "-01"
        Dim fechaF As String = fechaA.ToString() & "-" & fechaM.ToString() & "-" & ultimodia
        Dim fecha As String = fechaA.ToString() & "-" & fechaM.ToString() & "-"
        Dim diafin As Integer = CInt(ultimodia)
        Dim Q As New StringBuilder()
        Q.Append("SELECT A.NHC, LTRIM(RTRIM(C.PrimerNombre)) + (CASE WHEN C.SegundoNombre IS NULL THEN '' WHEN C.SegundoNombre = 'SSN' THEN '' ELSE ' ' ")
        Q.Append("+ LTRIM(RTRIM(C.SegundoNombre)) END) + ' ' + LTRIM(RTRIM(C.PrimerApellido)) ")
        Q.Append("+ (CASE WHEN C.SegundoApellido IS NULL THEN '' WHEN C.SegundoApellido = 'SSA' THEN '' ELSE ' ' ")
        Q.Append("+ LTRIM(RTRIM(C.SegundoApellido)) END) AS 'Paciente', ")
        Q.Append("C.IdGenero,(SELECT TOP(1) GV.NomGrupoVulnerabilidad  FROM dbo.PAC_CONDUCTARIESGO AS C1 LEFT OUTER JOIN dbo.PAC_M_GRUPOVULNERABILIDAD AS GV ON GV.IdGrupoVulnerabilidad = C1.GrupoVulnerabilidad WHERE C1.IdPaciente = B.IdPaciente AND C1.GrupoVulnerabilidad IS NOT NULL ORDER BY C1.FechaConductaRiesgo DESC ) AS 'GrupoVulnerabilidad', ")
        Q.Append("dbo.fn_ObtieneEdad(CONVERT(DATE, C.FechaNacimiento), CONVERT(DATE, '" & fechaF & "')) AS 'Edad', A.Embarazo, ")
        Q.Append("CONVERT(VARCHAR,A.FechaEntrega, 103) AS 'FechaEntrega', CONVERT(VARCHAR,A.FechaRetorno, 103) AS 'FechaRetorno', A.IdEsquema, ")
        Q.Append("A.IdSEsquema, A.EsquemaEstatus, ")
        Q.Append("dbo.fn_ObtieneMed(A.Med1_Codigo) AS 'M1', A.Med1_ARVEstatus AS 'ME1', dbo.fn_ObtieneMed(A.Med2_Codigo) AS 'M2', A.Med2_ARVEstatus AS 'ME2', ")
        Q.Append("dbo.fn_ObtieneMed(A.Med3_Codigo) AS 'M3', A.Med3_ARVEstatus AS 'ME3', dbo.fn_ObtieneMed(A.Med4_Codigo) AS 'M4', A.Med4_ARVEstatus AS 'ME4', ")
        Q.Append("dbo.fn_ObtieneMed(A.Med5_Codigo) AS 'M5', A.Med5_ARVEstatus AS 'ME5', dbo.fn_ObtieneMed(A.Med6_Codigo) AS 'M6', A.Med6_ARVEstatus AS 'ME6', ")
        Q.Append("dbo.fn_ObtieneMed(A.Med7_Codigo) AS 'M7', A.Med7_ARVEstatus AS 'ME7', dbo.fn_ObtieneMed(A.Med8_Codigo) AS 'M8', A.Med8_ARVEstatus AS 'ME8' ")
        Q.Append("FROM ControlARV AS A INNER JOIN ")
        Q.Append("PAC_ID AS B ON A.NHC = B.NHC LEFT OUTER JOIN ")
        Q.Append("PAC_BASALES AS C ON C.IdPaciente = B.IdPaciente ")
        Q.Append("WHERE A.NHC NOT LIKE '%P%' AND A.EsquemaEstatus NOT IN (6, 7, 12, 21) ")
        Q.Append("AND A.IdCCARV = (SELECT TOP(1) X.IdCCARV FROM ControlARV AS X ")
        Q.Append("WHERE X.NHC = A.NHC AND X.FechaEntrega = (SELECT TOP(1) Y.FechaEntrega FROM ControlARV AS Y ")
        Q.Append("WHERE Y.NHC = X.NHC AND Y.FechaEntrega <= '" & fechaF & " 23:59:59.999' ORDER BY Y.FechaEntrega DESC) ")
        Q.Append("ORDER BY X.IdCCARV DESC)")
        Dim Query As String = Q.ToString()
        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim adapter As New SqlDataAdapter()
                adapter.SelectCommand = New SqlCommand(Query, connection)
                adapter.SelectCommand.CommandTimeout = TimeoutDB
                adapter.Fill(Ds, _page)
                adapter.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
            Return Ds.Tables(0)
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page & "_" & fechaF
            GrabarErrores(usuario & "|" & _pageO & "|" & ex.Number & "|" & ex.Message)
            Return Nothing
        End Try
    End Function

    '*Lista Pacientes*//
    Public Function ListaPacientes(ByVal tipo As String, ByVal estatus As String, ByVal usuario As String) As DataTable
        _page = "db.ListaPacientes"
        Dim Query As String = ""
        If tipo = "A" Then
            Select Case estatus
                Case Is = "AC"
                    'todos los pacientes adultos activos
                    Query = "SELECT NHC, Cohorte, Paciente, UltimaFechaEntrega, FechaRetorno, Dias FROM v_ListaPacientes WHERE TIPO = 'A' AND ME NOT IN (6, 7, 12, 21)"
                Case Is = "PP"
                    'todos los pacientes Post-parto
                    Query = "SELECT NHC, Cohorte, Paciente, UltimaFechaEntrega, FechaRetorno, Dias FROM v_ListaPacientes WHERE TIPO = 'A' AND ME = 21"
                Case Is = "AB"
                    'todos los pacientes adultos abandonos
                    Query = "SELECT NHC, Cohorte, Paciente, UltimaFechaEntrega, FechaRetorno, Dias FROM v_ListaPacientes WHERE TIPO = 'A' AND ME = 6"
                Case Is = "TR"
                    'todos los pacientes adultos traslados		
                    Query = "SELECT NHC, Cohorte, Paciente, UltimaFechaEntrega, FechaRetorno, Dias FROM v_ListaPacientes WHERE TIPO = 'A' AND ME = 7"
                Case Is = "FA"
                    'todos los pacientes adultos fallecidos		
                    Query = "SELECT NHC, Cohorte, Paciente, UltimaFechaEntrega, FechaRetorno, Dias FROM v_ListaPacientes WHERE TIPO = 'A' AND ME = 12"
            End Select
        ElseIf tipo = "P" Then
            Select Case estatus
                Case Is = "AC"
                    'todos los pacientes pediatria activos	
                    Query = "SELECT NHC, Cohorte, Paciente, UltimaFechaEntrega, FechaRetorno, Dias FROM v_ListaPacientes WHERE TIPO = 'P' AND ME NOT IN (6, 7, 12)"
                Case Is = "AB"
                    'todos los pacientes pediatria abandonos		
                    Query = "SELECT NHC, Cohorte, Paciente, UltimaFechaEntrega, FechaRetorno, Dias FROM v_ListaPacientes WHERE TIPO = 'P' AND ME = 6"
                Case Is = "TR"
                    'todos los pacientes pediatria traslados		
                    Query = "SELECT NHC, Cohorte, Paciente, UltimaFechaEntrega, FechaRetorno, Dias FROM v_ListaPacientes WHERE TIPO = 'P' AND ME = 7"
                Case Is = "FA"
                    'todos los pacientes pediatria fallecidos		
                    Query = "SELECT NHC, Cohorte, Paciente, UltimaFechaEntrega, FechaRetorno, Dias FROM v_ListaPacientes WHERE TIPO = 'P' AND ME = 12"
            End Select
        End If

        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim adapter As New SqlDataAdapter()
                adapter.SelectCommand = New SqlCommand(Query, connection)
                adapter.SelectCommand.CommandTimeout = TimeoutDB
                adapter.Fill(Ds, _page)
                adapter.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
            Return Ds.Tables(0)
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page
            GrabarErrores(usuario & "|" & _page & "|" & ex.Number & "|" & ex.Message)
            Return Nothing
        End Try
    End Function

    Public Function RegistrosPacienteARV(ByVal nhc As String, ByVal usuario As String) As DataTable
        _page = "db.RegistrosPacienteARV"
        Dim Q As New StringBuilder()
        Dim Query As String = ""
        Q.Append("SELECT A.NHC,A.IdCCARV,CONVERT(VARCHAR,A.FechaEntrega,103) AS 'FechaEntrega',CONVERT(VARCHAR,A.FechaRetorno,103) AS 'FechaRetorno', ")
        Q.Append("dbo.fn_ObtieneCodMed('A', A.Med1_Codigo) AS 'M1C',A.Med1_Cantidad AS 'M1N',dbo.fn_ObtieneEstatusMed(A.Med1_ARVEstatus) AS 'M1E', ")
        Q.Append("dbo.fn_ObtieneCodMed('A', A.Med2_Codigo) AS 'M2C',A.Med2_Cantidad AS 'M2N',dbo.fn_ObtieneEstatusMed(A.Med2_ARVEstatus) AS 'M2E', ")
        Q.Append("dbo.fn_ObtieneCodMed('A', A.Med3_Codigo) AS 'M3C',A.Med3_Cantidad AS 'M3N',dbo.fn_ObtieneEstatusMed(A.Med3_ARVEstatus) AS 'M3E', ")
        Q.Append("dbo.fn_ObtieneCodMed('A', A.Med4_Codigo) AS 'M4C',A.Med4_Cantidad AS 'M4N',dbo.fn_ObtieneEstatusMed(A.Med4_ARVEstatus) AS 'M4E', ")
        Q.Append("dbo.fn_ObtieneCodMed('A', A.Med5_Codigo) AS 'M5C',A.Med5_Cantidad AS 'M5N',dbo.fn_ObtieneEstatusMed(A.Med5_ARVEstatus) AS 'M5E', ")
        Q.Append("dbo.fn_ObtieneCodMed('A', A.Med6_Codigo) AS 'M6C',A.Med6_Cantidad AS 'M6N',dbo.fn_ObtieneEstatusMed(A.Med6_ARVEstatus) AS 'M6E', ")
        Q.Append("dbo.fn_ObtieneCodMed('A', A.Med7_Codigo) AS 'M7C',A.Med7_Cantidad AS 'M7N',dbo.fn_ObtieneEstatusMed(A.Med7_ARVEstatus) AS 'M7E', ")
        Q.Append("dbo.fn_ObtieneCodMed('A', A.Med8_Codigo) AS 'M8C',A.Med8_Cantidad AS 'M8N',dbo.fn_ObtieneEstatusMed(A.Med8_ARVEstatus) AS 'M8E', ")
        Q.Append("S.SCodigo, E.Descripcion, dbo.fn_ObtieneEstatusMed(A.EsquemaEstatus) AS 'EsquemaEstatus', A.TiempoTARV, A.Embarazo, A.CD4, A.CV, A.Id_auto_adherencia ")
        Q.Append("FROM ControlARV AS A LEFT OUTER JOIN ")
        Q.Append("SubEsquemas AS S ON A.IdSEsquema = S.IdSEsquema LEFT OUTER JOIN ")
        Q.Append("Esquemas AS E ON A.IdEsquema = E.IdEsquema ")
        Q.Append("WHERE A.NHC = '" & nhc & "' ")
        Q.Append("ORDER BY A.FechaEntrega DESC, A.IdCCARV DESC")
        Query = Q.ToString()
        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim adapter As New SqlDataAdapter()
                adapter.SelectCommand = New SqlCommand(Query, connection)
                adapter.SelectCommand.CommandTimeout = TimeoutDB
                adapter.Fill(Ds, _page)
                adapter.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
            Return Ds.Tables(0)
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page & "_" & nhc
            GrabarErrores(usuario & "|" & _pageO & "|" & ex.Number & "|" & ex.Message)
            Return Nothing
        End Try
    End Function

    Public Function ObtieneRegARV(ByVal id As String, ByVal usuario As String) As String
        _page = "db.ObtieneRegARV"
        Dim Q As New StringBuilder()
        Dim Query As String = ""
        Dim Str As String = ""
        Q.Append("SELECT IdCCARV, FechaEntrega, IdEsquema, IdSEsquema, EsquemaEstatus, Med1_Codigo, Med1_Cantidad, Med1_Dosis, Med1_Frecuencia, Med1_UExCantidad, Med1_ARVEstatus, Med1_DevCantidad, ")
        Q.Append("Med2_Codigo, Med2_Cantidad, Med2_Dosis, Med2_Frecuencia, Med2_UExCantidad, Med2_ARVEstatus, Med2_DevCantidad, ")
        Q.Append("Med3_Codigo, Med3_Cantidad, Med3_Dosis, Med3_Frecuencia, Med3_UExCantidad, Med3_ARVEstatus, Med3_DevCantidad, ")
        Q.Append("Med4_Codigo, Med4_Cantidad, Med4_Dosis, Med4_Frecuencia, Med4_UExCantidad, Med4_ARVEstatus, Med4_DevCantidad, ")
        Q.Append("Med5_Codigo, Med5_Cantidad, Med5_Dosis, Med5_Frecuencia, Med5_UExCantidad, Med5_ARVEstatus, Med5_DevCantidad, ")
        Q.Append("Med6_Codigo, Med6_Cantidad, Med6_Dosis, Med6_Frecuencia, Med6_UExCantidad, Med6_ARVEstatus, Med6_DevCantidad, ")
        Q.Append("Med7_Codigo, Med7_Cantidad, Med7_Dosis, Med7_Frecuencia, Med7_UExCantidad, Med7_ARVEstatus, Med7_DevCantidad, ")
        Q.Append("Med8_Codigo, Med8_Cantidad, Med8_Dosis, Med8_Frecuencia, Med8_UExCantidad, Med8_ARVEstatus, Med8_DevCantidad, ")
        Q.Append("convert(varchar,FechaRetorno,103) as 'FechaRetorno', TiempoTARV, CitaMedica, CitaFarmacia, Embarazo, TiempoRetorno, Adherencia, CD4, CV, Observaciones, Id_auto_adherencia ")
        Q.Append("FROM ControlARV ")
        Q.Append("WHERE IdCCARV = " & id)
        Query = Q.ToString()
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim command As New SqlCommand(Query, connection)
                command.CommandTimeout = TimeoutDB
                Dim reader As SqlDataReader = command.ExecuteReader()
                If reader IsNot Nothing Then
                    While reader.Read()
                        Str = "True|" + reader("IdCCARV").ToString() + "|" + reader("FechaEntrega").ToString() + "|" + reader("Med1_Codigo").ToString() + "|" + reader("Med1_Cantidad").ToString() + "|" + reader("Med1_Dosis").ToString() + "|" + reader("Med1_Frecuencia").ToString() + "|" + reader("Med1_UExCantidad").ToString() + "|" + reader("Med1_ARVEstatus").ToString() + "|" + reader("Med1_DevCantidad").ToString() + "|" + reader("Med2_Codigo").ToString() + "|" + reader("Med2_Cantidad").ToString() + "|" + reader("Med2_Dosis").ToString() + "|" + reader("Med2_Frecuencia").ToString() + "|" + reader("Med2_UExCantidad").ToString() + "|" + reader("Med2_ARVEstatus").ToString() + "|" + reader("Med2_DevCantidad").ToString() + "|" + reader("Med3_Codigo").ToString() + "|" + reader("Med3_Cantidad").ToString() + "|" + reader("Med3_Dosis").ToString() + "|" + reader("Med3_Frecuencia").ToString() + "|" + reader("Med3_UExCantidad").ToString() + "|" + reader("Med3_ARVEstatus").ToString() + "|" + reader("Med3_DevCantidad").ToString() + "|" + reader("Med4_Codigo").ToString() + "|" + reader("Med4_Cantidad").ToString() + "|" + reader("Med4_Dosis").ToString() + "|" + reader("Med4_Frecuencia").ToString() + "|" + reader("Med4_UExCantidad").ToString() + "|" + reader("Med4_ARVEstatus").ToString() + "|" + reader("Med4_DevCantidad").ToString() + "|" + reader("Med5_Codigo").ToString() + "|" + reader("Med5_Cantidad").ToString() + "|" + reader("Med5_Dosis").ToString() + "|" + reader("Med5_Frecuencia").ToString() + "|" + reader("Med5_UExCantidad").ToString() + "|" + reader("Med5_ARVEstatus").ToString() + "|" + reader("Med5_DevCantidad").ToString() + "|" + reader("Med6_Codigo").ToString() + "|" + reader("Med6_Cantidad").ToString() + "|" + reader("Med6_Dosis").ToString() + "|" + reader("Med6_Frecuencia").ToString() + "|" + reader("Med6_UExCantidad").ToString() + "|" + reader("Med6_ARVEstatus").ToString() + "|" + reader("Med6_DevCantidad").ToString() + "|" + reader("Med7_Codigo").ToString() + "|" + reader("Med7_Cantidad").ToString() + "|" + reader("Med7_Dosis").ToString() + "|" + reader("Med7_Frecuencia").ToString() + "|" + reader("Med7_UExCantidad").ToString() + "|" + reader("Med7_ARVEstatus").ToString() + "|" + reader("Med7_DevCantidad").ToString() + "|" + reader("Med8_Codigo").ToString() + "|" + reader("Med8_Cantidad").ToString() + "|" + reader("Med8_Dosis").ToString() + "|" + reader("Med8_Frecuencia").ToString() + "|" + reader("Med8_UExCantidad").ToString() + "|" + reader("Med8_ARVEstatus").ToString() + "|" + reader("Med8_DevCantidad").ToString() + "|" + reader("FechaRetorno").ToString() + "|" + reader("TiempoTARV").ToString() + "|" + reader("CitaMedica").ToString() + "|" + reader("CitaFarmacia").ToString() + "|" + reader("Embarazo").ToString() + "|" + reader("TiempoRetorno").ToString() + "|" + reader("Adherencia").ToString() + "|" + reader("CD4").ToString() + "|" + reader("CV").ToString() + "|" + reader("Observaciones").ToString() + "|" + reader("IdEsquema").ToString() + "|" + reader("IdSEsquema").ToString() + "|" + reader("EsquemaEstatus").ToString() + "|" + reader("Id_auto_adherencia").ToString()
                        Exit While
                    End While
                End If
                If Str = String.Empty Then
                    Str = "False|No se Encontró Información."
                End If
                reader.Dispose()
                reader.Close()
                command.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page & "_" & id
            GrabarErrores(usuario & "|" & _pageO & "|" & ex.Number & "|" & ex.Message)
            Str = "False|" + ex.Message
        End Try
        Return Str
    End Function

    Public Function RegistrosPacienteProf(ByVal nhc As String, ByVal usuario As String) As DataTable
        _page = "db.RegistrosPacienteProf"
        Dim Q As New StringBuilder()
        Dim Query As String = ""
        Q.Append("SELECT NHC,IdCCProf,CONVERT(DATE,FechaEntrega) AS 'FechaEntrega', CD4, ")
        Q.Append("dbo.fn_ObtieneCodMed('P', Prof1_Codigo) AS 'P1',Prof1_Cantidad AS 'P1C', Prof1_Tipo AS 'P1T', Prof1_TipoTratamiento AS 'P1TT', dbo.fn_ObtieneEstatusMed(Prof1_Estatus) AS 'P1E', ")
        Q.Append("dbo.fn_ObtieneCodMed('P', Prof2_Codigo) AS 'P2',Prof2_Cantidad AS 'P2C', Prof2_Tipo AS 'P2T', Prof2_TipoTratamiento AS 'P2TT', dbo.fn_ObtieneEstatusMed(Prof2_Estatus) AS 'P2E', ")
        Q.Append("dbo.fn_ObtieneCodMed('P', Prof3_Codigo) AS 'P3',Prof3_Cantidad AS 'P3C', Prof3_Tipo AS 'P3T', Prof3_TipoTratamiento AS 'P3TT', dbo.fn_ObtieneEstatusMed(Prof3_Estatus) AS 'P3E', ")
        Q.Append("dbo.fn_ObtieneCodMed('P', Prof4_Codigo) AS 'P4',Prof4_Cantidad AS 'P4C', Prof4_Tipo AS 'P4T', Prof4_TipoTratamiento AS 'P4TT', dbo.fn_ObtieneEstatusMed(Prof4_Estatus) AS 'P4E', ")
        Q.Append("dbo.fn_ObtieneCodMed('P', Prof5_Codigo) AS 'P5',Prof5_Cantidad AS 'P5C', Prof5_Tipo AS 'P5T', Prof5_TipoTratamiento AS 'P5TT', dbo.fn_ObtieneEstatusMed(Prof5_Estatus) AS 'P5E', ")
        Q.Append("dbo.fn_ObtieneCodMed('P', Prof6_Codigo) AS 'P6',Prof6_Cantidad AS 'P6C', Prof6_Tipo AS 'P6T', Prof6_TipoTratamiento AS 'P6TT', dbo.fn_ObtieneEstatusMed(Prof6_Estatus) AS 'P6E' ")
        Q.Append("FROM ControlProf ")
        Q.Append("WHERE NHC = '" & nhc & "' ")
        Q.Append("ORDER BY FechaEntrega DESC")
        Query = Q.ToString()
        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim adapter As New SqlDataAdapter()
                adapter.SelectCommand = New SqlCommand(Query, connection)
                adapter.SelectCommand.CommandTimeout = TimeoutDB
                adapter.Fill(Ds, _page)
                adapter.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
            Return Ds.Tables(0)
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page & "_" & nhc
            GrabarErrores(usuario & "|" & _pageO & "|" & ex.Number & "|" & ex.Message)
            Return Nothing
        End Try
    End Function

    Public Function ObtieneRegProf(ByVal id As String, ByVal usuario As String) As String
        _page = "db.ObtieneRegProf"
        Dim Q As New StringBuilder()
        Dim Query As String = ""
        Dim Str As String = ""
        Q.Append("SELECT C.IdCCPROF, C.NHC, C.FechaEntrega, C.TipoPaciente, ")
        Q.Append("C.Prof1_Codigo, dbo.fn_ObtieneNomProf(C.Prof1_Codigo) AS 'NomProf1', C.Prof1_Cantidad, C.Prof1_Dosis, C.Prof1_VIA, C.Prof1_Frecuencia, C.Prof1_Tipo, C.Prof1_TipoTratamiento, C.Prof1_Estatus, C.Prof1_TTMed, C.Prof1_Observaciones, ")
        Q.Append("C.Prof2_Codigo, dbo.fn_ObtieneNomProf(C.Prof2_Codigo) AS 'NomProf2', C.Prof2_Cantidad, C.Prof2_Dosis, C.Prof2_VIA, C.Prof2_Frecuencia, C.Prof2_Tipo, C.Prof2_TipoTratamiento, C.Prof2_Estatus, C.Prof2_TTMed, C.Prof2_Observaciones, ")
        Q.Append("C.Prof3_Codigo, dbo.fn_ObtieneNomProf(C.Prof3_Codigo) AS 'NomProf3', C.Prof3_Cantidad, C.Prof3_Dosis, C.Prof3_VIA, C.Prof3_Frecuencia, C.Prof3_Tipo, C.Prof3_TipoTratamiento, C.Prof3_Estatus, C.Prof3_TTMed, C.Prof3_Observaciones, ")
        Q.Append("C.Prof4_Codigo, dbo.fn_ObtieneNomProf(C.Prof4_Codigo) AS 'NomProf4', C.Prof4_Cantidad, C.Prof4_Dosis, C.Prof4_VIA, C.Prof4_Frecuencia, C.Prof4_Tipo, C.Prof4_TipoTratamiento, C.Prof4_Estatus, C.Prof4_TTMed, C.Prof4_Observaciones, ")
        Q.Append("C.Prof5_Codigo, dbo.fn_ObtieneNomProf(C.Prof5_Codigo) AS 'NomProf5', C.Prof5_Cantidad, C.Prof5_Dosis, C.Prof5_VIA, C.Prof5_Frecuencia, C.Prof5_Tipo, C.Prof5_TipoTratamiento, C.Prof5_Estatus, C.Prof5_TTMed, C.Prof5_Observaciones, ")
        Q.Append("C.Prof6_Codigo, dbo.fn_ObtieneNomProf(C.Prof6_Codigo) AS 'NomProf6', C.Prof6_Cantidad, C.Prof6_Dosis, C.Prof6_VIA, C.Prof6_Frecuencia, C.Prof6_Tipo, C.Prof6_TipoTratamiento, C.Prof6_Estatus, C.Prof6_TTMed, C.Prof6_Observaciones, ")
        Q.Append("C.CD4 ")
        Q.Append("FROM ControlProf AS C ")
        Q.Append("WHERE C.IdCCProf = " & id)
        Query = Q.ToString()
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim command As New SqlCommand(Query, connection)
                command.CommandTimeout = TimeoutDB
                Dim reader As SqlDataReader = command.ExecuteReader()
                If reader IsNot Nothing Then
                    While reader.Read()
                        Str = "True|" + reader("IdCCPROF").ToString() + "|" + reader("FechaEntrega").ToString() + "|" + reader("TipoPaciente").ToString() + "|"
                        Str += reader("Prof1_Codigo").ToString() + "|" + reader("NomProf1").ToString() + "|" + reader("Prof1_Cantidad").ToString() + "|" + reader("Prof1_Dosis").ToString() + "|" + reader("Prof1_VIA").ToString() + "|" + reader("Prof1_Frecuencia").ToString() + "|" + reader("Prof1_Tipo").ToString() + "|" + reader("Prof1_TipoTratamiento").ToString() + "|" + reader("Prof1_Estatus").ToString() + "|" + reader("Prof1_TTMed").ToString() + "|" + reader("Prof1_Observaciones").ToString() + "|"
                        Str += reader("Prof2_Codigo").ToString() + "|" + reader("NomProf2").ToString() + "|" + reader("Prof2_Cantidad").ToString() + "|" + reader("Prof2_Dosis").ToString() + "|" + reader("Prof2_VIA").ToString() + "|" + reader("Prof2_Frecuencia").ToString() + "|" + reader("Prof2_Tipo").ToString() + "|" + reader("Prof2_TipoTratamiento").ToString() + "|" + reader("Prof2_Estatus").ToString() + "|" + reader("Prof2_TTMed").ToString() + "|" + reader("Prof2_Observaciones").ToString() + "|"
                        Str += reader("Prof3_Codigo").ToString() + "|" + reader("NomProf3").ToString() + "|" + reader("Prof3_Cantidad").ToString() + "|" + reader("Prof3_Dosis").ToString() + "|" + reader("Prof3_VIA").ToString() + "|" + reader("Prof3_Frecuencia").ToString() + "|" + reader("Prof3_Tipo").ToString() + "|" + reader("Prof3_TipoTratamiento").ToString() + "|" + reader("Prof3_Estatus").ToString() + "|" + reader("Prof3_TTMed").ToString() + "|" + reader("Prof3_Observaciones").ToString() + "|"
                        Str += reader("Prof4_Codigo").ToString() + "|" + reader("NomProf4").ToString() + "|" + reader("Prof4_Cantidad").ToString() + "|" + reader("Prof4_Dosis").ToString() + "|" + reader("Prof4_VIA").ToString() + "|" + reader("Prof4_Frecuencia").ToString() + "|" + reader("Prof4_Tipo").ToString() + "|" + reader("Prof4_TipoTratamiento").ToString() + "|" + reader("Prof4_Estatus").ToString() + "|" + reader("Prof4_TTMed").ToString() + "|" + reader("Prof4_Observaciones").ToString() + "|"
                        Str += reader("Prof5_Codigo").ToString() + "|" + reader("NomProf5").ToString() + "|" + reader("Prof5_Cantidad").ToString() + "|" + reader("Prof5_Dosis").ToString() + "|" + reader("Prof5_VIA").ToString() + "|" + reader("Prof5_Frecuencia").ToString() + "|" + reader("Prof5_Tipo").ToString() + "|" + reader("Prof5_TipoTratamiento").ToString() + "|" + reader("Prof5_Estatus").ToString() + "|" + reader("Prof5_TTMed").ToString() + "|" + reader("Prof5_Observaciones").ToString() + "|"
                        Str += reader("Prof6_Codigo").ToString() + "|" + reader("NomProf6").ToString() + "|" + reader("Prof6_Cantidad").ToString() + "|" + reader("Prof6_Dosis").ToString() + "|" + reader("Prof6_VIA").ToString() + "|" + reader("Prof6_Frecuencia").ToString() + "|" + reader("Prof6_Tipo").ToString() + "|" + reader("Prof6_TipoTratamiento").ToString() + "|" + reader("Prof6_Estatus").ToString() + "|" + reader("Prof6_TTMed").ToString() + "|" + reader("Prof6_Observaciones").ToString() + "|"
                        Str += reader("CD4").ToString()
                        Exit While
                    End While
                End If
                If Str = String.Empty Then
                    Str = "False|No se Encontró Información."
                End If
                reader.Dispose()
                reader.Close()
                command.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page & "_" & id
            GrabarErrores(usuario & "|" & _pageO & "|" & ex.Number & "|" & ex.Message)
            Str = "False|" + ex.Message
        End Try
        Return Str
    End Function

    Public Function ConsultaRegistroARV(ByVal id As String, ByVal usuario As String) As String
        _page = "db.ConsultaRegistroARV"
        Dim Q As New StringBuilder()
        Dim Query As String = ""
        Dim Str As String = ""
        Q.Append("SELECT A.IdCCARV, CONVERT(VARCHAR,A.FechaEntrega, 103) AS 'FechaEntrega', ")
        Q.Append("(CASE WHEN A.Med1_Codigo <> 0 THEN (SELECT Codigo FROM FFARV WHERE IdFFARV = A.Med1_Codigo) ELSE '' END) AS 'Med1_Codigo', ")
        Q.Append("(CASE WHEN A.Med2_Codigo <> 0 THEN (SELECT Codigo FROM FFARV WHERE IdFFARV = A.Med2_Codigo) ELSE '' END) AS 'Med2_Codigo', ")
        Q.Append("(CASE WHEN A.Med3_Codigo <> 0 THEN (SELECT Codigo FROM FFARV WHERE IdFFARV = A.Med3_Codigo) ELSE '' END) AS 'Med3_Codigo', ")
        Q.Append("(CASE WHEN A.Med4_Codigo <> 0 THEN (SELECT Codigo FROM FFARV WHERE IdFFARV = A.Med4_Codigo) ELSE '' END) AS 'Med4_Codigo', ")
        Q.Append("(CASE WHEN A.Med5_Codigo <> 0 THEN (SELECT Codigo FROM FFARV WHERE IdFFARV = A.Med5_Codigo) ELSE '' END) AS 'Med5_Codigo', ")
        Q.Append("(CASE WHEN A.Med6_Codigo <> 0 THEN (SELECT Codigo FROM FFARV WHERE IdFFARV = A.Med6_Codigo) ELSE '' END) AS 'Med6_Codigo', ")
        Q.Append("(CASE WHEN A.Med7_Codigo <> 0 THEN (SELECT Codigo FROM FFARV WHERE IdFFARV = A.Med7_Codigo) ELSE '' END) AS 'Med7_Codigo', ")
        Q.Append("(CASE WHEN A.Med8_Codigo <> 0 THEN (SELECT Codigo FROM FFARV WHERE IdFFARV = A.Med8_Codigo) ELSE '' END) AS 'Med8_Codigo' ")
        Q.Append("FROM ControlARV AS A ")
        Q.Append("WHERE A.IdCCARV = " & id)
        Query = Q.ToString()
        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim command As New SqlCommand(Query, connection)
                command.CommandTimeout = TimeoutDB
                Dim reader As SqlDataReader = command.ExecuteReader()
                If reader IsNot Nothing Then
                    While reader.Read()
                        Str = "True|" + reader("IdCCARV").ToString() + "|" + reader("FechaEntrega").ToString() + "|" + reader("Med1_Codigo").ToString() + "|" + reader("Med2_Codigo").ToString() + "|" + reader("Med3_Codigo").ToString() + "|" + reader("Med4_Codigo").ToString() + "|" + reader("Med5_Codigo").ToString() + "|" + reader("Med6_Codigo").ToString() + "|" + reader("Med7_Codigo").ToString() + "|" + reader("Med8_Codigo").ToString()
                        Exit While
                    End While
                End If
                If Str = String.Empty Then
                    Str = "False|No existe última fecha."
                End If
                reader.Dispose()
                reader.Close()
                command.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page & "_" & id
            GrabarErrores(usuario & "|" & _page & "|" & ex.Number & "|" & ex.Message)
            Str = "False|" + ex.Message
        End Try
        Return Str
    End Function

    Public Function Esquemas(ByVal usuario As String) As DataTable
        _page = "db.Esquemas"
        'Dim Query As String = "SELECT IdEsquema, Descripcion, Codigos FROM Esquemas ORDER BY IdEsquema"
        Dim Query As String = "SELECT E.IdEsquema, E.Descripcion, E.Codigos, (SELECT COUNT(*) FROM SubEsquemas AS S WHERE S.IdEsquema = E.IdEsquema) AS 'SubEsquemas' FROM Esquemas AS E ORDER BY E.IdEsquema"
        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim adapter As New SqlDataAdapter()
                adapter.SelectCommand = New SqlCommand(Query, connection)
                adapter.SelectCommand.CommandTimeout = TimeoutDB
                adapter.Fill(Ds, _page)
                adapter.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
            Return Ds.Tables(0)
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page
            GrabarErrores(usuario & "|" & _pageO & "|" & ex.Number & "|" & ex.Message)
            Return Nothing
        End Try
    End Function

    Public Function ListaEsquemas(ByVal usuario As String) As DataTable
        _page = "db.ListaEsquemas"
        Dim Query As String = "SELECT IdEsquema FROM Esquemas ORDER BY IdEsquema"
        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim adapter As New SqlDataAdapter()
                adapter.SelectCommand = New SqlCommand(Query, connection)
                adapter.SelectCommand.CommandTimeout = TimeoutDB
                adapter.Fill(Ds, _page)
                adapter.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
            Return Ds.Tables(0)
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page
            GrabarErrores(usuario & "|" & _pageO & "|" & ex.Number & "|" & ex.Message)
            Return Nothing
        End Try
    End Function

    Public Function ListaSEsquemas(ByVal idse As String, ByVal usuario As String) As DataTable
        _page = "db.ListaSEsquemas"
        Dim Query As String = String.Format("SELECT IdSEsquema, SCodigo FROM dbo.SubEsquemas WHERE IdEsquema = {0} ORDER BY SCodigo ASC", idse)
        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim adapter As New SqlDataAdapter()
                adapter.SelectCommand = New SqlCommand(Query, connection)
                adapter.SelectCommand.CommandTimeout = TimeoutDB
                adapter.Fill(Ds, _page)
                adapter.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
            Return Ds.Tables(0)
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page
            GrabarErrores(usuario & "|" & _pageO & "|" & ex.Number & "|" & ex.Message)
            Return Nothing
        End Try
    End Function

    Public Function ObtieneEsquema(ByVal id As String, ByVal usuario As String) As String
        _page = "db.ObtieneEsquema"
        Dim Query As String = String.Format("SELECT Descripcion FROM Esquemas WHERE IdEsquema = {0}", id)
        Dim Str As String = ""
        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim command As New SqlCommand(Query, connection)
                command.CommandTimeout = TimeoutDB
                Dim reader As SqlDataReader = command.ExecuteReader()
                If reader IsNot Nothing Then
                    While reader.Read()
                        Str = reader("Descripcion").ToString()
                        Exit While
                    End While
                End If
                reader.Dispose()
                reader.Close()
                command.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page & "_" & id
            GrabarErrores(usuario & "|" & _page & "|" & ex.Number & "|" & ex.Message)
            Str = String.Empty
        End Try
        Return Str
    End Function

    Public Function SEsquemas(ByVal id As String, ByVal usuario As String) As DataTable
        _page = "db.SEsquemas"
        Dim Query As String = String.Format("SELECT IdSEsquema, SCodigo, Descripcion, Codigos FROM SubEsquemas WHERE IdEsquema = {0} ORDER BY IdSEsquema ASC", id)
        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim adapter As New SqlDataAdapter()
                adapter.SelectCommand = New SqlCommand(Query, connection)
                adapter.SelectCommand.CommandTimeout = TimeoutDB
                adapter.Fill(Ds, _page)
                adapter.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
            Return Ds.Tables(0)
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page
            GrabarErrores(usuario & "|" & _pageO & "|" & ex.Number & "|" & ex.Message)
            Return Nothing
        End Try
    End Function

    '*Basales Paciente*//
    Public Function ObtieneBasales(ByVal nhc As String, ByVal usuario As String) As String
        _page = "db.ObtieneBasales"
        Dim Q As New StringBuilder()
        Dim Query As String = ""
        Dim Str As String = ""
        Q.Append("SELECT Z.NomGenero, Z.Paciente, Z.Telefono, Z.FechaNacimiento, Z.Direccion, ")
        Q.Append("(CASE WHEN LTRIM(RTRIM(Z.NomMotivoBaja)) IS NULL THEN 'Activo' ELSE LTRIM(RTRIM(Z.NomMotivoBaja)) END) AS 'MotivoBaja', Z.Clasificación_Pac, z.FechaProximaVisitaTS,z.FechaProximaVisitaMangua FROM ")
        Q.Append("(SELECT C.NomGenero, LTRIM(RTRIM(B.PrimerNombre)) + (CASE WHEN B.SegundoNombre IS NULL ")
        Q.Append("THEN '' WHEN B.SegundoNombre = 'SSN' THEN '' ELSE ' ' + LTRIM(RTRIM(B.SegundoNombre)) END) + ' ' + LTRIM(RTRIM(B.PrimerApellido)) ")
        Q.Append("+ (CASE WHEN B.SegundoApellido IS NULL THEN '' WHEN B.SegundoApellido = 'SSA' THEN '' ELSE ' ' + LTRIM(RTRIM(B.SegundoApellido)) END) AS 'Paciente', ")
        Q.Append("(CASE WHEN LTRIM(RTRIM(B.TelefonoFijo)) IS NOT NULL THEN LTRIM(RTRIM(B.TelefonoFijo)) ELSE '' END) + ")
        Q.Append("(CASE WHEN LTRIM(RTRIM(B.TelefonoMovil)) IS NOT NULL THEN ', ' + LTRIM(RTRIM(B.TelefonoMovil)) ELSE '' END) AS 'Telefono', ")
        Q.Append("CONVERT(VARCHAR,B.FechaNacimiento,103) AS 'FechaNacimiento', ")
        Q.Append("(CASE WHEN LTRIM(RTRIM(B.Direccion)) IS NOT NULL THEN LTRIM(RTRIM(B.Direccion)) ELSE '' END) + ")
        Q.Append("(CASE WHEN LTRIM(RTRIM(E.NomMunicipio)) IS NOT NULL THEN ', ' + LTRIM(RTRIM(E.NomMunicipio)) ELSE '' END) + ")
        Q.Append("(CASE WHEN LTRIM(RTRIM(F.NomDepartamento)) IS NOT NULL THEN ', ' + LTRIM(RTRIM(F.NomDepartamento)) ELSE '' END) + ")
        Q.Append("(CASE WHEN LTRIM(RTRIM(D.NomPais)) IS NOT NULL THEN ', ' + LTRIM(RTRIM(D.NomPais)) ELSE '' END) AS 'Direccion', ")
        Q.Append("(SELECT TOP 1 N.NomMotivoBaja FROM PAC_BAJA AS M LEFT OUTER JOIN PAC_ID AS O ON M.IdPaciente = O.IdPaciente LEFT OUTER JOIN PAC_M_MOTIVOBAJA AS N ON M.MotivoBaja = N.IdMotivoBaja WHERE M.IdPaciente = A.IdPaciente AND O.Baja = 1 ORDER BY M.FechaBaja DESC) AS 'NomMotivoBaja', ")
        Q.Append("(SELECT TOP 1 dbo.fn_ObtieneClasificacion_pac(PEP.Id_Clasificacion_Pac) FROM PSOEP AS PEP WHERE PEP.NHC = A.NHC AND  PEP.Id_Clasificacion_Pac IS NOT NULL ORDER BY PEP.FechaFicha DESC) AS 'Clasificación_Pac',")
        Q.Append("(Select top 1 cita.FechaProximaVisita from BDTrabajoSocial.dbo.PAC_CITAS as cita where cita.IdPaciente = b.IdPaciente order by cita.FechaProximaVisita desc) as FechaProximaVisitaTS,")
        Q.Append("(select top 1 K.FechaProximaVisita from SIGNOSVITALES as K where K.IdPaciente = B.IdPaciente order by K.FechaProximaVisita desc) as FechaProximaVisitaMangua ")
        'Q.Append("(CASE WHEN LTRIM(RTRIM(H.NomMotivoBaja)) IS NULL THEN 'Activo' ELSE LTRIM(RTRIM(H.NomMotivoBaja)) END) AS 'MotivoBaja' ")
        Q.Append("FROM PAC_ID AS A LEFT OUTER JOIN ")
        'Q.Append("PAC_BAJA AS G ON A.IdPaciente = G.IdPaciente LEFT OUTER JOIN ")
        'Q.Append("PAC_M_MOTIVOBAJA AS H ON G.MotivoBaja = H.IdMotivoBaja LEFT OUTER JOIN ")
        Q.Append("PAC_BASALES AS B ON A.IdPaciente = B.IdPaciente LEFT OUTER JOIN ")
        Q.Append("PAC_M_GENERO AS C ON B.IdGenero = C.IdGenero LEFT OUTER JOIN ")
        Q.Append("PAC_M_PAIS AS D ON B.PaisNacimiento = D.IdPais AND B.PaisResidencia = D.IdPais LEFT OUTER JOIN ")
        Q.Append("M_MUNICIPIO AS E ON B.MunicipioNacimiento = E.IdMunicipio AND B.MunicipioResidencia = E.IdMunicipio LEFT OUTER JOIN ")
        Q.Append("M_DEPARTAMENTO AS F ON B.DepartamentoNacimiento = F.IdDepartamento AND ")
        Q.Append("B.DepartamentoResidencia = F.IdDepartamento AND E.Departamento = F.IdDepartamento AND ")
        Q.Append("E.Departamento = F.IdDepartamento ")
        Q.Append("WHERE (A.NHC = '" & nhc & "')) AS Z")
        Query = Q.ToString()
        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn2)
                connection.Open()
                Dim command As New SqlCommand(Query, connection)
                command.CommandTimeout = TimeoutDB
                Dim reader As SqlDataReader = command.ExecuteReader()
                If reader IsNot Nothing Then
                    While reader.Read()
                        Str = "True|" + reader("NomGenero") + "|" + reader("Paciente") + "|" + reader("Telefono") + "|" + reader("FechaNacimiento") + "|" + reader("Direccion") + "|" + reader("MotivoBaja") + "|" + reader("Clasificación_Pac") + "|" + reader("FechaProximaVisitaTS") + "|" + reader("FechaProximaVisitaMangua")
                        Exit While
                    End While
                End If
                If Str = String.Empty Then
                    Str = "False|No se Encontró Información."
                End If
                reader.Dispose()
                reader.Close()
                command.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page & "_" & nhc
            GrabarErrores(usuario & "|" & _pageO & "|" & ex.Number & "|" & ex.Message)
            Str = "False|" + ex.Message
        End Try
        Return Str
    End Function

    Public Function ObtieneBasalesP(ByVal nhc As String, ByVal usuario As String) As String
        _page = "db.ObtieneBasalesP"
        Dim Q As New StringBuilder()
        Dim Query As String = ""
        Dim Str As String = ""
        Q.Append("SELECT B.NomGenero, LTRIM(RTRIM(A.PrimerNombre)) + (CASE WHEN A.SegundoNombre IS NULL ")
        Q.Append("THEN '' WHEN A.SegundoNombre = 'SSN' THEN '' ELSE ' ' + LTRIM(RTRIM(A.SegundoNombre)) END) + ' ' + LTRIM(RTRIM(A.PrimerApellido)) ")
        Q.Append("+ (CASE WHEN A.SegundoApellido IS NULL THEN '' WHEN A.SegundoApellido = 'SSA' THEN '' ELSE ' ' + LTRIM(RTRIM(A.SegundoApellido)) END) AS 'Paciente', ")
        'Q.Append("(CASE WHEN A.FechaNacimiento = '' THEN '' ELSE CONVERT(VARCHAR, CONVERT(DATE, (SUBSTRING(A.FechaNacimiento,4,2)+'/'+SUBSTRING(A.FechaNacimiento,1,2)+'/'+SUBSTRING(A.FechaNacimiento,7,4))),103) END) AS 'FechaNacimiento', A.Telefono, A.Direccion, ")
        'Q.Append("(CASE WHEN A.FechaNacimiento = '' THEN '' ELSE CONVERT(VARCHAR, CONVERT(DATE, (SUBSTRING(A.FechaNacimiento,1,2)+'/'+SUBSTRING(A.FechaNacimiento,4,2)+'/'+SUBSTRING(A.FechaNacimiento,7,4))),103) END) AS 'FechaNacimiento', A.Telefono, A.Direccion, ")
        Q.Append("(CASE WHEN A.FechaNacimiento = '' THEN '' ELSE A.FechaNacimiento END) AS 'FechaNacimiento', A.Telefono, A.Direccion, ")
        Q.Append("(CASE WHEN LTRIM(RTRIM(C.NomMotivoBaja)) IS NULL THEN '' ELSE LTRIM(RTRIM(C.NomMotivoBaja)) END) AS 'MotivoBaja' ")
        Q.Append("FROM BasalesPediatria AS A LEFT OUTER JOIN ")
        Q.Append("PAC_M_GENERO AS B ON A.Genero = B.IdGenero LEFT OUTER JOIN ")
        Q.Append("PAC_M_MOTIVOBAJA AS C ON A.IdBaja = C.IdMotivoBaja ")
        Q.Append("WHERE (A.NHC = '" & nhc & "')")
        Query = Q.ToString()
        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim command As New SqlCommand(Query, connection)
                command.CommandTimeout = TimeoutDB
                Dim reader As SqlDataReader = command.ExecuteReader()
                If reader IsNot Nothing Then
                    While reader.Read()
                        Str = "True|" + reader("NomGenero") + "|" + reader("Paciente") + "|" + reader("Telefono") + "|" + reader("FechaNacimiento") + "|" + reader("Direccion") + "|" + reader("MotivoBaja")
                        Exit While
                    End While
                End If
                If Str = String.Empty Then
                    Str = "False|No se Encontró Información."
                End If
                reader.Dispose()
                reader.Close()
                command.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page & "_" & nhc
            GrabarErrores(usuario & "|" & _page & "|" & ex.Number & "|" & ex.Message)
            Str = "False|" + ex.Message
        End Try
        Return Str
    End Function

    Public Function ObtieneBasalesP2(ByVal nhc As String, ByVal usuario As String) As String
        _page = "db.ObtieneBasalesP2"
        Dim Q As New StringBuilder()
        Dim Query As String = ""
        Dim Str As String = ""
        Q.Append("SELECT A.Genero, A.PrimerNombre, A.SegundoNombre, A.PrimerApellido, A.SegundoApellido, ")
        'Q.Append("(CASE WHEN A.FechaNacimiento = '' THEN '' ELSE CONVERT(VARCHAR, CONVERT(DATE, (SUBSTRING(A.FechaNacimiento,4,2)+'/'+SUBSTRING(A.FechaNacimiento,1,2)+'/'+SUBSTRING(A.FechaNacimiento,7,4))),103) END) AS 'FechaNacimiento', A.Telefono, A.Direccion, IdBaja ")
        Q.Append("(CASE WHEN A.FechaNacimiento = '' THEN '' ELSE A.FechaNacimiento END) AS 'FechaNacimiento', A.Telefono, A.Direccion, IdBaja ")
        Q.Append("FROM BasalesPediatria AS A ")
        Q.Append("WHERE (A.NHC = '" & nhc & "')")
        Query = Q.ToString()
        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim command As New SqlCommand(Query, connection)
                command.CommandTimeout = TimeoutDB
                Dim reader As SqlDataReader = command.ExecuteReader()
                If reader IsNot Nothing Then
                    While reader.Read()
                        Str = "True|" + reader("Genero").ToString() + "|" + reader("PrimerNombre").ToString() + "|" + reader("SegundoNombre").ToString() + "|" + reader("PrimerApellido").ToString() + "|" + reader("SegundoApellido").ToString() + "|" + reader("FechaNacimiento").ToString() + "|" + reader("Telefono").ToString() + "|" + reader("Direccion").ToString() + "|" + reader("IdBaja").ToString()
                        Exit While
                    End While
                End If
                If Str = String.Empty Then
                    Str = "False|No se Encontró Información."
                End If
                reader.Dispose()
                reader.Close()
                command.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page & "_" & nhc
            GrabarErrores(usuario & "|" & _page & "|" & ex.Number & "|" & ex.Message)
            Str = "False|" + ex.Message
        End Try
        Return Str
    End Function

    '*Obtiene Profilaxis y dosis*//
    Public Function ObtieneMED(ByVal codigo As String, ByVal usuario As String) As String
        _page = "db.ObtieneMED"
        Dim Q As New StringBuilder()
        Dim Query As String = ""
        Dim Str As String = ""
        Q.Append("SELECT M.NomProfilaxis, F.Concentracion ")
        Q.Append("FROM FFProf AS F INNER JOIN ")
        Q.Append("MedProf AS M ON F.IdProf = M.IdProf ")
        Q.Append("WHERE F.IdFFProf = '" & codigo & "'")
        Query = Q.ToString()
        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim command As New SqlCommand(Query, connection)
                command.CommandTimeout = TimeoutDB
                Dim reader As SqlDataReader = command.ExecuteReader()
                If reader IsNot Nothing Then
                    While reader.Read()
                        Str = "True|" + reader("NomProfilaxis") + "|" + reader("Concentracion")
                        Exit While
                    End While
                End If
                If Str = String.Empty Then
                    Str = "False|No se Encontró Información."
                End If
                reader.Dispose()
                reader.Close()
                command.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page & "_" & codigo
            GrabarErrores(usuario & "|" & _pageO & "|" & ex.Number & "|" & ex.Message)
            Str = "False|" + ex.Message
        End Try
        Return Str
    End Function
    '*Obtiene medicamento, forma farmaceutica y concentracion (Ingreso Inventario MEd ARV)
    Public Function ObtieneARV_FF_Concentracion(ByVal codigo As String, ByVal usuario As String) As String
        _page = "db.ObtieneARV_FF_Concentracion"
        Dim Q As New StringBuilder()
        Dim Query As String = ""
        Dim Str As String = ""
        Q.Append("SELECT FA.IdFFARV, M.NomARV, FA.Concentracion, FR.NomFF  ")
        Q.Append("FROM FFARV AS FA INNER JOIN ")
        Q.Append("MedARV AS M ON FA.IdARV = M.IdARV LEFT OUTER JOIN ")
        Q.Append("FormaFarmaceutica AS FR ON FR.IdFF = FA.IdFF ")
        Q.Append("WHERE FA.IdFFARV = '" & codigo & "'")
        Query = Q.ToString()
        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim command As New SqlCommand(Query, connection)
                command.CommandTimeout = TimeoutDB
                Dim reader As SqlDataReader = command.ExecuteReader()
                If reader IsNot Nothing Then
                    While reader.Read()
                        Str = "True|" + reader("NomARV") + "|" + reader("NomFF") + "|" + reader("Concentracion")
                        Exit While
                    End While
                End If
                If Str = String.Empty Then
                    Str = "False|No se Encontró Información."
                End If
                reader.Dispose()
                reader.Close()
                command.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page & "_" & codigo
            GrabarErrores(usuario & "|" & _pageO & "|" & ex.Number & "|" & ex.Message)
            Str = "False|" + ex.Message
        End Try
        Return Str
    End Function
    '*Obtiene medicamento, forma farmaceutica y concentracion (Ingreso Inventario MEd PROF)
    Public Function ObtienePROF_FF_Concentracion(ByVal codigo As String, ByVal usuario As String) As String
        _page = "db.ObtienePROF_FF_Concentracion"
        Dim Q As New StringBuilder()
        Dim Query As String = ""
        Dim Str As String = ""
        Q.Append("SELECT FP.IdFFProf, M.NomProfilaxis, FP.Concentracion, FR.NomFF ")
        Q.Append("FROM FFProf AS FP INNER JOIN ")
        Q.Append("MedProf AS M ON FP.IdProf = M.IdProf LEFT OUTER JOIN ")
        Q.Append("FormaFarmaceutica AS FR ON FR.IdFF = FP.IdFF ")
        Q.Append("WHERE FP.IdFFProf = '" & codigo & "'")
        Query = Q.ToString()
        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim command As New SqlCommand(Query, connection)
                command.CommandTimeout = TimeoutDB
                Dim reader As SqlDataReader = command.ExecuteReader()
                If reader IsNot Nothing Then
                    While reader.Read()
                        Str = "True|" + reader("NomProfilaxis") + "|" + reader("NomFF") + "|" + reader("Concentracion")
                        Exit While
                    End While
                End If
                If Str = String.Empty Then
                    Str = "False|No se Encontró Información."
                End If
                reader.Dispose()
                reader.Close()
                command.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page & "_" & codigo
            GrabarErrores(usuario & "|" & _pageO & "|" & ex.Number & "|" & ex.Message)
            Str = "False|" + ex.Message
        End Try
        Return Str
    End Function
    '*Obtiene Datos Existencias ARV diario
    Public Function ObtieneARV_FF_Existencias_D(ByVal usuario As String) As DataTable
        _page = "db.ObtieneARV_FF_Existencias_D"
        Dim Q As New StringBuilder()
        Dim Query As String = ""
        Dim Str As String = ""
        Q.Append("SELECT FA.IdFFARV, convert(VARCHAR,getdate(),103) AS 'Fecha_Corte', convert(nchar , FA.Codigo,0) AS 'Codigo', (M.NomARV) + '/' + (FA.Concentracion) + '/' + (FR.NomFF) AS 'Medicamento', FA.Existencia ")
        Q.Append("FROM FFARV AS FA LEFT OUTER  JOIN ")
        Q.Append("MedARV AS M ON FA.IdARV = M.IdARV LEFT OUTER JOIN  ")
        Q.Append("FormaFarmaceutica AS FR ON FR.IdFF = FA.IdFF ")
        Query = Q.ToString()
        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim adapter As New SqlDataAdapter()
                adapter.SelectCommand = New SqlCommand(Query, connection)
                adapter.SelectCommand.CommandTimeout = TimeoutDB
                adapter.Fill(Ds, _page)
                adapter.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
            Return Ds.Tables(0)
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page
            GrabarErrores(usuario & "|" & _pageO & "|" & ex.Number & "|" & ex.Message)
            Return Nothing
        End Try

    End Function
    '*Obtiene Datos Existencias PROF diario
    Public Function ObtienePROF_FF_Existencias_D(ByVal usuario As String) As DataTable
        _page = "db.ObtienePROF_FF_Existencias_D"
        Dim Q As New StringBuilder()
        Dim Query As String = ""
        Dim Str As String = ""
        Q.Append("SELECT FP.IdFFPROF, convert(VARCHAR,getdate(),103) AS 'Fecha_Corte', convert(nchar , FP.Codigo,0) AS 'Codigo', (M.NomProfilaxis) + '/' + (FP.Concentracion) + '/' + (FR.NomFF) AS 'Medicamento', FP.Existencia ")
        Q.Append("FROM FFProf AS FP LEFT OUTER  JOIN ")
        Q.Append("MedProf AS M ON FP.IdProf = M.IdProf LEFT OUTER JOIN ")
        Q.Append("FormaFarmaceutica AS FR ON FR.IdFF = FP.IdFF ")
        Query = Q.ToString()
        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim adapter As New SqlDataAdapter()
                adapter.SelectCommand = New SqlCommand(Query, connection)
                adapter.SelectCommand.CommandTimeout = TimeoutDB
                adapter.Fill(Ds, _page)
                adapter.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
            Return Ds.Tables(0)
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page
            GrabarErrores(usuario & "|" & _pageO & "|" & ex.Number & "|" & ex.Message)
            Return Nothing
        End Try

    End Function
    '*/INSERTA OTROS EGRESOS
    Public Sub Graba_OtrosEgresos(ByVal fechaegreso As String, ByVal tipo_med As String, ByVal idff_med As String, ByVal cantidad As String, ByVal tipo_egreso As String, ByVal nhc_tv As String, ByVal usuario As String)
        _page = "db.Graba_OtrosEgresos"
        Dim sql As String = String.Format("INSERT INTO dbo.Otros_EgresosMed (Fecha_Egreso, Tipo_Medicamento, IdFF, Cantidad, Tipo_Egreso, NHC_TV, Usuario) VALUES ('{0}', {1}, {2}, {3}, {4}, '{5}', '{6}')", fechaegreso, tipo_med, idff_med, cantidad, tipo_egreso, If(String.IsNullOrEmpty(nhc_tv), "", nhc_tv), usuario)
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim command As New SqlCommand(sql, connection)
                command.ExecuteNonQuery()
                command.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page
            GrabarErrores(usuario & "|" & _page & "|" & ex.Number & "|" & ex.Message)
        End Try
    End Sub

    '*Lista FFProf-FFARV*//
    Public Function busquedaMedProf(ByVal tipo As Integer, ByVal usuario As String) As DataTable
        _page = "db.busquedaMedProf"
        Dim Query As String = ""
        If tipo = "1" Then
            Query = "SELECT A.IdFFARV, A.Codigo, B.NomARV, C.NomFF, A.Concentracion "
            Query += "FROM FFARV AS A LEFT OUTER JOIN "
            Query += "MedARV AS B ON A.IdARV = B.IdARV LEFT OUTER JOIN "
            Query += "FormaFarmaceutica AS C ON A.IdFF = C.IdFF "
            Query += "ORDER BY A.Codigo ASC"
        ElseIf tipo = "2" Then
            Query = "SELECT A.IdFFProf, A.Codigo, B.NomProfilaxis, C.NomFF, A.Concentracion "
            Query += "FROM FFProf AS A LEFT OUTER JOIN "
            Query += "MedProf AS B ON A.IdProf = B.IdProf LEFT OUTER JOIN "
            Query += "FormaFarmaceutica AS C ON A.IdFF = C.IdFF "
            Query += "ORDER BY A.Codigo ASC"
        End If
        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim adapter As New SqlDataAdapter()
                adapter.SelectCommand = New SqlCommand(Query, connection)
                adapter.SelectCommand.CommandTimeout = TimeoutDB
                adapter.Fill(Ds, _page)
                adapter.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
            Return Ds.Tables(0)
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page
            GrabarErrores(usuario & "|" & _page & "|" & ex.Number & "|" & ex.Message)
            Return Nothing
        End Try
    End Function

    Public Function ContenidoFFARV(ByVal id As String, ByVal usuario As String) As String
        _page = "db.ContenidoFFARV"
        Dim Q As New StringBuilder()
        Dim Query As String = "SELECT IdARV, IdFF, Concentracion FROM FFARV WHERE IdFFARV = " & id
        Dim Str As String = ""
        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim command As New SqlCommand(Query, connection)
                command.CommandTimeout = TimeoutDB
                Dim reader As SqlDataReader = command.ExecuteReader()
                If reader IsNot Nothing Then
                    While reader.Read()
                        Str = "True|" + reader("IdARV").ToString() + "|" + reader("IdFF").ToString() + "|" + reader("Concentracion").ToString()
                        Exit While
                    End While
                End If
                If Str = String.Empty Then
                    Str = "False|No existe Información."
                End If
                reader.Dispose()
                reader.Close()
                command.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page & "_" & id
            GrabarErrores(usuario & "|" & _pageO & "|" & ex.Number & "|" & ex.Message)
            Str = "False|" + ex.Message
        End Try
        Return Str
    End Function

    Public Function ContenidoFFProf(ByVal id As String, ByVal usuario As String) As String
        _page = "db.ContenidoFFProf"
        Dim Q As New StringBuilder()
        Dim Query As String = "SELECT IdProf, IdFF, Concentracion FROM FFProf WHERE IdFFProf = " & id
        Dim Str As String = ""
        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim command As New SqlCommand(Query, connection)
                command.CommandTimeout = TimeoutDB
                Dim reader As SqlDataReader = command.ExecuteReader()
                If reader IsNot Nothing Then
                    While reader.Read()
                        Str = "True|" + reader("IdProf").ToString() + "|" + reader("IdFF").ToString() + "|" + reader("Concentracion").ToString()
                        Exit While
                    End While
                End If
                If Str = String.Empty Then
                    Str = "False|No existe Información."
                End If
                reader.Dispose()
                reader.Close()
                command.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page & "_" & id
            GrabarErrores(usuario & "|" & _pageO & "|" & ex.Number & "|" & ex.Message)
            Str = "False|" + ex.Message
        End Try
        Return Str
    End Function

    Public Function busquedaCod(ByVal tipo As Integer, ByVal usuario As String) As DataTable
        _page = "db.busquedaCod"
        Dim Query As String = ""
        If tipo = "1" Then
            Query = "SELECT IdARV, NomARV, NomCorto FROM MedARV ORDER BY IdARV"
        ElseIf tipo = "2" Then
            Query = "SELECT IdProf, NomProfilaxis FROM MedProf ORDER BY IdProf"
        End If
        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim adapter As New SqlDataAdapter()
                adapter.SelectCommand = New SqlCommand(Query, connection)
                adapter.SelectCommand.CommandTimeout = TimeoutDB
                adapter.Fill(Ds, _page)
                adapter.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
            Return Ds.Tables(0)
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page
            GrabarErrores(usuario & "|" & _page & "|" & ex.Number & "|" & ex.Message)
            Return Nothing
        End Try
    End Function

    Public Function ContenidoCodARV(ByVal id As String, ByVal usuario As String) As String
        _page = "db.ContenidoCodARV"
        Dim Q As New StringBuilder()
        Dim Query As String = "SELECT IdARV, NomARV, NomCorto FROM MedARV WHERE IdARV = " & id
        Dim Str As String = ""
        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim command As New SqlCommand(Query, connection)
                command.CommandTimeout = TimeoutDB
                Dim reader As SqlDataReader = command.ExecuteReader()
                If reader IsNot Nothing Then
                    While reader.Read()
                        Str = "True|" + reader("IdARV").ToString() + "|" + reader("NomARV").ToString() + "|" + reader("NomCorto").ToString()
                        Exit While
                    End While
                End If
                If Str = String.Empty Then
                    Str = "False|No existe Información."
                End If
                reader.Dispose()
                reader.Close()
                command.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page & "_" & id
            GrabarErrores(usuario & "|" & _pageO & "|" & ex.Number & "|" & ex.Message)
            Str = "False|" + ex.Message
        End Try
        Return Str
    End Function

    Public Function ContenidoCodProf(ByVal id As String, ByVal usuario As String) As String
        _page = "db.ContenidoCodProf"
        Dim Q As New StringBuilder()
        Dim Query As String = "SELECT IdProf, NomProfilaxis FROM MedProf WHERE IdProf = " & id
        Dim Str As String = ""
        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim command As New SqlCommand(Query, connection)
                command.CommandTimeout = TimeoutDB
                Dim reader As SqlDataReader = command.ExecuteReader()
                If reader IsNot Nothing Then
                    While reader.Read()
                        Str = "True|" + reader("IdProf").ToString() + "|" + reader("NomProfilaxis").ToString()
                        Exit While
                    End While
                End If
                If Str = String.Empty Then
                    Str = "False|No existe Información."
                End If
                reader.Dispose()
                reader.Close()
                command.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page & "_" & id
            GrabarErrores(usuario & "|" & _pageO & "|" & ex.Number & "|" & ex.Message)
            Str = "False|" + ex.Message
        End Try
        Return Str
    End Function

    '*Lista FF*//
    Public Function busquedaff(ByVal usuario As String) As DataTable
        _page = "db.busquedaff"
        Dim Query As String = ""
        Query = "SELECT IdFF, NomFF FROM FormaFarmaceutica ORDER BY IdFF"
        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim adapter As New SqlDataAdapter()
                adapter.SelectCommand = New SqlCommand(Query, connection)
                adapter.SelectCommand.CommandTimeout = TimeoutDB
                adapter.Fill(Ds, _page)
                adapter.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
            Return Ds.Tables(0)
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page
            GrabarErrores(usuario & "|" & _page & "|" & ex.Number & "|" & ex.Message)
            Return Nothing
        End Try
    End Function

    '*Graba FF*//
    Public Sub GrabaFF(ByVal ff As String, ByVal usuario As String)
        _page = "db.GrabaFF"
        Dim sql As String = String.Format("INSERT INTO FormaFarmaceutica (NomFF) VALUES ('{0}')", ff)
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim command As New SqlCommand(sql, connection)
                command.ExecuteNonQuery()
                command.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page
            GrabarErrores(usuario & "|" & _page & "|" & ex.Number & "|" & ex.Message)
        End Try
    End Sub

    Public Function ContenidoFF(ByVal id As String, ByVal usuario As String) As String
        _page = "db.ContenidoFF"
        Dim Q As New StringBuilder()
        Dim Query As String = "SELECT IdFF, NomFF FROM FormaFarmaceutica WHERE IdFF = " & id
        Dim Str As String = ""
        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim command As New SqlCommand(Query, connection)
                command.CommandTimeout = TimeoutDB
                Dim reader As SqlDataReader = command.ExecuteReader()
                If reader IsNot Nothing Then
                    While reader.Read()
                        Str = "True|" + reader("IdFF").ToString() + "|" + reader("NomFF").ToString()
                        Exit While
                    End While
                End If
                If Str = String.Empty Then
                    Str = "False|No existe Información."
                End If
                reader.Dispose()
                reader.Close()
                command.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page & "_" & id
            GrabarErrores(usuario & "|" & _pageO & "|" & ex.Number & "|" & ex.Message)
            Str = "False|" + ex.Message
        End Try
        Return Str
    End Function

    Public Sub ActualizaFF(ByVal id As String, ByVal datos As String, ByVal usuario As String)
        _page = "db.ActualizaFF"
        'Dim d As String() = datos.Split("|")
        Dim sql As String = String.Format("UPDATE FormaFarmaceutica SET NomFF = '{1}', FechaModificacion = GETDATE() WHERE IdFF = '{0}'", id, datos.ToString())
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim command As New SqlCommand(sql, connection)
                command.ExecuteNonQuery()
                command.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page + "_" + id
            GrabarErrores(usuario & "|" & _pageO & "|" & ex.Number & "|" & ex.Message)
        End Try
    End Sub

    '*Lista Estatus*//
    Public Function busquedaEstatus(ByVal usuario As String) As DataTable
        _page = "db.busquedaEstatus"
        Dim Query As String = ""
        Query = "SELECT IdEstatus, Codigo, Descripcion FROM Estatus ORDER BY IdEstatus"
        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim adapter As New SqlDataAdapter()
                adapter.SelectCommand = New SqlCommand(Query, connection)
                adapter.SelectCommand.CommandTimeout = TimeoutDB
                adapter.Fill(Ds, _page)
                adapter.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
            Return Ds.Tables(0)
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page
            GrabarErrores(usuario & "|" & _page & "|" & ex.Number & "|" & ex.Message)
            Return Nothing
        End Try
    End Function

    '*Graba Estatus*//
    Public Sub GrabaEstatus(ByVal codigo As String, ByVal descripcion As String, ByVal usuario As String)
        _page = "db.GrabaEstatus"
        Dim sql As String = String.Format("INSERT INTO Estatus (Codigo, Descripcion) VALUES ('{0}', '{1}')", codigo, descripcion)
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim command As New SqlCommand(sql, connection)
                command.ExecuteNonQuery()
                command.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page
            GrabarErrores(usuario & "|" & _page & "|" & ex.Number & "|" & ex.Message)
        End Try
    End Sub

    Public Function ContenidoEstatus(ByVal id As String, ByVal usuario As String) As String
        _page = "db.ContenidoEstatus"
        Dim Q As New StringBuilder()
        Dim Query As String = "SELECT IdEstatus, Codigo, Descripcion FROM Estatus WHERE IdEstatus = " & id
        Dim Str As String = ""
        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim command As New SqlCommand(Query, connection)
                command.CommandTimeout = TimeoutDB
                Dim reader As SqlDataReader = command.ExecuteReader()
                If reader IsNot Nothing Then
                    While reader.Read()
                        Str = "True|" + reader("IdEstatus").ToString() + "|" + reader("Codigo").ToString() + "|" + reader("Descripcion").ToString()
                        Exit While
                    End While
                End If
                If Str = String.Empty Then
                    Str = "False|No existe Información."
                End If
                reader.Dispose()
                reader.Close()
                command.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page & "_" & id
            GrabarErrores(usuario & "|" & _pageO & "|" & ex.Number & "|" & ex.Message)
            Str = "False|" + ex.Message
        End Try
        Return Str
    End Function

    Public Sub ActualizaEstatus(ByVal id As String, ByVal codigo As String, ByVal descripcion As String, ByVal usuario As String)
        _page = "db.ActualizaEstatus"
        'Dim d As String() = datos.Split("|")
        Dim sql As String = String.Format("UPDATE Estatus SET Codigo = '{1}', Descripcion = '{2}' WHERE IdEstatus = '{0}'", id, codigo, descripcion)
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim command As New SqlCommand(sql, connection)
                command.ExecuteNonQuery()
                command.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page + "_" + id
            GrabarErrores(usuario & "|" & _pageO & "|" & ex.Number & "|" & ex.Message)
        End Try
    End Sub

    '*Lista Frecuencia*//
    Public Function busquedaFx(ByVal usuario As String) As DataTable
        _page = "db.busquedaFx"
        Dim Query As String = ""
        Query = "SELECT IdFrecuencia, Codigo, Descripcion FROM Frecuencia ORDER BY IdFrecuencia"
        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim adapter As New SqlDataAdapter()
                adapter.SelectCommand = New SqlCommand(Query, connection)
                adapter.SelectCommand.CommandTimeout = TimeoutDB
                adapter.Fill(Ds, _page)
                adapter.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
            Return Ds.Tables(0)
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page
            GrabarErrores(usuario & "|" & _page & "|" & ex.Number & "|" & ex.Message)
            Return Nothing
        End Try
    End Function

    '*Graba Frecuencia*//
    Public Sub GrabaFx(ByVal codigo As String, ByVal descripcion As String, ByVal usuario As String)
        _page = "db.GrabaFx"
        Dim sql As String = String.Format("INSERT INTO Frecuencia (Codigo, Descripcion) VALUES ('{0}', '{1}')", codigo, descripcion)
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim command As New SqlCommand(sql, connection)
                command.ExecuteNonQuery()
                command.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page
            GrabarErrores(usuario & "|" & _page & "|" & ex.Number & "|" & ex.Message)
        End Try
    End Sub

    Public Function ContenidoFx(ByVal id As String, ByVal usuario As String) As String
        _page = "db.ContenidoFx"
        Dim Q As New StringBuilder()
        Dim Query As String = "SELECT IdFrecuencia, Codigo, Descripcion FROM Frecuencia WHERE IdFrecuencia = " & id
        Dim Str As String = ""
        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim command As New SqlCommand(Query, connection)
                command.CommandTimeout = TimeoutDB
                Dim reader As SqlDataReader = command.ExecuteReader()
                If reader IsNot Nothing Then
                    While reader.Read()
                        Str = "True|" + reader("IdFrecuencia").ToString() + "|" + reader("Codigo").ToString() + "|" + reader("Descripcion").ToString()
                        Exit While
                    End While
                End If
                If Str = String.Empty Then
                    Str = "False|No existe Información."
                End If
                reader.Dispose()
                reader.Close()
                command.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page & "_" & id
            GrabarErrores(usuario & "|" & _pageO & "|" & ex.Number & "|" & ex.Message)
            Str = "False|" + ex.Message
        End Try
        Return Str
    End Function

    Public Sub ActualizaFx(ByVal id As String, ByVal codigo As String, ByVal descripcion As String, ByVal usuario As String)
        _page = "db.ActualizaFx"
        'Dim d As String() = datos.Split("|")
        Dim sql As String = String.Format("UPDATE Frecuencia SET Codigo = '{1}', Descripcion = '{2}' WHERE IdFrecuencia = '{0}'", id, codigo.ToString(), descripcion.ToString())
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim command As New SqlCommand(sql, connection)
                command.ExecuteNonQuery()
                command.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page + "_" + id
            GrabarErrores(usuario & "|" & _pageO & "|" & ex.Number & "|" & ex.Message)
        End Try
    End Sub

    '*Codigo MedARV*//
    Public Function ObtieneMedARVProf(ByVal tipo As String, ByVal usuario As String) As DataTable
        _page = "db.ObtieneMedARVProf"
        Dim Query As String = ""
        If tipo = "1" Then
            Query = "SELECT IdARV, NomARV FROM MedARV ORDER BY NomARV ASC"
        ElseIf tipo = "2" Then
            Query = "SELECT IdProf, NomProfilaxis FROM MedProf ORDER BY NomProfilaxis ASC"
        End If

        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim adapter As New SqlDataAdapter()
                adapter.SelectCommand = New SqlCommand(Query, connection)
                adapter.SelectCommand.CommandTimeout = TimeoutDB
                adapter.Fill(Ds, _page)
                adapter.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
            Return Ds.Tables(0)
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page
            GrabarErrores(usuario & "|" & _page & "|" & ex.Number & "|" & ex.Message)
            Return Nothing
        End Try
    End Function

    Public Function ObtieneMedARV(ByVal id As String, ByVal usuario As String) As String
        _page = "db.ObtieneMedARV"
        Dim Query As String = ""
        Query = "SELECT NomCorto FROM MedARV WHERE IdARV = " & id
        Dim Ds As New DataSet()
        Dim Str As String = ""
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim command As New SqlCommand(Query, connection)
                command.CommandTimeout = TimeoutDB
                Dim reader As SqlDataReader = command.ExecuteReader()
                If reader IsNot Nothing Then
                    While reader.Read()
                        Str = "True|" + reader("NomCorto").ToString()
                        Exit While
                    End While
                End If
                If Str = String.Empty Then
                    Str = "False|No existe Información."
                End If
                reader.Dispose()
                reader.Close()
                command.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page & "_" & id
            GrabarErrores(usuario & "|" & _pageO & "|" & ex.Number & "|" & ex.Message)
            Str = "False|" + ex.Message
        End Try
        Return Str
    End Function

    '*Codigo FF*//
    Public Function ObtieneFF(ByVal usuario As String) As DataTable
        _page = "db.ObtieneFF"
        Dim Query As String = "SELECT IdFF, NomFF FROM FormaFarmaceutica ORDER BY NomFF ASC"

        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim adapter As New SqlDataAdapter()
                adapter.SelectCommand = New SqlCommand(Query, connection)
                adapter.SelectCommand.CommandTimeout = TimeoutDB
                adapter.Fill(Ds, _page)
                adapter.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
            Return Ds.Tables(0)
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page
            GrabarErrores(usuario & "|" & _page & "|" & ex.Number & "|" & ex.Message)
            Return Nothing
        End Try
    End Function

    '*Codigo ARV/Medicamentos*//
    Public Function ObtieneARVMedicamento(ByVal tipo As String, ByVal usuario As String) As DataTable
        _page = "db.ObtieneARVMedicamento"
        Dim Query As String = ""
        If tipo = "1" Then
            Query = "SELECT IdFFARV, Codigo FROM FFARV ORDER BY Codigo ASC"
        ElseIf tipo = "2" Then
            Query = "SELECT IdFFProf, Codigo FROM FFProf WHERE IdFFProf NOT IN (21,22,23) ORDER BY Codigo ASC"
        ElseIf tipo = "3" Then
            Query = "SELECT IdFFProf, Codigo FROM FFProf ORDER BY Codigo ASC"
        End If

        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim adapter As New SqlDataAdapter()
                adapter.SelectCommand = New SqlCommand(Query, connection)
                adapter.SelectCommand.CommandTimeout = TimeoutDB
                adapter.Fill(Ds, _page)
                adapter.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
            Return Ds.Tables(0)
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page
            GrabarErrores(usuario & "|" & _page & "|" & ex.Number & "|" & ex.Message)
            Return Nothing
        End Try
    End Function
    '*/Obtiene Profilaxis Med y condones/lubricantes Ingreso inventario prof
    Public Function ObtienePROFMedicamento(ByVal tipo As String, ByVal usuario As String) As DataTable
        _page = "db.ObtienePROFMedicamento"
        Dim Query As String = ""
        If tipo = "1" Then
            Query = "SELECT IdFFARV, Codigo FROM FFARV ORDER BY Codigo ASC"
        ElseIf tipo = "2" Then
            Query = "SELECT IdFFProf, Codigo FROM FFProf  ORDER BY Codigo ASC"
        End If

        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim adapter As New SqlDataAdapter()
                adapter.SelectCommand = New SqlCommand(Query, connection)
                adapter.SelectCommand.CommandTimeout = TimeoutDB
                adapter.Fill(Ds, _page)
                adapter.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
            Return Ds.Tables(0)
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page
            GrabarErrores(usuario & "|" & _page & "|" & ex.Number & "|" & ex.Message)
            Return Nothing
        End Try
    End Function
    '*/Obtiene tipo Egreso
    Public Function ObtieneTipoEgresoMed(ByVal usuario As String) As DataTable
        _page = "db.ObtieneTipoEgresoMed"
        Dim Query As String = ""

        Query = "SELECT Id_TipoEgreso, Nom_TipoEgreso FROM dbo.Tipo_Egreso_Med ORDER BY Id_TipoEgreso ASC"


        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim adapter As New SqlDataAdapter()
                adapter.SelectCommand = New SqlCommand(Query, connection)
                adapter.SelectCommand.CommandTimeout = TimeoutDB
                adapter.Fill(Ds, _page)
                adapter.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
            Return Ds.Tables(0)
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page
            GrabarErrores(usuario & "|" & _page & "|" & ex.Number & "|" & ex.Message)
            Return Nothing
        End Try
    End Function

    '*OBTIENE CODIGO CONDONES LUBRICANTES
    Public Function ObtieneCodCondones(ByVal tipo As String, ByVal usuario As String) As DataTable
        _page = "db.ObtieneCodCondones"
        Dim Query As String = ""
        If tipo = "1" Then
            Query = "SELECT IdFFARV, Codigo FROM FFARV ORDER BY Codigo ASC"
        ElseIf tipo = "2" Then
            Query = "SELECT IdFFProf, Codigo FROM FFProf WHERE IdFFProf IN (21,22,23) ORDER BY Codigo ASC"
        End If

        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim adapter As New SqlDataAdapter()
                adapter.SelectCommand = New SqlCommand(Query, connection)
                adapter.SelectCommand.CommandTimeout = TimeoutDB
                adapter.Fill(Ds, _page)
                adapter.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
            Return Ds.Tables(0)
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page
            GrabarErrores(usuario & "|" & _page & "|" & ex.Number & "|" & ex.Message)
            Return Nothing
        End Try
    End Function
    Public Function ObtieneARVMedicamentoXid(ByVal id As String, ByVal usuario As String) As DataTable
        _page = "db.ObtieneARVMedicamentoXid"
        Dim Query As String = ""
        Query = String.Format("dbo.sp_ListaMedXEsquema {0}", id)
        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn1)
                Dim coDetalle As New SqlCommand
                coDetalle.CommandText = "sp_ListaMedXEsquema"
                coDetalle.CommandType = CommandType.StoredProcedure
                coDetalle.Connection = connection  'Previamente definida
                'El Adaptador y su SelectCommand
                Dim daDetalle As New SqlDataAdapter
                daDetalle.SelectCommand = coDetalle
                'Parámetros si hubieran
                Dim miParam As New SqlParameter("@esquema", SqlDbType.Int)
                miParam.Direction = ParameterDirection.Input
                coDetalle.Parameters.Add(miParam)
                coDetalle.Parameters("@esquema").Value = id
                'Llenar el DataSet
                'Al llenar el DataSet se ejecuta el Store Procedure
                'el SQLCommand del SQLDataAdapter se especificó del tipo StoreProcedure
                daDetalle.Fill(Ds, _page)
                daDetalle.Dispose()
                'connection.Dispose()
                'connection.Close()
            End Using
            Return Ds.Tables(0)
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page
            GrabarErrores(usuario & "|" & _page & "|" & ex.Number & "|" & ex.Message)
            Return Nothing
        End Try
    End Function

    Public Function ObtieneARVMedicamentoXidSE(ByVal id As String, ByVal usuario As String) As DataTable
        _page = "db.ObtieneARVMedicamentoXidSE"
        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn1)
                Dim coDetalle As New SqlCommand
                coDetalle.CommandText = "sp_ListaMedXEsquema4"
                coDetalle.CommandType = CommandType.StoredProcedure
                coDetalle.Connection = connection  'Previamente definida
                'El Adaptador y su SelectCommand
                Dim daDetalle As New SqlDataAdapter
                daDetalle.SelectCommand = coDetalle
                'Parámetros si hubieran
                Dim miParam As New SqlParameter("@sesquema", SqlDbType.Int)
                miParam.Direction = ParameterDirection.Input
                coDetalle.Parameters.Add(miParam)
                coDetalle.Parameters("@sesquema").Value = id
                'Llenar el DataSet
                'Al llenar el DataSet se ejecuta el Store Procedure
                'el SQLCommand del SQLDataAdapter se especificó del tipo StoreProcedure
                daDetalle.Fill(Ds, _page)
                daDetalle.Dispose()
            End Using
            Return Ds.Tables(0)
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page
            GrabarErrores(usuario & "|" & _page & "|" & ex.Number & "|" & ex.Message)
            Return Nothing
        End Try
    End Function

    Public Function ObtieneARVMedicamentoXid3(ByVal id As String, ByVal usuario As String) As DataTable
        _page = "db.ObtieneARVMedicamentoXid3"
        Dim Query As String = ""
        Query = String.Format("dbo.sp_ListaMedXEsquema3 {0}", id)
        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn1)
                Dim coDetalle As New SqlCommand
                coDetalle.CommandText = "sp_ListaMedXEsquema3"
                coDetalle.CommandType = CommandType.StoredProcedure
                coDetalle.Connection = connection  'Previamente definida
                'El Adaptador y su SelectCommand
                Dim daDetalle As New SqlDataAdapter
                daDetalle.SelectCommand = coDetalle
                'Parámetros si hubieran
                Dim miParam As New SqlParameter("@esquema", SqlDbType.Int)
                miParam.Direction = ParameterDirection.Input
                coDetalle.Parameters.Add(miParam)
                coDetalle.Parameters("@esquema").Value = id
                'Llenar el DataSet
                'Al llenar el DataSet se ejecuta el Store Procedure
                'el SQLCommand del SQLDataAdapter se especificó del tipo StoreProcedure
                daDetalle.Fill(Ds, _page)
                daDetalle.Dispose()
                'connection.Dispose()
                'connection.Close()
            End Using
            Return Ds.Tables(0)
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page
            GrabarErrores(usuario & "|" & _page & "|" & ex.Number & "|" & ex.Message)
            Return Nothing
        End Try
    End Function

    '*Codigo Estatus*//
    Public Function ObtieneEstatus(ByVal usuario As String) As DataTable
        _page = "db.ObtieneEstatus"
        Dim Query As String = "SELECT IdEstatus, Codigo FROM Estatus ORDER BY Codigo ASC"
        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim adapter As New SqlDataAdapter()
                adapter.SelectCommand = New SqlCommand(Query, connection)
                adapter.SelectCommand.CommandTimeout = TimeoutDB
                adapter.Fill(Ds, _page)
                adapter.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
            Return Ds.Tables(0)
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page
            GrabarErrores(usuario & "|" & _page & "|" & ex.Number & "|" & ex.Message)
            Return Nothing
        End Try
    End Function

    Public Function ObtieneEstatusProf(ByVal usuario As String) As DataTable
        _page = "db.ObtieneEstatusProf"
        Dim Query As String = "SELECT IdEstatus, Codigo FROM Estatus WHERE IdEstatus IN (2,1,8,3) ORDER BY Codigo ASC"
        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim adapter As New SqlDataAdapter()
                adapter.SelectCommand = New SqlCommand(Query, connection)
                adapter.SelectCommand.CommandTimeout = TimeoutDB
                adapter.Fill(Ds, _page)
                adapter.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
            Return Ds.Tables(0)
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page
            GrabarErrores(usuario & "|" & _page & "|" & ex.Number & "|" & ex.Message)
            Return Nothing
        End Try
    End Function

    '*Via Administración*//
    Public Function ObtieneVIA(ByVal usuario As String) As DataTable
        _page = "db.ObtieneVIA"
        Dim Query As String = "SELECT IdViaAdministracion, NomViaAdministracion FROM VIAADMINISTRACION ORDER BY IdViaAdministracion ASC"
        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim adapter As New SqlDataAdapter()
                adapter.SelectCommand = New SqlCommand(Query, connection)
                adapter.SelectCommand.CommandTimeout = TimeoutDB
                adapter.Fill(Ds, _page)
                adapter.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
            Return Ds.Tables(0)
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page
            GrabarErrores(usuario & "|" & _page & "|" & ex.Number & "|" & ex.Message)
            Return Nothing
        End Try
    End Function

    '*Codigo Frecuencia*//
    Public Function ObtieneFrecuenia(ByVal usuario As String) As DataTable
        _page = "db.ObtieneFrecuenia"
        Dim Query As String = "SELECT IdFrecuencia FROM Frecuencia ORDER BY IdFrecuencia ASC"
        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim adapter As New SqlDataAdapter()
                adapter.SelectCommand = New SqlCommand(Query, connection)
                adapter.SelectCommand.CommandTimeout = TimeoutDB
                adapter.Fill(Ds, _page)
                adapter.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
            Return Ds.Tables(0)
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page
            GrabarErrores(usuario & "|" & _page & "|" & ex.Number & "|" & ex.Message)
            Return Nothing
        End Try
    End Function

    '*Codigo Embarazo*//
    Public Function ObtieneEmbarazo(ByVal usuario As String) As DataTable
        _page = "db.ObtieneEmbarazo"
        Dim Query As String = "SELECT IdEmbarazo FROM Embarazo ORDER BY IdEmbarazo ASC"
        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim adapter As New SqlDataAdapter()
                adapter.SelectCommand = New SqlCommand(Query, connection)
                adapter.SelectCommand.CommandTimeout = TimeoutDB
                adapter.Fill(Ds, _page)
                adapter.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
            Return Ds.Tables(0)
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page
            GrabarErrores(usuario & "|" & _page & "|" & ex.Number & "|" & ex.Message)
            Return Nothing
        End Try
    End Function

    '*Codigo Baja*//
    Public Function ObtieneBaja(ByVal usuario As String) As DataTable
        _page = "db.ObtieneBaja"
        Dim Query As String = "SELECT IdMotivoBaja, NomMotivoBaja FROM PAC_M_MOTIVOBAJA ORDER BY IdMotivoBaja ASC"
        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim adapter As New SqlDataAdapter()
                adapter.SelectCommand = New SqlCommand(Query, connection)
                adapter.SelectCommand.CommandTimeout = TimeoutDB
                adapter.Fill(Ds, _page)
                adapter.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
            Return Ds.Tables(0)
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page
            GrabarErrores(usuario & "|" & _page & "|" & ex.Number & "|" & ex.Message)
            Return Nothing
        End Try
    End Function

    '*Ultima Fecha Entrega*//
    Public Function ObtieneUltimoReg(ByVal nhc As String, ByVal usuario As String) As String
        _page = "db.ObtieneUltimoReg"
        Dim Q As New StringBuilder()
        Dim Query As String = ""
        Dim Str As String = ""
        Q.Append("SELECT A.IdCCARV, CONVERT(VARCHAR,A.FechaEntrega, 103) AS 'FechaEntrega', (E.Codigo + ' - ' + E.Descripcion) AS 'Estatus', ")
        Q.Append("(CASE WHEN A.IdEsquema <> 0 THEN (A.IdEsquema) ELSE '' END) AS 'Esquema', ")
        Q.Append("(CASE WHEN A.IdSEsquema <> 0 THEN (A.IdSEsquema) ELSE '' END) AS 'SubEsquema', ")
        Q.Append("(CASE WHEN A.EsquemaEstatus <> 0 THEN (A.EsquemaEstatus) ELSE '' END) AS 'EstatusEsquema', ")
        Q.Append("(CASE WHEN A.Med1_Codigo <> 0 THEN (A.Med1_Codigo) ELSE '' END) AS 'Med1_Codigo',  ")
        Q.Append("(CASE WHEN A.Med1_Frecuencia <> 0 THEN (A.Med1_Frecuencia) ELSE '' END) AS 'Med1_Frecuencia', ")
        Q.Append("(CASE WHEN A.Med1_Dosis <> 0 THEN (A.Med1_Dosis) ELSE '' END) AS 'Med1_Dosis', ")
        Q.Append("(CASE WHEN A.Med1_ARVEstatus <> 0 THEN (A.Med1_ARVEstatus) ELSE '' END) AS 'Med1_Estatus', ")
        Q.Append("(CASE WHEN A.Med1_Codigo <> 0 THEN (SELECT Codigo FROM FFARV WHERE IdFFARV = A.Med1_Codigo) ELSE '' END) AS 'Med1_CodigoDesc',  ")
        Q.Append("(CASE WHEN A.Med2_Codigo <> 0 THEN (A.Med2_Codigo) ELSE '' END) AS 'Med2_Codigo',  ")
        Q.Append("(CASE WHEN A.Med2_Frecuencia <> 0 THEN (A.Med2_Frecuencia) ELSE '' END) AS 'Med2_Frecuencia', ")
        Q.Append("(CASE WHEN A.Med2_Dosis <> 0 THEN (A.Med2_Dosis) ELSE '' END) AS 'Med2_Dosis', ")
        Q.Append("(CASE WHEN A.Med2_ARVEstatus <> 0 THEN (A.Med2_ARVEstatus) ELSE '' END) AS 'Med2_Estatus', ")
        Q.Append("(CASE WHEN A.Med2_Codigo <> 0 THEN (SELECT Codigo FROM FFARV WHERE IdFFARV = A.Med2_Codigo) ELSE '' END) AS 'Med2_CodigoDesc',  ")
        Q.Append("(CASE WHEN A.Med3_Codigo <> 0 THEN (A.Med3_Codigo) ELSE '' END) AS 'Med3_Codigo',  ")
        Q.Append("(CASE WHEN A.Med3_Frecuencia <> 0 THEN (A.Med3_Frecuencia) ELSE '' END) AS 'Med3_Frecuencia',  ")
        Q.Append("(CASE WHEN A.Med3_Dosis <> 0 THEN (A.Med3_Dosis) ELSE '' END) AS 'Med3_Dosis', ")
        Q.Append("(CASE WHEN A.Med3_ARVEstatus <> 0 THEN (A.Med3_ARVEstatus) ELSE '' END) AS 'Med3_Estatus', ")
        Q.Append("(CASE WHEN A.Med3_Codigo <> 0 THEN (SELECT Codigo FROM FFARV WHERE IdFFARV = A.Med3_Codigo) ELSE '' END) AS 'Med3_CodigoDesc',  ")
        Q.Append("(CASE WHEN A.Med4_Codigo <> 0 THEN (A.Med4_Codigo) ELSE '' END) AS 'Med4_Codigo', ")
        Q.Append("(CASE WHEN A.Med4_Frecuencia <> 0 THEN (A.Med4_Frecuencia) ELSE '' END) AS 'Med4_Frecuencia', ")
        Q.Append("(CASE WHEN A.Med4_Dosis <> 0 THEN (A.Med4_Dosis) ELSE '' END) AS 'Med4_Dosis', ")
        Q.Append("(CASE WHEN A.Med4_ARVEstatus <> 0 THEN (A.Med4_ARVEstatus) ELSE '' END) AS 'Med4_Estatus',")
        Q.Append("(CASE WHEN A.Med4_Codigo <> 0 THEN (SELECT Codigo FROM FFARV WHERE IdFFARV = A.Med4_Codigo) ELSE '' END) AS 'Med4_CodigoDesc',  ")
        Q.Append("(CASE WHEN A.Med5_Codigo <> 0 THEN (A.Med5_Codigo) ELSE '' END) AS 'Med5_Codigo',  ")
        Q.Append("(CASE WHEN A.Med5_Frecuencia <> 0 THEN (A.Med5_Frecuencia) ELSE '' END) AS 'Med5_Frecuencia', ")
        Q.Append("(CASE WHEN A.Med5_Dosis <> 0 THEN (A.Med5_Dosis) ELSE '' END) AS 'Med5_Dosis', ")
        Q.Append("(CASE WHEN A.Med5_ARVEstatus <> 0 THEN (A.Med5_ARVEstatus) ELSE '' END) AS 'Med5_Estatus',")
        Q.Append("(CASE WHEN A.Med5_Codigo <> 0 THEN (SELECT Codigo FROM FFARV WHERE IdFFARV = A.Med5_Codigo) ELSE '' END) AS 'Med5_CodigoDesc',  ")
        Q.Append("(CASE WHEN A.Med6_Codigo <> 0 THEN (A.Med6_Codigo) ELSE '' END) AS 'Med6_Codigo',  ")
        Q.Append("(CASE WHEN A.Med6_Frecuencia <> 0 THEN (A.Med6_Frecuencia) ELSE '' END) AS 'Med6_Frecuencia', ")
        Q.Append("(CASE WHEN A.Med6_Dosis <> 0 THEN (A.Med6_Dosis) ELSE '' END) AS 'Med6_Dosis', ")
        Q.Append("(CASE WHEN A.Med6_ARVEstatus <> 0 THEN (A.Med6_ARVEstatus) ELSE '' END) AS 'Med6_Estatus',")
        Q.Append("(CASE WHEN A.Med6_Codigo <> 0 THEN (SELECT Codigo FROM FFARV WHERE IdFFARV = A.Med6_Codigo) ELSE '' END) AS 'Med6_CodigoDesc',  ")
        Q.Append("(CASE WHEN A.Med7_Codigo <> 0 THEN (A.Med7_Codigo) ELSE '' END) AS 'Med7_Codigo',  ")
        Q.Append("(CASE WHEN A.Med7_Frecuencia <> 0 THEN (A.Med7_Frecuencia) ELSE '' END) AS 'Med7_Frecuencia', ")
        Q.Append("(CASE WHEN A.Med7_Dosis <> 0 THEN (A.Med7_Dosis) ELSE '' END) AS 'Med7_Dosis', ")
        Q.Append("(CASE WHEN A.Med7_ARVEstatus <> 0 THEN (A.Med7_ARVEstatus) ELSE '' END) AS 'Med7_Estatus', ")
        Q.Append("(CASE WHEN A.Med7_Codigo <> 0 THEN (SELECT Codigo FROM FFARV WHERE IdFFARV = A.Med7_Codigo) ELSE '' END) AS 'Med7_CodigoDesc',  ")
        Q.Append("(CASE WHEN A.Med8_Codigo <> 0 THEN (A.Med8_Codigo) ELSE '' END) AS 'Med8_Codigo' , ")
        Q.Append("(CASE WHEN A.Med8_Frecuencia <> 0 THEN (A.Med8_Frecuencia) ELSE '' END) AS 'Med8_Frecuencia', ")
        Q.Append("(CASE WHEN A.Med8_Dosis <> 0 THEN (A.Med8_Dosis) ELSE '' END) AS 'Med8_Dosis', ")
        Q.Append("(CASE WHEN A.Med8_ARVEstatus <> 0 THEN (A.Med8_ARVEstatus) ELSE '' END) AS 'Med8_Estatus',")
        Q.Append("(CASE WHEN A.Med8_Codigo <> 0 THEN (SELECT Codigo FROM FFARV WHERE IdFFARV = A.Med8_Codigo) ELSE '' END) AS 'Med8_CodigoDesc'  ")
        Q.Append("FROM ControlARV AS A LEFT OUTER JOIN ")
        Q.Append("Estatus AS E ON E.IdEstatus = A.EsquemaEstatus ")
        Q.Append("WHERE A.NHC = '" & nhc & "' ")
        Q.Append("AND A.IdCCARV = (SELECT TOP(1) B.IdCCARV FROM ControlARV AS B WHERE B.NHC = A.NHC AND B.FechaEntrega = (SELECT DISTINCT MAX(C.FechaEntrega) FROM ControlARV AS C WHERE C.NHC = B.NHC) ORDER BY B.IdCCARV DESC) ")
        Query = Q.ToString()
        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim command As New SqlCommand(Query, connection)
                command.CommandTimeout = TimeoutDB
                Dim reader As SqlDataReader = command.ExecuteReader()
                If reader IsNot Nothing Then
                    While reader.Read()
                        Str = "True|" + reader("IdCCARV").ToString() + "|" + reader("FechaEntrega").ToString() + "|" + reader("Esquema").ToString() + "|" + reader("SubEsquema").ToString() + "|" + reader("EstatusEsquema").ToString() + "|" + reader("Med1_Codigo").ToString() + "|" + reader("Med1_Frecuencia").ToString() + "|" + reader("Med2_Codigo").ToString() + "|" + reader("Med2_Frecuencia").ToString() + "|" + reader("Med3_Codigo").ToString() + "|" + reader("Med3_Frecuencia").ToString() + "|" + reader("Med4_Codigo").ToString() + "|" + reader("Med4_Frecuencia").ToString() + "|" + reader("Med5_Codigo").ToString() + "|" + reader("Med5_Frecuencia").ToString() + "|" + reader("Med6_Codigo").ToString() + "|" + reader("Med6_Frecuencia").ToString() + "|" + reader("Med7_Codigo").ToString() + "|" + reader("Med7_Frecuencia").ToString() + "|" + reader("Med8_Codigo").ToString() + "|" + reader("Med8_Frecuencia").ToString() + "|" + reader("Estatus").ToString() + "|" + reader("Med1_CodigoDesc").ToString() + "|" + reader("Med2_CodigoDesc").ToString() + "|" + reader("Med3_CodigoDesc").ToString() + "|" + reader("Med4_CodigoDesc").ToString() + "|" + reader("Med5_CodigoDesc").ToString() + "|" + reader("Med6_CodigoDesc").ToString() + "|" + reader("Med7_CodigoDesc").ToString() + "|" + reader("Med8_CodigoDesc").ToString() + "|" + reader("Med1_Dosis").ToString() + "|" + reader("Med1_Estatus").ToString() + "|" + reader("Med2_Dosis").ToString() + "|" + reader("Med2_Estatus").ToString() + "|" + reader("Med3_Dosis").ToString() + "|" + reader("Med3_Estatus").ToString() + "|" + reader("Med4_Dosis").ToString() + "|" + reader("Med4_Estatus").ToString() + "|" + reader("Med5_Dosis").ToString() + "|" + reader("Med5_Estatus").ToString() + "|" + reader("Med6_Dosis").ToString() + "|" + reader("Med6_Estatus").ToString() + "|" + reader("Med7_Dosis").ToString() + "|" + reader("Med7_Estatus").ToString() + "|" + reader("Med8_Dosis").ToString() + "|" + reader("Med8_Estatus").ToString()
                        Exit While
                    End While
                End If
                If Str = String.Empty Then
                    Str = "False|No existe última fecha."
                End If
                reader.Dispose()
                reader.Close()
                command.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page & "_" & nhc
            GrabarErrores(usuario & "|" & _page & "|" & ex.Number & "|" & ex.Message)
            Str = "False|" + ex.Message
        End Try
        Return Str
    End Function
    '*Ultima Fecha Entrega*//
    Public Function ObtieneUltimoReg_calculo_Adherencia(ByVal nhc As String, ByVal usuario As String) As String
        _page = "db.ObtieneUltimoReg_calculo_Adherencia"
        Dim Q As New StringBuilder()
        Dim Query As String = ""
        Dim Str As String = ""
        Q.Append("SELECT TOP 1 NHC ,convert(date,FechaEntrega) FechaEntrega , convert(date,FechaRetorno)FechaRetorno,(ISNULL(A.Med1_Cantidad,0) + ISNULL(A.Med2_Cantidad,0) + ISNULL(A.Med3_Cantidad,0) + ISNULL(A.Med4_Cantidad,0) + ISNULL(A.Med5_Cantidad,0) + ISNULL(A.Med6_Cantidad,0) + ISNULL(A.Med7_Cantidad,0) + ISNULL(A.Med8_Cantidad,0)) TotalMedicamentos FROM ControlARV A ")
        Q.Append("WHERE NHC =   '" & nhc & "' ORDER BY  IdCCARV DESC ")
        Query = Q.ToString()
        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim command As New SqlCommand(Query, connection)
                command.CommandTimeout = TimeoutDB
                Dim reader As SqlDataReader = command.ExecuteReader()
                If reader IsNot Nothing Then
                    While reader.Read()
                        Str = "True|" + reader("NHC").ToString() + "|" + reader("FechaEntrega").ToString() + "|" + reader("FechaRetorno").ToString() + "|" + reader("TotalMedicamentos").ToString()
                        Exit While
                    End While
                End If
                If Str = String.Empty Then
                    Str = "False|No existe última fecha."
                End If
                reader.Dispose()
                reader.Close()
                command.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page & "_" & nhc
            GrabarErrores(usuario & "|" & _page & "|" & ex.Number & "|" & ex.Message)
            Str = "False|" + ex.Message
        End Try
        Return Str
    End Function
    Public Function ObtieneUltimoRegProf(ByVal nhc As String, ByVal usuario As String) As String
        _page = "db.ObtieneUltimoRegProf"
        Dim Q As New StringBuilder()
        Dim Query As String = ""
        Dim Str As String = ""
        Q.Append("SELECT A.IdCCPROF, CONVERT(VARCHAR,A.FechaEntrega, 103) AS 'FechaEntrega', ")
        Q.Append("(CASE WHEN A.Prof1_Codigo <> 0 THEN (SELECT Codigo FROM FFProf WHERE IdFFProf = A.Prof1_Codigo) ELSE '' END) AS 'Prof1_Codigo', ")
        Q.Append("(CASE WHEN A.Prof2_Codigo <> 0 THEN (SELECT Codigo FROM FFProf WHERE IdFFProf = A.Prof2_Codigo) ELSE '' END) AS 'Prof2_Codigo', ")
        Q.Append("(CASE WHEN A.Prof3_Codigo <> 0 THEN (SELECT Codigo FROM FFProf WHERE IdFFProf = A.Prof3_Codigo) ELSE '' END) AS 'Prof3_Codigo', ")
        Q.Append("(CASE WHEN A.Prof4_Codigo <> 0 THEN (SELECT Codigo FROM FFProf WHERE IdFFProf = A.Prof4_Codigo) ELSE '' END) AS 'Prof4_Codigo' ")
        Q.Append("FROM ControlProf AS A ")
        Q.Append("WHERE A.FechaEntrega = (SELECT DISTINCT MAX(B.FechaEntrega) FROM ControlProf AS B WHERE B.NHC = '" & nhc & "') ")
        Q.Append("AND A.NHC = '" & nhc & "'")
        Query = Q.ToString()
        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim command As New SqlCommand(Query, connection)
                command.CommandTimeout = TimeoutDB
                Dim reader As SqlDataReader = command.ExecuteReader()
                If reader IsNot Nothing Then
                    While reader.Read()
                        Str = "True|" + reader("IdCCPROF").ToString() + "|" + reader("FechaEntrega").ToString() + "|" + reader("Prof1_Codigo").ToString() + "|" + reader("Prof2_Codigo").ToString() + "|" + reader("Prof3_Codigo").ToString() + "|" + reader("Prof4_Codigo").ToString()
                        Exit While
                    End While
                End If
                If Str = String.Empty Then
                    Str = "False|No existe última fecha."
                End If
                reader.Dispose()
                reader.Close()
                command.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page & "_" & nhc
            GrabarErrores(usuario & "|" & _page & "|" & ex.Number & "|" & ex.Message)
            Str = "False|" + ex.Message
        End Try
        Return Str
    End Function

    Public Function ObtieneUltimaFecha_EntregaCondones(ByVal nhc As String, ByVal usuario As String) As String
        _page = "db.ObtieneUltimaFecha_EntregaCondones"
        Dim Q As New StringBuilder()
        Dim Query As String = ""
        Dim Str As String = ""
        Q.Append("SELECT CC.NHC, (SELECT TOP(1)  convert(VARCHAR, C.Fecha_Entrega, 103) FROM ControlCONDONES AS C WHERE C.NHC = CC.NHC ORDER BY C.Fecha_Entrega desc) AS 'Ultima_Entrega' ")
        Q.Append("FROM ControlCONDONES AS CC ")
        Q.Append("WHERE CC.NHC = '" & nhc & "'")
        Query = Q.ToString()
        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim command As New SqlCommand(Query, connection)
                command.CommandTimeout = TimeoutDB
                Dim reader As SqlDataReader = command.ExecuteReader()
                If reader IsNot Nothing Then
                    While reader.Read()
                        Str = "True|" + reader("NHC").ToString() + "|" + reader("Ultima_Entrega").ToString()
                        Exit While
                    End While
                End If
                If Str = String.Empty Then
                    Str = "False|No existe última fecha."
                End If
                reader.Dispose()
                reader.Close()
                command.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page & "_" & nhc
            GrabarErrores(usuario & "|" & _page & "|" & ex.Number & "|" & ex.Message)
            Str = "False|" + ex.Message
        End Try
        Return Str
    End Function

    '*Graba Controles*//
    Public Sub GrabaUFechaControlARV(ByVal datos As String, ByVal usuario As String)
        _page = "db.GrabaUFechaControlARV"
        Dim d As String() = datos.Split("|")
        Dim sql As String = String.Format("UPDATE ControlARV SET Med1_DevCantidad = {1}, Med2_DevCantidad = {2}, Med3_DevCantidad = {3}, Med4_DevCantidad = {4}, Med5_DevCantidad = {5}, Med6_DevCantidad = {6}, Med7_DevCantidad = {7}, Med8_DevCantidad = {8}, Adherencia = {9}, TiempoRetorno = {10}, Id_auto_adherencia = {11}, FechaModificacion = GETDATE(), Id_Hospitalizado= {12}, Id_EnvioMedicamento= {13} WHERE IdCCARV = {0}", d(0).ToString(), d(1).ToString(), d(2).ToString(), d(3).ToString(), d(4).ToString(), d(5).ToString(), d(6).ToString(), d(7).ToString(), d(8).ToString(), d(9).ToString(), d(10).ToString(), d(11).ToString(), d(12).ToString(), d(13).ToString())
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim command As New SqlCommand(sql, connection)
                command.ExecuteNonQuery()
                command.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page
            GrabarErrores(usuario & "|" & _page & "|" & ex.Number & "|" & ex.Message)
        End Try
    End Sub

    Public Sub GrabaControlARV(ByVal datos As String, ByVal usuario As String)
        _page = "db.GrabaControlARV"
        Dim sql As String = ""
        Dim d As String() = datos.Split("|")
        'NHC, FechaEntrega, Med1_Codigo, Med1_Cantidad, Med1_Dosis, Med1_Frecuencia, Med1_UExCantidad, Med1_ARVEstatus, Med2_Codigo, Med2_Cantidad, Med2_Dosis, Med2_Frecuencia, Med2_UExCantidad, Med2_ARVEstatus, Med3_Codigo, Med3_Cantidad, Med3_Dosis, Med3_Frecuencia, Med3_UExCantidad, Med3_ARVEstatus, Med4_Codigo, Med4_Cantidad, Med4_Dosis, Med4_Frecuencia, Med4_UExCantidad, Med4_ARVEstatus, Med5_Codigo, Med5_Cantidad, Med5_Dosis, Med5_Frecuencia, Med5_UExCantidad, Med5_ARVEstatus, Med6_Codigo, Med6_Cantidad, Med6_Dosis, Med6_Frecuencia, Med6_UExCantidad, Med6_ARVEstatus, Med7_Codigo, Med7_Cantidad, Med7_Dosis, Med7_Frecuencia, Med7_UExCantidad, Med7_ARVEstatus, Med8_Codigo, Med8_Cantidad, Med8_Dosis, Med8_Frecuencia, Med8_UExCantidad, Med8_ARVEstatus, FechaRetorno, TiempoTARV, CitaMedica, CitaFarmacia, Embarazo, TiempoRetorno, CD4, CV, Observaciones, NomUsuario
        sql = "INSERT INTO ControlARV (NHC, FechaEntrega, IdEsquema, IdSEsquema, EsquemaEstatus, Med1_Codigo, Med1_Cantidad, Med1_Dosis, Med1_Frecuencia, Med1_UExCantidad, Med1_ARVEstatus, Med2_Codigo, Med2_Cantidad, Med2_Dosis, Med2_Frecuencia, Med2_UExCantidad, Med2_ARVEstatus, Med3_Codigo, Med3_Cantidad, Med3_Dosis, Med3_Frecuencia, Med3_UExCantidad, Med3_ARVEstatus, Med4_Codigo, Med4_Cantidad, Med4_Dosis, Med4_Frecuencia, Med4_UExCantidad, Med4_ARVEstatus, Med5_Codigo, Med5_Cantidad, Med5_Dosis, Med5_Frecuencia, Med5_UExCantidad, Med5_ARVEstatus, Med6_Codigo, Med6_Cantidad, Med6_Dosis, Med6_Frecuencia, Med6_UExCantidad, Med6_ARVEstatus, Med7_Codigo, Med7_Cantidad, Med7_Dosis, Med7_Frecuencia, Med7_UExCantidad, Med7_ARVEstatus, Med8_Codigo, Med8_Cantidad, Med8_Dosis, Med8_Frecuencia, Med8_UExCantidad, Med8_ARVEstatus, FechaRetorno, TiempoTARV, CitaMedica, CitaFarmacia, Embarazo, CD4, CV, Observaciones, NomUsuario) "
        sql += String.Format("VALUES('{0}', CONVERT(date,'{1}'), {2}, {3}, {4}, {5}, {6}, '{7}', {8}, {9}, {10}, {11}, {12}, '{13}', {14}, {15}, {16}, {17}, {18}, '{19}', {20}, {21}, {22}, {23}, {24}, '{25}', {26}, {27}, {28}, {29}, {30}, '{31}', {32}, {33}, {34}, {35}, {36}, '{37}', {38}, {39}, {40}, {41}, {42}, '{43}', {44}, {45}, {46}, {47}, {48}, '{49}', {50}, {51}, {52}, CONVERT(date,'{53}'), {54}, '{55}', '{56}', '{57}', '{58}', '{59}', '{60}', '{61}')", d(0).ToString(), d(1).ToString(), d(2).ToString(), d(3).ToString(), d(4).ToString(), d(5).ToString(), d(6).ToString(), d(7).ToString(), d(8).ToString(), d(9).ToString(), d(10).ToString(), d(11).ToString(), d(12).ToString(), d(13).ToString(), d(14).ToString(), d(15).ToString(), d(16).ToString(), d(17).ToString(), d(18).ToString(), d(19).ToString(), d(20).ToString(), d(21).ToString(), d(22).ToString(), d(23).ToString(), d(24).ToString(), d(25).ToString(), d(26).ToString(), d(27).ToString(), d(28).ToString(), d(29).ToString(), d(30).ToString(), d(31).ToString(), d(32).ToString(), d(33).ToString(), d(34).ToString(), d(35).ToString(), d(36).ToString(), d(37).ToString(), d(38).ToString(), d(39).ToString(), d(40).ToString(), d(41).ToString(), d(42).ToString(), d(43).ToString(), d(44).ToString(), d(45).ToString(), d(46).ToString(), d(47).ToString(), d(48).ToString(), d(49).ToString(), d(50).ToString(), d(51).ToString(), d(52).ToString(), d(53).ToString(), d(54).ToString(), d(55).ToString(), d(56).ToString(), d(57).ToString(), d(58).ToString(), d(59).ToString(), d(60).ToString(), usuario)
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim command As New SqlCommand(sql, connection)
                command.ExecuteNonQuery()
                command.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page
            GrabarErrores(usuario & "|" & _page & "|" & ex.Number & "|" & ex.Message)
        End Try
    End Sub

    Public Sub ActualizaControlARV(ByVal datos As String, ByVal usuario As String)
        _page = "db.ActualizaControlARV"
        Dim sql As String = ""
        Dim d As String() = datos.Split("|")
        sql = String.Format("UPDATE ControlARV SET Med1_Cantidad = {0}, Med1_Dosis = '{1}', Med1_Frecuencia = '{2}', Med1_UExCantidad = {3}, Med1_DevCantidad = {4}, ", d(1).ToString(), d(2).ToString(), d(3).ToString(), d(4).ToString(), d(5).ToString())
        sql += String.Format("Med2_Cantidad = {0}, Med2_Dosis = '{1}', Med2_Frecuencia = '{2}', Med2_UExCantidad = {3}, Med2_DevCantidad = {4}, ", d(6).ToString(), d(7).ToString(), d(8).ToString(), d(9).ToString(), d(10).ToString())
        sql += String.Format("Med3_Cantidad = {0}, Med3_Dosis = '{1}', Med3_Frecuencia = '{2}', Med3_UExCantidad = {3}, Med3_DevCantidad = {4}, ", d(11).ToString(), d(12).ToString(), d(13).ToString(), d(14).ToString(), d(15).ToString())
        sql += String.Format("Med4_Cantidad = {0}, Med4_Dosis = '{1}', Med4_Frecuencia = '{2}', Med4_UExCantidad = {3}, Med4_DevCantidad = {4}, ", d(16).ToString(), d(17).ToString(), d(18).ToString(), d(19).ToString(), d(20).ToString())
        sql += String.Format("Med5_Cantidad = {0}, Med5_Dosis = '{1}', Med5_Frecuencia = '{2}', Med5_UExCantidad = {3}, Med5_DevCantidad = {4}, ", d(21).ToString(), d(22).ToString(), d(23).ToString(), d(24).ToString(), d(25).ToString())
        sql += String.Format("Med6_Cantidad = {0}, Med6_Dosis = '{1}', Med6_Frecuencia = '{2}', Med6_UExCantidad = {3}, Med6_DevCantidad = {4}, ", d(26).ToString(), d(27).ToString(), d(28).ToString(), d(29).ToString(), d(30).ToString())
        sql += String.Format("Med7_Cantidad = {0}, Med7_Dosis = '{1}', Med7_Frecuencia = '{2}', Med7_UExCantidad = {3}, Med7_DevCantidad = {4}, ", d(31).ToString(), d(32).ToString(), d(33).ToString(), d(34).ToString(), d(35).ToString())
        sql += String.Format("Med8_Cantidad = {0}, Med8_Dosis = '{1}', Med8_Frecuencia = '{2}', Med8_UExCantidad = {3}, Med8_DevCantidad = {4}, ", d(36).ToString(), d(37).ToString(), d(38).ToString(), d(39).ToString(), d(40).ToString())
        sql += String.Format("FechaRetorno = '{0}', TiempoTARV = {1}, CitaMedica = '{2}', CitaFarmacia = '{3}', Embarazo = '{4}', ", d(41).ToString(), d(42).ToString(), d(43).ToString(), d(44).ToString(), d(45).ToString())
        sql += String.Format("TiempoRetorno = {0}, Adherencia = {1}, CD4 = '{2}', CV = '{3}', Observaciones = '{4}', Id_auto_adherencia = {5} ", d(46).ToString(), d(47).ToString(), d(48).ToString(), d(49).ToString(), d(50).ToString(), d(51).ToString())
        sql += String.Format("WHERE IdCCARV = {0}", d(0).ToString())
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim command As New SqlCommand(sql, connection)
                command.ExecuteNonQuery()
                command.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page
            GrabarErrores(usuario & "|" & _page & "|" & ex.Number & "|" & ex.Message)
        End Try
    End Sub

    Public Sub ActualizaControlProf(ByVal datos As String, ByVal usuario As String)
        _page = "db.ActualizaControlProf"
        Dim sql As String = ""
        Dim d As String() = datos.Split("|")
        sql = String.Format("UPDATE ControlProf SET TipoPaciente = {0}, ", d(2).ToString())
        sql += String.Format("Prof1_Cantidad = {0}, Prof1_Dosis = '{1}', Prof1_VIA = '{2}', Prof1_Frecuencia = '{3}', Prof1_Tipo = {4}, Prof1_TipoTratamiento = {5}, Prof1_Estatus = {6}, Prof1_TTMed = {7}, Prof1_Observaciones = '{8}', ", d(3).ToString(), d(4).ToString(), d(5).ToString(), d(6).ToString(), d(7).ToString(), d(8).ToString(), d(9).ToString(), d(10).ToString(), d(11).ToString())
        sql += String.Format("Prof2_Cantidad = {0}, Prof2_Dosis = '{1}', Prof2_VIA = '{2}', Prof2_Frecuencia = '{3}', Prof2_Tipo = {4}, Prof2_TipoTratamiento = {5}, Prof2_Estatus = {6}, Prof2_TTMed = {7}, Prof2_Observaciones = '{8}', ", d(12).ToString(), d(13).ToString(), d(14).ToString(), d(15).ToString(), d(16).ToString(), d(17).ToString(), d(18).ToString(), d(19).ToString(), d(20).ToString())
        sql += String.Format("Prof3_Cantidad = {0}, Prof3_Dosis = '{1}', Prof3_VIA = '{2}', Prof3_Frecuencia = '{3}', Prof3_Tipo = {4}, Prof3_TipoTratamiento = {5}, Prof3_Estatus = {6}, Prof3_TTMed = {7}, Prof3_Observaciones = '{8}', ", d(21).ToString(), d(22).ToString(), d(23).ToString(), d(24).ToString(), d(25).ToString(), d(26).ToString(), d(27).ToString(), d(28).ToString(), d(29).ToString())
        sql += String.Format("Prof4_Cantidad = {0}, Prof4_Dosis = '{1}', Prof4_VIA = '{2}', Prof4_Frecuencia = '{3}', Prof4_Tipo = {4}, Prof4_TipoTratamiento = {5}, Prof4_Estatus = {6}, Prof4_TTMed = {7}, Prof4_Observaciones = '{8}', ", d(30).ToString(), d(31).ToString(), d(32).ToString(), d(33).ToString(), d(34).ToString(), d(35).ToString(), d(36).ToString(), d(37).ToString(), d(38).ToString())
        sql += String.Format("Prof5_Cantidad = {0}, Prof5_Dosis = '{1}', Prof5_VIA = '{2}', Prof5_Frecuencia = '{3}', Prof5_Tipo = {4}, Prof5_TipoTratamiento = {5}, Prof5_Estatus = {6}, Prof5_TTMed = {7}, Prof5_Observaciones = '{8}', ", d(39).ToString(), d(40).ToString(), d(41).ToString(), d(42).ToString(), d(43).ToString(), d(44).ToString(), d(45).ToString(), d(46).ToString(), d(47).ToString())
        sql += String.Format("Prof6_Cantidad = {0}, Prof6_Dosis = '{1}', Prof6_VIA = '{2}', Prof6_Frecuencia = '{3}', Prof6_Tipo = {4}, Prof6_TipoTratamiento = {5}, Prof6_Estatus = {6}, Prof6_TTMed = {7}, Prof6_Observaciones = '{8}', ", d(48).ToString(), d(49).ToString(), d(50).ToString(), d(51).ToString(), d(52).ToString(), d(53).ToString(), d(54).ToString(), d(55).ToString(), d(56).ToString())
        sql += String.Format("CD4 = '{0}', NomUsuario = '{1}', FechaModificacion = GETDATE() ", d(1).ToString(), d(57).ToString())
        sql += String.Format("WHERE IdCCPROF = {0}", d(0).ToString())
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim command As New SqlCommand(sql, connection)
                command.ExecuteNonQuery()
                command.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page
            GrabarErrores(usuario & "|" & _page & "|" & ex.Number & "|" & ex.Message)
        End Try
    End Sub

    Public Sub GrabaUFechaControlPROF(ByVal datos As String, ByVal usuario As String)
        _page = "db.GrabaUFechaControlPROF"
        Dim d As String() = datos.Split("|")
        Dim sql As String = String.Format("UPDATE ControlPROF SET Prof1_DevCantidad = {1}, Prof2_DevCantidad = {2}, Prof3_DevCantidad = {3}, Prof4_DevCantidad = {4}, FechaModificacion = GETDATE() WHERE IdCCProf = {0}", d(0).ToString(), d(1).ToString(), d(2).ToString(), d(3).ToString(), d(4).ToString())
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim command As New SqlCommand(sql, connection)
                command.ExecuteNonQuery()
                command.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page
            GrabarErrores(usuario & "|" & _page & "|" & ex.Number & "|" & ex.Message)
        End Try
    End Sub

    Public Sub GrabaControlPROF(ByVal datos As String, ByVal usuario As String)
        _page = "db.GrabaControlPROF"
        Dim sql As String = ""
        Dim d As String() = datos.Split("|")
        'NHC, FechaEntrega, Prof1_Codigo, Prof1_Cantidad, Prof1_Dosis, Prof1_Frecuencia, Prof1_MedEstatus, Prof2_Codigo, Prof2_Cantidad, Prof2_Dosis, Prof2_Frecuencia, Prof2_MedEstatus, Prof3_Codigo, Prof3_Cantidad, Prof3_Dosis, Prof3_Frecuencia, Prof3_MedEstatus, Prof4_Codigo, Prof4_Cantidad, Prof4_Dosis, Prof4_Frecuencia, Prof4_MedEstatus, FechaRetorno, TiempoTMed, CitaMedica, CitaFarmacia, Embarazo, TiempoRetorno, CD4, CV, Observaciones
        sql = "INSERT INTO ControlPROF (NHC, FechaEntrega, TipoPaciente, "
        sql += "Prof1_Codigo, Prof1_Cantidad, Prof1_Dosis, Prof1_VIA, Prof1_Frecuencia, Prof1_Tipo, Prof1_TipoTratamiento, Prof1_Estatus, Prof1_TTMed, Prof1_Observaciones, "
        sql += "Prof2_Codigo, Prof2_Cantidad, Prof2_Dosis, Prof2_VIA, Prof2_Frecuencia, Prof2_Tipo, Prof2_TipoTratamiento, Prof2_Estatus, Prof2_TTMed, Prof2_Observaciones, "
        sql += "Prof3_Codigo, Prof3_Cantidad, Prof3_Dosis, Prof3_VIA, Prof3_Frecuencia, Prof3_Tipo, Prof3_TipoTratamiento, Prof3_Estatus, Prof3_TTMed, Prof3_Observaciones, "
        sql += "Prof4_Codigo, Prof4_Cantidad, Prof4_Dosis, Prof4_VIA, Prof4_Frecuencia, Prof4_Tipo, Prof4_TipoTratamiento, Prof4_Estatus, Prof4_TTMed, Prof4_Observaciones, "
        sql += "Prof5_Codigo, Prof5_Cantidad, Prof5_Dosis, Prof5_VIA, Prof5_Frecuencia, Prof5_Tipo, Prof5_TipoTratamiento, Prof5_Estatus, Prof5_TTMed, Prof5_Observaciones, "
        sql += "Prof6_Codigo, Prof6_Cantidad, Prof6_Dosis, Prof6_VIA, Prof6_Frecuencia, Prof6_Tipo, Prof6_TipoTratamiento, Prof6_Estatus, Prof6_TTMed, Prof6_Observaciones, "
        sql += "CD4, NomUsuario)"
        sql += String.Format("VALUES ('{0}', CONVERT(date,'{1}'), {2}, {3}, {4}, '{5}', {6}, {7}, {8}, {9}, {10}, {11}, '{12}', {13}, {14}, '{15}', {16}, {17}, {18}, {19}, {20}, {21}, '{22}', {23}, {24}, '{25}', {26}, {27}, {28}, {29}, {30}, {31}, '{32}', {33}, {34}, '{35}', {36}, {37}, {38}, {39}, {40}, {41}, '{42}', {43}, {44}, '{45}', {46}, {47}, {48}, {49}, {50}, {51}, '{52}', {53}, {54}, '{55}', {56}, {57}, {58}, {59}, {60}, {61}, '{62}', '{63}', '{64}')", d(0).ToString(), d(1).ToString(), d(2).ToString(), d(3).ToString(), d(4).ToString(), d(5).ToString(), d(6).ToString(), d(7).ToString(), d(8).ToString(), d(9).ToString(), d(10).ToString(), d(11).ToString(), d(12).ToString(), d(13).ToString(), d(14).ToString(), d(15).ToString(), d(16).ToString(), d(17).ToString(), d(18).ToString(), d(19).ToString(), d(20).ToString(), d(21).ToString(), d(22).ToString(), d(23).ToString(), d(24).ToString(), d(25).ToString(), d(26).ToString(), d(27).ToString(), d(28).ToString(), d(29).ToString(), d(30).ToString(), d(31).ToString(), d(32).ToString(), d(33).ToString(), d(34).ToString(), d(35).ToString(), d(36).ToString(), d(37).ToString(), d(38).ToString(), d(39).ToString(), d(40).ToString(), d(41).ToString(), d(42).ToString(), d(43).ToString(), d(44).ToString(), d(45).ToString(), d(46).ToString(), d(47).ToString(), d(48).ToString(), d(49).ToString(), d(50).ToString(), d(51).ToString(), d(52).ToString(), d(53).ToString(), d(54).ToString(), d(55).ToString(), d(56).ToString(), d(57).ToString(), d(58).ToString(), d(59).ToString(), d(60).ToString(), d(61).ToString(), d(62).ToString(), d(63).ToString(), usuario)
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim command As New SqlCommand(sql, connection)
                command.ExecuteNonQuery()
                command.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page
            GrabarErrores(usuario & "|" & _page & "|" & ex.Number & "|" & ex.Message)
        End Try
    End Sub

    Public Sub GrabaControlCONDONES(ByVal datos As String, ByVal usuario As String)
        _page = "db.GrabaControlCONDONES"
        Dim sql As String = ""
        Dim d As String() = datos.Split("|")
        'NHC, FechaEntrega, Prof1_Codigo, Prof1_Cantidad, Prof1_Dosis, Prof1_Frecuencia, Prof1_MedEstatus, Prof2_Codigo, Prof2_Cantidad, Prof2_Dosis, Prof2_Frecuencia, Prof2_MedEstatus, Prof3_Codigo, Prof3_Cantidad, Prof3_Dosis, Prof3_Frecuencia, Prof3_MedEstatus, Prof4_Codigo, Prof4_Cantidad, Prof4_Dosis, Prof4_Frecuencia, Prof4_MedEstatus, FechaRetorno, TiempoTMed, CitaMedica, CitaFarmacia, Embarazo, TiempoRetorno, CD4, CV, Observaciones
        sql = "INSERT INTO ControlCONDONES (NHC, Fecha_Entrega, "
        sql += "Codigo_1, Cantidad_1, Codigo_2, Cantidad_2, Observaciones, NomUsuario )"
        sql += String.Format("VALUES ('{0}', CONVERT(date,'{1}'), {2}, {3}, {4}, {5}, '{6}', '{7}')", d(0).ToString(), d(1).ToString(), d(2).ToString(), d(3).ToString(), d(4).ToString(), d(5).ToString(), d(6).ToString(), usuario)
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim command As New SqlCommand(sql, connection)
                command.ExecuteNonQuery()
                command.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page
            GrabarErrores(usuario & "|" & _page & "|" & ex.Number & "|" & ex.Message)
        End Try
    End Sub
    '*Esquemas y SubEsquemas*//
    Public Sub GrabaEsquema(ByVal descripcion As String, ByVal codigos As String, ByVal usuario As String)
        _page = "db.GrabaEsquema"
        Dim sql As String = ""
        sql = "INSERT INTO Esquemas (Descripcion, Codigos) "
        sql += String.Format("VALUES('{0}', '{1}')", descripcion, codigos)
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim command As New SqlCommand(sql, connection)
                command.ExecuteNonQuery()
                command.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page
            GrabarErrores(usuario & "|" & _page & "|" & ex.Number & "|" & ex.Message)
        End Try
    End Sub

    Public Sub GrabaSEsquemas(ByVal IdEsquema As String, ByVal Descripcion As String, ByVal Codigos As String, ByVal usuario As String)
        _page = "db.GrabaSEsquemas"
        Dim sql As String = ""
        sql = "INSERT INTO SubEsquemas (IdEsquema, Descripcion, Codigos, SCodigo) "
        sql += String.Format("VALUES({0}, '{1}', '{2}', (SELECT RIGHT('000' + CAST({0} AS VARCHAR(3)),3) + '-' + RIGHT('00' + CAST(COUNT(IdEsquema) + 1 AS VARCHAR(2)),2) FROM SubEsquemas WHERE IdEsquema = {0}))", IdEsquema, Descripcion, Codigos)
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim command As New SqlCommand(sql, connection)
                command.ExecuteNonQuery()
                command.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page
            GrabarErrores(usuario & "|" & _page & "|" & ex.Number & "|" & ex.Message)
        End Try
    End Sub

    '*Graba FFProf-FFARV*//
    Public Sub GrabaFFARV(ByVal datos As String, ByVal usuario As String)
        _page = "db.GrabaFFARV"
        Dim sql As String = ""
        Dim d As String() = datos.Split("|")
        sql = "INSERT INTO FFARV (IdARV, IdFF, Concentracion, Codigo) "
        'sql += String.Format("VALUES({0}, {1}, '{2}', (SELECT (CASE WHEN LEN({0}) = 1 THEN ('0' + CONVERT(VARCHAR, {0})) ELSE CONVERT(VARCHAR, {0}) END) +'-'+ (CASE WHEN LEN(COUNT(Codigo)+1) = 1 THEN ('0' + CONVERT(VARCHAR, COUNT(Codigo)+1)) ELSE CONVERT(VARCHAR, COUNT(Codigo)+1) END) FROM FFARV WHERE IdARV = {0}))", d(0).ToString(), d(1).ToString(), d(2).ToString())
        sql += String.Format("VALUES({0}, {1}, '{2}', (SELECT RIGHT('00' + CAST({0} AS VARCHAR(2)),2) + '-' + RIGHT('00' + CAST(COUNT(Codigo) + 1 AS VARCHAR(2)),2) FROM FFARV WHERE IdARV = {0}))", d(0).ToString(), d(1).ToString(), d(2).ToString())
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim command As New SqlCommand(sql, connection)
                command.ExecuteNonQuery()
                command.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page
            GrabarErrores(usuario & "|" & _page & "|" & ex.Number & "|" & ex.Message)
        End Try
    End Sub

    Public Sub ActualizaFFARV(ByVal id As String, ByVal datos As String, ByVal usuario As String)
        _page = "db.ActualizaFFARV"
        'Dim d As String() = datos.Split("|")
        Dim sql As String = String.Format("UPDATE FFARV SET Concentracion = '{1}', FechaModificacion = GETDATE() WHERE IdFFARV = '{0}'", id, datos.ToString())
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim command As New SqlCommand(sql, connection)
                command.ExecuteNonQuery()
                command.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page + "_" + id
            GrabarErrores(usuario & "|" & _pageO & "|" & ex.Number & "|" & ex.Message)
        End Try
    End Sub

    Public Sub GrabaFFProf(ByVal datos As String, ByVal usuario As String)
        _page = "db.GrabaFFProf"
        Dim sql As String = ""
        Dim d As String() = datos.Split("|")
        sql = "INSERT INTO FFProf (IdProf, IdFF, Concentracion, Codigo) "
        'sql += String.Format("VALUES({0}, {1}, '{2}', (SELECT (CASE WHEN LEN({0}) = 1 THEN ('0' + CONVERT(VARCHAR, {0})) ELSE CONVERT(VARCHAR, {0}) END) +'-'+ (CASE WHEN LEN(COUNT(Codigo)+1) = 1 THEN ('0' + CONVERT(VARCHAR, COUNT(Codigo)+1)) ELSE CONVERT(VARCHAR, COUNT(Codigo)+1) END) FROM FFProf WHERE IdProf = {0}))", d(0).ToString(), d(1).ToString(), d(2).ToString())
        sql += String.Format("VALUES({0}, {1}, '{2}', (SELECT RIGHT('000' + CAST({0} AS VARCHAR(3)),3) + '-' + RIGHT('00' + CAST(COUNT(Codigo) + 1 AS VARCHAR(2)),2) FROM FFProf WHERE IdProf = {0}))", d(0).ToString(), d(1).ToString(), d(2).ToString())
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim command As New SqlCommand(sql, connection)
                command.ExecuteNonQuery()
                command.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page
            GrabarErrores(usuario & "|" & _page & "|" & ex.Number & "|" & ex.Message)
        End Try
    End Sub

    Public Sub ActualizaFFProf(ByVal id As String, ByVal datos As String, ByVal usuario As String)
        _page = "db.ActualizaFFProf"
        'Dim d As String() = datos.Split("|")
        Dim sql As String = String.Format("UPDATE FFProf SET Concentracion = '{1}', FechaModificacion = GETDATE() WHERE IdFFProf = '{0}'", id, datos.ToString())
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim command As New SqlCommand(sql, connection)
                command.ExecuteNonQuery()
                command.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page + "_" + id
            GrabarErrores(usuario & "|" & _pageO & "|" & ex.Number & "|" & ex.Message)
        End Try
    End Sub

    '*Graba LProf-LARV*//
    Public Sub GrabaLARV(ByVal nombre As String, ByVal ncorto As String, ByVal usuario As String)
        _page = "db.GrabaLARV"
        Dim sql As String = String.Format("INSERT INTO MedARV (NomARV, NomCorto) VALUES('{0}', '{1}')", nombre, ncorto)
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim command As New SqlCommand(sql, connection)
                command.ExecuteNonQuery()
                command.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page
            GrabarErrores(usuario & "|" & _page & "|" & ex.Number & "|" & ex.Message)
        End Try
    End Sub

    Public Sub ActualizaLARV(ByVal id As String, ByVal nombre As String, ByVal ncorto As String, ByVal usuario As String)
        _page = "db.ActualizaLARV"
        Dim sql As String = String.Format("UPDATE MedARV SET NomARV = '{1}', NomCorto = '{2}', FechaModificacion = GETDATE() WHERE IdARV = '{0}'", id, nombre.ToString(), ncorto.ToString())
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim command As New SqlCommand(sql, connection)
                command.ExecuteNonQuery()
                command.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page + "_" + id
            GrabarErrores(usuario & "|" & _pageO & "|" & ex.Number & "|" & ex.Message)
        End Try
    End Sub

    Public Sub GrabaLProf(ByVal datos As String, ByVal usuario As String)
        _page = "db.GrabaLProf"
        Dim sql As String = String.Format("INSERT INTO MedProf (NomProfilaxis) VALUES('{0}')", datos)
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim command As New SqlCommand(sql, connection)
                command.ExecuteNonQuery()
                command.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page
            GrabarErrores(usuario & "|" & _page & "|" & ex.Number & "|" & ex.Message)
        End Try
    End Sub

    Public Sub ActualizaLProf(ByVal id As String, ByVal datos As String, ByVal usuario As String)
        _page = "db.ActualizaLProf"
        Dim sql As String = String.Format("UPDATE MedProf SET NomProfilaxis = '{1}', FechaModificacion = GETDATE() WHERE IdProf = '{0}'", id, datos.ToString())
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim command As New SqlCommand(sql, connection)
                command.ExecuteNonQuery()
                command.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page + "_" + id
            GrabarErrores(usuario & "|" & _pageO & "|" & ex.Number & "|" & ex.Message)
        End Try
    End Sub

    '*Actualiza Basal Pediatrico*//
    Public Sub ActualizaBPediatrico(ByVal nhc As String, ByVal datos As String, ByVal usuario As String)
        _page = "db.ActualizaBPediatrico"
        Dim d As String() = datos.Split("|")
        Dim sql As String = String.Format("UPDATE BasalesPediatria SET PrimerNombre = '{1}', SegundoNombre = '{2}', PrimerApellido = '{3}', SegundoApellido = '{4}', Genero = {5}, FechaNacimiento = '{6}', Telefono = '{7}', Direccion = '{8}', IdBaja = {9} WHERE NHC = '{0}'", d(0).ToString(), d(1).ToString(), d(2).ToString(), d(3).ToString(), d(4).ToString(), d(5).ToString(), d(6).ToString(), d(7).ToString(), d(8).ToString(), d(9).ToString())
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim command As New SqlCommand(sql, connection)
                command.ExecuteNonQuery()
                command.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page + "_" + nhc
            GrabarErrores(usuario & "|" & _pageO & "|" & ex.Number & "|" & ex.Message)
        End Try
    End Sub

    '*Agrega Basal Pediatrico*//
    Public Function AgregaBPediatrico(ByVal nhc As String, ByVal datos As String, ByVal usuario As String) As String
        _page = "db.AgregaBPediatrico"
        Dim sql As String
        Dim str As String
        Dim x As Boolean = False
        sql = "SELECT NHC FROM BasalesPediatria WHERE NHC = '" & nhc & "'"
        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim command As New SqlCommand(sql, connection)
                command.CommandTimeout = TimeoutDB
                Dim reader As SqlDataReader = command.ExecuteReader()
                'If reader IsNot Nothing Then
                If reader.HasRows Then
                    x = True
                Else
                    x = False
                End If
                reader.Dispose()
                reader.Close()
                command.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page & "_" & nhc
            GrabarErrores(usuario & "|" & _pageO & "|" & ex.Number & "|" & ex.Message)
            str = "False|" + ex.Message
        End Try
        If x Then
            str = "False|El NHC pediatrico ya existe, revise porfavor."
        Else
            Dim d As String() = datos.Split("|")
            sql = String.Format("INSERT INTO BasalesPediatria (NHC, PrimerNombre, SegundoNombre, PrimerApellido, SegundoApellido, Genero, FechaNacimiento, Telefono, Direccion, IdBaja) VALUES('{0}', '{1}', '{2}', '{3}', '{4}', {5}, '{6}', '{7}', '{8}', {9})", d(0).ToString(), d(1).ToString(), d(2).ToString(), d(3).ToString(), d(4).ToString(), d(5).ToString(), d(6).ToString(), d(7).ToString(), d(8).ToString(), d(9).ToString())
            Try
                Using connection As New SqlConnection(_cn1)
                    connection.Open()
                    Dim command As New SqlCommand(sql, connection)
                    command.ExecuteNonQuery()
                    command.Dispose()
                    connection.Dispose()
                    connection.Close()
                End Using
                str = "True|Ok"
            Catch ex As SqlException
                _error = ex.Message
                _pageO = _page + "_" + nhc
                GrabarErrores(usuario & "|" & _pageO & "|" & ex.Number & "|" & ex.Message)
                str = "False|" + ex.Message
            End Try
        End If
        Return str
    End Function

    '*Graba Errores*//
    Public Sub GrabarErrores(ByVal Errores As String)
        Try
            Dim r As String() = Errores.Split("|")
            Dim r3 As String = r(3).Replace("'", "").ToString()
            Using connection As New SqlConnection(_cn1)
                Dim sql As String = ""
                sql = String.Format("insert into logEBD(IdUsuario,Pagina,Error,ErrorMsg) values('{0}', '{1}', '{2}', '{3}')", r(0), r(1), r(2), r3)
                connection.Open()
                Dim command As New SqlCommand(sql, connection)
                command.ExecuteNonQuery()
                command.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
        Catch ex As SqlException
            Dim r As String() = Errores.Split("|")
            Dim narchivo As String = "FARMACIA_EBD"
            Dim path As String = ConfigurationManager.AppSettings("LogEBD")
            Dim archivo As String = (path & narchivo) & System.DateTime.Now.Year.ToString() & System.DateTime.Now.Month.ToString("00") & System.DateTime.Now.Day.ToString("00") & ".txt"
            Dim sw As New StreamWriter(archivo, True)
            sw.WriteLine(System.DateTime.Now.ToLongTimeString() & " - " & r(0) & " - Página: " & r(1))
            sw.WriteLine("Error No.: " & r(2))
            sw.WriteLine("Error Mensaje: " & r(3))
            sw.WriteLine("---------------------------------------------------")
            sw.WriteLine("Error Base de Datos al Grabar")
            sw.WriteLine("Error: " & ex.Number)
            sw.WriteLine("Error Mensaje: " & ex.Message)
            sw.WriteLine("---------------------------------------------------")
            sw.Close()
        End Try
    End Sub

    Public Function Auto_adh() As DataTable
        _page = "db.Auto_adh"
        Dim Query As String = ""
        'Auto Adherencia
        Query = "SELECT Id_auto_adherencia, Nom_autoadherencia FROM auto_adherencia ORDER BY Id_auto_adherencia ASC "
        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim adapter As New SqlDataAdapter()
                adapter.SelectCommand = New SqlCommand(Query, connection)
                adapter.SelectCommand.CommandTimeout = TimeoutDB
                adapter.Fill(Ds, _page)
                adapter.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
            Return Ds.Tables(0)
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page
            GrabarErrores(_page & "|" & ex.Number & "|" & ex.Message)
            Return Nothing
        End Try
    End Function

    Public Function Obtiene_ingresos_med_ARV(ByVal fecha_mes_min As String, ByVal fecha_mes_max As String, ByVal fecha_anio As String, ByVal usuario As String) As DataTable
        _page = "db.Obtiene_ingresos_med_ARV"
        Dim Query As String
        Query = "SELECT IM.IdIngresoMed, convert(VARCHAR,IM.FechaIngreso,103) AS 'FechaIngreso', FA.Codigo, "
        Query += "LTRIM(RTRIM(MA.NomARV)) + '/' + LTRIM(RTRIM(FF.NomFF)) +  '/'+ LTRIM(RTRIM(FA.Concentracion)) AS 'Medicamento', IM.Cantidad_Ingreso, IM.No_Requisicion, IM.No_Lote, convert(VARCHAR, IM.Fecha_Vencimiento, 103) AS 'Fecha_Vencimiento' "
        Query += String.Format("FROM dbo.Ingreso_Med AS IM LEFT OUTER JOIN FFARV AS FA ON FA.IdFFARV = IM.IdFF LEFT OUTER JOIN MedARV AS MA ON MA.IdARV = FA.IdARV LEFT OUTER JOIN FormaFarmaceutica AS FF ON FF.IdFF = FA.IdFF WHERE  DATEPART(month,FechaIngreso) >='{0}' AND datepart(month,FechaIngreso) <= '{1}' AND datepart(year,FechaIngreso) = '{2}' AND TipoIngresoMed = 1 ORDER BY FechaIngreso ASC", fecha_mes_min, fecha_mes_max, fecha_anio)
        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim adapter As New SqlDataAdapter()
                adapter.SelectCommand = New SqlCommand(Query, connection)
                adapter.SelectCommand.CommandTimeout = TimeoutDB
                adapter.Fill(Ds, _page)
                adapter.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
            Return Ds.Tables(0)
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page
            GrabarErrores(usuario & "|" & _pageO & "|" & ex.Number & "|" & ex.Message)
            Return Nothing
        End Try
    End Function

    Public Function Obtiene_ingresos_med_PROF(ByVal fecha_mes_min As String, ByVal fecha_mes_max As String, ByVal fecha_anio As String, ByVal usuario As String) As DataTable
        _page = "db.Obtiene_ingresos_med_PROF"
        Dim Query As String
        Query = "SELECT IM.IdIngresoMed, convert(VARCHAR,IM.FechaIngreso,103) AS 'FechaIngreso', FP.Codigo, "
        Query += "LTRIM(RTRIM(MP.NomProfilaxis)) + '/' + LTRIM(RTRIM(FF.NomFF)) +  '/'+ LTRIM(RTRIM(FP.Concentracion)) AS 'Medicamento', IM.Cantidad_Ingreso, IM.No_Requisicion, IM.No_Lote, convert(VARCHAR, IM.Fecha_Vencimiento, 103) AS 'Fecha_Vencimiento' "
        Query += String.Format("FROM dbo.Ingreso_Med AS IM LEFT OUTER JOIN FFProf AS FP ON FP.IdFFProf  = IM.IdFF LEFT OUTER JOIN MedProf AS MP ON MP.IdProf = FP.IdProf LEFT OUTER JOIN  FormaFarmaceutica AS FF ON FF.IdFF = FP.IdFF WHERE  DATEPART(month,FechaIngreso) >='{0}' AND datepart(month,FechaIngreso) <= '{1}' AND datepart(year,FechaIngreso) = '{2}' AND  TipoIngresoMed = 2 ORDER BY FechaIngreso ASC", fecha_mes_min, fecha_mes_max, fecha_anio)
        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim adapter As New SqlDataAdapter()
                adapter.SelectCommand = New SqlCommand(Query, connection)
                adapter.SelectCommand.CommandTimeout = TimeoutDB
                adapter.Fill(Ds, _page)
                adapter.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
            Return Ds.Tables(0)
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page
            GrabarErrores(usuario & "|" & _pageO & "|" & ex.Number & "|" & ex.Message)
            Return Nothing
        End Try
    End Function
    '*/ OBTIENE EGRESOS POR DÍA 
    Public Function Obtiene_egresos_med(ByVal fecha_inicio_m As String, ByVal fecha_fin_m As String, ByVal fecha_anio As String, ByVal tipoM As String, ByVal usuario As String) As DataTable
        _page = "db.Obtiene_egresos_med"
        Dim Query As String
        Select Case tipoM
            Case "1"
                Query = "SELECT O.Id_Otros_EgresosMed, convert(VARCHAR, O.Fecha_Egreso,103) AS 'Fecha_Egreso', O.Tipo_Medicamento, O.IdFF, LTRIM(RTRIM(AR.NomARV)) + '/' + LTRIM(RTRIM(F.NomFF)) + '/' + LTRIM(RTRIM(a.Concentracion)) AS 'Medicamento', O.Cantidad, TE.Nom_TipoEgreso "
                Query += "FROM dbo.Otros_EgresosMed AS O LEFT OUTER JOIN FFARV AS A  ON A.IdFFARV= O.IdFF LEFT OUTER JOIN MedARV AS AR ON AR.IdARV= A.IdARV LEFT OUTER JOIN FormaFarmaceutica AS F ON F.IdFF= A.IdFF LEFT OUTER JOIN Tipo_Egreso_Med AS TE ON TE.Id_TipoEgreso = O.Tipo_Egreso "
                Query += String.Format("WHERE DATEPART(month,O.Fecha_Egreso ) >= '{0}' AND datepart(month,O.Fecha_Egreso ) <= '{1}' AND datepart(year,O.Fecha_Egreso ) = '{2}' AND  O.Tipo_Medicamento = 1 ORDER BY O.Fecha_Egreso ASC", fecha_inicio_m, fecha_fin_m, fecha_anio)

            Case "2"
                Query = "SELECT O.Id_Otros_EgresosMed, convert(VARCHAR, O.Fecha_Egreso,103) AS 'Fecha_Egreso', O.Tipo_Medicamento, O.IdFF, LTRIM(RTRIM(PR.NomProfilaxis)) + '/' + LTRIM(RTRIM(F.NomFF)) + '/' + LTRIM(RTRIM(P.Concentracion)) AS 'Medicamento', O.Cantidad, TE.Nom_TipoEgreso "
                Query += "FROM dbo.Otros_EgresosMed AS O LEFT OUTER JOIN FFProf AS P  ON P.IdFFProf = O.IdFF LEFT OUTER JOIN MedProf AS PR ON PR.IdProf = P.IdProf LEFT OUTER JOIN  FormaFarmaceutica AS F ON F.IdFF= P.IdFF LEFT OUTER JOIN Tipo_Egreso_Med AS TE ON TE.Id_TipoEgreso = O.Tipo_Egreso "
                Query += String.Format("WHERE DATEPART(month,O.Fecha_Egreso ) >= '{0}' AND datepart(month,O.Fecha_Egreso ) <= '{1}' AND datepart(year,O.Fecha_Egreso ) = '{2}' AND  O.Tipo_Medicamento = 2 ORDER BY O.Fecha_Egreso ASC", fecha_inicio_m, fecha_fin_m, fecha_anio)

        End Select


        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim adapter As New SqlDataAdapter()
                adapter.SelectCommand = New SqlCommand(Query, connection)
                adapter.SelectCommand.CommandTimeout = TimeoutDB
                adapter.Fill(Ds, _page)
                adapter.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
            Return Ds.Tables(0)
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page
            GrabarErrores(usuario & "|" & _pageO & "|" & ex.Number & "|" & ex.Message)
            Return Nothing
        End Try
    End Function
    '*/ Obeiene Registro de ingreso ARV para actualizar existencias al eliminar ingreso
    Public Function Obtiene_ingresos_med_ARV_Update(ByVal idingreso As String, ByVal usuario As String) As String
        _page = "db.Obtiene_ingresos_med_ARV_Update"
        Dim str As String = ""
        Dim Query As String = String.Format("SELECT IM.IdIngresoMed, convert(VARCHAR,IM.FechaIngreso,103) AS 'FechaIngreso', FA.IdFFARV, FA.Codigo, LTRIM(RTRIM(MA.NomARV)) + '/' + LTRIM(RTRIM(FF.NomFF)) +  '/'+ LTRIM(RTRIM(FA.Concentracion)) AS 'Medicamento', IM.Cantidad_Ingreso FROM dbo.Ingreso_Med AS IM LEFT OUTER JOIN FFARV AS FA ON FA.IdFFARV = IM.IdFF LEFT OUTER JOIN MedARV AS MA ON MA.IdARV = FA.IdARV LEFT OUTER JOIN FormaFarmaceutica AS FF ON FF.IdFF = FA.IdFF WHERE IdIngresoMed = '{0}' AND TipoIngresoMed = 1 ORDER BY FechaIngreso ASC", idingreso)
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim command As New SqlCommand(Query, connection)
                command.CommandTimeout = TimeoutDB
                Dim reader As SqlDataReader = command.ExecuteReader()
                If reader IsNot Nothing Then
                    While reader.Read()
                        str = "True|" & Convert.ToString(reader("IdIngresoMed")) & "|" & Convert.ToString(reader("Codigo")) & "|" & Convert.ToString(reader("Cantidad_Ingreso")) & "|" & Convert.ToString(reader("IdFFARV"))
                        Exit While
                    End While
                Else
                    str = "False|No se Encontró Información."
                End If
                reader.Dispose()
                reader.Close()
                command.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page & "_" & idingreso
            GrabarErrores(usuario & "|" & _page & "|" & ex.Number & "|" & ex.Message)
            str = "False|" & ex.Message
        End Try
        Return str
    End Function
    '*/ Obeiene Registro de ingreso PROF para actualizar existencias al eliminar ingreso
    Public Function Obtiene_ingresos_med_PROF_Update(ByVal idingreso As String, ByVal usuario As String) As String
        _page = "db.Obtiene_ingresos_med_PROF_Update"
        Dim str As String = ""
        Dim Query As String = String.Format("SELECT IM.IdIngresoMed, convert(VARCHAR,IM.FechaIngreso,103) AS 'FechaIngreso', FP.IdFFProf, FP.Codigo, LTRIM(RTRIM(MP.NomProfilaxis)) + '/' + LTRIM(RTRIM(FF.NomFF)) +  '/'+ LTRIM(RTRIM(FP.Concentracion)) AS 'Medicamento', IM.Cantidad_Ingreso FROM dbo.Ingreso_Med AS IM LEFT OUTER JOIN  FFProf AS FP ON FP.IdFFProf = IM.IdFF LEFT OUTER JOIN  MedProf AS MP ON MP.IdProf = FP.IdProf LEFT OUTER JOIN  FormaFarmaceutica AS FF ON FF.IdFF = FP.IdFF WHERE IdIngresoMed = '{0}' AND TipoIngresoMed = 2 ORDER BY FechaIngreso ASC", idingreso)
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim command As New SqlCommand(Query, connection)
                command.CommandTimeout = TimeoutDB
                Dim reader As SqlDataReader = command.ExecuteReader()
                If reader IsNot Nothing Then
                    While reader.Read()
                        str = "True|" & Convert.ToString(reader("IdIngresoMed")) & "|" & Convert.ToString(reader("Codigo")) & "|" & Convert.ToString(reader("Cantidad_Ingreso")) & "|" & Convert.ToString(reader("IdFFProf"))
                        Exit While
                    End While
                Else
                    str = "False|No se Encontró Información."
                End If
                reader.Dispose()
                reader.Close()
                command.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page & "_" & idingreso
            GrabarErrores(usuario & "|" & _page & "|" & ex.Number & "|" & ex.Message)
            str = "False|" & ex.Message
        End Try
        Return str
    End Function

    '* SP Inserta Ingresos ARV y Prof existencias y modificacion
    Public Sub Ingreso_ARV_PROF_Existencia_Modificar(ByVal tipo_ingreso_med As String, ByVal fecha_ingreso As String, ByVal producto As String, ByVal qty_ingreso As String, ByVal qty_salida As String, tipo_movimiento As String, ByVal usuario As String, ByVal no_req As String, ByVal no_lote As String, ByVal fecha_ven As String)
        _page = "db.Ingreso_ARV_PROF_Existencia_Modificar"
        Dim sql As String = String.Format("EXECUTE dbo.sp_movimientoinventario {0}, '{1}', {2}, {3}, {4}, {5}, '{6}', '{7}', '{8}', '{9}' ", tipo_ingreso_med, fecha_ingreso, producto, qty_ingreso, qty_salida, tipo_movimiento, usuario, no_req, no_lote, fecha_ven)
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim command As New SqlCommand(sql, connection)
                command.ExecuteNonQuery()
                command.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page
            GrabarErrores(usuario & "|" & _page & "|" & ex.Number & "|" & ex.Message)
        End Try
    End Sub

    Public Function Eliminar_Ingreso_ARV(ByVal id As String, ByVal usuario As String) As String
        _page = "db.Eliminar_Ingreso_ARV"
        Dim X As String = Nothing
        Dim sql As String = String.Format("DELETE FROM Ingreso_Med WHERE IdIngresoMed = {0} ", id)
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim command As New SqlCommand(sql, connection)
                command.ExecuteNonQuery()
                command.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
            X = "True|Ingreso eliminado con exito."
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page & "_" & id
            GrabarErrores(usuario & "|" & _page & "|" & ex.Number & "|" & ex.Message)
            X = "False|" & ex.Message
        End Try
        Return X
    End Function
    '* Actualiza existencias al momento de eliminar un ingreso
    Public Sub Update_Existencia_Eliminar_Ingreso(ByVal tipo_ingreso_med As String, ByVal fecha_ingreso As String, ByVal producto As String, ByVal qty_ingreso As String, ByVal qty_salida As String, tipo_movimiento As String, ByVal usuario As String)
        _page = "db.Update_Existencia_Eliminar_Ingreso"
        Dim sql As String = String.Format("EXECUTE dbo.sp_movimientoinventario {0}, '{1}', {2}, {3}, {4}, {5}, '{6}' ", tipo_ingreso_med, fecha_ingreso, producto, qty_ingreso, qty_salida, tipo_movimiento, usuario)
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim command As New SqlCommand(sql, connection)
                command.ExecuteNonQuery()
                command.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page
            GrabarErrores(usuario & "|" & _page & "|" & ex.Number & "|" & ex.Message)
        End Try
    End Sub
    '* Actualiza existencias al momento de Modificar Entrega ARV
    Public Sub Update_Existencia_Egreso(ByVal tipo_ingreso_med As String, ByVal fecha_ingreso As String, ByVal producto As String, ByVal qty_ingreso As String, ByVal qty_salida As String, tipo_movimiento As String, ByVal usuario As String, ByVal no_req As String, ByVal no_lote As String, ByVal fecha_v As String)
        _page = "db.Update_Existencia_Egreso"
        Dim sql As String = String.Format("EXECUTE dbo.sp_movimientoinventario {0}, '{1}', {2}, {3}, {4}, {5}, '{6}', '{7}', '{8}', '{9}' ", tipo_ingreso_med, fecha_ingreso, producto, qty_ingreso, qty_salida, tipo_movimiento, usuario, no_req, no_lote, fecha_v)
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim command As New SqlCommand(sql, connection)
                command.ExecuteNonQuery()
                command.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page
            GrabarErrores(usuario & "|" & _page & "|" & ex.Number & "|" & ex.Message)
        End Try
    End Sub
    '*/Insert corte diario ARV existencia
    Public Sub Graba_Corte_ARV_Existencia(ByVal usuario As String)
        _page = "db.Graba_Corte_ARV_Existencia"
        Dim Q As New StringBuilder()
        ' Dim d As String() = datos.Split("|")
        Q.Append("INSERT INTO Corte_ARV_Inventario_D (Fecha_Corte, IdFFARV, Exsistencia, Usuario)")
        Q.Append("SELECT  (getdate()) AS 'Fecha_Corte', FA.IdFFARV, FA.Existencia , '" & usuario & "' ")
        Q.Append("FROM FFARV AS FA LEFT OUTER  JOIN ")
        Q.Append("MedARV AS M ON FA.IdARV = M.IdARV LEFT OUTER JOIN ")
        Q.Append("FormaFarmaceutica AS FR ON FR.IdFF = FA.IdFF  ")
        Q.Append("ORDER BY FA.IdFFARV  ")
        Dim Query As String = Q.ToString()
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim command As New SqlCommand(Query, connection)
                command.ExecuteNonQuery()
                command.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page
            GrabarErrores(usuario & "|" & _page & "|" & ex.Number & "|" & ex.Message)
        End Try
    End Sub
    '*/Insert corte diario PROF existencia
    Public Sub Graba_Corte_PROF_Existencia(ByVal usuario As String)
        _page = "db.Graba_Corte_PROF_Existencia"
        Dim Q As New StringBuilder()
        ' Dim d As String() = datos.Split("|")
        Q.Append("INSERT INTO Corte_PROF_Inventario_D (Fecha_Corte, IdFFPROF, Exsistencia, Usuario)")
        Q.Append("SELECT  (getdate()) AS 'Fecha_Corte', FP.IdFFPROF, FP.Existencia, '" & usuario & "' ")
        Q.Append("FROM FFProf AS FP LEFT OUTER  JOIN ")
        Q.Append("MedProf AS M ON FP.IdProf = M.IdProf LEFT OUTER JOIN ")
        Q.Append("FormaFarmaceutica AS FR ON FR.IdFF = FP.IdFF ")
        Q.Append("ORDER BY FP.IdFFPROF  ")
        Dim Query As String = Q.ToString()
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim command As New SqlCommand(Query, connection)
                command.ExecuteNonQuery()
                command.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page
            GrabarErrores(usuario & "|" & _page & "|" & ex.Number & "|" & ex.Message)
        End Try
    End Sub
    '* REPORTE CONDONES CONSUMO 
    Public Function Reporte_CondonesLubricantes(ByVal fechaI As String, ByVal fechaF As String, ByVal usuario As String) As DataTable
        _page = "db.Reporte_CondonesLubricantes"
        'Dim fechaI As String = fechaA.ToString() & "-" & fechaM.ToString() & "-01"
        'Dim fechaF As String = fechaA.ToString() & "-" & fechaM.ToString() & "-" & ultimodia
        'Dim fecha As String = fechaA.ToString() & "-" & fechaM.ToString() & "-"
        'Dim diafin As Integer = CInt(ultimodia)
        Dim Q As New StringBuilder()
        Q.Append("SELECT convert(VARCHAR,C.Fecha_Entrega,103) AS 'Fecha_Entrega', C.NHC AS 'NHC', ")
        Q.Append("substring(B1.PrimerNombre,1,1)+''+substring(B1.SegundoNombre,1,1)+''+substring(B1.PrimerApellido,1,1)+''+substring(B1.SegundoApellido,1,1) AS 'Iniciales', ")
        Q.Append("dbo.fn_ObtieneNomProf(C.Codigo_1) AS 'Condon/Lubricante1',C.Cantidad_1, dbo.fn_ObtieneNomProf(C.Codigo_2) AS 'Condon/Lubricante2', C.Cantidad_2, ")
        Q.Append("dbo.fn_ObtieneGeneroXId(P.IdPaciente) AS 'Genero',dbo.fn_ObtieneEdad(B1.FechaNacimiento, getdate()) AS 'edad', ")
        Q.Append("GP.NomGenero  AS 'Genero_Pediatria', dbo.fn_ObtieneEdad(B.FechaNacimiento,getdate()) AS 'Edad' ")
        Q.Append("FROM ControlCONDONES AS C LEFT OUTER JOIN ")
        Q.Append("dbo.PAC_ID AS P ON P.NHC = C.NHC LEFT OUTER JOIN ")
        Q.Append("dbo.PAC_BASALES AS B1 ON B1.IdPaciente = P.IdPaciente LEFT OUTER JOIN ")
        Q.Append("BasalesPediatria AS B ON B.NHC = C.NHC LEFT OUTER JOIN ")
        Q.Append("PAC_M_GENERO AS GP ON GP.IdGenero = B.Genero ")
        Q.Append("WHERE C.NomUsuario <> 'Aldana' AND C.Fecha_Entrega BETWEEN '"& fechaI &"' AND '"& fechaF &"' ")
        Q.Append("ORDER BY C.Fecha_Entrega ASC  ")
        Dim Query As String = Q.ToString()
        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim adapter As New SqlDataAdapter()
                adapter.SelectCommand = New SqlCommand(Query, connection)
                adapter.SelectCommand.CommandTimeout = TimeoutDB
                adapter.Fill(Ds, _page)
                adapter.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
            Return Ds.Tables(0)
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page & "_" & fechaF
            GrabarErrores(usuario & "|" & _pageO & "|" & ex.Number & "|" & ex.Message)
            Return Nothing
        End Try
    End Function

'* REPORTE CONDONES  
    Public Function RCondones(ByVal fechaA As String, ByVal fechaM As String, ByVal ultimodia As String, ByVal usuario As String) As DataTable
        _page = "db.RCondones"
        Dim fecha As String = ultimodia & "/" & fechaM.ToString() & "/" & fechaA.ToString()
        Dim fecha1 As String = "01/" & fechaM.ToString() & "/" & fechaA.ToString()
        Dim Q As New StringBuilder()
        Dim Query As String = ""
        Q.Append("exec ProcReporteCondonLubricante '" & fecha1 & "' , '" & fecha & "' ")
        Query = Q.ToString()
        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim adapter As New SqlDataAdapter()
                adapter.SelectCommand = New SqlCommand(Query, connection)
                adapter.SelectCommand.CommandTimeout = TimeoutDB
                adapter.Fill(Ds, _page)
                adapter.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
            Return Ds.Tables(0)
        Catch ex As SqlException
            _error = ex.Message
            _page = _page
            GrabarErrores(usuario & "|" & ex.Number & "|" & _page & "|" & ex.Message)
            Return Nothing
        End Try
    End Function	
	
	'FUNCIONES PARA ASIGNACION HORARIO CITAS
    'Funcion para buscar DataBase Mangua
    Public Function DatosBasales(ByVal nhc As String, ByVal usuario As String) As String
        _page = "db.DatosBasales"

        Dim Query As String = ""
        Dim Str As String = ""

        Query = String.Format("SELECT P.NHC AS 'NHC', B.PrimerNombre+ ' '+B.SegundoNombre+ ' '+B.PrimerApellido +' '+B.SegundoApellido AS 'Nombre',  dbo.fn_ObtieneGeneroXId(P.IdPaciente) AS 'Genero', dbo.fn_ObtieneEdadXId(P.IdPaciente, getdate()) AS 'Edad', ")
        Query += String.Format("(SELECT TOP(1) convert(VARCHAR,C.FechaEntrega,103)  FROM dbo.ControlARV AS C WHERE C.NHC = P.NHC ORDER BY C.FechaEntrega DESC ) AS 'Fecha_Entrega', ")
        Query += String.Format("(SELECT TOP(1) convert(VARCHAR,C.FechaRetorno,103)  FROM dbo.ControlARV AS C WHERE C.NHC = P.NHC ORDER BY C.FechaRetorno DESC ) AS 'Fecha_Retorno' ")
        Query += String.Format("FROM PAC_ID AS P LEFT OUTER JOIN PAC_BASALES AS B ON B.IdPaciente = P.IdPaciente WHERE P.NHC = '" & nhc & "'")
        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim command As New SqlCommand(Query, connection)
                command.CommandTimeout = TimeoutDB
                Dim reader As SqlDataReader = command.ExecuteReader()
                If reader IsNot Nothing Then
                    While reader.Read()
                        Str = "True|" & Convert.ToString(reader("NHC")) & "|" & Convert.ToString(reader("Nombre")) & "|" & Convert.ToString(reader("Genero")) & "|" & Convert.ToString(reader("Edad")) & "|" & Convert.ToString(reader("Fecha_Entrega")) & "|" & Convert.ToString(reader("Fecha_Retorno"))
                        Exit While
                    End While
                Else
                    Str = "False|No se Encontró Información."
                End If

                reader.Dispose()
                reader.Close()
                command.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page & "_" & nhc
            GrabarErrores(usuario & "|" & _pageO & "|" & ex.Number & "|" & ex.Message)
            Str = "False|" + ex.Message
        End Try
        Return Str
    End Function
	
	'Funcion para buscar Database Farmacia
    Public Function DatosBasalesP(ByVal nhc As String, ByVal usuario As String) As String
        _page = "db.DatosBasalesP"

        Dim Query As String = ""
        Dim Str As String = ""

        Query = String.Format("SELECT B.NHC AS 'NHC', B.PrimerNombre+' '+B.SegundoNombre+' '+B.PrimerApellido+' '+B.SegundoApellido AS 'Nombre', dbo.fn_ObtieneGeneroXId(B.Id) AS 'Genero', dbo.fn_ObtieneEdad(B.FechaNacimiento,getdate()) AS 'Edad', ")
        Query += String.Format("(SELECT TOP(1) convert(VARCHAR,C.FechaEntrega,103)  FROM ControlARV AS C WHERE C.NHC = B.NHC ORDER BY C.FechaEntrega DESC ) AS 'Fecha_Entrega', ")
        Query += String.Format("(SELECT TOP(1) convert(VARCHAR,C.FechaRetorno,103)  FROM dbo.ControlARV AS C WHERE C.NHC = B.NHC ORDER BY C.FechaRetorno DESC ) AS 'Fecha_Retorno' ")
        Query += String.Format("FROM BasalesPediatria AS B WHERE B.NHC = '" & nhc & "'")
        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn2)
                connection.Open()
                Dim command As New SqlCommand(Query, connection)
                command.CommandTimeout = TimeoutDB
                Dim reader As SqlDataReader = command.ExecuteReader()
                If reader IsNot Nothing Then
                    While reader.Read()
                        Str = "True|" & Convert.ToString(reader("NHC")) & "|" & Convert.ToString(reader("Nombre")) & "|" & Convert.ToString(reader("Genero")) & "|" & Convert.ToString(reader("Edad")) & "|" & Convert.ToString(reader("Fecha_Entrega")) & "|" & Convert.ToString(reader("Fecha_Retorno"))
                        Exit While
                    End While
                Else
                    Str = "False|No se Encontró Información."
                End If

                reader.Dispose()
                reader.Close()
                command.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page & "_" & nhc
            GrabarErrores(usuario & "|" & _pageO & "|" & ex.Number & "|" & ex.Message)
            Str = "False|" + ex.Message
        End Try
        Return Str
    End Function
	
	'Listado de Horarios
    Public Function Lista_Horarios(ByVal usuario As String) As DataTable
        _page = "db.Lista_Horarios"
        Dim Query As String = "SELECT H.Id_Bloque_H, H.Desc_Horario FROM HORARIOS_JORNADA_CITAS AS H ORDER BY H.Id_Bloque_H"
        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn2)
                connection.Open()
                Dim adapter As New SqlDataAdapter()
                adapter.SelectCommand = New SqlCommand(Query, connection)
                adapter.SelectCommand.CommandTimeout = TimeoutDB
                adapter.Fill(Ds, _page)
                adapter.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
            Return Ds.Tables(0)
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page
            GrabarErrores(usuario & "|" & _pageO & "|" & ex.Number & "|" & ex.Message)
            Return Nothing
        End Try
    End Function
	
	'Funcion inserta Horario Cita Paciente
    Function Insert_HorarioCitaPaciente(ByVal datos As String, ByVal usuario As String) As String
        _page = "db.Insert_HorarioCitaPaciente"
        Dim sql As String = String.Empty
        Dim X As String = String.Empty
        Dim d As String() = datos.Split("|")
        Dim sqldatenull As sqlDatetime
        sqldatenull = SqlDateTime.Null
        Dim intnull As DBNull
        intnull = Nothing
        Dim Q As New StringBuilder()
        Q.Append("INSERT INTO dbo.HORARIO_CITA_PACIENTE (nhc, fecha_entrega, fecha_retorno, fecha_proxima_cita, id_horario_cita, Usuario)")
        sql = Q.ToString() & String.Format("VALUES('{0}',{1},{2},{3},{4},'{5}')", If(String.IsNullOrEmpty(d(0).ToString()), "Null", d(0).ToString()), IIf(String.IsNullOrEmpty(d(1).ToString()), sqldatenull, "'" & d(1).ToString() & "'"), IIf(String.IsNullOrEmpty(d(2).ToString()), sqldatenull, "'" & d(2).ToString() & "'"), IIf(String.IsNullOrEmpty(d(3).ToString()), sqldatenull, "'" & d(3).ToString() & "'"), If(String.IsNullOrEmpty(d(4).ToString()), "Null", d(4).ToString()), usuario)
        Try
            Using connection As New SqlConnection(_cn2)
                connection.Open()
                Dim command As New SqlCommand(sql, connection)
                command.ExecuteNonQuery()
                command.Dispose()
                connection.Dispose()
                connection.Close()
                X = "True|Registro grabado satisfactoriamente"
            End Using
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page
            GrabarErrores(usuario & "|" & _page & "|" & ex.Number & "|" & ex.Message)
        End Try
        Return X
    End Function

    'FUNCION RESUMEN DE CITAS
   Public Function Resumen(ByVal fecha As String, ByVal usuario As String) As DataTable
        _page = "db.Resumen"
        Dim Q As New StringBuilder()
        Dim Query As String = ""
        Q.Append("SELECT CONVERT(VARCHAR,C.fecha_proxima_cita,103) AS 'Fecha_Retorno', H.Desc_Horario AS 'Horario', count(C.id_horario_cita) AS 'Total' ")
        Q.Append("FROM HORARIO_CITA_PACIENTE AS C INNER JOIN ")
        Q.Append("HORARIOS_JORNADA_CITAS AS H ON H.Id_Bloque_H = C.id_horario_cita ")
        Q.Append("GROUP BY C.fecha_proxima_cita, H.Desc_Horario ,C.id_horario_cita ")
        Query = Q.ToString()
        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn2)
                connection.Open()
                Dim adapter As New SqlDataAdapter()
                adapter.SelectCommand = New SqlCommand(Query, connection)
                adapter.SelectCommand.CommandTimeout = TimeoutDB
                adapter.Fill(Ds, _page)
                adapter.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
            Return Ds.Tables(0)
        Catch ex As SqlException
            _error = ex.Message
            _page = _page
            GrabarErrores(usuario & "|" & ex.Number & "|" & _page & "|" & ex.Message)
            Return Nothing
        End Try
    End Function
	
	 'REPORTE Ingreso medicamento ARV 
    Public Function RepIngresoMedicamentoARV(ByVal fechaA As String, ByVal fechaM As String, ByVal ultimodia As String, ByVal usuario As String) As DataTable
        _page = "db.RepIngresoMedicamentoARV"
        Dim fecha As String = ultimodia & "/" & fechaM.ToString() & "/" & fechaA.ToString()
        Dim fecha1 As String = "01/" & fechaM.ToString() & "/" & fechaA.ToString()
        Dim Q As New StringBuilder()
        Dim Query As String = ""
        Q.Append("SELECT convert(VARCHAR, M.FechaIngreso,103) AS 'Fecha_Ingreso', M.IdFF AS 'IdFF', F.NomFF AS 'NomFF', M1.NomARV+ '/'+F.NomFF+ '/'+F1.Concentracion AS 'Medicamento',substring(F1.Codigo,1,2)+'-'+substring(F1.Codigo,3,3) AS 'Codigo', ")
        Q.Append("M.Cantidad_Ingreso AS 'Cantidad_Ingreso', M.No_Requisicion AS 'No_Requisicion', M.No_Lote AS 'No_Lote', convert(VARCHAR,M.Fecha_Vencimiento,103) AS 'Fecha_Vencimiento' ")
        Q.Append("FROM Ingreso_Med AS M INNER JOIN ")
        Q.Append("FFARV AS F1 ON F1.IdFFARV = M.IdFF LEFT OUTER JOIN ")
        Q.Append("MedARV AS M1 ON M1.IdARV = F1.IdARV LEFT OUTER JOIN  ")
        Q.Append("FormaFarmaceutica AS F ON F.IdFF = F1.IdFF ")
        Q.Append("WHERE M.FechaIngreso BETWEEN '" & fecha1 & "' and '" & fecha & "' AND M.TipoIngresoMed = 1 ")
        Q.Append("ORDER BY M.FechaIngreso ASC")
        Query = Q.ToString()
        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim adapter As New SqlDataAdapter()
                adapter.SelectCommand = New SqlCommand(Query, connection)
                adapter.SelectCommand.CommandTimeout = TimeoutDB
                adapter.Fill(Ds, _page)
                adapter.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
            Return Ds.Tables(0)
        Catch ex As SqlException
            _error = ex.Message
            _page = _page
            GrabarErrores(usuario & "|" & ex.Number & "|" & _page & "|" & ex.Message)
            Return Nothing
        End Try
    End Function

    'REPORTE Ingreso medicamento PRF
    Public Function RepIngresoMedicamentoPROF(ByVal fechaA As String, ByVal fechaM As String, ByVal ultimodia As String, ByVal usuario As String) As DataTable
        _page = "db.RepIngresoMedicamentoPROF"
        Dim fecha As String = ultimodia & "/" & fechaM.ToString() & "/" & fechaA.ToString()
        Dim fecha1 As String = "01/" & fechaM.ToString() & "/" & fechaA.ToString()
        Dim Q As New StringBuilder()
        Dim Query As String = ""
        Q.Append("SELECT convert(VARCHAR, M.FechaIngreso,103) AS 'Fecha_Ingreso',M.IdFF AS 'IdFF',F.NomFF AS 'NomFF', M1.NomProfilaxis+ '/'+F.NomFF+ '/'+P.Concentracion 'Medicamento', P.Codigo 'Codigo', ")
        Q.Append("M.Cantidad_Ingreso AS 'Cantidad_Ingreso', M.No_Requisicion AS 'No_Requisicion', M.No_Lote AS 'No_Lote', convert(VARCHAR,M.Fecha_Vencimiento,103) 'Fecha_Vencimiento' ")
        Q.Append("FROM Ingreso_Med AS M INNER JOIN ")
        Q.Append("FFProf AS P ON P.IdFFProf = M.IdFF LEFT OUTER JOIN ")
        Q.Append("MedProf AS M1 ON M1.IdProf = P.IdProf LEFT OUTER JOIN   ")
        Q.Append("FormaFarmaceutica AS F ON F.IdFF = P.IdFF ")
        Q.Append("WHERE M.FechaIngreso BETWEEN '" & fecha1 & "' and '" & fecha & "' AND M.TipoIngresoMed = 2")
        Q.Append("ORDER BY M.FechaIngreso ASC")
        Query = Q.ToString()
        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim adapter As New SqlDataAdapter()
                adapter.SelectCommand = New SqlCommand(Query, connection)
                adapter.SelectCommand.CommandTimeout = TimeoutDB
                adapter.Fill(Ds, _page)
                adapter.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
            Return Ds.Tables(0)
        Catch ex As SqlException
            _error = ex.Message
            _page = _page
            GrabarErrores(usuario & "|" & ex.Number & "|" & _page & "|" & ex.Message)
            Return Nothing
        End Try
    End Function
	
	'REPORTE otros Egreso medicamento ARV 
    Public Function RepEgresoMedicamentoARV(ByVal fechaA As String, ByVal fechaM As String, ByVal ultimodia As String, ByVal usuario As String) As DataTable
        _page = "db.RepEgresoMedicamentoARV"
        Dim fecha As String = ultimodia & "/" & fechaM.ToString() & "/" & fechaA.ToString()
        Dim fecha1 As String = "01/" & fechaM.ToString() & "/" & fechaA.ToString()
        Dim Q As New StringBuilder()
        Dim Query As String = ""
        Q.Append("SELECT  convert(VARCHAR,O.Fecha_Egreso,103) AS 'Fecha_Egreso' ,O.Tipo_Medicamento ,  O.IdFF , E.Nom_TipoEgreso , I.NomFF , (M.NomARV + '  ' + A.Concentracion) AS  'Medicamento'  , O.Cantidad , O.NHC_TV ")
        Q.Append("FROM Otros_EgresosMed AS O LEFT OUTER JOIN ")
        Q.Append("Tipo_Egreso_Med AS E ON E.Id_TipoEgreso = O.Tipo_Egreso LEFT OUTER JOIN ")
        Q.Append("FFARV AS A ON A.IdFFARV = O.IdFF LEFT OUTER JOIN  ")
        Q.Append("MedARV AS M ON M.IdARV = A.IdARV  LEFT OUTER JOIN ")
        Q.Append("FormaFarmaceutica AS I ON I.IdFF = A.IdFF ")
        Q.Append("WHERE O.Fecha_Egreso BETWEEN '" & fecha1 & "' and '" & fecha & "' AND O.Tipo_Medicamento = 1 AND O.Usuario <> 'aldana'")
        Q.Append("ORDER BY O.Fecha_Egreso DESC")
        Query = Q.ToString()
        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim adapter As New SqlDataAdapter()
                adapter.SelectCommand = New SqlCommand(Query, connection)
                adapter.SelectCommand.CommandTimeout = TimeoutDB
                adapter.Fill(Ds, _page)
                adapter.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
            Return Ds.Tables(0)
        Catch ex As SqlException
            _error = ex.Message
            _page = _page
            GrabarErrores(usuario & "|" & ex.Number & "|" & _page & "|" & ex.Message)
            Return Nothing
        End Try
    End Function

    'REPORTE otros Egreso medicamento PRF
    Public Function RepEgresoMedicamentoPROF(ByVal fechaA As String, ByVal fechaM As String, ByVal ultimodia As String, ByVal usuario As String) As DataTable
        _page = "db.RepEgresoMedicamentoPROF"
        Dim fecha As String = ultimodia & "/" & fechaM.ToString() & "/" & fechaA.ToString()
        Dim fecha1 As String = "01/" & fechaM.ToString() & "/" & fechaA.ToString()
        Dim Q As New StringBuilder()
        Dim Query As String = ""
        Q.Append("SELECT convert(VARCHAR,O.Fecha_Egreso,103) AS 'Fecha_Egreso' ,O.Tipo_Medicamento ,  O.IdFF , E.Nom_TipoEgreso , I.NomFF , (MP.NomProfilaxis  + '  ' + P.Concentracion) AS  'Medicamento'  , O.Cantidad , O.NHC_TV ")
        Q.Append("FROM Otros_EgresosMed AS O LEFT OUTER JOIN ")
        Q.Append("Tipo_Egreso_Med AS E ON E.Id_TipoEgreso = O.Tipo_Egreso LEFT OUTER JOIN ")
        Q.Append("FFProf AS P ON P.IdFFProf = O.IdFF LEFT OUTER JOIN ")
        Q.Append("MedProf AS MP ON MP.IdProf = P.IdProf LEFT OUTER JOIN ")
        Q.Append("FormaFarmaceutica AS I ON I.IdFF = P.IdFF ")
        Q.Append("WHERE O.Fecha_Egreso BETWEEN '" & fecha1 & "' and '" & fecha & "' AND O.Tipo_Medicamento = 2 AND O.Usuario <> 'aldana'")
        Q.Append("ORDER BY O.Fecha_Egreso DESC")
        Query = Q.ToString()
        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim adapter As New SqlDataAdapter()
                adapter.SelectCommand = New SqlCommand(Query, connection)
                adapter.SelectCommand.CommandTimeout = TimeoutDB
                adapter.Fill(Ds, _page)
                adapter.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
            Return Ds.Tables(0)
        Catch ex As SqlException
            _error = ex.Message
            _page = _page
            GrabarErrores(usuario & "|" & ex.Number & "|" & _page & "|" & ex.Message)
            Return Nothing
        End Try
    End Function

    'ASIGNACION DE CITAS-HORARIO 1 
    Public Function NoHorario(ByVal prox_cita As String, ByVal usuario As String) As String
        _page = "db.NoHorario"
        Dim Query As String = String.Empty
        Dim Str As String = ""
        Query = "SELECT count(H.id_hcp) AS 'Citas' FROM HORARIO_CITA_PACIENTE AS H WHERE  convert(VARCHAR,H.fecha_proxima_cita,103) = '" & prox_cita & "'  AND H.id_horario_cita=1 "
        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn2)
                connection.Open()
                Dim command As New SqlCommand(Query, connection)
                command.CommandTimeout = TimeoutDB
                Dim reader As SqlDataReader = command.ExecuteReader()
                If reader IsNot Nothing Then
                    reader.Read()
                    Str = reader("Citas").ToString
                End If
                reader.Dispose()
                reader.Close()
                command.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page & "_"
            GrabarErrores(usuario & "|" & _pageO & "|" & ex.Number & "|" & ex.Message)
            Str = "False|" + ex.Message
        End Try
        Return Str
    End Function

    'ASIGNACION DE CITAS-HORARIO 2 
    Public Function NoHorario1(ByVal prox_cita As String, ByVal usuario As String) As String
        _page = "db.NoHorario1"
        Dim Query As String = String.Empty
        Dim Str As String = ""
        Query = "SELECT count(H.id_hcp) AS 'Citas' FROM HORARIO_CITA_PACIENTE AS H WHERE  convert(VARCHAR,H.fecha_proxima_cita,103) = '" & prox_cita & "'  AND H.id_horario_cita=2 "
        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn2)
                connection.Open()
                Dim command As New SqlCommand(Query, connection)
                command.CommandTimeout = TimeoutDB
                Dim reader As SqlDataReader = command.ExecuteReader()
                If reader IsNot Nothing Then
                    reader.Read()
                    Str = reader("Citas").ToString
                End If
                reader.Dispose()
                reader.Close()
                command.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page & "_"
            GrabarErrores(usuario & "|" & _pageO & "|" & ex.Number & "|" & ex.Message)
            Str = "False|" + ex.Message
        End Try
        Return Str
    End Function

    'ASIGNACION DE CITAS-HORARIO 3 
    Public Function NoHorario2(ByVal prox_cita As String, ByVal usuario As String) As String
        _page = "db.NoHorario2"
        Dim Query As String = String.Empty
        Dim Str As String = ""
        Query = "SELECT count(H.id_hcp) AS 'Citas' FROM HORARIO_CITA_PACIENTE AS H WHERE  convert(VARCHAR,H.fecha_proxima_cita,103) = '" & prox_cita & "'  AND H.id_horario_cita=3 "
        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn2)
                connection.Open()
                Dim command As New SqlCommand(Query, connection)
                command.CommandTimeout = TimeoutDB
                Dim reader As SqlDataReader = command.ExecuteReader()
                If reader IsNot Nothing Then
                    reader.Read()
                    Str = reader("Citas").ToString
                End If
                reader.Dispose()
                reader.Close()
                command.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page & "_"
            GrabarErrores(usuario & "|" & _pageO & "|" & ex.Number & "|" & ex.Message)
            Str = "False|" + ex.Message
        End Try
        Return Str
    End Function

    'ASIGNACION DE CITAS-HORARIO 4 
    Public Function NoHorario3(ByVal prox_cita As String, ByVal usuario As String) As String
        _page = "db.NoHorario3"
        Dim Query As String = String.Empty
        Dim Str As String = ""
        Query = "SELECT count(H.id_hcp) AS 'Citas' FROM HORARIO_CITA_PACIENTE AS H WHERE  convert(VARCHAR,H.fecha_proxima_cita,103) = '" & prox_cita & "'  AND H.id_horario_cita=4 "
        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn2)
                connection.Open()
                Dim command As New SqlCommand(Query, connection)
                command.CommandTimeout = TimeoutDB
                Dim reader As SqlDataReader = command.ExecuteReader()
                If reader IsNot Nothing Then
                    reader.Read()
                    Str = reader("Citas").ToString
                End If
                reader.Dispose()
                reader.Close()
                command.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page & "_"
            GrabarErrores(usuario & "|" & _pageO & "|" & ex.Number & "|" & ex.Message)
            Str = "False|" + ex.Message
        End Try
        Return Str
    End Function

    'ASIGNACION DE CITAS-HORARIO 5 
    Public Function NoHorario4(ByVal prox_cita As String, ByVal usuario As String) As String
        _page = "db.NoHorario4"
        Dim Query As String = String.Empty
        Dim Str As String = ""
        Query = "SELECT count(H.id_hcp) AS 'Citas' FROM HORARIO_CITA_PACIENTE AS H WHERE  convert(VARCHAR,H.fecha_proxima_cita,103) = '" & prox_cita & "'  AND H.id_horario_cita=5 "
        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn2)
                connection.Open()
                Dim command As New SqlCommand(Query, connection)
                command.CommandTimeout = TimeoutDB
                Dim reader As SqlDataReader = command.ExecuteReader()
                If reader IsNot Nothing Then
                    reader.Read()
                    Str = reader("Citas").ToString
                End If
                reader.Dispose()
                reader.Close()
                command.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page & "_"
            GrabarErrores(usuario & "|" & _pageO & "|" & ex.Number & "|" & ex.Message)
            Str = "False|" + ex.Message
        End Try
        Return Str
    End Function

    'ASIGNACION DE CITAS-HORARIO 6 
    Public Function NoHorario5(ByVal prox_cita As String, ByVal usuario As String) As String
        _page = "db.NoHorario5"
        Dim Query As String = String.Empty
        Dim Str As String = ""
        Query = "SELECT count(H.id_hcp) AS 'Citas' FROM HORARIO_CITA_PACIENTE AS H WHERE  convert(VARCHAR,H.fecha_proxima_cita,103) = '" & prox_cita & "'  AND H.id_horario_cita=6 "
        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn2)
                connection.Open()
                Dim command As New SqlCommand(Query, connection)
                command.CommandTimeout = TimeoutDB
                Dim reader As SqlDataReader = command.ExecuteReader()
                If reader IsNot Nothing Then
                    reader.Read()
                    Str = reader("Citas").ToString
                End If
                reader.Dispose()
                reader.Close()
                command.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page & "_"
            GrabarErrores(usuario & "|" & _pageO & "|" & ex.Number & "|" & ex.Message)
            Str = "False|" + ex.Message
        End Try
        Return Str
    End Function

    'ASIGNACION DE CITAS-HORARIO 7 
    Public Function NoHorario6(ByVal prox_cita As String, ByVal usuario As String) As String
        _page = "db.NoHorario6"
        Dim Query As String = String.Empty
        Dim Str As String = ""
        Query = "SELECT count(H.id_hcp) AS 'Citas' FROM HORARIO_CITA_PACIENTE AS H WHERE  convert(VARCHAR,H.fecha_proxima_cita,103) = '" & prox_cita & "'  AND H.id_horario_cita=7 "
        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn2)
                connection.Open()
                Dim command As New SqlCommand(Query, connection)
                command.CommandTimeout = TimeoutDB
                Dim reader As SqlDataReader = command.ExecuteReader()
                If reader IsNot Nothing Then
                    reader.Read()
                    Str = reader("Citas").ToString
                End If
                reader.Dispose()
                reader.Close()
                command.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page & "_"
            GrabarErrores(usuario & "|" & _pageO & "|" & ex.Number & "|" & ex.Message)
            Str = "False|" + ex.Message
        End Try
        Return Str
    End Function

    'ASIGNACION DE CITAS-HORARIO 8 
    Public Function NoHorario7(ByVal prox_cita As String, ByVal usuario As String) As String
        _page = "db.NoHorario7"
        Dim Query As String = String.Empty
        Dim Str As String = ""
        Query = "SELECT count(H.id_hcp) AS 'Citas' FROM HORARIO_CITA_PACIENTE AS H WHERE  convert(VARCHAR,H.fecha_proxima_cita,103) = '" & prox_cita & "'  AND H.id_horario_cita=8 "
        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn2)
                connection.Open()
                Dim command As New SqlCommand(Query, connection)
                command.CommandTimeout = TimeoutDB
                Dim reader As SqlDataReader = command.ExecuteReader()
                If reader IsNot Nothing Then
                    reader.Read()
                    Str = reader("Citas").ToString
                End If
                reader.Dispose()
                reader.Close()
                command.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page & "_"
            GrabarErrores(usuario & "|" & _pageO & "|" & ex.Number & "|" & ex.Message)
            Str = "False|" + ex.Message
        End Try
        Return Str
    End Function

    'ASIGNACION DE CITAS-HORARIO 9 
    Public Function NoHorario8(ByVal prox_cita As String, ByVal usuario As String) As String
        _page = "db.NoHorario8"
        Dim Query As String = String.Empty
        Dim Str As String = ""
        Query = "SELECT count(H.id_hcp) AS 'Citas' FROM HORARIO_CITA_PACIENTE AS H WHERE  convert(VARCHAR,H.fecha_proxima_cita,103) = '" & prox_cita & "'  AND H.id_horario_cita=9 "
        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn2)
                connection.Open()
                Dim command As New SqlCommand(Query, connection)
                command.CommandTimeout = TimeoutDB
                Dim reader As SqlDataReader = command.ExecuteReader()
                If reader IsNot Nothing Then
                    reader.Read()
                    Str = reader("Citas").ToString
                End If
                reader.Dispose()
                reader.Close()
                command.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page & "_"
            GrabarErrores(usuario & "|" & _pageO & "|" & ex.Number & "|" & ex.Message)
            Str = "False|" + ex.Message
        End Try
        Return Str
    End Function

    Public Function Llena_horarios_citas(ByVal op As String, ByVal usuario As String) As DataTable
        _page = "db.Llena_horarios_citas"
        Dim Query As String = String.Empty
        'Seleccion horarios asignados lunes -jueves
        Query = "SELECT H.Id_Bloque_H, H.Desc_Horario FROM HORARIOS_JORNADA_CITAS AS H WHERE h.Id_Bloque_H IN ('1','2', '3','4','5','7','8','9') ORDER BY H.Id_Bloque_H ASC "
        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim adapter As New SqlDataAdapter()
                adapter.SelectCommand = New SqlCommand(Query, connection)
                adapter.SelectCommand.CommandTimeout = TimeoutDB
                adapter.Fill(Ds, _page)
                adapter.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
            Return Ds.Tables(0)
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page
            GrabarErrores(usuario & "|" & _pageO & "|" & ex.Number & "|" & ex.Message)
            Return Nothing
        End Try
    End Function
	
	Public Function RepConsumoTotal(ByVal tipo As String, ByVal fechaI As String, ByVal fechaF As String, ByVal usuario As String) As DataTable
        _page = "db.RepConsumoTotal"
        Dim Query As String = String.Empty
        Select Case tipo
            Case "1" 'ARVS
                Query = "execute RepConsumoTotalARV '" & fechaI & "' , '" & fechaF & "' "

            Case "2" 'PROFILAXIS
                Query = "execute RepConsumoTotalPROF  '" & fechaI & "' , '" & fechaF & "' "
        End Select
        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim adapter As New SqlDataAdapter()
                adapter.SelectCommand = New SqlCommand(Query, connection)
                adapter.SelectCommand.CommandTimeout = TimeoutDB
                adapter.Fill(Ds, _page)
                adapter.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
            Return Ds.Tables(0)
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page & "_" & tipo
            GrabarErrores(usuario & "|" & _pageO & "|" & ex.Number & "|" & ex.Message)
            Return Nothing
        End Try
    End Function
   'Funcion que obtiene CD4 y CV ultima
    Public Function ResultadoCD4CV(ByVal nhc As String, ByVal usuario As String) As String
        _page = "db.ResultadoCD4CV"
        Dim Q As New StringBuilder()
        Dim Query As String = ""
        Dim Str As String = ""
        Q.Append("Select (Select top 1 A.CD4 from ANA_ESPECIAL A where P.IdPaciente = A.IdPaciente and not A.CD4 is null order by A.FechaAnalitica desc) CD4")
        Q.Append(",(Select top 1 A.VIHCVbDNA from ANA_ESPECIAL A where P.IdPaciente = A.IdPaciente and not A.VIHCVbDNA is null order by A.FechaAnalitica desc) CV ")
        Q.Append("FROM PAC_ID P ")
        Q.Append("WHERE P.NHC = '" & nhc & "'")
        Query = Q.ToString()
        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn2)
                connection.Open()
                Dim command As New SqlCommand(Query, connection)
                command.CommandTimeout = TimeoutDB
                Dim reader As SqlDataReader = command.ExecuteReader()
                If reader IsNot Nothing Then
                    While reader.Read()
                        Str = "True|" & Convert.ToString(reader("CD4")) & "|" & Convert.ToString(reader("CV"))
                        Exit While
                    End While
                End If
                If Str = String.Empty Then
                    Str = "False|No se Encontró Información."
                End If
                reader.Dispose()
                reader.Close()
                command.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page & "_" & nhc
            GrabarErrores(usuario & "|" & _page & "|" & ex.Number & "|" & ex.Message)
            Str = "False|" + ex.Message
        End Try
        Return Str
    End Function
	
	Public Function ValidacionARV(ByVal nhc As String, ByVal fecha As String, ByVal esquema As String, ByVal usuario As String) As String
        _page = "db.ValidacionARV"

        Dim Q As New StringBuilder()
        Dim Query As String = ""
        Dim Str As String = ""

        Q.Append(" Select Case When (Select Top 1 NHC From ControlARV  ")
        Q.Append(" Where NHC = '" & nhc & "' and FechaEntrega = '" & fecha & "' and EsquemaEstatus = " & esquema & " ")
        Q.Append(" Group by NHC, FechaEntrega, EsquemaEstatus) is null Then 0 Else 1 End As 'Existencia' ")
        Query = Q.ToString()

        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim command As New SqlCommand(Query, connection)
                command.CommandTimeout = TimeoutDB
                Dim reader As SqlDataReader = command.ExecuteReader()
                If reader IsNot Nothing Then
                    While reader.Read()
                        If Convert.ToString(reader("Existencia")) = "0" Then
                            Str = "True"
                        Else
                            Str = "False"
                        End If
                        Exit While
                    End While
                End If
                If Str = String.Empty Then
                    Str = "False"
                End If
                reader.Dispose()
                reader.Close()
                command.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page & "_" & nhc
            GrabarErrores(usuario & "|" & _page & "|" & ex.Number & "|" & ex.Message)
            Str = "False|" + ex.Message
        End Try
        Return Str

    End Function


    Public Function RepFarmacia(ByVal tipo As String, ByVal fechaI As String, ByVal fechaF As String, ByVal usuario As String) As DataTable
        _page = "db.RepFarmacia"
        Dim Query As String = String.Empty
        Select Case tipo
            Case "1" 'MEDICAMENTOS CON FRECUENCIA
                Query = "execute proc_RepMedFrecuencia '" & fechaI & "' , '" & fechaF & "' "
            Case "2" 'VISITAS
                Query = "execute proc_RepVisitas  '" & fechaI & "' , '" & fechaF & "' "
            Case "3" 'ESQUEMA
                Query = "execute proc_RepEsquema  '" & fechaI & "' , '" & fechaF & "' "
            Case "4" 'ESQUEMA PEDIATRICO
                Query = "execute proc_RepEsquemaPediatrico '" & fechaI & "' , '" & fechaF & "' "

        End Select
        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim adapter As New SqlDataAdapter()
                adapter.SelectCommand = New SqlCommand(Query, connection)
                adapter.SelectCommand.CommandTimeout = TimeoutDB
                adapter.Fill(Ds, _page)
                adapter.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
            Return Ds.Tables(0)
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page & "_" & tipo
            GrabarErrores(usuario & "|" & _pageO & "|" & ex.Number & "|" & ex.Message)
            Return Nothing
        End Try
    End Function



    Public Function Eliminar_RegistroARV(ByVal id As String, ByVal usuario As String) As String
        _page = "db.Eliminar_RegistroARV"
        Dim X As String = Nothing
        Dim sql As String = String.Format("execute sp_MigrarEliminacionARV {0}, '{1}' ", id, usuario)
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim command As New SqlCommand(sql, connection)
                command.ExecuteNonQuery()
                command.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
            X = "True|Registro eliminado con exito."
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page & "_" & id
            GrabarErrores(usuario & "|" & _page & "|" & ex.Number & "|" & ex.Message)
            X = "False|" & ex.Message
        End Try
        Return X
    End Function


    Public Function Eliminar_RegistroPROF(ByVal id As String, ByVal usuario As String) As String
        _page = "db.Eliminar_RegistroPROF"
        Dim X As String = Nothing
        Dim sql As String = String.Format("execute sp_MigrarEliminacionPROF {0}, '{1}' ", id, usuario)
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim command As New SqlCommand(sql, connection)
                command.ExecuteNonQuery()
                command.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
            X = "True|Registro eliminado con exito."
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page & "_" & id
            GrabarErrores(usuario & "|" & _page & "|" & ex.Number & "|" & ex.Message)
            X = "False|" & ex.Message
        End Try
        Return X
    End Function

	    Public Function Eliminar_otro_egreso_ARV(ByVal id As String, ByVal usuario As String) As String
        _page = "db.Eliminar_otro_egreso_ARV"
        Dim X As String = Nothing
        Dim sql As String = String.Format("DELETE FROM Otros_EgresosMed WHERE Id_Otros_EgresosMed = {0} ", id)
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim command As New SqlCommand(sql, connection)
                command.ExecuteNonQuery()
                command.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
            X = "True|Ingreso eliminado con exito."
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page & "_" & id
            GrabarErrores(usuario & "|" & _page & "|" & ex.Number & "|" & ex.Message)
            X = "False|" & ex.Message
        End Try
        Return X
    End Function
	
	'17-12-2020 jchete
	
	Public Function ObtieneEstatusPacienteNuevo(ByVal usuario As String) As DataTable
        _page = "db.ObtieneEstatus"
        Dim Query As String = "SELECT IdEstatus, Codigo FROM Estatus where IdEstatus in (2,13,24,26) ORDER BY Codigo ASC"
        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim adapter As New SqlDataAdapter()
                adapter.SelectCommand = New SqlCommand(Query, connection)
                adapter.SelectCommand.CommandTimeout = TimeoutDB
                adapter.Fill(Ds, _page)
                adapter.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
            Return Ds.Tables(0)
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page
            GrabarErrores(usuario & "|" & _page & "|" & ex.Number & "|" & ex.Message)
            Return Nothing
        End Try
    End Function

'17-12-2020 jchete
    Public Function ObtieneEstatusXpaciSuspendido(ByVal usuario As String) As DataTable
        _page = "db.ObtieneEstatus"
        Dim Query As String = "SELECT IdEstatus, Codigo FROM Estatus where IdEstatus in (18,20,25) ORDER BY Codigo ASC"
        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim adapter As New SqlDataAdapter()
                adapter.SelectCommand = New SqlCommand(Query, connection)
                adapter.SelectCommand.CommandTimeout = TimeoutDB
                adapter.Fill(Ds, _page)
                adapter.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
            Return Ds.Tables(0)
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page
            GrabarErrores(usuario & "|" & _page & "|" & ex.Number & "|" & ex.Message)
            Return Nothing
        End Try
    End Function

    'jchete 07-01-2021
    Public Function ObtieneEstatusSiEstadoContinua(ByVal usuario As String) As DataTable
        _page = "db.ObtieneEstatus"
        Dim Query As String = "SELECT IdEstatus, Codigo FROM Estatus where IdEstatus not in (2,14,18,20,25,26) ORDER BY IdEstatus ASC "
        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim adapter As New SqlDataAdapter()
                adapter.SelectCommand = New SqlCommand(Query, connection)
                adapter.SelectCommand.CommandTimeout = TimeoutDB
                adapter.Fill(Ds, _page)
                adapter.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
            Return Ds.Tables(0)
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page
            GrabarErrores(usuario & "|" & _page & "|" & ex.Number & "|" & ex.Message)
            Return Nothing
        End Try
    End Function

    'jchete 11-01-2021
    Public Function ObtieneEstatusComplementoSuspendido(ByVal usuario As String) As DataTable
        _page = "db.ObtieneEstatus"
        Dim Query As String = "SELECT IdEstatus, Codigo FROM Estatus where IdEstatus in (3) ORDER BY Codigo ASC"
        Dim Ds As New DataSet()
        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim adapter As New SqlDataAdapter()
                adapter.SelectCommand = New SqlCommand(Query, connection)
                adapter.SelectCommand.CommandTimeout = TimeoutDB
                adapter.Fill(Ds, _page)
                adapter.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
            Return Ds.Tables(0)
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page
            GrabarErrores(usuario & "|" & _page & "|" & ex.Number & "|" & ex.Message)
            Return Nothing
        End Try
    End Function
	
	
	 'jchete 11-01-2021
    Public Function ValidacionProf(ByVal nhc As String, ByVal fecha As String, ByVal usuario As String) As String
        _page = "db.ValidacionARV"

        Dim Q As New StringBuilder()
        Dim Query As String = ""
        Dim Str As String = ""

        Q.Append(" Select Case When (Select Top 1 NHC From ControlProf  ")
        Q.Append(" Where NHC = '" & nhc & "' and FechaEntrega = '" & fecha & "'  ")
        Q.Append(" Group by NHC, FechaEntrega) is null Then 0 Else 1 End As 'Existencia' ")
        Query = Q.ToString()

        Try
            Using connection As New SqlConnection(_cn1)
                connection.Open()
                Dim command As New SqlCommand(Query, connection)
                command.CommandTimeout = TimeoutDB
                Dim reader As SqlDataReader = command.ExecuteReader()
                If reader IsNot Nothing Then
                    While reader.Read()
                        If Convert.ToString(reader("Existencia")) = "0" Then
                            Str = "True"
                        Else
                            Str = "False"
                        End If
                        Exit While
                    End While
                End If
                If Str = String.Empty Then
                    Str = "False"
                End If
                reader.Dispose()
                reader.Close()
                command.Dispose()
                connection.Dispose()
                connection.Close()
            End Using
        Catch ex As SqlException
            _error = ex.Message
            _pageO = _page & "_" & nhc
            GrabarErrores(usuario & "|" & _page & "|" & ex.Number & "|" & ex.Message)
            Str = "False|" + ex.Message
        End Try
        Return Str

    End Function
	
	
End Class
