Imports XBMCDataLayer

Public Class fSettings

    Private Sub fSettings_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        My.Settings.Save()
    End Sub

    Private Sub fSettings_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.CExcludesBindingSource.DataSource = My.Settings.Excludes
        Dim o As cExcludes = My.Settings.Excludes
    End Sub
End Class