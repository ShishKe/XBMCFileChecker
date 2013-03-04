Imports System.Runtime.Serialization
Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary
Imports XBMCDataLayer

' This is a console application.  
Public Class Test


    Public Shared Sub Main()
        ' This is the name of the file holding the data. You can use any file extension you like. 
        Dim fileName As String = "dataStuff.myData"

        ' Use a BinaryFormatter or SoapFormatter. 
        Dim formatter As IFormatter = New BinaryFormatter()
        'Dim formatter As IFormatter = New SoapFormatter()

        Test.SerializeItem(fileName, formatter)
        ' Serialize an instance of the class.
        Test.DeserializeItem(fileName, formatter)
        ' Deserialize the instance.
        Console.WriteLine("Done")
        Console.ReadLine()
    End Sub

    Public Shared Sub SerializeItem(ByVal fileName As String, ByVal formatter As IFormatter)
        ' Create an instance of the type and serialize it. 
        Dim oExclude As New cExclude()
        oExclude.sRule = "Rule"
        oExclude.sDescription = "Description"

        Dim oExcludes As New cExcludes
        oExcludes.Add(oExclude)
        oExcludes.Add(oExclude)

        Dim x As New Xml.Serialization.XmlSerializer(oExclude.GetType)
        x.Serialize(Console.Out, oExclude)

        Console.WriteLine()
        Console.WriteLine()
        Console.WriteLine()
        Dim y As New Xml.Serialization.XmlSerializer(oExcludes.GetType)
        y.Serialize(Console.Out, oExcludes)
        Dim fs As New FileStream(fileName, FileMode.Create)
        y.Serialize(fs, oExcludes)
        fs.Close()

        Console.WriteLine()
        Console.ReadLine()
        'Dim fs As New FileStream(fileName, FileMode.Create)
        'formatter.Serialize(fs, oExclude)
        'fs.Close()
    End Sub


    Public Shared Sub DeserializeItem(ByVal fileName As String, ByVal formatter As IFormatter)
        Dim oExcludes As New cExcludes
        Dim y As New Xml.Serialization.XmlSerializer(oExcludes.GetType)
        Dim fs As New FileStream(fileName, FileMode.Open)
        oExcludes = y.Deserialize(fs)

        fs.Close()
        For Each oExclude As cExclude In oExcludes
            Console.WriteLine(oExclude.sRule)
            Console.WriteLine(oExclude.sDescription)
        Next

        'Dim fs As New FileStream(fileName, FileMode.Open)

        'Dim oExclude As cExclude = DirectCast(formatter.Deserialize(fs), cExclude)
        'Console.WriteLine(oExclude.sRule)
        'Console.WriteLine(oExclude.sDescription)
    End Sub
End Class