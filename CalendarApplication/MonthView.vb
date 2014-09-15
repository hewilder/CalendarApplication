Imports MySql.Data
Imports MySql.Data.MySqlClient

Public Class MonthView
    Dim ctlList As Dictionary(Of String, Control) = New Dictionary(Of String, Control)

    Private Sub makeCalendar()
        Dim pictureboxes(41) As PictureBox
        Dim weekDayLabels(7) As Label
        Dim labels(41) As Label

        'How much to offset the calendar from top and left
        Dim topOffset As Integer = 165
        Dim leftOffset As Integer = 25

        'Generate labels for the weekdays 
        For counter As Integer = 0 To 6
            weekDayLabels(counter) = New Label
            weekDayLabels(counter).BackColor = Color.White
            weekDayLabels(counter).BorderStyle = BorderStyle.None
            weekDayLabels(counter).MinimumSize = New Size(121, 20)
            weekDayLabels(counter).TextAlign = ContentAlignment.BottomCenter
            weekDayLabels(counter).Font = New Font("Microsoft Sans Serif", 12, FontStyle.Bold)
            weekDayLabels(counter).Location = New Point(leftOffset + 131 * (counter Mod 7), topOffset - 30 + 75 * (counter \ 7))

            'Choose correct text
            If (counter = 0) Then
                weekDayLabels(counter).Text = "Sunday"
            ElseIf (counter = 1) Then
                weekDayLabels(counter).Text = "Monday"
            ElseIf (counter = 2) Then
                weekDayLabels(counter).Text = "Tuesday"
            ElseIf (counter = 3) Then
                weekDayLabels(counter).Text = "Wednesday"
            ElseIf (counter = 4) Then
                weekDayLabels(counter).Text = "Thursday"
            ElseIf (counter = 5) Then
                weekDayLabels(counter).Text = "Friday"
            ElseIf (counter = 6) Then
                weekDayLabels(counter).Text = "Saturday"
            End If

            'Add week day label to form
            Me.Controls.Add(weekDayLabels(counter))
        Next

        For counter As Integer = 0 To 41
            labels(counter) = New Label
            labels(counter).BackColor = Color.WhiteSmoke
            labels(counter).BorderStyle = BorderStyle.None
            labels(counter).MinimumSize = New Size(30, 30)
            labels(counter).TextAlign = ContentAlignment.TopRight
            labels(counter).Font = New Font("Microsoft Sans Serif", 12)
            labels(counter).Location = New Point(leftOffset + 10 + 131 * (counter Mod 7), topOffset + 5 + 75 * (counter \ 7))
            labels(counter).Tag = "label" + counter.ToString()
            labels(counter).Name = "label" + counter.ToString()
            labels(counter).Text = counter.ToString()
            Me.Controls.Add(labels(counter))

            'Add actions to picture boxes
            'Add click action
            AddHandler labels(counter).Click, AddressOf Me.labelClick
            'Add hover action
            AddHandler labels(counter).MouseHover, AddressOf Me.calHover
            AddHandler labels(counter).MouseLeave, AddressOf Me.calLeave

            'Add label to form
            ctlList.Add(labels(counter).Tag, labels(counter))

        Next

        'Add the picture boxes
        For counter As Integer = 0 To 41
            pictureboxes(counter) = New PictureBox
            pictureboxes(counter).BackColor = Color.WhiteSmoke
            pictureboxes(counter).BorderStyle = BorderStyle.FixedSingle
            pictureboxes(counter).Size = New Size(121, 65)
            pictureboxes(counter).Location = New Point(leftOffset + 131 * (counter Mod 7), topOffset + 75 * (counter \ 7))
            pictureboxes(counter).Name = "pbox" + counter.ToString()
            pictureboxes(counter).Tag = "pbox" + counter.ToString()
            Me.Controls.Add(pictureboxes(counter))

            'Add actions to picture boxes
            'Add click action
            AddHandler pictureboxes(counter).Click, AddressOf Me.pboxClick
            'Add hover action
            AddHandler pictureboxes(counter).MouseHover, AddressOf Me.calHover
            AddHandler pictureboxes(counter).MouseLeave, AddressOf Me.calLeave

            'Add picture box to form
            ctlList.Add(pictureboxes(counter).Tag, pictureboxes(counter))

        Next

    End Sub


    'Function that handles a click on a label
    Private Sub labelClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim label As Label = DirectCast(sender, Label)

        'Take action if th picture box was a valid day
        If (label.Text <> String.Empty) Then
            Dim formatDate As DateTime
            formatDate = DateTime.Parse(getLabelNum(label.Tag) + lblMonthName.Text)
            Dim dayViewForm As New DayView(formatDate)
            Call dayViewForm.Show()
        End If
       

    End Sub

    'Function handles a click on a picture box
    Private Sub pboxClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim pbox As PictureBox = DirectCast(sender, PictureBox)
        Dim label As Label = ctlList.Item("label" + getPBoxNum(pbox.Name))

        'Take action if the picture box represents a valid day
        If (label.Text <> String.Empty) Then
            Dim formatDate As DateTime
            formatDate = DateTime.Parse(getLabelNum(label.Tag) + lblMonthName.Text)
            Dim dayViewForm As New DayView(formatDate)
            Call dayViewForm.Show()
        End If
    End Sub

    'Function that handles the mouse entering a calendar box
    Private Sub calHover(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim control As Control = DirectCast(sender, Control)
        Dim tag As String = control.Tag
        Dim otherControl As Control
        Dim selColor As Color = Color.LightSteelBlue

        'Take action if the label represents a valid day
        If (tag.Contains("label")) Then
            otherControl = ctlList.Item("pbox" + getLabelNum(tag))
            If (control.Text <> String.Empty) Then
                otherControl.BackColor = selColor
                control.BackColor = selColor
            End If
        Else
            otherControl = ctlList.Item("label" + getPBoxNum(tag))
            If (otherControl.Text <> String.Empty) Then
                otherControl.BackColor = selColor
                control.BackColor = selColor
            End If
        End If

    End Sub

    'Function that handles the mouse leaving a calendar box
    Private Sub calLeave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim control As Control = DirectCast(sender, Control)
        Dim tag As String = control.Tag
        Dim otherControl As Control
        Dim selColor As Color = Color.WhiteSmoke

        If (tag.Contains("label")) Then
            otherControl = ctlList.Item("pbox" + getLabelNum(tag))
            If (control.Text <> String.Empty) Then
                otherControl.BackColor = selColor
                control.BackColor = selColor
            End If
        Else
            otherControl = ctlList.Item("label" + getPBoxNum(tag))
            If (otherControl.Text <> String.Empty) Then
                otherControl.BackColor = selColor
                control.BackColor = selColor
            End If
        End If
    End Sub

    'Get the number in the label's name
    Private Function getLabelNum(wholeName As String) As String
        Dim substring As String = wholeName.Substring(5)
        Return wholeName.Substring(5)
    End Function

    'Get the number in the picture box's name
    Private Function getPBoxNum(wholeName As String) As String
        Dim substring As String = wholeName.Substring(4)
        Return wholeName.Substring(4)
    End Function

    Private Sub labelCalendar(offset As Integer, numDays As Integer)
        Dim ctl As Control
        Dim dayNum As Integer = 1

        'Clear all the previous labels, make sure all the boxes are shown
        For counter As Integer = 0 To 41
            ctl = ctlList.Item("label" + counter.ToString())
            ctl.Text = ""
            ctl.Show()

            ctl = ctlList.Item("pbox" + counter.ToString())
            ctl.Show()
        Next

        'Number the days 
        For counter As Integer = offset To (offset + numDays - 1)
            ctl = ctlList.Item("label" + counter.ToString())
            ctl.Text = dayNum
            dayNum = dayNum + 1
        Next

        'Hide excess weeks if only 4 weeks are required 
        If ((offset + numDays - 1) < 28) Then
            For counter As Integer = 28 To 41
                ctl = ctlList.Item("pbox" + counter.ToString())
                ctl.Hide()
                ctl = ctlList.Item("label" + counter.ToString())
                ctl.Hide()
            Next

            'Hide excess weeks if only 5 weeks are required 
        ElseIf ((offset + numDays - 1) < 35) Then
            For counter As Integer = 35 To 41
                ctl = ctlList.Item("pbox" + counter.ToString())
                ctl.Hide()
                ctl = ctlList.Item("label" + counter.ToString())
                ctl.Hide()
            Next
        End If


    End Sub

    Private Sub labelCalForMonth(monthIndex As Integer, year As Integer, monthName As String, useIndex As Integer)
        Dim connectionString As String = "Server=127.0.0.1; Database=calendar; Uid=root;Pwd=teamsoftware"
        Dim connection As New MySqlConnection(connectionString)
        Dim da As New MySqlDataAdapter
        Dim dt As DataSet = New DataSet

        Try
            connection.Open()
            Dim sqlComm As New MySqlCommand

            'Construct the query and open the connection
            If (useIndex = 1) Then
                sqlComm.CommandText = "SELECT * FROM months WHERE monthIndex = " + monthIndex.ToString() + " AND year = " + year.ToString() + ";"
            Else
                sqlComm.CommandText = "SELECT * FROM months WHERE name = '" + monthName + "' AND year = " + year.ToString() + ";"
            End If

            sqlComm.Connection = connection
            da.SelectCommand = sqlComm

            'Get the data
            da.Fill(dt, "months")
            connection.Close()

            'Get the information from the returned data
            Dim row As DataRow
            Dim counter As Integer = 0
            For Each row In dt.Tables(0).Rows
                'Make sure we only process the top row if more than one is returned 
                If (counter = 0) Then

                    'Label the calendar boxes
                    labelCalendar(row("startOffset"), row("numDays"))

                    'Put the month and year on the calendar 
                    lblMonthName.Text = row("name").ToString() + " " + row("year").ToString()
                End If


                counter = counter + 1
            Next

            'Catch any errors that occur and close the connection
        Catch ex As Exception
            If (connection.State = Data.ConnectionState.Open) Then
                connection.Close()
            End If


        End Try
    End Sub

    Private Sub MonthView_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Get the current date
        Dim currentDate As Date = DateTime.Now()

        'Make the layout for the calendar (create all the necessary objects)
        makeCalendar()

        'Load the current month
        labelCalForMonth(currentDate.Month, currentDate.Year, "", 1)
        
    End Sub

    Private Sub btnDay_Click(sender As Object, e As EventArgs) Handles btnDay.Click
        'Create a new instance of the day view form, pass in the date
        Dim dayViewForm As New DayView(dtpDay.Value)

        'Bring up the new instance of the form
        Call dayViewForm.Show()
    End Sub

    Private Sub btnMonth_Click(sender As Object, e As EventArgs) Handles btnMonth.Click

        'Check to make sure a month and a year were both selected
        If ((cboxMonth.Text <> String.Empty) And (cboxYear.Text <> String.Empty)) Then
            labelCalForMonth(0, Integer.Parse(cboxYear.Text), cboxMonth.Text, 0)

            'Alert the user that the month has not been selected
        ElseIf (cboxMonth.Text = String.Empty) Then
            MessageBox.Show("Please select a month", "Display Month View", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            'Alert the user that the year has not been selected
        ElseIf (cboxYear.Text = String.Empty) Then
            MessageBox.Show("Please select a year", "Display Month View", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If

    End Sub

    'Open the add event form
    Private Sub btnAddEvent_Click(sender As Object, e As EventArgs) Handles btnAddEvent.Click
        Dim newAddEvent As New AddEvent()
        Call newAddEvent.Show()
    End Sub
End Class
