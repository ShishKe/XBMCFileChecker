Public Class cApplication
    Public Property oConnection As cConnection
    Private foXBMCPaths As cXBMCPaths
    Private foXBMCFiles As cXBMCFiles
    Private foFiles As cFiles
    Private foExcludes As cExcludes
    Private foIncludes As cIncludes
    Sub New(ByVal oConnection As cConnection)
        Me.oConnection = oConnection
    End Sub
    Public ReadOnly Property oXBMCPaths As cXBMCPaths
        Get
            If foXBMCPaths Is Nothing Then foXBMCPaths = New cXBMCPaths(Me)
            Return foXBMCPaths
        End Get
    End Property
    Public ReadOnly Property oXBMCFiles As cXBMCFiles
        Get
            If foXBMCFiles Is Nothing Then foXBMCFiles = New cXBMCFiles(Me)
            Return foXBMCFiles
        End Get
    End Property
    Public ReadOnly Property oFiles As cFiles
        Get
            If foFiles Is Nothing Then foFiles = New cFiles(Me)
            Return foFiles
        End Get
    End Property
    Public ReadOnly Property oExcludes As cExcludes
        Get
            If foExcludes Is Nothing Then
                foExcludes = CType(mSerialization.DeserializeItem(GetType(cExcludes)), cExcludes)
            End If
            Return foExcludes
        End Get
    End Property
    Public ReadOnly Property oIncludes As cIncludes
        Get
            If foIncludes Is Nothing Then
                foIncludes = CType(mSerialization.DeserializeItem(GetType(cIncludes)), cIncludes)
            End If
            Return foIncludes
        End Get
    End Property
End Class
