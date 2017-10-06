Imports System.Threading
Imports System.ComponentModel


Public Class win_Busqueda_Binaria

    Private WithEvents bw As New BackgroundWorker()
    Private WithEvents bw2 As New BackgroundWorker()

    Dim lista As New List(Of Integer)
    Dim intNumElementos As Integer

    Private Sub Window_Loaded_1(sender As Object, e As RoutedEventArgs)
        Try

            intNumElementos = 5000000
            Dim rnd As Random = New Random()

            For i As Integer = 0 To intNumElementos - 1
                Dim num As Integer = rnd.Next(0, intNumElementos)
                lista.Add(num)
            Next i

            lista.Sort()

            tblSecuencial.Text = "Busqueda Secuencial (" & intNumElementos & " elementos)"
            tblBinaria.Text = "Busqueda Binaria (" & intNumElementos & " elementos)"

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


    Dim intPos As Integer = -1

    Function Busqueda_Secuencial(ByVal intNum As Integer) As Boolean


        For i As Integer = 0 To lista.Count - 1

            If bw.CancellationPending Then
                Return True
            End If

            Thread.Sleep(1)

            Dim percent As Integer = (i * 100) / intNum

            bw.ReportProgress(percent, lista.Item(i))

            If lista.Item(i) = intNum Then
                intPos = i
                Exit For
            End If

        Next i

        Return True

    End Function


    Dim intPos2 As Integer = -1

    Function Busqueda_Binaria(ByVal intNum As Integer) As Integer

        If bw2.CancellationPending Then
            Return True
        End If

        Thread.Sleep(1)

        Dim centro As Integer = 0
        Dim inf As Integer = 0
        Dim sup As Integer = lista.Count - 1

        While inf <= sup

            bw2.ReportProgress(0, intNum)

            centro = ((sup - inf) / 2) + inf

            If lista(centro) = intNum Then
                intPos2 = centro
                Return centro
            ElseIf intNum < lista(centro) Then
                sup = centro - 1
            Else
                inf = centro + 1
            End If

        End While

        Return -1

    End Function


    Dim stopWatch As New Stopwatch()
    Dim stopWatch2 As New Stopwatch()

    Private Sub btnAcept_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnAccept.Click
        Try

            intPos = -1
            stopWatch.Reset()
            stopWatch.Start()
            grdExportando.Visibility = Windows.Visibility.Visible
            grdResultado.Visibility = Windows.Visibility.Collapsed
            btnAccept.IsEnabled = False
            bw.RunWorkerAsync(txtNumero.Text)

            intPos2 = -1
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
        Try

            Dim value As Integer = CInt(e.Argument)
            e.Result = Busqueda_Secuencial(value)
            '
            If bw.CancellationPending Then
                e.Cancel = True
                Return
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub


    Private Sub backgroundWorker_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bw.ProgressChanged
        Try
            Valor.Content = CStr(e.UserState)
            'lblPercent.Content = CStr(e.ProgressPercentage) & "%"
            'progresss.Value = e.ProgressPercentage
            'MsgBox(progresss.Value)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub


    Private Sub backgroundWorker_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs) Handles bw.RunWorkerCompleted
        Try

            grdExportando.Visibility = Windows.Visibility.Collapsed
            grdResultado.Visibility = Windows.Visibility.Visible

            If intPos = -1 Then
                lblResultVersion01.Content = "El número " & txtNumero.Text & " no se encuentra en la lista."  '& " en " & Stopwatch.Elapsed.Seconds & " seg. y " & Stopwatch.Elapsed.TotalMilliseconds & " miliseg."
            Else
                lblResultVersion01.Content = "El número " & txtNumero.Text & " está en la posición: " & intPos  '& " en " & Stopwatch.Elapsed.Seconds & " seg. y " & Stopwatch.Elapsed.TotalMilliseconds & " miliseg."
            End If

            stopWatch.Stop()

            lblDuracion01.Content = stopWatch.Elapsed.TotalSeconds & " seg." '& stopWatch.Elapsed.TotalMilliseconds & " miliseg."
            btnAccept.IsEnabled = True

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub


    Private Sub backgroundWorker2_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs) Handles bw2.DoWork
        Try

            Dim value As Integer = CInt(e.Argument)
            e.Result = Busqueda_Binaria(value)
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
            'lblPercent2.Content = CStr(e.ProgressPercentage) & "%"
            'progresss2.Value = e.ProgressPercentage
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub


    Private Sub backgroundWorker2_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs) Handles bw2.RunWorkerCompleted
        Try

            grdExportando2.Visibility = Windows.Visibility.Collapsed
            grdResultado2.Visibility = Windows.Visibility.Visible

            If intPos2 = -1 Then
                lblResultVersion02.Content = "El número " & txtNumero.Text & " no se encuentra en la lista."  '& " en " & Stopwatch.Elapsed.Seconds & " seg. y " & Stopwatch.Elapsed.TotalMilliseconds & " miliseg."
            Else
                lblResultVersion02.Content = "El número " & txtNumero.Text & " está en la posición: " & intPos2  '& " en " & Stopwatch.Elapsed.Seconds & " seg. y " & Stopwatch.Elapsed.TotalMilliseconds & " miliseg."
            End If

            stopWatch2.Stop()

            lblDuracion02.Content = stopWatch2.Elapsed.TotalSeconds & " seg." '& stopWatch2.Elapsed.Milliseconds & " miliseg."
            'btnAccept.IsEnabled = True

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
