using System.Runtime.InteropServices;

namespace WinFileExplorerGbxAddons.IconOverlay;

public sealed partial class ShellInterop
{
    private ShellInterop()
    {
    }

    [LibraryImport("shell32.dll")]
    internal static partial void SHChangeNotify(int eventID, uint flags, nint item1, nint item2);
}