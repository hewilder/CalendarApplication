<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MonthView
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.cboxMonth = New System.Windows.Forms.ComboBox()
        Me.btnMonth = New System.Windows.Forms.Button()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.lblMonthName = New System.Windows.Forms.Label()
        Me.dtpDay = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cboxYear = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnDay = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'cboxMonth
        '
        Me.cboxMonth.BackColor = System.Drawing.Color.DarkSeaGreen
        Me.cboxMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboxMonth.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cboxMonth.FormattingEnabled = True
        Me.cboxMonth.Items.AddRange(New Object() {"January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"})
        Me.cboxMonth.Location = New System.Drawing.Point(669, 34)
        Me.cboxMonth.Name = "cboxMonth"
        Me.cboxMonth.Size = New System.Drawing.Size(104, 21)
        Me.cboxMonth.TabIndex = 3
        '
        'btnMonth
        '
        Me.btnMonth.BackgroundImage = Global.CalendarApplication.My.Resources.Resources.green_background
        Me.btnMonth.Location = New System.Drawing.Point(856, 32)
        Me.btnMonth.Name = "btnMonth"
        Me.btnMonth.Size = New System.Drawing.Size(75, 23)
        Me.btnMonth.TabIndex = 4
        Me.btnMonth.Text = "Display"
        Me.btnMonth.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(872, 598)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(100, 20)
        Me.TextBox1.TabIndex = 5
        '
        'lblMonthName
        '
        Me.lblMonthName.AutoSize = True
        Me.lblMonthName.Font = New System.Drawing.Font("Microsoft Sans Serif", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMonthName.Location = New System.Drawing.Point(14, 65)
        Me.lblMonthName.Name = "lblMonthName"
        Me.lblMonthName.Size = New System.Drawing.Size(203, 55)
        Me.lblMonthName.TabIndex = 6
        Me.lblMonthName.Text = "January"
        '
        'dtpDay
        '
        Me.dtpDay.Location = New System.Drawing.Point(669, 69)
        Me.dtpDay.MaxDate = New Date(2015, 12, 31, 0, 0, 0, 0)
        Me.dtpDay.MinDate = New Date(2014, 1, 1, 0, 0, 0, 0)
        Me.dtpDay.Name = "dtpDay"
        Me.dtpDay.Size = New System.Drawing.Size(181, 20)
        Me.dtpDay.TabIndex = 7
        Me.dtpDay.Value = New Date(2014, 1, 1, 0, 0, 0, 0)
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(597, 37)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(66, 13)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "View Month:"
        '
        'cboxYear
        '
        Me.cboxYear.BackColor = System.Drawing.Color.DarkSeaGreen
        Me.cboxYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboxYear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cboxYear.FormattingEnabled = True
        Me.cboxYear.Items.AddRange(New Object() {"2014", "2015"})
        Me.cboxYear.Location = New System.Drawing.Point(779, 34)
        Me.cboxYear.Name = "cboxYear"
        Me.cboxYear.Size = New System.Drawing.Size(71, 21)
        Me.cboxYear.TabIndex = 9
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(597, 75)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(55, 13)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "View Day:"
        '
        'btnDay
        '
        Me.btnDay.BackgroundImage = Global.CalendarApplication.My.Resources.Resources.green_background
        Me.btnDay.Location = New System.Drawing.Point(856, 68)
        Me.btnDay.Name = "btnDay"
        Me.btnDay.Size = New System.Drawing.Size(75, 23)
        Me.btnDay.TabIndex = 11
        Me.btnDay.Text = "Display"
        Me.btnDay.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.BackgroundImage = Global.CalendarApplication.My.Resources.Resources.green_background
        Me.Button1.Location = New System.Drawing.Point(779, 97)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(152, 23)
        Me.Button1.TabIndex = 12
        Me.Button1.Text = "Add an Event"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'MonthView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(984, 630)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.btnDay)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cboxYear)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.dtpDay)
        Me.Controls.Add(Me.lblMonthName)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.btnMonth)
        Me.Controls.Add(Me.cboxMonth)
        Me.Name = "MonthView"
        Me.Text = "Calendar Application - Month View"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cboxMonth As System.Windows.Forms.ComboBox
    Friend WithEvents btnMonth As System.Windows.Forms.Button
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents lblMonthName As System.Windows.Forms.Label
    Friend WithEvents dtpDay As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cboxYear As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnDay As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button

End Class
