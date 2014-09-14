Public Class Form2
    Dim myDate As Date

    Public Sub New(ByVal dateValue As Date)
        InitializeComponent()
        myDate = dateValue
        lblDate.Text = myDate.ToString("MMMM d, yyyy")
        lblDayOfWeek.Text = myDate.DayOfWeek.ToString()
    End Sub


    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        
    End Sub
End Class