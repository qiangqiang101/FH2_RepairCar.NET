Imports FH2
Imports FH2ModPlatform

Public Class RepairCar
    Inherits [Mod]

    Dim notify As String = "Repaired your {0} for $100"

    Public Overrides ReadOnly Property Info As ModInfo
        Get
            Return New ModInfo("RepairCar", "1.0.0") With {
                .Description = "Repair Player's Car.",
                .CheckUpdates = True,
                .GithubRepo = "qiangqiang101/FH2_RepairCar.NET",
                .OfflineOnly = False
            }
        End Get
    End Property

    Protected Overrides Sub OnInit()
        Log("RepairCar Mod Loaded.")

        Lang.Init()

        repairItem = ANNA.Items("Car").Add("Repair", Nothing)
        Dim ask = ANNA.List(repairItem.Key)
        yesItem = ask.Add("Yes", Sub() RepairPlayerCar())
        noItem = ask.Add("No", Sub() Return)
        AddHandler Game.Tick, AddressOf TranslateWhenReady
    End Sub

    Protected Overrides Sub OnUnload()
        RemoveHandler Game.Tick, AddressOf TranslateWhenReady
    End Sub

    Private repairItem As ANNA.MenuItem
    Private yesItem As ANNA.MenuItem
    Private noItem As ANNA.MenuItem

    Private Sub TranslateWhenReady()
        If Not Lang.IsReady Then Return
        RemoveHandler Game.Tick, AddressOf TranslateWhenReady

        repairItem.Label = Tr("Repair")
        yesItem.Label = Tr("Yes")
        noItem.Label = Tr("No")
        notify = Tr("Repaired your {0} for $100")
    End Sub

    Private Sub RepairPlayerCar()
        Dim car = Player.Car
        If car Is Nothing Then Return
        car.Repair()
        Player.Credits -= 100
        UI.Notify("ANNA", String.Format(notify, If(car.Model?.FullName, "car")))
    End Sub

End Class
