Imports NOVHSE.BusinessLogic
Imports NOVHSE.BusinessEntities
Imports NOVDOC.Utiles
Imports Reutilizables
Imports System.Web.UI.WebControls
Imports Telerik.Web.UI

Public Class AlertaSuministro
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
                Utiles.ValidacionGridBoundColumn(oGridEditableItem, "ccosto", 50, True, TipoValidacion.Texto, "100")
                Utiles.ValidacionGridBoundColumn(oGridEditableItem, "arrend", 50, False, TipoValidacion.Texto, "100")
                Utiles.ValidacionGridBoundColumn(oGridEditableItem, "fr1", 50, False, TipoValidacion.Texto, "50")
                Utiles.ValidacionGridBoundColumn(oGridEditableItem, "fr2", 50, False, TipoValidacion.Texto, "50")
                Utiles.ValidacionGridBoundColumn(oGridEditableItem, "fr3", 50, False, TipoValidacion.Texto, "50")
                Utiles.ValidacionGridBoundColumn(oGridEditableItem, "estatus", 50, False, TipoValidacion.Texto, "50")
                Utiles.ValidacionGridBoundColumn(oGridEditableItem, "caprob", 50, False, TipoValidacion.Texto, "50")
                Utiles.ValidacionGridBoundColumn(oGridEditableItem, "abogcargo", 50, False, TipoValidacion.Texto, "100")
                Utiles.ValidacionGridBoundColumn(oGridEditableItem, "ContFecIni", 10, False, TipoValidacion.Texto, "60")
                Utiles.ValidacionGridBoundColumn(oGridEditableItem, "ContFecFin", 10, False, TipoValidacion.Texto, "60")
                Utiles.ValidacionGridBoundColumn(oGridEditableItem, "ordencompra", 50, False, TipoValidacion.Texto, "100")
                Utiles.ValidacionGridBoundColumn(oGridEditableItem, "contacto", 50, False, TipoValidacion.Texto, "100")
                Utiles.ValidacionGridBoundColumn(oGridEditableItem, "telcontacto", 50, False, TipoValidacion.Texto, "100")
              
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
            Dim ccosto As String = String.Empty
            Dim arrend As String = String.Empty
            Dim fr1 As String = String.Empty
            Dim fr2 As String = String.Empty
            Dim fr3 As String = String.Empty
            Dim estatus As String = String.Empty
            Dim caprob As String = String.Empty
            Dim abogcargo As String = String.Empty
            Dim ContFecIni As String = String.Empty
            Dim ContFecFin As String = String.Empty
            Dim ordencompra As String = String.Empty
            Dim contacto As String = String.Empty
            Dim telcontacto As String = String.Empty
           

            nombre = CType(editedItem.EditManager.GetColumnEditor("nombre"), GridTextColumnEditor).Text
            direccion = CType(editedItem.EditManager.GetColumnEditor("direccion"), GridTextColumnEditor).Text
            ccosto = CType(editedItem.EditManager.GetColumnEditor("ccosto"), GridTextColumnEditor).Text
            arrend = CType(editedItem.EditManager.GetColumnEditor("arrend"), GridTextColumnEditor).Text
            fr1 = CType(editedItem.EditManager.GetColumnEditor("fr1"), GridTextColumnEditor).Text
            fr2 = CType(editedItem.EditManager.GetColumnEditor("fr2"), GridTextColumnEditor).Text
            fr3 = CType(editedItem.EditManager.GetColumnEditor("fr3"), GridTextColumnEditor).Text
            estatus = CType(editedItem.EditManager.GetColumnEditor("estatus"), GridTextColumnEditor).Text
            caprob = CType(editedItem.EditManager.GetColumnEditor("caprob"), GridTextColumnEditor).Text
            abogcargo = CType(editedItem.EditManager.GetColumnEditor("abogcargo"), GridTextColumnEditor).Text
            ContFecIni = CType(editedItem.EditManager.GetColumnEditor("ContFecIni"), GridTextColumnEditor).Text
            ContFecFin = CType(editedItem.EditManager.GetColumnEditor("ContFecFin"), GridTextColumnEditor).Text
            ordencompra = CType(editedItem.EditManager.GetColumnEditor("ordencompra"), GridTextColumnEditor).Text
            contacto = CType(editedItem.EditManager.GetColumnEditor("contacto"), GridTextColumnEditor).Text
            telcontacto = CType(editedItem.EditManager.GetColumnEditor("telcontacto"), GridTextColumnEditor).Text
            
            If String.IsNullOrEmpty(nombre) Then Throw New UserException("El nombre es obligatorio.")
            If String.IsNullOrEmpty(direccion) Then Throw New UserException("La dirección es obligatorio.")
            If String.IsNullOrEmpty(ccosto) Then Throw New UserException("El CCosto es obligatorio.")

            Item.Id = Parametro(0)
            Item.nombre = nombre
            Item.direccion = direccion
            Item.ccosto = ccosto
            Item.arrend = arrend
            Item.fr1 = fr1
            Item.fr2 = fr2
            Item.fr3 = fr3
            Item.estatus = estatus
            Item.caprob = caprob
            Item.abogcargo = abogcargo
            Item.ContFecIni = ContFecIni
            Item.ContFecFin = ContFecFin
            Item.ordencompra = ordencompra
            Item.contacto = contacto
            Item.telcontacto = telcontacto
          
            Item.UsuarioMod = UsuarioActual_ID
            Item.Host = UsuarioActual_Host


            ObjUpdate.ModificarAlertaDocumento(Item)

            CargarGrilla()

            GenerarAlerta_ScriptManager("La Alerta del Establecimiento NOV se ha Actualizado Correctamente.")

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

            'Campos Obligatorios
            Dim nombre As String = String.Empty
            Dim direccion As String = String.Empty
            Dim ccosto As String = String.Empty
            Dim arrend As String = String.Empty
            Dim fr1 As String = String.Empty
            Dim fr2 As String = String.Empty
            Dim fr3 As String = String.Empty
            Dim estatus As String = String.Empty
            Dim caprob As String = String.Empty
            Dim abogcargo As String = String.Empty
            Dim ContFecIni As String = String.Empty
            Dim ContFecFin As String = String.Empty
            Dim ordencompra As String = String.Empty
            Dim contacto As String = String.Empty
            Dim telcontacto As String = String.Empty
           

            nombre = CType(editedItem.EditManager.GetColumnEditor("nombre"), GridTextColumnEditor).Text
            direccion = CType(editedItem.EditManager.GetColumnEditor("direccion"), GridTextColumnEditor).Text
            ccosto = CType(editedItem.EditManager.GetColumnEditor("ccosto"), GridTextColumnEditor).Text
            arrend = CType(editedItem.EditManager.GetColumnEditor("arrend"), GridTextColumnEditor).Text
            fr1 = CType(editedItem.EditManager.GetColumnEditor("fr1"), GridTextColumnEditor).Text
            fr2 = CType(editedItem.EditManager.GetColumnEditor("fr2"), GridTextColumnEditor).Text
            fr3 = CType(editedItem.EditManager.GetColumnEditor("fr3"), GridTextColumnEditor).Text
            estatus = CType(editedItem.EditManager.GetColumnEditor("estatus"), GridTextColumnEditor).Text
            caprob = CType(editedItem.EditManager.GetColumnEditor("caprob"), GridTextColumnEditor).Text
            abogcargo = CType(editedItem.EditManager.GetColumnEditor("abogcargo"), GridTextColumnEditor).Text
            ContFecIni = CType(editedItem.EditManager.GetColumnEditor("ContFecIni"), GridTextColumnEditor).Text
            ContFecFin = CType(editedItem.EditManager.GetColumnEditor("ContFecFin"), GridTextColumnEditor).Text
            ordencompra = CType(editedItem.EditManager.GetColumnEditor("ordencompra"), GridTextColumnEditor).Text
            contacto = CType(editedItem.EditManager.GetColumnEditor("contacto"), GridTextColumnEditor).Text
            telcontacto = CType(editedItem.EditManager.GetColumnEditor("telcontacto"), GridTextColumnEditor).Text
           
            If String.IsNullOrEmpty(nombre) Then Throw New UserException("El nombre es obligatorio.")
            If String.IsNullOrEmpty(direccion) Then Throw New UserException("La dirección es obligatorio.")
            If String.IsNullOrEmpty(ccosto) Then Throw New UserException("El CCosto es obligatorio.")

            Item.nombre = nombre
            Item.direccion = direccion
            Item.ccosto = ccosto
            Item.arrend = arrend
            Item.fr1 = fr1
            Item.fr2 = fr2
            Item.fr3 = fr3
            Item.estatus = estatus
            Item.caprob = caprob
            Item.abogcargo = abogcargo
            Item.ContFecIni = ContFecIni
            Item.ContFecFin = ContFecFin
            Item.ordencompra = ordencompra
            Item.contacto = contacto
            Item.telcontacto = telcontacto
            Item.UsuarioReg = UsuarioActual_ID
            Item.Host = UsuarioActual_Host
            
            ObjInsert.InsertarAlertaDocumento(Item)

            GenerarAlerta_ScriptManager("La Alerta del Establecimiento NOV se ha Agregado Correctamente.")
            CargarGrilla()

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