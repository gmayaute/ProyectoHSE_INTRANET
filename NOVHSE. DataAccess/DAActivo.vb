Imports System.Data
Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System
Imports NOVHSE.BusinessEntities
Imports System.Data.Common
Imports System.ComponentModel.Component
Imports System.Collections.Generic


Public Class DAActivo
#Region "Comunes"

    Private cnn As MySqlConnection

#End Region


#Region "Metodos"

    Public Function ListarActivoYID(ByVal tActivo As String, ByVal cActivo As String) As DataTable
        Dim Comando As New MySqlCommand
        Dim adapter1 As New MySqlDataAdapter()

        Try
            'Cadena de conexion.
            cnn = New MySqlConnection(BDConexion.GetConnectionnovperuit())

            'Preparando comando
            Comando = BDConexion.PrepareCommand(Comando, cnn, CommandType.StoredProcedure, "IN_SP_ListaEquiposIT", Nothing)

            'Agregando Parametros.
            Comando.Parameters.Add("tActivo", MySqlDbType.VarChar, 200).Value = tActivo
            Comando.Parameters.Add("cActivo", MySqlDbType.VarChar, 200).Value = cActivo

            'ejecutar consulta.
            adapter1.SelectCommand = Comando

            Dim Data As New DataSet
            adapter1.Fill(Data)

            Return Data.Tables(0)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            Comando.Connection.Close()
            Comando.Dispose()
            adapter1.Dispose()
        End Try

    End Function


    Public Function ListarAssetAttribute(ByVal Activo As String) As DataTable
        Dim Comando As New MySqlCommand
        Dim adapter1 As New MySqlDataAdapter()

        Try
            'Cadena de conexion.
            cnn = New MySqlConnection(BDConexion.GetConnectionnovperuit())

            'Preparando comando
            Comando = BDConexion.PrepareCommand(Comando, cnn, CommandType.StoredProcedure, "IN_SP_ListaAssetAttribute", Nothing)

            'Agregando Parametros.
            Comando.Parameters.Add("cActivo", MySqlDbType.VarChar, 200).Value = Activo

            'ejecutar consulta.
            adapter1.SelectCommand = Comando

            Dim Data As New DataSet
            adapter1.Fill(Data)

            Return Data.Tables(0)


        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            Comando.Connection.Close()
            Comando.Dispose()
            adapter1.Dispose()
        End Try

    End Function


    Public Function ListarAssetMove(ByVal Activo As String) As DataTable
        Dim Comando As New MySqlCommand
        Dim adapter1 As New MySqlDataAdapter()

        Try
            'Cadena de conexion.
            cnn = New MySqlConnection(BDConexion.GetConnectionnovperuit())

            'Preparando comando
            Comando = BDConexion.PrepareCommand(Comando, cnn, CommandType.StoredProcedure, "IN_SP_ListaAssetMove", Nothing)

            'Agregando Parametros.
            Comando.Parameters.Add("cActivo", MySqlDbType.VarChar, 200).Value = Activo

            'ejecutar consulta.
            adapter1.SelectCommand = Comando

            Dim Data As New DataSet
            adapter1.Fill(Data)

            Return Data.Tables(0)


        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            Comando.Connection.Close()
            Comando.Dispose()
            adapter1.Dispose()
        End Try

    End Function


    Public Function InsertarAsset(ByVal Item As BEActivo, Optional ByVal tran As MySqlTransaction = Nothing) As String
        Dim Comando As New MySqlCommand
        Dim MensajeBD As String
        Try
            'Cadena de conexion.
            cnn = New MySqlConnection(BDConexion.GetConnectionnovperuit())

            'Preparando comando
            Comando = BDConexion.PrepareCommand(Comando, cnn, CommandType.StoredProcedure, "pr_NOVIT_Asset_Ins_Reg", tran)

            'Agregando Parametros.
            Comando.Parameters.Add("p_bl_id", MySqlDbType.VarChar, 4).Value = Item.BusinessLine
            Comando.Parameters.Add("p_type_id", MySqlDbType.Int32).Value = Item.AssetsTipo
            Comando.Parameters.Add("p_network", MySqlDbType.VarChar, 100).Value = Item.Network
            Comando.Parameters.Add("p_purchase", MySqlDbType.VarChar, 10).Value = Item.FecCompra
            Comando.Parameters.Add("p_warranty", MySqlDbType.VarChar, 10).Value = Item.FecGarantia
            Comando.Parameters.Add("p_branch", MySqlDbType.Int32).Value = Item.Marca
            Comando.Parameters.Add("p_model", MySqlDbType.VarChar, 80).Value = Item.Modelo
            Comando.Parameters.Add("p_serial", MySqlDbType.VarChar, 80).Value = Item.Serie
            Comando.Parameters.Add("p_finance", MySqlDbType.VarChar, 20).Value = Item.NomRed
            Comando.Parameters.Add("p_state", MySqlDbType.Int32).Value = Item.StateName
            Comando.Parameters.Add("p_down", MySqlDbType.VarChar, 10).Value = Item.Down
            'Ejecutamos comando
            MensajeBD = Comando.ExecuteScalar()

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If tran Is Nothing Then
                Comando.Connection.Close()
                Comando.Dispose()
            End If
        End Try

        Return MensajeBD
    End Function

    Public Function CopiarAsset(ByVal Item As BEActivo, Optional ByVal tran As MySqlTransaction = Nothing) As String
        Dim Comando As New MySqlCommand
        Dim MensajeBD As String
        Try
            'Cadena de conexion.
            cnn = New MySqlConnection(BDConexion.GetConnectionnovperuit())

            'Preparando comando
            Comando = BDConexion.PrepareCommand(Comando, cnn, CommandType.StoredProcedure, "pr_NOVIT_Asset_Copy_Reg", tran)

            'Agregando Parametros.
            Comando.Parameters.Add("p_idAct", MySqlDbType.VarChar, 4).Value = Item.ID

            'Ejecutamos comando
            MensajeBD = Comando.ExecuteScalar()

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If tran Is Nothing Then
                Comando.Connection.Close()
                Comando.Dispose()
            End If
        End Try

        Return MensajeBD
    End Function


    Public Function ActualizarAsset(ByVal Item As BEActivo, Optional ByVal tran As MySqlTransaction = Nothing) As String
        Dim Comando As New MySqlCommand
        Dim MensajeBD As String
        Try
            'Cadena de conexion.
            cnn = New MySqlConnection(BDConexion.GetConnectionnovperuit())

            'Preparando comando
            Comando = BDConexion.PrepareCommand(Comando, cnn, CommandType.StoredProcedure, "pr_NOVIT_Asset_Upd_Reg", tran)

            'Agregando Parametros.
            Comando.Parameters.Add("p_id", MySqlDbType.Int32).Value = Item.Id
            Comando.Parameters.Add("p_bl_id", MySqlDbType.VarChar, 4).Value = Item.BusinessLine
            Comando.Parameters.Add("p_type_id", MySqlDbType.Int32).Value = Item.AssetsTipo
            Comando.Parameters.Add("p_network", MySqlDbType.VarChar, 100).Value = Item.Network
            Comando.Parameters.Add("p_purchase", MySqlDbType.VarChar, 10).Value = Item.FecCompra
            Comando.Parameters.Add("p_warranty", MySqlDbType.VarChar, 10).Value = Item.FecGarantia
            Comando.Parameters.Add("p_branch", MySqlDbType.Int32).Value = Item.Marca
            Comando.Parameters.Add("p_model", MySqlDbType.VarChar, 80).Value = Item.Modelo
            Comando.Parameters.Add("p_serial", MySqlDbType.VarChar, 80).Value = Item.Serie
            Comando.Parameters.Add("p_finance", MySqlDbType.VarChar, 20).Value = Item.NomRed
            Comando.Parameters.Add("p_state", MySqlDbType.Int32).Value = Item.StateName
            Comando.Parameters.Add("p_down", MySqlDbType.VarChar, 10).Value = Item.Down

            'Ejecutamos comando
            MensajeBD = Comando.ExecuteScalar()

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If tran Is Nothing Then
                Comando.Connection.Close()
                Comando.Dispose()
            End If
        End Try

        Return MensajeBD
    End Function

    Public Function EliminarAsset(ByVal Item As BEActivo, Optional ByVal tran As MySqlTransaction = Nothing) As String
        Dim Comando As New MySqlCommand
        Dim MensajeBD As String
        Try
            'Cadena de conexion.
            cnn = New MySqlConnection(BDConexion.GetConnectionnovperuit())

            'Preparando comando
            Comando = BDConexion.PrepareCommand(Comando, cnn, CommandType.StoredProcedure, "pr_NOVIT_Asset_Del_Reg", tran)

            'Agregando Parametros.
            Comando.Parameters.Add("p_id", MySqlDbType.Int32).Value = Item.ID
            'Ejecutamos comando
            MensajeBD = Comando.ExecuteScalar()

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If tran Is Nothing Then
                Comando.Connection.Close()
                Comando.Dispose()
            End If
        End Try

        Return MensajeBD
    End Function


    Public Function InsertarAtributosAsset(ByVal Item As BEActivo, Optional ByVal tran As MySqlTransaction = Nothing) As String
        Dim Comando As New MySqlCommand
        Dim MensajeBD As String
        Try
            'Cadena de conexion.
            cnn = New MySqlConnection(BDConexion.GetConnectionnovperuit())

            'Preparando comando
            Comando = BDConexion.PrepareCommand(Comando, cnn, CommandType.StoredProcedure, "pr_NOVIT_AssetAtributtes_Ins_Reg", tran)

            'Agregando Parametros.
            Comando.Parameters.Add("p_id", MySqlDbType.Int32).Value = Item.ID
            Comando.Parameters.Add("p_attribName", MySqlDbType.VarChar, 100).Value = Item.NomRed
            Comando.Parameters.Add("p_descrip", MySqlDbType.VarChar, 100).Value = Item.Description
            Comando.Parameters.Add("p_value", MySqlDbType.VarChar, 100).Value = Item.Value
            Comando.Parameters.Add("p_serial", MySqlDbType.VarChar, 100).Value = Item.Serie
            Comando.Parameters.Add("p_obs", MySqlDbType.VarChar, 200).Value = Item.Obs
            Comando.Parameters.Add("p_installdate", MySqlDbType.VarChar, 20).Value = Item.Installdate
            Comando.Parameters.Add("p_state", MySqlDbType.Int32).Value = Item.StateName
            Comando.Parameters.Add("p_down", MySqlDbType.VarChar, 10).Value = Item.Down
            'Ejecutamos comando
            MensajeBD = Comando.ExecuteScalar()

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If tran Is Nothing Then
                Comando.Connection.Close()
                Comando.Dispose()
            End If
        End Try

        Return MensajeBD
    End Function



    Public Function ActualizarAtributosAsset(ByVal Item As BEActivo, Optional ByVal tran As MySqlTransaction = Nothing) As String
        Dim Comando As New MySqlCommand
        Dim MensajeBD As String
        Try
            'Cadena de conexion.
            cnn = New MySqlConnection(BDConexion.GetConnectionnovperuit())

            'Preparando comando
            Comando = BDConexion.PrepareCommand(Comando, cnn, CommandType.StoredProcedure, "pr_NOVIT_AssetAtributtes_Upd_Reg", tran)

            'Agregando Parametros.
            Comando.Parameters.Add("p_idAtrib", MySqlDbType.Int32).Value = Item.IDAttribute
            Comando.Parameters.Add("p_id", MySqlDbType.Int32).Value = Item.ID
            Comando.Parameters.Add("p_attribName", MySqlDbType.VarChar, 100).Value = Item.NomRed
            Comando.Parameters.Add("p_descrip", MySqlDbType.VarChar, 100).Value = Item.Description
            Comando.Parameters.Add("p_value", MySqlDbType.VarChar, 100).Value = Item.Value
            Comando.Parameters.Add("p_serial", MySqlDbType.VarChar, 100).Value = Item.Serie
            Comando.Parameters.Add("p_obs", MySqlDbType.VarChar, 200).Value = Item.Obs
            Comando.Parameters.Add("p_installdate", MySqlDbType.VarChar, 20).Value = Item.Installdate
            Comando.Parameters.Add("p_state", MySqlDbType.Int32).Value = Item.StateName
            Comando.Parameters.Add("p_down", MySqlDbType.VarChar, 10).Value = Item.Down

            'Ejecutamos comando
            MensajeBD = Comando.ExecuteScalar()

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If tran Is Nothing Then
                Comando.Connection.Close()
                Comando.Dispose()
            End If
        End Try

        Return MensajeBD
    End Function

    Public Function EliminarAtributosAsset(ByVal Item As BEActivo, Optional ByVal tran As MySqlTransaction = Nothing) As String
        Dim Comando As New MySqlCommand
        Dim MensajeBD As String
        Try
            'Cadena de conexion.
            cnn = New MySqlConnection(BDConexion.GetConnectionnovperuit())

            'Preparando comando
            Comando = BDConexion.PrepareCommand(Comando, cnn, CommandType.StoredProcedure, "pr_NOVIT_AssetAtributtes_Del_Reg", tran)

            'Agregando Parametros.
            Comando.Parameters.Add("p_idAtrib", MySqlDbType.Int32).Value = Item.IDAttribute
            Comando.Parameters.Add("p_id", MySqlDbType.Int32).Value = Item.ID
            'Ejecutamos comando
            MensajeBD = Comando.ExecuteScalar()

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If tran Is Nothing Then
                Comando.Connection.Close()
                Comando.Dispose()
            End If
        End Try

        Return MensajeBD
    End Function


    Public Function InsertarMovAsset(ByVal Item As BEActivo, Optional ByVal tran As MySqlTransaction = Nothing) As String
        Dim Comando As New MySqlCommand
        Dim MensajeBD As String
        Try
            'Cadena de conexion.
            cnn = New MySqlConnection(BDConexion.GetConnectionnovperuit())

            'Preparando comando
            Comando = BDConexion.PrepareCommand(Comando, cnn, CommandType.StoredProcedure, "pr_NOVIT_AssetMov_Ins_Reg", tran)

            'Agregando Parametros.
            Comando.Parameters.Add("p_id", MySqlDbType.Int32).Value = Item.ID
            Comando.Parameters.Add("p_area", MySqlDbType.VarChar, 100).Value = Item.IDArea
            Comando.Parameters.Add("p_location", MySqlDbType.VarChar, 10).Value = Item.IDLocation
            Comando.Parameters.Add("p_fecentrega", MySqlDbType.VarChar, 10).Value = Item.FecEntrega
            Comando.Parameters.Add("p_user", MySqlDbType.VarChar, 11).Value = Item.UserName
            Comando.Parameters.Add("p_obs", MySqlDbType.VarChar, 20).Value = Item.Obs

            'Ejecutamos comando
            MensajeBD = Comando.ExecuteScalar()

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If tran Is Nothing Then
                Comando.Connection.Close()
                Comando.Dispose()
            End If
        End Try

        Return MensajeBD
    End Function


    Public Function ActualizarMovAsset(ByVal Item As BEActivo, Optional ByVal tran As MySqlTransaction = Nothing) As String
        Dim Comando As New MySqlCommand
        Dim MensajeBD As String
        Try
            'Cadena de conexion.
            cnn = New MySqlConnection(BDConexion.GetConnectionnovperuit())

            'Preparando comando
            Comando = BDConexion.PrepareCommand(Comando, cnn, CommandType.StoredProcedure, "pr_NOVIT_AssetMov_Upd_Reg", tran)

            'Agregando Parametros.
            Comando.Parameters.Add("p_idMov", MySqlDbType.Int32).Value = Item.IDMov
            Comando.Parameters.Add("p_id", MySqlDbType.Int32).Value = Item.ID
            Comando.Parameters.Add("p_area", MySqlDbType.VarChar, 100).Value = Item.IDArea
            Comando.Parameters.Add("p_location", MySqlDbType.VarChar, 10).Value = Item.IDLocation
            Comando.Parameters.Add("p_fecentrega", MySqlDbType.VarChar, 10).Value = Item.FecEntrega
            Comando.Parameters.Add("p_user", MySqlDbType.VarChar, 11).Value = Item.UserName
            Comando.Parameters.Add("p_obs", MySqlDbType.VarChar, 20).Value = Item.Obs

            'Ejecutamos comando
            MensajeBD = Comando.ExecuteScalar()

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If tran Is Nothing Then
                Comando.Connection.Close()
                Comando.Dispose()
            End If
        End Try

        Return MensajeBD
    End Function


    Public Function EliminarMovimientosAsset(ByVal Item As BEActivo, Optional ByVal tran As MySqlTransaction = Nothing) As String
        Dim Comando As New MySqlCommand
        Dim MensajeBD As String
        Try
            'Cadena de conexion.
            cnn = New MySqlConnection(BDConexion.GetConnectionnovperuit())

            'Preparando comando
            Comando = BDConexion.PrepareCommand(Comando, cnn, CommandType.StoredProcedure, "pr_NOVIT_AssetMov_Del_Reg", tran)

            'Agregando Parametros.
            Comando.Parameters.Add("p_idMov", MySqlDbType.Int32).Value = Item.IDMov
            Comando.Parameters.Add("p_id", MySqlDbType.Int32).Value = Item.ID
            'Ejecutamos comando
            MensajeBD = Comando.ExecuteScalar()

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If tran Is Nothing Then
                Comando.Connection.Close()
                Comando.Dispose()
            End If
        End Try

        Return MensajeBD
    End Function

#End Region



End Class
