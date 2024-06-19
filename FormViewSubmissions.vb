Imports System.Net.Http
Imports Newtonsoft.Json
Imports System.Threading.Tasks
Imports System.Text

Public Class FormViewSubmissions
    Inherits Form

    Private WithEvents ListBoxSubmissions As ListBox
    Private WithEvents TextBoxName As TextBox
    Private WithEvents TextBoxEmail As TextBox
    Private WithEvents TextBoxPhone As TextBox
    Private WithEvents TextBoxGithubLink As TextBox
    Private WithEvents TextBoxStopwatchTime As TextBox



    Private Async Sub FormViewSubmissions_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await LoadSubmissionsAsync()
    End Sub

    Private Async Function LoadSubmissionsAsync() As Task
        Dim submissions As List(Of Submission) = Await FetchSubmissionsAsync()

        If submissions IsNot Nothing Then
            For Each submission In submissions
                ListBoxSubmissions.Items.Add($"{submission.name} ({submission.email})")
            Next
        End If
    End Function

    Private Async Function FetchSubmissionsAsync() As Task(Of List(Of Submission))
        Try
            Using client As New HttpClient()
                Dim response As HttpResponseMessage = Await client.GetAsync("http://localhost:3000/read")

                If response.IsSuccessStatusCode Then
                    Dim responseBody As String = Await response.Content.ReadAsStringAsync()
                    Return JsonConvert.DeserializeObject(Of List(Of Submission))(responseBody)
                Else
                    MessageBox.Show("Failed to load submissions.")
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show($"Error: {ex.Message}")
        End Try

        Return Nothing
    End Function

    Private Sub ListBoxSubmissions_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBoxSubmissions.SelectedIndexChanged
        Dim selectedIndex As Integer = ListBoxSubmissions.SelectedIndex

        If selectedIndex >= 0 Then
            Dim submission As Submission = CType(ListBoxSubmissions.Items(selectedIndex), Submission)
            TextBoxName.Text = submission.name
            TextBoxEmail.Text = submission.email
            TextBoxPhone.Text = submission.phone
            TextBoxGithubLink.Text = submission.github_link
            TextBoxStopwatchTime.Text = submission.stopwatch_time
        End If
    End Sub

End Class
