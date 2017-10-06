Imports System.Threading
Imports System.ComponentModel

Public Class win_Counting


    Private WithEvents bw As New BackgroundWorker()
    Private WithEvents bw2 As New BackgroundWorker()

    Dim lista As New List(Of Integer)
    Dim intNumElementos As Integer

    Private Sub Window_Loaded_1(sender As Object, e As RoutedEventArgs)

        Try

            bw.WorkerSupportsCancellation = True
            bw.WorkerReportsProgress = True

            bw2.WorkerSupportsCancellation = True
            bw2.WorkerReportsProgress = True

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub


    Dim intCountIp As Integer = 0
    Private Function CountUniqueIPs1() As Integer

        Dim logReader = New LogReader()
        Dim ipsSeen = New List(Of String)

        For Each logLine As LogLine In logReader

            If bw.CancellationPending Then
                Return True
            End If

            Dim ip = logLine.GetIP()
            bw.ReportProgress(0, ip)

            If Not ipsSeen.Contains(ip) Then
                ipsSeen.Add(ip)
            End If

        Next

        intCountIp = ipsSeen.Count

        Return ipsSeen.Count


    End Function





    Dim intCountIp2 As Integer = 0
    Private Function CountUniqueIPs2() As Integer

        Dim logReader = New LogReader()
        Dim ipsSeen = New HashSet(Of String)

        For Each logLine As LogLine In logReader

            If bw2.CancellationPending Then
                Return True
            End If

            Dim ip = logLine.GetIP()
            bw2.ReportProgress(0, ip)

            If Not ipsSeen.Contains(ip) Then
                ipsSeen.Add(ip)
            End If

        Next

        intCountIp2 = ipsSeen.Count

        Return ipsSeen.Count

    End Function


    Dim stopWatch As New Stopwatch()
    Dim stopWatch2 As New Stopwatch()

    Private Sub btnAcept_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnAccept.Click

        Try
            stopWatch.Reset()
            stopWatch.Start()
            grdExportando.Visibility = Windows.Visibility.Visible
            grdResultado.Visibility = Windows.Visibility.Collapsed
            btnAccept.IsEnabled = False
            bw.RunWorkerAsync()

            stopWatch2.Reset()
            stopWatch2.Start()
            grdExportando2.Visibility = Windows.Visibility.Visible
            grdResultado2.Visibility = Windows.Visibility.Collapsed
            bw2.RunWorkerAsync()

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

            e.Result = CountUniqueIPs1()
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
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub


    Private Sub backgroundWorker_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs) Handles bw.RunWorkerCompleted
        Try

            grdExportando.Visibility = Windows.Visibility.Collapsed
            grdResultado.Visibility = Windows.Visibility.Visible
         
            lblResultVersion01.Content = "El número de ips únicos es: " & intCountIp

            stopWatch.Stop()

            lblDuracion01.Content = stopWatch.Elapsed.TotalSeconds & " seg."
            btnAccept.IsEnabled = True

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub


    Private Sub backgroundWorker2_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs) Handles bw2.DoWork
        Try

            e.Result = CountUniqueIPs2()
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
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub


    Private Sub backgroundWorker2_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs) Handles bw2.RunWorkerCompleted
        Try

            grdExportando2.Visibility = Windows.Visibility.Collapsed
            grdResultado2.Visibility = Windows.Visibility.Visible

            lblResultVersion02.Content = "El número de ips únicos es: " & intCountIp2

                stopWatch2.Stop()

            lblDuracion02.Content = stopWatch2.Elapsed.TotalSeconds & " seg."

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

        If e.LeftButton = System.Windows.Input.MouseButtonState.Pressed Then
            Me.DragMove()
        End If

    End Sub


End Class
