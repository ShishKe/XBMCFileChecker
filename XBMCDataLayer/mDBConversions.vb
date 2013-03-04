Imports System.IO
Imports Syncfusion.Windows.Forms.Grid.Grouping
Imports Syncfusion.Windows.Forms.Grid
Imports Syncfusion.Drawing
Imports System.Drawing

Module mDBConversions
    ' Date and time formatting
    Public ReadOnly sLongDateFormat As String = "d MMM yyy"
    Public ReadOnly sLongDateTimeFormat As String = sLongDateFormat + " H:mm:ss"
    Public ReadOnly sTimeFormat As String = "Short Time"
    Public Function ToDBType(ByVal value As Object, ByVal oType As Type) As String
        If IsDBNull(value) Then
            value = Nothing
        End If
        If oType.Equals(GetType(Integer)) Then
            Return ToDBNumeric(CInt(value))
        ElseIf oType.Equals(GetType(String)) Then
            Return ToDBString(CStr(value))
        ElseIf oType.Equals(GetType(Boolean)) Then
            Return BuildDBBooleanString(CBool(value))
            'there is no difference between a date and a datetime
        ElseIf oType.Equals(GetType(Date)) Then
            Return BuildDBDateTimeString(CDate(value))
        ElseIf oType.Equals(GetType(Double)) Then
            Return ToDBNumeric(CDbl(value))
        Else
            Throw New Exception("Cannot convert type " & oType.Name & " to database column")
        End If

    End Function
    Public Function ToDBString(ByVal sValue As String) As String
        'make sure the string isn't blank before we do our replace
        If sValue Is Nothing Then
            Return "NULL"
        Else
            Return "'" & sValue.Replace("'", "''") & "'"
        End If

        'If Len(sValue) > 0 Then
        '    ' Double up 's
        '    Return sValue.Replace("'", "''")
        'Else
        '    Return sValue
        'End If
    End Function
    Public Function ToDBDate(ByVal dValue As Date) As String
        If dValue = Nothing Then Return "NULL"
        ' Convert to SQL format date string
        If dValue.Year < 1800 Then
            dValue = dValue.AddYears(1800 - dValue.Year)
        End If
        Return dValue.ToString(sLongDateFormat)
    End Function
    Public Function ToDBDateTime(ByVal dValue As DateTime) As String
        If dValue = Nothing Then Return "NULL"
        ' Convert to a Univiersal Time, SQL formatted date time string
        If dValue.Year < 1800 Then
            dValue = dValue.AddYears(1800 - dValue.Year)
        End If
        Return dValue.ToString(sLongDateTimeFormat)
    End Function
    Public Function ToDBNumeric(ByVal d As Double) As String
        Return d.ToString
        'Return d.ToString("F6")
    End Function
    Public Function ToDBBoolean(ByVal bl As Boolean) As String
        Return bl.ToString
    End Function


    Public Function FromDBDateTime(ByVal oValue As Object) As DateTime
        ' Convert to local date time
        Return Convert.ToDateTime(oValue)
    End Function

    Public Function BuildDBDateString(ByVal dt As DateTime, Optional ByVal blIsNull As Boolean = False, Optional ByVal blCommaPrefix As Boolean = False, Optional ByVal blCommaPostfix As Boolean = False) As String
        Dim s2 As String
        If blIsNull Then
            s2 = "NULL"
        Else
            s2 = "'" & ToDBDate(dt) & "'"
        End If
        If blCommaPrefix Then s2 = ", " & s2
        If blCommaPostfix Then s2 = s2 & ", "
        Return s2
    End Function
    Public Function BuildDBBooleanString(ByVal bl As Boolean, Optional ByVal blIsNull As Boolean = False, Optional ByVal blCommaPrefix As Boolean = False, Optional ByVal blCommaPostfix As Boolean = False) As String
        Dim s2 As String
        If blIsNull Then
            s2 = "NULL"
        Else
            s2 = "'" & ToDBBoolean(bl) & "'"
        End If
        If blCommaPrefix Then s2 = ", " & s2
        If blCommaPostfix Then s2 = s2 & ", "
        Return s2
    End Function
    Public Function BuildDBDateTimeString(ByVal dt As DateTime, Optional ByVal blIsNull As Boolean = False, Optional ByVal blCommaPrefix As Boolean = False, Optional ByVal blCommaPostfix As Boolean = False) As String
        Dim s2 As String
        If blIsNull Or dt = Nothing Then
            s2 = "NULL"
        Else
            s2 = "'" & ToDBDateTime(dt) & "'"
        End If
        If blCommaPrefix Then s2 = ", " & s2
        If blCommaPostfix Then s2 = s2 & ", "
        Return s2
    End Function
    Public Function BuildDBString(ByVal s As String, Optional ByVal blIsNull As Boolean = False, Optional ByRef blCommaPrefix As Boolean = False, Optional ByRef blCommaPostfix As Boolean = False) As String
        Dim s2 As String
        If blIsNull OrElse s Is Nothing Then
            s2 = "NULL"
        Else
            s2 = "'" & s.Replace("'", "''") & "'"
        End If
        If blCommaPrefix Then s2 = ", " & s2 Else blCommaPrefix = True
        If blCommaPostfix Then s2 = s2 & ", " Else blCommaPostfix = True
        Return s2
    End Function
    Public Function BuildDBIntegerString(ByVal n As Integer, ByVal blIsNull As Boolean, Optional ByRef blCommaPrefix As Boolean = False, Optional ByRef blCommaPostfix As Boolean = False) As String
        Dim s As String
        If blIsNull Then
            s = "NULL"
        Else
            s = n.ToString
        End If
        If blCommaPrefix Then s = ", " & s Else blCommaPrefix = True
        If blCommaPostfix Then s = s & ", " Else blCommaPostfix = True
        Return s
    End Function
    Public Function BuildDBNumericString(ByVal n As Double, ByVal blIsNull As Boolean, Optional ByRef blCommaPrefix As Boolean = False, Optional ByRef blCommaPostfix As Boolean = False) As String
        Dim s As String
        If blIsNull Then
            s = "NULL"
        Else
            s = ToDBNumeric(n)
        End If
        If blCommaPrefix Then s = ", " & s Else blCommaPrefix = True
        If blCommaPostfix Then s = s & ", " Else blCommaPostfix = True
        Return s
    End Function
    Public Function BuildDBLongString(ByVal n As Long, ByVal blIsNull As Boolean, Optional ByRef blCommaPrefix As Boolean = False, Optional ByRef blCommaPostfix As Boolean = False) As String
        Dim s As String
        If blIsNull Then
            s = "NULL"
        Else
            s = n.ToString
        End If
        If blCommaPrefix Then s = ", " & s Else blCommaPrefix = True
        If blCommaPostfix Then s = s & ", " Else blCommaPostfix = True
        Return s
    End Function

    Public Function ParseDBString(ByVal o As Object) As String
        If Not IsDBNull(o) Then
            Return CType(o, String)
        Else
            Return Nothing
        End If
    End Function
    Public Sub ParseDBDouble(ByVal o As Object, ByRef n As Double, ByRef oIsNull As Boolean)
        If Not IsDBNull(o) Then
            n = CType(o, Double)
            oIsNull = False
        Else
            n = Nothing
            oIsNull = True
        End If
    End Sub
    Public Function ParseDBDate(ByVal o As Object) As Nullable(Of Date)
        If Not IsDBNull(o) Then
            Return CType(o, Date)
        Else
            Return Nothing
        End If
    End Function
    Public Function ParseDBInteger(ByVal o As Object) As Nullable(Of Integer)
        If Not IsDBNull(o) Then
            Return CType(o, Integer)
        Else
            Return Nothing
        End If
    End Function
    Public Function ParseDBBoolean(ByVal o As Object) As Boolean
        If Not IsDBNull(o) Then
            Return CType(o, Boolean)
        Else
            Return Nothing
        End If
    End Function

    Public Sub ParseDBLong(ByVal o As Object, ByRef n As Long, ByRef oIsNull As Boolean)
        If Not IsDBNull(o) Then
            n = CType(o, Long)
            oIsNull = False
        Else
            n = Nothing
            oIsNull = True
        End If
    End Sub

End Module
Public Module mGlobals
    Function WinPath(ByVal sFolder As String) As String
        sFolder = sFolder.Replace("smb://", "\\")
        sFolder = sFolder.Replace("stack://", "")
        sFolder = sFolder.Replace("/", "\")
        Return sFolder
    End Function
    Function WinFile(ByVal sFile As String) As String
        sFile = sFile.Replace("smb://", "\\")
        sFile = sFile.Replace("stack://", "")
        sFile = sFile.Replace("/", "\")
        Dim sArray As String() = sFile.Split("\"c)
        Return sArray.Last.Trim(" "c)
    End Function
    Public Const nColumnHeaderHeight As Integer = 40
    Public oGGCBackgroundStyle As Syncfusion.Drawing.BrushInfo = New BrushInfo(PatternStyle.None, Color.Black, Color.WhiteSmoke)
    Public oGGCBackgroundStyleAlt As Syncfusion.Drawing.BrushInfo = New BrushInfo(PatternStyle.None, Color.Black, System.Drawing.SystemColors.ControlLightLight)

    Public Sub InitaliseGrid(ByVal GGCBaseGrid As GridGroupingControl)
        'maybe does a little too much in here, not sure if ALL grids should have filter on them for example

        GGCBaseGrid.TableDescriptor.AllowEdit = False
        GGCBaseGrid.TableDescriptor.AllowNew = False
        GGCBaseGrid.TableDescriptor.AllowRemove = False
        'GGCBaseGrid.TableOptions.AllowSelection = Syncfusion.Windows.Forms.Grid.GridSelectionFlags.None
        GGCBaseGrid.TableOptions.ListBoxSelectionMode = System.Windows.Forms.SelectionMode.One
        GGCBaseGrid.Text = "GridGroupingControl1"
        GGCBaseGrid.TopLevelGroupOptions.ShowCaption = False
        'GGCBaseGrid.TableControl.CellToolTip.Active = False

        'GGCBaseGrid.TableDescriptor.TableOptions.ColumnHeaderRowHeight = nColumnHeaderHeight
        For Each oCol As GridColumnDescriptor In GGCBaseGrid.TableDescriptor.Columns
            oCol.AllowFilter = True
        Next
        GGCBaseGrid.TopLevelGroupOptions.ShowFilterBar = True
        GGCBaseGrid.TableDescriptor.Appearance.RecordFieldCell.Interior = oGGCBackgroundStyle
        GGCBaseGrid.TableDescriptor.Appearance.AlternateRecordFieldCell.Interior = oGGCBackgroundStyleAlt

        GGCBaseGrid.AutoPopulateRelations = False
        GGCBaseGrid.Engine.ShowNestedPropertiesFields = False
        GGCBaseGrid.TableModel.Options.ActivateCurrentCellBehavior = GridCellActivateAction.SelectAll

    End Sub
End Module
Module mSerialization
    Public Sub SerializeItem(ByVal o As Object)
        'could use the type as the filename, but going to use a static field instead
        'Dim filename As String = o.GetType.ToString & ".xml"
        Dim sFilename As String = o.GetType.GetField("sFilename").GetValue(Nothing).ToString
        Dim oFileStream As New FileStream(sFilename, FileMode.Create)

        Dim oXmlSerializer As New Xml.Serialization.XmlSerializer(o.GetType)
        oXmlSerializer.Serialize(oFileStream, o)
        oFileStream.Close()

    End Sub
    Public Function DeserializeItem(ByVal t As Type) As Object
        'could use the type as the filename, but going to use a static field instead
        'Dim filename As String = t.ToString & ".xml"
        Dim sFilename As String = t.GetField("sFilename").GetValue(Nothing).ToString
        Dim o As Object = Nothing
        Dim oXmlSerializer As New Xml.Serialization.XmlSerializer(t)
        If File.Exists(sFilename) Then
            Dim oFileStream As New FileStream(sFilename, FileMode.Open)
            'deserializing an object will create a new object
            o = oXmlSerializer.Deserialize(oFileStream)
            oFileStream.Close()
        Else
            'cannot return nothing, so use reflection to create a new object of the correct type
            o = Activator.CreateInstance(t)
        End If
        Return o
    End Function
End Module