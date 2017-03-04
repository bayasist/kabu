Public Class Form1
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim List As KabuList = New KabuList()

        KabuLoader.Instance().Load(List)

        Dim buy As New StockBuy1
        buy.Do(List)


        Me.KabuList = List
        Me.PictureBox1.Invalidate()
    End Sub


    Dim Buy As KabuBuy = Nothing
    Private Sub PictureBox1_Paint(sender As Object, e As PaintEventArgs) Handles PictureBox1.Paint
        Me.DrawKabuList(e.Graphics)
    End Sub

    Private KabuList As KabuList = Nothing
    Public Sub DrawKabuList(g As Graphics)
        If KabuList Is Nothing Then
            Exit Sub
        End If


        Dim Max As Integer = Integer.MinValue
        Dim Min As Integer = Integer.MaxValue

        Dim MaxCount As Integer = 200

        Dim Count As Integer = Math.Min(MaxCount, Me.KabuList.Count)
        For i As Integer = 0 To Count - 1
            Dim item As KabuItem = KabuList(i + Me.KabuList.Count - Count)
            If Max < item.Max Then
                Max = item.Max
            End If
            If Min > item.Min Then
                Min = item.Min
            End If
        Next
        For i As Integer = 0 To Count - 1
            Dim item As KabuItem = KabuList(i + Me.KabuList.Count - Count)
            Dim width As Double = PictureBox1.Width / Count


            Dim LineY1 As Double = (1 - ((item.Max - Min)) / (Max - Min)) * PictureBox1.Height
            Dim LineY2 As Double = (1 - ((item.Min - Min)) / (Max - Min)) * PictureBox1.Height
            Dim LineWidth As Double = 1 ' width * 0.2
            Dim LineX As Double = width * (i + 0.5) - LineWidth / 2
            g.FillRectangle(Brushes.Black, New RectangleF(LineX, LineY1, LineWidth, LineY2 - LineY1))



            Dim x As Double = width * i
            Dim y1 As Double = (1 - ((item.End - Min)) / (Max - Min)) * PictureBox1.Height
            Dim y2 As Double = (1 - ((item.Start - Min)) / (Max - Min)) * PictureBox1.Height
            Dim y As Double
            Dim brush As Brush
            If y1 < y2 Then
                y = y1
                brush = Brushes.Red
            Else
                y = y2
                brush = Brushes.Blue
            End If
            Dim height As Double = Math.Max(Math.Abs(y1 - y2), 1)
            g.FillRectangle(brush, New RectangleF(x, y, width, height))



        Next



    End Sub


End Class
