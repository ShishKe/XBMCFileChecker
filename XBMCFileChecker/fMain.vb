Imports System.IO
Imports XBMCdatalayer
Public Class fMain
    Private pApplication As capplication
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Dim oConnection As New cConnection(cConnection.enumConnectionType.MySQL)

        pApplication = New cApplication(oConnection)
        pApplication.oExcludes.Save()

        pApplication.oIncludes.Save()
        For Each o As cXBMCPath In pApplication.oXBMCPaths
            pApplication.oFiles.AddAllFiles(o.sPath)
        Next
        For Each o As cXBMCFile In pApplication.oXBMCFiles
            'Debug.Print(o.sFullFileName)
        Next
        Debug.Print("Files which have not been scanned into XBMC")
        For Each o As cFile In pApplication.oFiles
            If o.blHasXBMCFile = False Then Debug.Print(o.sFullFileName & vbTab & o.blExcluded)
        Next
        InitialiseGrid()
    End Sub

    Sub InitialiseGrid()

        'pApplication.oFiles(1).blHasXBMCFile
        GGCFiles.TableDescriptor.Columns.Add("sFullFileName")
        GGCFiles.TableDescriptor.Columns.Add("blHasXBMCFile")
        GGCFiles.TableDescriptor.Columns.Add("blExcluded")
        GGCFiles.TableDescriptor.Columns.Add("sRule")
        GGCFiles.TableDescriptor.Columns.Add("sDescription")
        GGCFiles.DataSource = pApplication.oFiles
        mGlobals.InitaliseGrid(GGCFiles)
    End Sub
End Class

