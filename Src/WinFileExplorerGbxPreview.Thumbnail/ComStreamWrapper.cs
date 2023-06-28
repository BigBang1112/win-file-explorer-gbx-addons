using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace WinFileExplorerGbxPreview.Thumbnail;

public class ComStreamWrapper : Stream
{
    private readonly IStream mSource;
    private readonly nint mInt64;

    public ComStreamWrapper(IStream source)
    {
        mSource = source;
        mInt64 = Marshal.AllocCoTaskMem(sizeof(ulong));
    }

    ~ComStreamWrapper()
    {
        Marshal.FreeCoTaskMem(mInt64);
    }

    public override bool CanRead => true;
    public override bool CanSeek => true;
    public override bool CanWrite => true;

    public override void Flush()
    {
        mSource.Commit(0);
    }

    public override long Length
    {
        get
        {
            mSource.Stat(out STATSTG stat, 1);
            return stat.cbSize;
        }
    }

    public override long Position
    {
        get => throw new NotImplementedException();
        set => throw new NotImplementedException();
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
        if (offset != 0)
        {
            throw new NotImplementedException();
        }

        mSource.Read(buffer, count, mInt64);

        return Marshal.ReadInt32(mInt64);
    }

    public override long Seek(long offset, SeekOrigin origin)
    {
        mSource.Seek(offset, (int)origin, mInt64);

        return Marshal.ReadInt64(mInt64);
    }

    public override void SetLength(long value)
    {
        mSource.SetSize(value);
    }

    public override void Write(byte[] buffer, int offset, int count)
    {
        if (offset != 0)
        {
            throw new NotImplementedException();
        }

        mSource.Write(buffer, count, nint.Zero);
    }
}
