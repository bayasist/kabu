Imports kabu

Public MustInherit Class Graph
    Private WithEvents _Parent As KabuList
    Private _Item As New Dictionary(Of Date, Decimal?)
    Public ReadOnly Property Item(Time As Date)
        Get
            If Not Me._Item.ContainsKey(Time) Then
                Return Nothing
            End If
            Return Me._Item(Time)
        End Get
    End Property
    Public ReadOnly Property Parent As KabuList
        Get
            Return Me._Parent
        End Get
    End Property

    Public Sub SetParent(Parent As KabuList)
        Me._Parent = Parent
    End Sub

    Public Sub New()
    End Sub

    Protected Overridable Sub _Parent_StockItemAdded(sender As Object, e As StockItemAddedEventArgs) Handles _Parent.StockItemAdded
        Me._Item(e.KabuItem.Time) = Me.GetGraphItem(e.KabuItem)
    End Sub

    Protected MustOverride Function GetGraphItem(KabuItem As KabuItem) As Decimal?
End Class
