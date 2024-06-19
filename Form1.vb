Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Net.Http
Imports System.Text.Json
Imports System.Text
Imports System.Threading.Tasks


Public Class Form1
    ' Initialize the UI
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeUI()
    End Sub

    ' UI elements
    Private WithEvents btnViewSubmissions As Button
    Private WithEvents btnCreateNewSubmission As Button

    ' Form View Submissions
    Private WithEvents FormViewSubmissions As Form
    Private WithEvents btnPrevious As Button
    Private WithEvents btnNext As Button
    Private WithEvents lblName As Label
    Private WithEvents lblEmail As Label
    Private WithEvents lblPhoneNum As Label
    Private WithEvents lblGithubLink As Label
    Private WithEvents lblStopwatchTime As Label
    Private WithEvents txtName As TextBox
    Private WithEvents txtEmail As TextBox
    Private WithEvents txtPhoneNum As TextBox
    Private WithEvents txtGithubLink As TextBox
    Private WithEvents txtStopwatchTime As TextBox
    Private currentIndex As Integer = 0

    ' Form Create New Submission
    Private WithEvents FormCreateNewSubmission As Form
    Private WithEvents txtName2 As TextBox
    Private WithEvents txtEmail2 As TextBox
    Private WithEvents txtPhoneNum2 As TextBox
    Private WithEvents txtGithubLink2 As TextBox
    Private WithEvents btnSubmit As Button
    Private WithEvents btnToggleStopwatch As Button
    Private WithEvents lblStopwatchTime2 As Label
    Private stopwatch As New Stopwatch
    Private timer As New Timers.Timer

    ' Backend API endpoints
    Private Const apiBaseUrl As String = "http://localhost:3000"
    Private Const apiPingUrl As String = apiBaseUrl & "/ping"
    Private Const apiSubmitUrl As String = apiBaseUrl & "/submit"
    Private Const apiReadUrl As String = apiBaseUrl & "/read"

    ' Initialize the UI
    Private Sub InitializeUI()
        ' Form1
        Me.Text = "Slidely Task 2"
        Me.Size = New Size(400, 200)

        ' Buttons
        btnViewSubmissions = New Button() With {
            .Text = "View Submissions (Ctrl + V)",
            .Location = New Point(50, 50),
            .Size = New Size(300, 30),
            .BackColor = Color.FromArgb(255, 192, 128) 
        }
        Me.Controls.Add(btnViewSubmissions)

        btnCreateNewSubmission = New Button() With {
            .Text = "Create New Submission (Ctrl + N)",
            .Location = New Point(50, 100),
            .Size = New Size(300, 30),
            .BackColor = Color.FromArgb(128, 192, 255)
        }
        Me.Controls.Add(btnCreateNewSubmission)

    Dim labelExample As New Label() With {
        .Location = New Point(50,10),
        .AutoSize = True, ' Adjusts size based on content
        .Text = "Delvin Joseph, Slidely Task 2- Slidely Form App"
    }
    Me.Controls.Add(labelExample)

        ' Add keyboard shortcuts
        AddHandler btnViewSubmissions.KeyDown, AddressOf btnViewSubmissions_KeyDown
        AddHandler btnCreateNewSubmission.KeyDown, AddressOf btnCreateNewSubmission_KeyDown

        ' Create FormViewSubmissions
        CreateFormViewSubmissions()

        ' Create FormCreateNewSubmission
        CreateFormCreateNewSubmission()
    End Sub

    ' Handle keyboard shortcuts
    Private Sub btnViewSubmissions_KeyDown(sender As Object, e As KeyEventArgs) Handles btnViewSubmissions.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.V Then
            btnViewSubmissions_Click(sender, e)
        End If
    End Sub

    Private Sub btnCreateNewSubmission_KeyDown(sender As Object, e As KeyEventArgs) Handles btnCreateNewSubmission.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.N Then
            btnCreateNewSubmission_Click(sender, e)
        End If
    End Sub

    ' Create FormViewSubmissions
    Private Sub CreateFormViewSubmissions()
        FormViewSubmissions = New Form() With {
            .Text = "Slidely Task 2 - View Submissions",
            .Size = New Size(400, 300),
            .StartPosition = FormStartPosition.CenterScreen
        }

        Dim labelExample As New Label() With {
            .Location = New Point(50,10),
            .AutoSize = True, ' Adjusts size based on content
            .Text = "Delvin Joseph, Slidely Task 2- View Submission"
        }
        FormViewSubmissions.Controls.Add(labelExample)

        ' Labels
        lblName = New Label() With {
            .Text = "Name:",
            .Location = New Point(50, 50),
            .AutoSize = True
        }
        FormViewSubmissions.Controls.Add(lblName)

        lblEmail = New Label() With {
            .Text = "Email:",
            .Location = New Point(50, 80),
            .AutoSize = True
        }
        FormViewSubmissions.Controls.Add(lblEmail)

        lblPhoneNum = New Label() With {
            .Text = "Phone Number:",
            .Location = New Point(50, 110),
            .AutoSize = True
        }
        FormViewSubmissions.Controls.Add(lblPhoneNum)

        lblGithubLink = New Label() With {
            .Text = "Github Link:",
            .Location = New Point(50, 140),
            .AutoSize = True
        }
        FormViewSubmissions.Controls.Add(lblGithubLink)

        lblStopwatchTime = New Label() With {
            .Text = "Stopwatch Time:",
            .Location = New Point(50, 170),
            .AutoSize = True
        }
        FormViewSubmissions.Controls.Add(lblStopwatchTime)

        ' Text boxes (read-only)
        txtName = New TextBox() With {
            .Location = New Point(150, 50),
            .Size = New Size(200, 20),
            .ReadOnly = True
        }
        FormViewSubmissions.Controls.Add(txtName)

        txtEmail = New TextBox() With {
            .Location = New Point(150, 80),
            .Size = New Size(200, 20),
            .ReadOnly = True
        }
        FormViewSubmissions.Controls.Add(txtEmail)

        txtPhoneNum = New TextBox() With {
            .Location = New Point(150, 110),
            .Size = New Size(200, 20),
            .ReadOnly = True
        }
        FormViewSubmissions.Controls.Add(txtPhoneNum)

        txtGithubLink = New TextBox() With {
            .Location = New Point(150, 140),
            .Size = New Size(200, 20),
            .ReadOnly = True
        }
        FormViewSubmissions.Controls.Add(txtGithubLink)

        txtStopwatchTime = New TextBox() With {
            .Location = New Point(150, 170),
            .Size = New Size(200, 20),
            .ReadOnly = True
        }
        FormViewSubmissions.Controls.Add(txtStopwatchTime)

        ' Buttons
        btnPrevious = New Button() With {
            .Text = "Previous (Ctrl + P)",
            .Location = New Point(50, 220),
            .Size = New Size(100, 30),
            .BackColor = Color.FromArgb(255, 192, 128) 
        }
        FormViewSubmissions.Controls.Add(btnPrevious)

        btnNext = New Button() With {
            .Text = "Next (Ctrl + N)",
            .Location = New Point(250, 220),
            .Size = New Size(100, 30),
            .BackColor = Color.FromArgb(128, 192, 255)
        }
        FormViewSubmissions.Controls.Add(btnNext)

        ' Add keyboard shortcuts
        AddHandler btnPrevious.KeyDown, AddressOf btnPrevious_KeyDown
        AddHandler btnNext.KeyDown, AddressOf btnNext_KeyDown

        ' Load the first submission
        LoadSubmission(currentIndex)
    End Sub

    ' Handle keyboard shortcuts
    Private Sub btnPrevious_KeyDown(sender As Object, e As KeyEventArgs) Handles btnPrevious.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.P Then
            btnPrevious_Click(sender, e)
        End If
    End Sub
    Private Sub btnNext_KeyDown(sender As Object, e As KeyEventArgs) Handles btnNext.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.N Then
            btnNext_Click(sender, e)
        End If
    End Sub

    ' Create FormCreateNewSubmission
    Private Sub CreateFormCreateNewSubmission()
        FormCreateNewSubmission = New Form() With {
            .Text = "Slidely Task 2 - Create Submission",
            .Size = New Size(400, 300),
            .StartPosition = FormStartPosition.CenterScreen
        }
        
        Dim labelExample As New Label() With {
            .Location = New Point(50,10),
            .AutoSize = True, ' Adjusts size based on content
            .Text = "Delvin Joseph, Slidely Task 2- Create Submission"
        }
        FormCreateNewSubmission.Controls.Add(labelExample)
        ' Labels
        lblName = New Label() With {
            .Text = "Name:",
            .Location = New Point(50, 50),
            .AutoSize = True
        }
        FormCreateNewSubmission.Controls.Add(lblName)

        lblEmail = New Label() With {
            .Text = "Email:",
            .Location = New Point(50, 80),
            .AutoSize = True
        }
        FormCreateNewSubmission.Controls.Add(lblEmail)

        lblPhoneNum = New Label() With {
            .Text = "Phone Number:",
            .Location = New Point(50, 110),
            .AutoSize = True
        }
        FormCreateNewSubmission.Controls.Add(lblPhoneNum)

        lblGithubLink = New Label() With {
            .Text = "Github Link:",
            .Location = New Point(50, 140),
            .AutoSize = True
        }
        FormCreateNewSubmission.Controls.Add(lblGithubLink)

        lblStopwatchTime2 = New Label() With {
            .Text = "Stopwatch Time:",
            .Location = New Point(50, 170),
            .AutoSize = True
        }
        FormCreateNewSubmission.Controls.Add(lblStopwatchTime2)

        ' Text boxes
        txtName2 = New TextBox() With {
            .Location = New Point(150, 50),
            .Size = New Size(200, 20)
        }
        FormCreateNewSubmission.Controls.Add(txtName2)

        txtEmail2 = New TextBox() With {
            .Location = New Point(150, 80),
            .Size = New Size(200, 20)
        }
        FormCreateNewSubmission.Controls.Add(txtEmail2)

        txtPhoneNum2 = New TextBox() With {
            .Location = New Point(150, 110),
            .Size = New Size(200, 20)
        }
        FormCreateNewSubmission.Controls.Add(txtPhoneNum2)

        txtGithubLink2 = New TextBox() With {
            .Location = New Point(150, 140),
            .Size = New Size(200, 20)
        }
        FormCreateNewSubmission.Controls.Add(txtGithubLink2)

        ' Buttons
        btnSubmit = New Button() With {
            .Text = "Submit",
            .Location = New Point(150, 220),
            .Size = New Size(100, 30),
            .BackColor = Color.FromArgb(128, 192, 255)
        }
        FormCreateNewSubmission.Controls.Add(btnSubmit)

        btnToggleStopwatch = New Button() With {
            .Text = "Start/Stop Stopwatch",
            .Location = New Point(50, 220),
            .Size = New Size(100, 30),
            .BackColor = Color.FromArgb(255, 192, 128) 
        }
        FormCreateNewSubmission.Controls.Add(btnToggleStopwatch)

        ' Add stopwatch event handler
        AddHandler btnToggleStopwatch.Click, AddressOf btnToggleStopwatch_Click

        ' Initialize timer
        timer.Interval = 1000
        AddHandler timer.Elapsed, AddressOf TimerElapsed
    End Sub

    ' Toggle stopwatch
    Private Sub btnToggleStopwatch_Click(sender As Object, e As EventArgs)
        If stopwatch.IsRunning Then
            stopwatch.Stop()
            timer.Stop()
        Else
            stopwatch.Start()
            timer.Start()
        End If
    End Sub

    ' Timer elapsed event
    Private Sub TimerElapsed(sender As Object, e As Timers.ElapsedEventArgs)
        If lblStopwatchTime2.InvokeRequired Then
            lblStopwatchTime2.Invoke(New MethodInvoker(Sub() lblStopwatchTime2.Text = stopwatch.Elapsed.ToString("hh\:mm\:ss")))
        Else
            lblStopwatchTime2.Text = stopwatch.Elapsed.ToString("hh\:mm\:ss")
        End If
    End Sub

    ' Load submission
    Private Async Sub LoadSubmission(index As Integer)
        Try
            Using client As New HttpClient()
                Dim response = Await client.GetAsync($"{apiReadUrl}?index={index}")
                If response.IsSuccessStatusCode Then
                    Dim jsonString = Await response.Content.ReadAsStringAsync()
                    Dim submission = JsonSerializer.Deserialize(Of Submission)(jsonString)
                    If submission IsNot Nothing Then
                        txtName.Text = submission.name
                        txtEmail.Text = submission.email
                        txtPhoneNum.Text = submission.phone
                        txtGithubLink.Text = submission.github_link
                        txtStopwatchTime.Text = submission.stopwatch_time
                    End If
                Else
                    MessageBox.Show("Failed to load submission.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show("An error occurred while loading submission: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Previous button click event
    Private Sub btnPrevious_Click(sender As Object, e As EventArgs) Handles btnPrevious.Click
        If currentIndex > 0 Then
            currentIndex -= 1
            LoadSubmission(currentIndex)
        End If
    End Sub

    ' Next button click event
    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        currentIndex += 1
        LoadSubmission(currentIndex)
    End Sub

    ' View Submissions button click event
    Private Sub btnViewSubmissions_Click(sender As Object, e As EventArgs) Handles btnViewSubmissions.Click
        FormViewSubmissions.ShowDialog()
    End Sub

    ' Create New Submission button click event
    Private Sub btnCreateNewSubmission_Click(sender As Object, e As EventArgs) Handles btnCreateNewSubmission.Click
        FormCreateNewSubmission.ShowDialog()
    End Sub

    ' Submit button click event
    Private Async Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Try
            Dim newSubmission As New Submission With {
                .name = txtName2.Text,
                .email = txtEmail2.Text,
                .phone = txtPhoneNum2.Text,
                .github_link = txtGithubLink2.Text,
                .stopwatch_time = lblStopwatchTime2.Text
            }

            Dim jsonContent = New StringContent(JsonSerializer.Serialize(newSubmission), Encoding.UTF8, "application/json")

            Using client As New HttpClient()
                Dim response = Await client.PostAsync(apiSubmitUrl, jsonContent)
                If response.IsSuccessStatusCode Then
                    MessageBox.Show("Submission successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    MessageBox.Show("Failed to submit.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show("An error occurred while submitting: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class

Public Class Submission
    Public Property name As String
    Public Property email As String
    Public Property phone As String
    Public Property github_link As String
    Public Property stopwatch_time As String
End Class
