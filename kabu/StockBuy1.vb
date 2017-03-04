Imports kabu

Public Class StockBuy1
    Inherits KabuBuy

    Dim yesterday As New KabuList
    Dim today As New KabuList

    Public Overrides Sub [Next](Kabu As KabuItem)
        If today.Count > 0 Then
            yesterday.Add(today(today.Count - 1))
        End If
        today.Add(Kabu)


        Dim YesAvg5 As Decimal? = yesterday.CalcAverage(1)
        Dim TodAvg5 As Decimal? = today.CalcAverage(1)
        Dim YesAvg15 As Decimal? = yesterday.CalcAverage(15)
        Dim TodAvg15 As Decimal? = today.CalcAverage(15)

        If YesAvg5 Is Nothing OrElse TodAvg5 Is Nothing OrElse YesAvg15 Is Nothing OrElse TodAvg15 Is Nothing Then
            Exit Sub
        End If


        If YesAvg15 > YesAvg5 AndAlso TodAvg15 < TodAvg5 Then
            Me.Buy()
        End If

        If YesAvg15 < YesAvg5 AndAlso TodAvg15 > TodAvg5 Then
            Me.sell()
        End If


    End Sub
End Class
