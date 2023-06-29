using Microsoft.Win32;
using System.Runtime.InteropServices;

namespace WinFileExplorerGbxPreview.IconOverlay;

[ComVisible(true)]
[Guid("dabfa3b2-7f7b-4dd9-a8b6-29bf7a3c0879")]
public partial class GbxIconOverlay : IShellIconOverlayIdentifier
{
    int IShellIconOverlayIdentifier.IsMemberOf(string path, uint attributes)
    {
        try
        {
            unchecked
            {
                return File.Exists(path) && Path.GetExtension(path).ToLowerInvariant() == ".gbx" ? (int)HRESULT.S_OK : (int)HRESULT.S_FALSE;
            }
        }
        catch
        {
            unchecked
            {
                return (int)HRESULT.E_FAIL;
            }
        }
    }

    int IShellIconOverlayIdentifier.GetPriority(out int priority)
    {
        priority = 0;
        return (int)HRESULT.S_OK;
    }

    public int GetOverlayInfo(nint iconFileBuffer, int iconFileBufferSize, out int iconIndex, out uint flags)
    {
        var iconFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "WinFileExplorerGbxPreview", "gbx.ico");
        var bytes = System.Text.Encoding.Unicode.GetBytes(iconFile);

        if (bytes.Length + 2 < iconFileBufferSize)
        {
            for (var i = 0; i < bytes.Length; i++)
            {
                Marshal.WriteByte(iconFileBuffer, i, bytes[i]);
            }

            Marshal.WriteByte(iconFileBuffer, bytes.Length, 0);
            Marshal.WriteByte(iconFileBuffer, bytes.Length + 1, 0);
        }

        iconIndex = 0;
        flags = (int)(HFLAGS.ISIOI_ICONFILE | HFLAGS.ISIOI_ICONINDEX);

        return (int)HRESULT.S_OK;
    }

    [ComRegisterFunction]
    public static void RegisterFunction(Type t)
    {
        var regKey = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\ShellIconOverlayIdentifiers\" + t.Name);
        regKey.SetValue("", t.GUID.ToString("B"));
        regKey.Close();
        ShellInterop.SHChangeNotify(0x08000000, 0, nint.Zero, nint.Zero);
    }

    [ComUnregisterFunction]
    public static void UnregisterFunction(Type t)
    {
        Registry.LocalMachine.DeleteSubKeyTree(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\ShellIconOverlayIdentifiers\" + t.Name);
        ShellInterop.SHChangeNotify(0x08000000, 0, nint.Zero, nint.Zero);
    }
}