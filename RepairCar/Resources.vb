Imports System.IO

Friend Module Resources

    Friend Function Bytes(name As String) As Byte()
        Using stream = GetType(Resources).Assembly.GetManifestResourceStream(name)
            If stream Is Nothing Then Throw New FileNotFoundException("Not in the mod: " & name)
            Using copy As New MemoryStream()
                stream.CopyTo(copy)
                Return copy.ToArray()
            End Using
        End Using
    End Function

End Module

