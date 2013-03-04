Imports System.IO
Imports XBMCdatalayer
Public Class Form1
    Private pApplication As capplication
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Dim oConnection As New cConnection
        pApplication = New cApplication(oConnection)
        Debug.Print(pApplication.oExcludes.Count)
        pApplication.oExcludes.Save()
        'pApplication.oIncludes.Add(New cInclude("mpg", ""))
        'pApplication.oIncludes.Add(New cInclude("mpeg", ""))
        'pApplication.oIncludes.Add(New cInclude("avi", ""))
        'pApplication.oIncludes.Add(New cInclude("flv", ""))
        'pApplication.oIncludes.Add(New cInclude("wmv", ""))
        'pApplication.oIncludes.Add(New cInclude("mkv", ""))
        'pApplication.oIncludes.Add(New cInclude("264", ""))
        'pApplication.oIncludes.Add(New cInclude("3g2", ""))
        'pApplication.oIncludes.Add(New cInclude("3gp", ""))
        'pApplication.oIncludes.Add(New cInclude("ifo", ""))
        'pApplication.oIncludes.Add(New cInclude("mp4", ""))
        'pApplication.oIncludes.Add(New cInclude("mov", ""))
        'pApplication.oIncludes.Add(New cInclude("iso", ""))
        'pApplication.oIncludes.Add(New cInclude("ogm", ""))

        pApplication.oIncludes.Save()
        For Each o As cXBMCPath In pApplication.oXBMCPaths
            pApplication.oFiles.AddAllFiles(o.sPath)
        Next
        For Each o As cXBMCFile In pApplication.oXBMCFiles
            'Debug.Print(o.sFullFileName)
        Next
        Debug.Print("Files which have not been scanned into XBMC")
        For Each o As cFile In pApplication.oFiles
            If o.blHasXBMCFile = False Then Debug.Print(o.sFullFileName)
        Next

    End Sub


End Class

