
'Autor   : Gustavo Mayaute
'Fecha   : 2012.03.02       
'Descripcion: Clase...
Imports System.Data
Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System
Imports NOVHSE.BusinessEntities
Imports System.Data.Common
Imports System.ComponentModel.Component
Imports System.Collections.Generic

Public Class DAConsultaMedica

#Region "Comunes"

    Private cnn As MySqlConnection

#End Region

#Region "Metodos"



    Public Function EliminarConsultaMedica(ByVal Id As String, ByVal IdFicha As String, Optional ByVal tran As MySqlTransaction = Nothing) As String
        Dim Comando As New MySqlCommand
        Dim MensajeBD As String = String.Empty
        Try
            'Cadena de conexion.
            cnn = New MySqlConnection(BDConexion.GetConnectionnovperuvhse())
            'Preparando comando

            Comando = BDConexion.PrepareCommand(Comando, cnn, CommandType.StoredProcedure, "pr_NOVHSE_consultamedica_Del_Reg", tran)
            'Agregando Parametros.
            Comando.Parameters.Add("p_ID", MySqlDbType.VarChar, 6).Value = Id
            Comando.Parameters.Add("p_IDFicha", MySqlDbType.VarChar, 5).Value = IdFicha
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



    Public Function ActualizarConsultaMedica(ByVal Item As BEConsultaMedica, Optional ByVal tran As MySqlTransaction = Nothing) As String
        Dim Comando As New MySqlCommand
        Dim MensajeBD As String = String.Empty

        Try

            'Cadena de conexion.

            cnn = New MySqlConnection(BDConexion.GetConnectionnovperuvhse())

            'Preparando comando

            Comando = BDConexion.PrepareCommand(Comando, cnn, CommandType.StoredProcedure, "pr_NOVHSE_consultamedica_Upd_Reg", tran)

            'Agregando Parametros.
            Comando.Parameters.Add("p_idCons", MySqlDbType.VarChar, 6).Value = Item.CodConsulta
            Comando.Parameters.Add("p_idFicha", MySqlDbType.VarChar, 11).Value = Item.CodFicha
            Comando.Parameters.Add("p_tipo", MySqlDbType.VarChar, 2).Value = Item.Tipo
            Comando.Parameters.Add("p_medico", MySqlDbType.VarChar, 35).Value = Item.Medico
            Comando.Parameters.Add("p_fecha", MySqlDbType.VarChar, 10).Value = Item.Fecha
            Comando.Parameters.Add("p_peso", MySqlDbType.VarChar, 20).Value = Item.Peso
            Comando.Parameters.Add("p_talla", MySqlDbType.VarChar, 20).Value = Item.Talla
            Comando.Parameters.Add("p_imc", MySqlDbType.VarChar, 20).Value = Item.IMC
            Comando.Parameters.Add("p_pa", MySqlDbType.VarChar, 50).Value = Item.PA
            Comando.Parameters.Add("p_atencion", MySqlDbType.VarChar, 1).Value = Item.Atencion
            Comando.Parameters.Add("p_orden", MySqlDbType.VarChar, 10).Value = Item.Orden
            Comando.Parameters.Add("p_obs", MySqlDbType.VarChar, 200).Value = Item.Obs
            Comando.Parameters.Add("p_UsuarioMod", MySqlDbType.VarChar, 100).Value = Item.UsuarioModif
            Comando.Parameters.Add("p_Host", MySqlDbType.VarChar, 30).Value = Item.Host

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





    Public Function InsertarConsultaMedica(ByVal Item As BEConsultaMedica, Optional ByVal tran As MySqlTransaction = Nothing) As String
        Dim Comando As New MySqlCommand
        Dim MensajeBD As String = String.Empty

        Try
            'Cadena de conexion.
            cnn = New MySqlConnection(BDConexion.GetConnectionnovperuvhse())

            'Preparando comando
            Comando = BDConexion.PrepareCommand(Comando, cnn, CommandType.StoredProcedure, "pr_NOVHSE_consultamedica_ins_Reg", tran)



            'Agregando Parametros.
            Comando.Parameters.Add("p_idFicha", MySqlDbType.VarChar, 5).Value = Item.CodFicha
            Comando.Parameters.Add("p_tipo", MySqlDbType.VarChar, 2).Value = Item.Tipo
            Comando.Parameters.Add("p_medico", MySqlDbType.VarChar, 35).Value = Item.Medico
            Comando.Parameters.Add("p_fecha", MySqlDbType.VarChar, 10).Value = Item.Fecha
            Comando.Parameters.Add("p_peso", MySqlDbType.VarChar, 25).Value = Item.Peso
            Comando.Parameters.Add("p_talla", MySqlDbType.VarChar, 25).Value = Item.Talla
            Comando.Parameters.Add("p_imc", MySqlDbType.VarChar, 25).Value = Item.IMC
            Comando.Parameters.Add("p_pa", MySqlDbType.VarChar, 60).Value = Item.PA
            Comando.Parameters.Add("p_atencion", MySqlDbType.VarChar, 1).Value = Item.Atencion
            Comando.Parameters.Add("p_orden", MySqlDbType.VarChar, 9).Value = Item.Orden
            Comando.Parameters.Add("p_obs", MySqlDbType.VarChar, 200).Value = Item.Obs
            Comando.Parameters.Add("p_FechaReg", MySqlDbType.VarChar, 100).Value = Item.FecReg
            Comando.Parameters.Add("p_HoraReg", MySqlDbType.VarChar, 100).Value = Item.HorReg
            Comando.Parameters.Add("p_UsuarioReg", MySqlDbType.VarChar, 100).Value = Item.UsuarioReg
            Comando.Parameters.Add("p_Host", MySqlDbType.VarChar, 30).Value = Item.Host

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
