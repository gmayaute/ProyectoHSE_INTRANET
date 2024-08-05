Imports NOVHSE.BusinessLogic
Imports NOVHSE.BusinessEntities
Imports Utiles
Imports Reutilizables
Imports Telerik.Web.UI

Public Class BuscarEmpleado
    Inherits PaginaBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.Page.IsPostBack Then
                '' Try to force IE9 into IE8 Standards mode
                Dim script As String = String.Format("enviarOk=ValidarIngresoCampos(); if(enviarOk)  {{ return MostrarAnimacionCargando('div_Cargando_01','{0}');}} return false;", btnBuscar.ClientID)
                Me.btnBuscar.Attributes.Add("OnClick", script)
                CompatibleIE8()
                InicializarControles()
            End If
        Catch ex As UserException
            GenerarAlerta_ScriptManager(ex.Message)
        Catch ex As Exception
            ManejarExcepcion_ScriptManager(ex, System.IO.Path.GetFileName(Request.ServerVariables("SCRIPT_NAME")))
        End Try
    End Sub

   

    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click

        Try
            If (ValidarIngreso()) Then
                Dim listaempleado As New GridItemCollection
                Dim CadEmpRQ As String
                listaempleado = rgMantenimiento.SelectedItems
                'rgMantenimiento.DataSource = New BLEmpleadoRequisito().ListarEmpleadoPorFiltro(ddlTipoBusqueda.SelectedValue, IIf(IsNothing(ddlTipoDocumento.SelectedValue), "", ddlTipoDocumento.SelectedValue), LTrim(RTrim(txtBusqueda.Text)))
                TablaDatos = New BLEmpleadoRequisito().ListarEmpleadoPorFiltro(ddlTipoBusqueda.SelectedValue, IIf(IsNothing(ddlTipoDocumento.SelectedValue), "", ddlTipoDocumento.SelectedValue), LTrim(RTrim(txtBusqueda.Text)))


                If listaempleado.Count() > 0 Then
                    CadEmpRQ = ""
                    For Each ItemEmpleado As GridDataItem In listaempleado

                        Dim CoEmp As String = ItemEmpleado("codigo").Text
                        Dim dataRows As DataRow()

                        dataRows = TablaDatos.Select("codigo = '" & CoEmp & "'")

                        If dataRows.Length = 0 Then
                            Dim row As DataRow = TablaDatos.NewRow()
                            row("Nombre") = ItemEmpleado("Nombre").Text
                            row("ApellidoPaterno") = ItemEmpleado("ApellidoPaterno").Text
                            row("ApellidoMaterno") = ItemEmpleado("ApellidoMaterno").Text
                            row("numdocide") = ItemEmpleado("numdocide").Text
                            row("codigo") = CoEmp
                            TablaDatos.Rows.Add(row)
                        End If

                    Next

                End If

                rgMantenimiento.DataSource = TablaDatos
                rgMantenimiento.DataBind()


                If listaempleado.Count() > 0 Then
                    For Each Item As GridDataItem In listaempleado
                        Dim codigo As String = Item("codigo").Text

                        Dim filaempleado As GridDataItem = rgMantenimiento.MasterTableView.FindItemByKeyValue("codigo", codigo)
                        filaempleado.Selected = True
                    Next
                End If

                Me.lbContador.Text = rgMantenimiento.Items.Count() & " " & "registros encontrados."
                upDatos.Update()

            End If

            
        Catch ex As UserException
            GenerarAlerta_ScriptManager(ex.Message)
        Catch ex As Exception
            ManejarExcepcion_ScriptManager(ex, System.IO.Path.GetFileName(Request.ServerVariables("SCRIPT_NAME")))
        End Try
    End Sub

    Function ValidarIngreso() As Boolean

        If String.IsNullOrEmpty(txtBusqueda.Text) Then Throw New UserException("Ingrese criterio de busqueda.")
        Return True
    End Function

    Sub InicializarControles()
        Utiles.EnlazarComboBox(ddlTipoBusqueda, New BLParametro().BuscarParametroPorDenominacionUnica("FiltroEmpleado", "", "A"), "valor", "Denominacion")

        Utiles.EnlazarComboBox(ddlTipoDocumento, New BLParametro().BuscarParametroPorDenominacionUnica("TipoDocumentoEmpleado", "", "A"), "Valor", "Denominacion")

        ddlTipoBusqueda.SelectedIndex = ddlTipoBusqueda.Items.IndexOf(ddlTipoBusqueda.Items.FindByValue("1"))

        Dim ValorParametroDNI As String = New BLParametro().BuscarParametroPorDenominacionUnica("FiltroEmpleado", "FiltroEmpleado_Documento", "").Item(0).Valor

        ddlTipoDocumento.Visible = IIf(ddlTipoBusqueda.SelectedValue = ValorParametroDNI, True, False)

        ViewState("ValorParametroDNI") = ValorParametroDNI

    End Sub

    Protected Sub ddlTipoBusqueda_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTipoBusqueda.SelectedIndexChanged

        Try

            ddlTipoDocumento.Visible = IIf(ddlTipoBusqueda.SelectedValue = ViewState("ValorParametroDNI"), True, False)
            txtBusqueda.Text = String.Empty

            upCriterios.Update()
        Catch ex As UserException
            GenerarAlerta_ScriptManager(ex.Message)
        Catch ex As Exception
            ManejarExcepcion_ScriptManager(ex, System.IO.Path.GetFileName(Request.ServerVariables("SCRIPT_NAME")))
        End Try


    End Sub

    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click

        Try
            Dim Concatenar As String
            Dim ListaCodigos As New List(Of String)

            For Each item As GridDataItem In rgMantenimiento.Items
                If item.Selected = True Then
                    ListaCodigos.Add(item("codigo").Text)
                End If
            Next

            ' Use string join function that receives IEnumerable.
            Concatenar = String.Join(",", ListaCodigos)
            Session.Add("CodEmpleado", Concatenar)

            'Cerramos ventana.
            ScriptManager.RegisterStartupScript(Me.Page, Page.GetType(), "Cargar", "closeWin();", True)

        Catch ex As UserException
            GenerarAlerta_ScriptManager(ex.Message)
        Catch ex As Exception
            ManejarExcepcion_ScriptManager(ex, System.IO.Path.GetFileName(Request.ServerVariables("SCRIPT_NAME")))
        End Try

    End Sub

    'Private Sub CloseWindow()
    '    Dim sb As New StringBuilder
    '    'sb.Append("window.opener.document.form1.submit();")
    '    sb.Append("window.close();")
    '    ScriptManager.RegisterStartupScript(Me, Me.GetType, "CloseWindowScript", sb.ToString(), True)
    'End Sub

End Class