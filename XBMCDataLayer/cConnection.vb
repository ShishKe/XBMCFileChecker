Imports MySql.Data.MySqlClient
Imports System.Data.Odbc

Public Class cConnection
    Public Enum enumConnectionType
        None = 0
        MySQL
        SQLLite
        ODBC
    End Enum
    Private foMySqlConnection As MySql.Data.MySqlClient.MySqlConnection
    Private foODBCConnection As OdbcConnection
    Private fsConnectionString As String
    Public Sub New(ByVal eConnectionType As enumConnectionType)
        Me.eConnectionType = eConnectionType
        Select Case eConnectionType
            Case enumConnectionType.ODBC
                SetupODBCConnection()
            Case enumConnectionType.MySQL
                SetupMySQLConnection()
            Case enumConnectionType.SQLLite
            Case Else
                Throw New Exception("Unknown Connection Type")
        End Select

    End Sub
    Private Sub SetupMySQLConnection()
        fsConnectionString = "server=san1a;" _
                    & "uid=xbmc;" _
                    & "pwd=xbmc;" _
                    & "database=xbmc_video60;"
        Try
            foMySqlConnection = New MySqlConnection(fsConnectionString)
            foMySqlConnection.Open()
        Catch ex As MySql.Data.MySqlClient.MySqlException
            Throw New Exception("Cannot open MySQLDataBase", ex)
        End Try
    End Sub
    Private Sub SetupODBCConnection()
        fsConnectionString = "DSN=Excel Files;" _
            & "DBQ=C:\SyncDocs\Development\XBMCFileChecker\Test\TestDB.xlsx;" _
            & "DefaultDir=C:\SyncDocs\Development\XBMCFileChecker\Test;" _
            & "DriverId=1046;" _
            & "MaxBufferSize=2048;" _
            & "PageTimeout=5;"
        Try
            foODBCConnection = New OdbcConnection(fsConnectionString)
            foODBCConnection.Open()
        Catch ex As MySql.Data.MySqlClient.MySqlException
            Throw New Exception("Cannot open MySQLDataBase", ex)
        End Try
    End Sub

    Public Property eConnectionType As enumConnectionType
    Public ReadOnly Property sConnectionType As String
        Get
            Return eConnectionType.ToString
        End Get
    End Property
    Public Sub Open()
        Try
            foMySqlConnection = New MySqlConnection(fsConnectionString)
            foMySqlConnection.Open()
        Catch ex As Exception  'MySql.Data.MySqlClient.MySqlException
            Throw New Exception("Cannot open MySQLDataBase", ex)
        End Try
    End Sub
    Public Sub Close()
        foMySqlConnection.Close()
    End Sub

    Public Function ExecuteQuerySQL(ByVal sSQL As String) As DataSet
        Select Case Me.eConnectionType
            Case enumConnectionType.MySQL
                Return ExecuteMySQLQuerySQL(sSQL)
            Case enumConnectionType.SQLLite
            Case enumConnectionType.ODBC
                Return ExecuteODBCQuerySQL(sSQL)
        End Select
    End Function
    Private Function ExecuteMySQLQuerySQL(ByVal sSQL As String) As DataSet
        Try
            Dim SQLDataAdapter As MySqlDataAdapter
            Dim SQLDataSet As DataSet

            SQLDataAdapter = New MySqlDataAdapter(sSQL, foMySqlConnection)
            SQLDataSet = New DataSet()
            SQLDataAdapter.Fill(SQLDataSet)
            Return SQLDataSet

        Catch ex As Exception
            Throw New Exception("Cannot Execute Query", ex)
        End Try
    End Function
    Private Function ExecuteODBCQuerySQL(ByVal sSQL As String) As DataSet
        Try
            Dim SQLDataAdapter As OdbcDataAdapter
            Dim SQLDataSet As DataSet

            SQLDataAdapter = New OdbcDataAdapter(sSQL, foODBCConnection)
            SQLDataSet = New DataSet()
            SQLDataAdapter.Fill(SQLDataSet)
            Return SQLDataSet

        Catch ex As Exception
            Throw New Exception("Cannot Execute Query", ex)
        End Try
    End Function

    Public Function ExecuteQuerySQL2(ByVal sSQL As String) As List(Of String)
        Try
            Dim cmd As MySqlCommand = New MySqlCommand()
            Dim sRet As New List(Of String)
            cmd.CommandText = sSQL
            cmd.Connection = foMySqlConnection
            cmd.CommandType = CommandType.Text
            Dim reader As MySqlDataReader = cmd.ExecuteReader()
            While (reader.Read())
                sRet.Add(reader(0).ToString)
            End While
            Return sRet
        Catch ex As Exception
            Throw New Exception("Cannot Execute Query", ex)
        End Try
    End Function

End Class
