Partial Public Class PaginaMasterX
    Inherits System.Web.UI.MasterPage

    Private m_Titulo As String

    Public Property Titulo() As String
        Get
            Return m_Titulo
        End Get
        Set(ByVal value As String)
            m_Titulo = value
            Me.LbDias.Text = value
        End Set
    End Property

End Class