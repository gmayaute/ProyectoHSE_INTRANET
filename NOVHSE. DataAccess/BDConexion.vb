
'Autor   : Gustavo Mayaute     
'Fecha   : 2012.02.03       
'Descripcion: Clase...
Imports System.Data
Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System
Imports System.Configuration


Public Class BDConexion

    ''' <summary>
    ''' Devuelve la cadena de conexion a la BD novperuvhse 
    ''' </summary>
    ''' 
    Public Shared ReadOnly Property GetConnectionnovperuit() As String
        Get
            Return ConfigurationSettings.AppSettings("ConnectionStringnovperuIT")
        End Get
    End Property

    Public Shared ReadOnly Property GetConnectionnovperuvhse() As String
        Get
            Return ConfigurationSettings.AppSettings("ConnectionStringnovperuvhse")
        End Get
    End Property

    ''' <summary>
    ''' Devuelve la cadena de conexion a la BD novperu_dev 
    ''' </summary>
    Public Shared ReadOnly Property GetConnectionnovperu_dev() As String
        Get
            Return ConfigurationSettings.AppSettings("ConnectionStringnovperu")
        End Get
    End Property


    ''' <summary>
    ''' Prepara el Command Para la Ejecucion en la BD
    ''' </summary>
    Public Shared Function PrepareCommand(ByVal command As MySqlCommand, ByVal cnn As MySqlConnection, ByVal commandType As CommandType, ByVal commandText As String, Optional transaction As MySqlTransaction = Nothing)

        'Verificamos si llaman al metodo como transaccion o sin transaccion.

        If transaction Is Nothing Then
            command.Connection = cnn

        Else
            command.Connection = transaction.Connection
            command.Transaction = transaction
        End If

        'Verificamos si la conexion esta Abierta.
        If (command.Connection.State <> ConnectionState.Open) Then
            command.Connection.Open()
        End If

        command.CommandType = commandType
        command.CommandText = commandText

        Return command

    End Function



End Class
