Public Class KabuList
    Inherits List(Of KabuItem)


    Public Function CalcAverage(Count As Integer) As Decimal?
        If Count > Me.Count Then
            Return Nothing
        End If

        Dim Sum As Decimal = 0
        For i As Integer = Me.Count - 1 To Me.Count - Count Step -1
            Sum += Me(i).End
        Next
        Return Sum / Count
    End Function
End Class
