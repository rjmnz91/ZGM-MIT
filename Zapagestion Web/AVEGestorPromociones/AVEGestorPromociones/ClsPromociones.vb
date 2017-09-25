Imports System.IO
Imports AVEGestorPromociones.GestorPromociones

Public Class clsPromociones
    Public Shared StrCadenaConexion As String
    Public WriteOnly Property ConexionString() As String
        Set(ByVal Value As String)
            StrCadenaConexion = Value
        End Set
    End Property
#Region "Metodos"
    '''<sumary >
    '''METODO PARA EL CALULO DE LA PROMOCION *** PRUEBA DLL ***  
    '''</sumary>
    Public Function Promocion_Calculo(ByVal obj As IEnumerable(Of Promocion)) As IEnumerable(Of Promocion)
        Try

            Dim clsPromo As clsModPromociones = New clsModPromociones

            clsPromo.Recalculo_Promociones(obj)

            Return obj

        Catch ex As Exception
            Throw New Exception(ex.Message, ex.InnerException)
        End Try

    End Function



#End Region



End Class