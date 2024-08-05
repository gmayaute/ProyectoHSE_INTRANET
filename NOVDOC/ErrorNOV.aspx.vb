Public Class ErrorNOV_aspx
    Inherits System.Web.UI.Page

    Const SS_Base_Msj As String = "SS_Base_Msj" 'Propio de Pagina [ErrorNov]: Contiene un texto mensaje para pasarlo a otras páginas
    Const SS_Base_MsjDetalle As String = "SS_Base_MsjDetalle" 'Propio de Pagina [ErrorNov]: Contiene un texto detalle para pasarlo a otras páginas


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        If Not Page.IsPostBack Then


            If Session(SS_Base_Msj) Is Nothing Then Exit Sub


            Me.txtDetalle.Text = String.Format("{0}:{1}", Session(SS_Base_Msj).ToString, Session(SS_Base_MsjDetalle).ToString)

            'Si existe sesiones de Error lo mostramos.
            If txtDetalle.Text.Trim().Length > 0 Then
                txtDetalle.Visible = True
            Else
                txtDetalle.Visible = False
            End If

            'Eliminamos las sesiones.

            Session.Contents.Remove(SS_Base_Msj)
            Session.Contents.Remove(SS_Base_MsjDetalle)


        End If



    End Sub

    Protected Sub lnkRegresar_Click(sender As Object, e As EventArgs) Handles lnkRegresar.Click
        Response.Redirect("Inicio.aspx", False)
    End Sub
End Class