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

Public Class DAContactos

#Region "Comunes"

    Private cnn As MySqlConnection

#End Region

#Region "Metodos"

    Public Sub InsertarContactos(ByVal Item As BEContactos, Optional ByVal tran As MySqlTransaction = Nothing)

        Dim Comando As New MySqlCommand

        Try

            'Cadena de conexion.

            cnn = New MySqlConnection(BDConexion.GetConnectionnovperuvhse())

            'Preparando comando

            Comando = BDConexion.PrepareCommand(Comando, cnn, CommandType.StoredProcedure, "pr_NOVHSE_Contactos_Ins_Reg", tran)

            'Agregando Parametros.

            Comando.Parameters.Add("p_Primernombre", MySqlDbType.VarChar, 50).Value = Item.Primernombre
            Comando.Parameters.Add("p_SegundoNombre", MySqlDbType.VarChar, 50).Value = Item.SegundoNombre
            Comando.Parameters.Add("p_PrimerApellido", MySqlDbType.VarChar, 50).Value = Item.PrimerApellido
            Comando.Parameters.Add("p_SegundoApellido", MySqlDbType.VarChar, 50).Value = Item.SegundoApellido
            Comando.Parameters.Add("p_Estado", MySqlDbType.VarChar, 1).Value = Item.Estado
            Comando.Parameters.Add("p_TipoEntidad", MySqlDbType.VarChar, 3).Value = Item.TipoEntidad
            Comando.Parameters.Add("p_RucEntidad", MySqlDbType.VarChar, 20).Value = Item.Ruc
            Comando.Parameters.Add("p_UsuarioReg", MySqlDbType.VarChar, 100).Value = Item.UsuarioReg
            Comando.Parameters.Add("p_Host", MySqlDbType.VarChar, 30).Value = Item.Host

            'Ejecutamos comando

            Comando.ExecuteScalar()

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If tran Is Nothing Then
                Comando.Connection.Close()
                Comando.Dispose()
            End If
        End Try

    End Sub




    Public Sub EliminarContactos(ByVal Id As Integer, Optional ByVal tran As MySqlTransaction = Nothing)

        Dim Comando As New MySqlCommand

        Try

            'Cadena de conexion.

            cnn = New MySqlConnection(BDConexion.GetConnectionnovperuvhse())

            'Preparando comando

            Comando = BDConexion.PrepareCommand(Comando, cnn, CommandType.StoredProcedure, "pr_NOVHSE_Contactos_Del_Reg", tran)

            'Agregando Parametros.

            Comando.Parameters.Add("p_Id", MySqlDbType.Int32).Value = Id

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



    Public Function ListarContactos(ByVal Item As BEContactos) As List(Of BEContactos)

        Dim Comando As New MySqlCommand
        Dim Lista As New List(Of BEContactos)


        Try

            'Cadena de conexion.

            cnn = New MySqlConnection(BDConexion.GetConnectionnovperuvhse())

            'Preparando comando

            Comando = BDConexion.PrepareCommand(Comando, cnn, CommandType.StoredProcedure, "pr_NOVHSE_Contactos_Sel_Reg", Nothing)

            'Agregando Parametros.

            Comando.Parameters.Add("p_TipoEntidad", MySqlDbType.VarChar, 3).Value = Item.TipoEntidad
            Comando.Parameters.Add("p_RucEntidad", MySqlDbType.VarChar, 20).Value = Item.Ruc
            Comando.Parameters.Add("p_Estado", MySqlDbType.VarChar, 1).Value = Item.Estado


            'ejecutar consulta.


            Dim Data As MySqlDataReader = Comando.ExecuteReader()


            While Data.Read()

                Dim con As New BEContactos()


                con.Id = IIf(IsDBNull(Data("Id")) = True, 0, Data("Id"))
                con.Primernombre = IIf(IsDBNull(Data("Primernombre")) = True, "", Data("Primernombre"))
                con.SegundoNombre = IIf(IsDBNull(Data("SegundoNombre")) = True, "", Data("SegundoNombre"))
                con.PrimerApellido = IIf(IsDBNull(Data("PrimerApellido")) = True, "", Data("PrimerApellido"))
                con.SegundoApellido = IIf(IsDBNull(Data("SegundoApellido")) = True, "", Data("SegundoApellido"))
                con.Estado = IIf(IsDBNull(Data("Estado")) = True, "", Data("Estado"))
                con.TipoEntidad = IIf(IsDBNull(Data("TipoEntidad")) = True, "", Data("TipoEntidad"))
                con.Ruc = IIf(IsDBNull(Data("Ruc")) = True, "", Data("Ruc"))
                con.FecReg = IIf(IsDBNull(Data("FecReg")) = True, 0, Data("FecReg"))
                con.HorReg = IIf(IsDBNull(Data("HorReg")) = True, "", Data("HorReg"))
                con.UsuarioReg = IIf(IsDBNull(Data("UsuarioReg")) = True, "", Data("UsuarioReg"))
                con.FecModif = IIf(IsDBNull(Data("FecModif")) = True, 0, Data("FecModif"))
                con.HorModif = IIf(IsDBNull(Data("HorModif")) = True, "", Data("HorModif"))
                con.UsuarioModif = IIf(IsDBNull(Data("UsuarioModif")) = True, "", Data("UsuarioModif"))
                con.Host = IIf(IsDBNull(Data("Host")) = True, "", Data("Host"))
                con.DesTipoEntidad = IIf(IsDBNull(Data("DesTipoEntidad")) = True, "", Data("DesTipoEntidad"))
                con.DesEstado = IIf(IsDBNull(Data("DesEstado")) = True, "", Data("DesEstado"))

                Lista.Add(con)

            End While

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            Comando.Connection.Close()
            Comando.Dispose()
        End Try

        Return Lista

    End Function


    Public Sub ActualizarContactos(ByVal Item As BEContactos, Optional ByVal tran As MySqlTransaction = Nothing)

        Dim Comando As New MySqlCommand

        Try

            'Cadena de conexion.

            cnn = New MySqlConnection(BDConexion.GetConnectionnovperuvhse())

            'Preparando comando

            Comando = BDConexion.PrepareCommand(Comando, cnn, CommandType.StoredProcedure, "pr_NOVHSE_Contactos_Upd_Reg", tran)

            'Agregando Parametros.

            Comando.Parameters.Add("p_Id", MySqlDbType.Int32).Value = Item.Id
            Comando.Parameters.Add("p_Primernombre", MySqlDbType.VarChar, 50).Value = Item.Primernombre
            Comando.Parameters.Add("p_SegundoNombre", MySqlDbType.VarChar, 50).Value = Item.SegundoNombre
            Comando.Parameters.Add("p_PrimerApellido", MySqlDbType.VarChar, 50).Value = Item.PrimerApellido
            Comando.Parameters.Add("p_SegundoApellido", MySqlDbType.VarChar, 50).Value = Item.SegundoApellido
            Comando.Parameters.Add("p_Estado", MySqlDbType.VarChar, 1).Value = Item.Estado
            Comando.Parameters.Add("p_UsuarioModif", MySqlDbType.VarChar, 100).Value = Item.UsuarioModif
            Comando.Parameters.Add("p_Host", MySqlDbType.VarChar, 30).Value = Item.Host

            'Ejecutamos comando

            Comando.ExecuteScalar()

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
