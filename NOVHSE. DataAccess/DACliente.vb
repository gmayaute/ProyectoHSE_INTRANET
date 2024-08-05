
Imports System.Data
Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System
Imports NOVHSE.BusinessEntities
Imports System.Data.Common
Imports System.ComponentModel.Component
Imports System.Collections.Generic

Public Class DACliente

#Region "Comunes"

    Private cnn As MySqlConnection

#End Region


#Region "Metodos"

    Public Function Listar(ByVal DNI As String) As BECliente

        Dim Cliente As New BECliente
        Dim Comando As New MySqlCommand

        Try

            'Cadena de conexion.


            cnn = New MySqlConnection(BDConexion.GetConnectionnovperuvhse())

            'Preparando comando

            Comando = BDConexion.PrepareCommand(Comando, cnn, CommandType.StoredProcedure, "pr_Cliente_Sel_ListarPorDNI", Nothing)

            'Agregando Parametros.
            Comando.Parameters.AddWithValue("?p_DNI", DNI)
            Dim Data As MySqlDataReader = Comando.ExecuteReader()


            While Data.Read()

                Cliente.IdCliente = Data("IdCliente")
                Cliente.DNI = Data("DNI")
                Cliente.ApellidoPaterno = Data("ApellidoPaterno")
                Cliente.ApellidoMaterno = Data("ApellidoMaterno")
                Cliente.Nombres = Data("Nombres")
                Cliente.Email = Data("Email")
                Cliente.Direccion = Data("Direccion")
                Cliente.FechaNacimiento = Data("FechaNacimiento")
                Cliente.PaisNacimiento = Data("PaisNacimiento")
                Cliente.DepartamentoNacimiento = Data("DepartamentoNacimiento")
                Cliente.Imagen = Data("Imagen")

            End While

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            Comando.Connection.Close()
            Comando.Dispose()
        End Try


        Return Cliente

    End Function

    Public Function InsertarCliente(ByVal Item As BECliente, Optional tran As MySqlTransaction = Nothing) As Integer

        Dim Comando As MySqlCommand = cnn.CreateCommand
        Dim Resultado As Integer

        Try

            'Cadena de conexion.

            cnn = New MySqlConnection(BDConexion.GetConnectionnovperuvhse())


            'Preparando comando

            Comando = BDConexion.PrepareCommand(Comando, cnn, CommandType.StoredProcedure, "pr_Cliente_ins_Datos", tran)

            'Agregando Parametros.

            Comando.Parameters.AddWithValue("?p_ApellidoPaterno", Item.ApellidoPaterno)
            Comando.Parameters.AddWithValue("?p_ApellidoMaterno", Item.ApellidoMaterno)
            Comando.Parameters.AddWithValue("?p_Nombres", Item.Nombres)
            Comando.Parameters.AddWithValue("?p_Email", Item.Email)
            Comando.Parameters.AddWithValue("?p_Direccion", Item.Direccion)
            Comando.Parameters.AddWithValue("?p_PaisNacimiento", Item.PaisNacimiento)
            Comando.Parameters.AddWithValue("?p_FechaNacimiento", Item.FechaNacimiento)
            Comando.Parameters.AddWithValue("?p_DepartamentoNacimiento", Item.DepartamentoNacimiento)
            Comando.Parameters.AddWithValue("?p_Distrito", Item.Distrito)
            Comando.Parameters.AddWithValue("?p_Imagen", Item.Imagen)
            Comando.Parameters.AddWithValue("?p_DNI", Item.DNI)
            Comando.Parameters.AddWithValue("?p_NroCuenta", Item.NroCuenta)
            Comando.Parameters.AddWithValue("?p_MontoCuenta", Item.MontoCuenta)



            Resultado = Comando.ExecuteScalar()

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            Comando.Connection.Close()
            Comando.Dispose()
        End Try

        Return Resultado

    End Function


    Public Function InsertarCuentasPorCliente(ByVal Item As BECliente, Optional tran As MySqlTransaction = Nothing) As Integer

        Dim Comando As MySqlCommand = cnn.CreateCommand
        Dim Resultado As Integer

        Try

            'Cadena de conexion.

            cnn = New MySqlConnection(BDConexion.GetConnectionnovperuvhse())

            'Preparando comando

            Comando = BDConexion.PrepareCommand(Comando, cnn, CommandType.StoredProcedure, "pr_Cuentas_cliente_ins_Datos", tran)

            'Agregando Parametros.

            Comando.Parameters.AddWithValue("?p_Idcliente", Item.IdCliente)
            Comando.Parameters.AddWithValue("?p_NroCuenta", Item.NroCuenta)
            Comando.Parameters.AddWithValue("?p_MontoCuenta", Item.MontoCuenta)

            Resultado = Comando.ExecuteNonQuery()

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            Comando.Connection.Close()
            Comando.Dispose()
        End Try

        Return Resultado

    End Function




#End Region

End Class
