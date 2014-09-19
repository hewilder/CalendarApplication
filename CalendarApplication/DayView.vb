Imports MySql.Data
Imports MySql.Data.MySqlClient

Public Class DayView
    Public myDate As Date
    Dim ctlList As Dictionary(Of String, Control) = New Dictionary(Of String, Control)

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
        Dim connectionString As String = "Server=orion.csl.mtu.edu; Database=hewample; Uid=hewample;Pwd=hewample"
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
        Dim connectionString As String = "Server=orion.csl.mtu.edu; Database=hewample; Uid=hewample;Pwd=hewample"
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
        Dim events As DataRowCollection = getEvents()

        addCtrls(events.Count - 1, events)

        'Dim row As DataRow
        'For Each row In getEvents()

        'Next

    End Sub


    Private Sub addCtrls(numEvents As Integer, events As DataRowCollection)
        Dim startLabels(numEvents) As Label
        Dim endLabels(numEvents) As Label
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
            startLabels(counter).Text = events.Item(counter).Item("startTime")
            Panel1.Controls.Add(startLabels(counter))

            endLabels(counter) = New Label
            endLabels(counter).BackColor = Color.White
            endLabels(counter).BorderStyle = BorderStyle.None
            endLabels(counter).MinimumSize = New Size(68, 20)
            endLabels(counter).TextAlign = ContentAlignment.BottomCenter
            endLabels(counter).Font = New Font("Microsoft Sans Serif", 12, FontStyle.Bold)
            endLabels(counter).Location = New Point(leftOffset, (topOffset + 30 + (100 * counter)))
            endLabels(counter).Text = events.Item(counter).Item("endTime")
            Panel1.Controls.Add(endLabels(counter))

            titleLabels(counter) = New Label
            titleLabels(counter).BackColor = Color.White
            titleLabels(counter).BorderStyle = BorderStyle.None
            titleLabels(counter).MinimumSize = New Size(68, 20)
            titleLabels(counter).TextAlign = ContentAlignment.BottomCenter
            titleLabels(counter).Font = New Font("Microsoft Sans Serif", 12, FontStyle.Bold)
            titleLabels(counter).Location = New Point(leftOffset + 120, (topOffset + (100 * counter)))
            titleLabels(counter).Text = events.Item(counter).Item("title")
            Panel1.Controls.Add(titleLabels(counter))

            descTexts(counter) = New TextBox
            descTexts(counter).BackColor = Color.White
            descTexts(counter).BorderStyle = BorderStyle.None
            descTexts(counter).MinimumSize = New Size(68, 20)
            descTexts(counter).Font = New Font("Microsoft Sans Serif", 12, FontStyle.Bold)
            descTexts(counter).Location = New Point(leftOffset + 120, (topOffset + 30 + (100 * counter)))
            descTexts(counter).Text = events.Item(counter).Item("description")
            descTexts(counter).ScrollBars = ScrollBars.Vertical
            descTexts(counter).ReadOnly = True
            Panel1.Controls.Add(descTexts(counter))

            updateButtons(counter) = New Button
            updateButtons(counter).BackColor = Color.White
            updateButtons(counter).MinimumSize = New Size(68, 20)
            updateButtons(counter).TextAlign = ContentAlignment.BottomCenter
            updateButtons(counter).Font = New Font("Microsoft Sans Serif", 8.25, FontStyle.Regular)
            updateButtons(counter).Location = New Point(400, (30 + (100 * counter)))
            updateButtons(counter).Text = "Update"
            AddHandler updateButtons(counter).Click, AddressOf Me.update_click
            Panel1.Controls.Add(updateButtons(counter))

            deleteButtons(counter) = New Button
            deleteButtons(counter).BackColor = Color.White
            deleteButtons(counter).MinimumSize = New Size(68, 20)
            deleteButtons(counter).TextAlign = ContentAlignment.BottomCenter
            deleteButtons(counter).Font = New Font("Microsoft Sans Serif", 8.25, FontStyle.Regular)
            deleteButtons(counter).Location = New Point(500, (30 + (100 * counter)))
            deleteButtons(counter).Text = "Delete"
            AddHandler deleteButtons(counter).Click, AddressOf Me.delete_click
            Panel1.Controls.Add(deleteButtons(counter))
        Next

    End Sub

    Private Sub btnAddEvent_Click(sender As Object, e As EventArgs) Handles btnAddEvent.Click
        Dim newAddEvent As New AddEvent(myDate)
        Call newAddEvent.Show()
    End Sub

    Private Sub update_click(sender As Object, e As EventArgs)
        'Dim newUpdateEvent As New UpdateEvent(eventId)
        'Call newUpdateEvent.Show()
    End Sub

    Private Sub delete_click(sender As Object, e As EventArgs)
        'Dim newUpdateEvent As New UpdateEvent(eventId)
        'Call newUpdateEvent.Show()
    End Sub
End Class