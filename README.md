![Universe Gbx Addons for Windows File Explorer](UniverseGbxAddons.png)

<div align="center">

[![GitHub release (latest by date including pre-releases)](https://img.shields.io/github/v/release/BigBang1112/win-file-explorer-gbx-addons?include_prereleases&style=for-the-badge)](https://github.com/BigBang1112/win-file-explorer-gbx-addons/releases) [![GitHub all releases](https://img.shields.io/github/downloads/BigBang1112/win-file-explorer-gbx-addons/total?style=for-the-badge)](https://github.com/BigBang1112/win-file-explorer-gbx-addons/releases)

</div>

<p align="center">Happy to present a couple of addons to make Gbx file scrolling just a little more comfortable on Windows.</p>

<p align="center">Due to the usage of COM, these addons are supported widely across alternative file browsers on Windows.</p>

<p align="center">Powered by <a href="https://github.com/BigBang1112/gbx-net">GBX.NET</a>.</p>

## Thumbnail / Icon

**Package name: `WinFileExplorerGbxAddons.Thumbnail`**

![WinFileExplorerGbxAddons.Thumbnail example](Addon_Thumbnail_Example.jpg)

- Shows thumbnail on all `CGameCtnChallenge` files:
    - Challenge.Gbx
    - Map.Gbx
- Shows icons on all `CGameCtnCollector` files:
    - Item.Gbx / Block.Gbx / ObjectInfo.Gbx
    - Macroblock.Gbx
    - Decoration.Gbx
    - (TM)ED___.Gbx
    - Also works for modern WEBP icons

## Gbx Icon Overlay

**Package name: `WinFileExplorerGbxAddons.IconOverlay`**

![WinFileExplorerGbxAddons.IconOverlay example](Addon_IconOverlay_Example.jpg)

Shows a small Gbx icon on top of the file thumbnail, making it easier to distinguish image file from Gbx file for example.

## Installation

**.NET 8 Runtime is required for these addons to work,** see below for the easiest ways to install it.

### Recommended (via WinGet)

> [!WARNING]
> All WinGet packages coming from the BigBang1112 GitHub account will be available **ONLY on https://winget.bigbang1112.cz** !!!

1. Install [WinGet](https://www.microsoft.com/p/app-installer/9nblggh4nns1) via Microsoft Store.
2. In your Start Menu, type `cmd`, and Run as Administrator.
3. Execute these commands below (you can all at once):

```
winget install Microsoft.DotNet.Runtime.8
winget source add --name BigBang1112 https://winget.bigbang1112.cz -t Microsoft.Rest
winget install BigBang1112.WinFileExplorerGbxAddons.Thumbnail --force
winget install BigBang1112.WinFileExplorerGbxAddons.IconOverlay --force
```

> --force is added temporarily because there's a weird ID match bug either caused by installer or winget. Contributions are welcome.

4. Done.

The changes should appear immediately. In some cases, you need to restart File Explorer, log in/out from Windows or restart Windows.

Each `WinFileExplorerGbxAddons` package is independent, you can only install those you want.

> Many of my other projects will be coming soon to WinGet as well!

### Alternative (manually via .msi file download)

1. Install [.NET 8 Runtime (desktop apps)](https://dotnet.microsoft.com/en-us/download/dotnet/8.0/runtime).
2. Download the MSI asset from [the latest release](https://github.com/BigBang1112/win-file-explorer-gbx-addons/releases) of the addons you want (according to your OS).
3. Run the MSI and accept elevated (admin) privileges.
4. Done.

The changes should appear immediately. In rare cases, you need to log in/out from Windows or restart it.

## Build

You need .NET 8 SDK and Wix Toolset to build the solution fully.

In Visual Studio, you can simply install HeatWave extension, which will automatically prepare Wix for you.

To test the addon after building (which is a COM library), use `regsvr32.exe [addon].comhost.dll` to install the addon and `regsvr32.exe /u [addon].comhost.dll` to uninstall it.

## Special thanks

To people sharing their development problems on the internet, this was really hard to put together.

<h2 align="center">#20yearsoftrackmania</h2>
