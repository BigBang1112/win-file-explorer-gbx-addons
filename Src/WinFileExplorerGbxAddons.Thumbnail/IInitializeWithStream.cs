using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace WinFileExplorerGbxAddons.Thumbnail;

[ComVisible(true)]
[Guid("b824b49d-22ac-4161-ac8a-9916e8fa3f7f")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IInitializeWithStream
{
    void Initialize(IStream stream, uint grfMode);
}
