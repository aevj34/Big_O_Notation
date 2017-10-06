

Public Class Examples


    Shared fibonacciCache As Long() = New Long(5000) {}

    'Complexity: O(N)
    Sub [Loop](Num As Long)
        Dim counter = 0
        While counter < Num
            Console.WriteLine(counter)
            counter = counter + 1
        End While
    End Sub


    'Complexity: O(N^2)
    Sub CreateAllPairs(Num As Long)
        Dim x = 0
        Dim y = 0
        While x < Num
            While y < Num
                Console.WriteLine("{0}, {1}", x, y)
                y = y + 1
            End While
            x = x + 1
        End While
    End Sub


    'Complexity: O(N)
    Function ContainsNeedle(needle As Integer, haystack As List(Of Integer)) As Boolean
        For Each sample As Integer In haystack
            If sample = needle Then
                Return True
            End If
        Next
        Return False
    End Function


    'Complexity: O(N)
    Function Faculty(Num As Integer) As Integer
        If Num = 1 Then
            Return 1
        End If
        Return Num * Faculty(Num - 1)
    End Function


    'Complexity: O(2^N)
    Function Fibonacci1(num As Long) As Integer
        If num < 0 Then
            Throw New Exception("Num can not be less than zero")
        End If
        If num <= 2 Then
            Return 1
        End If
        Return Fibonacci1(num - 1) + Fibonacci1(num - 2)
    End Function


    'Complexity: O(N)
    Function Fibonacci2(num As Long) As Long

        If num < 0 Then
            Throw New Exception("num can not be less than zero")
        End If
        If num <= 2 Then
            Return 1
        End If

        Dim fibonacci As Long = 0
        Dim previous As Long = 1
        Dim penultimate As Long = 0
        For i As Integer = 1 To num
            penultimate = fibonacci
            fibonacci = previous + fibonacci
            previous = penultimate
        Next

        Return fibonacci

    End Function


    'Complexity: O(N)
    Public Function Fibonacci3(num As Long) As Long
        If num < 0 Then
            Throw New Exception("Num can not be less than zero")
        End If
        If num <= 2 Then
            fibonacciCache(num) = 1
        End If
        If fibonacciCache(num) = 0 Then
            fibonacciCache(num) = Fibonacci3(num - 1) + Fibonacci3(num - 2)
        End If
        Return fibonacciCache(num)
    End Function


    'Complexity: O(log2 N)
    Function BinarySearch(haystack As List(Of Integer), needle As Integer, min As Integer, max As Integer) As System.Nullable(Of Integer)

        Dim midpoint = (max + min) / 2
        If haystack.Count > 0 AndAlso haystack(midpoint) = needle Then
            Return midpoint
        End If

        If min >= max Then
            Return Nothing
        End If

        If haystack(midpoint) > needle Then
            Return BinarySearch(haystack, needle, min, midpoint - 1)
        End If

        Return BinarySearch(haystack, needle, midpoint + 1, max)

    End Function


    Function ReadAllLogs() As Integer
        Dim logReader = New LogReader()
        Dim linesSeen = 0
        For Each line As LogLine In logReader
            Dim ip = line.GetIP()
            linesSeen += 1
        Next
        Return linesSeen
    End Function


    'Complexity: O(N^2)

    Function CountUniqueIPs1() As Integer

        Dim logReader = New LogReader()
        Dim ipsSeen = New List(Of String)()

        For Each logLine As LogLine In logReader
            Dim ip = logLine.GetIP()
            If Not ipsSeen.Contains(ip) Then
                ipsSeen.Add(ip)
            End If
        Next

        Return ipsSeen.Count

    End Function


    'Complexity: O(N)
    Function CountUniqueIPs2() As Integer

        Dim logReader = New LogReader()

        Dim ipsSeen = New HashSet(Of String)()
        For Each logLine As LogLine In logReader
            Dim ip = logLine.GetIP()
            If Not ipsSeen.Contains(ip) Then
                ipsSeen.Add(ip)
            End If
        Next

        Return ipsSeen.Count

    End Function


End Class
