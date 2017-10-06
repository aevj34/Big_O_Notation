Public Class Main


    Private Sub btnFibonacci_Click(sender As Object, e As RoutedEventArgs) Handles btnFibonacci.Click

        Try

            Dim win As New MainWindow
            win.ShowDialog()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub


    Private Sub btnBusqueda_Click(sender As Object, e As RoutedEventArgs) Handles btnBusqueda.Click

        Try

            Dim win As New win_Busqueda_Binaria
            win.ShowDialog()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub


    Private Sub btnIpUnico_Click(sender As Object, e As RoutedEventArgs) Handles btnIpUnico.Click

        Try

            Dim win As New win_Counting
            win.ShowDialog()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub


End Class
