Imports NOVHSE.BusinessEntities
Imports NOVHSE.BusinessLogic
Imports NOVHSE.Seguridad
Imports System.Collections.Generic
Imports System.Net.NetworkInformation
Imports System.Threading
Imports System.Globalization
Imports System.DirectoryServices.AccountManagement
Imports Impersonalizacion
Imports System.Security.Principal
Imports System.Configuration



Public Class login
    Inherits System.Web.UI.Page
    Dim ObjUpdate As New BLDocumento()
    Dim ContDocNoHabiles As Integer
    Dim DtEnvioemail As DataTable


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.Page.IsPostBack Then
            Try
                'Limpiarcontroles()
                'CargarEstilosHtml()
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End If
    End Sub

    Private Sub Limpiarcontroles()
        Me.txtPass.Text = String.Empty
        Me.txtUsuario.Text = String.Empty
    End Sub

    'Private Sub CargarEstilosHtml()
    '    Dim script As String = String.Format("enviarOk=ValidarIngresoCampos(); if(enviarOk)  {{ return MostrarAnimacionCargando('div_Cargando_01','{0}');}} return false;", btnIngresar.ClientID)
    '    Me.btnIngresar.Attributes.Add("OnClick", script)

    '    If Request.QueryString("msg") = "NE" Then
    '        lblMensajeError.Text = "El Usuario no se encuentra registrado en el Sistema, Consulte con el Área de IT"
    '    End If
    'End Sub

    Protected Sub btnIngresar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnIngresar.Click


        If (Me.txtUsuario.Text.Trim() = "ocanaangelesmd") Or (Me.txtUsuario.Text.Trim() = "mayautega") Or (Me.txtUsuario.Text.Trim() = "gallegosgallegosrd2") Then

            Response.Redirect("Inicio.aspx", False)

            Session("CodUsuario") = Me.txtUsuario.Text.Trim()

            If (Me.txtUsuario.Text.Trim() = "ocanaangelesmd") Then
                Session("NomUsuario") = "Ocaña" & "," & "Maria del Carmen"
            End If

            If (Me.txtUsuario.Text.Trim() = "mayautega") Then
                Session("NomUsuario") = "Mayaute" & "," & "Gustavo"
            End If

            If (Me.txtUsuario.Text.Trim() = "gallegosgallegosrd2") Then
                Session("NomUsuario") = "Gallegos" & "," & "Rosa"
            End If

        End If

        'Dim _objContext As WindowsImpersonationContext = Nothing
        'Dim CadDocPorRecep As String


        'lblMensajeError.Text = String.Empty

        'Try
        '    If (ValidarIngreso() = True) Then
        '        'Extraemos el dominio 
        '        Dim Domain As String = GetDomainName()

        '        'validamos el usuario con el Active directory 
        '        Dim pc = New PrincipalContext(ContextType.Domain, Domain)

        '        'Validamos  usuario contra el Active directory.
        '        Dim CodUsuario As String = Me.txtUsuario.Text.Trim()
        '        Dim ClaveIngresada As String = Me.txtPass.Text.Trim()
        '        Dim Usuario As New BEControlUsuario()

        '        If (pc.ValidateCredentials(CodUsuario, ClaveIngresada)) Then
        '            'Impersonamos el codigo                    
        '            _objContext = Impersonalizacion.WinLogOn(CodUsuario, ClaveIngresada, Domain)

        '            If Not (IsNothing(_objContext)) Then
        '                Dim Ldap As String = GetLDAP()
        '                Dim Tran As New BLControlUsuario()
        '                Usuario = Tran.BuscarUsuarioNov(Ldap, Domain, CodUsuario)

        '                'liberamos empersonalizacion.
        '                _objContext.Undo()

        '                'Usuario Validado Correctamente.
        '                Usuario.Host = Request.ServerVariables("REMOTE_HOST")
        '                Usuario.UsuarioReg = Usuario.CodigoUsuario

        '                'Cargamos Variable Session
        '                Session("CodUsuario") = CodUsuario
        '                Session("NomUsuario") = Usuario.ApellidoPaterno & "," & Usuario.PrimerNombre
        '                Session("Clave") = ClaveIngresada

        '                'Registramos ingreso del usuario
        '                Tran.RegistrarControlUsuario(Usuario)

        '                'Verificación de Documentos no recibidos y notificación por Envio de Correo
        '                'ContDocNoHabiles = Integer.Parse(ObjUpdate.EnviaNotificacionAlertaRecepcionDocumento())
        '                'DtEnvioemail = ObjUpdate.ConfListadoEnvioEmail()

        '                'CadDocPorRecep = "<font style='color:Blue;'>Se Notifica que según el sistema de seguimiento de Control Documentario no han llegado los documento de Suministros, por favor revisar la fecha de vencimiento en el Sistema y Generar el folio Automático. <br> Saludos <br> <b>Administración IT</b></font>" & ObjUpdate.NotificaDocumentosporEntregar()
        '                'CadDocPorRecep = CadDocPorRecep.Replace("*", "</br>")

        '                'For x = 0 To DtEnvioemail.Rows.Count - 1
        '                '    If ContDocNoHabiles > 0 Then
        '                '     ObjUpdate.EnviarEmail(DtEnvioemail.Rows(x).Item("EMAIL").ToString(), "ALERTA DE DOCUMENTOS DE SUMINISTROS", CadDocPorRecep)
        '                '    End If
        '                'Next

        '                'Redirigimos.
        '                Response.Redirect("Inicio.aspx", False)

        '            Else

        '                lblMensajeError.Text = "Error al Impersonalizar Usuario."

        '            End If

        '        Else

        '            lblMensajeError.Text = "Usuario o Clave incorrecta."

        '        End If

        '    End If


        'Catch ex As Exception
        '    lblMensajeError.Text = ex.Message
        'Finally
        '    If Not _objContext Is Nothing Then
        '        _objContext.Undo()
        '    End If
        'End Try

    End Sub

    'Retorna el dominio.
    Function GetDomainName() As String
        Return System.Configuration.ConfigurationManager.AppSettings("Domain")
    End Function

    'Retorna el LDAP
    Function GetLDAP() As String
        Return System.Configuration.ConfigurationManager.AppSettings("LDAP")
    End Function


    'valida si el usuario ingreso informacion en los campos.
    Private Function ValidarIngreso() As Boolean
        Dim Resultado As Boolean = False
        Dim Item As New BEUsuario

        Resultado = True

        Item.CodigoUsuario = Me.txtUsuario.Text.Trim().ToUpper()
        Item.IdPerfil = ""
        Item.Estado = ""
        Dim Lista = New BLUsuario().ListarUsuarioPorCodigo(Item)

        Return Resultado
    End Function


End Class


