Imports MySql.Data
Imports MySql.Data.MySqlClient

Public Class AddEvent

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

        Dim connectionString As String = "Server=127.0.0.1; Database=calendar; Uid=root;Pwd=teamsoftware"
        Dim connection As New MySqlConnection(connectionString)
        Dim da As New MySqlDataAdapter
        Dim dt As DataSet = New DataSet

        Try
            connection.Open()
            Dim sqlComm As New MySqlCommand

            'Construct the query and open the connection
            sqlComm.CommandText = "INSERT INTO events SET title = '" + title + "', details = '" + details + "', startTime = TIME '" + startTime + "', startDate = '" + formatDate(startDate) + "', endTime = TIME '" + endTime + "', endDate = DATE '" + formatDate(endDate) + "';"

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

    Private Sub AddEvent_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        '-------------------------------------------------------
        'Example of inserting a new event (remove after testing)
        '-------------------------------------------------------
        insertEvent(DateTime.Now, DateTime.Now, "newEvent", "new event description")
    End Sub
End Class