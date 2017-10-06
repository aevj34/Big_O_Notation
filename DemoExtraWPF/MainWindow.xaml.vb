Imports System.Threading
Imports System.ComponentModel


Class MainWindow


    Private WithEvents bw As New BackgroundWorker()
    Private WithEvents bw2 As New BackgroundWorker()


    Private Sub Window_Loaded_1(sender As Object, e As RoutedEventArgs)

        Try

            bw.WorkerSupportsCancellation = True
            bw.WorkerReportsProgress = True

            bw2.WorkerSupportsCancellation = True
            bw2.WorkerReportsProgress = True

            txtNumero.Focus()

        Catch ex As Exception
            'MsgBox(fact)
            MessageBox.Show(ex.Message)
        End Try

    End Sub


    Dim fiboN As Long

    Function Fibonacci_For(ByVal intNum As Integer) As Boolean


        Dim fibo0 As Long = 0
        Dim fibo1 As Long = 1

        fiboN = 0

        For i As Integer = 2 To intNum

            If bw.CancellationPending Then
                Return True
            End If

            Thread.Sleep(1)

            Dim percent As Integer = (i * 100) / intNum
            bw.ReportProgress(percent, fibo1)

            fibo1 = fibo1 + fibo0
            fibo0 = fibo1 - fibo0

        Next i

        fiboN = fibo1

        Return True

    End Function


    Dim fiboN2 As Long

    Function Fibonnaci_Recursivo(ByVal intNum As Integer) As Boolean

        fiboN2 = Fibonnaci(intNum)

        Return True

    End Function


    Function Fibonnaci(ByVal intNum As Integer) As Long

        If bw.CancellationPending Then
            Return True
        End If

        Thread.Sleep(1)

        Dim percent As Integer = (intNum * 100) / intNum

        If intNum <= 2 Then
            bw2.ReportProgress(percent, intNum)
            Return 1
        Else
            Dim value As Long = intNum
            bw2.ReportProgress(percent, value)
            Return Fibonnaci(intNum - 2) + Fibonnaci(intNum - 1)
        End If

    End Function


    Dim stopWatch As New Stopwatch()
    Dim stopWatch2 As New Stopwatch()

    Private Sub btnAcept_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnAccept.Click
        Try
            stopWatch.Reset()
            stopWatch.Start()
            grdExportando.Visibility = Windows.Visibility.Visible
            grdResultado.Visibility = Windows.Visibility.Collapsed
            bw.RunWorkerAsync(txtNumero.Text)

            stopWatch2.Reset()
            stopWatch2.Start()
            grdExportando2.Visibility = Windows.Visibility.Visible
            grdResultado2.Visibility = Windows.Visibility.Collapsed
            bw2.RunWorkerAsync(txtNumero.Text)

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnCancel.Click
        Try
            bw.CancelAsync()
            bw2.CancelAsync()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub


    Private Sub backgroundWorker_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs) Handles bw.DoWork
        'Try

        Dim value As Integer = CInt(e.Argument)
        e.Result = Fibonacci_For(value)
        '
        If bw.CancellationPending Then
            e.Cancel = True
            Return
        End If

        'Catch ex As Exception
        '    MessageBox.Show(ex.Message)
        'End Try

    End Sub


    Private Sub backgroundWorker_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bw.ProgressChanged
        'Try
        Valor.Content = CStr(e.UserState)
        'lblPercent.Content = CStr(e.ProgressPercentage) & "%"
        'progresss.Value = e.ProgressPercentage
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message)
        'End Try
    End Sub


    Private Sub backgroundWorker_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs) Handles bw.RunWorkerCompleted
        'Try

        grdExportando.Visibility = Windows.Visibility.Collapsed
        grdResultado.Visibility = Windows.Visibility.Visible
        lblResultVersion01.Content = "Fibonacci (" & txtNumero.Text & ") es: " & fiboN  '& " en " & Stopwatch.Elapsed.Seconds & " seg. y " & Stopwatch.Elapsed.TotalMilliseconds & " miliseg."
        stopWatch.Stop()

        lblDuracion01.Content = stopWatch.Elapsed.Seconds & " seg. y " & stopWatch.Elapsed.Milliseconds & " miliseg."

        'Catch ex As Exception
        '    MessageBox.Show(ex.Message)
        'End Try
    End Sub


    Private Sub backgroundWorker2_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs) Handles bw2.DoWork
        Try

            Dim value As Integer = CInt(e.Argument)
            e.Result = Fibonnaci_Recursivo(value)
            '
            If bw2.CancellationPending Then
                e.Cancel = True
                Return
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub


    Private Sub backgroundWorker2_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bw2.ProgressChanged
        Try
            Valor2.Content = CStr(e.UserState)
            lblPercent2.Content = CStr(e.ProgressPercentage) & "%"
            progresss2.Value = e.ProgressPercentage
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub


    Private Sub backgroundWorker2_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs) Handles bw2.RunWorkerCompleted
        Try

            grdExportando2.Visibility = Windows.Visibility.Collapsed
            grdResultado2.Visibility = Windows.Visibility.Visible
            lblResultVersion02.Content = "Fibonacci (" & txtNumero.Text & ") es: " & fiboN2  '& " en " & Stopwatch.Elapsed.Seconds & " seg. y " & Stopwatch.Elapsed.TotalMilliseconds & " miliseg."
            stopWatch2.Stop()

            lblDuracion02.Content = stopWatch2.Elapsed.Seconds & " seg. y " & stopWatch2.Elapsed.Milliseconds & " miliseg."


        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub


    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnClose.Click
        Try
            Me.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub Window_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Input.MouseButtonEventArgs) Handles MyBase.MouseDown

        ' Permite que se pueda mover la ventana por el escritorio, esto es necesario ya que estas ventanas no tiene los bordes predeterminados del sistema operativo.
        If e.LeftButton = System.Windows.Input.MouseButtonState.Pressed Then
            Me.DragMove()
        End If

    End Sub


End Class
