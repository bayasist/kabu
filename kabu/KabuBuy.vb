Public MustInherit Class KabuBuy

    Private _Money As Decimal

    Private _MoneyManager As MoneyManager
    Private _StockManager As StockManager
    Private _List As KabuList


    Public ReadOnly Property HasShareCertificate As Boolean
        Get
            Return ShareCertificateList.Count <> 0
        End Get
    End Property


    Private _CurrentItem As KabuItem
    Private _ShareCertificateList As New ShareCertificateList
    Public ReadOnly Property ShareCertificateList As ShareCertificateList
        Get
            Return _ShareCertificateList
        End Get
    End Property
    Public Sub [Do](List As KabuList, MoneyManager As MoneyManager, StockManager As StockManager)
        Me._MoneyManager = MoneyManager
        Me._StockManager = StockManager
        Me._List = List
        For i As Integer = 0 To List.Count - 1
            Me._CurrentItem = List(i)
            If i > 0 Then
                Me.Next(List(i - 1))
            End If
        Next
    End Sub

    Public MustOverride Sub [Next](Kabu As KabuItem)


    Public Sub Buy()
        Dim item As New ShareCertificate()
        item.Count = 1
        item.Price = _CurrentItem.Start
        Me.ShareCertificateList.Add(item)
        _Money -= item.TotalPrice
        Me._MoneyManager.Trade(-item.TotalPrice, _CurrentItem.Time)
        Me._StockManager.Buy(item, _List, _CurrentItem.Time)
    End Sub

    Public Sub sell()
        If Me.ShareCertificateList.Count = 0 Then
            Exit Sub
        End If
        Me._StockManager.Sell(Me.ShareCertificateList(0), _CurrentItem.Time)
        Me.ShareCertificateList.RemoveAt(0)
        _Money += _CurrentItem.Start
        Me._MoneyManager.Trade(_CurrentItem.Start, _CurrentItem.Time)
    End Sub

    Public Function CalcAssets() As Decimal
        Return _Money + Me.ShareCertificateList.TotalPrice
    End Function


End Class
