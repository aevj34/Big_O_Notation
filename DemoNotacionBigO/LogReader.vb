

Class LogReader
    Implements IEnumerable(Of LogLine)

    Private counter As Integer = 0



    Public Function GetEnumerator() As IEnumerator(Of LogLine) Implements IEnumerable(Of LogLine).GetEnumerator

        Dim N = 100000
        Dim uniqueIPs = 90001

        Dim lst As New List(Of LogLine)

        While counter < N

            Dim obj As New LogLine(counter Mod uniqueIPs)
            lst.Add(obj)

            counter += 1

        End While

        Return lst.GetEnumerator

    End Function

    Private Function IEnumerable_GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
        Return Me.GetEnumerator()
    End Function


   


End Class
