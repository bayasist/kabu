Imports kabu

Public Class StockBuy2
    Inherits KabuBuy

    Dim yesterday As New KabuList
    Dim today As New KabuList

    Dim SellPoint1 As Decimal
    Dim OverSellPoint1 As Boolean

    Dim SellPoint2 As Decimal

    Public Overrides Sub [Next](Kabu As KabuItem)
        If today.Count > 0 Then
            yesterday.Add(today(today.Count - 1))
        End If
        today.Add(Kabu)


        Dim Min As Decimal? = today.MinStartEnd(10)
        Dim Min30 As Decimal? = today.Min(20)
        Dim Max As Decimal? = today.MaxStartEnd(10)
        If Min Is Nothing OrElse Max Is Nothing OrElse Min30 Is Nothing Then
            Exit Sub
        End If

        Dim Value As Decimal = today.Pointing(Kabu.End, 10) '(Kabu.End - Min) / (Max - Min)

        If Me.HasShareCertificate Then
            If SellPoint1 < Kabu.End Then
                OverSellPoint1 = True
            End If
            If OverSellPoint1 AndAlso Math.Max(yesterday(yesterday.Count - 1).End, yesterday(yesterday.Count - 1).Start) > Math.Max(Kabu.End, Kabu.Start) Then
                Me.sell()
                Exit Sub
            End If
            If Kabu.End = Min30 Then
                Me.sell()
                Exit Sub
            End If
            If Value < 0.2 AndAlso Kabu.End < yesterday(yesterday.Count - 1).End Then
                Exit Sub
            End If



            If SellPoint2 > Kabu.End Then
                Me.sell()
                Exit Sub
            End If

        End If





        If Kabu.End = Min Then
            If Me.HasShareCertificate Then
                'Me.sell()
            End If

            Exit Sub
        End If

        If Kabu.End = Max Then
            If Not Me.HasShareCertificate Then
            End If
            Exit Sub
        End If

        If Value < 0.2 AndAlso Not Me.HasShareCertificate Then
            Me.Buy()
            SellPoint1 = Max
            OverSellPoint1 = False
            SellPoint2 = Min30
            Exit Sub
        End If

        If Value > 0.9 AndAlso Me.HasShareCertificate AndAlso Not OverSellPoint1 Then
            'Me.sell()
            Exit Sub
        End If


    End Sub
End Class
