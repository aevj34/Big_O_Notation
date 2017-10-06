Class LogLine

    Private counter As Integer

    Public Sub New(counter As Integer)
        Me.counter = counter
    End Sub

    Public Function GetIP() As String
        Return "ip" & counter
    End Function

End Class
