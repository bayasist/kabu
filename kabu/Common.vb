Module Common


    Private Const UNIX_TIME_START As Date = #1/1/1970#

    Public Function ConvertDateFromUnixTime(UnixTime As Long) As Date
        Return UNIX_TIME_START.AddSeconds(UnixTime)
    End Function
End Module
