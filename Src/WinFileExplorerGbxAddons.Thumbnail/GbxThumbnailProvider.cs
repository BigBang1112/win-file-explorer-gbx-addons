using GBX.NET;
using GBX.NET.Engines.Game;
using GBX.NET.Engines.GameData;
using GBX.NET.Imaging;
using Microsoft.Win32;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace WinFileExplorerGbxAddons.Thumbnail;

[ComVisible(true)]
[Guid("41694c1a-d860-4fbe-855b-c9dbdf5b7057")]
[ClassInterface(ClassInterfaceType.None)]
public class GbxThumbnailProvider : IThumbnailProvider, IInitializeWithStream
{
    private IStream? stream;

    public void Initialize(IStream stream, uint grfMode)
    {
        this.stream = stream;
    }

    /// <summary>
    /// Gets the thumbnail.
    /// </summary>
    /// <remarks>This method is not testable.</remarks>
    /// <param name="cx">Width of the thumbnail.</param>
    /// <param name="phbmp">HBITMAP.</param>
    /// <param name="pdwAlpha">The alpha type of the thumbnail (e.g., whether it has transparency). Set pdwAlpha to WTS_ALPHATYPE.WTSAT_UNKNOWN if the alpha type is unknown.</param>
    public void GetThumbnail(int cx, out nint phbmp, out WTS_ALPHATYPE pdwAlpha)
    {
        pdwAlpha = WTS_ALPHATYPE.WTSAT_ARGB;

        // Set phbmp to nint.Zero if the thumbnail cannot be generated
        phbmp = IntPtr.Zero;

        if (stream is null)
        {
            return;
        }

        using var streamWrap = new ComStreamWrapper(stream);

        var bmp = GetBitmap(streamWrap, size: cx);

        if (bmp is null)
        {
            return;
        }

        // Convert the thumbnail to an HBITMAP handle
        phbmp = bmp.GetHbitmap();
    }

    internal static Bitmap? GetBitmap(Stream stream, int size)
    {
        try
        {
            var node = GameBox.ParseNodeHeader(stream);

            if (node is null)
            {
                return null;
            }

            return GetBitmap(node, size);
        }
        catch
        {
            return null;
        }
    }

    internal static Bitmap? GetBitmap(Node node, int size) => node switch
    {
        CGameCtnChallenge map => GetThumbnailBitmap(map, size),
        CGameCtnCollector collector => GetIconBitmap(collector, size),
        _ => null
    };

    internal static Bitmap? GetThumbnailBitmap(CGameCtnChallenge map, int size)
    {
        using var thumbnail = map.GetThumbnailBitmap();

        if (thumbnail is null)
        {
            return null;
        }

        var width = thumbnail.Width;
        var height = thumbnail.Height;

        Resize(ref width, ref height, size);

        return new Bitmap(thumbnail, width, height);
    }

    internal static Bitmap? GetIconBitmap(CGameCtnCollector collector, int size)
    {
        var bitmap = collector.GetIconBitmap();

        if (bitmap is not null || collector.IconWebP is null)
        {
            return bitmap;
        }

        using var webpStream = new MemoryStream(collector.IconWebP);
        using var image = SixLabors.ImageSharp.Image.Load(webpStream);

        var width = image.Width;
        var height = image.Height;

        Resize(ref width, ref height, size);

        image.Mutate(x =>
        {
            x.Resize(width, height);
            x.RotateFlip(RotateMode.Rotate180, FlipMode.Horizontal);
        });

        using var bmpStream = new MemoryStream();

        image.SaveAsPng(bmpStream);

        bmpStream.Position = 0;

        return new Bitmap(bmpStream);
    }

    internal static void Resize(ref int width, ref int height, int size)
    {
        if (size > width && size > height)
        {
            return;
        }

        if (width > height)
        {
            var ratio = (double)width / height;
            width = size;
            height = (int)(size / ratio);
        }
        else
        {
            var ratio = (double)height / width;
            height = size;
            width = (int)(size / ratio);
        }
    }

    /// <remarks>This method is not testable.</remarks>
    [ComRegisterFunction]
    public static void RegisterFunction(Type t)
    {
        var regKey = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Classes\.gbx\ShellEx\{e357fccd-a995-4576-b01f-234630154e96}");
        regKey.SetValue("", t.GUID.ToString("B").ToUpperInvariant());
        regKey.Close();
        ShellInterop.SHChangeNotify(0x08000000, 0, IntPtr.Zero, IntPtr.Zero);
    }

    /// <remarks>This method is not testable.</remarks>
    [ComUnregisterFunction]
#pragma warning disable IDE0060
    public static void UnregisterFunction(Type t)
#pragma warning restore IDE0060
    {
        var regKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Classes\.gbx\ShellEx\{e357fccd-a995-4576-b01f-234630154e96}", true);

        if (regKey is null)
        {
            return;
        }

        regKey.DeleteValue("", false);
        regKey.Close();

        ShellInterop.SHChangeNotify(0x08000000, 0, IntPtr.Zero, IntPtr.Zero);
    }
}
