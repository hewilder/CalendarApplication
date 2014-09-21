Imports MySql.Data
Imports MySql.Data.MySqlClient

Public Class DayView
    Public myDate As Date
    Dim ctlList As Dictionary(Of String, Control) = New Dictionary(Of String, Control)
    Public dateString As String

    Public Sub New(ByVal dateValue As Date)
        InitializeComponent()
        myDate = dateValue
        lblDate.Text = myDate.ToString("MMMM d, yyyy")
        lblDayOfWeek.Text = myDate.DayOfWeek.ToString()
    End Sub

    'Returns a collection of data rows from the database
    'Values can be retrieved as follows 
    '   returnedCollection.Items(IndexOfRow).Item("nameOfColumn")
    '   - OR -
    'You can do something similar to this, which is used in MonthView
    '     Dim row As DataRow
    '     For Each row In returnedCollection
    '          (Label the calendar boxes)
    '          labelCalendar(row("startOffset"), row("numDays"))
    '          (Put the month and year on the calendar) 
    '          lblMonthName.Text = row("name").ToString() + " " + row("year").ToString()
    '     Next
    '***Return values from this function should be checked for null references as I could not make an empty DataRowCollection****

    Public Function getEvents() As DataRowCollection
        'Dim connectionString As String = "Server=orion.csl.mtu.edu; Database=hewample; Uid=hewample;Pwd=hewample" 
        Dim connectionString As String = "Server=localhost; Database=calendar; Uid=root; Pwd=teamsoftware"
        Dim connection As New MySqlConnection(connectionString)
        Dim da As New MySqlDataAdapter
        Dim dt As DataSet = New DataSet

        Try
            connection.Open()
            Dim sqlComm As New MySqlCommand

            'Construct the query and open the connection
            sqlComm.CommandText = "SELECT * FROM events WHERE startDate = '" + formatDate(dateString) + "' ORDER BY startTime;"

            sqlComm.Connection = connection
            da.SelectCommand = sqlComm

            'Get the data
            da.Fill(dt, "events")
            connection.Close()

            'Get the information from the returned data
            Return dt.Tables(0).Rows


            'Catch any errors that occur and close the connection
        Catch ex As Exception
            If (connection.State = Data.ConnectionState.Open) Then
                connection.Close()
            End If

            MessageBox.Show("Events for this day could not be retrieved, the following error occurred:" + Environment.NewLine + ex.Message, "Event Retreival Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Function

    Private Function formatDate(unformatted As String) As String
        Dim dateArr As String() = unformatted.Split("/")
        Return (dateArr(2) + "/" + dateArr(0) + "/" + dateArr(1))

    End Function

    Private Sub afterSuccessfulDelete(startDate As String)
        Dim dayViewForm As DayView
        Dim closeForm As DayView = Nothing
        Dim loc As Point
        For Each frm In My.Application.OpenForms
            Try
                dayViewForm = DirectCast(frm, DayView)
                If (dayViewForm.myDate = startDate) Then
                    closeForm = dayViewForm
                End If
            Catch ex As Exception

            End Try

        Next

        If (Not IsNothing(closeForm)) Then
            loc = New Point(closeForm.Location.X, closeForm.Location.Y)
            closeForm.Close()
        End If

        Dim dateArr As String() = startDate.Split("/")
        Dim newDayViewForm As DayView = New DayView(New Date(dateArr(2), dateArr(0), dateArr(1)))

        'Open day view form
        Call newDayViewForm.Show()
        newDayViewForm.Location = loc

        'Close add event form
        Me.Close()
    End Sub

    Public Function deleteEvent(eventId As String) As Integer
        'Dim connectionString As String = "Server=orion.csl.mtu.edu; Database=hewample; Uid=hewample;Pwd=hewample" 
        Dim connectionString As String = "Server=localhost; Database=calendar; Uid=root; Pwd=teamsoftware"
        Dim connection As New MySqlConnection(connectionString)
        Dim da As New MySqlDataAdapter
        Dim dt As DataSet = New DataSet

        Try
            connection.Open()
            Dim sqlComm As New MySqlCommand

            'Construct the query and open the connection
            sqlComm.CommandText = "DELETE FROM events WHERE id = " + eventId + ";"

            sqlComm.Connection = connection
            sqlComm.ExecuteNonQuery()

            connection.Close()

            'Catch any errors that occur and close the connection
        Catch ex As Exception
            If (connection.State = Data.ConnectionState.Open) Then
                connection.Close()
            End If

            MessageBox.Show("Event was not deleted, the following error occurred:" + Environment.NewLine + ex.Message, "Event Deletion Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

            Return 1
        End Try

        Return 0
    End Function

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Take care of formatting issues
        Dim startDateArr() As String = myDate.Date.ToString().Split
        dateString = startDateArr(0)

        Dim events As DataRowCollection = getEvents()

        If (Not IsNothing(events)) Then
            addCtrls(events.Count, events)
        End If



    End Sub


    Private Sub addCtrls(numEvents As Integer, events As DataRowCollection)
        btnAddEvent.BackColor = Color.White

        Dim startLabels(numEvents) As Label
        Dim endLabels(numEvents) As Label
        Dim endDateLabels(numEvents) As Label
        Dim titleLabels(numEvents) As Label
        Dim descTexts(numEvents) As TextBox
        Dim updateButtons(numEvents) As Button
        Dim deleteButtons(numEvents) As Button

        Dim topOffset As Integer = 24
        Dim leftOffset As Integer = 17

        For counter As Integer = 0 To (numEvents - 1)
            startLabels(counter) = New Label
            startLabels(counter).BackColor = Color.White
            startLabels(counter).BorderStyle = BorderStyle.None
            startLabels(counter).MinimumSize = New Size(68, 20)
            startLabels(counter).TextAlign = ContentAlignment.BottomCenter
            startLabels(counter).Font = New Font("Microsoft Sans Serif", 12, FontStyle.Bold)
            startLabels(counter).Location = New Point(leftOffset, (topOffset + (100 * counter)))
            startLabels(counter).Tag = events.Item(counter).Item("id").ToString()
            Dim startTime As Date = DateTime.Parse(events.Item(counter).Item("startTime").ToString())
            Dim startTimeArr() As String = startTime.ToString.Split(" ")
            startLabels(counter).Text = (startTime.Hour Mod 12).ToString + ":" + startTime.Minute.ToString + " " + startTimeArr(2)
            Panel1.Controls.Add(startLabels(counter))
            ctlList.Add("start" + startLabels(counter).Tag, startLabels(counter))

            endLabels(counter) = New Label
            endLabels(counter).BackColor = Color.White
            endLabels(counter).BorderStyle = BorderStyle.None
            endLabels(counter).MinimumSize = New Size(68, 20)
            endLabels(counter).TextAlign = ContentAlignment.BottomCenter
            endLabels(counter).Font = New Font("Microsoft Sans Serif", 12, FontStyle.Bold)
            endLabels(counter).ForeColor = Color.DarkGray
            endLabels(counter).Location = New Point(leftOffset + 20, (topOffset + 25 + (100 * counter)))
            endLabels(counter).Tag = events.Item(counter).Item("id").ToString()
            Dim endTime As Date = DateTime.Parse(events.Item(counter).Item("endTime").ToString())
            Dim endTimeArr() As String = endTime.ToString.Split(" ")
            endLabels(counter).Text = (endTime.Hour Mod 12).ToString + ":" + endTime.Minute.ToString + " " + endTimeArr(2)
            Panel1.Controls.Add(endLabels(counter))
            ctlList.Add("end" + endLabels(counter).Tag, endLabels(counter))

            endDateLabels(counter) = New Label
            endDateLabels(counter).BackColor = Color.White
            endDateLabels(counter).BorderStyle = BorderStyle.None
            endDateLabels(counter).MinimumSize = New Size(68, 20)
            endDateLabels(counter).TextAlign = ContentAlignment.TopCenter
            endDateLabels(counter).Font = New Font("Microsoft Sans Serif", 9, FontStyle.Bold)
            endDateLabels(counter).ForeColor = Color.DarkGray
            endDateLabels(counter).Location = New Point(leftOffset + 20, (topOffset + 45 + (100 * counter)))
            endDateLabels(counter).Tag = events.Item(counter).Item("id").ToString()
            Dim endDate As Date = DateTime.Parse(events.Item(counter).Item("endDate").ToString())
            Dim startDate As Date = DateTime.Parse(events.Item(counter).Item("startDate").ToString())
            If (endDate <> startDate) Then
                endDateLabels(counter).Text = "(" + endDate.Month.ToString + "/" + endDate.Day.ToString + "/" + endDate.Year.ToString + ")"
            End If
            Panel1.Controls.Add(endDateLabels(counter))
            ctlList.Add("endDate" + endDateLabels(counter).Tag, endDateLabels(counter))

            titleLabels(counter) = New Label
            titleLabels(counter).BackColor = Color.White
            titleLabels(counter).BorderStyle = BorderStyle.None
            titleLabels(counter).MinimumSize = New Size(300, 20)
            titleLabels(counter).TextAlign = ContentAlignment.BottomLeft
            titleLabels(counter).Font = New Font("Microsoft Sans Serif", 12, FontStyle.Bold)
            titleLabels(counter).Location = New Point(leftOffset + 130, (topOffset + (100 * counter)))
            titleLabels(counter).Tag = events.Item(counter).Item("id").ToString()
            titleLabels(counter).Text = events.Item(counter).Item("title").ToString()
            Panel1.Controls.Add(titleLabels(counter))
            ctlList.Add("title" + titleLabels(counter).Tag, titleLabels(counter))

            descTexts(counter) = New TextBox
            descTexts(counter).BackColor = Color.White
            descTexts(counter).BorderStyle = BorderStyle.None
            descTexts(counter).MinimumSize = New Size(400, 40)
            descTexts(counter).Font = New Font("Microsoft Sans Serif", 12, FontStyle.Bold)
            descTexts(counter).ForeColor = Color.DarkGray
            descTexts(counter).Location = New Point(leftOffset + 133, (topOffset + 27 + (100 * counter)))
            descTexts(counter).Text = events.Item(counter).Item("details").ToString()
            descTexts(counter).ScrollBars = ScrollBars.Vertical
            descTexts(counter).Multiline = True
            descTexts(counter).ReadOnly = True
            descTexts(counter).Tag = events.Item(counter).Item("id").ToString()
            AddHandler descTexts(counter).GotFocus, AddressOf Me.textboxFocus
            Panel1.Controls.Add(descTexts(counter))
            ctlList.Add("desc" + descTexts(counter).Tag, descTexts(counter))

            updateButtons(counter) = New Button
            updateButtons(counter).BackColor = Color.White
            updateButtons(counter).MinimumSize = New Size(70, 20)
            updateButtons(counter).TextAlign = ContentAlignment.BottomCenter
            updateButtons(counter).Font = New Font("Microsoft Sans Serif", 8.25, FontStyle.Regular)
            updateButtons(counter).Location = New Point(600, (25 + (100 * counter)))
            updateButtons(counter).Text = "Update"
            updateButtons(counter).Tag = events.Item(counter).Item("id").ToString()
            AddHandler updateButtons(counter).Click, AddressOf Me.update_click
            Panel1.Controls.Add(updateButtons(counter))
            ctlList.Add("update" + updateButtons(counter).Tag, updateButtons(counter))

            deleteButtons(counter) = New Button
            deleteButtons(counter).BackColor = Color.White
            deleteButtons(counter).MinimumSize = New Size(70, 20)
            deleteButtons(counter).TextAlign = ContentAlignment.BottomCenter
            deleteButtons(counter).Font = New Font("Microsoft Sans Serif", 8.25, FontStyle.Regular)
            deleteButtons(counter).Location = New Point(683, (25 + (100 * counter)))
            deleteButtons(counter).Tag = events.Item(counter).Item("id").ToString()
            deleteButtons(counter).Text = "Delete"
            AddHandler deleteButtons(counter).Click, AddressOf Me.delete_click
            Panel1.Controls.Add(deleteButtons(counter))
            ctlList.Add("delete" + deleteButtons(counter).Tag, deleteButtons(counter))

        Next

    End Sub

    Private Sub btnAddEvent_Click(sender As Object, e As EventArgs) Handles btnAddEvent.Click
        Dim newAddEvent As New AddEvent(myDate)
        Call newAddEvent.Show()
    End Sub

    Private Sub update_click(sender As Object, e As EventArgs)
        Dim upButton As Button
        upButton = CType(sender, Button)
        Dim newUpdateEvent As New UpdateEvent(upButton.Tag)
        Call newUpdateEvent.Show()
    End Sub

    Private Sub textboxFocus(sender As Object, e As EventArgs)
        btnAddEvent.Focus()
    End Sub

    Private Sub delete_click(sender As Object, e As EventArgs)
        Dim delButton As Button
        delButton = DirectCast(sender, Button)
        Dim id As String = delButton.Tag

        Dim deleteConfirm As System.Windows.Forms.DialogResult = MessageBox.Show("Are you sure you would like to delete: " + Environment.NewLine + ctlList.Item("title" + id).Text, "Confirm event deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
        If (deleteConfirm = Windows.Forms.DialogResult.No) Then
            Return
        End If

        Dim result As Integer = deleteEvent(delButton.Tag.ToString())
        If (result = 0) Then
            Dim startDateArr() As String = myDate.Date.ToString().Split
            Dim startDate As String = startDateArr(0)

            afterSuccessfulDelete(startDate)
        End If

    End Sub
End Class