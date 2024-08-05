
'Autor   : Zoluxiones Consulting (LuisLucas)       
'Fecha   : 2011.10.05       
'Descripcion: Clase...

Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System

Public Class BDTransaccion
    Implements IDisposable


    Private DBTransaccion As MySqlTransaction
    Private cnn As MySqlConnection

    Public Sub New(ByVal cadenaConexion As String)
        cnn = New MySqlConnection(cadenaConexion)
    End Sub

    Public ReadOnly Property Transaccion() As MySqlTransaction
        Get
            Return DBTransaccion
        End Get
    End Property


#Region "Metodos"

    Public Sub BeginTransaction()
        If Not (cnn.State = ConnectionState.Open) Then cnn.Open()
        DBTransaccion = cnn.BeginTransaction()
    End Sub

    Public Sub Rollback()
        DBTransaccion.Rollback()
    End Sub

    Public Sub Commit()
        DBTransaccion.Commit()
    End Sub

#End Region


#Region " IDisposable Support "
    Private disposedValue As Boolean = False ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: free unmanaged resources when explicitly called
                cnn.Close()
                cnn.Dispose() 'Aqui se cierra la conexion tambien 
            End If

            ' TODO: free shared unmanaged resources
        End If
        Me.disposedValue = True
    End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
