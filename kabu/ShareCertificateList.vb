Public Class ShareCertificateList
    Inherits List(Of ShareCertificate)

    Public ReadOnly Property TotalPrice As Decimal
        Get
            Dim result As Decimal = 0
            For Each o In Me
                result += o.TotalPrice
            Next
            Return result
        End Get
    End Property

End Class
