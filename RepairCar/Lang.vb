Imports System.Runtime
Imports System.Text.Json
Imports FH2

Friend Module Lang

    Private ReadOnly Texts As New Dictionary(Of String, Dictionary(Of String, String))
    Private ReadOnly Codes As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)

    Friend Sub Init()
        Using doc = JsonDocument.Parse(Resources.Bytes("lang.json"))
            For Each language In doc.RootElement.GetProperty("codes").EnumerateArray()
                Codes.Add(language.GetString())
            Next
            For Each entry In doc.RootElement.GetProperty("texts").EnumerateObject()
                Dim byCode As New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)
                For Each translation In entry.Value.EnumerateObject()
                    byCode(translation.Name) = translation.Value.GetString()
                Next
                Texts(entry.Name) = byCode
            Next
        End Using
    End Sub

    ''' <summary>True once the game has loaded its text, so <see cref="Tr"/> can translate (it's Nothing at OnInit).</summary>
    Friend ReadOnly Property IsReady As Boolean
        Get
            Return Game.Language IsNot Nothing
        End Get
    End Property

    ''' <summary>The text in the game's language; English until the game has loaded its text.</summary>
    Friend Function Tr(english As String) As String
        Dim code = Game.Language
        If code Is Nothing Then Return english
        Dim byCode As Dictionary(Of String, String) = Nothing
        Dim translated As String = Nothing
        If Texts.TryGetValue(english, byCode) AndAlso byCode.TryGetValue(code, translated) Then Return translated
        Return english
    End Function


End Module
