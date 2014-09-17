﻿Imports MySql.Data
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

        Dim connectionString As String = "Server=orion.csl.mtu.edu; Database=hewample; Uid=hewample;Pwd=hewample"
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

        'Close if another instance of the add event form is already open
        For Each frm In My.Application.OpenForms
            Try
                Dim addEventForm As AddEvent = DirectCast(frm, AddEvent)
                If (addEventForm IsNot (Me)) Then
                    MessageBox.Show("Only one instance of of the add event window can be open at once. Please close the other instance and try again.", "Multiple Instances Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    addEventForm.Focus()
                    Me.Close()
                End If

            Catch ex As Exception

            End Try

        Next
    End Sub

    'Action occurs on button click
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
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

    'Method for setting the start date automatically (used if coming from dayView)
    Public Sub New(startDate As Date)
        InitializeComponent()
        dtpStartDate.Value = startDate
        dtpEndDate.Value = startDate

    End Sub

    Public Sub New()
        InitializeComponent()
    End Sub
End Class