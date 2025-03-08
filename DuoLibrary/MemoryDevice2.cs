﻿using System;
using System.IO.MemoryMappedFiles;
using System.Diagnostics;
using DuoLibrary;

static class MemoryDevice
{
    private const string DeviceFile = "/dev/mem";
    private static MemoryMappedFile _mmf;
    private static nint _fileHandle;

    static MemoryDevice()
    {

        Console.WriteLine($"Openiing device {DeviceFile}  Memory Size {Device.MaxOffset}");

        var file = File.Open(DeviceFile, FileMode.Open, FileAccess.ReadWrite);


        // Create memory-mapped file with the actual memory size
        _mmf = MemoryMappedFile.CreateFromFile(file, null, Device.BaseAddress_Pinmux + Device.MaxOffset+4,
            MemoryMappedFileAccess.ReadWrite, HandleInheritability.None, false);
        _fileHandle = _mmf.SafeMemoryMappedFileHandle.DangerousGetHandle();
    }

    public static unsafe uint ReadUint32(uint address)
    {
        lock (_mmf)
        {
            using (var accessor = _mmf.CreateViewAccessor(address, 4, MemoryMappedFileAccess.Read))
            {
                byte* ptr = null;
                try
                {
                    accessor.SafeMemoryMappedViewHandle.AcquirePointer(ref ptr);
                    return *(uint*)ptr;
                }
                finally
                {
                    if (ptr != null)
                        accessor.SafeMemoryMappedViewHandle.ReleasePointer();
                }
            }
        }
    }

    public static unsafe void WriteUint32(uint address, uint value)
    {
        lock (_mmf)
        {
            using (var accessor = _mmf.CreateViewAccessor(address, 4, MemoryMappedFileAccess.Write))
            {
                byte* ptr = null;
                try
                {
                    accessor.SafeMemoryMappedViewHandle.AcquirePointer(ref ptr);
                    *(uint*)ptr = value;
                }
                finally
                {
                    if (ptr != null)
                        accessor.SafeMemoryMappedViewHandle.ReleasePointer();
                }
            }
        }
    }
}