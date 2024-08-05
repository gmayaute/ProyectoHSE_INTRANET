

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

Public Class DAConsulta


#Region "Comunes"

    Private cnn As MySqlConnection

#End Region


#Region "Metodos"

    Function ListarConsultasPorMenu(ByVal CodModulo As Integer, ByVal IdMenu As Integer) As List(Of BEConsultas)

        Dim ListaConsulta As New List(Of BEConsultas)
        Dim Comando As New MySqlCommand

        Try
            'Cadena de conexion.

            cnn = New MySqlConnection(BDConexion.GetConnectionnovperuvhse())

            'Preparando comando

            Comando = BDConexion.PrepareCommand(Comando, cnn, CommandType.StoredProcedure, "pr_NOVHSE_Consultas_Sel_Reg", Nothing)

            'Agregando Parametros.

            Comando.Parameters.Add("p_modulo", MySqlDbType.Int32).Value = CodModulo
            Comando.Parameters.Add("p_idmenu", MySqlDbType.Int32).Value = IdMenu

            'ejecutar consulta.
            Dim Data As MySqlDataReader = Comando.ExecuteReader()


            While Data.Read()
                Dim Consulta As New BEConsultas()

                Consulta.Id = Data("Id")
                Consulta.IdModulo = Data("IdModulo")
                Consulta.IdMenu = Data("IdMenu")
                Consulta.Nombre = Data("Nombre")
                Consulta.Descripcion = IIf(IsDBNull(Data("Descripcion")) = True, "", Data("Descripcion"))
                Consulta.Ruta = Data("Ruta")
                Consulta.FechaRegistro = IIf(IsDBNull(Data("FechaRegistro")) = True, 0, Data("FechaRegistro"))
                Consulta.HoraRegistro = IIf(IsDBNull(Data("HoraRegistro")) = True, "", Data("FechaRegistro"))
                Consulta.Host = IIf(IsDBNull(Data("Host")) = True, "", Data("Host"))

                ListaConsulta.Add(Consulta)

            End While

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            Comando.Connection.Close()
            Comando.Dispose()
        End Try

        Return ListaConsulta

    End Function



#End Region










End Class
