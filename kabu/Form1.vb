Public Class Form1
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        kabu = New StockManager
        Dim List As KabuList = New KabuList()
        Me.money = New MoneyManager(0)
        KabuLoader.Instance().Load(List)

        Dim buy As New StockBuy2
        buy.Do(List, Me.money, kabu)


        Me.KabuList = List
        Me.PictureBox1.Invalidate()
    End Sub


    Dim Buy As KabuBuy = Nothing
    Private Sub PictureBox1_Paint(sender As Object, e As PaintEventArgs) Handles PictureBox1.Paint
        Dim Dates As New List(Of Date)
        Me.DrawKabuList(e.Graphics, Dates)
        Me.DrawMoney(e.Graphics, Dates)
    End Sub
    Private money As MoneyManager
    Private kabu As New StockManager
    Public Sub DrawMoney(g As Graphics, Dates As List(Of Date))
        If money Is Nothing Then
            Exit Sub
        End If
        Dim Count As Integer = Dates.Count
        Dim Max As Integer = 10000 'money.Max
        Dim Min As Integer = -10000 'money.Min
        For i As Integer = 1 To Dates.Count - 1

            Dim Value1 As Decimal = money.GetMoney(Dates(i - 1)) + kabu.GetValue(Dates(i - 1))
            Dim Value2 As Decimal = money.GetMoney(Dates(i)) + kabu.GetValue(Dates(i))
            Dim width As Double = PictureBox1.Width / Count
            Dim x1 As Double = width * (i - 1)
            Dim x2 As Double = width * i
            Dim y1 As Double = (1 - ((Value1 - Min)) / (Max - Min)) * PictureBox1.Height
            Dim y2 As Double = (1 - ((Value2 - Min)) / (Max - Min)) * PictureBox1.Height
            g.DrawLine(Pens.Green, New PointF(x1, y1), New PointF(x2, y2))

            If kabu.ExistBuyDate(Dates(i - 1)) Then
                g.FillEllipse(Brushes.Red, New RectangleF(x1 - 3, y1 - 3, 6, 6))
                g.DrawLine(Pens.Red, New PointF(x1, 0), New PointF(x1, PictureBox1.Height))
            End If


            If kabu.ExistSellDate(Dates(i - 1)) Then
                g.FillEllipse(Brushes.Blue, New RectangleF(x1 - 3, y1 - 3, 6, 6))
                g.DrawLine(Pens.Blue, New PointF(x1, 0), New PointF(x1, PictureBox1.Height))
            End If
        Next

        Dim ZeroY = (1 - ((0 - Min)) / (Max - Min)) * PictureBox1.Height
        g.DrawLine(Pens.Black, New PointF(0, ZeroY), New PointF(Me.PictureBox1.Width, ZeroY))
    End Sub

    Private KabuList As KabuList = Nothing
    Public Sub DrawKabuList(g As Graphics, ByVal Dates As List(Of Date))
        If KabuList Is Nothing Then
            Exit Sub
        End If


        Dim Max As Integer = Integer.MinValue
        Dim Min As Integer = Integer.MaxValue

        Dim MaxCount As Integer = 20000

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
            Dates.Add(item.Time)


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
