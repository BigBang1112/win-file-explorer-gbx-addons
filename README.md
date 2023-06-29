![Universe Gbx Preview for Windows File Explorer](UniverseGbxPreview.png)

<p align="center">Happy to present a couple of addons to make Gbx file scrolling just a little more comfortable on Windows.</p>

## Thumbnail / Icon

**Package name: `WinFileExplorerGbxPreview.Thumbnail`**

1. Shows thumbnail on all `CGameCtnChallenge` files:
    - Challenge.Gbx
    - Map.Gbx
2. Shows icons on all `CGameCtnCollector` files:
    - Item.Gbx / Block.Gbx / ObjectInfo.Gbx
    - Macroblock.Gbx
    - Decoration.Gbx
    - (TM)ED___.Gbx

## Gbx Icon Overlay

**Package name: `WinFileExplorerGbxPreview.IconOverlay`**

Shows a small Gbx icon on top of the file thumbnail, making it easier to distinguish image file from Gbx file for example.

## Installation

**.NET 7 Runtime is required for these addons to work,** see below for the easiest ways to install it.

### Recommended (via WinGet)

> !!! All WinGet packages coming from the BigBang1112 GitHub account will be available **ONLY on https://winget.bigbang1112.cz** !!!

1. Install [WinGet](https://www.microsoft.com/p/app-installer/9nblggh4nns1) via Microsoft Store.
2. In your Start Menu, type `cmd`, press Enter.
3. Type these commands below (individually):

```
winget install Microsoft.DotNet.Runtime.7
winget source add --name BigBang1112 https://winget.bigbang1112.cz -t Microsoft.Rest
winget install WinFileExplorerGbxPreview.Thumbnail
winget install WinFileExplorerGbxPreview.IconOverlay
```

Each `WinFileExplorerGbxPreview` package is independent, you can only install those that you want.

### Alternative (manually via .msi file download)

TODO
