Imports System.ComponentModel
Public Class cXBMCFile
    Public Property nIdFile As Nullable(Of Integer)
    Public Property nIdPath As Nullable(Of Integer)
    Public Property sPath As String
    Public Property sFilename As String
    Public Property nPlaycount As Nullable(Of Integer)
    Public Property dtLastPlayed As Nullable(Of Date)
    Private Property pApplication As cApplication
    Private Property oXBMCFiles As cXBMCFiles
    Sub New(ByVal oApplication As cApplication, ByVal oFiles As cXBMCFiles, ByVal oDataRow As DataRow, ByVal sFilename As String)
        pApplication = oApplication
        Me.oXBMCFiles = oFiles
        Me.sFilename = sFilename
        Me.Add(oDataRow)
    End Sub
    Public ReadOnly Property sFullFileName As String
        Get
            Return WinPath(sPath & sFilename)
        End Get
    End Property
    Private Sub Add(ByVal oDataRow As DataRow)
        nIdFile = ParseDBInteger(oDataRow("idFile"))
        nIdPath = ParseDBInteger(oDataRow("idPath"))
        'forget parsing it out of the database, it might stacked, so it has already been done by the caller
        'sFilename = ParseDBString(oDataRow("strFilename"))
        sPath = ParseDBString(oDataRow("strPath"))
        nPlaycount = ParseDBInteger(oDataRow("playCount"))
        dtLastPlayed = ParseDBDate(oDataRow("lastPlayed"))
    End Sub
End Class

Public Class cXBMCFiles
    Inherits BindingList(Of cXBMCFile)
    Private Property pApplication As cApplication
    Private foDictionary As New Dictionary(Of String, cXBMCFile)
    Sub New(ByVal oApplication As cApplication)
        pApplication = oApplication
        Dim sSQL As String = "select * from path inner join files on files.idpath = path.idpath"
        Dim oDataSet As DataSet
        oDataSet = pApplication.oConnection.ExecuteQuerySQL(sSQL)
        For Each oDataRow As DataRow In oDataSet.Tables(0).Rows
            'could be a comma seperated list
            Dim sFiles As String() = ParseDBString(oDataRow("strFilename")).Split(","c)
            For Each sFile As String In sFiles
                Dim oPath As New cXBMCFile(pApplication, Me, oDataRow, WinFile(sFile))
                'could be duplicates as a file could exist as its own record as well as in a stack record
                If foDictionary.ContainsKey(oPath.sFullFileName) = False Then
                    Me.Add(oPath)
                    foDictionary.Add(oPath.sFullFileName, oPath)
                End If
            Next
        Next
    End Sub
    Default Overloads ReadOnly Property Item(ByVal sFullFileName As String) As cXBMCFile
        Get
            If foDictionary.ContainsKey(sFullFileName) Then
                Return foDictionary(sFullFileName)
            Else
                Return Nothing
            End If
        End Get
    End Property
End Class
