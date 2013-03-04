Imports System.ComponentModel
Public Class cXBMCPath
    Public Property nIdPath As Nullable(Of Integer)
    Public Property sPath As String
    Public Property sContent As String
    Public Property sScraper As String
    Public Property sHash As String
    Public Property blScanRecursive As Nullable(Of Boolean)
    Public Property blUseFolderNames As Nullable(Of Boolean)
    Public Property sSettings As String
    Public Property blNoUpdate As Nullable(Of Boolean)
    Public Property blExclude As Nullable(Of Boolean)
    Public Property dtDateAdded As Nullable(Of Date)
    Private Property pApplication As cApplication
    Private Property oPaths As cXBMCPaths
    Sub New(ByVal oApplication As cApplication, ByVal oPaths As cXBMCPaths, ByVal oDataRow As DataRow)
        pApplication = oApplication
        Me.oPaths = oPaths
        Me.Add(oDataRow)
    End Sub
    Sub New(ByVal oApplication As cApplication, _
            ByVal nIdPath As Integer, _
            ByVal sPath As String, _
            ByVal sContent As String, _
            ByVal sScraper As String, _
            ByVal sHash As String, _
            ByVal blScanRecursive As Integer, _
            ByVal blUseFolderNames As Boolean, _
            ByVal blSSettings As String, _
            ByVal blNoUpdate As Boolean, _
            ByVal blExclude As Boolean, _
            ByVal dtDateAdded As Date)
        pApplication = oApplication
        Throw New Exception("Method not supported")
    End Sub
    Private Sub Add(ByVal oDataRow As DataRow)
        nIdPath = ParseDBInteger(oDataRow("idPath"))
        sPath = ParseDBString(oDataRow("strPath"))
        sContent = ParseDBString(oDataRow("strContent"))
        sScraper = ParseDBString(oDataRow("strScraper"))

        sHash = ParseDBString(oDataRow("strHash"))
        blScanRecursive = ParseDBBoolean(oDataRow("scanRecursive"))
        blUseFolderNames = ParseDBBoolean(oDataRow("useFolderNames"))
        sSettings = ParseDBString(oDataRow("strSettings"))
        blNoUpdate = ParseDBBoolean(oDataRow("noUpdate"))
        blExclude = ParseDBBoolean(oDataRow("exclude"))
        'only for later version
        'dtDateAdded = oDataRow("dateAdded")
    End Sub
End Class

Public Class cXBMCPaths
    Inherits BindingList(Of cXBMCPath)
    Private Property pApplication As cApplication
    Sub New(ByVal oApplication As cApplication)
        pApplication = oApplication
        Dim sSQL As String = "Select * from path"
        Dim oDataSet As DataSet
        oDataSet = pApplication.oConnection.ExecuteQuerySQL(sSQL)
        For Each oDataRow As DataRow In oDataSet.Tables(0).Rows
            Dim oPath As New cXBMCPath(pApplication, Me, oDataRow)
            Me.Add(oPath)
        Next
    End Sub
End Class
