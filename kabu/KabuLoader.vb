Public Class KabuLoader

    Private Sub New()

    End Sub


    Private Shared _Instance As KabuLoader = Nothing
    Public Shared ReadOnly Property Instance() As KabuLoader
        Get
            If _Instance Is Nothing Then
                _Instance = New KabuLoader
            End If
            Return _Instance
        End Get
    End Property

    Public Sub Load(ByVal List As KabuList)
        Dim TimeSliceSecond As Integer = 86400
        Dim CloseOpenSlice As Integer = TimeSliceSecond
        If CloseOpenSlice = 86400 Then
            CloseOpenSlice = 6 * 60 * 60
        End If
        'ダウンロード元のURL
        Dim url As String = "https://www.google.com/finance/getprices?p=10Y&f=d,h,o,l,c,v&i=" & TimeSliceSecond & "x=INDEXNIKKEI&q=NI225"




        'WebClientを作成
        Dim wc As New System.Net.WebClient()
        '文字コードを指定
        wc.Encoding = System.Text.Encoding.UTF8
        'データを文字列としてダウンロードする
        Dim source As String = wc.DownloadString(url)
        '後始末
        wc.Dispose()
        Dim Lines As String() = Split(source, vbLf)

        Dim BeforeTime As Date = Date.MinValue
        Dim BasedTime As Date = Date.MinValue
        For i As Integer = 8 To Lines.Count - 1
            Dim Line As String = Lines(i)
            If Line.Trim.Length = 0 Then
                Continue For
            End If
            Dim LineItems As String() = Split(Line,",")
            Dim Time As Date
            If Line.StartsWith("a") Then
                Time = Common.ConvertDateFromUnixTime(Mid(LineItems(0), 2)).AddHours(9).AddSeconds(-CloseOpenSlice)
                BasedTime = Time
            Else
                Time = BasedTime.AddSeconds(LineItems(0) * TimeSliceSecond)
            End If


            Dim KabuItem As New KabuItem
            KabuItem.End = LineItems(1)
            KabuItem.Start = LineItems(4)
            KabuItem.Max = LineItems(2)
            KabuItem.Min = LineItems(3)
            KabuItem.Time = Time

            List.Add(KabuItem)


            BeforeTime = Time
        Next


    End Sub
    ' UNIXエポックを表すDateTimeオブジェクトを取得
    Private UNIX_EPOCH As DateTime =
    New DateTime(1970, 1, 1, 0, 0, 0, 0)

    Private Function GetUnixTime(ByVal targetTime As DateTime) As Long

        ' UTC時間に変換
        targetTime = targetTime.ToUniversalTime()

        ' UNIXエポックからの経過時間を取得
        Dim elapsedTime As TimeSpan = targetTime - UNIX_EPOCH

        ' 経過秒数に変換
        Return CType(elapsedTime.TotalSeconds, Long)

    End Function
End Class
