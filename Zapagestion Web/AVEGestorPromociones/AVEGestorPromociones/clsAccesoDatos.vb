
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.IO

Public Class clsAccesoDatos
    'clase padre para operar las tablas de la BBDD
    Friend conexion As SqlConnection

    Dim objCon As clsConexion


    Public Sub New()

        objCon = New clsConexion()
        conexion = objCon.GetCon

    End Sub

    Friend Sub AbrirConexion(ByRef cnn As SqlConnection)
        Try
            cnn.Open()
        Catch ex As Exception
            Throw New Exception(ex.Message, ex.InnerException)
        End Try
    End Sub

    Friend Overloads Sub EjecutarSQL(ByVal StrSql As String, ByVal conection As SqlConnection, ByVal transacion As SqlTransaction)
        Try
            Dim SqlActualizar As SqlCommand
            conection.Open()
            SqlActualizar = New SqlCommand()
            SqlActualizar.CommandType = CommandType.Text
            SqlActualizar.CommandText = StrSql
            SqlActualizar.Connection = conection
            SqlActualizar.Transaction = transacion
            SqlActualizar.CommandTimeout = 0
            SqlActualizar.ExecuteNonQuery()
            conection.Close()
        Catch ex As Exception
            Throw New Exception(ex.Message, ex.InnerException)
        End Try
    End Sub

    Friend Overloads Sub EjecutarSQL(ByVal StrSql As String)
        Try

            Dim SqlActualizar As SqlCommand

            conexion.Open()

            SqlActualizar = New SqlCommand()
            SqlActualizar.CommandType = CommandType.Text
            SqlActualizar.CommandText = StrSql
            SqlActualizar.Connection = conexion
            SqlActualizar.CommandTimeout = 0

            SqlActualizar.ExecuteNonQuery()

            conexion.Close()

        Catch ex As Exception
            Throw New Exception(ex.Message, ex.InnerException)
        End Try
    End Sub

    Friend Sub CerrarConexion(ByRef cnn As SqlConnection)
        Try

            cnn.Close()

        Catch ex As Exception
            Throw New Exception(ex.Message, ex.InnerException)
        End Try

    End Sub

    Friend Sub CargarDataset(ByRef Datset As DataSet, ByVal ordensql As String, ByVal nombretabla As String)
        Try

            Dim dadapter As New SqlDataAdapter
            Dim comando As New SqlCommand

            conexion.Open()

            With comando
                .CommandType = CommandType.Text
                .CommandText = ordensql
                .Connection = conexion
                .CommandTimeout = 0
            End With

            Datset = New DataSet()

            dadapter.SelectCommand = comando
            dadapter.Fill(Datset, nombretabla)
            conexion.Close()


        Catch ex As Exception
            Throw New Exception(ex.Message, ex.InnerException)
        End Try
    End Sub

End Class