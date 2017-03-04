Public Class ShareCertificate
    Public Property Count As Integer
    Public Property Price As Decimal
    Public ReadOnly Property TotalPrice As Decimal
        Get
            Return _Price * Count
        End Get
    End Property
End Class
