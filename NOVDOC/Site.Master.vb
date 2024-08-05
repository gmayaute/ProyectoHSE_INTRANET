Imports NOVHSE.BusinessLogic
Imports NOVHSE.BusinessEntities
Imports System.Web.UI.WebControls
Imports Telerik.Web.UI
' Imports Reutilizables


Partial Public Class Site
    Inherits System.Web.UI.MasterPage
    Dim ValorNodo As Integer
    Dim CodModulo As String
    Dim CodUsuario As String
    Dim ListMenu As New List(Of BEMenu)
    Private dataSetArbol As DataSet

#Region "Llena Menu Principal"

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                CargarCabecera()
                CargaMenus()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub CrearNodosDelPadre(ByVal indicePadre As Integer, ByVal nodePadre As Telerik.Web.UI.RadTreeNode)
        Dim dataViewHijos As DataView
        dataViewHijos = New DataView(dataSetArbol.Tables("TablaArbol"))
        dataViewHijos.RowFilter = dataSetArbol.Tables("TablaArbol").Columns("IdentificadorPadre").ColumnName + " = " + indicePadre.ToString()

        For Each dataRowCurrent As DataRowView In dataViewHijos
            Dim nuevoNodo As New Telerik.Web.UI.RadTreeNode
            nuevoNodo.Text = dataRowCurrent("NombreNodo").ToString().Trim()
            nuevoNodo.Value = dataRowCurrent("IdentificadorNodo").ToString().Trim()

            If Session("NodoSeleccionado") <> "" Then
                If nuevoNodo.Value = Session("NodoSeleccionado") Then
                    nuevoNodo.Selected = True
                End If
            End If

            If nodePadre Is Nothing Then
                RadTreeView.Nodes.Add(nuevoNodo)
            Else
                nodePadre.Nodes.Add(nuevoNodo)
            End If

            CrearNodosDelPadre(Int32.Parse(dataRowCurrent("IdentificadorNodo").ToString()), nuevoNodo)
        Next dataRowCurrent
    End Sub

    Private Sub InsertarDataRow(ByVal column1 As String, ByVal column2 As Integer, ByVal column3 As Integer)
        Dim nuevaFila As DataRow
        nuevaFila = dataSetArbol.Tables("TablaArbol").NewRow()
        nuevaFila("NombreNodo") = column1
        nuevaFila("IdentificadorNodo") = column2
        nuevaFila("IdentificadorPadre") = column3
        dataSetArbol.Tables("TablaArbol").Rows.Add(nuevaFila)
    End Sub

    Private Sub CargaMenus()
        RadTreeView.Nodes.Clear()
        CrearDataSet()
        CrearNodosDelPadre(0, Nothing)
        RadTreeView.SelectedNode.ExpandParentNodes()
        RadTreeView.SelectedNode.Expanded = True
    End Sub


    Private Sub RadTreeView_NodeClick(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadTreeNodeEventArgs) Handles RadTreeView.NodeClick

        RadTreeView.LoadingStatusPosition = DirectCast([Enum].Parse(GetType(TreeViewLoadingStatusPosition), "AfterNodeText"), TreeViewLoadingStatusPosition)

        Session("NodoSeleccionado") = RadTreeView.SelectedNode.Value

        If RadTreeView.SelectedNode.Nodes.Count = 0 Then
            Call EjecutarMenu()
        Else
            Response.Redirect("~/Inicio.aspx")
        End If

    End Sub



#End Region

    Private Sub CrearDataSet()
        CodModulo = Utiles.ObtenerModuloSistema
        CodUsuario = Session("CodUsuario")
        ListMenu = New BLMenu().BuscarMenuPorUsuarioModuloNovPeru(CodModulo, CodUsuario)

        If ListMenu.Count > 0 Then
            Dim tablaArbol As DataTable
            dataSetArbol = New DataSet("DataSetArbol")
            tablaArbol = dataSetArbol.Tables.Add("TablaArbol")
            tablaArbol.Columns.Add("NombreNodo", GetType(String))
            tablaArbol.Columns.Add("IdentificadorNodo", GetType(Integer))
            tablaArbol.Columns.Add("IdentificadorPadre", GetType(Integer))
            For Each dr As BEMenu In ListMenu
                InsertarDataRow(dr.Nombre, dr.Id_Menu, dr.IdPadre)
            Next
        Else
            Response.Redirect("~/Login.aspx?msg=NE")
        End If

    End Sub

    Protected Sub EjecutarMenu()
        Dim cTextoNodo As String
        Dim cTextoUrl As String
        cTextoNodo = RadTreeView.SelectedNode.Value
        CodModulo = Utiles.ObtenerModuloSistema
        CodUsuario = Session("CodUsuario")
        ListMenu = New BLMenu().BuscarMenuPorUsuarioModuloNovPeru(CodModulo, CodUsuario)

        cTextoUrl = "~/Inicio.aspx"

        For Each dr As BEMenu In ListMenu
            If dr.Id_Menu = cTextoNodo Then
                cTextoUrl = Utiles.GenerarURL_Menu(dr.DireccionURL)
                Exit For
            End If
        Next
        Response.Redirect(cTextoUrl)


        'Dim cTextoNodo As String
        'cTextoNodo = RadTreeView.SelectedNode.Text
        'Select cTextoNodo
        '    Case "Proforma"
        '        Response.Redirect("/GestionFact/Proformas.aspx")

        '    Case "Consideraciones Generales"
        '        Response.Redirect("manual_ayuda.aspx")

        '    Case Else
        '        Response.Redirect("inicio.aspx")
        'End Select


    End Sub

    Protected Sub ImgSalir_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgSalir.Click
        Try
            'Eliminamos Sesiones de servidor

            Session.RemoveAll()

            Response.Redirect("~/EndSession.aspx", False)

        Catch ex As Exception
          
        End Try
    End Sub

    Private Sub CargarCabecera()
        'colocamos la hora actual.
        lblUsuario.Text = "Welcome: " + Session("NomUsuario")
        lblFecha.Text = Date.Today.Year.ToString

    End Sub


    Protected Sub ImgInformacion_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgInformacion.Click
        Response.Redirect("~/informacion.aspx", False)
    End Sub

    Protected Sub ImgMenu_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgMenu.Click
        Response.Redirect("~/Inicio.aspx", False)
    End Sub


End Class