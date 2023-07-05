using System.Runtime.InteropServices;

namespace WinFileExplorerGbxAddons.IconOverlay;

public sealed partial class ShellInterop
{
    private ShellInterop()
    {
    }

    [DllImport("shell32.dll")]
    internal static extern void SHChangeNotify(int eventID, uint flags, nint item1, nint item2);
}