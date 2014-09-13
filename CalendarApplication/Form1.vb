Imports MySql.Data
Imports MySql.Data.MySqlClient

Public Class MonthView
    Dim ctlList As Dictionary(Of String, Control) = New Dictionary(Of String, Control)

    Private Sub makeCalendar()
        Dim pictureboxes(35) As PictureBox
        Dim labels(35) As Label

        Dim calPanel As Panel = New Panel
        calPanel.Size = New Size(131 * 7 + 25, 75 * 5 + 50)
        calPanel.Name = "pboxPanel"

        For counter As Integer = 0 To 34
            labels(counter) = New Label
            labels(counter).BackColor = Color.WhiteSmoke
            labels(counter).BorderStyle = BorderStyle.None
            labels(counter).MinimumSize = New Size(30, 30)
            labels(counter).TextAlign = ContentAlignment.TopRight
            labels(counter).Font = New Font("Microsoft Sans Serif", 12)
            labels(counter).Location = New Point(35 + 131 * (counter Mod 7), 55 + 75 * (counter \ 7))
            labels(counter).Tag = "label" + counter.ToString()
            labels(counter).Name = "label" + counter.ToString()
            labels(counter).Text = counter.ToString()
            calPanel.Controls.Add(labels(counter))
            AddHandler labels(counter).Click, AddressOf Me.labelClick
            AddHandler labels(counter).MouseHover, AddressOf Me.calHover
            AddHandler labels(counter).MouseLeave, AddressOf Me.calLeave
            ctlList.Add(labels(counter).Tag, labels(counter))

        Next

        'new comment
        For counter As Integer = 0 To 34
            pictureboxes(counter) = New PictureBox
            pictureboxes(counter).BackColor = Color.WhiteSmoke
            pictureboxes(counter).BorderStyle = BorderStyle.FixedSingle
            pictureboxes(counter).Size = New Size(121, 65)
            pictureboxes(counter).Location = New Point(25 + 131 * (counter Mod 7), 50 + 75 * (counter \ 7))
            pictureboxes(counter).Name = "pbox" + counter.ToString()
            pictureboxes(counter).Tag = "pbox" + counter.ToString()
            calPanel.Controls.Add(pictureboxes(counter))
            AddHandler pictureboxes(counter).Click, AddressOf Me.pboxClick
            AddHandler pictureboxes(counter).MouseHover, AddressOf Me.calHover
            AddHandler pictureboxes(counter).MouseLeave, AddressOf Me.calLeave
            ctlList.Add(pictureboxes(counter).Tag, pictureboxes(counter))

        Next

        Me.Controls.Add(calPanel)
    End Sub


    Private Sub labelClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim label As Label = DirectCast(sender, Label)
        TextBox1.Text = label.Tag
    End Sub

    Private Sub pboxClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim pbox As PictureBox = DirectCast(sender, PictureBox)
        TextBox1.Text = pbox.Tag
    End Sub

    Private Sub calHover(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim control As Control = DirectCast(sender, Control)
        Dim tag As String = control.Tag
        Dim otherControl As Control
        control.BackColor = Color.LightGray

        If (tag.Contains("label")) Then
            otherControl = ctlList.Item("pbox" + getLabelNum(tag))
        Else
            otherControl = ctlList.Item("label" + getPBoxNum(tag))
        End If

        otherControl.BackColor = Color.LightGray
    End Sub

    Private Sub calLeave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim control As Control = DirectCast(sender, Control)
        Dim tag As String = control.Tag
        Dim otherControl As Control
        control.BackColor = Color.WhiteSmoke

        If (tag.Contains("label")) Then
            otherControl = ctlList.Item("pbox" + getLabelNum(tag))
        Else
            otherControl = ctlList.Item("label" + getPBoxNum(tag))
        End If

        otherControl.BackColor = Color.WhiteSmoke
    End Sub

    Private Function getLabelNum(wholeName As String) As String
        Dim substring As String = wholeName.Substring(5)
        Return wholeName.Substring(5)
    End Function

    Private Function getPBoxNum(wholeName As String) As String
        Dim substring As String = wholeName.Substring(4)
        Return wholeName.Substring(4)
    End Function

    Private Sub labelCalendar(offset As Integer, numDays As Integer)
        Dim ctl As Control
        Dim dayNum As Integer = 1
        For counter As Integer = 0 To 34
            ctl = ctlList.Item("label" + counter.ToString())
            ctl.Text = ""
            ctl.Show()

            ctl = ctlList.Item("pbox" + counter.ToString())
            ctl.Show()
        Next

        For counter As Integer = offset To (offset + numDays - 1)
            ctl = ctlList.Item("label" + counter.ToString())
            ctl.Text = dayNum
            dayNum = dayNum + 1
        Next

        If ((offset + numDays - 1) < 28) Then
            For counter As Integer = 28 To 34
                ctl = ctlList.Item("pbox" + counter.ToString())
                ctl.Hide()
                ctl = ctlList.Item("label" + counter.ToString())
                ctl.Hide()
            Next
        End If
    End Sub


    Private Sub MonthView_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim connectionString As String = "Server=127.0.0.1; Database=calendar; Uid=root;Pwd=teamsoftware"
        Dim connection As New MySqlConnection(connectionString)
        Dim da As New MySqlDataAdapter
        Dim dt As DataSet = New DataSet
        Try
            connection.Open()
            TextBox1.Text = connection.ServerVersion
            Dim sqlComm As New MySqlCommand

            'sqlComm.CommandText = "SELECT * FROM months WHERE Name = " + ComboBox1.Text + ";"
            sqlComm.CommandText = "SELECT * FROM months;"
            'da = New MySqlDataAdapter(, connection)
            sqlComm.Connection = connection
            da.SelectCommand = sqlComm
            da.Fill(dt, "months")
            connection.Close()
        Catch ex As Exception
            If (connection.State = Data.ConnectionState.Open) Then
                connection.Close()
            End If


        End Try

        Dim row As DataRow
        For Each row In dt.Tables(0).Rows
            Dim name As String = row("Name").ToString()
            TextBox1.Text = TextBox1.Text + " " + row("Name").ToString()
        Next

        makeCalendar()
        labelCalendar(1, 28)
    End Sub
End Class
