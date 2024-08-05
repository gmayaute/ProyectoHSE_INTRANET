Imports NOVHSE.BusinessLogic
Imports NOVHSE.BusinessEntities
Imports NOVDOC.Utiles
Imports Reutilizables
Imports System.Web.UI.WebControls
Imports Telerik.Web.UI

Public Class AlertaSuministroLicencia
    Inherits PaginaBase

#Region "Eventos"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            '' Try to force IE9 into IE8 Standards mode
            If Not Me.Page.IsPostBack Then
                CompatibleIE8()
                InicializarValores()
            End If
        Catch ex As UserException
            GenerarAlerta_ScriptManager(ex.Message)
        Catch ex As Exception
            ManejarExcepcion_ScriptManager(ex, System.IO.Path.GetFileName(Request.ServerVariables("SCRIPT_NAME")))
        End Try
    End Sub

    Protected Sub rgContactos_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgContactos.NeedDataSource
        Try
            rgContactos.DataSource = ViewState("ListaAlertaDocumento")
        Catch ex As UserException
            GenerarAlerta_ScriptManager(ex.Message)
        Catch ex As Exception
            ManejarExcepcion_ScriptManager(ex, System.IO.Path.GetFileName(Request.ServerVariables("SCRIPT_NAME")))
        End Try
    End Sub

    Protected Sub rgContactos_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles rgContactos.ItemCommand
        Try
            Dim ObjUpdate As New BLDocumento()

            If e.CommandName.Equals("Edit") Then
                Dim Parametro As String() = CType(e.Item.FindControl("ImgDatos"), ImageButton).CommandArgument().Split(",")
                ViewState("ParametrosEdit") = Parametro
            ElseIf e.CommandName.Equals("VerDocumento") Then
                Dim Datos As String() = e.CommandArgument.ToString().Split(",")
            End If

        Catch ex As UserException
            GenerarAlerta_ScriptManager(ex.Message)
        Catch ex As Exception
            ManejarExcepcion_ScriptManager(ex, System.IO.Path.GetFileName(Request.ServerVariables("SCRIPT_NAME")))
        End Try
    End Sub

    Protected Sub rgContactos_ItemCreated(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgContactos.ItemCreated
        Try

            If TypeOf e.Item Is GridEditFormItem AndAlso e.Item.IsInEditMode Then
                Dim oGridEditableItem As GridEditableItem = e.Item
                Dim Parametros As String() = ViewState("ParametrosEdit")

                Utiles.ValidacionGridBoundColumn(oGridEditableItem, "nombre", 50, True, TipoValidacion.Texto, "200")
                Utiles.ValidacionGridBoundColumn(oGridEditableItem, "direccion", 100, True, TipoValidacion.Texto, "300")
                Utiles.ValidacionGridBoundColumn(oGridEditableItem, "codlicencia", 50, False, TipoValidacion.Texto, "100")
                Utiles.ValidacionGridBoundColumn(oGridEditableItem, "LicenFecIni", 10, False, TipoValidacion.Texto, "60")
                Utiles.ValidacionGridBoundColumn(oGridEditableItem, "LicenFecFin", 10, False, TipoValidacion.Texto, "60")
                Utiles.ValidacionGridBoundColumn(oGridEditableItem, "coddefensa", 50, False, TipoValidacion.Texto, "100")
                Utiles.ValidacionGridBoundColumn(oGridEditableItem, "DefenFecIni", 10, False, TipoValidacion.Texto, "60")
                Utiles.ValidacionGridBoundColumn(oGridEditableItem, "DefenFecFin", 10, False, TipoValidacion.Texto, "60")
                Utiles.ValidacionGridBoundColumn(oGridEditableItem, "codcertambiental", 50, False, TipoValidacion.Texto, "100")
                Utiles.ValidacionGridBoundColumn(oGridEditableItem, "AmbienFecIni", 10, False, TipoValidacion.Texto, "60")
                Utiles.ValidacionGridBoundColumn(oGridEditableItem, "AmbienFecFin", 10, False, TipoValidacion.Texto, "60")
                Utiles.ValidacionGridBoundColumn(oGridEditableItem, "codfumigacion", 50, False, TipoValidacion.Texto, "100")
                Utiles.ValidacionGridBoundColumn(oGridEditableItem, "FumigFecIni", 10, False, TipoValidacion.Texto, "60")
                Utiles.ValidacionGridBoundColumn(oGridEditableItem, "FumigFecFin", 10, False, TipoValidacion.Texto, "60")


                'If oGridEditableItem.ItemIndex <> -1 Then 'Estas Modficando
                '    CType(e.Item.FindControl("RadComboEstado"), RadComboBox).SelectedValue = Parametros(1)
                '    CType(e.Item.FindControl("RadComboTipoServ"), RadComboBox).SelectedValue = Parametros(6)
                'End If
            End If

        Catch ex As UserException
            GenerarAlerta_ScriptManager(ex.Message)
        Catch ex As Exception
            ManejarExcepcion_ScriptManager(ex, System.IO.Path.GetFileName(Request.ServerVariables("SCRIPT_NAME")))
        End Try
    End Sub

    Protected Sub rgContactos_UpdateCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles rgContactos.UpdateCommand
        Try
            Dim Item As New BEDocumento
            Dim ObjUpdate As New BLDocumento()
            Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
            Dim Parametro As String() = ViewState("ParametrosEdit")

            'Campos Obligatorios
            Dim nombre As String = String.Empty
            Dim direccion As String = String.Empty
            Dim codlicencia As String = String.Empty
            Dim LicenFecIni As String = String.Empty
            Dim LicenFecFin As String = String.Empty
            Dim coddefensa As String = String.Empty
            Dim DefenFecIni As String = String.Empty
            Dim DefenFecFin As String = String.Empty
            Dim codcertambiental As String = String.Empty
            Dim AmbienFecIni As String = String.Empty
            Dim AmbienFecFin As String = String.Empty
            Dim codfumigacion As String = String.Empty
            Dim FumigFecIni As String = String.Empty
            Dim FumigFecFin As String = String.Empty


            nombre = CType(editedItem.EditManager.GetColumnEditor("nombre"), GridTextColumnEditor).Text
            direccion = CType(editedItem.EditManager.GetColumnEditor("direccion"), GridTextColumnEditor).Text
            codlicencia = CType(editedItem.EditManager.GetColumnEditor("codlicencia"), GridTextColumnEditor).Text
            LicenFecIni = CType(editedItem.EditManager.GetColumnEditor("LicenFecIni"), GridTextColumnEditor).Text
            LicenFecFin = CType(editedItem.EditManager.GetColumnEditor("LicenFecFin"), GridTextColumnEditor).Text
            coddefensa = CType(editedItem.EditManager.GetColumnEditor("coddefensa"), GridTextColumnEditor).Text
            DefenFecIni = CType(editedItem.EditManager.GetColumnEditor("DefenFecIni"), GridTextColumnEditor).Text
            DefenFecFin = CType(editedItem.EditManager.GetColumnEditor("DefenFecFin"), GridTextColumnEditor).Text
            codcertambiental = CType(editedItem.EditManager.GetColumnEditor("codcertambiental"), GridTextColumnEditor).Text
            AmbienFecIni = CType(editedItem.EditManager.GetColumnEditor("AmbienFecIni"), GridTextColumnEditor).Text
            AmbienFecFin = CType(editedItem.EditManager.GetColumnEditor("AmbienFecFin"), GridTextColumnEditor).Text
            codfumigacion = CType(editedItem.EditManager.GetColumnEditor("codfumigacion"), GridTextColumnEditor).Text
            FumigFecIni = CType(editedItem.EditManager.GetColumnEditor("FumigFecIni"), GridTextColumnEditor).Text
            FumigFecFin = CType(editedItem.EditManager.GetColumnEditor("FumigFecFin"), GridTextColumnEditor).Text

            If String.IsNullOrEmpty(nombre) Then Throw New UserException("El nombre es obligatorio.")
            If String.IsNullOrEmpty(direccion) Then Throw New UserException("La dirección es obligatorio.")


            Item.Id = Parametro(0)
            Item.nombre = nombre
            Item.direccion = direccion
            Item.codlicencia = codlicencia
            Item.LicenFecIni = LicenFecIni
            Item.LicenFecFin = LicenFecFin
            Item.coddefensa = coddefensa
            Item.DefenFecIni = DefenFecIni
            Item.DefenFecFin = DefenFecFin
            Item.codcertambiental = codcertambiental
            Item.AmbienFecIni = AmbienFecIni
            Item.AmbienFecFin = AmbienFecFin
            Item.codfumigacion = codfumigacion
            Item.FumigFecIni = FumigFecIni
            Item.FumigFecFin = FumigFecFin

            Item.UsuarioMod = UsuarioActual_ID
            Item.Host = UsuarioActual_Host


            ObjUpdate.ModificarAlertaDocumentoLicencia(Item)

            CargarGrilla()

            GenerarAlerta_ScriptManager("La Alerta de Licencias del Establecimiento NOV se ha Actualizado Correctamente.")

        Catch ex As UserException
            GenerarAlerta_ScriptManager(ex.Message)
        Catch ex As Exception
            ManejarExcepcion_ScriptManager(ex, System.IO.Path.GetFileName(Request.ServerVariables("SCRIPT_NAME")))
        End Try
    End Sub

#End Region

#Region "Funciones y Procedimientos"

    Sub InicializarValores()
        CargarGrilla()
    End Sub

    Sub CargarGrilla()
        Dim Lista As New List(Of BEDocumento)
        Dim Item As New BEDocumento()

        Lista = New BLDocumento().ListarAlertaDocumento("")
        ViewState("ListaAlertaDocumento") = Lista
        rgContactos.Rebind()
        lbContador.Text = rgContactos.Items.Count() & " " & "registros encontrados."
        upDatos.Update()
    End Sub


#End Region


End Class