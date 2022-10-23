﻿using System.Runtime.InteropServices;
using NAPS2.Dependencies;
using NAPS2.Platform.Windows;

namespace NAPS2.Platform;

public abstract class WindowsSystemCompat : ISystemCompat
{
    public bool IsWiaDriverSupported => true;

    public bool IsWia20Supported => PlatformSupport.ModernWindows.Validate();

    public bool IsTwainDriverSupported => true;

    public bool IsAppleDriverSupported => false;

    public bool IsSaneDriverSupported => false;

    public bool CanUseWin32 => true;

    public bool UseSystemTesseract => false;

    public bool RenderInWorker => true;

    public abstract string[] LibrarySearchPaths { get;  }

    public string TesseractExecutableName => "tesseract.exe";

    public string PdfiumLibraryName => "pdfium.dll";

    public string[]? SaneLibraryDeps => null;

    public string SaneLibraryName => "sane.dll";

    public IntPtr LoadLibrary(string path) => Win32.LoadLibrary(path);

    public string GetLoadError() => Marshal.GetLastWin32Error().ToString();

    public abstract IntPtr LoadSymbol(IntPtr libraryHandle, string symbol);

    public void SetEnv(string name, string value) => throw new NotSupportedException();

    public IDisposable? FileReadLock(string path) => null;

    public IDisposable? FileWriteLock(string path) => null;
}