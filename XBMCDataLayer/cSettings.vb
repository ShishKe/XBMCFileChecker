'Imports System.ComponentModel
'Imports System.Runtime.Serialization
'<Serializable()> _
'Public Class cExclude
'    Implements ISerializable
'    Public Property sRule As String
'    Public Property sDescription As String
'    ' Empty constructor required to compile.
'    Sub New()
'    End Sub
'    ' Implement this method to serialize data. The method is called  
'    ' on serialization. 
'    Public Sub GetObjectData(ByVal info As SerializationInfo, ByVal context As StreamingContext) Implements ISerializable.GetObjectData
'        ' Use the AddValue method to specify serialized values.
'        info.AddValue("Rule", sRule, GetType(String))
'        info.AddValue("Description", sDescription, GetType(String))
'    End Sub

'    ' The special constructor is used to deserialize values. 
'    Public Sub New(ByVal info As SerializationInfo, ByVal context As StreamingContext)
'        ' Reset the property value using the GetValue method.
'        sRule = DirectCast(info.GetValue("Rule", GetType(String)), String)
'        sDescription = DirectCast(info.GetValue("Description", GetType(String)), String)
'    End Sub
'End Class
'<Serializable()> _
'Public Class cExcludes
'    Inherits BindingList(Of cExclude)
'    Implements ISerializable
'    Sub New()

'    End Sub

'    Public Sub GetObjectData(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext) Implements System.Runtime.Serialization.ISerializable.GetObjectData

'    End Sub
'End Class
Imports System.ComponentModel
Imports System.Runtime.Serialization
Public Class cExclude
    Public Property sRule As String
    Public Property sDescription As String
End Class
Public Class cExcludes
    Inherits BindingList(Of cExclude)
    'it would be nice to define the name of the file for serialization, but I cannot get reflection to access this statis property
    Public Shared sFilename As String = "Settings.Exclude.xml"
    Sub New()
    End Sub
    Sub Save()
        mSerialization.SerializeItem(Me)
    End Sub
End Class
Public Class cInclude
    Public Property sRule As String
    Public Property sDescription As String
    Sub New()
    End Sub
    Sub New(ByVal sRule As String, ByVal sDescription As String)
        Me.sRule = sRule
        Me.sDescription = sDescription
    End Sub
End Class
Public Class cIncludes
    Inherits BindingList(Of cInclude)
    'it would be nice to define the name of the file for serialization, but I cannot get reflection to access this statis property
    Public Shared sFilename As String = "Settings.Include.xml"
    Sub New()
    End Sub
    Sub Save()
        mSerialization.SerializeItem(Me)
    End Sub
End Class
