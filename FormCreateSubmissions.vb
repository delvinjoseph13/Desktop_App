Imports System.Net.Http
Imports Newtonsoft.Json
Imports System.Text

Public Class FormCreateSubmissions
    Inherits Form

    Private WithEvents ButtonSubmit As Button
    Private TextBoxName As TextBox
    Private TextBoxEmail As TextBox
    Private TextBoxPhone As TextBox
    Private TextBoxGithubLink As TextBox
    Private TextBoxStopwatchTime As TextBox

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub InitializeComponent()
        Me.ButtonSubmit = New Button()
        Me.TextBoxName = New TextBox()
        Me.TextBoxEmail = New TextBox()
        Me.TextBoxPhone = New TextBox()
        Me.TextBoxGithubLink = New TextBox()
        Me.TextBoxStopwatchTime = New TextBox()

        ' Set the properties of the controls
        Me.ButtonSubmit.Location = New Point(100, 200)
        Me.ButtonSubmit.Text = "Submit"

        Me.TextBoxName.Location = New Point(100, 50)
        Me.TextBoxEmail.Location = New Point(100, 80)
        Me.TextBoxPhone.Location = New Point(100, 110)
        Me.TextBoxGithubLink.Location = New Point(100, 140)
        Me.TextBoxStopwatchTime.Location = New Point(100, 170)

        ' Add the controls to the form
        Me.Controls.Add(Me.ButtonSubmit)
        Me.Controls.Add(Me.TextBoxName)
        Me.Controls.Add(Me.TextBoxEmail)
        Me.Controls.Add(Me.TextBoxPhone)
        Me.Controls.Add(Me.TextBoxGithubLink)
        Me.Controls.Add(Me.TextBoxStopwatchTime)
    End Sub

    Private Async Sub ButtonSubmit_Click(sender As Object, e As EventArgs) Handles ButtonSubmit.Click
        Dim newSubmission As New Submission With {
            .name = TextBoxName.Text,
            .email = TextBoxEmail.Text,
            .phone = TextBoxPhone.Text,
            .github_link = TextBoxGithubLink.Text,
            .stopwatch_time = TextBoxStopwatchTime.Text
        }

        Await SubmitSubmissionAsync(newSubmission)
    End Sub

    Private Async Function SubmitSubmissionAsync(submission As Submission) As Task
        Try
            Using client As New HttpClient()
                Dim json As String = JsonConvert.SerializeObject(submission)
                Dim content As New StringContent(json, Encoding.UTF8, "application/json")

                Dim response As HttpResponseMessage = Await client.PostAsync("http://localhost:3000/submit", content)

                If response.IsSuccessStatusCode Then
                    MessageBox.Show("Submission successful!")
                Else
                    MessageBox.Show("Failed to submit.")
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show($"Error: {ex.Message}")
        End Try
    End Function
End Class