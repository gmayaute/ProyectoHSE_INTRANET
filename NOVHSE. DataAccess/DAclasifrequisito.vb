
'Autor   : Zoluxiones Consulting (Luis Lucas)       
'Fecha   : 2011.10.06       
'Descripcion: Clase...
Imports System.Data
Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System
Imports NOVHSE.BusinessEntities
Imports System.Data.Common
Imports System.ComponentModel.Component
Imports System.Collections.Generic



Public Class DAclasifrequisito

#Region "Comunes"

    Private cnn As MySqlConnection

#End Region




#Region "Metodos"

    Public Function ListarClasificacionRequisito(ByVal ModuloIdentificador As String, ByVal IdclaseReq As Integer) As DataTable

        Dim Comando As New MySqlCommand
        Dim adapter1 As New MySqlDataAdapter()

        Try
            'Cadena de conexion.

            cnn = New MySqlConnection(BDConexion.GetConnectionnovperuvhse())

            'Preparando comando

            Comando = BDConexion.PrepareCommand(Comando, cnn, CommandType.StoredProcedure, "pr_NOVHSE_clasif_requisito_Sel_Reg", Nothing)

            'Agregando Parametros.

            Comando.Parameters.Add("p_Id", MySqlDbType.Int32).Value = IdclaseReq
            Comando.Parameters.Add("p_ModuloIdentificador", MySqlDbType.VarChar, 10).Value = ModuloIdentificador



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






    Public Function ActualizarClasificacionRequisito(ByVal Item As BEclasifrequisito, Optional ByVal tran As MySqlTransaction = Nothing) As String

        Dim Comando As New MySqlCommand
        Dim MensajeBD As String
        Try

            'Cadena de conexion.

            cnn = New MySqlConnection(BDConexion.GetConnectionnovperuvhse())

            'Preparando comando

            Comando = BDConexion.PrepareCommand(Comando, cnn, CommandType.StoredProcedure, "pr_NOVHSE_clasif_requisito_upd_Reg", tran)

            'Agregando Parametros.

            Comando.Parameters.Add("p_Id", MySqlDbType.Int32).Value = Item.Id
            Comando.Parameters.Add("p_Nombre", MySqlDbType.VarChar, 50).Value = Item.Nombre
            Comando.Parameters.Add("p_Desc", MySqlDbType.VarChar, 300).Value = Item.Desc
            Comando.Parameters.Add("p_Estado", MySqlDbType.VarChar, 1).Value = Item.Estado
            Comando.Parameters.Add("p_UsuarioModif", MySqlDbType.VarChar, 100).Value = Item.UsuarioModif
            Comando.Parameters.Add("p_ModuloIdentificador", MySqlDbType.VarChar, 10).Value = Item.ModuloIdentificador
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




    Public Function InsertarClasificacionRequisito(ByVal Item As BEclasifrequisito, Optional ByVal tran As MySqlTransaction = Nothing)

        Dim Comando As New MySqlCommand
        Dim MensajeBD As String

        Try

            'Cadena de conexion.

            cnn = New MySqlConnection(BDConexion.GetConnectionnovperuvhse())

            'Preparando comando

            Comando = BDConexion.PrepareCommand(Comando, cnn, CommandType.StoredProcedure, "pr_NOVHSE_clasif_requisito_ins_Reg", tran)

            'Agregando Parametros.

            Comando.Parameters.Add("p_Nombre", MySqlDbType.VarChar, 50).Value = Item.Nombre
            Comando.Parameters.Add("p_Desc", MySqlDbType.VarChar, 300).Value = Item.Desc
            Comando.Parameters.Add("p_Estado", MySqlDbType.VarChar, 1).Value = Item.Estado
            Comando.Parameters.Add("p_UsuarioReg", MySqlDbType.VarChar, 100).Value = Item.UsuarioReg
            Comando.Parameters.Add("p_ModuloIdentificador", MySqlDbType.VarChar, 10).Value = Item.ModuloIdentificador
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
