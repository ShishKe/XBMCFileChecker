<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fMain
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
        Me.components = New System.ComponentModel.Container()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.SplitContainerAdv1 = New Syncfusion.Windows.Forms.Tools.SplitContainerAdv()
        Me.GGCFiles = New Syncfusion.Windows.Forms.Grid.Grouping.GridGroupingControl()
        Me.CFilesBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        CType(Me.SplitContainerAdv1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerAdv1.Panel2.SuspendLayout()
        Me.SplitContainerAdv1.SuspendLayout()
        CType(Me.GGCFiles, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CFilesBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 416)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(1115, 22)
        Me.StatusStrip1.TabIndex = 0
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'SplitContainerAdv1
        '
        Me.SplitContainerAdv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerAdv1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerAdv1.Name = "SplitContainerAdv1"
        Me.SplitContainerAdv1.Orientation = System.Windows.Forms.Orientation.Vertical
        '
        'SplitContainerAdv1.Panel2
        '
        Me.SplitContainerAdv1.Panel2.Controls.Add(Me.GGCFiles)
        Me.SplitContainerAdv1.Size = New System.Drawing.Size(1115, 416)
        Me.SplitContainerAdv1.SplitterDistance = 79
        Me.SplitContainerAdv1.TabIndex = 1
        Me.SplitContainerAdv1.Text = "SplitContainerAdv1"
        '
        'GGCFiles
        '
        Me.GGCFiles.BackColor = System.Drawing.SystemColors.Window
        Me.GGCFiles.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GGCFiles.Location = New System.Drawing.Point(0, 0)
        Me.GGCFiles.Name = "GGCFiles"
        Me.GGCFiles.Size = New System.Drawing.Size(1115, 330)
        Me.GGCFiles.TabIndex = 0
        Me.GGCFiles.Text = "GridGroupingControl1"
        Me.GGCFiles.VersionInfo = "11.104.0.21"
        '
        'CFilesBindingSource
        '
        Me.CFilesBindingSource.DataSource = GetType(XBMCDataLayer.cFiles)
        '
        'fMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1115, 438)
        Me.Controls.Add(Me.SplitContainerAdv1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Name = "fMain"
        Me.Text = "XBMC File Checker"
        Me.SplitContainerAdv1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainerAdv1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerAdv1.ResumeLayout(False)
        CType(Me.GGCFiles, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CFilesBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents SplitContainerAdv1 As Syncfusion.Windows.Forms.Tools.SplitContainerAdv
    Friend WithEvents GGCFiles As Syncfusion.Windows.Forms.Grid.Grouping.GridGroupingControl
    Friend WithEvents CFilesBindingSource As System.Windows.Forms.BindingSource

End Class
