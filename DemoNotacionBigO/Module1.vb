Module Module1

    Sub Main()

        Dim examples = New Examples()
        Dim Num = 15

        Dim stopWatch = New Stopwatch()

        'Fibonacci method O(N):
        stopWatch.Start()
        stopWatch.Start()
        Console.WriteLine(examples.Fibonacci2(Num))
        Console.WriteLine("Time elapsed: {0:0.00} seconds", Math.Round(stopWatch.ElapsedMilliseconds / 1000.0, 2))

        'Fibonacci method O(N):
        stopWatch.Restart()
        stopWatch.Start()
        Console.WriteLine(examples.Fibonacci3(Num))
        Console.WriteLine("Time elapsed: {0:0.00} seconds", Math.Round(stopWatch.ElapsedMilliseconds / 1000.0, 2))

        'Fibonacci method O(2^N):
        stopWatch.Restart()
        Console.WriteLine(examples.Fibonacci1(Num))
        Console.WriteLine("Time elapsed: {0:0.00} seconds", Math.Round(stopWatch.ElapsedMilliseconds / 1000.0, 2))

        Console.WriteLine("**********************************************")


        'Unique Ips O(N)
        stopWatch.Restart()
        Dim lineCount = examples.ReadAllLogs()
        Dim ipCount = examples.CountUniqueIPs2()
        Console.WriteLine("Number of unique IPs: " & ipCount)
        Console.WriteLine("Time elapsed: {0:0.00} seconds", Math.Round(stopWatch.ElapsedMilliseconds / 1000.0, 2))

        'Unique Ips O(N^2)
        stopWatch.Restart()
        lineCount = examples.ReadAllLogs()
        ipCount = examples.CountUniqueIPs1()
        Console.WriteLine("Number of unique IPs: " & ipCount)
        Console.WriteLine("Time elapsed: {0:0.0} seconds", Math.Round(stopWatch.ElapsedMilliseconds / 1000.0, 2))

        Console.ReadLine()

    End Sub

End Module
