Imports MySql.Data
Imports MySql.Data.MySqlClient

Public Class UpdateEvent
    Dim myId As Integer

    Private Sub New(eventId As Integer)
        InitializeComponent()

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
        Dim connection As New MySqlConnection(connectionString)
        Dim da As New MySqlDataAdapter
        Dim dt As DataSet = New DataSet

        Try
            connection.Open()
            Dim sqlComm As New MySqlCommand

            'Construct the query and open the connection
            sqlComm.CommandText = "SELECT * FROM events WHERE id = " + myId + " ORDER BY startTime;"

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
    Private Function insertEvent(startDateTime As Date, endDateTime As Date, title As String, details As String) As Integer

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
        Dim connection As New MySqlConnection(connectionString)
        Dim da As New MySqlDataAdapter
        Dim dt As DataSet = New DataSet

        Try
            connection.Open()
            Dim sqlComm As New MySqlCommand

            'Construct the query and open the connection
            sqlComm.CommandText = "UPDATE events SET title = '" + title + "', details = '" + details + "', startTime = TIME '" + startTime + "', startDate = '" + formatDate(startDate) + "', endTime = TIME '" + endTime + "', endDate = DATE '" + formatDate(endDate) + "' WHERE id = " + myId + ";"

            sqlComm.Connection = connection
            sqlComm.ExecuteNonQuery()

            'Catch any errors that occur and close the connection
        Catch ex As Exception
            If (connection.State = Data.ConnectionState.Open) Then
                connection.Close()
            End If

            MessageBox.Show("Event was not saved, the following error occurred:" + Environment.NewLine + ex.Message, "Event Insertion Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

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
    Private Sub afterSuccessfulInsert(startDate As String)
        For Each frm In My.Application.OpenForms
            Try
                Dim dayViewForm As DayView = DirectCast(frm, DayView)
                If (dayViewForm.myDate = startDate) Then
                    dayViewForm.Close()
                End If
            Catch ex As Exception

            End Try

        Next

        Dim dateArr As String() = startDate.Split("/")
        Dim newDayViewForm As DayView = New DayView(New Date(dateArr(2), dateArr(0), dateArr(1)))
        'Open day view form
        Call newDayViewForm.Show()

        'Close add event form
        Me.Close()
    End Sub

    'Action occurs on form load 
    Private Sub AddEvent_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        
    End Sub

    'Action occurs on button click
    Private Sub btnSave_Click(sender As Object, e As EventArgs)
        '-------------------------------------------------------
        'Example of inserting a new event (remove after testing)
        '-------------------------------------------------------
        Dim result As Integer = insertEvent(DateTime.Now, DateTime.Now, "newEvent", "new event description")
        If (result = 0) Then
            Dim startDateArr() As String = DateTime.Now.Date.ToString().Split
            Dim startDate As String = startDateArr(0)

            afterSuccessfulInsert(startDate)
        End If
    End Sub
End Class