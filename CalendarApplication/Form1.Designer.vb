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
        Me.SuspendLayout()
        '
        'cboxMonth
        '
        Me.cboxMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboxMonth.FormattingEnabled = True
        Me.cboxMonth.Items.AddRange(New Object() {"January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"})
        Me.cboxMonth.Location = New System.Drawing.Point(761, 34)
        Me.cboxMonth.Name = "cboxMonth"
        Me.cboxMonth.Size = New System.Drawing.Size(121, 21)
        Me.cboxMonth.TabIndex = 3
        '
        'btnMonth
        '
        Me.btnMonth.Location = New System.Drawing.Point(897, 32)
        Me.btnMonth.Name = "btnMonth"
        Me.btnMonth.Size = New System.Drawing.Size(75, 23)
        Me.btnMonth.TabIndex = 4
        Me.btnMonth.Text = "Display"
        Me.btnMonth.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(30, 13)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(100, 20)
        Me.TextBox1.TabIndex = 5
        '
        'MonthView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(984, 562)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.btnMonth)
        Me.Controls.Add(Me.cboxMonth)
        Me.Name = "MonthView"
        Me.Text = "Calendar Application"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cboxMonth As System.Windows.Forms.ComboBox
    Friend WithEvents btnMonth As System.Windows.Forms.Button
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox

End Class
