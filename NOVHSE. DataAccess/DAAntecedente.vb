Imports System.Data
Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System
Imports NOVHSE.BusinessEntities
Imports System.Data.Common
Imports System.ComponentModel.Component
Imports System.Collections.Generic

Public Class DAAntecedente

#Region "Comunes"

    Private cnn As MySqlConnection

#End Region


#Region "Metodos"




    Public Function ListarAntecedentes(ByVal codemp As String) As List(Of BEAntecedente)

        Dim Comando As New MySqlCommand
        Dim Lista As New List(Of BEAntecedente)

        Try
            'Cadena de conexion.
            cnn = New MySqlConnection(BDConexion.GetConnectionnovperuvhse())

            'Preparando comando
            Comando = BDConexion.PrepareCommand(Comando, cnn, CommandType.StoredProcedure, "pr_NOVHSE_anteced_Sel_Adm_Reg", Nothing)

            'Agregando Parametros.
            Comando.Parameters.Add("p_codemp", MySqlDbType.VarChar, 50).Value = codemp


            'ejecutar consulta.
            Dim Data As MySqlDataReader = Comando.ExecuteReader()


            While Data.Read()

                Dim Item As New BEAntecedente
                Item.CodAntec = IIf(IsDBNull(Data("Cod")) = True, "", Data("Cod"))
                Item.CodTipo = IIf(IsDBNull(Data("CodTipo")) = True, "", Data("CodTipo"))
                Item.Tipo = IIf(IsDBNull(Data("Tipo")) = True, "", Data("Tipo"))
                Item.CodParentesco = IIf(IsDBNull(Data("CodParentesco")) = True, "", Data("CodParentesco"))
                Item.Parentesco = IIf(IsDBNull(Data("Parentesco")) = True, "", Data("Parentesco"))
                Item.Emp = IIf(IsDBNull(Data("Emp")) = True, "", Data("Emp"))
                Item.Descrip = IIf(IsDBNull(Data("Descripcion")) = True, "", Data("Descripcion"))
                Item.FechaIni = IIf(IsDBNull(Data("FecIni")) = True, "", Data("FecIni"))
                Item.FechaFin = IIf(IsDBNull(Data("FecFin")) = True, "", Data("FecFin"))
                Item.FecReg = IIf(IsDBNull(Data("FecReg")) = True, 0, Data("FecReg"))
                Item.HorReg = IIf(IsDBNull(Data("HorReg")) = True, "", Data("HorReg"))
                Item.UsuarioReg = IIf(IsDBNull(Data("UsuarioReg")) = True, 0, Data("UsuarioReg"))
                Item.FecModif = IIf(IsDBNull(Data("FecModif")) = True, 0, Data("FecModif"))
                Item.HorModif = IIf(IsDBNull(Data("HorModif")) = True, "", Data("HorModif"))
                Item.UsuarioModif = IIf(IsDBNull(Data("UsuarioModif")) = True, "", Data("UsuarioModif"))
                Item.Host = IIf(IsDBNull(Data("Host")) = True, "", Data("Host"))

                Lista.Add(Item)

            End While


            Return Lista

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            Comando.Connection.Close()
            Comando.Dispose()
        End Try

        Return Lista
    End Function



    Public Function InsertarAntecedente(ByVal Item As BEAntecedente, Optional ByVal tran As MySqlTransaction = Nothing) As String

        Dim Comando As New MySqlCommand
        Dim MensajeBD As String
        Try

            'Cadena de conexion.
            cnn = New MySqlConnection(BDConexion.GetConnectionnovperuvhse())

            'Preparando comando
            Comando = BDConexion.PrepareCommand(Comando, cnn, CommandType.StoredProcedure, "pr_NOVHSE_anteced_Ins_Reg", tran)

            'Agregando Parametros.
            Comando.Parameters.Add("p_Tipo", MySqlDbType.VarChar, 500).Value = Item.Tipo
            Comando.Parameters.Add("p_Emp", MySqlDbType.VarChar, 15).Value = Item.Emp
            Comando.Parameters.Add("p_Parentesco", MySqlDbType.VarChar, 500).Value = Item.Parentesco
            Comando.Parameters.Add("p_Descripcion", MySqlDbType.VarChar, 500).Value = Item.Descrip
            Comando.Parameters.Add("p_FechaIni", MySqlDbType.VarChar, 500).Value = Item.FechaIni
            Comando.Parameters.Add("p_FechaFin", MySqlDbType.VarChar, 500).Value = Item.FechaFin
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



    Public Sub ModificarAntecedente(ByVal Item As BEAntecedente, Optional ByVal tran As MySqlTransaction = Nothing)
        Dim Comando As New MySqlCommand
        Try
            'Cadena de conexion.

            cnn = New MySqlConnection(BDConexion.GetConnectionnovperuvhse())
            'Preparando comando
            Comando = BDConexion.PrepareCommand(Comando, cnn, CommandType.StoredProcedure, "pr_NOVHSE_anteced_Upd_Reg", tran)

            'Agregando Parametros.
            Comando.Parameters.Add("p_IdAntec", MySqlDbType.VarChar, 10).Value = Item.CodAntec
            Comando.Parameters.Add("p_Tipo", MySqlDbType.VarChar, 500).Value = Item.Tipo
            Comando.Parameters.Add("p_Emp", MySqlDbType.VarChar, 15).Value = Item.Emp
            Comando.Parameters.Add("p_Parentesco", MySqlDbType.VarChar, 500).Value = Item.Parentesco
            Comando.Parameters.Add("p_Descripcion", MySqlDbType.VarChar, 500).Value = Item.Descrip
            Comando.Parameters.Add("p_FechaIni", MySqlDbType.VarChar, 500).Value = Item.FechaIni
            Comando.Parameters.Add("p_FechaFin", MySqlDbType.VarChar, 500).Value = Item.FechaFin
            Comando.Parameters.Add("p_UsuarioModif", MySqlDbType.VarChar, 100).Value = Item.UsuarioModif
            Comando.Parameters.Add("p_Host", MySqlDbType.VarChar, 30).Value = Item.Host

            'Ejecutamos comando
            Comando.ExecuteNonQuery()

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If tran Is Nothing Then
                Comando.Connection.Close()
                Comando.Dispose()
            End If
        End Try
    End Sub

#End Region



End Class
