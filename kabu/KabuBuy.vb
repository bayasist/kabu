Public MustInherit Class KabuBuy

    Private _Money As Decimal

    Private _CurrentItem As KabuItem
    Private _ShareCertificateList As New ShareCertificateList
    Public ReadOnly Property ShareCertificateList As ShareCertificateList
        Get
            Return _ShareCertificateList
        End Get
    End Property
    Public Sub [Do](List As KabuList)
        For i As Integer = 0 To List.Count - 1
            Me._CurrentItem = List(i)
            If i > 0 Then
                Me.Next(List(i - 1))
            End If
        Next
    End Sub

    Public MustOverride Sub [Next](Kabu As KabuItem)


    Public Sub Buy()
        Dim item As New ShareCertificate
        item.Count = 1
        item.Price = _CurrentItem.Start
        Me.ShareCertificateList.Add(item)
        _Money -= item.TotalPrice
    End Sub

    Public Sub sell()
        If Me.ShareCertificateList.Count = 0 Then
            Exit Sub
        End If
        Me.ShareCertificateList.RemoveAt(0)
        _Money += _CurrentItem.Start
    End Sub

    Public Function CalcAssets() As Decimal
        Return _Money + Me.ShareCertificateList.TotalPrice
    End Function


End Class
