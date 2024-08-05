
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Net.Mail
Imports System.Net.Mime
Imports System.Security.Cryptography
Imports System.IO
Imports System.Text
Imports Microsoft.Win32
Imports NOVHSE.BusinessEntities
Imports System.Collections.Generic
Imports System.Configuration.ConfigurationSettings
Imports System.Net
Imports Telerik.Web.UI
Imports System.Web.UI



Public Class Utiles
    Inherits PaginaBase


    Public Shared Function ConvertirFechaDateADecimal(ByVal Fecha As String) As Decimal

        Dim Resultado As Decimal

        If IsDate(Fecha) Then
            Dim Dia As String = Right("0" & (Convert.ToString(Convert.ToDateTime(Fecha).Day)), 2)
            Dim Mes As String = Right("0" & Convert.ToString(Convert.ToDateTime(Fecha).Month), 2)

            Resultado = Convert.ToDecimal(Convert.ToDateTime(Fecha).Year & Mes & Dia)
        Else
            Resultado = 0
        End If

        Return Resultado

    End Function

    Public Shared Function GenerarURL_Menu(ByVal Direccion As String) As String
        GenerarURL_Menu = String.Format(Direccion)
        Return GenerarURL_Menu
    End Function


    'Obtiene el codigo del sistema 
    Public Shared Function ObtenerModuloSistema() As String
        Return ConfigurationSettings.AppSettings("CodigoModulo")
    End Function


    Public Shared Function ObtenerDominio() As String
        Return ConfigurationSettings.AppSettings("Domain")
    End Function



    Public Shared Function ObtenerLDAP() As String
        Return ConfigurationSettings.AppSettings("LDAP")
    End Function







    ''' <summary>
    ''' Permite Cargar datos en el combo.
    ''' </summary>
    ''' <param name="combo">Indica el combo para la carga de datos.</param>
    ''' <param name="DataSource">Indica colleccion de datos en DataTable.</param>
    ''' <param name="ValueField">Indica el campo que se visualizara en en Combo.</param>
    ''' <param name="TextField">Indica el campo que se establecerá en value</param>
    ''' <remarks></remarks>
    Public Shared Sub EnlazarComboBox(ByVal combo As DropDownList, ByVal DataSource As Object, ByVal ValueField As String, ByVal TextField As String)
        combo.DataSource = DataSource
        combo.DataValueField = ValueField
        combo.DataTextField = TextField
        combo.DataBind()
    End Sub

    Public Shared Sub EnlazarRadComboBox(ByVal combo As RadComboBox, ByVal DataSource As Object, ByVal ValueField As String, ByVal TextField As String)
        combo.DataSource = DataSource
        combo.DataValueField = ValueField
        combo.DataTextField = TextField
        combo.DataBind()
    End Sub



    Public Shared Sub EnlazarRadComboBox(ByVal combo As RadComboBox, ByVal DataSource As Object, ByVal ValueField As String, ByVal TextField As String, ByVal primerItem As RadComboBoxItem)
        If Not DataSource Is Nothing Then
            combo.DataSource = DataSource
            combo.DataValueField = ValueField
            combo.DataTextField = TextField
            combo.DataBind()
            combo.Items.Insert(0, primerItem)
        Else
            combo.Items.Clear()
            combo.Items.Insert(0, primerItem)
        End If
    End Sub






    ''' <summary>
    ''' Permite Cargar datos en el combo.
    ''' </summary>
    ''' <param name="combo">Indica el combo para la carga de datos.</param>
    ''' <param name="DataSource">Indica colleccion de datos en DataTable.</param>
    ''' <param name="ValueField">Indica el campo que se visualizara en en Combo.</param>
    ''' <param name="TextField">Indica el campo que se establecerá en value</param>
    ''' <param name="primerItem">Indica el primer campo que se establecerá</param>
    ''' <remarks></remarks>
    Public Shared Sub EnlazarComboBox(ByVal combo As DropDownList, ByVal DataSource As Object, ByVal ValueField As String, ByVal TextField As String, ByVal primerItem As ListItem)
        If Not DataSource Is Nothing Then
            combo.DataSource = DataSource
            combo.DataValueField = ValueField
            combo.DataTextField = TextField
            combo.DataBind()
            combo.Items.Insert(0, primerItem)
        Else
            combo.Items.Clear()
            combo.Items.Insert(0, primerItem)
        End If
    End Sub


    ''' <summary>
    ''' Funcion que permite obtener la fecha actual en formato dd/mm/aaaa
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ObtenerFechaActual() As String
        Dim dia As String = ""
        Dim mes As String = ""
        Dim anio As String = ""
        Dim fecha As String = ""
        dia = IIf(Date.Today.Day.ToString < 10, "0" & Date.Today.Day.ToString, Date.Today.Day.ToString)
        mes = IIf(Date.Today.Month.ToString < 10, "0" & Date.Today.Month.ToString, Date.Today.Month.ToString)
        anio = IIf(Date.Today.Year.ToString < 10, "0" & Date.Today.Year.ToString, Date.Today.Year.ToString)
        fecha = dia & "/" & mes & "/" & anio
        Return fecha
    End Function




    ''' <summary>
    ''' Funcion para CONVERTIR FECHA tipo DECIMAL a FECHA tipo CADENA
    ''' </summary>
    ''' <param name="fecha"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ConvertirFechaString(ByVal fecha As Decimal) As String
        If fecha <> 0 Then
            Return fecha.ToString().Substring(6, 2) + "/" + fecha.ToString().Substring(4, 2) + "/" + fecha.ToString().Substring(0, 4)
        Else
            Return String.Empty
        End If
    End Function


    ''' <summary>
    ''' Habilita o deshabilita un ImageButon. Si se intenta deshabilitar el control, este tomará un aspecto opaco.
    ''' </summary>
    ''' <param name="btn"></param>
    Public Shared Sub Habilitar_Deshabilitar_Button(ByVal btn As ImageButton, ByVal enableEstate As Boolean)
        If enableEstate Then
            btn.Enabled = True
            btn.Style.Add("filter", "alpha(opacity=100)")
            btn.Style.Add("cursor", "hand")
        Else
            btn.Enabled = False
            btn.Style.Add("filter", "alpha(opacity=30)")
            btn.Style.Add("cursor", "default")
        End If
    End Sub

    ''' <summary>
    ''' Habilita o deshabilita un Buton. Si se intenta deshabilitar el control, este tomará un aspecto opaco.
    ''' </summary>
    ''' <param name="btn"></param>
    Public Shared Sub Habilitar_Deshabilitar_Button(ByVal btn As Button, ByVal enableEstate As Boolean)
        If enableEstate Then
            btn.Enabled = True
            btn.Style.Add("filter", "alpha(opacity=100)")
            btn.Style.Add("cursor", "hand")
        Else
            btn.Enabled = False
            btn.Style.Add("filter", "alpha(opacity=40)")
            btn.Style.Add("cursor", "default")
        End If
    End Sub


    Public Shared Sub EstablecerValidaciones(ByRef oGridEditableItem As GridEditableItem, ByVal FieldName As String, ByVal MaxLength As Integer, ByVal Requerido As Boolean, ByVal Tipo As String)
        Dim oGridTextBoxColumnEditor As GridTextBoxColumnEditor = oGridEditableItem.EditManager.GetColumnEditor(FieldName)
        Dim oRequiredFieldValidator As New RequiredFieldValidator()

        oGridTextBoxColumnEditor.TextBoxControl.MaxLength = MaxLength

        If Requerido = True Then

            If Tipo = TipoValidacion.entero Then
                oGridTextBoxColumnEditor.TextBoxControl.Attributes.Add("onkeypress", "ValidaEntero()")
            ElseIf Tipo = TipoValidacion.Fecha Then
            ElseIf Tipo = TipoValidacion.Numerico Then
            ElseIf Tipo = TipoValidacion.Texto Then

            End If

        End If

    End Sub

    Public Shared Sub ValidacionGridBoundColumn_Multiline(ByRef oGridEditableItem As GridEditableItem, ByVal FieldName As String, ByVal MaxLength As Integer, ByVal Requerido As Boolean, ByVal Tipo As String)


        Dim oGridTextBoxColumnEditor As GridTextBoxColumnEditor = oGridEditableItem.EditManager.GetColumnEditor(FieldName)
        Dim oRequiredFieldValidator As New RequiredFieldValidator()


        oGridTextBoxColumnEditor.TextBoxControl.ID = "tb" + FieldName.ToLower()
        oGridTextBoxColumnEditor.TextBoxControl.MaxLength = MaxLength
        oGridTextBoxColumnEditor.TextBoxControl.TextMode = TextBoxMode.MultiLine
        oGridTextBoxColumnEditor.TextBoxControl.CssClass = "stlCajaTexto"
        oGridTextBoxColumnEditor.TextBoxControl.Width = 600

        If Requerido = True Then

            oRequiredFieldValidator.ControlToValidate = oGridTextBoxColumnEditor.TextBoxControl.ID
            oRequiredFieldValidator.Text = "*"
            CType(oGridTextBoxColumnEditor.TextBoxControl.Parent, TableCell).Controls.Add(oRequiredFieldValidator)

            If Tipo = TipoValidacion.entero Then
                oGridTextBoxColumnEditor.TextBoxControl.Attributes.Add("onkeypress", "ValidaEntero()")
            ElseIf Tipo = TipoValidacion.Numerico Then
                oGridTextBoxColumnEditor.TextBoxControl.Attributes.Add("onkeypress", "OnKeyPressTextNumeroPunto('" & oGridTextBoxColumnEditor.TextBoxControl.ClientID & "')")
            ElseIf Tipo = TipoValidacion.Texto Then

            End If

        End If

    End Sub


    Public Shared Sub ValidacionGridBoundColumn(ByRef oGridEditableItem As GridEditableItem, ByVal FieldName As String, ByVal MaxLength As Integer, ByVal Requerido As Boolean, ByVal Tipo As String, ByVal Tamano As String)
        Dim oGridTextBoxColumnEditor As GridTextBoxColumnEditor = oGridEditableItem.EditManager.GetColumnEditor(FieldName)
        Dim oRequiredFieldValidator As New RequiredFieldValidator()


        oGridTextBoxColumnEditor.TextBoxControl.ID = "tb" + FieldName.ToLower()
        oGridTextBoxColumnEditor.TextBoxControl.MaxLength = MaxLength
        oGridTextBoxColumnEditor.TextBoxControl.TextMode = TextBoxMode.SingleLine
        oGridTextBoxColumnEditor.TextBoxControl.CssClass = "stlCajaTexto"
        oGridTextBoxColumnEditor.TextBoxControl.Width = Tamano

        If Requerido = True Then

            oRequiredFieldValidator.ControlToValidate = oGridTextBoxColumnEditor.TextBoxControl.ID
            oRequiredFieldValidator.Text = "*"
            CType(oGridTextBoxColumnEditor.TextBoxControl.Parent, TableCell).Controls.Add(oRequiredFieldValidator)

            If Tipo = TipoValidacion.entero Then
                oGridTextBoxColumnEditor.TextBoxControl.Attributes.Add("onkeypress", "ValidaEntero()")
            ElseIf Tipo = TipoValidacion.Numerico Then
                oGridTextBoxColumnEditor.TextBoxControl.Attributes.Add("onkeypress", "OnKeyPressTextNumeroPunto('" & oGridTextBoxColumnEditor.TextBoxControl.ClientID & "')")
            ElseIf Tipo = TipoValidacion.Texto Then

            End If

        End If

    End Sub


#Region "funciones Datatable"


    Public Shared Function SelectDistinct(ByVal SourceTable As DataTable, ByVal ParamArray FieldNames() As String) As DataTable
        Dim lastValues() As Object
        Dim newTable As DataTable

        If FieldNames Is Nothing OrElse FieldNames.Length = 0 Then
            Throw New ArgumentNullException("FieldNames")
        End If

        lastValues = New Object(FieldNames.Length - 1) {}
        newTable = New DataTable

        For Each field As String In FieldNames
            newTable.Columns.Add(field, SourceTable.Columns(field).DataType)
        Next

        For Each Row As DataRow In SourceTable.Select("", String.Join(", ", FieldNames))
            If Not fieldValuesAreEqual(lastValues, Row, FieldNames) Then
                newTable.Rows.Add(createRowClone(Row, newTable.NewRow(), FieldNames))

                setLastValues(lastValues, Row, FieldNames)
            End If
        Next

        Return newTable
    End Function

    Private Shared Function fieldValuesAreEqual(ByVal lastValues() As Object, ByVal currentRow As DataRow, ByVal fieldNames() As String) As Boolean
        Dim areEqual As Boolean = True

        For i As Integer = 0 To fieldNames.Length - 1
            If lastValues(i) Is Nothing OrElse Not lastValues(i).Equals(currentRow(fieldNames(i))) Then
                areEqual = False
                Exit For
            End If
        Next

        Return areEqual
    End Function

    Private Shared Function createRowClone(ByVal sourceRow As DataRow, ByVal newRow As DataRow, ByVal fieldNames() As String) As DataRow
        For Each field As String In fieldNames
            newRow(field) = sourceRow(field)
        Next

        Return newRow
    End Function

    Private Shared Sub setLastValues(ByVal lastValues() As Object, ByVal sourceRow As DataRow, ByVal fieldNames() As String)
        For i As Integer = 0 To fieldNames.Length - 1
            lastValues(i) = sourceRow(fieldNames(i))
        Next
    End Sub


#End Region


End Class

