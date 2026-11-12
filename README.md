# RepairCar

[![Build](https://github.com/qiangqiang101/FH2_RepairCar.NET/actions/workflows/build.yml/badge.svg)](https://github.com/qiangqiang101/FH2_RepairCar.NET/actions/workflows/build.yml)

A small Forza Horizon 2 mod, written in VB.NET for [FH2 Mod Platform for .NET](https://github.com/qiangqiang101/FH2-ModPlatform.NET). It adds a **Repair** option to ANNA's **Car** menu, which fixes all damage on your current car for **$100** credits.

## Features

- **ANNA > Car > Repair > Yes** repairs the car you're driving and takes 100 credits.
- A notification confirms the repair, with your car's name.
- The menu text is in 13 languages: English, Portuguese (BR), German, French, Spanish (ES/MX), Italian, Japanese, Korean, Traditional Chinese, Polish, Russian and Dutch.
- Works online as well as offline.
- Update notices: the platform tells you in game when a newer release is out.

## Requirements

- Forza Horizon 2 (Azimuth) with the [FH2 Mod Platform](https://github.com/Trinculo54/fh2-mod-platform) 2.8.0 or later. No .NET runtime or Platform.NET needed.

## Installing

Download `RepairCar-<version>-NativeAOT.zip` from [Releases](https://github.com/qiangqiang101/FH2_RepairCar.NET/releases) and extract it into your Azimuth folder (the one with `azimuth.exe`). It installs one DLL, `fh2-mods\RepairCar.dll`, and needs no .NET runtime. To remove the mod, delete that file.

## Usage

In game, open ANNA, go to **Car > Repair** and pick **Yes**. You need to be in a car.

## Building from source

The project imports the SDK's MSBuild files from a sibling checkout, so clone both repos into the same folder:

```
GitHub\
├── FH2-ModPlatform.NET\     https://github.com/qiangqiang101/FH2-ModPlatform.NET
└── FH2_RepairCar.NET\       this repo
```

You also need:

- The .NET 10 SDK (see [global.json](global.json)).
- The `FH2ModPlatform` NuGet package (the version in [RepairCar.vbproj](RepairCar/RepairCar.vbproj)). Download `FH2ModPlatform.<version>.nupkg` from the SDK's [releases](https://github.com/qiangqiang101/FH2-ModPlatform.NET/releases) into a folder and add that folder as a source: `dotnet nuget add source <folder> --name FH2ModPlatform`.
- For NativeAOT: Visual Studio's **Desktop development with C++** workload (for the linker).

Open `RepairCar/RepairCar.slnx` in Visual Studio, or from the command line:

```sh
# Hosted: bin/Release/net10.0/
dotnet build RepairCar/RepairCar.vbproj -c Release

# NativeAOT: bin/Release/net10.0/win-x64/publish/RepairCar.dll
dotnet publish RepairCar/RepairCar.vbproj -c Release -p:FH2ModHosting=Aot
```

To have builds copied straight into the game, set `FH2ModsDir` to your `fh2-mods` folder, for example in `RepairCar.vbproj.user` (not committed):

```xml
<Project>
  <PropertyGroup>
    <FH2ModsDir>D:\Games\FH2\azimuth-windows\fh2-mods</FH2ModsDir>
  </PropertyGroup>
</Project>
```

### Translations

The texts are in [RepairCar/Resources/lang.json](RepairCar/Resources/lang.json). Each English text maps a language code to its translation. Missing entries fall back to English.

## Releasing

[.github/workflows/build.yml](.github/workflows/build.yml) builds the NativeAOT zip when a release is published, or when you start it with **Run workflow** on the Actions tab, and uploads it as an artifact. To release:

1. Bump the version in `ModInfo` in [RepairCar.vb](RepairCar/RepairCar.vb).
2. Publish a GitHub release tagged with that version (`1.0.1` or `v1.0.1`). The workflow fails if they differ, because the in-game update check compares the two.
3. The workflow attaches the zip to the release.

## License

[MIT](LICENSE) © 2026 Bartholomew Ho
