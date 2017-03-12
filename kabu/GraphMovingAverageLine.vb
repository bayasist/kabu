Imports kabu

Public Class GraphMovingAverageLine
    Inherits Graph

    Private _Days As Integer
    Public ReadOnly Property Days As Integer
        Get
            Return _Days
        End Get
    End Property

    Public Sub New(Days As Integer)
        MyBase.New()
        Me._Days = Days
    End Sub
    Protected Overrides Function GetGraphItem(KabuItem As KabuItem) As Decimal?
        If Days > Me.Parent.Count Then
            Return Nothing
        End If

        Dim Sum As Decimal
        For i As Integer = Me.Parent.Count - 1 To Me.Parent.Count - Me.Days Step -1
            Sum += Me.Parent(i).Start
            Sum += Me.Parent(i).End
        Next
        Return Sum / (Me.Days * 2)
    End Function
End Class
