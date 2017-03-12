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



    Public Function Max(Count As Integer) As Decimal?
        If Count > Me.Count Then
            Return Nothing
        End If

        Dim Result As Decimal = Integer.MinValue
        For i As Integer = Me.Count - 1 To Me.Count - Count Step -1
            If Result < Me(i).Max Then
                Result = Me(i).Max
            End If
        Next
        Return Result
    End Function

    Public Function Min(Count As Integer) As Decimal?
        If Count > Me.Count Then
            Return Nothing
        End If

        Dim Result As Decimal = Integer.MaxValue
        For i As Integer = Me.Count - 1 To Me.Count - Count Step -1
            If Result > Me(i).Min Then
                Result = Me(i).Min
            End If
        Next
        Return Result
    End Function

    Public Function MinStartEnd(Count As Integer) As Decimal?
        If Count > Me.Count Then
            Return Nothing
        End If

        Dim Result As Decimal = Integer.MaxValue
        For i As Integer = Me.Count - 1 To Me.Count - Count Step -1
            If Result > Me(i).Start Then
                Result = Me(i).Start
            End If
            If Result > Me(i).End Then
                Result = Me(i).End
            End If
        Next
        Return Result
    End Function

    Public Function MaxStartEnd(Count As Integer) As Decimal?
        If Count > Me.Count Then
            Return Nothing
        End If

        Dim Result As Decimal = Integer.MinValue
        For i As Integer = Me.Count - 1 To Me.Count - Count Step -1
            If Result < Me(i).Start Then
                Result = Me(i).Start
            End If
            If Result < Me(i).End Then
                Result = Me(i).End
            End If
        Next
        Return Result
    End Function

    Public Function Pointing(Value As Decimal, Count As Integer) As Decimal?
        If Count > Me.Count Then
            Return Nothing
        End If
        Dim Points As New List(Of Decimal)
        For i As Integer = Me.Count - 1 To Me.Count - Count Step -1
            Points.Add(Me(i).Min)
            Points.Add(Me(i).Max)
            Points.Add(Me(i).Start)
            Points.Add(Me(i).End)
        Next
        Points.Sort()
        For i As Integer = 0 To Points.Count - 1
            If Points(i) > Value Then
                If i = 0 Then
                    Return 0D
                End If
                Return i / Points.Count
            End If
        Next
        Return 1D


    End Function
End Class
