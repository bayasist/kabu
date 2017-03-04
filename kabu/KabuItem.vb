Public Class KabuItem

    Public Property Min As Decimal
    Public Property Max As Decimal
    Public Property Start As Decimal
    Public Property [End] As Decimal
    Public Property Time As date

    Public Shadows Function ToString() As String
        Return "時間 : " & Me.Time.ToString("yyyy/MM/dd") & " 初値 : " & Me.Start & " 高値 : " & Me.Max & " 安値 : " & Me.Min & " 終値 : " & Me.End
    End Function


End Class
