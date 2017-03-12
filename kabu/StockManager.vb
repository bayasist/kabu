Public Class StockManager
    Private _List As New List(Of StockManagerItem)

    Public Sub Buy(ShareCertificate As ShareCertificate, StockTransition As KabuList, Time As Date)
        Dim item As New StockManagerItem
        item.ShareCertificate = ShareCertificate
        item.StockTransition = StockTransition
        item.BuyTime = Time
        Me._List.Add(item)
    End Sub

    Public Sub Sell(ShareCertificate As ShareCertificate, Time As Date)
        Dim item As StockManagerItem = Nothing
        For i As Integer = 0 To _List.Count - 1
            If _List(i).ShareCertificate Is ShareCertificate Then
                item = _List(i)
                Exit For
            End If
        Next
        If item.SellTime < Date.MinValue Then
            Exit Sub
        End If
        item.SellTime = Time
    End Sub

    Public Function GetValue(Time As Date) As Decimal
        Dim Result As Decimal = 0
        For i As Integer = 0 To _List.Count - 1
            If Not _List(i).HavePeriod(Time) Then
                Continue For
            End If
            Result += Me._List(i).GetValue(Time)
        Next
        Return Result
    End Function


    Public Function ExistBuyDate(Time As Date) As Boolean
        For i As Integer = 0 To _List.Count - 1
            If _List(i).BuyTime.Date = Time.Date Then
                Return True
            End If
        Next
        Return False

    End Function

    Public Function ExistSellDate(Time As Date) As Boolean
        For i As Integer = 0 To _List.Count - 1
            If _List(i).SellTime.Date = Time.Date Then
                Return True
            End If
        Next
        Return False

    End Function


    Public Sub Clear()
        Me._List.Clear()
    End Sub
End Class

Public Class StockManagerItem
    Public Property ShareCertificate As ShareCertificate
    Public Property StockTransition As KabuList
    Public Property BuyTime As Date
    Public Property SellTime As Date = Date.MaxValue

    Public Function HavePeriod(Value As Date)
        If Me.BuyTime <= Value AndAlso (Value < Me.SellTime) Then
            Return True
        End If
        Return False
    End Function
    

    Public Function GetValue(Time As Date) As Decimal?
        If Not Me.HavePeriod(Time) Then
            Return Nothing
        End If

        If Time < StockTransition(0).Time Then
            Return StockTransition(0).End * Me.ShareCertificate.Count
        End If

        For i As Integer = 0 To StockTransition.Count - 1
            If Time <= StockTransition(i).Time Then
                Return StockTransition(i).End * Me.ShareCertificate.Count
            End If
        Next
        Return StockTransition(StockTransition.Count - 1).End * Me.ShareCertificate.Count
    End Function
End Class
