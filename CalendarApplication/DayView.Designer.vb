<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DayView
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lblDate = New System.Windows.Forms.Label()
        Me.lblDayOfWeek = New System.Windows.Forms.Label()
        Me.btnAddEvent = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.AutoScroll = True
        Me.Panel1.Location = New System.Drawing.Point(12, 127)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(624, 443)
        Me.Panel1.TabIndex = 0
        '
        'lblDate
        '
        Me.lblDate.AutoSize = True
        Me.lblDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDate.ForeColor = System.Drawing.Color.Black
        Me.lblDate.Location = New System.Drawing.Point(2, 69)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(385, 55)
        Me.lblDate.TabIndex = 7
        Me.lblDate.Text = "January 5, 2015"
        '
        'lblDayOfWeek
        '
        Me.lblDayOfWeek.AutoSize = True
        Me.lblDayOfWeek.Font = New System.Drawing.Font("Microsoft Sans Serif", 21.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDayOfWeek.ForeColor = System.Drawing.Color.DarkGray
        Me.lblDayOfWeek.Location = New System.Drawing.Point(6, 36)
        Me.lblDayOfWeek.Name = "lblDayOfWeek"
        Me.lblDayOfWeek.Size = New System.Drawing.Size(134, 33)
        Me.lblDayOfWeek.TabIndex = 8
        Me.lblDayOfWeek.Text = "Tuesday"
        '
        'btnAddEvent
        '
        Me.btnAddEvent.Location = New System.Drawing.Point(477, 97)
        Me.btnAddEvent.Name = "btnAddEvent"
        Me.btnAddEvent.Size = New System.Drawing.Size(159, 23)
        Me.btnAddEvent.TabIndex = 13
        Me.btnAddEvent.Text = "Add an Event"
        Me.btnAddEvent.UseVisualStyleBackColor = True
        '
        'DayView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(648, 582)
        Me.Controls.Add(Me.btnAddEvent)
        Me.Controls.Add(Me.lblDayOfWeek)
        Me.Controls.Add(Me.lblDate)
        Me.Controls.Add(Me.Panel1)
        Me.ForeColor = System.Drawing.Color.Black
        Me.Name = "DayView"
        Me.Text = "Calendar Application - Day View"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lblDate As System.Windows.Forms.Label
    Friend WithEvents lblDayOfWeek As System.Windows.Forms.Label
    Friend WithEvents btnAddEvent As System.Windows.Forms.Button
End Class
