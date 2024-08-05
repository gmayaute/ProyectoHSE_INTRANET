Imports Reutilizables
Imports System.Data
Imports System.Configuration.ConfigurationSettings
Imports NOVHSE.BusinessEntities
Imports NOVHSE.BusinessLogic
Imports System


Public Class PaginaBase
    Inherits System.Web.UI.Page


#Region "Constantes"
    Const SS_Base_Msj As String = "SS_Base_Msj" 'Propio de Pagina [Errornov]: Contiene un texto mensaje para pasarlo a otras páginas
    Const SS_Base_MsjDetalle As String = "SS_Base_MsjDetalle" 'Propio de Pagina [Errornov]: Contiene un texto detalle para pasarlo a otras páginas
#End Region

#Region "Grillas Editables"

    Public Enum AdminPageAction
        Insert = 1
        Update = 2
        Delete = 3
    End Enum
    Public Enum TipoValidacion
        entero = 1
        Texto = 2
        Fecha = 3
        Numerico = 4
    End Enum

    Public Property TablaDatos() As DataTable
        Get
            Return ViewState("Tabla")
        End Get
        Set(ByVal value As DataTable)
            ViewState("Tabla") = value
        End Set
    End Property


    Public Property Accion() As PaginaBase.AdminPageAction
        Get
            If ViewState("Accion") = Nothing Then
                Return AdminPageAction.Insert
            Else
                Return CType(ViewState("Accion"), AdminPageAction)
            End If
        End Get
        Set(ByVal value As PaginaBase.AdminPageAction)
            ViewState("Accion") = value
        End Set
    End Property

#End Region

#Region "Atributos y Propiedades"
    
    Private _Tracer As Log.LogTracer

    ''' <summary>Retorna el Id del usuario que inició la sesión en esta página (Podría  ser el ID del Dominio o un ID de BD propio)</summary>
    Public ReadOnly Property Tracer() As Log.LogTracer
        Get
            Return _Tracer
        End Get
    End Property

    ''' <summary>Retorna el Id (nov) del usuario que inició la sesión en esta página</summary>
    Public ReadOnly Property UsuarioActual_ID() As String
        Get
            If Session("CodUsuario") Is Nothing Then Return "" 'Puede haberse perdido la sesión
            Return Session("CodUsuario")
        End Get
    End Property


    ''' <summary>Retorna el Host del usuario que inició la sesión en esta página</summary>
    Public ReadOnly Property UsuarioActual_Host() As String
        Get
            Return Request.ServerVariables("REMOTE_ADDR")
        End Get
    End Property

    ''' <summary>Retorna el la ruta http donde esta alojada la aplicación web</summary>
    Public ReadOnly Property Application_DirectoryServer() As String
        Get
            Return "http://" & Request.ServerVariables("HTTP_HOST") & Me.Page.ResolveUrl("~")
        End Get
    End Property


    ''' <summary>Permite Compatibilidad con Explorer 8</summary>
    Public Sub CompatibleIE8()
        ' Try to force IE9 into IE8 Standards mode
        Dim meta As New HtmlMeta()
        meta.Attributes.Add("http-equiv", "X-UA-Compatible")
        meta.Attributes.Add("content", "IE=8")
        Me.Header.Controls.AddAt(0, meta) 'Must insert before any link or script tags!
    End Sub

#End Region

#Region "Inicializacion de la Pagina Base"

    Public Sub New()
        Me.InicializarPagina()
    End Sub



    ''' <summary>Se cargan los objetos propios de la página</summary>
    Private Sub InicializarPagina()
        ''Inicializamos los valores para el manejador de excepciones (Las configuraciones comentadas lineas abajo no son necesaria 
        ''ya q ya se definieron en el Web.config, pero quedan de ejemplo para futuras revisiones)

        Me._Tracer = New Log.LogTracer
        Me._Tracer.BDConection = NOVHSE.DataAccess.BDConexion.GetConnectionnovperuvhse()
    End Sub

    ' ''' <summary>Carga los datos referentes a la sesión (Esto se debería ejecutar solo cuando un usuario inicia una nueva sesión en el servidor web)</summary>
    'Public Function CargarDatosDeSesion(ByVal usuarioLogin As String) As Boolean

    '    Dim usuarioValidado As Boolean = False

    '    Try

    '        If usuarioLogin.Length = 0 Then 'Si no vienen el usuario significa que debemos cargarlo desde Windows.Identity
    '            Dim identityName As String = System.Threading.Thread.CurrentPrincipal.Identity.Name
    '            If identityName.Contains("\") Then identityName = identityName.Split("\").GetValue(1)

    '            usuarioLogin = identityName
    '        End If


    '        Dim ListUser As New List(Of BEControlUsuario)

    '        'ListUser = New BLUsuarioNOV().BuscarUsuarioNov(usuarioLogin)

    '        If ListUser.Count = 0 Then
    '            Response.Redirect("login.aspx", False)
    '            usuarioValidado = False
    '        Else
    '            If ListUser.Count = 1 Then
    '                usuarioValidado = True
    '            Else
    '                usuarioValidado = False
    '                Throw New UserException("Existe mas de un usuario con el mismo codigo.")
    '            End If

    '        End If

    '    Catch ex As Exception
    '        Me._Tracer.Publicar(ex, "")
    '        Session(SS_Base_Msj) = String.Format("({0}):  No se ha podido validar su ID de usuario en el Sistema. El sistema encontró un problema al intentar realizar la operación.", Me._Tracer.ID)
    '        Session(SS_Base_MsjDetalle) = ex.Message
    '        Return False
    '    End Try

    '    Return usuarioValidado

    'End Function

#End Region

#Region "Control de sesión de Página"

    ''' <summary>Permite verificar si los datos de sesión aun estan presentes, de lo contrario se asumirá como nueva sesión.</summary>
    Private Function EsNuevaSesion() As Boolean

        If Session.IsNewSession Then Return True
        If Session("CodUsuario") Is Nothing Then Return True
        'If Session(SS_UsuarioLogin).ToString.Trim.Length = 0 Then Return True

        Return False
    End Function

    'Principalmente debemos manejar la sesión cuando caduca
    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Me.EsNuevaSesion() Then

            Dim paginaActual As String = Request.ServerVariables("PATH_TRANSLATED")

            Session(SS_Base_Msj) = "Su sesión ha expirado. Vuelva a acceder a alguna página de la aplicación para renovarla."
            Response.Redirect("~/EndSession.aspx", False)

            'If paginaActual.EndsWith("Comun\Adjuntar.aspx") Then 'Se trata del fin de session de la pagina de archivos adjuntos
            '    Session("SS_Default_Mensaje") = "Su sesión ha expirado."
            '    Response.Redirect("~/Default.aspx", True)
            'ElseIf AppSettings("ModoLogin") IsNot Nothing AndAlso AppSettings("ModoLogin").ToString = "1" Then
            '    Session(SS_Base_Msj) = "Su sesión ha expirado. Vuelva a ingresar al sistema."
            '    Response.Redirect("~/ErrorNOV.aspx", True)
            'Else
            '    Session(SS_Base_Msj) = "Su sesión ha expirado. Vuelva a acceder a alguna página de la aplicación para renovarla."
            '    Response.Redirect("~/Inicio.aspx", True)
            'End If
        End If
    End Sub

#End Region

#Region "Manejar las Excepciones"

    ''' <summary>Metodo para manejar las excepciones Genericas (System.Exception)</summary>
    Public Sub ManejarExcepcion(ByVal ex As Exception, ByVal Pagina As String)
        If UserException.EsMensajeDeUsuario(ex) Then 'Mensajes desde BD por ejemplo
            Me.GenerarAlerta(UserException.GetMensajeDeUsuario(ex.Message))
        Else
            If ex.Message.StartsWith(":::Privilegio:::") Then
                Session(SS_Base_Msj) = ex.Message.ToString.Substring(":::Privilegio:::".Length)
                Session(SS_Base_MsjDetalle) = ""
            ElseIf ex.Message.StartsWith(":::MENSAJE:::") Then
                Session(SS_Base_Msj) = "No puede continuar con esta operación."
                Session(SS_Base_MsjDetalle) = ex.Message.Replace(":::MENSAJE:::", "")
            Else 'Solo se deberia publicar el detalle de la excepcion si es error del sistema
                Me._Tracer.PublicarEnBD(ex, Pagina)
                Session(SS_Base_Msj) = String.Format("({0}) El sistema encontró un problema al intentar realizar la operación.", Me._Tracer.ID)
                Session(SS_Base_MsjDetalle) = ex.Message & Environment.NewLine & ex.StackTrace + Pagina
            End If
            Response.Redirect("~/ErrorNOV.aspx", True)
        End If
    End Sub



    ''' <summary>Metodo para manejar las excepciones Genericas (System.Exception) considerando datos de la pagina donde Ocurrio el Error </summary>
    Public Sub ManejarExcepcion_ScriptManager(ByVal ex As Exception, ByVal Pagina As String)
        If UserException.EsMensajeDeUsuario(ex) Then 'Mensajes desde BD por ejemplo
            Me.GenerarAlerta_ScriptManager(UserException.GetMensajeDeUsuario(ex.Message))
        Else
            If ex.Message.StartsWith(":::Privilegio:::") Then
                Session(SS_Base_Msj) = ex.Message.ToString.Substring(":::Privilegio:::".Length)
                Session(SS_Base_MsjDetalle) = ""
            ElseIf ex.Message.StartsWith(":::MENSAJE:::") Then
                Session(SS_Base_Msj) = "No puede continuar con esta operación."
                Session(SS_Base_MsjDetalle) = ex.Message.Replace(":::MENSAJE:::", "") + Pagina
            Else 'Solo se deberia publicar el detalle de la excepcion si es error del sistema
                Me._Tracer.PublicarEnBD(ex, Pagina)
                Session(SS_Base_Msj) = String.Format("({0}) El sistema encontró un problema al intentar realizar la operación.", Me._Tracer.ID)
                Session(SS_Base_MsjDetalle) = ex.Message & Environment.NewLine & ex.StackTrace
            End If
            Response.Redirect("~/ErrorNOV.aspx", True)
        End If
    End Sub


#End Region

#Region "Alertas y Script Web"

    ''' <summary>Muestra una alerta en la página a través del objeto ClientScript</summary>
    Public Sub GenerarAlerta(ByVal mensaje As String)
        mensaje = String.Format("radalert('{0}',330,110,'Alerta');", mensaje.Replace("\", "\\").Replace("'", ""))
        Page.ClientScript.RegisterStartupScript(GetType(String), Guid.NewGuid().ToString(), mensaje, True)
    End Sub

    ''' <summary>Genera un script en la página a través del objeto ClientScript</summary>
    Public Sub GenerarScript(ByVal script As String)
        Page.ClientScript.RegisterStartupScript(GetType(String), Guid.NewGuid().ToString(), script, True)
    End Sub

    ''' <summary>Muestra un Bloque de script en la página a través del objeto ClientScript</summary>
    Public Sub GenerarScriptBloque(ByVal script As String)
        ClientScript.RegisterClientScriptBlock(GetType(String), Guid.NewGuid().ToString(), script, True)
    End Sub

    ''' <summary>Muestra una alerta en la página a través del objeto ScriptManager</summary>
    Public Sub GenerarAlerta_ScriptManager(ByVal mensaje As String)
        mensaje = String.Format("radalert('{0}',330,110,'Alerta');", mensaje.Replace("\", "\\").Replace("'", ""))
        ScriptManager.RegisterStartupScript(Me, GetType(String), Guid.NewGuid().ToString(), mensaje, True)
    End Sub

    '"radalert('Radalert is called from the client!', 330, 100,'Client RadAlert'); return false;"

    Public Sub ScriptRedireccionar(ByVal pagina As String)
        pagina = String.Format("location.href='{0}';", pagina)
        ScriptManager.RegisterStartupScript(Me, GetType(String), Guid.NewGuid().ToString(), pagina, True)
    End Sub

    ''' <summary>Genera un script en la página a través del objeto ScriptManager</summary>
    Public Sub GenerarScript_ScriptManager(ByVal script As String)
        ScriptManager.RegisterStartupScript(Me, GetType(String), Guid.NewGuid().ToString(), script, True)
    End Sub

#End Region

#Region "Otros"

    'Public Function TituloPagina(ByVal Titulo As String) As String

    '    Titulo = New BLParametro().BuscarParametroPorDenominacionUnica("VARIABLE_SISTEMA", "TITULO_BANCO").Valor + Titulo
    '    Return Titulo

    'End Function

#End Region

End Class
