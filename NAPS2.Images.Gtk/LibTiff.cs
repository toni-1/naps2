using System.Runtime.InteropServices;
using toff_t = System.IntPtr;
using tsize_t = System.IntPtr;
using thandle_t = System.IntPtr;
using tdata_t = System.IntPtr;

namespace NAPS2.Images.Gtk;

public static class LibTiff
{
    // TODO: String marshalling?
    [DllImport("libtiff.so.5")]
    public static extern IntPtr TIFFOpen(string filename, string mode);

    [DllImport("libtiff.so.5")]
    public static extern IntPtr TIFFSetErrorHandler(TIFFErrorHandler handler);

    [DllImport("libtiff.so.5")]
    public static extern IntPtr TIFFSetWarningHandler(TIFFErrorHandler handler);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TIFFErrorHandler(string x, string y, IntPtr va_args);

    [DllImport("libtiff.so.5")]
    public static extern IntPtr TIFFClientOpen(string filename, string mode, IntPtr clientdata,
        TIFFReadWriteProc readproc, TIFFReadWriteProc writeproc, TIFFSeekProc seekproc, TIFFCloseProc closeproc,
        TIFFSizeProc sizeproc, TIFFMapFileProc mapproc, TIFFUnmapFileProc unmapproc);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate tsize_t TIFFReadWriteProc(thandle_t clientdata, tdata_t data, tsize_t size);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate toff_t TIFFSeekProc(thandle_t clientdata, toff_t off, int c);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int TIFFCloseProc(thandle_t clientdata);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate toff_t TIFFSizeProc(thandle_t clientdata);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int TIFFMapFileProc(thandle_t clientdata, ref tdata_t a, ref toff_t b);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TIFFUnmapFileProc(thandle_t clientdata, tdata_t a, toff_t b);

    [DllImport("libtiff.so.5")]
    public static extern IntPtr TIFFClose(IntPtr tiff);

    [DllImport("libtiff.so.5")]
    public static extern short TIFFNumberOfDirectories(IntPtr tiff);

    [DllImport("libtiff.so.5")]
    public static extern int TIFFReadDirectory(IntPtr tiff);

    [DllImport("libtiff.so.5")]
    public static extern int TIFFGetField(IntPtr tiff, int tag, out int field);

    [DllImport("libtiff.so.5")]
    public static extern int TIFFReadRGBAImage(
        IntPtr tiff, int w, int h, IntPtr raster, int stopOnError);

    [DllImport("libtiff.so.5")]
    public static extern int TIFFReadRGBAImageOriented(
        IntPtr tiff, int w, int h, IntPtr raster, int orientation, int stopOnError);

    // TODO: For streams
    // https://linux.die.net/man/3/tiffclientopen
}