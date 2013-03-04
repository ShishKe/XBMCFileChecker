Imports System.IO
Imports System.ComponentModel
Public Class cFile
    Public Property sFullFileName As String
    Public Property blHasXBMCFile As Boolean
    Public Property oExclude As cExclude
    Public Property oXBMCFile As cXBMCFile
    Public Property pApplication As cApplication
    Public Property oFiles As cFiles

    Sub New(ByVal oApplication As cApplication, ByVal oFiles As cFiles, ByVal sFullFileName As String)
        pApplication = oApplication
        Me.oFiles = oFiles
        Me.sFullFileName = sFullFileName
        For Each o As cExclude In pApplication.oExcludes

            Dim regex As New Text.RegularExpressions.Regex(o.sRule)
            Dim match As Text.RegularExpressions.Match = regex.Match(sFullFileName)
            If match.Success Then
                Me.oExclude = o
                Exit For
            End If
        Next
    End Sub
    Public ReadOnly Property sRule As String
        Get
            If oExclude Is Nothing Then Return Nothing
            Return oExclude.sRule
        End Get
    End Property
    Public ReadOnly Property sDescription As String
        Get
            If oExclude Is Nothing Then Return Nothing
            Return oExclude.sDescription
        End Get
    End Property
    Public ReadOnly Property blExcluded As Boolean
        Get
            If oExclude Is Nothing Then
                Return False
            Else
                Return True
            End If
        End Get
    End Property
End Class
Public Class cFiles
    Inherits bindinglist(Of cFile)
    Private pApplication As cApplication
    Public sAllfiles As String()
    Sub New(ByVal oApplication As cApplication)
        pApplication = oApplication
    End Sub
    Public Sub AddAllFiles(ByVal sPath As String)
        Debug.Print("Searching " & sPath & "...")
        Dim sFiles As String() = GetFiles(sPath)
        If sFiles Is Nothing Then Exit Sub
        Debug.Print("Adding " & sFiles.Count)
        For Each s As String In sFiles
            Dim o As New cFile(pApplication, Me, s)
            If pApplication.oXBMCFiles(s) Is Nothing Then
                o.blHasXBMCFile = False
            Else
                o.blHasXBMCFile = True
                o.oXBMCFile = pApplication.oXBMCFiles(s)
            End If
            Me.Add(o)
        Next
        'If sAllfiles Is Nothing Then
        '    sAllfiles = sFiles
        'Else
        '    sAllfiles = sAllfiles.Union(sFiles).ToArray
        '    'sAllfiles = sAllfiles.Concat(sFiles).ToArray
        'End If
    End Sub
    Private Function GetFiles(ByVal sFolder As String) As String()
        Try
            sFolder = WinPath(sFolder)
            'only look for mpg, mpeg, avi, flv, wmv, mkv, 264, 3g2, 3gp, ifo, mp4, mov, iso, ogm?
            Return Directory.GetFiles(sFolder, "*.*", SearchOption.AllDirectories)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
End Class
