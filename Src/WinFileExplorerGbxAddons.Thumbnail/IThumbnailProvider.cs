using System.Runtime.InteropServices;

namespace WinFileExplorerGbxAddons.Thumbnail;

[ComVisible(true)]
[Guid("e357fccd-a995-4576-b01f-234630154e96")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IThumbnailProvider
{
    void GetThumbnail(int cx, out nint phbmp, out WTS_ALPHATYPE pdwAlpha);
}
