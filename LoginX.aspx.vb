Public Partial Class LoginX
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        FormsAuthentication.SignOut()
        Session.Item("Fecha") = Date.Now.Day & "/" & Date.Now.Month & "/" & Date.Now.Year
        Txtuser.Focus()
        'DE PRUEBA+++++++++++++++++++++++++++++++++
        'Txtuser.Text = "Atorres"
        'TxtPass.Text = "d3s4rr0ll0"
        'BtnAceptar_Click(Nothing, Nothing)
        'DE PRUEBA+++++++++++++++++++++++++++++++++
    End Sub

    Protected Sub BtnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnAceptar.Click
        Dim aceptado As Boolean = False
        If Autentificacion(Txtuser.Text, TxtPass.Text) Then
            FormsAuthentication.RedirectFromLoginPage(Txtuser.Text, False)
            Response.Redirect("~/Cotizadores/Default.aspx")
        Else
            TxtPass.Text = ""
            Label1.Text = "No estas Autorizado para Entrar a este Sitio."
            Txtuser.Focus()
        End If
    End Sub

    Function Autentificacion(ByVal User As String, ByVal Pass As String) As Boolean
        Autentificacion = False
        If Txtuser.Text = "Creditaria" And TxtPass.Text = "Credit2018" Then
            Session.Item("User") = "Creditaria"
            Session.Item("Nombre") = "Creditaria"
            Autentificacion = True
        End If
    End Function

    

End Class