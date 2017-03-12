Public Class MoneyManager
    Private _MoneyTrade As New List(Of MoneyManagerItem)
    Private _StartMoney As Decimal
    Public ReadOnly Property StartMoney As Decimal
        Get
            Return _StartMoney
        End Get
    End Property

    Public Sub New(StartMoney As Decimal)
        Me._StartMoney = StartMoney
    End Sub

    Public Sub Trade(Money As Decimal, Time As Date)
        Dim Item As New MoneyManagerItem
        Item.Time = Time
        Item.AdditionalFund = Money
        Me._MoneyTrade.Add(Item)
    End Sub

    Public Function GetMoney(Time As Date) As Decimal
        Dim result As Decimal = Me.StartMoney
        For i As Integer = 0 To Me._MoneyTrade.Count - 1
            If Me._MoneyTrade(i).Time > Time Then
                Return result
            End If
            result += Me._MoneyTrade(i).AdditionalFund
        Next
        Return result
    End Function

    Public Function Min() As Decimal
        Dim money As Decimal
        Dim result As Decimal = StartMoney
        For i As Integer = 0 To Me._MoneyTrade.Count - 1
            money += Me._MoneyTrade(i).AdditionalFund
            If result > money Then
                result = money
            End If
        Next
        Return result
    End Function

    Public Function Max() As Decimal
        Dim money As Decimal
        Dim result As Decimal = StartMoney
        For i As Integer = 0 To Me._MoneyTrade.Count - 1
            money += Me._MoneyTrade(i).AdditionalFund
            If result < money Then
                result = money
            End If
        Next
        Return result
    End Function

    Public Sub Clear()
        Me._MoneyTrade.Clear()
    End Sub
End Class

Public Class MoneyManagerItem
    Public Property AdditionalFund As Decimal
    Public Property Time As Date
End Class