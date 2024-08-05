Imports NOVHSE.BusinessLogic
Imports NOVHSE.BusinessEntities
Imports NOVDOC.Utiles
Imports Reutilizables
Imports System.Web.UI.WebControls
Imports Telerik.Web.UI

Public Class MantEmail
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

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBuscar.Click
        Try
            CargarGrilla()
        Catch ex As UserException
            GenerarAlerta_ScriptManager(ex.Message)
        Catch ex As Exception
            ManejarExcepcion_ScriptManager(ex, System.IO.Path.GetFileName(Request.ServerVariables("SCRIPT_NAME")))
        End Try
    End Sub

    Protected Sub rgContactos_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgContactos.NeedDataSource
        Try
            rgContactos.DataSource = ViewState("ListaEmailAlertaDocumento")
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

                Utiles.ValidacionGridBoundColumn(oGridEditableItem, "nomfunc", 300, True, TipoValidacion.Texto, "10")
                Utiles.ValidacionGridBoundColumn(oGridEditableItem, "empfunc", 300, True, TipoValidacion.Texto, "10")
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
            Dim nomemail As String = String.Empty
            Dim corremail As String = String.Empty
            
            nomemail = CType(editedItem.EditManager.GetColumnEditor("nomfunc"), GridTextColumnEditor).Text
            corremail = CType(editedItem.EditManager.GetColumnEditor("empfunc"), GridTextColumnEditor).Text
            
            If String.IsNullOrEmpty(nomemail) Then Throw New UserException("El nombre del contacto es obligatorio.")
            If String.IsNullOrEmpty(nomemail) Then Throw New UserException("El email del contacto es obligatorio.")

            Item.Coddoc = Parametro(0)
            Item.nomfunc = nomemail
            Item.empfunc = corremail
            
            ObjUpdate.ModificarEmailAlertaDocumento(Item)

            CargarGrilla()

            GenerarAlerta_ScriptManager("El correo del contacto se ha Actualizado Correctamente.")

        Catch ex As UserException
            GenerarAlerta_ScriptManager(ex.Message)
        Catch ex As Exception
            ManejarExcepcion_ScriptManager(ex, System.IO.Path.GetFileName(Request.ServerVariables("SCRIPT_NAME")))
        End Try
    End Sub

    Protected Sub rgContactos_InsertCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles rgContactos.InsertCommand
        Try
            Dim Item As New BEDocumento
            Dim ObjInsert As New BLDocumento()

            Dim editedItem As GridEditableItem = CType(e.Item, GridEditableItem)
            Dim Parametro As String() = ViewState("ParametrosEdit")

            'Campos Obligatorios
            Dim nomemail As String = String.Empty
            Dim corremail As String = String.Empty

            nomemail = CType(editedItem.EditManager.GetColumnEditor("nomfunc"), GridTextColumnEditor).Text
            corremail = CType(editedItem.EditManager.GetColumnEditor("empfunc"), GridTextColumnEditor).Text

            If String.IsNullOrEmpty(nomemail) Then Throw New UserException("El nombre del contacto es obligatorio.")
            If String.IsNullOrEmpty(corremail) Then Throw New UserException("El email del contacto es obligatorio.")

            Item.nomfunc = nomemail
            Item.empfunc = corremail

            ObjInsert.InsertarEmailAlertaDocumento(Item)

            GenerarAlerta_ScriptManager("El correo del contacto se ha Agregado Correctamente.")
            CargarGrilla()

        Catch ex As UserException
            GenerarAlerta_ScriptManager(ex.Message)
        Catch ex As Exception
            ManejarExcepcion_ScriptManager(ex, System.IO.Path.GetFileName(Request.ServerVariables("SCRIPT_NAME")))
        End Try
    End Sub


    Sub CargarGrilla()
        Dim Lista As New List(Of BEDocumento)
        Dim Item As New BEDocumento()

        Lista = New BLDocumento().ListarEmailAlertaDocumento(RadTxtOrden.Text)
        ViewState("ListaEmailAlertaDocumento") = Lista
        rgContactos.Rebind()
        lbContador.Text = rgContactos.Items.Count() & " " & "registros encontrados."
        upDatos.Update()
    End Sub


#End Region

#Region "Funciones y Procedimientos"

    Sub InicializarValores()
        CargarGrilla()
    End Sub

#End Region


End Class