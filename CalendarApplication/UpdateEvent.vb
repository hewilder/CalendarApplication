Imports MySql.Data
Imports MySql.Data.MySqlClient

Public Class UpdateEvent
    Dim myId As Integer

    Public Sub New(eventId As Integer)
        InitializeComponent()

        myId = eventId
        'Make sure there are no other instances of a form editting the same event
        For Each frm In My.Application.OpenForms
            Try
                Dim updateForm As UpdateEvent = DirectCast(frm, UpdateEvent)
                If (updateForm IsNot (Me) And updateForm.myId = myId) Then
                    MessageBox.Show("Only one instance of of the update event window can be open for an event. Please close the other instance and try again.", "Multiple Instances Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    updateForm.Focus()
                    Me.Close()
                End If

                'Pull existing form data out of database
                getEventData()
            Catch ex As Exception

            End Try

        Next


    End Sub

    'Requries the event ID (obtained by constructor)
    '****Should be checked for null return, which is what it will return on error*****
    Private Function getEventData() As DataRow
        Dim connectionString As String = "Server=orion.csl.mtu.edu; Database=hewample; Uid=hewample;Pwd=hewample"
        'Dim connectionString As String = "Server=localhost; Database=calendar; Uid=root; Pwd=teamsoftware"
        Dim connection As New MySqlConnection(connectionString)
        Dim da As New MySqlDataAdapter
        Dim dt As DataSet = New DataSet

        Try
            connection.Open()
            Dim sqlComm As New MySqlCommand

            'Construct the query and open the connection
            sqlComm.CommandText = "SELECT * FROM events WHERE id = " + myId.ToString() + " ORDER BY startTime;"

            sqlComm.Connection = connection
            da.SelectCommand = sqlComm

            'Get the data
            da.Fill(dt, "events")
            connection.Close()

            'Get return the first row found (there should only be one since ID is the primary key)
            If (dt.Tables(0).Rows.Count > 0) Then
                Return dt.Tables(0).Rows(0)
            Else
                Return Nothing
            End If



            'Catch any errors that occur and close the connection
        Catch ex As Exception
            If (connection.State = Data.ConnectionState.Open) Then
                connection.Close()
            End If

            MessageBox.Show("Event information could not be retrieved, the following error occurred:" + Environment.NewLine + ex.Message, "Event Lookup Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

            Return Nothing

        End Try
    End Function

    'Takes a start date and time (together), takes a end date and time (together), a title, and a description
    Private Function updateEvent(startDateTime As Date, endDateTime As Date, title As String, details As String) As Integer

        'Take care of formatting issues
        Dim startDateArr() As String = startDateTime.Date.ToString().Split
        Dim startDate As String = startDateArr(0)


        Dim sep As Char() = {"."c}
        Dim startTimeArr() As String = startDateTime.TimeOfDay.ToString().Split(sep)
        Dim startTime As String = startTimeArr(0)

        Dim endDateArr() As String = endDateTime.Date.ToString().Split
        Dim endDate As String = endDateArr(0)

        Dim endTimeArr() As String = endDateTime.TimeOfDay.ToString().Split(sep)
        Dim endTime As String = endTimeArr(0)

        Dim connectionString As String = "Server=orion.csl.mtu.edu; Database=hewample; Uid=hewample;Pwd=hewample"
        'Dim connectionString As String = "Server=localhost; Database=calendar; Uid=root; Pwd=teamsoftware"
        Dim connection As New MySqlConnection(connectionString)
        Dim da As New MySqlDataAdapter
        Dim dt As DataSet = New DataSet

        Try
            connection.Open()
            Dim sqlComm As New MySqlCommand

            'Construct the query and open the connection
            sqlComm.CommandText = "UPDATE events SET title = '" + escapeMYSQL(title) + "', details = '" + escapeMYSQL(details) + "', startTime = TIME '" + startTime + "', startDate = '" + formatDate(startDate) + "', endTime = TIME '" + endTime + "', endDate = DATE '" + formatDate(endDate) + "' WHERE id = " + myId.ToString() + ";"

            sqlComm.Connection = connection
            sqlComm.ExecuteNonQuery()

            'Catch any errors that occur and close the connection
        Catch ex As Exception
            If (connection.State = Data.ConnectionState.Open) Then
                connection.Close()
            End If

            MessageBox.Show("Event was not saved, the following error occurred:" + Environment.NewLine + ex.Message, "Event Update Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

            'Return 1 if there was an error and we should keep the window open
            Return 1
        End Try

        'Return 0 if insert was successful and we should close the window and return to the day view
        Return 0
    End Function

    'Corrects the date format to be inserted into the database
    Private Function formatDate(unformatted As String) As String
        Dim dateArr As String() = unformatted.Split("/")
        Return (dateArr(2) + "/" + dateArr(0) + "/" + dateArr(1))

    End Function

    'Make sure to use this after a successful insert, it will close all instances of the form 
    'displaying the date on which this event was added and open a new one for the start date of the new event
    Private Sub afterSuccessfulUpdate(startDate As String)
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

        'Close update event form
        Me.Close()
    End Sub

    'Action occurs on form load 
    Private Sub AddEvent_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim row As DataRow = getEventData()
        Try
            txtTitle.Text = row.Item("title").ToString()
            txtDescription.Text = row.Item("details").ToString()
            dtpEndDate.Value = DateTime.Parse(row.Item("endDate").ToString())
            dtpEndTime.Value = DateTime.Parse(row.Item("endTime").ToString())
            dtpStartDate.Value = DateTime.Parse(row.Item("startDate").ToString())
            dtpStartTime.Value = DateTime.Parse(row.Item("startTime").ToString())
        Catch ex As Exception
            Dim str As String = ex.Message
        End Try
        

        
    End Sub

    'Action occurs on button click
    Private Sub btnSave_Click_1(sender As Object, e As EventArgs) Handles btnSave.Click

        If (IsNothing(dtpEndDate.Value)) Then
            MessageBox.Show("Please enter a value for the end date of this event", "Event Information Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If

        If (IsNothing(dtpEndTime.Value)) Then
            MessageBox.Show("Please enter a value for the end time of this event", "Event Information Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If

        If (IsNothing(dtpStartTime.Value)) Then
            MessageBox.Show("Please enter a value for the start time of this event", "Event Information Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If

        If (IsNothing(dtpStartDate.Value)) Then
            MessageBox.Show("Please enter a value for the start date of this event", "Event Information Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If

        If (dtpStartDate.Value > dtpEndDate.Value) Then
            MessageBox.Show("Your event appears to be ending before it starts, please check your input and resave", "Event Information Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If

        If (dtpStartDate.Value.Day = dtpEndDate.Value.Day) Then
            If (dtpStartTime.Value > dtpEndTime.Value) Then
                MessageBox.Show("Your event appears to be ending before it starts, please check your input and resave", "Event Information Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return
            End If
        End If

        Dim wholeStartDate As Date = New Date(dtpStartDate.Value.Year, dtpStartDate.Value.Month, dtpStartDate.Value.Day, dtpStartTime.Value.Hour, dtpStartTime.Value.Minute, dtpStartTime.Value.Second)
        Dim wholeEndDate As Date = New Date(dtpEndDate.Value.Year, dtpEndDate.Value.Month, dtpEndDate.Value.Day, dtpEndTime.Value.Hour, dtpEndTime.Value.Minute, dtpEndTime.Value.Second)

        Dim result As Integer = updateEvent(wholeStartDate, wholeEndDate, txtTitle.Text, txtDescription.Text)
        If (result = 0) Then
            Dim startDateArr() As String = wholeStartDate.Date.ToString().Split
            Dim startDate As String = startDateArr(0)

            afterSuccessfulUpdate(startDate)
        End If
    End Sub

    Public Shared Function escapeMYSQL(original As String) As String
        original = original.Replace("\", "\\")
        original = original.Replace("'", "\'")
        'original = original.Replace("""", "\""")

        Return original
    End Function
End Class