Imports System.Data.SqlClient

Public Class clsConexion

    Dim conexion As SqlConnection
    Dim strConex As String


    Public Sub New()
        Try
            'Constructor lo leemos del app.config

            conexion = New SqlConnection()
            ' conexion.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings(BBDD).ToString()
            conexion.ConnectionString = clsPromociones.StrCadenaConexion
        Catch ex As Exception
            Throw New Exception(ex.Message, ex.InnerException)
        End Try
    End Sub

    Private Function LeerConexionRegistro() As String

        Try
            Dim Dns As String
            Dim User As String
            Dim Passwd As String
            Dim Controlador As String
            Dim Servidor As String
            Dim IdUsuario As String
            Dim NomBD As String

            Dns = GetSetting("ZAPAGESTIONTPV", "Config", "DNS")
            User = GetSetting("ZAPAGESTIONTPV", "Config", "User")
            Passwd = Encriptar(GetSetting("ZAPAGESTIONTPV", "Config", "Passwd"), False)
            NomBD = GetSetting("ZAPAGESTIONTPV", "Config", "NomBD")
            Controlador = GetSetting("ZAPAGESTIONTPV", "Config", "Controlador")
            Servidor = GetSetting("ZAPAGESTIONTPV", "Config", "Servidor")
            IdUsuario = GetSetting("ZAPAGESTIONTPV", "Config", "IdUsuario")

            If User.Length = 0 And Passwd.Length = 0 Then
                Return "Data Source=" & Servidor & ";Initial Catalog=" & NomBD & ";Integrated Security=True"
            Else
                Return "Data Source=" & Servidor & ";Initial Catalog=" & NomBD & ";User ID=" & User & ";Password=" & Passwd
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message, ex.InnerException)
            Return String.Empty
        End Try

    End Function

    Public Function GetCon() As SqlConnection
        GetCon = conexion
    End Function

    Private Shared Function Encriptar(ByVal sPassword As String, ByVal bAccion As Boolean) As String

        Try
            Dim sAuxiliar As String = String.Empty
            Dim sAuxiliar2 As String = String.Empty
            Dim I As Integer
            Dim iascii As Integer

            If bAccion Then

                For I = Len(sPassword) To 1 Step -1
                    sAuxiliar = sAuxiliar + Mid$(sPassword, I, 1)
                Next

                For I = 1 To Len(sAuxiliar)
                    iascii = Asc(Mid(sAuxiliar, I, 1))
                    If iascii = 49 Then iascii = 40
                    sAuxiliar2 = sAuxiliar2 + Chr(iascii - 5)
                Next

            Else

                For I = 1 To Len(sPassword)
                    iascii = Asc(Mid$(sPassword, I, 1))
                    If iascii = 35 Then iascii = Asc(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator)
                    sAuxiliar = sAuxiliar + Chr(iascii + 5)
                Next

                For I = Len(sPassword) To 1 Step -1
                    sAuxiliar2 = sAuxiliar2 + Mid$(sAuxiliar, I, 1)
                Next

            End If

            Encriptar = sAuxiliar2

            Return Encriptar
        Catch ex As Exception
            Throw New Exception(ex.Message, ex.InnerException)
        End Try
    End Function


End Class
