
Imports System.IO
Imports System.EnterpriseServices
Imports System.Runtime.InteropServices

Public Interface IIO

    Sub File_Copy(ByVal nombreArchivoOrigen As String, ByVal nombreArchivoDestino As String)
    Sub File_Copy(ByVal nombreArchivoOrigen As String, ByVal nombreArchivoDestino As String, ByVal reemplazar As Boolean)
    Sub File_Delete(ByVal nombreArchivo As String)
    Function File_Exists(ByVal nombreArchivo As String) As Boolean
    Function File_Info(ByVal nombreArchivo As String) As FileInfo

    Sub Directory_CreateDirectory(ByVal carpeta As String)
    Function Directory_Exists(ByVal carpeta As String) As Boolean
    Sub Directory_Delete(ByVal carpeta As String)
    Function Directory_GetFiles(ByVal carpeta As String) As String()

    Function TestConection(ByVal mensaje As String) As String

End Interface


Public Class IONOV
    Implements IIO

#Region "Metodos con Archivos"

    Public Sub File_Copy(ByVal nombreArchivoOrigen As String, ByVal nombreArchivoDestino As String) Implements IIO.File_Copy
        File.Copy(nombreArchivoOrigen, nombreArchivoDestino)
    End Sub

    Public Sub File_Copy(ByVal nombreArchivoOrigen As String, ByVal nombreArchivoDestino As String, ByVal reemplazar As Boolean) Implements IIO.File_Copy
        File.Copy(nombreArchivoOrigen, nombreArchivoDestino, reemplazar)
    End Sub

    Public Sub File_Delete(ByVal nombreArchivo As String) Implements IIO.File_Delete
        File.Delete(nombreArchivo)
    End Sub

    Public Function File_Exists(ByVal nombreArchivo As String) As Boolean Implements IIO.File_Exists
        Return File.Exists(nombreArchivo)
    End Function

    Public Function File_Info(ByVal nombreArchivo As String) As FileInfo Implements IIO.File_Info
        Return New FileInfo(nombreArchivo)
    End Function

#End Region

#Region "Metodos con Carpetas"

    Public Sub Directory_CreateDirectory(ByVal carpeta As String) Implements IIO.Directory_CreateDirectory
        Directory.CreateDirectory(carpeta)
    End Sub

    Public Function Directory_Exists(ByVal carpeta As String) As Boolean Implements IIO.Directory_Exists
        Return Directory.Exists(carpeta)
    End Function

    Public Sub Directory_Delete(ByVal carpeta As String) Implements IIO.Directory_Delete
        Directory.Delete(carpeta)
    End Sub

    Public Function Directory_GetFiles(ByVal carpeta As String) As String() Implements IIO.Directory_GetFiles
        Return Directory.GetFiles(carpeta)
    End Function

#End Region


    Public Function TestConection(ByVal mensaje As String) As String Implements IIO.TestConection
        Return "Conexión exitosa (" & mensaje & ")"
    End Function



End Class
