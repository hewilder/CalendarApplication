Imports MySql.Data
Imports MySql.Data.MySqlClient

Public Class DayView
    Public myDate As Date

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
        Dim connectionString As String = "Server=127.0.0.1; Database=calendar; Uid=root;Pwd=teamsoftware"
        Dim connection As New MySqlConnection(connectionString)
        Dim da As New MySqlDataAdapter
        Dim dt As DataSet = New DataSet

        Try
            connection.Open()
            Dim sqlComm As New MySqlCommand

            'Construct the query and open the connection
            sqlComm.CommandText = "SELECT * FROM events WHERE startDate = " + " ORDER BY startTime;"

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

        End Try
    End Function

    '
    Public Function deleteEvent(eventId As String) As Integer
        Dim connectionString As String = "Server=127.0.0.1; Database=calendar; Uid=root;Pwd=teamsoftware"
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

            Return 1
        End Try

        Return 0
    End Function

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub btnAddEvent_Click(sender As Object, e As EventArgs) Handles btnAddEvent.Click
        Dim newAddEvent As New AddEvent(myDate)
        Call newAddEvent.Show()
    End Sub

    Private Sub update_click(sender As Object, e As EventArgs)
        'Dim newUpdateEvent As New UpdateEvent(eventId)
        'Call newUpdateEvent.Show()
    End Sub
End Class