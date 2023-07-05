using GBX.NET.Engines.Game;
using GBX.NET.Engines.GameData;
using System.Drawing;
using System.Drawing.Imaging;

namespace WinFileExplorerGbxAddons.Thumbnail.Tests;

public class GbxThumbnailProviderTests
{
    [Fact]
    public void Resize_WhenWidthGreaterThanHeight_ShouldResizeWidthAndHeight()
    {
        // Arrange
        int width = 200;
        int height = 100;
        int size = 150;

        // Act
        GbxThumbnailProvider.Resize(ref width, ref height, size);

        // Assert
        Assert.Equal(150, width);
        Assert.Equal(75, height);
    }

    [Fact]
    public void Resize_WhenHeightGreaterThanWidth_ShouldResizeWidthAndHeight()
    {
        // Arrange
        int width = 100;
        int height = 200;
        int size = 150;

        // Act
        GbxThumbnailProvider.Resize(ref width, ref height, size);

        // Assert
        Assert.Equal(75, width);
        Assert.Equal(150, height);
    }

    [Fact]
    public void Resize_WhenHeightAndWidthGreaterThanSize_DoNotResize()
    {
        // Arrange
        int width = 100;
        int height = 200;
        int size = 250;

        // Act
        GbxThumbnailProvider.Resize(ref width, ref height, size);

        // Assert
        Assert.Equal(100, width);
        Assert.Equal(200, height);
    }

    [Fact]
    public void GetThumbnailBitmap_NoThumbnail_ShouldReturnNull()
    {
        // Arrange
        var map = (CGameCtnChallenge)Activator.CreateInstance(typeof(CGameCtnChallenge), nonPublic: true)!;

        // Act
        var bmp = GbxThumbnailProvider.GetThumbnailBitmap(map, size: 100);

        // Assert
        Assert.Null(bmp);
    }

    [Fact]
    public void GetThumbnailBitmap_HasThumbnail_ResizeBitmap()
    {
        // Arrange
        var expectedSize = 100;

        var map = (CGameCtnChallenge)Activator.CreateInstance(typeof(CGameCtnChallenge), nonPublic: true)!;

        using var ms = new MemoryStream();
        using var exampleBmp = new Bitmap(200, 200);
        exampleBmp.Save(ms, ImageFormat.Jpeg);
        map.Thumbnail = ms.ToArray();
        
        // Act
        var bmp = GbxThumbnailProvider.GetThumbnailBitmap(map, expectedSize);

        // Assert
        Assert.NotNull(bmp);
        Assert.Equal(expectedSize, actual: bmp.Width);
        Assert.Equal(expectedSize, actual: bmp.Height);
    }

    [Fact]
    public void GetIconBitmap_NoIcon_ShouldReturnNull()
    {
        // Arrange
        var collector = (CGameCtnCollector)Activator.CreateInstance(typeof(CGameCtnCollector), nonPublic: true)!;

        // Act
        var bmp = GbxThumbnailProvider.GetIconBitmap(collector, size: 100);

        // Assert
        Assert.Null(bmp);
    }

    [Fact]
    public void GetIconBitmap_HasIconWebP_ResizeBitmap()
    {
        // Arrange
        var expectedSize = 64;
        
        var collector = (CGameCtnCollector)Activator.CreateInstance(typeof(CGameCtnCollector), nonPublic: true)!;
        collector.IconWebP = File.ReadAllBytes("icon.webp");

        // Act
        var bmp = GbxThumbnailProvider.GetIconBitmap(collector, expectedSize);

        // Assert
        Assert.NotNull(bmp);
        Assert.Equal(expectedSize, actual: bmp.Width);
        Assert.Equal(expectedSize, actual: bmp.Height);
    }
}
