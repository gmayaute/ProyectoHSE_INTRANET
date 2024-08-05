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

Public Class DAArticulo

#Region "Comunes"

    Private cnn As MySqlConnection

#End Region


#Region "Metodos"



    Public Function ListarGrupoArticulos(ByVal IdGrupo As Integer) As List(Of BEGrupoArticulo)


        Dim Comando As New MySqlCommand
        Dim Lista As New List(Of BEGrupoArticulo)

        Try
            'Cadena de conexion.

            cnn = New MySqlConnection(BDConexion.GetConnectionnovperuvhse())

            'Preparando comando

            Comando = BDConexion.PrepareCommand(Comando, cnn, CommandType.StoredProcedure, "pr_NOVHSE_grupo_articulo_Sel_Reg", Nothing)

            'Agregando Parametros.

            Comando.Parameters.Add("p_Id", MySqlDbType.Int32).Value = IdGrupo

            'ejecutar consulta.

            Dim Data As MySqlDataReader = Comando.ExecuteReader()


            While Data.Read()

                Dim Item As New BEGrupoArticulo

                Item.Id = Data("Id")
                Item.Nombre = IIf(IsDBNull(Data("Nombre")) = True, "", Data("Nombre"))
                Item.Desc = IIf(IsDBNull(Data("descripcion")) = True, "", Data("descripcion"))
                Item.Estado = IIf(IsDBNull(Data("Estado")) = True, "", Data("Estado"))
                Item.FecReg = IIf(IsDBNull(Data("FecReg")) = True, 0, Data("FecReg"))
                Item.HorReg = IIf(IsDBNull(Data("HorReg")) = True, "", Data("HorReg"))
                Item.UsuarioReg = IIf(IsDBNull(Data("UsuarioReg")) = True, "", Data("UsuarioReg"))
                Item.FecModif = IIf(IsDBNull(Data("FecModif")) = True, 0, Data("FecModif"))
                Item.HorModif = IIf(IsDBNull(Data("HorModif")) = True, "", Data("HorModif"))
                Item.UsuarioModif = IIf(IsDBNull(Data("UsuarioModif")) = True, "", Data("UsuarioModif"))
                Item.Host = IIf(IsDBNull(Data("Host")) = True, "", Data("Host"))

                Lista.Add(Item)

            End While




        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            Comando.Connection.Close()
            Comando.Dispose()
        End Try

        Return Lista
    End Function



    Public Function ListarArticulosPorArticuloYID(ByVal IdGrupo As Integer, Id As Integer) As List(Of BEArticulo)

        Dim Comando As New MySqlCommand
        Dim Lista As New List(Of BEArticulo)

        Try
            'Cadena de conexion.
            cnn = New MySqlConnection(BDConexion.GetConnectionnovperuvhse())

            'Preparando comando
            Comando = BDConexion.PrepareCommand(Comando, cnn, CommandType.StoredProcedure, "pr_NOVHSE_articulo_Sel_Reg", Nothing)

            'Agregando Parametros.
            Comando.Parameters.Add("p_IdGrupo", MySqlDbType.Int32).Value = IdGrupo
            Comando.Parameters.Add("p_Id", MySqlDbType.Int32).Value = Id

            'ejecutar consulta.
            Dim Data As MySqlDataReader = Comando.ExecuteReader()

            While Data.Read()
                Dim Item As New BEArticulo
                Item.Id = Data("Id")
                Item.Nombre = IIf(IsDBNull(Data("Nombre")) = True, "", Data("Nombre"))
                Item.Desc = IIf(IsDBNull(Data("descripcion")) = True, "", Data("descripcion"))
                Item.TipoArticulo = IIf(IsDBNull(Data("TipoArticulo")) = True, "", Data("TipoArticulo"))
                Item.Inventariable = IIf(IsDBNull(Data("Inventariable")) = True, "", Data("Inventariable"))
                Item.IdGrupo = IIf(IsDBNull(Data("IdGrupo")) = True, 0, Data("IdGrupo"))
                Item.Estado = IIf(IsDBNull(Data("Estado")) = True, "", Data("Estado"))
                Item.FecReg = IIf(IsDBNull(Data("FecReg")) = True, 0, Data("FecReg"))
                Item.HorReg = IIf(IsDBNull(Data("HorReg")) = True, "", Data("HorReg"))
                Item.UsuarioReg = IIf(IsDBNull(Data("UsuarioReg")) = True, 0, Data("UsuarioReg"))
                Item.FecModif = IIf(IsDBNull(Data("FecModif")) = True, 0, Data("FecModif"))
                Item.HorModif = IIf(IsDBNull(Data("HorModif")) = True, "", Data("HorModif"))
                Item.UsuarioModif = IIf(IsDBNull(Data("UsuarioModif")) = True, "", Data("UsuarioModif"))
                Item.Host = IIf(IsDBNull(Data("Host")) = True, "", Data("Host"))
                Item.UnidadMedida = IIf(IsDBNull(Data("UnidadMedida")) = True, "", Data("UnidadMedida"))
                Item.Marca = IIf(IsDBNull(Data("Marca")) = True, "", Data("Marca"))
                Item.CantidadStock = IIf(IsDBNull(Data("CantidadStock")) = True, "", Data("CantidadStock"))
                Item.Orden = IIf(IsDBNull(Data("Orden")) = True, "", Data("Orden"))
                Item.ValorRef = IIf(IsDBNull(Data("ValorRef")) = True, "", Data("ValorRef"))

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



    Public Function ListarArticulosPorGrupoyModulo(ByVal ModuloIdentificador As String, ByVal IdGrupo As Integer) As List(Of BEArticulo)

        Dim Comando As New MySqlCommand
        Dim Lista As New List(Of BEArticulo)

        Try
            'Cadena de conexion.

            cnn = New MySqlConnection(BDConexion.GetConnectionnovperuvhse())

            'Preparando comando

            Comando = BDConexion.PrepareCommand(Comando, cnn, CommandType.StoredProcedure, "pr_NOVHSE_articulo_Sel_RegXGrupoYModuloIdentif", Nothing)

            'Agregando Parametros.

            Comando.Parameters.Add("p_ModuloIdentificador", MySqlDbType.VarChar, 3).Value = ModuloIdentificador
            Comando.Parameters.Add("p_IdGrupo", MySqlDbType.Int32).Value = IdGrupo


            'ejecutar consulta.

            Dim Data As MySqlDataReader = Comando.ExecuteReader()


            While Data.Read()

                Dim Item As New BEArticulo

                Item.NombreGrupo = IIf(IsDBNull(Data("NombreGrupo")) = True, "", Data("NombreGrupo"))
                Item.ModIdentificadorArticulo = IIf(IsDBNull(Data("ModIdentificadorArticulo")) = True, "", Data("ModIdentificadorArticulo"))
                Item.DenominModulo = IIf(IsDBNull(Data("DenominModulo")) = True, "", Data("DenominModulo"))
                Item.Agrupador = IIf(IsDBNull(Data("Agrupador")) = True, "", Data("Agrupador"))
                Item.Id = IIf(IsDBNull(Data("Id")) = True, 0, Data("Id"))
                Item.Nombre = IIf(IsDBNull(Data("nomArticulo")) = True, "", Data("nomArticulo"))
                Item.Desc = IIf(IsDBNull(Data("Desc")) = True, "", Data("Desc"))
                Item.TipoArticulo = IIf(IsDBNull(Data("TipoArticulo")) = True, "", Data("TipoArticulo"))
                Item.Inventariable = IIf(IsDBNull(Data("Inventariable")) = True, "", Data("Inventariable"))
                Item.IdGrupo = IIf(IsDBNull(Data("IdGrupo")) = True, 0, Data("IdGrupo"))
                Item.Estado = IIf(IsDBNull(Data("Estado")) = True, "", Data("Estado"))
                Item.FecReg = IIf(IsDBNull(Data("FecReg")) = True, 0, Data("FecReg"))
                Item.HorReg = IIf(IsDBNull(Data("HorReg")) = True, "", Data("HorReg"))
                Item.UsuarioReg = IIf(IsDBNull(Data("UsuarioReg")) = True, "", Data("UsuarioReg"))
                Item.FecModif = IIf(IsDBNull(Data("FecModif")) = True, 0, Data("FecModif"))
                Item.HorModif = IIf(IsDBNull(Data("HorModif")) = True, "", Data("HorModif"))
                Item.UsuarioModif = IIf(IsDBNull(Data("UsuarioModif")) = True, "", Data("UsuarioModif"))
                Item.Host = IIf(IsDBNull(Data("Host")) = True, "", Data("Host"))
                Item.UnidadMedida = IIf(IsDBNull(Data("UnidadMedida")) = True, "", Data("UnidadMedida"))
                Item.Marca = IIf(IsDBNull(Data("Marca")) = True, "", Data("Marca"))
                Item.ModIdentifGrupo = IIf(IsDBNull(Data("ModIdentifGrupo")) = True, "", Data("ModIdentifGrupo"))
                Item.CantidadSolicitada = IIf(IsDBNull(Data("CantidadSolicitada")) = True, "", Data("CantidadSolicitada"))
                Item.Motivo = IIf(IsDBNull(Data("Motivo")) = True, "", Data("Motivo"))

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




    Public Function InsertarArticulo(ByVal Item As BEArticulo, Optional ByVal tran As MySqlTransaction = Nothing) As String
        Dim Comando As New MySqlCommand
        Dim MensajeBD As String
        Try
            'Cadena de conexion.
            cnn = New MySqlConnection(BDConexion.GetConnectionnovperuvhse())

            'Preparando comando
            Comando = BDConexion.PrepareCommand(Comando, cnn, CommandType.StoredProcedure, "pr_NOVHSE_articulo_Ins_Reg", tran)

            'Agregando Parametros.
            Comando.Parameters.Add("p_Nombre", MySqlDbType.VarChar, 500).Value = Item.Nombre
            Comando.Parameters.Add("p_Desc", MySqlDbType.VarChar, 500).Value = Item.Desc
            Comando.Parameters.Add("p_TipoArticulo", MySqlDbType.VarChar, 10).Value = Item.TipoArticulo
            Comando.Parameters.Add("p_Inventariable", MySqlDbType.VarChar, 1).Value = Item.Inventariable
            Comando.Parameters.Add("p_IdGrupo", MySqlDbType.Int32).Value = Item.IdGrupo
            Comando.Parameters.Add("p_Estado", MySqlDbType.VarChar, 1).Value = Item.Estado
            Comando.Parameters.Add("p_UsuarioReg", MySqlDbType.VarChar, 100).Value = Item.UsuarioReg
            Comando.Parameters.Add("p_Host", MySqlDbType.VarChar, 30).Value = Item.Host
            Comando.Parameters.Add("p_UnidadMedida", MySqlDbType.VarChar, 4).Value = Item.UnidadMedida
            Comando.Parameters.Add("p_Marca", MySqlDbType.VarChar, 10).Value = Item.Marca
            Comando.Parameters.Add("p_Orden", MySqlDbType.Int32).Value = Item.Orden

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



    Public Sub ModificarArticulo(ByVal Item As BEArticulo, Optional ByVal tran As MySqlTransaction = Nothing)

        Dim Comando As New MySqlCommand
        Try

            'Cadena de conexion.

            cnn = New MySqlConnection(BDConexion.GetConnectionnovperuvhse())

            'Preparando comando

            Comando = BDConexion.PrepareCommand(Comando, cnn, CommandType.StoredProcedure, "pr_NOVHSE_articulo_Upd_Reg", tran)

            'Agregando Parametros.

            Comando.Parameters.Add("p_Id", MySqlDbType.Int32).Value = Item.Id
            Comando.Parameters.Add("p_Nombre", MySqlDbType.VarChar, 500).Value = Item.Nombre
            Comando.Parameters.Add("p_Desc", MySqlDbType.VarChar, 500).Value = Item.Desc
            Comando.Parameters.Add("p_TipoArticulo", MySqlDbType.VarChar, 10).Value = Item.TipoArticulo
            Comando.Parameters.Add("p_Inventariable", MySqlDbType.VarChar, 1).Value = Item.Inventariable
            Comando.Parameters.Add("p_IdGrupo", MySqlDbType.Int32).Value = Item.IdGrupo
            Comando.Parameters.Add("p_Estado", MySqlDbType.VarChar, 1).Value = Item.Estado
            Comando.Parameters.Add("p_UsuarioModif", MySqlDbType.VarChar, 100).Value = Item.UsuarioModif
            Comando.Parameters.Add("p_Host", MySqlDbType.VarChar, 30).Value = Item.Host
            Comando.Parameters.Add("p_UnidadMedida", MySqlDbType.VarChar, 4).Value = Item.UnidadMedida
            Comando.Parameters.Add("p_Marca", MySqlDbType.VarChar, 10).Value = Item.Marca
            Comando.Parameters.Add("p_Orden", MySqlDbType.Int32).Value = Item.Orden

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
